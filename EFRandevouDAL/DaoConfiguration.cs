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
            //var u2 = new User("user2", string.Empty, 'F', new DateTime(1998, 12, 12)) { Id = 2 };
            //var u3 = new User("user3", string.Empty, 'M', new DateTime(1980, 12, 12)) { Id = 3 };

            return new User[] { u1 };
        }
        //private User[] _initialData = new User[]
        //    {
        //        new User("user1", string.Empty, 'M', new DateTime(1990, 12, 12)) { Id = 1, UserDetails = new UserDetails(this) },
        //        new User("user2", string.Empty, 'F', new DateTime(1998, 12, 12)){ Id = 2 },
        //        new User("user3", string.Empty, 'M', new DateTime(1980, 12, 12)){ Id = 3 }
        //    };

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(generateUsers());
            builder.OwnsOne(e => e.UserDetails).HasData(
                new UserDetails() { UserId = 1 });
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
                    DetailsType = UserDetailsTypes.EyesColor,
                    DisplayName = "Brązowe",
                    Name = "Brązowe",
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    Id = 2,
                    DetailsType = UserDetailsTypes.EyesColor,
                    DisplayName = "Niebieskie",
                    Name = "Niebieskie",
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    Id = 3,
                    DetailsType = UserDetailsTypes.EyesColor,
                    DisplayName = "zielone",
                    Name = "zielone",
                    ObjectType = "boolean",
                },

                new UserDetailsDictionaryItem()
                {
                    Id = 4,
                    DetailsType = UserDetailsTypes.HairColor,
                    DisplayName = "ciemne",
                    Name = "ciemne",
                    ObjectType = "boolean",
                },

               new UserDetailsDictionaryItem()
               {
                   Id = 5,
                   DetailsType = UserDetailsTypes.HairColor,
                   DisplayName = "jasne",
                   Name = "jasne",
                   ObjectType = "boolean",
               },

                new UserDetailsDictionaryItem()
                {
                    Id = 6,
                    DetailsType = UserDetailsTypes.Interests,
                    DisplayName = "piłka nożna",
                    Name = "football",
                    ObjectType = "boolean",
                },

                 new UserDetailsDictionaryItem()
                 {
                     Id = 7,
                     DetailsType = UserDetailsTypes.Interests,
                     DisplayName = "koszykówka",
                     Name = "koszykówka",
                     ObjectType = "boolean",
                 },

                new UserDetailsDictionaryItem()
                {
                    Id = 8,
                    DetailsType = UserDetailsTypes.Interests,
                    DisplayName = "szachy",
                    Name = "szachy",
                    ObjectType = "boolean",
                }
        };
        public void Configure(EntityTypeBuilder<UserDetailsDictionaryItem> builder)
        {
            builder.HasData(_initialData);
        }
    }
}
