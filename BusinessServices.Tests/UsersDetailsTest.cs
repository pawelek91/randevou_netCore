using System.Linq;
using AutoMapper;
using BusinessServices.UsersFinderService;
using EFRandevouDAL.Users;
using Microsoft.EntityFrameworkCore;
using RandevouData.Users;
using RandevouData.Users.Details;
using Xunit;
namespace BusinessServices.Tests
{
    public class UsersDetailsTest
    {
        public UsersDetailsTest()
        {
            using (var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                UsersTest.FillUsersInDb(dao);
                GenerateUsersDetails(dao);
            }
        }

        [Fact]
        public void TestUsersInterestQuery()
        {
            int footballInterestId;
            int basketballInterestId;
            int chessInterestId;

            using (var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var dao = new DetailsDictionaryDao(dbc);
                footballInterestId = dao.QueryDictionary().Where(x => x.Name.ToLower() == UserDetailsTypes.InterestFootball.ToLower()).First().Id;
                basketballInterestId = dao.QueryDictionary().Where(x => x.Name.ToLower() == UserDetailsTypes.InterestBasketball.ToLower()).First().Id;
                chessInterestId = dao.QueryDictionary().Where(x => x.Name.ToLower() == UserDetailsTypes.InterestChess.ToLower()).First().Id;
            }

            var footballInterestQuery = new SearchQueryDto()
            {
                InterestIds=new int[] { footballInterestId }
            }; //3

            var basketballInterestQuery = new SearchQueryDto()
            {
                InterestIds = new int[] { basketballInterestId }
            }; //1

            var chessInterestQuery = new SearchQueryDto()
            {
                InterestIds = new int[] { chessInterestId }
            }; //2

            var footballAndChessInterestQuery = new SearchQueryDto()
            {
                InterestIds = new int[] { footballInterestId, chessInterestId }
            }; //1

            var footballAndbasketballInterestQuery = new SearchQueryDto()
            {
                InterestIds = new int[] { footballInterestId, basketballInterestId}
            }; //1

            var service = new UsersFinderService.UserFinderService();


            var c1 = service.FindUsers(footballAndChessInterestQuery);
            var c2 = service.FindUsers(footballAndbasketballInterestQuery);


            Assert.True(service.FindUsers(footballInterestQuery).Count() == 3);
            Assert.True(service.FindUsers(basketballInterestQuery).Count() == 1);
            Assert.True(service.FindUsers(chessInterestQuery).Count() == 2);
            Assert.True(service.FindUsers(footballAndChessInterestQuery).Count() == 1);
            Assert.True(service.FindUsers(footballAndbasketballInterestQuery).Count() == 1);

        }

        [Fact]
        public void TestQueryUserDetails()
        {
            using(var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var dao = new UsersDao(dbc);

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

            AddDictionaryValues(user1, user2, user3, user4);
        }

        private void AddDictionaryValues(params User[] users)
        {
            using (var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var dao = new DetailsDictionaryDao(dbc);

                if (dao.QueryDictionaryValues().Count() > 3)
                    return;


                var footballInterest = dao.QueryDictionary().Where(x => x.Name.ToLower() == UserDetailsTypes.InterestFootball.ToLower()).First();
                var basketballInterest = dao.QueryDictionary().Where(x => x.Name.ToLower() == UserDetailsTypes.InterestBasketball.ToLower()).First();
                var chessInterest = dao.QueryDictionary().Where(x => x.Name.ToLower() == UserDetailsTypes.InterestChess.ToLower()).First();

                var detailsValues = new UsersDetailsItemsValues[]
                {
                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = footballInterest.Id,
                        UserDetailsId = users[0].UserDetails.Id,
                        Value = true,
                   },

                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = basketballInterest.Id,
                        UserDetailsId = users[0].UserDetails.Id,
                        Value = true,
                   },

                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = footballInterest.Id,
                        UserDetailsId = users[1].UserDetails.Id,
                        Value = true,
                   },

                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = chessInterest.Id,
                        UserDetailsId = users[1].UserDetails.Id,
                        Value = true,
                   },

                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = footballInterest.Id,
                        UserDetailsId = users[2].UserDetails.Id,
                        Value = true,
                   },

                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = chessInterest.Id,
                        UserDetailsId = users[3].UserDetails.Id,
                        Value = true,
                   },
                };

                foreach(var itemValue in detailsValues)
                {
                    dao.AddItemValue(itemValue);
                }
            }
        }
    }
}