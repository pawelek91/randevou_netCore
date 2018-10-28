using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFRandevouDAL.Users;
using RandevouData.Users;

namespace BusinessServices.InitialValues
{
    public static class UsersInitialValues
    {
        public static void AddUsers(EFRandevouDAL.RandevouDbContext dbc)
        {
                var dao = new UsersDao(dbc);
                if (dao.QueryUsers().Count() > 2)
                    return;

                var user = new User("user1", string.Empty, 'M', new DateTime(1990, 12, 12));
                var user2 = new User("user2", string.Empty, 'F', new DateTime(1998, 12, 12));
                var user3 = new User("user3", string.Empty, 'M', new DateTime(1980, 12, 12));
                dao.Insert(user);
                dao.Insert(user2);
                dao.Insert(user3);
            }
        }
    }