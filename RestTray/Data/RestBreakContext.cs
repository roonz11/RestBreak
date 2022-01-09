using Microsoft.EntityFrameworkCore;
using RestTray.Models;

namespace RestTray.Data
{
    public class RestBreakContext : DbContext
    {
        public DbSet<Session> Session { get; set; }

        public RestBreakContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=/RestBreakDB.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>();

            base.OnModelCreating(modelBuilder);
        }



    }
}
