using Microsoft.AspNetCore.Identity;
using PhoneBook.Models.Entities;

namespace PhoneBook.BL.Helpers
{
    public class UserRegistrationHelper
    {
        public static SystemUser GetUserEntity(string username, string firstName, string lastName, string phoneNumber, string password)
        {
            var userEntity = new SystemUser()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                Email = username,
                NormalizedEmail = username.ToUpper(),
                LockoutEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = phoneNumber,
                IsActive = true,
                PhoneNumberConfirmed = true,
            };

            var passwordHasher = new PasswordHasher<SystemUser>();
            userEntity.PasswordHash = passwordHasher.HashPassword(userEntity, password);

            return userEntity;
        }

        public static string GenerateTemporaryPassword(int length = 8)
        {
            return "Admin*123";

            var specialChars = new string[] { "*", "@", "#", "&", "!", "$" };
            var prefix = "Pro";
            var result = prefix + "";

            var randomiser = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < length - prefix.Length ; i ++)
            {

                if (i == 0)
                {
                    result = $"{result}{specialChars[randomiser.Next(0, specialChars.Length)]}";
                }
                else if (i % 2 == 0)
                {
                    result = $"{result}{randomiser.Next(0, 10)}";
                }
                else if(i % 3 == 0)
                {
                    result = $"{result}{Convert.ToChar(randomiser.Next(65, 91))}";
                }
                else
                {
                    result = $"{result}{Convert.ToChar(randomiser.Next(97, 122))}";
                }
            }
            return result;
        }
    }
}
