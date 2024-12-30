using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models.DTOs.Users
{
    public class RegisterDto : UpdateDto
    {
        

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
