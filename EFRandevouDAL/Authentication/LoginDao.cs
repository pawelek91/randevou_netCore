using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandevouData.Authentication;

namespace EFRandevouDAL.Authentication
{
    public class LoginDao : BasicAuthDao
    {
        public LoginDao(RandevouAuthDbContext dbContext) : base(dbContext)
        {
            
        }

        public void Log(Login log)
        {
            _dbContext.Logins.Add(log);
            _dbContext.SaveChanges();
        }

    }
}
