using Microsoft.EntityFrameworkCore;
using System;

namespace EntityFrameworkLab
{
    public class Context: DbContext
    {
        private const String ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=EFLab";

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Person> People { get; set; }

        public Context(): base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Context.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}