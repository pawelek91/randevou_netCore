using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessServices.UsersService.DetailsDictionary;
using EFRandevouDAL.Users;
using RandevouData.Users;
using RandevouData.Users.Details;

namespace BusinessServices.Tests.Helper
{
    internal class DictionaryHelper
    {
        public int BrowEyesColorId { get; private set; } 
        public int BlueEyesColorId { get; private set; }
        public int GreenEyesColorId { get; private set; }
        public int DarkHairColorId { get; private set; }
        public int LightHairColorId { get; private set; }

        private readonly IUserDetailsDictionaryService userDetailsService;
        public DictionaryHelper(IUserDetailsDictionaryService service)
        {
            userDetailsService = service;

            BrowEyesColorId = GetEyesColorId(UserDetailsTypesConsts.EyesBrown);
            BlueEyesColorId = GetEyesColorId(UserDetailsTypesConsts.EyesBlue);
            GreenEyesColorId = GetEyesColorId(UserDetailsTypesConsts.EyesGreen);
            DarkHairColorId = GetHairColorId(UserDetailsTypesConsts.HairColorDark);
            LightHairColorId = GetHairColorId(UserDetailsTypesConsts.HairColorLight);
        }
           
        public int GetEyesColorId(string color)
        {
            var id = userDetailsService.GetEyesColorItemId(color);
            return id;
        }

        public int GetHairColorId(string color)
        {
            var id = userDetailsService.GetHairColorItemId(color);
            return id;
        }

        public void AddUsersDictionaryValues(User[] users)
        {
            using (var dbc = new EFRandevouDAL.RandevouBusinessDbContext())
            {
                var dao = new DetailsDictionaryDao(dbc);

                if (dao.QueryDictionaryValues().Count() > 3)
                    return;


                var footballInterest = dao.QueryDictionary().Where(x => x.Name.ToLower() == UserDetailsTypesConsts.InterestFootball.ToLower()).First();
                var basketballInterest = dao.QueryDictionary().Where(x => x.Name.ToLower() == UserDetailsTypesConsts.InterestBasketball.ToLower()).First();
                var chessInterest = dao.QueryDictionary().Where(x => x.Name.ToLower() == UserDetailsTypesConsts.InterestChess.ToLower()).First();

                var detailsValues = new UsersDetailsItemsValues[]
                {

                    #region user0
                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = footballInterest.Id,
                        UserDetailsId = users[0].UserDetails.Id,
                        Value = true,
                   },

                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = BrowEyesColorId,
                        UserDetailsId = users[0].UserDetails.Id,
                        Value = true,
                   },

                      new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = DarkHairColorId,
                        UserDetailsId = users[0].UserDetails.Id,
                        Value = true,
                   },

                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = basketballInterest.Id,
                        UserDetailsId = users[0].UserDetails.Id,
                        Value = true,
                   },
                #endregion

                #region user1
                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = BrowEyesColorId,
                        UserDetailsId = users[1].UserDetails.Id,
                        Value = true,
                   },

                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId =LightHairColorId,
                        UserDetailsId = users[1].UserDetails.Id,
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
                    #endregion
                    #region user2

                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = BlueEyesColorId,
                        UserDetailsId = users[2].UserDetails.Id,
                        Value = true,
                   },

                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = LightHairColorId,
                        UserDetailsId = users[2].UserDetails.Id,
                        Value = true,
                   },

                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = footballInterest.Id,
                        UserDetailsId = users[2].UserDetails.Id,
                        Value = true,
                   },
                   #endregion
                   new UsersDetailsItemsValues()
                   {
                        UserDetailsDictionaryItemId = chessInterest.Id,
                        UserDetailsId = users[3].UserDetails.Id,
                        Value = true,
                   },
                };


                foreach (var itemValue in detailsValues)
                {
                    dao.AddItemValue(itemValue);
                }
            }
        }
    }
}
