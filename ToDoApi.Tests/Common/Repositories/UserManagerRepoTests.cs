using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using ToDoApi.Domain.Entities;
using ToDoApi.Infrastructure.Repositories;
using ToDoApi.Persistence.Context;
using ToDoApi.Tests.Common.MockHelper;

namespace ToDoApi.Tests.Common.Repositories
{
    [TestFixture]
    public class UserManagerRepoTests
    {
        private UserManager<ApplicationUser> _userManager;
        private UserManagerRepo _sut;
        private ApplicationDbContext _inMemoryDbContext;
        [SetUp]
        public void Setup()
        {
            _userManager = MockObject.GetUserManager();
            _inMemoryDbContext= InMemoryDbContextFactory.CreateInMemoryDbContext();
            _sut = new UserManagerRepo(_userManager);
        }

        [Test]
        public async Task GivenUserIssavedInDb_WhenGetUserByNameIsCalled_ThenShouldReturnUserRecord()
        {
            // Arrange
            var appUser = MockObject.GetApplicationUser();
            _userManager.FindByNameAsync(appUser.UserName).Returns(appUser);

            // Act
            var actualItems = await _sut.GetUserByName(appUser.UserName);

            // Assert
            actualItems.UserName.Should().Be(appUser.UserName);
            actualItems.Should().BeEquivalentTo(appUser);
        }

        //TODO: Write similar unit tests for Other Methods.
    }
}
