using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ToDoApi.Application.Interfaces.Helpers;
using ToDoApi.Domain.Entities;

namespace ToDoApi.Infrastructure.Helpers
{
    public class LoginHelper : ILoginHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfigurationHelper _configuration;

        public LoginHelper(UserManager<ApplicationUser> userManager, IConfigurationHelper configurationHelper)
        {
            _userManager = userManager;
            _configuration = configurationHelper;
        }

        public async Task<List<Claim>> GetClaimList(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            return authClaims;
        }

        public async Task<JwtSecurityToken> GetJwtSecurityToken(ApplicationUser user)
        {
            var authClaims = await GetClaimList(user);

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetJwtSecret("JWT:Secret")));
            return new JwtSecurityToken(
                    issuer: _configuration.GetJwtValidIssuer("JWT:ValidIssuer"),
                    audience: _configuration.GetJwtValidAudience("JWT:ValidAudience"),
                    expires: DateTime.Now.AddHours(_configuration.GetJwtTtl("JWT:TTL")),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
        }
    }
}
