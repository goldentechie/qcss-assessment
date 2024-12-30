using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models.DTOs.Contacts
{
  public class ContactPostDto
  {
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(15)]
    public string PhoneNumber { get; set; }
  }
}
