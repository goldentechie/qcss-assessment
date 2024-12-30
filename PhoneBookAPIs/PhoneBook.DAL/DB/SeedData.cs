using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Models.Entities;

namespace PhoneBook.DAL.DB
{
    public class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedUsers(modelBuilder);
            SeedRoles(modelBuilder);
            SeedUserRoles(modelBuilder);

        }

        private static void SeedUsers(ModelBuilder builder)
        {
            var userList = new List<SystemUser> 
            {
                new SystemUser()
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "User",
                    UserName = "admin@phonebook.com",
                    NormalizedUserName = "admin@phonebook.com",
                    Email = "admin@phonebook.com",
                    NormalizedEmail = "admin@phonebook.com",
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                    PhoneNumber = "1234567890",
                    RequirePasswordReset = false,
                }
            };

            var passwordHasher = new PasswordHasher<SystemUser>();

            userList.ForEach(user =>
            {
                user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");
            });

            builder.Entity<SystemUser>().HasData(userList);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            var rolesData = new List<Role>();

            foreach (int e in Enum.GetValues(typeof(ROLES)))
            {
                rolesData.Add(new Role
                {
                    Id = e,
                    Name = ((ROLES)e).ToString(),
                    NormalizedName = ((ROLES)e).ToString().ToUpper(),
                    ConcurrencyStamp = ""
                });
            }

            builder.Entity<Role>().HasData(rolesData);
        }

    private static void SeedUserRoles(ModelBuilder builder)
    {
      builder.Entity<UserRole>().HasData(
          new UserRole() { RoleId = (int)ROLES.Admin, UserId = 1 }
          );
    }
  }
}
