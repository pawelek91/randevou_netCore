using System;
using System.Collections.Generic;
using System.Text;
using BusinessServices.UsersService;
using RandevouData.Users;
using Xunit;
using AutoMapper;
using EFRandevouDAL.Users;
using System.Linq;
using BusinessServices.Tests.Helper;

namespace BusinessServices.Tests
{
    public class UsersTest : BasicTest
    {
        private readonly UsersGeneratorHelper usersGeneratorHelper;
        public UsersTest()
        {
            usersGeneratorHelper = new UsersGeneratorHelper();
        }
        [Fact]
        public void QueryBasicUsersDataTest()
        {
            IUsersService service = GetService<IUsersService>();
            using (var dbc = new EFRandevouDAL.RandevouBusinessDbContext())
            {
                var dao = new UsersDao(dbc);
                usersGeneratorHelper.FillUsersInDb(dao);

                var malesCount = dao.QueryUsers().Where(x => x.Gender == 'm' || x.Gender == 'M').Count();
                var femalesCount = dao.QueryUsers().Where(x => x.Gender == 'f' || x.Gender == 'F').Count();

                var bornAfter1990 = dao.QueryUsers().Where(x => x.BirthDate >= new DateTime(1990,1,1) && !x.IsDeleted).Count();
                var bornBefore1990 = dao.QueryUsers().Where(x => x.BirthDate <= new DateTime(1990,1,1) && !x.IsDeleted).Count();
                var bornAfter1980 = dao.QueryUsers().Where(x => x.BirthDate >= new DateTime(1980,1,1) && !x.IsDeleted).Count();
                var bornBefore1980 = dao.QueryUsers().Where(x => x.BirthDate <= new DateTime(1980,1,1) && !x.IsDeleted).Count();

                Assert.True(malesCount >= 4);
                Assert.True(femalesCount >= 2);
                Assert.True(bornAfter1980 >=4);
                Assert.True(bornAfter1990 >= 2);
                Assert.True(bornBefore1980 == 3);
                Assert.True(bornBefore1990 >= 5);
            }
        }

        [Fact]
        public void UsersAddedToDb()
        {
            IUsersService service = GetService<IUsersService>();
            using (var dbc = new EFRandevouDAL.RandevouBusinessDbContext())
            {
                var dao = new UsersDao(dbc);
                usersGeneratorHelper.FillUsersInDb(dao);
                var usersCount = dao.QueryUsers().Count();
                Assert.True(usersCount >= 6);
            }
        }

       
       
    }
}
