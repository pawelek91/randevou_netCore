using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessServices.FriendshipService;
using EFRandevouDAL;
using EFRandevouDAL.Users;
using RandevouData.Users;
using Xunit;

namespace BusinessServices.Tests
{
    public class FriendshipTest : BasicTest
    {
        private readonly User user1;
        private readonly User user2;
        private readonly User user3;

        public FriendshipTest()
        {
            using (var dbc = new RandevouDbContext())
            {
                var usersDao = new UsersDao(dbc);
                var friendsDao = new FriendshipDao(dbc);
                FlushTestsFriendships(friendsDao);
                UsersTest.FillUsersInDb(usersDao);

                user1 = usersDao.QueryUsers().Where(x => x.Name == "user1").Single();
                user2 = usersDao.QueryUsers().Where(x => x.Name == "user2").Single();
                user3 = usersDao.QueryUsers().Where(x => x.Name == "user3").Single();
            }
        }

        [Fact]
        public void TestRelations()
        {
            using (var dbc = new RandevouDbContext())
            {
                var usersDao = new UsersDao(dbc);
                var service = GetService<IFriendshipService>();

                service.SendFriendshipRequest(user1.Id, user2.Id);
                service.SendFriendshipRequest(user3.Id, user2.Id);

                Action duplicateRequest = () => { service.SendFriendshipRequest(user1.Id, user2.Id); };
                Assert.Throws<ArgumentException>(duplicateRequest);

                var requestsForUser2 = service.GetFriendshipRequests(user2.Id);
                Assert.Contains(user1.Id, requestsForUser2);
                Assert.Contains(user3.Id, requestsForUser2);

                service.UpdateFriendshipStatus(user2.Id, user1.Id, FriendshipsConsts.Accept);
                service.UpdateFriendshipStatus(user2.Id, user3.Id, FriendshipsConsts.Delete);

                requestsForUser2 = service.GetFriendshipRequests(user2.Id);
                Assert.Empty(requestsForUser2);

            }
        }

        private void FlushTestsFriendships(FriendshipDao dao)
        {
            var relations = dao.QueryFriendships().Where(x =>
            x.User1.Name.Contains("user")
            || x.User2.Name.Contains("user")).ToArray();

            foreach(var rel in relations)
            {
                dao.Delete(rel);
            }
        }
    }
}
