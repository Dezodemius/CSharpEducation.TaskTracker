using DataBaseProject.Tests;
using Microsoft.EntityFrameworkCore;

namespace DataBaseProject.TaskTracker
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres1;Username=postgres;Password=1234");
        }
    }
}
