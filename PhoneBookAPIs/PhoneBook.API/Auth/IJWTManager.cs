using System.Security.Claims;

namespace PhoneBook.API.Auth
{
    public interface IJWTManager
    {
        AuthTokens Authenticate(IEnumerable<Claim> claims);
    }
}
