using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RandevouData.Authentication;

namespace EFRandevouDAL
{
    public class RandevouAuthDbContext : RandveouDbContext
    {
        public DbSet<Login> Logins { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
    }
}
