namespace PhoneBook.Models.DTOs.Users
{
    public class UserGetDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
        public bool RequirePasswordReset { get; set; }

        public List<string> Contacts { get; set; }

        public List<string> Roles { get; set; }

    }
}
