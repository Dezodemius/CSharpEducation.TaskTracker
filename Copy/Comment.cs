namespace Main.Copy
{
  public class Comment : IEntity<Guid>
  {
    public Guid Id { get; set; }

    public string Description { get; set; }

    public DateTime DateCreate { get; set; }

    public string AppUserId { get; set; }

    public Guid TaskId { get; set; }
  }
}