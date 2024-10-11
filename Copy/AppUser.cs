using Microsoft.AspNetCore.Identity;

namespace Main.Copy
{
  /// <summary>
  /// Данные юзера.
  /// </summary>
  public class AppUser : IdentityUser<string>
  {
    /// <summary>
    /// Псевдоним,имя
    /// </summary>
    public string? Alias { get; set; }

    public string? UserID { get; set; }

    //public IEnumerable<Responsible>? Responsibles { get; set; }

    //public IEnumerable<Comment>? Comments { get; set; }
  }
}