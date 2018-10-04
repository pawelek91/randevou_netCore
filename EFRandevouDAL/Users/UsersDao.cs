﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandevouData.Users;

namespace EFRandevouDAL.Users
{
    public class UsersDao : BasicDao<User>
    {
        public override User Get(int id)
        {
            using (var dbc = new RandevouDbContext())
            { 
                var user = dbc.Find<User>(id);
                return user;
            }
        }


        /// <summary>
        /// WRONG! use search patterns...
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> QueryUsers()
        {
            using (var dbc = new RandevouDbContext())
            {
                var users = dbc.Query<User>().ToArray();
                return users.AsQueryable();
            }
        }
    }
}
