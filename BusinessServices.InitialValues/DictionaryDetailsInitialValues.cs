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
                    Name = "Brązowe",
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.EyesColor,
                    DisplayName = "Niebieskie",
                    Name = "Niebieskie",
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.EyesColor,
                    DisplayName = "zielone",
                    Name = "zielone",
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.HairColor,
                    DisplayName = "ciemne",
                    Name = "ciemne",
                    ObjectType = "boolean",
                },

               new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.HairColor,
                    DisplayName = "jasne",
                    Name = "jasne",
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.Interests,
                    DisplayName = "piłka nożna",
                    Name = "football",
                    ObjectType = "boolean",
                },

                 new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.Interests,
                    DisplayName = "koszykówka",
                    Name = "koszykówka",
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    DetailsType = UserDetailsTypes.Interests,
                    DisplayName = "szachy",
                    Name = "szachy",
                    ObjectType = "boolean",
                }
            };

            foreach (var item in dictionaryItems)
                dao.Insert(item);
        }
    }
}
