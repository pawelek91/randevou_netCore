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

        public User GetUserWithDetails(int id)
        {
           return dbc.Users.Include(x => x.UserDetails).FirstOrDefault(x => x.Id == id);
        }

        //public IQueryable<UserDetailsDictionaryItem> QueryUsersDetails()
        //{
        //    return dbc.UsersDetails.AsQueryable();
        //}

        //public Guid InsertDictionaryItem(UserDetailsDictionaryItem item)
        //{
        //     dbc.UsersDetails.Add(item);
        //    return item.Id;
        //}
    }
}
