using System.Linq;
using EFRandevouDAL.Users;
using Xunit;
namespace BusinessServices.Tests
{
    public class UsersDetailsTest
    {
        public void GenerateUsersWithDetals()
        {
            using(var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                UsersTest.FillUsersInDb(dao);
                GenerateUsersDetails(dao);

               

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
                user4.UserDetails.Heigth = 654;
                user4.UserDetails.Width = 180;

        }
    }
}