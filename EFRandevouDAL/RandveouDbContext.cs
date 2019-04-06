using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EFRandevouDAL
{
    public abstract class RandveouDbContext:DbContext
    {
        protected const string _dbName = "Randevou.db";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var filesPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var dbPath = Path.Combine(filesPath, "..", "..", "..", "..", _dbName);
            optionsBuilder.UseSqlite("Data Source=" + dbPath);
        }
    }
}
