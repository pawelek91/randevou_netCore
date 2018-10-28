using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandevouData.Users;
using RandevouData.Users.Details;
using Microsoft.EntityFrameworkCore;

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

        public void InsertUserDetails(UserDetails entity)
        {
            dbc.UsersDetails.Add(entity);
        }

        public IQueryable<UserDetails> QueryUserDetails()
        {
            return dbc.UsersDetails.AsQueryable();
        }

        public User GetUserWithDetails(int id)
        {
           return dbc.Users.Include(x => x.UserDetails.DetailsItemsValues).FirstOrDefault(x => x.Id == id);
        }

    }
}
