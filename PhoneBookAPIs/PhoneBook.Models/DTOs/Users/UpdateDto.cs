using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models.DTOs.Users
{
    public class UpdateDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

    }
}
