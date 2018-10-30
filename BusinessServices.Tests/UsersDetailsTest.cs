using System.Linq;
using AutoMapper;
using BusinessServices.UsersFinderService;
using EFRandevouDAL.Users;
using Xunit;
namespace BusinessServices.Tests
{
    public class UsersDetailsTest
    {
        [Fact]
        public void QueryUserDetails()
        {
            using(var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                UsersTest.FillUsersInDb(dao);
                GenerateUsersDetails(dao);

                var regionQuery = new UserFinderService().FindUsers(
                    new SearchQueryDto()
                    { Region = "Małopolskie" });

               
                

                var cityQuery= new UserFinderService().FindUsers(
                    new SearchQueryDto()
                    { City = "Kraków" });

              

                var regionCityQuery = new UserFinderService().FindUsers(
                    new SearchQueryDto()
                    { City = "Wieliczka", Region="Małopolskie" });

                var heightQuery = new UserFinderService().FindUsers(
                    new SearchQueryDto()
                    { HeightFrom =  65});

                var heightQueryFromTo = new UserFinderService().FindUsers(
                    new SearchQueryDto()
                    { HeightFrom = 65, HeightTo = 75 });

                var widthQuery = new UserFinderService().FindUsers(
                    new SearchQueryDto()
                    { WidthFrom = 180, WidthTo= 195 });

                var tatooAnyQuery = new UserFinderService().FindUsers(
                    new SearchQueryDto()
                    { Tatoos = true });

                var nameLikeQuery = new UserFinderService().FindUsers(
                   new SearchQueryDto()
                   { Name = "user" });


                Assert.True(regionQuery.Count() == 2);
                Assert.True(cityQuery.Count() == 1);
                Assert.True(regionCityQuery.Count() == 1);
                Assert.True(heightQuery.Count() == 3);
                Assert.True(heightQueryFromTo.Count() == 2);
                Assert.True(widthQuery.Count() == 3);
                Assert.True(tatooAnyQuery.Count() == 2);
                Assert.True(nameLikeQuery.Count() == 6);

            }
        }

        private void GenerateUsersDetails(UsersDao dao)
        {
             var user1 = dao.QueryUsers().Where(x=>x.Name == "user1").First();
                var user2 = dao.QueryUsers().Where(x=>x.Name == "user2").First();
                var user3 = dao.QueryUsers().Where(x=>x.Name == "user3").First();
                var user4 = dao.QueryUsers().Where(x=>x.Name == "user4").First();

 

            user1.UserDetails.City = "Warszawa";
                user1.UserDetails.Region = "Mazowieckie";
                user1.UserDetails.Tattos = 2;
                user1.UserDetails.Heigth = 60;
                user1.UserDetails.Width = 180;

                user2.UserDetails.City = "Warszawa";
                user2.UserDetails.Region = "Mazowieckie";
                user2.UserDetails.Tattos = 0;
                user2.UserDetails.Heigth = 70;
                user2.UserDetails.Width = 170;

                user3.UserDetails.City = "Wieliczka";
                user3.UserDetails.Region = "Małopolskie";
                user3.UserDetails.Tattos = 0;
                user3.UserDetails.Heigth = 90;
                user3.UserDetails.Width = 190;

                user4.UserDetails.City = "Kraków";
                user4.UserDetails.Region = "Małopolskie";
                user4.UserDetails.Tattos = 1;
                user4.UserDetails.Heigth = 65;
                user4.UserDetails.Width = 180;

                dao.Update(user1);
                dao.Update(user2);
                dao.Update(user3);
                dao.Update(user4);

        }
    }
}