using Microsoft.EntityFrameworkCore;
using RestTray.Models;
using System.Reflection;

namespace RestTray.Data
{
    public class RestBreakContext : DbContext
    {
        public DbSet<Session> Session { get; set; }

        public RestBreakContext(DbContextOptions<RestBreakContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=RestBreakDB.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>(e =>
            {
                e.Property(s => s.Date).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            base.OnModelCreating(modelBuilder);
        }



    }
}
