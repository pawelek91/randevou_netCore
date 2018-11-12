using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFRandevouDAL;
using EFRandevouDAL.Users;
using RandevouData.Users.Details;

namespace BusinessServices.InitialValues
{
    public static class DictionaryDetailsInitialValues
    {
        public static void AddDictionaryValues(RandevouDbContext dbc)
        {
            var dao = new DetailsDictionaryDao(dbc);
            if (dao.QueryDictionary().Count() > 5)
                return;

            var dictionaryItems = new List<UserDetailsDictionaryItem>()
            {
                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypesConsts.EyesColor,
                    DisplayName = "Brązowe",
                    Name =  UserDetailsTypesConsts.EyesBrown,
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypesConsts.EyesColor,
                    DisplayName = "Niebieskie",
                    Name = UserDetailsTypesConsts.EyesBlue,
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypesConsts.EyesColor,
                    DisplayName = "zielone",
                    Name = UserDetailsTypesConsts.EyesGreen,
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypesConsts.HairColor,
                    DisplayName = "ciemne",
                    Name =  UserDetailsTypesConsts.HairColorDark,
                    ObjectType = "boolean",
                },

               new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypesConsts.HairColor,
                    DisplayName = "jasne",
                    Name = UserDetailsTypesConsts.HairColorLight,
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypesConsts.Interests,
                    DisplayName = "piłka nożna",
                    Name = UserDetailsTypesConsts.InterestFootball,
                    ObjectType = "boolean",
                },

                 new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypesConsts.Interests,
                    DisplayName = UserDetailsTypesConsts.InterestBasketball,
                    Name = "koszykówka",
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypesConsts.Interests,
                    DisplayName = "szachy",
                    Name = UserDetailsTypesConsts.InterestChess,
                    ObjectType = "boolean",
                }
            };

            foreach (var item in dictionaryItems)
                dao.Insert(item);
        }
    }
}
