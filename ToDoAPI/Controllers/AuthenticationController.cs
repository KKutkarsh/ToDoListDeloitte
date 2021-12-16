using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ToDoApi.Application.Interfaces.Helpers;
using ToDoApi.Application.Interfaces.Repositories;
using ToDoApi.Application.Models;
using ToDoApi.Domain.Entities;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserManagerRepo _userManager;
        private readonly ILoginHelper _loginHelper;

        public AuthenticationController(IUserManagerRepo userManagerRepo,
            ILoginHelper loginHelper)

        {
            _userManager = userManagerRepo;
            _loginHelper = loginHelper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.GetUserByName(model.Username);

            if (user != null && await _userManager.VerifyPassword(user, model.Password))
            {
                JwtSecurityToken token = await _loginHelper.GetJwtSecurityToken(user);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var userExists = await _userManager.GetUserByName(model.Username);

            if (userExists != null)
            {
                return base.StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Status = "Error", Message = "User already exists!" });
            };

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.AddUser(user, model.Password);

            if (!result.Succeeded)
            {
                return base.StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            return base.Ok(new ApiResponse { Status = "Success", Message = "User created successfully" });
        }
    }
}
