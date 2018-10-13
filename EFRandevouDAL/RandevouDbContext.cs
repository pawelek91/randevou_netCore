using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RandevouData.Messages;
using RandevouData.Users;
using RandevouData.Users.Details;

namespace EFRandevouDAL
{
    public class RandevouDbContext : DbContext
    {
        
        private const string _dbName = "Randevou.db";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(),_dbName);
            optionsBuilder.UseSqlite("Data Source=" + dbPath);            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<UserDetails>(x => x.UserDetails)
                .WithOne(ud => ud.User)
                .HasForeignKey<UserDetails>(ud => ud.UserId);

            modelBuilder.Entity<UserDetails>()
                .HasOne<User>(x => x.User)
                .WithOne(x => x.UserDetails)
                .HasForeignKey<UserDetails>(x => x.UserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserDetails> UsersDetails { get; set; }
    }
}
