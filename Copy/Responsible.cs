namespace Main.Copy
{
  public class Responsible
  {
    public Guid TaskId { get; set; }

    public string? AppUserId { get; set; }

    public AppUser? AppUser { get; set; }

    public Task? Task { get; set; }
  }
}