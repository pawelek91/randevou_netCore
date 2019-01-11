using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFRandevouDAL.Users;
using RandevouData.Users;

namespace BusinessServices.Tests.Helper
{
    internal class UsersGeneratorHelper
    {
        public User[] GenerateUsers()
        {
            var user1 = new User("user1", string.Empty, 'M', new DateTime(1991, 12, 12));
            var user2 = new User("user2", string.Empty, 'F', new DateTime(1998, 12, 12));
            var user3 = new User("user3", string.Empty, 'M', new DateTime(1980, 12, 12));

            var user4 = new User("user4", string.Empty, 'M', new DateTime(1970, 12, 12));
            var user5 = new User("user4", string.Empty, 'F', new DateTime(1973, 12, 12));
            var user6 = new User("user4", string.Empty, 'M', new DateTime(1989, 12, 12));
            return new User[] { user1, user2, user3, user4, user5, user6 };
        }


        public void FillUsersInDb(UsersDao dao)
        {
            var users = GenerateUsers();
            var usersToDelete = dao.QueryUsers().Where(x => x.Name.Contains("NowyUserek")).ToArray();

            foreach (var u in usersToDelete)
                dao.Delete(u);

            var userNamesInDb = dao.QueryUsers().Select(x => x.Name).ToArray();

            var usersToAdd = users.Where(x => !userNamesInDb.Contains(x.Name));
            foreach (var newUser in usersToAdd)
            {
                dao.Insert(newUser);
            }
        }

        internal User[] GenerateUsersDetails(UsersDao dao)
        {
            var user1 = dao.QueryUsers().Where(x => x.Name == "user1").First();
            var user2 = dao.QueryUsers().Where(x => x.Name == "user2").First();
            var user3 = dao.QueryUsers().Where(x => x.Name == "user3").First();
            var user4 = dao.QueryUsers().Where(x => x.Name == "user4").First();

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
    }
}
