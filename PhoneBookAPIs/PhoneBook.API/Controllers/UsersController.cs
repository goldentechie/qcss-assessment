using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.API.Auth;
using PhoneBook.BL.Helpers;
using PhoneBook.DAL;
using PhoneBook.DAL.Repositories.Abstraction;
using System.Security.Claims;
using PhoneBook.Models.Entities;
using PhoneBook.API.Models;
using PhoneBook.Models.DTOs.Users;
using PhoneBook.Models.Infrastructure;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        private readonly UserManager<SystemUser> _userManager;
        private readonly IJWTManager _jwtManager;
        private readonly IUserRepository _userRepository;

        public UsersController(UserManager<SystemUser> userManager, IJWTManager jwtManager, IUserRepository userRepository)
        {
            this._userManager = userManager;
            this._jwtManager = jwtManager;
            this._userRepository = userRepository;
        }

        [Route("resetpassword")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await this._userManager.FindByEmailAsync(User.GetUsername());

            if (user == null)
            {
                return NotFound();
            }

            var isPasswordValid = await this._userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (isPasswordValid)
            {
                var result = await this._userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);
                if (result.Succeeded)
                {
                    var userEntity = this._userRepository.Get().Where(u => u.Email == User.GetUsername()).FirstOrDefault();
                    userEntity.RequirePasswordReset = false;
                    await this._userRepository.SaveChanges();

                    return await Authenticate(new AuthenticateDto
                    {
                        Password = model.Password,
                        Username = User.GetUsername(),
                    });
                }
                else
                {
                    return Ok(new { IsError = true, Message = string.Join(",", result.Errors) });
                }
            }
            else
            {
                return Ok(new { IsError = true, Message = "current password is invalid" });
            }
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateDto userData)
        {
            var user = await this._userManager.FindByNameAsync(userData.Username);
            if (user != null)
            {
                var isPasswordValid = await this._userManager.CheckPasswordAsync(user, userData.Password);
                if (isPasswordValid)
                {
                    var userEntity = await this._userRepository.Get(user.Id)
                        .Include(u => u.Roles)
                        .FirstOrDefaultAsync();

                    var claims = new List<Claim>();
                    var userRoles = await this._userManager.GetRolesAsync(user);
                    if (userRoles != null)
                    {
                        userRoles.ToList().ForEach(r =>
                        {
                            claims.Add(new Claim(ClaimTypes.Role, r));
                        });
                    }

                    claims.Add(new Claim("Id", userEntity.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, userData.Username));
                    claims.Add(new Claim(ClaimTypes.Email, userData.Username));
                    var token = this._jwtManager.Authenticate(claims);

                    var userDetails = new UserGetDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Token = token.Token,
                        RequirePasswordReset = userEntity.RequirePasswordReset,
                        Roles = new List<string>(userRoles?.ToArray() ?? new List<string>().ToArray()),
                    };


                    return Ok(new APIResponse<UserGetDto> 
                    { 
                        IsError = false, 
                        Message = "", 
                        data = userDetails 
                    });
                }
            }
            return Ok(new { IsError = true, Message = "Invalid username or password" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var password = UserRegistrationHelper.GenerateTemporaryPassword();
            var userEntity = UserRegistrationHelper.GetUserEntity(model.Email, model.FirstName, model.LastName, model.PhoneNumber, password);

            userEntity.Roles = new List<Role> { new Role { Id = (int)ROLES.SystemUser } };
            
            this._userRepository.Add(userEntity);
            await this._userRepository.SaveChanges();

            return Created("", new { IsError = false, Message = "" });
            
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await this._userRepository.Get().Include(u => u.Roles)
                .OrderByDescending(x => x.Id).ToListAsync();

            var result = new List<UserGetDto>();
            foreach (var user in users)
            {
                result.Add(new UserGetDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = user.Roles.Select(x => x.Name).ToList(),
                });
            }

            return Ok(new APIResponse<List<UserGetDto>>
            {
                IsError = false,
                Message = "",
                data = result
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await this._userRepository.Get(id)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var data = new UserGetDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = user.Roles.Select(x => x.Name).ToList(),
                };

                return Ok(new APIResponse<UserGetDto>
                {
                    IsError = false,
                    Message = "",
                    data = data
                });
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDto model)
        {
            var user = await this._userRepository.Get(id)
              .FirstOrDefaultAsync();

            if (user != null)
            {
                user.PhoneNumber = model.PhoneNumber;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
        
                this._userRepository.Update(user);
                await this._userRepository.SaveChanges();
                return Ok();
            }

            return NotFound();
        }

        
        [Route("entityexists/{data}")]
        [HttpGet]
        public async Task<bool> EntityExists(string data)
        {
            var userEntity = await this._userRepository.Get().Where(u => u.Email.ToLower() == data.ToLower()).FirstOrDefaultAsync();
            if (userEntity != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
