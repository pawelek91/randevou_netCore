using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandevouData.Users;

namespace EFRandevouDAL.Users
{
    public class UsersDao : BasicDao<User>
    {
        public UsersDao(RandevouDbContext context) : base(context) { }

        /// <summary>
        /// WRONG! use search patterns...
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> QueryUsers()
        {
                return dbc.Users.AsQueryable();
        }
    }
}
