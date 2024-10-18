using Main.Copy;
using Microsoft.EntityFrameworkCore;

namespace Main.DataBase
{
  public class ApplicationContext : DbContext
  {
    public DbSet<UserRoles> UserRoles { get; set; }
    
    public DbSet<Main.Copy.Task> Tasks { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<AppUser> AppUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TaskTracker;Username=postgres;Password=1234");
    }
  }
}
