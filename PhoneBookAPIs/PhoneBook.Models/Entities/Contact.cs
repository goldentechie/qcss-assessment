
using System.ComponentModel.DataAnnotations;
using PhoneBook.Models.Entities.Abstraction;

namespace PhoneBook.Models.Entities
{
  public class Contact : IBaseEntity, IAuditable, ISoftDelete
  {
    public int Id { get; set; }

    public bool IsActive { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }

    [MaxLength(15)]
    public string PhoneNumber { get; set; }

    public int CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
  }
}
