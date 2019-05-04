using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessServices.UsersService;
using EFRandevouDAL.Messages;
using EFRandevouDAL.Users;
using RandevouData.Messages;
using EFRandevouDAL;
using System;

namespace BusinessServices.MessageService
{
    public class MessagesService : IMessagesService
    {

        IMapper mapper;
        public MessagesService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public int SendMessage(int senderId, int receiverId, string content)
        {
            using (var dbc = new RandevouBusinessDbContext())
            { 
                var dao = new MessagesDao(dbc);
                var usersDao = new UsersDao(dbc);
                var userService = new UserService(mapper);
            
                var sender = usersDao.Get(senderId);
                var receiver = usersDao.Get(receiverId);
            
                var entity = new Message(sender,receiver,content);
                var id = dao.Insert(entity);
                return id;
            }
        }

        public IEnumerable<MessageDto> GetConversationBetweenUsers(int user1Id, int user2Id)
        {
            using (var dbc = new RandevouBusinessDbContext())
            {
                var dao = new MessagesDao(dbc);

                var messagesFromFistUser = dao.QueryMessages()
                    .Where(x => x.FromUser.Id == user1Id && x.ToUser.Id == user2Id)
                    .Select(x => new MessageDto()
                    {
                        Content = x.MessageContent,
                        MessageId = x.Id,
                        ReceiverId = user2Id,
                        SenderId = user1Id,
                        ReceiverName = x.ToUser.Name,
                        SenderName = x.FromUser.Name,
                        SendDate = x.SendDate,
                        IsRead = x.ReadDate.HasValue,
                    }).ToArray();

                var messagesFromSecondUser = dao.QueryMessages()
                    .Where(x => x.FromUser.Id == user2Id && x.ToUser.Id == user1Id)
                    .Select(x => new MessageDto()
                    {
                        Content = x.MessageContent,
                        MessageId = x.Id,
                        ReceiverId = user1Id,
                        SenderId = user2Id,
                        ReceiverName = x.ToUser.Name,
                        SenderName = x.FromUser.Name,
                        SendDate = x.SendDate,
                        IsRead = x.ReadDate.HasValue,
                    }).ToArray();


                var fullConversation = messagesFromFistUser.Concat(messagesFromSecondUser).OrderByDescending(x => x.SendDate);
                return fullConversation;
            }
        }

        public MessageDto GetMessage(int id)
        {
            using (var dbc = new RandevouBusinessDbContext())
            {
                var dao = new MessagesDao(dbc);
                var message = dao.Get(id);

                return mapper.Map<Message, MessageDto>(message);
            }
        }

        public IEnumerable<int> GetUserConversationsSpeakers(int userId)
        {
            using (var dbc = new RandevouBusinessDbContext())
            {
                var dao = new MessagesDao(dbc);
                var speakersIds = dao.QueryMessages()
                                    .Where(x => x.FromUser.Id == userId)
                                    .Select(x => x.ToUser.Id)
                                    .Concat(
                                    dao.QueryMessages()
                                    .Where(x => x.ToUser.Id == userId)
                                    .Select(x => x.FromUser.Id)
                                    ).Distinct();
                var result = speakersIds.ToArray();
                return result;
            }
        }

        /// <summary>
        /// paginantion will be needed!
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<LastMessagesFromConversationsDto> GetConversationsLastMessages(int userId)
        {
            var result = new List<LastMessagesFromConversationsDto>();
            var speakersIds = GetUserConversationsSpeakers(userId);
            using (var dbc = new RandevouBusinessDbContext())
            {
                var userDao = new UsersDao(dbc);

                var usersData = userDao.QueryUsers().Where(x => speakersIds.Contains(x.Id))
                    .ToDictionary(x => x.Id, y => y.Name);

                var messagesDao = new MessagesDao(dbc);

                foreach(var id in speakersIds)
                {
                    var lastMessageBetweenUsers = messagesDao.QueryMessages()
                         .Where(x =>
                            (x.FromUser.Id == userId && x.ToUser.Id == id)
                            || (x.FromUser.Id == id && x.ToUser.Id == userId)
                            ).OrderByDescending(x => x.SendDate)
                            .Select(x => new LastMessagesFromConversationsDto()
                            {
                                 SpeakerId = id,
                                 SpeakerName = usersData[id],
                                 MessageDate = x.SendDate,
                                 MessageShortContent = x.MessageContent,
                                 IsRead = x.ReadDate.HasValue,
                            }).FirstOrDefault();

                    if (lastMessageBetweenUsers.MessageShortContent.Length>100)
                    {
                        lastMessageBetweenUsers.MessageShortContent = lastMessageBetweenUsers.MessageShortContent.Substring(0, 100);
                        lastMessageBetweenUsers.MessageShortContent += "...";
                    }

                    result.Add(lastMessageBetweenUsers);
                }
                return result.ToArray();
            }

        }

        public void MarkMessageRead(int messageId, int ownerId)
        {
            using (var dbc = new RandevouBusinessDbContext())
            {
                var dao = new MessagesDao(dbc);
                var messageToMark = dao.QueryMessages().Where(x => x.Id == messageId && x.ToUser.Id == ownerId).FirstOrDefault();
                if (messageToMark == null)
                    throw new ArgumentOutOfRangeException(nameof(messageId));

                messageToMark.ReadDate = DateTime.Now;
                dao.Update(messageToMark);
            }
        }

        public void MarkMessageUnread(int messageId, int ownerId)
        {
            using (var dbc = new RandevouBusinessDbContext())
            {
                var dao = new MessagesDao(dbc);
                var messageToMark = dao.QueryMessages().Where(x => x.Id == messageId && x.ToUser.Id == ownerId).FirstOrDefault();
                if (messageToMark == null)
                    throw new ArgumentOutOfRangeException(nameof(messageId));

                messageToMark.ReadDate = null;
                dao.Update(messageToMark);
            }
        }
    }
}