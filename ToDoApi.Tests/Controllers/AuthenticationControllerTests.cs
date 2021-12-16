using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ToDoApi.Application.Interfaces.Helpers;
using ToDoApi.Application.Interfaces.Repositories;
using ToDoApi.Tests.Common.MockHelper;
using ToDoAPI.Controllers;
using ToDoApi.Domain.Entities;

namespace ToDoApi.Tests.Controllers
{
    [TestFixture]
    public class AuthenticationControllerTests
    {
        private IUserManagerRepo _userManager;
        private ILoginHelper _loginHelper;
        private AuthenticationController _sut;

        [SetUp]
        public void Setup()
        {

            _userManager = Substitute.For<IUserManagerRepo>();
            _loginHelper = Substitute.For<ILoginHelper>();
            _sut = new AuthenticationController(_userManager, _loginHelper);
        }

        [Test]
        public async Task GivenLoginModel_whenLoginIsCalled_ItHitsGetJwtSecurityToken()
        {
            // Arrange
            var appUser = MockObject.GetApplicationUser();
            var loginObj = MockObject.GetLoginObject();
            _userManager.GetUserByName(Arg.Any<string>()).Returns(appUser);
            _userManager.VerifyPassword(appUser,loginObj.Password).Returns(true);
            _loginHelper.GetJwtSecurityToken(appUser).Returns(new JwtSecurityToken());

            // Act
            await _sut.Login(loginObj);

            // Assert
            await _loginHelper.Received(1).GetJwtSecurityToken(Arg.Any<ApplicationUser>());
        }

        [Test]
        public async Task GivenRegisterModel_whenRegisterIsCalled_ItHitsAddUser()
        {

            // Arrange
            ApplicationUser appUser = null;
            var identityResultStub = new IdentityResultStub();
            _userManager.GetUserByName(Arg.Any<string>()).Returns(appUser);
            var registerModel = MockObject.GetRegisterObject();
            _userManager.AddUser(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(identityResultStub);
            // Act
            var actualResult= _sut.Register(registerModel);

            // Assert
            await _userManager.Received(1).AddUser(Arg.Any<ApplicationUser>(), Arg.Any<string>());
        }
    }

    public class IdentityResultStub : IdentityResult
    {
        public IdentityResultStub()
        {
            Succeeded = true;
        }
    }
}
