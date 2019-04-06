using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessServices.FriendshipService;
using BusinessServices.Tests.Helper;
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
        private readonly UsersGeneratorHelper usersGeneratorHelper;
        public FriendshipTest()
        {
            usersGeneratorHelper = new UsersGeneratorHelper();
            using (var dbc = new RandevouBusinessDbContext())
            {
                
                var usersDao = new UsersDao(dbc);
                usersGeneratorHelper.FillUsersInDb(usersDao);

                user1 = usersDao.QueryUsers().Where(x => x.Name == "user1").Single();
                user2 = usersDao.QueryUsers().Where(x => x.Name == "user2").Single();
                user3 = usersDao.QueryUsers().Where(x => x.Name == "user3").Single();
            }
        }

        [Fact]
        public void TestRelations()
        {
            using (var dbc = new RandevouBusinessDbContext())
            {
                var usersDao = new UsersDao(dbc);
                var service = GetService<IFriendshipService>();

                FlushTestsFriendships();

                service.SendFriendshipRequest(user1.Id, user2.Id);
                service.SendFriendshipRequest(user3.Id, user2.Id);

                Assert.Throws<ArgumentException>(() => service.SendFriendshipRequest(user1.Id, user2.Id));

                var requestsForUser2 = service.GetFriendshipRequests(user2.Id);
                Assert.Contains(user1.Id, requestsForUser2);
                Assert.Contains(user3.Id, requestsForUser2);

                service.UpdateFriendshipStatus(user2.Id, user1.Id, FriendshipsConsts.Accept);
                service.UpdateFriendshipStatus(user2.Id, user3.Id, FriendshipsConsts.Delete);

                requestsForUser2 = service.GetFriendshipRequests(user2.Id);
                Assert.Empty(requestsForUser2);

                FlushTestsFriendships();

                service.SendFriendshipRequest(user1.Id, user2.Id);
                service.SendFriendshipRequest(user1.Id, user3.Id);

                service.UpdateFriendshipStatus(user2.Id, user1.Id, FriendshipsConsts.Accept);
                service.UpdateFriendshipStatus(user3.Id, user1.Id, FriendshipsConsts.Accept);

                var friends = service.GetFriends(user1.Id);
                Assert.True(friends.Count() == 2);

                service.UpdateFriendshipStatus(user3.Id, user1.Id, FriendshipsConsts.Delete);
                friends = service.GetFriends(user1.Id);
                Assert.True(friends.Count() == 1);

                service.SendFriendshipRequest(user3.Id, user1.Id);
                service.UpdateFriendshipStatus(user1.Id, user3.Id, FriendshipsConsts.Accept);
                friends = service.GetFriends(user1.Id);
                Assert.True(friends.Count() == 2);

                Assert.Throws<ArgumentException>(() => service.SendFriendshipRequest(user2.Id, user1.Id));
            }
        }



        private void FlushTestsFriendships()
        {
            using (RandevouBusinessDbContext dbc = new RandevouBusinessDbContext())
            {
                FriendshipDao dao = new FriendshipDao(dbc);
                var relations = dao.QueryFriendships().Where(x =>
                x.User1.Name.Contains("user")
                || x.User2.Name.Contains("user")).ToArray();

                foreach (var rel in relations)
                {
                    dao.Delete(rel);

                }
            }
        }
    }
}
