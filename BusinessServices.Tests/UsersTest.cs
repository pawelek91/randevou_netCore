using System;
using System.Collections.Generic;
using System.Text;
using BusinessServices.UsersService;
using RandevouData.Users;
using Xunit;
using AutoMapper;
using EFRandevouDAL.Users;
using System.Linq;

namespace BusinessServices.Tests
{
    public class UsersTest
    {
        IMapper mapper;
        public UsersTest()
        {
        }

        [Fact]
        public void QueryBasicUsersDataTest()
        {
            var users = GenerateUsers();

            IUsersService service = new UsersService.UserService(mapper);
            using (var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                FillUsersInDb(dao);

                var malesCount = dao.QueryUsers().Where(x => x.Gender == 'm' || x.Gender == 'M').Count();
                var femalesCount = dao.QueryUsers().Where(x => x.Gender == 'm' || x.Gender == 'M').Count();

                var bornAfter1990 = dao.QueryUsers().Where(x => x.BirthDate >= new DateTime(1990,1,1)).Count();
                var bornBefore1990 = dao.QueryUsers().Where(x => x.BirthDate <= new DateTime(1990,1,1)).Count();
                var bornAfter1980 = dao.QueryUsers().Where(x => x.BirthDate >= new DateTime(1980,1,1)).Count();
                var bornBefore1980 = dao.QueryUsers().Where(x => x.BirthDate <= new DateTime(1980,1,1)).Count();

                Assert.True(malesCount >= 4);
                Assert.True(femalesCount >= 2);
                Assert.True(bornAfter1980 >=4);
                Assert.True(bornAfter1990 >= 2);
                Assert.True(bornBefore1980 == 2);
                Assert.True(bornBefore1990 == 4);
            }
        }

        [Fact]
        public void UsersAddedToDb()
        {
            IUsersService service = new UsersService.UserService(mapper);
            using (var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                FillUsersInDb(dao);
                var usersCount = dao.QueryUsers().Count();
                Assert.True(usersCount >= 6);
            }
        }

        private void FillUsersInDb(UsersDao dao)
        {
            var users = GenerateUsers();
            var userNamesInDb = dao.QueryUsers().Select(x => x.Name).ToArray();

            var usersToAdd = users.Where(x => !userNamesInDb.Contains(x.Name));
            foreach (var newUser in usersToAdd)
            {
                dao.Insert(newUser);
            }
        }
        

        private User[] GenerateUsers()
        {
            var user1 = new User("user1", string.Empty, 'M', new DateTime(1991, 12, 12));
            var user2 = new User("user2", string.Empty, 'F', new DateTime(1998, 12, 12));
            var user3 = new User("user3", string.Empty, 'M', new DateTime(1980, 12, 12));

            var user4 = new User("user4", string.Empty, 'M', new DateTime(1970, 12, 12));
            var user5 = new User("user4", string.Empty, 'F', new DateTime(1973, 12, 12));
            var user6 = new User("user4", string.Empty, 'M', new DateTime(1989, 12, 12));
            return new User[] { user1, user2, user3, user4, user5, user6 };
        }
       
    }
}
