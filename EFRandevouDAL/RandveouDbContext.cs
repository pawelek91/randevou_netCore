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
            string dbPath = string.Empty;

            
            #if DEBUG
                dbPath = Path.Combine(filesPath, "..", "..", "..", "..", _dbName);
            #else
             dbPath = Path.Combine(filesPath, _dbName);
            #endif
            optionsBuilder.UseSqlite("Data Source=" + dbPath);
        }
    }
}
