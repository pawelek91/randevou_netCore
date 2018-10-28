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
                    DetailsType = UserDetailsTypes.EyesColor,
                    DisplayName = "Brązowe",
                    Name =  UserDetailsTypes.EyesBrown,
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.EyesColor,
                    DisplayName = "Niebieskie",
                    Name = UserDetailsTypes.EyesBlue,
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.EyesColor,
                    DisplayName = "zielone",
                    Name = UserDetailsTypes.EyesGreen,
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.HairColor,
                    DisplayName = "ciemne",
                    Name =  UserDetailsTypes.HairColorDark,
                    ObjectType = "boolean",
                },

               new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.HairColor,
                    DisplayName = "jasne",
                    Name = UserDetailsTypes.HairColorLight,
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.Interests,
                    DisplayName = "piłka nożna",
                    Name = UserDetailsTypes.InterestFootball,
                    ObjectType = "boolean",
                },

                 new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.Interests,
                    DisplayName = UserDetailsTypes.InterestBasketball,
                    Name = "koszykówka",
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.Interests,
                    DisplayName = "szachy",
                    Name = UserDetailsTypes.InterestChess,
                    ObjectType = "boolean",
                }
            };

            foreach (var item in dictionaryItems)
                dao.Insert(item);
        }
    }
}
