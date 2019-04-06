using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessServices.MessageService;
using BusinessServices.Tests.Helper;
using EFRandevouDAL.Messages;
using EFRandevouDAL.Users;
using RandevouData.Messages;
using RandevouData.Users;
using Xunit;

namespace BusinessServices.Tests
{   
    public class MessagesTest : BasicTest
    {
        private User _user1;
        private User _user2;
        private readonly UsersGeneratorHelper usersGeneratorHelper;
        public MessagesTest()
        {
            this.usersGeneratorHelper = new UsersGeneratorHelper();
            using (var dbc = new EFRandevouDAL.RandevouBusinessDbContext())
            {
                var usersDao = new UsersDao(dbc);

                if (usersDao.QueryUsers().Where(x => x.Name == "user1" || x.Name == "user2").Count() != 2)
                    usersGeneratorHelper.FillUsersInDb(usersDao);
            }
        }

        [Fact]
        public void TestMessaging()
        {

            using (var dbc = new EFRandevouDAL.RandevouBusinessDbContext())
            {
                var messagesDao = new MessagesDao(dbc);
                var usersDao = new UsersDao(dbc);

                var user1 = usersDao.QueryUsers().Where(x => x.Name == "user1").FirstOrDefault();
                var user2 = usersDao.QueryUsers().Where(x => x.Name == "user2").FirstOrDefault();

                FlushConversation(messagesDao, user1, user2);

                var message = new Message(user1, user2, "hello");
                var response = new Message(user2, user1, "hi");

                var messageId = messagesDao.Insert(message);
                var responseId = messagesDao.Insert(response);

                var service = GetService<IMessagesService>();

                var messageFromDb = service.GetMessage(messageId);
                var responseMessageFromDb = service.GetMessage(responseId);



                Assert.True(messageFromDb.Content == "hello");
                Assert.True(responseMessageFromDb.Content == "hi");

                Assert.True(messageFromDb.SenderId == user1.Id);
                Assert.True(messageFromDb.ReceiverId == user2.Id);

                Assert.True(responseMessageFromDb.SenderId == user2.Id);
                Assert.True(responseMessageFromDb.ReceiverId == user1.Id);

                var conversation = service.GetConversationBetweenUsers(user1.Id, user2.Id);
                Assert.True(conversation.Count() == 2);
            }
        }

        private void FlushConversation(MessagesDao dao, User user1, User user2)
        {
            var messages = dao.QueryMessages()
                 .Where(x =>
                 (x.FromUser.Id == user1.Id && x.ToUser.Id == user2.Id)
                 || (x.FromUser.Id == user2.Id && x.ToUser.Id == user1.Id)
                 ).ToArray();

            foreach (var message in messages)
            {
                dao.Delete(message);
            }
        }
    }
}
