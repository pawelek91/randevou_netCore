using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RandevouData.Messages;
using RandevouData.Users;

namespace EFRandevouDAL
{
    public class RandevouDbContext : DbContext
    {
        
        private const string _dbName = "Randevou.db";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath =Path.Combine(Directory.GetCurrentDirectory(),_dbName);
            optionsBuilder.UseSqlite("Data Source=" + dbPath);            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
