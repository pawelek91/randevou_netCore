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
        public UsersDao(RandevouBusinessDbContext context) : base(context) { }

        /// <summary>
        /// WRONG! use search patterns...
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> QueryUsers()
        {
            return dbc.Users.Include(x=>x.UserDetails).AsQueryable();
        }

        public void InsertUserDetails(UserDetails entity)
        {
            dbc.UsersDetails.Add(entity);
        }

        public override int Insert(User entity)
        {
            var id =  base.Insert(entity);
            return id;
        }

        public IQueryable<UserDetails> QueryUserDetails()
        {
            return dbc.UsersDetails.AsQueryable();
        }

        public User GetUserWithDetails(int id)
        {
           return dbc.Users.Include(x => x.UserDetails.DetailsItemsValues).FirstOrDefault(x => x.Id == id);
        }

        public override void Delete(User entity)
        {
            entity.IsDeleted = true;
            dbc.SaveChanges();
        }

    }
}
