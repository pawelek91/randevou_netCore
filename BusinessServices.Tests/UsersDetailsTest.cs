using System.Linq;
using AutoMapper;
using BusinessServices.UsersFinderService;
using BusinessServices.UsersService;
using EFRandevouDAL.Users;
using Microsoft.EntityFrameworkCore;
using RandevouData.Users;
using RandevouData.Users.Details;
using Xunit;
namespace BusinessServices.Tests
{
    public class UsersDetailsTest : BasicTest
    {
        public UsersDetailsTest()
        {
            using (var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                UsersTest.FillUsersInDb(dao);
                var users = GenerateUsersDetails(dao);
                FlushUsersInteres();
                AddDictionaryValues(users);
            }
        }

        [Fact]
        public void TestUsersDetailsUpdate()
        {
            using(var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var usersDao = new UsersDao(dbc);
                var detailsDao = new DetailsDictionaryDao(dbc);
                
                int userId;
                var user5 = usersDao.QueryUsers().Where(x=>x.Name == "user5").FirstOrDefault();
                if(user5 == null)
                    userId = CreateUser("user5", usersDao);
                else
                    userId=user5.Id;

                int footballInterestId = GetInterestId(UserDetailsTypesConsts.InterestFootball, detailsDao);
                int basketballInterestId = GetInterestId(UserDetailsTypesConsts.InterestBasketball, detailsDao);
                int chessInterestId = GetInterestId(UserDetailsTypesConsts.InterestChess, detailsDao);

                var usersService = GetService<IUsersService>();
                var searchService = GetService<IUserFinderService>();

                var userDetailsDto = new UserDetailsDto()
                {
                    Width = 160,
                    Heigth = 90,
                    Tattos = 2,
                    Interests = new int[] {chessInterestId},
                };

                usersService.UpdateUserDetails(userId, userDetailsDto);

                var searchDto = new SearchQueryDto()
                {
                    Name = "user5",
                    InterestIds = new int[] {chessInterestId},
                    WidthFrom = 120,
                    WidthTo = 170,
                };



                var findResult = searchService.FindUsers(searchDto);
                Assert.True(findResult.First() == userId);
                
                searchDto.WidthTo = 159;
                findResult = searchService.FindUsers(searchDto);
                Assert.True(findResult.Length == 0);

                searchDto.WidthTo = null;
                searchDto.InterestIds = new int[0];
                searchDto.Name = string.Empty;
                findResult = searchService.FindUsers(searchDto);
                Assert.True(findResult.Contains(userId));
                
                searchDto = new SearchQueryDto()
                {
                    Name = "user5",
                    InterestIds = new int[] {footballInterestId},
                };
                findResult = searchService.FindUsers(searchDto);
                Assert.True(findResult.Length == 0);

                userDetailsDto = new UserDetailsDto();
                userDetailsDto.Interests = new int[] {footballInterestId, basketballInterestId};
                usersService.UpdateUserDetails(userId, userDetailsDto);
                findResult = searchService.FindUsers(searchDto);
                Assert.True(findResult.Contains(userId));
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
                footballInterestId = GetInterestId(UserDetailsTypesConsts.InterestFootball, dao);
                basketballInterestId =GetInterestId(UserDetailsTypesConsts.InterestBasketball, dao);
                chessInterestId = GetInterestId(UserDetailsTypesConsts.InterestChess, dao);
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

            var service = GetService<IUserFinderService>();


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

                var userFinderService = GetService<IUserFinderService>();
                var regionQuery = userFinderService.FindUsers(
                    new SearchQueryDto()
                    { Region = "Małopolskie", Name = "user" });

                var cityQuery= userFinderService.FindUsers(
                    new SearchQueryDto()
                    { City = "Kraków", Name = "user" });

                var regionCityQuery = userFinderService.FindUsers(
                    new SearchQueryDto()
                    { City = "Wieliczka", Region="Małopolskie", Name = "user" });

                var heightQuery = userFinderService.FindUsers(
                    new SearchQueryDto()
                    { HeightFrom =  65, Name = "user" });

                var heightQueryFromTo = userFinderService.FindUsers(
                    new SearchQueryDto()
                    { HeightFrom = 65, HeightTo = 75, Name = "user" });

                var widthQuery = userFinderService.FindUsers(
                    new SearchQueryDto()
                    { WidthFrom = 180, WidthTo= 195, Name = "user" });

                var tatooAnyQuery = userFinderService.FindUsers(
                    new SearchQueryDto()
                    { Tatoos = true, Name = "user" });

                var nameLikeQuery = userFinderService.FindUsers(
                   new SearchQueryDto()
                   { Name = "user" });


                Assert.True(regionQuery.Count() == 2);
                Assert.True(cityQuery.Count() == 1);
                Assert.True(regionCityQuery.Count() == 1);
                Assert.True(heightQuery.Count() == 4);
                Assert.True(heightQueryFromTo.Count() == 2);
                Assert.True(widthQuery.Count() == 3);
                Assert.True(tatooAnyQuery.Count() == 3);
                Assert.True(nameLikeQuery.Count() == 7);

            }
        }

        private User[] GenerateUsersDetails(UsersDao dao)
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

            return new User[] { user1, user2, user3, user4 };
            
        }

        private void AddDictionaryValues(params User[] users)
        {
            using (var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var dao = new DetailsDictionaryDao(dbc);

                if (dao.QueryDictionaryValues().Count() > 3)
                    return;


                var footballInterest = dao.QueryDictionary().Where(x => x.Name.ToLower() == UserDetailsTypesConsts.InterestFootball.ToLower()).First();
                var basketballInterest = dao.QueryDictionary().Where(x => x.Name.ToLower() == UserDetailsTypesConsts.InterestBasketball.ToLower()).First();
                var chessInterest = dao.QueryDictionary().Where(x => x.Name.ToLower() == UserDetailsTypesConsts.InterestChess.ToLower()).First();

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

        private int GetInterestId(string interest, DetailsDictionaryDao detailsDao)
        =>  detailsDao.QueryDictionary().Where(x => x.Name.ToLower() == interest.ToLower()).First().Id;

        private int CreateUser(string userName, UsersDao dao)
        {
            var user = new User(userName,userName,'f', System.DateTime.Now.AddYears(-50));
            dao.Insert(user);
            return user.Id;
        }

        private void FlushUsersInteres()
        {
            using (var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var dao = new DetailsDictionaryDao(dbc);
                var usersDao = new UsersDao(dbc);

                var udIds = usersDao.QueryUsers()
                    .Where(x => x.Name.ToLower().Contains("user"))
                    .Select(x => x.UserDetails.Id);

                foreach(var udId in udIds)
                {
                    var values = dao.QueryDictionaryValues().Where(x => x.UserDetailsId == udId).ToArray();
                    foreach(var value in values)
                    {
                        dao.DeleteItemValue(value);
                    }

                }
            }
        }
    }
}