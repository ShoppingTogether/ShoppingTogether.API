using Dapper;
using Moq;
using Moq.Dapper;
using ShoppingTogether.API.Users;
using ShoppingTogether.API.Users.Models;
using System.Data;

namespace ShoppingTogether.API.Tests.Users
{
    public class UserRepositoryTests
    {
        private readonly Mock<IDbConnection> _connectionMock;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            _connectionMock = new();

            _userRepository = new UserRepository(_connectionMock.Object);
        }

        [Fact]
        public async Task GetAllUsersAsync_EmptyUsersTable_ReturnsEmptyList()
        {
            //Arrange
            _connectionMock.SetupDapperAsync(x => x.QueryAsync<User>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(() => null!);

            //Act
            var actual = await _userRepository.GetAllUsersAsync();

            //Assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public async Task GetAllUsersAsync_UsersFound_ReturnsUsersList()
        {
            //Arrange
            var expected = new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Test",
                    Sid = "123",
                    CreatedAt = DateTime.Now
                }
            };

            _connectionMock.SetupDapperAsync(x => x.QueryAsync<User>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(expected);

            //Act
            var actual = await _userRepository.GetAllUsersAsync();

            //Assert
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);

            Assert.Equal(expected.Count, actual.Count);
            Assert.Equal(expected[0].Id, actual[0].Id);
            Assert.Equal(expected[0].Name, actual[0].Name);
            Assert.Equal(expected[0].Sid, actual[0].Sid);
            Assert.Equal(expected[0].CreatedAt, actual[0].CreatedAt);
        }

        [Fact]
        public async Task GetUserBySidAsync_SidNotPassed_ReturnsNull()
        {
            //Arrange

            //Act
            var actual = await _userRepository.GetUserBySidAsync(string.Empty);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task GetUserBySidAsync_UserNotFound_ReturnsNull()
        {
            //Arrange
            var sid = "123";

            _connectionMock.SetupDapperAsync(x => x.QueryFirstOrDefaultAsync<User>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(() => null!);

            //Act
            var actual = await _userRepository.GetUserBySidAsync(sid);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task GetUserBySidAsync_UserFound_ReturnsUser()
        {
            //Arrange
            var sid = "123";

            var expected = new User
            {
                Id = 1,
                Name = "Test",
                Sid = sid,
                CreatedAt = DateTime.Now
            };

            _connectionMock.SetupDapperAsync(x => x.QueryFirstOrDefaultAsync<User>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(expected);

            //Act
            var actual = await _userRepository.GetUserBySidAsync(sid);

            //Assert
            Assert.NotNull(actual);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(sid, actual.Sid);
            Assert.Equal(expected.CreatedAt, actual.CreatedAt);
        }

        [Fact]
        public async Task AddUserAsync_SidNotPassed_ReturnsNull()
        {
            //Arrange

            //Act
            var actual = await _userRepository.AddUserAsync("Test", string.Empty);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task AddUserAsync_NameNotPassed_ReturnsNull()
        {
            //Arrange

            //Act
            var actual = await _userRepository.AddUserAsync(string.Empty, "123");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task AddUserAsync_NameAndSidNotPassed_ReturnsNull()
        {
            //Arrange

            //Act
            var actual = await _userRepository.AddUserAsync(string.Empty, string.Empty);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task AddUserAsync_Success_ReturnsUser()
        {
            //Arrange
            var name = "Test";
            var sid = "123";

            var expected = new User
            {
                Id = 1,
                Name = name,
                Sid = sid,
                CreatedAt = DateTime.Now
            };

            _connectionMock.SetupDapperAsync(x => x.QueryFirstOrDefaultAsync<User>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(expected);

            //Act
            var actual = await _userRepository.AddUserAsync(name, sid);

            //Assert
            Assert.NotNull(actual);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(name, actual.Name);
            Assert.Equal(sid, actual.Sid);
            Assert.Equal(expected.CreatedAt, actual.CreatedAt);
        }

    }
}

