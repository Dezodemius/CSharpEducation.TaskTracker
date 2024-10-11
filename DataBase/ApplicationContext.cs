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

    /// <summary>
    /// Здесь должен быть запрос всех доступных задач
    /// </summary>
    /// <returns></returns>
    public static Main.Copy.Task[] GetAllTask()
    {
      Main.Copy.Task task0 = new Main.Copy.Task();
      task0.Description = "Подтягивания делать";
      Main.Copy.Task task1 = new Main.Copy.Task();
      task1.Description = "Попу мыть";
      Main.Copy.Task task2 = new Main.Copy.Task();
      task2.Description = "Кота кормить";
      Main.Copy.Task[] Tasks = new Main.Copy.Task[] { task0, task1, task2 };
      return Tasks;
    }

    /// <summary>
    /// Здесь должен быть запрос ваших задач
    /// </summary>
    /// <returns></returns>
    public static Main.Copy.Task[] GetMyTask()
    {
      Main.Copy.Task task0 = new Main.Copy.Task();
      task0.Description = "Посуду мыть";
      Main.Copy.Task task1 = new Main.Copy.Task();
      task1.Description = "Кушать делать";
      Main.Copy.Task task2 = new Main.Copy.Task();
      task2.Description = "Работа ходить";
      Main.Copy.Task[] Tasks = new Main.Copy.Task[] { task0, task1, task2 };
      return Tasks;
    }
  }
}
