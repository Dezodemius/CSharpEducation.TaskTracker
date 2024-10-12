using Main.Copy;

namespace Main.DataBase
{
  /// <summary>
  /// Класс для работы с базой данных.
  /// </summary>
  internal class DataBaseMethods
  {
    private ApplicationContext DB;

    /// <summary>
    /// Добавление пользователя в базу данных.
    /// </summary>
    /// <param name="appUser"></param>
    public void CreateAppUser(AppUser appUser)
    {
      DB.AppUsers.Add(appUser);
      DB.SaveChanges();
    }

    /// <summary>
    /// Добавление задачи в базу данных.
    /// </summary>
    /// <param name="task"></param>
    public void CreateTask(Main.Copy.Task task)
    {
      DB.Tasks.Add(task);
      DB.SaveChanges();
    }

    /// <summary>
    /// Поиск пользователя в приложении.
    /// </summary>
    /// <param name="appUser">Пользователь</param>
    /// <returns>true/fasle</returns>
    public bool FindAppUser(AppUser appUser)
    {
      if(DB.AppUsers.Find(appUser)!=null)
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Добавление коментария в базу данных.
    /// </summary>
    /// <param name="comment"></param>
    public void CreateComment(Comment comment)
    {
      DB.Comments.Add(comment);
      DB.SaveChanges(); 
    }

    /// <summary>
    /// Удаление пользователя из базы данных.
    /// </summary>
    /// <param name="appUser"></param>
    public void RemoveAppUser(AppUser appUser)
    {
      DB.AppUsers.Remove(appUser);
      DB.SaveChanges();
    }

    /// <summary>
    /// Удаление задачи из базы данных.
    /// </summary>
    /// <param name="task"></param>
    public void RemoveTask(Main.Copy.Task task)
    {
      DB.Tasks.Remove(task);
      DB.SaveChanges();
    }

    /// <summary>
    /// Удаление коментария из базы данных.
    /// </summary>
    /// <param name="comment"></param>
    public void RemoveComment(Comment comment)
    {
      DB.Comments.Remove(comment);
      DB.SaveChanges();
    }

    /// <summary>
    /// Поиск всех задач заданного пользователя.
    /// </summary>
    /// <param name="appUser">Пользователь.</param>
    /// <returns>Список задач.</returns>
    public List<Main.Copy.Task> FindAllAppUserTasks(AppUser appUser)
    {
      List<Main.Copy.Task> findedTasks = DB.Tasks.Where(t=>t.AppUserId==appUser.UserID).ToList();
      return findedTasks;
    }

    /// <summary>
    /// Поиск всех коментариев по задаче.
    /// </summary>
    /// <param name="task">Задача.</param>
    /// <returns>Список коментариев.</returns>
    public List<Comment> FindAllCommentsFromTask(Main.Copy.Task task)
    {
      List<Comment> comments = DB.Comments.Where(c=>c.TaskId==task.Id).ToList();
      return comments;
    }

    /// <summary>
    /// Вывод задачи пользователя по заданному id.
    /// </summary>
    /// <param name="appUser">Пользователь.</param>
    /// <param name="id">Номер задачи.</param>
    /// <returns>Задача[ID].</returns>
    public Main.Copy.Task FindAppUserTaskById(AppUser appUser, int id)
    {
      var tasks = FindAllAppUserTasks(appUser);
      return tasks[id];
    }

    private static DataBaseMethods instance;

    public static DataBaseMethods getInstance()
    {
      if (instance == null)
        instance = new DataBaseMethods();
      return instance;
    }

    private DataBaseMethods()
    {
      DB= new ApplicationContext();
    }
  }
}
