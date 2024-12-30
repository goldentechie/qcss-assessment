using PhoneBook.Models.Services.Abstraction;
using PhoneBook.Models.Infrastructure;

using Microsoft.AspNetCore.Http;

namespace PhoneBook.Models.Services
{
  public class CurrentUserService : ICurrentUserService
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    public int GetCurrentUserId()
    {
      return this._httpContextAccessor.HttpContext.User.GetUserId();
    }
  }
}
