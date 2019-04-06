using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RandevouData.Users;
using RandevouData.Users.Details;

namespace EFRandevouDAL.DaoConfigurations
{
    public class UsersDaoConfiguration : IEntityTypeConfiguration<User>
    {
        private User[] generateUsers()
        {
            var u1 = new User("user1", string.Empty, 'M', new DateTime(1990, 12, 12)) { Id = 1 };

            return new User[] { u1 };
        }
        public void Configure(EntityTypeBuilder<User> builder)
        {
        }
    }

    public class UserDetailsDaoConfiguration : IEntityTypeConfiguration<UserDetails>
    {
        public void Configure(EntityTypeBuilder<UserDetails> builder)
        {
   
            builder.OwnsOne(e => e.User);
        }
    }

    public class DictionaryDetailsDaoConfiguration : IEntityTypeConfiguration<UserDetailsDictionaryItem>
    {
        private UserDetailsDictionaryItem[] _initialData = new UserDetailsDictionaryItem[]
        {
             new UserDetailsDictionaryItem()
                {
                    Id = 1,
                    DetailsType = UserDetailsTypesConsts.EyesColor,
                    DisplayName = "Brązowe",
                    Name = UserDetailsTypesConsts.EyesBrown,
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    Id = 2,
                    DetailsType = UserDetailsTypesConsts.EyesColor,
                    DisplayName = "Niebieskie",
                    Name = UserDetailsTypesConsts.EyesBlue,
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    Id = 3,
                    DetailsType = UserDetailsTypesConsts.EyesColor,
                    DisplayName = "zielone",
                    Name = UserDetailsTypesConsts.EyesGreen,
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    Id = 4,
                    DetailsType = UserDetailsTypesConsts.HairColor,
                    DisplayName = "ciemne",
                    Name = UserDetailsTypesConsts.HairColorDark,
                    ObjectType = "boolean",
                },

               new UserDetailsDictionaryItem()
               {
                   Id = 5,
                   DetailsType = UserDetailsTypesConsts.HairColor,
                   DisplayName = "jasne",
                   Name = UserDetailsTypesConsts.HairColorLight,
                   ObjectType = "boolean",
               },

                new UserDetailsDictionaryItem()
                {
                    Id = 6,
                    DetailsType = UserDetailsTypesConsts.Interests,
                    DisplayName = "piłka nożna",
                    Name = UserDetailsTypesConsts.InterestFootball,
                    ObjectType = "boolean",
                },

                 new UserDetailsDictionaryItem()
                 {
                     Id = 7,
                     DetailsType = UserDetailsTypesConsts.Interests,
                     DisplayName = "koszykówka",
                     Name = UserDetailsTypesConsts.InterestBasketball,
                     ObjectType = "boolean",
                 },

                new UserDetailsDictionaryItem()
                {
                    Id = 8,
                    DetailsType = UserDetailsTypesConsts.Interests,
                    DisplayName = "szachy",
                    Name =UserDetailsTypesConsts.InterestChess,
                    ObjectType = "boolean",
                }
        };
        public void Configure(EntityTypeBuilder<UserDetailsDictionaryItem> builder)
        {
            builder.HasData(_initialData);
        }
    }
}
