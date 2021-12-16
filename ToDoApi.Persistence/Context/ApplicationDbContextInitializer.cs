using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ToDoApi.Domain.Entities;

namespace ToDoApi.Persistence.Context
{
    public class ApplicationDbContextInitializer
    {
        public static void SeedUser(IServiceProvider serviceProvider)
        {
            SeedUsers(serviceProvider);
        }

        private static void SeedUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            const string username = "test";
            if (userManager.FindByNameAsync(username).Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = username,
                    Email = "abc@xyz.com",

                };

                var result = userManager.CreateAsync(user, "123456").Result;
            }
        }
    }
}
