using System.Linq;
using AutoMapper;
using BusinessServices.UsersService;
using EFRandevouDAL.Messages;
using EFRandevouDAL.Users;
using RandevouData.Messages;

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
            var dao = new MessagesDao();
            var usersDao = new UsersDao();
            var userService = new UserService(mapper);
            
            var sender = usersDao.Get(senderId);
            var receiver = usersDao.Get(receiverId);
            
            var entity = new Message(sender,receiver,content);
            var id = dao.Insert(entity);
            return id;
        }

        public Message[] GetConversation(int user1, int user2)
        {
            var dao = new MessagesDao();
            var conversation = dao.QueryMessages()
            .Where(x=>  (x.FromUser.Id == user1 && x.ToUser.Id == user2)
                    || (x.FromUser.Id == user2 && x.ToUser.Id == user1)
                ).OrderByDescending(x=>x.SendDate).ToArray();

            return conversation;        
        }



    }
}