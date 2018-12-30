using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EFRandevouDAL.DaoConfigurations;
using Microsoft.EntityFrameworkCore;
using RandevouData.Messages;
using RandevouData.Users;
using RandevouData.Users.Details;
//using RandevouData.Authentications;

namespace EFRandevouDAL
{
    public class RandevouDbContext : DbContext
    {
        
        private const string _dbName = "Randevou.db";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			var filesPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			var dbPath = Path.Combine(filesPath, "..","..","..","..",_dbName);
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

            modelBuilder.Entity<UsersDetailsItemsValues>()
            .HasKey(table => new { table.UserDetailsId, table.UserDetailsDictionaryItemId });

            modelBuilder
                .ApplyConfiguration(new DictionaryDetailsDaoConfiguration());

            modelBuilder.Entity<UsersFriendship>()
                .HasKey(x => new { x.User1Id, x.User2Id });

            modelBuilder.Entity<UsersFriendship>()
                .HasOne(fs => fs.User1)
                .WithMany()
                .HasForeignKey(fs => fs.User1Id);

            modelBuilder.Entity<UsersFriendship>()
                .HasOne(fs => fs.User2)
                .WithMany()
                .HasForeignKey(fs => fs.User2Id);



        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserDetails> UsersDetails { get; set; }
        public DbSet<UserDetailsDictionaryItem> UserDetailsDictionary { get; set; }
        public DbSet<UsersDetailsItemsValues> UsersDetailsItemsValues{get;set;}
        public DbSet<UsersFriendship> Friendships { get; set; }
        //public DbSet<Login> Login { get; set; }
        //public DbSet<Authentication> Authentication { get; set; }
    }
}
