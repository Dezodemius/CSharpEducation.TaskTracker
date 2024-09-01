using Microsoft.AspNetCore.Identity;

namespace Main.Copy
{
  public class AppUser : IdentityUser<string>
  {
    public string Alias { get; set; }

    public IEnumerable<Responsible> Responsibles { get; set; }

    public IEnumerable<Comment> Comments { get; set; }
  }
}