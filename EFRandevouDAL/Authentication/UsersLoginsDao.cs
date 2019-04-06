using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandevouData.Authentication;

namespace EFRandevouDAL.Authentication
{
    public class UsersLoginsDao : BasicAuthDao
    {
        public UsersLoginsDao(RandevouAuthDbContext dbContext) : base(dbContext)
        {
        }

        public void AddUserLogin(UserLogin ul)
        {
            _dbContext.Add(ul);
            _dbContext.SaveChanges();
        }

        public void UpdateLogin(UserLogin ul)
        {
            _dbContext.Update(ul);
            _dbContext.SaveChanges();
        }

        public void DeleteLogin(UserLogin ul)
        {
            ul.IsActive = false;
            _dbContext.SaveChanges();
        }

        public UserLogin Get(int user, string password)
        {
            var entity = _dbContext.UserLogins.SingleOrDefault(x => x.UserId == user && x.Password.Equals(password));
            return entity;
        }
    }
}
