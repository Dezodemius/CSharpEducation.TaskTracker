using Main.Copy;
using NLog;
using System.Threading.Tasks;

namespace Main.DataBase
{
    /// <summary>
    /// Класс для работы с базой данных.
    /// </summary>
    internal class DataBaseMethods
    {
        private ApplicationContext DB;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static DataBaseMethods instance;

        /// <summary>
        /// Добавление пользователя в базу данных.
        /// </summary>
        /// <param name="appUser"></param>
        public void CreateAppUser(AppUser appUser)
        {
            using (var context = new ApplicationContext())
            {
                context.AppUsers.Add(appUser);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Добавление задачи в базу данных.
        /// </summary>
        /// <param name="task"></param>
        public void CreateTask(Main.Copy.Task task)
        {
            using (var context = new ApplicationContext())
            {
                if(context.Tasks.Any(t=>t.Id ==task.Id))
                {
                    throw new InvalidOperationException("Задача с таким идентификатором уже существует");
                }
                if (task.Id == Guid.Empty)
                {
                    task.Id = Guid.NewGuid();
                }

                if (task.EndDate == DateTime.MinValue)
                {
                    task.EndDate = task.StartDate.AddDays(1); // Если EndDate не задано
                }

                task.StartDate = task.StartDate.ToUniversalTime();
                task.EndDate = task.EndDate.ToUniversalTime();

                logger.Info($"Добавление задачи в базу данных: Id={task.Id}, Description={task.Description}, " +
                $"StartDate={task.StartDate}, EndDate={task.EndDate}, AppUserId={task.AppUserId}");

                context.Tasks.Add(task);
                context.SaveChanges();
            }

        }

        /// <summary>
        /// Поиск пользователя в приложении.
        /// </summary>
        /// <param name="appUser">Пользователь</param>
        /// <returns>true/fasle</returns>
        public bool FindAppUser(AppUser appUser)
        {
            using (var context = new ApplicationContext())
            {
                return context.AppUsers.Find(appUser) != null;
            }
        }

        /// <summary>
        /// Поиск всех задач заданного пользователя.
        /// </summary>
        /// <param name="appUser">Пользователь.</param>
        /// <returns>Список задач.</returns>
        public List<Main.Copy.Task> FindAllAppUserTasks(AppUser appUser)
        {
            using (var context = new ApplicationContext())
            {
                return context.Tasks.Where(t => t.AppUserId == appUser.UserID).ToList();
            }
        }

        /// <summary>
        /// Добавление коментария в базу данных.
        /// </summary>
        /// <param name="comment"></param>
        public void CreateComment(Comment comment)
        {
            using (var context = new ApplicationContext())
            {
                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Удаление пользователя из базы данных.
        /// </summary>
        /// <param name="appUser"></param>
        public void RemoveAppUser(AppUser appUser)
        {
            using (var context = new ApplicationContext())
            {
                context.AppUsers.Remove(appUser);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Удаление задачи из базы данных.
        /// </summary>
        /// <param name="task"></param>
        public string RemoveTask(Guid taskId)
        {
            using (var context = new ApplicationContext())
            {
                var taskToRemove = context.Tasks.SingleOrDefault(t => t.Id == taskId);

                if (taskToRemove == null)
                {
                    return "Задача не найдена.";
                }

                context.Tasks.Remove(taskToRemove);
                context.SaveChanges();

                return "Задача удалена!";
            }

        }

        /// <summary>
        /// Удаление коментария из базы данных.
        /// </summary>
        /// <param name="comment"></param>
        public void RemoveComment(Comment comment)
        {
            using (var context = new ApplicationContext())
            {
                context.Comments.Remove(comment);
                context.SaveChanges();
            }

        }

        /// <summary>
        /// Поиск всех коментариев по задаче.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <returns>Список коментариев.</returns>
        public List<Comment> FindAllCommentsFromTask(Main.Copy.Task task)
        {
            using (var context = new ApplicationContext())
            {
                return context.Comments.Where(c => c.TaskId == task.Id).ToList();
            }
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

        public static DataBaseMethods getInstance()
        {
            if (instance == null)
                instance = new DataBaseMethods();
            return instance;
        }

        // Метод для получения всех задач
        public List<Main.Copy.Task> GetAllTasks()
        {
            try
            {
                using (var context = new ApplicationContext()) 
                {
                    return context.Tasks.ToList(); 
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                return new List<Main.Copy.Task>();
            }
        }

        // Метод для получения задач конкретного пользователя
        public List<Main.Copy.Task> GetMyTask(string appUserId)
        {
            using (var context = new ApplicationContext())
            {
                return context.Tasks.Where(t => t.AppUserId == appUserId).ToList();
            }
        }

        private DataBaseMethods()
        {
            DB = new ApplicationContext();
        }
    }
}
