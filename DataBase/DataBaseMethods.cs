using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.DataBase
{
    internal class DataBaseMethods
    {
        // метод для создания сообщения водителем. данные методы мне кажутся излишними, но я решил их оставить тк все работает
        //public static async Task<long> MsgCreateByDriverToTc(int IdThread, string Msg, string Crtd, string UL)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    DialogMsgs newMSG = new DialogMsgs { IdThread = IdThread, Created = Crtd, UrgentLevel = UL, Message = Msg };
        //    await db.DialogMsgs.AddAsync(newMSG);
        //    await db.SaveChangesAsync();
        //    try
        //    {
        //      var getMSG = await db.DialogMsgs.OrderBy(x => x.Created).LastOrDefaultAsync(x => x.IdThread == IdThread);
        //      return getMSG.IdUnique;
        //    }
        //    catch (Exception ex)
        //    {
        //      return 123123;
        //    }

        //  }
        //}
        //// метод для создания сообщения диспетчером
        //public static async Task<long> MsgCreateByTcToDriver(int IdThread, string Msg, string Crtd, string UL)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    DialogMsgs newMSG = new DialogMsgs { IdThread = IdThread, Created = Crtd, UrgentLevel = UL, Message = Msg };
        //    await db.DialogMsgs.AddAsync(newMSG);
        //    await db.SaveChangesAsync();
        //    try
        //    {
        //      var getMSG = await db.DialogMsgs.OrderBy(x => x.IdUnique).LastOrDefaultAsync(x => x.IdThread == IdThread);
        //      return getMSG.IdUnique;
        //    }
        //    catch (Exception ex)
        //    {
        //      return 123123;
        //    }

        //  }
        //}
        //// метод для получения сообщений диспетчером
        //public static async Task<long> MsgRecievierTc(long IdUnique, int IdThread, long IdDriver)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var getMSG = await db.DialogMsgs.FirstOrDefaultAsync(x => x.IdUnique == IdUnique);
        //    var getId = await db.DialogMembers.FirstOrDefaultAsync(x => (x.IdThread == IdThread && x.IdDriver == IdDriver));
        //    if (getMSG.IdThread == getId.IdThread)
        //    {
        //      return getId.IdTc;
        //    }
        //    else
        //    {
        //      return 404;
        //    }
        //  }
        //}
        //// метод для получения сообщений водителем 
        //public static async Task<long> MsgRecievierDriver(long IdUnique, int IdThread, long IdTc)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var getMSG = await db.DialogMsgs.FirstOrDefaultAsync(x => x.IdUnique == IdUnique);
        //    var getId = await db.DialogMembers.FirstOrDefaultAsync(x => (x.IdThread == IdThread && x.IdTc == IdTc));
        //    if (getMSG.IdThread == getId.IdThread)
        //    {
        //      return getId.IdDriver;
        //    }
        //    else
        //    {
        //      return 404;
        //    }
        //  }
        //}
        //// получение стаутса пользователя, чтобы бот мог разделять сообщения от пользователя по данному фильтру
        //public static async Task<int> GetStatus(long TgId)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var status = await db.DialogStatus.FirstOrDefaultAsync(x => x.TgId == TgId);
        //    return status.Status;
        //  }
        //}
        //// получение id юзера для отправки сообщения через бота из другого юзера при условии нажатия кнопок в диалогах
        //public static async Task<long> GetAddress(long TgId)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var status = await db.DialogStatus.FirstOrDefaultAsync(x => x.TgId == TgId);
        //    return status.CurrentDialog;
        //  }
        //}
        //// получение id диалога из бд
        //public static async Task<int> GetThreadByDriver(long IdDriver)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var dialog = await db.DialogMembers.FirstOrDefaultAsync(x => x.IdDriver == IdDriver);
        //    return dialog.IdThread;
        //  }
        //}
        //// получение id диалога из бд
        //public static async Task<int> GetThreadByTc(long IdTc, long IdDriver)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var dialog = await db.DialogMembers.FirstOrDefaultAsync(x => x.IdTc == IdTc && x.IdDriver == IdDriver);
        //    return dialog.IdThread;
        //  }
        //}
        //// получение роли юзера
        //public static async Task<UserRoles> GetUserRole(long TgId)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var user = await db.UserRoles.FirstOrDefaultAsync(x => x.TgId == TgId);
        //    return user;
        //  }
        //}
        //// получение данных водителя
        //public static async Task<Drivers> GetDriverData(long IdDriver)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var driver = await db.Drivers.FirstOrDefaultAsync(x => x.IdDriver == IdDriver);
        //    return driver;
        //  }
        //}
        //// получение данных диспетчера
        //public static async Task<TrafficControllers> GetTcData(long IdTc)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var tc = await db.TrafficControllers.FirstOrDefaultAsync(x => x.IdTc == IdTc);
        //    return tc;
        //  }
        //}
        //// обновление статуса юзера и id получателя сообщения
        //public static async Task ToggleInDialogStatus(long TgId, int Status, long receivier)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var dialogStatus = await db.DialogStatus.FirstOrDefaultAsync(x => x.TgId == TgId);
        //    if (dialogStatus is null)
        //    {
        //      DialogStatus StatusCreate = new DialogStatus { TgId = TgId, Status = Status, CurrentDialog = receivier };
        //      var result = await db.DialogStatus.AddAsync(StatusCreate);
        //      await db.SaveChangesAsync();
        //    }
        //    else
        //    {
        //      dialogStatus.Status = Status;
        //      dialogStatus.CurrentDialog = receivier;
        //      db.DialogStatus.Update(dialogStatus);
        //      await db.SaveChangesAsync();
        //    }
        //  }
        //}
        //// обновление статуса юзера
        //public static async Task ToggleInDialogStatus(long TgId, int Status)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var dialogStatus = await db.DialogStatus.FirstOrDefaultAsync(x => x.TgId == TgId);
        //    if (dialogStatus is null)
        //    {
        //      DialogStatus StatusCreate = new DialogStatus { TgId = TgId, Status = Status, CurrentDialog = 0 };
        //      var result = await db.DialogStatus.AddAsync(StatusCreate);
        //      await db.SaveChangesAsync();
        //    }
        //    else
        //    {
        //      dialogStatus.Status = Status;
        //      dialogStatus.CurrentDialog = 0;
        //      db.DialogStatus.Update(dialogStatus);
        //      await db.SaveChangesAsync();
        //    }
        //  }
        //}
        //// добавление или обновление юзера
        //public static async Task AddOrUpdateUser(long tg_ID, string role, string tg_username, long tg_chat_id, int StageReg)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var user = await db.UserRoles.FirstOrDefaultAsync(x => x.TgId == tg_ID);
        //    if (user is null)
        //    {
        //      if (tg_username == null)
        //      {
        //        tg_username = "Без ника";
        //      }
        //      UserRoles newuser = new UserRoles { TgId = tg_ID, Role = role, TgUsername = tg_username, TgChatId = tg_chat_id, StageReg = StageReg };
        //      await db.UserRoles.AddAsync(newuser);
        //      await db.SaveChangesAsync();
        //    }
        //    else
        //    {
        //      user.Role = role;
        //      user.TgUsername = tg_username;
        //      db.UserRoles.Update(user);
        //      await db.SaveChangesAsync();
        //    }
        //  }

        //}
        //// создание диалога
        //public static async Task DialogCreate(long IdTc, long IdDriver)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var dialogWithDriver = await db.DialogMembers.FirstOrDefaultAsync(x => x.IdDriver == IdDriver && x.IdTc == IdTc);
        //    if (dialogWithDriver is null)
        //    {
        //      DialogMembers newDialog = new DialogMembers { IdDriver = IdDriver, IdTc = IdTc };
        //      var result = await db.DialogMembers.AddAsync(newDialog);
        //      await db.SaveChangesAsync();
        //    }
        //    else
        //    {

        //    }
        //  }
        //}
        //// изменение статуса пользователя
        //public static async Task StageIncrement(long tg_ID, int StageReg)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var user = await db.UserRoles.FirstOrDefaultAsync(x => x.TgId == tg_ID);
        //    user.StageReg = StageReg;
        //    db.UserRoles.Update(user);
        //    await db.SaveChangesAsync();
        //  }
        //}
        //// добавление водителя до регистрации все строки по нулям, чтобы exception не словить
        //public static async Task AddDriver(long IdDriver)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var user = await db.Drivers.FirstOrDefaultAsync(x => x.IdDriver == IdDriver);
        //    if (user is null)
        //    {
        //      string Name = "0";
        //      string IdRoute = "0";
        //      string VehichleRegNum = "0";
        //      string DeviceSerialNum = "0";
        //      Drivers newDriver = new Drivers { IdDriver = IdDriver, Name = Name, IdRoute = IdRoute, VehichleRegNum = VehichleRegNum, DeviceSerialNum = DeviceSerialNum };
        //      var result = db.Drivers.AddAsync(newDriver);
        //      await db.SaveChangesAsync();
        //    }

        //  }
        //}
        //// добавление диспетчера
        //public static async Task AddTc(long IdTc)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var user = await db.TrafficControllers.FirstOrDefaultAsync(x => x.IdTc == IdTc);
        //    if (user is null)
        //    {
        //      string Name = "0";
        //      TrafficControllers newTc = new TrafficControllers { IdTc = IdTc, Name = Name };
        //      var result = await db.TrafficControllers.AddAsync(newTc);
        //      await db.SaveChangesAsync();
        //    }

        //  }
        //}
        //// добавление данных в бд водителя
        //public static async Task AddDataDriverName(long IdDriver, string Name)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var Driver = await db.Drivers.FirstOrDefaultAsync(x => x.IdDriver == IdDriver);
        //    Driver.Name = Name;
        //    db.Drivers.Update(Driver);
        //    await db.SaveChangesAsync();
        //  }
        //}
        //public static async Task AddDataDriverIdRoute(long IdDriver, string IdRoute)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var Driver = await db.Drivers.FirstOrDefaultAsync(x => x.IdDriver == IdDriver);
        //    Driver.IdRoute = IdRoute;
        //    db.Drivers.Update(Driver);
        //    await db.SaveChangesAsync();
        //  }
        //}
        //public static async Task AddDataDriverVehichleRegNum(long IdDriver, string VehichleRegNum)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var Driver = await db.Drivers.FirstOrDefaultAsync(x => x.IdDriver == IdDriver);
        //    Driver.VehichleRegNum = VehichleRegNum;
        //    db.Drivers.Update(Driver);
        //    await db.SaveChangesAsync();
        //  }
        //}
        //public static async Task AddDataDriverDeviceSerialNum(long IdDriver, string DeviceSerialNum)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var Driver = await db.Drivers.FirstOrDefaultAsync(x => x.IdDriver == IdDriver);
        //    Driver.DeviceSerialNum = DeviceSerialNum;
        //    db.Drivers.Update(Driver);
        //    await db.SaveChangesAsync();
        //  }
        //}
        //// добавление данных в бд диспетчера
        //public static async Task AddDataTcName(long IdDriver, string Name)
        //{
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var tc = await db.TrafficControllers.FirstOrDefaultAsync(x => x.IdTc == IdDriver);
        //    tc.Name = Name;
        //    db.TrafficControllers.Update(tc);
        //    await db.SaveChangesAsync();
        //  }
        //}
        //// вывод списка водителей
        //public static List<long> GetAllDriversId(string role)
        //{
        //  //Dictionary<long, string> driversId_Name = new Dictionary<long, string>();
        //  using (ApplicationContext db = new ApplicationContext())
        //  {
        //    var AllDrivers = db.UserRoles.Where(x => x.Role == role).ToList();
        //    List<long> driversIDs = new List<long>();
        //    foreach (var u in AllDrivers)
        //      driversIDs.Add(u.TgId);
        //    return driversIDs;
        //  }

        //}
    }
}
