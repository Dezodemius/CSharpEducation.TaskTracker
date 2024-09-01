namespace Main.Copy
{
  public interface IEntity<TKey>
  {
    TKey Id { get; set; }
  }
}