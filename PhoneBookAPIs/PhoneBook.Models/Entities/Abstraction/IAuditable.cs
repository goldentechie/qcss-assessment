namespace PhoneBook.Models.Entities.Abstraction
{
  public interface IAuditable
  {
    public int CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
  }
}
