using System;
using System.Collections.Generic;
using System.Text;

namespace EFRandevouDAL.Authentication
{
    public abstract class BasicAuthDao 
    {
        protected RandevouAuthDbContext _dbContext;
        public BasicAuthDao(RandevouAuthDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
    }
}
