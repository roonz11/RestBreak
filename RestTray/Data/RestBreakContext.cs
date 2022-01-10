using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestTray.Models;

namespace RestTray.Data
{
    public class RestBreakContext : DbContext
    {
        public IConfiguration Configuration { get; private set; }
        public DbSet<Session> Session { get; set; }

        public RestBreakContext(DbContextOptions<RestBreakContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>();

            base.OnModelCreating(modelBuilder);
        }



    }
}
