using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models.DTOs.Users
{
    public class ResetPasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
