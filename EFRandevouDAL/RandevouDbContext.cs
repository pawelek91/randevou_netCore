using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RandevouData.Messages;
using RandevouData.Users;

namespace EFRandevouDAL
{
    public class RandevouDbContext : DbContext
    {
        private static readonly string _connectionString = "Data Source=Randevou.db";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
