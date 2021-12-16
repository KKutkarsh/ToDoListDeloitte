using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using ToDoApi.Application.Models;
using ToDoApi.Domain.Entities;

namespace ToDoApi.Tests.Common.MockHelper
{
    public static class MockObject
    {
        public static ApplicationUser GetApplicationUser()
        {
            return new ApplicationUser
            {
                Email = "abc@xyz.com",
                UserName = "abc"
            };
        }
        public static Login GetLoginObject()
        {
            return new Login
            {
                Username = "abc",
                Password = "password"
            };
        }

        public static Register GetRegisterObject()
        {
            return new Register
            {
                Username = "utk",
                Password = "password",
                Email = "utk@xyz.com"
            };
        }

        public static UserManager<ApplicationUser> GetUserManager()
        {
            return new UserManager<ApplicationUser>(
                Substitute.For<IUserStore<ApplicationUser>>(),
                Substitute.For<IOptions<IdentityOptions>>(),
                Substitute.For<IPasswordHasher<ApplicationUser>>(),
                Substitute.For<IEnumerable<IUserValidator<ApplicationUser>>>(),
                Substitute.For<IEnumerable<IPasswordValidator<ApplicationUser>>>(),
                Substitute.For<ILookupNormalizer>(),
                Substitute.For<IdentityErrorDescriber>(),
                Substitute.For<IServiceProvider>(),
                Substitute.For<ILogger<UserManager<ApplicationUser>>>()

            );
        }

        public static RoleManager<IdentityRole> GetRoleManager()
        {
            return new RoleManager<IdentityRole>(
                Substitute.For<IRoleStore<IdentityRole>>(),
                Substitute.For<IEnumerable<IRoleValidator<IdentityRole>>>(),
                Substitute.For<ILookupNormalizer>(),
                Substitute.For<IdentityErrorDescriber>(),
                Substitute.For<ILogger<RoleManager<IdentityRole>>>()
            );
        }
    }
}
