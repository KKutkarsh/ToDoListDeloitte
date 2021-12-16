using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoApi.Domain.Entities;

namespace ToDoApi.Application.Interfaces.Helpers
{
    public interface ILoginHelper
    {
        Task<List<Claim>> GetClaimList(ApplicationUser user);
        Task<JwtSecurityToken> GetJwtSecurityToken(ApplicationUser user);
    }
}
