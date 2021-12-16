using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using ToDoApi.Infrastructure.Helpers;

namespace ToDoApi.Tests.Common.Helpers
{
    [TestFixture]
    public class ConfigurationHelperTests
    {
        private IConfiguration _configuration;
        private ConfigurationHelper _sut;

        private const string JwtValidAudienceKey = "JwtValidAudienceKey";
        private const string JwtValidIssuerKey = "JwtValidIssuerKey";
        private const string JwtSecretKey = "JwtSecretKey";
        private const string JwtTtlKey = "JwtTTLKey";

        [SetUp]
        public void Setup()
        {
            _configuration = Substitute.For<IConfiguration>();
            _sut = new ConfigurationHelper(_configuration);
        }

        [Test]
        public void GivenTheKey_WhenGetJwtValidAudienceIsCalled_ThenItReturnesValidAudienceString()
        {
            // Arrange
            var jwtValidAudienceResponse = "JwtValidAudienceResponse";
            _configuration[JwtValidAudienceKey].Returns(jwtValidAudienceResponse);

            // Act
            var actualResponse= _sut.GetJwtValidAudience(JwtValidAudienceKey);

            // Assert
            actualResponse.Should().Be(jwtValidAudienceResponse);
        }

        [Test]
        public void GivenTheKey_WhenGetJwtValidIssuerCalled_ThenItReturnesValidIssuerString()
        {
            // Arrange
            var jwtValidIssuerResponse = "JwtValidIssuerResponse";

            // Act
            _configuration[JwtValidIssuerKey].Returns(jwtValidIssuerResponse);
            var actualResponse = _sut.GetJwtValidIssuer(JwtValidIssuerKey);

            // Assert
            actualResponse.Should().Be(jwtValidIssuerResponse);
        }

        [Test]
        public void GivenTheKey_WhenGetJwtSecretCalled_ThenItReturnesJwtSecretString()
        {
            // Arrange
            var jwtSecretResponse = "JwtSecretResponse";

            // Act
            _configuration[JwtSecretKey].Returns(jwtSecretResponse);
            var actualResponse = _sut.GetJwtValidIssuer(JwtSecretKey);

            // Assert
            actualResponse.Should().Be(jwtSecretResponse);
        }

        [Test]
        public void GivenTheKey_WhenGetJwtTTLCalled_ThenItReturnesGetJwtTTLValue()
        {
            // Arrange
            var jwtTtlResponse = 5;

            // Act
            _configuration[JwtTtlKey].Returns(jwtTtlResponse.ToString());
            var actualResponse = _sut.GetJwtTtl(JwtTtlKey);

            // Assert
            actualResponse.Should().Be(jwtTtlResponse);
        }
    }
}
