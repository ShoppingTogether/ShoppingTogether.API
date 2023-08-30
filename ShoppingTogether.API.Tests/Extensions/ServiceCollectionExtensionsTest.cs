using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ShoppingTogether.API.Extensions;
using ShoppingTogether.API.Users;
using System.Data;

namespace ShoppingTogether.API.Tests.Extensions
{
    public class ServiceCollectionExtensionsTest
    {
        private Mock<IConfiguration> _configurationMock = null!;
        public ServiceCollectionExtensionsTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(x => x.GetSection(It.IsAny<string>()))
                .Returns(new ConfigurationSection(new ConfigurationManager(), string.Empty));
        }

        [Fact]
        public void RegisterServices_ReturnsServicesContainsPostgresDbConnection()
        {
            //Arrange
            var services = new ServiceCollection();

            //Act
            services.RegisterServices(_configurationMock.Object);

            //Assert
            var expected = services.FirstOrDefault(descriptor =>
            descriptor.ServiceType == typeof(IDbConnection) &&
            descriptor.Lifetime == ServiceLifetime.Transient);

            Assert.NotNull(expected);
        }

        [Fact]
        public void RegisterServices_ReturnsServicesContainsUserRepository()
        {
            //Arrange
            var services = new ServiceCollection();

            //Act
            services.RegisterServices(_configurationMock.Object);

            //Assert
            var expected = services.FirstOrDefault(descriptor =>
            descriptor.ServiceType == typeof(IUserRepository) &&
            descriptor.ImplementationType == typeof(UserRepository) &&
            descriptor.Lifetime == ServiceLifetime.Transient);

            Assert.NotNull(expected);
        }
    }
}
