using Microsoft.AspNetCore.Identity;
using PhoneBook.Models.Entities.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Models.Entities
{
  public class UserRole: IdentityUserRole<int>, IBaseEntity
    {
        public SystemUser User { get; set; }
        public Role Role { get; set; }

        [NotMapped]
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
