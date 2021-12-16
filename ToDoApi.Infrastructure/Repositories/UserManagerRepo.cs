using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ToDoApi.Application.Interfaces.Repositories;
using ToDoApi.Domain.Entities;

namespace ToDoApi.Infrastructure.Repositories
{
    public class UserManagerRepo: IUserManagerRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserManagerRepo(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserByName(string name)
        {
           return  await _userManager.FindByNameAsync(name);
        }

        public async Task<bool> VerifyPassword(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> AddUser(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}
