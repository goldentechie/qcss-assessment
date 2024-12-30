using Microsoft.AspNetCore.Identity;
using PhoneBook.Models.Entities.Abstraction;

namespace PhoneBook.Models.Entities
{
  public class SystemUser : IdentityUser<int>, IBaseEntity, ISoftDelete
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsActive { get; set; } = true;
        public bool RequirePasswordReset { get; set; } = true;

        public List<Role> Roles { get; set; } = new();
    }
}
