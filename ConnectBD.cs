using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.Copy;
using Microsoft.EntityFrameworkCore;

namespace Main
{
  public class ApplicationContext : DbContext
  {
    public DbSet<UserRoles> UserRoles { get; set; }
    //public DbSet<Drivers> Drivers { get; set; }
    //public DbSet<TrafficControllers> TrafficControllers { get; set; }
    //public DbSet<DialogMsgs> DialogMsgs { get; set; }
    //public DbSet<DialogMembers> DialogMembers { get; set; }
    //public DbSet<DialogStatus> DialogStatus { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

      //optionsBuilder.UseNpgsql(
      //    "Host=localhost;" +
      //    "Port=5433;" +
      //    "Database=Users;" +
      //    "Username=postgres;" +
      //    "Password=zaqwsxzaq");
    }
    /// <summary>
    /// Здесь должен быть запрос всех доступных задач
    /// </summary>
    /// <returns></returns>
    public static Tasks[] GetAllTask()
    {
      Tasks task0 = new Tasks();
      task0.Description = "Подтягивания делать";
      Tasks task1 = new Tasks();
      task1.Description = "Попу мыть";
      Tasks task2 = new Tasks();
      task2.Description = "Кота кормить";
      Tasks[] Tasks = new Tasks[] { task0, task1, task2 };
      return Tasks;
    }

    /// <summary>
    /// Здесь должен быть запрос ваших задач
    /// </summary>
    /// <returns></returns>
    public static Tasks[] GetMyTask()
    {
      Tasks task0 = new Tasks();
      task0.Description = "Посуду мыть";
      Tasks task1 = new Tasks();
      task1.Description = "Кушать делать";
      Tasks task2 = new Tasks();
      task2.Description = "Работа ходить";
      Tasks[] Tasks = new Tasks[] { task0, task1, task2 };
      return Tasks;
    }
  }
}
