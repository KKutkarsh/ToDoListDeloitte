using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ToDoApi.Domain.Entities;

namespace ToDoApi.Application.Interfaces.Repositories
{
    public interface IUserManagerRepo
    {
        Task<ApplicationUser> GetUserByName(string name);
        Task<bool> VerifyPassword(ApplicationUser user, string password);
        Task<IdentityResult> AddUser(ApplicationUser user, string password);
    }
}
