using System.Security.Claims;

namespace PhoneBook.Models.Infrastructure
{
    public static class Extensions
    {
        public static string GetUsername(this ClaimsPrincipal principal)
        {
            return principal.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault()?.Value ?? string.Empty;
        }

        public static int GetUserId(this ClaimsPrincipal principal)
        {
            return int.Parse(principal.Claims.Where(c => c.Type == "Id").FirstOrDefault()?.Value ?? "0");
        }

        public static string GetUserRole(this ClaimsPrincipal principal)
        {
            return principal.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault()?.Value ?? string.Empty;
        }
    }
}
