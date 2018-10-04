using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
namespace EntityFrameworkDao
{
	public class MyAppDbContext:DbContext
    {
		public DbSet<FirstNetCoreAPp.Person> Persons { get; set; }
    }
}
