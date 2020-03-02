using Microsoft.EntityFrameworkCore;
using System;

using Chat.Web.Models;

namespace Chat.Web.Data
{
    public class Context: DbContext
    {
        private const String ConnectionString = "Data Source=Messages.db;Cache=Shared";

        public DbSet<Message> Messages { get; set; }

        public Context(): base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Context.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}