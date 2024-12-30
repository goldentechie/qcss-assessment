using Microsoft.AspNetCore.Identity;
using PhoneBook.Models.Entities.Abstraction;

namespace PhoneBook.Models.Entities
{
  public class Role : IdentityRole<int>, IBaseEntity
    {
        public override int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public List<SystemUser> Users { get; set; } = new();

    }
}
