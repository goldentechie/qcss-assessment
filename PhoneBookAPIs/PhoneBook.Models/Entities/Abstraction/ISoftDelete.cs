namespace PhoneBook.Models.Entities.Abstraction
{
  public interface ISoftDelete
  {
    public bool IsActive { get; set; }
  }
}
