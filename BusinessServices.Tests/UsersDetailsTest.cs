using System.Linq;
using AutoMapper;
using BusinessServices.Tests.Helper;
using BusinessServices.UsersFinderService;
using BusinessServices.UsersService;
using BusinessServices.UsersService.DetailsDictionary;
using EFRandevouDAL.Users;
using Microsoft.EntityFrameworkCore;
using RandevouData.Users;
using RandevouData.Users.Details;
using Xunit;
namespace BusinessServices.Tests
{
    public class UsersDetailsTest : BasicTest
    {
        private readonly UsersGeneratorHelper usersGeneratorHelper;
        private readonly DictionaryHelper dictionaryHelper;
        public UsersDetailsTest()
        {
            usersGeneratorHelper = new UsersGeneratorHelper();
            dictionaryHelper = new DictionaryHelper(GetService<IUserDetailsDictionaryService>());
            using (var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                usersGeneratorHelper.FillUsersInDb(dao);
                var users = usersGeneratorHelper.GenerateUsersDetails(dao);
                FlushUsersDetails();
                dictionaryHelper.AddUsersDictionaryValues(users);
            }
        }

        [Fact]
        public void TestUsersDetailsUpdate()
        {
            using (var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var usersDao = new UsersDao(dbc);
                var detailsDao = new DetailsDictionaryDao(dbc);

                int userId;
                var user5 = usersDao.QueryUsers().Where(x => x.Name == "user5").FirstOrDefault();
                if (user5 == null)
                    userId = CreateUser("user5", usersDao);
                else
                    userId = user5.Id;

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
                    Interests = new int[] { chessInterestId },
                    HairColor = dictionaryHelper.DarkHairColorId,
                    EyesColor = dictionaryHelper.BrowEyesColorId,
                };

                usersService.UpdateUserDetails(userId, userDetailsDto);

                var searchDto = new SearchQueryDto()
                {
                    Name = "user5",
                    InterestIds = new int[] { chessInterestId },
                    WidthFrom = 120,
                    WidthTo = 170,
                };

                var search2Dto = new SearchQueryDto()
                {
                    HairColor = dictionaryHelper.DarkHairColorId,
                    EyesColor = dictionaryHelper.BrowEyesColorId,
                };

                var findResult = searchService.FindUsers(searchDto);
                Assert.True(findResult.First() == userId);

                var findResult2 = searchService.FindUsers(search2Dto);
                Assert.True(findResult2.Contains(userId));


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
                    InterestIds = new int[] { footballInterestId },
                };
                findResult = searchService.FindUsers(searchDto);
                Assert.True(findResult.Length == 0);

                userDetailsDto = new UserDetailsDto();
                userDetailsDto.HairColor = dictionaryHelper.LightHairColorId;

                userDetailsDto.Interests = new int[] { footballInterestId, basketballInterestId };
                usersService.UpdateUserDetails(userId, userDetailsDto);
                findResult = searchService.FindUsers(searchDto);
                Assert.True(findResult.Contains(userId));

                findResult2 = searchService.FindUsers(search2Dto);
                Assert.False(findResult.Contains(userId));
            }
        }

        [Fact]
        public void TestUsersHairAndEyesColorsQuery()
        {
            var lightHairColorUsersQuery = new SearchQueryDto()
            {
                HairColor = dictionaryHelper.LightHairColorId,
            };

            var darkHairColorUsersQuery = new SearchQueryDto()
            {
                HairColor = dictionaryHelper.DarkHairColorId,
            };

            var brownEyedUsersQuery = new SearchQueryDto()
            {
                EyesColor = dictionaryHelper.BrowEyesColorId,
            };

            var blueEyedUsersQuery = new SearchQueryDto()
            {
                EyesColor = dictionaryHelper.BlueEyesColorId,
            };

            var greenEyedUsersQuery = new SearchQueryDto()
            {
                EyesColor = dictionaryHelper.GreenEyesColorId,
            };

            var service = GetService<IUserFinderService>();

            Assert.True(service.FindUsers(lightHairColorUsersQuery).Count() == 2);
            Assert.True(service.FindUsers(darkHairColorUsersQuery).Count() == 1);
            Assert.True(service.FindUsers(brownEyedUsersQuery).Count() == 2);
            Assert.True(service.FindUsers(blueEyedUsersQuery).Count() == 1);
            Assert.True(service.FindUsers(greenEyedUsersQuery).Count() == 0);

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

        private int GetInterestId(string interest, DetailsDictionaryDao detailsDao)
        =>  detailsDao.QueryDictionary().Where(x => x.Name.ToLower() == interest.ToLower()).First().Id;

        private int CreateUser(string userName, UsersDao dao)
        {
            var user = new User(userName,userName,'f', System.DateTime.Now.AddYears(-50));
            dao.Insert(user);
            return user.Id;
        }


        private void FlushUsersDetails()
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