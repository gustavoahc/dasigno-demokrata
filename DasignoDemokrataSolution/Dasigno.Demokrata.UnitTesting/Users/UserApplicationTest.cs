using Dasigno.Demokrata.Core.Application.Parameters.Messages;
using Dasigno.Demokrata.Core.Application.Services.Users;
using Dasigno.Demokrata.Core.Domain.Entities;
using Dasigno.Demokrata.Core.Domain.Exceptions.UserExceptions;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace Dasigno.Demokrata.UnitTesting.Users
{
    internal class UserApplicationTest
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IOptions<DatabaseMessages>> _configMock;
        private readonly DatabaseMessages _databaseMessages;

        public UserApplicationTest()
        {
            _configMock = new Mock<IOptions<DatabaseMessages>>();
            _databaseMessages = new DatabaseMessages
            {
                InsertingErrorMessage = "Insert error",
                UpdatingErrorMessage = "Update error",
                DeletingErrorMessage = "Delete error"
            };
        }

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Test]
        public async Task GetUsers_OnSuccess_ReturnsUsersList()
        {
            _userRepositoryMock.Setup(s => s.GetAllAsync())
                .ReturnsAsync(UserTestData.GetUsers());
            _configMock.Setup(x => x.Value).Returns(_databaseMessages);
            var service = new UserService(_userRepositoryMock.Object, _configMock.Object);

            var result = await service.GetUsersAsync();

            result.Should().BeOfType<List<User>>();
            result.Should().NotContainNulls();
            result.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public async Task GetUserById_OnSuccess_ReturnsRequestedUser()
        {
            _userRepositoryMock.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(UserTestData.GetUser());
            _configMock.Setup(x => x.Value).Returns(_databaseMessages);
            var service = new UserService(_userRepositoryMock.Object, _configMock.Object);

            var result = await service.GetUserAsync(1);

            result.Should().NotBeNull();
            result.Should().BeOfType<User>();
        }

        [Test]
        public async Task SearchUsers_OnSuccess_ReturnsUsersList()
        {
            _userRepositoryMock.Setup(s => s.SearchAsync("user", 1, 1))
                .ReturnsAsync(UserTestData.GetUsers());
            _configMock.Setup(x => x.Value).Returns(_databaseMessages);
            var service = new UserService(_userRepositoryMock.Object, _configMock.Object);

            var result = await service.SearchUsersAsync("user", 1, 1);

            result.Should().BeOfType<List<User>>();
            result.Should().NotContainNulls();
            result.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public async Task SearchAllUsers_OnSuccess_ReturnsUsersList()
        {
            _userRepositoryMock.Setup(s => s.GetAllAsync())
                .ReturnsAsync(UserTestData.GetUsers());
            _configMock.Setup(x => x.Value).Returns(_databaseMessages);
            var service = new UserService(_userRepositoryMock.Object, _configMock.Object);

            var result = await service.SearchUsersAsync("", 1, 1);

            result.Should().BeOfType<List<User>>();
            result.Should().NotContainNulls();
            result.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public async Task CreateUser_OnSuccess_ReturnsUserObject()
        {
            User user = UserTestData.CreateUser();
            _userRepositoryMock.Setup(s => s.InsertAsync(user))
                .ReturnsAsync(user);
            _configMock.Setup(x => x.Value).Returns(_databaseMessages);
            var service = new UserService(_userRepositoryMock.Object, _configMock.Object);

            var result = await service.InsertUserAsync(user);

            result.Should().NotBeNull();
            result.Should().BeOfType<User>();
            result.Id.Should().Be(1);
            result.FirstName.Should().StartWith("Test").And.EndWith("Name");
        }

        [Test]
        public async Task CreateUser_OnValidationError_ReturnsException()
        {
            User user = UserTestData.CreateUserNotValidated();
            _userRepositoryMock.Setup(s => s.InsertAsync(user))
                .ReturnsAsync(user);
            _configMock.Setup(x => x.Value).Returns(_databaseMessages);
            var service = new UserService(_userRepositoryMock.Object, _configMock.Object);

            Assert.ThrowsAsync<UserValidationException>(async () => await service.InsertUserAsync(user));
        }

        [Test]
        public async Task CreateUser_OnDatabaseError_ReturnsException()
        {
            User user = UserTestData.CreateUserErrorFromDatabase();
            _userRepositoryMock.Setup(s => s.InsertAsync(user))
                .ReturnsAsync(user);
            _configMock.Setup(x => x.Value).Returns(_databaseMessages);
            var service = new UserService(_userRepositoryMock.Object, _configMock.Object);

            Assert.ThrowsAsync<UserDatabaseException>(async () => await service.InsertUserAsync(user));
        }

        [Test]
        public async Task UpdateUser_OnSuccess_ReturnsUserObject()
        {
            User user = UserTestData.GenerateUpdatedUser();
            _userRepositoryMock.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(user);
            _userRepositoryMock.Setup(s => s.UpdateAsync(user))
                .ReturnsAsync(1);
            _configMock.Setup(x => x.Value).Returns(_databaseMessages);
            var service = new UserService(_userRepositoryMock.Object, _configMock.Object);

            var result = await service.UpdateUserAsync(user);

            result.Should().NotBeNull();
            result.Should().BeOfType<User>();
        }

        [Test]
        public async Task UpdateUser_OnUserNotFound_ReturnsNull()
        {
            User user = UserTestData.GenerateUpdatedUser();
            _userRepositoryMock.Setup(s => s.GetByIdAsync(2))
                .ReturnsAsync(It.IsAny<User>());
            _userRepositoryMock.Setup(s => s.UpdateAsync(user))
                .ReturnsAsync(1);
            _configMock.Setup(x => x.Value).Returns(_databaseMessages);
            var service = new UserService(_userRepositoryMock.Object, _configMock.Object);

            var result = await service.UpdateUserAsync(user);

            result.Should().BeNull();
        }

        [Test]
        public async Task UpdateUser_OnDatabaseError_ReturnsException()
        {
            User user = UserTestData.CreateUser();
            _userRepositoryMock.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(user);
            _userRepositoryMock.Setup(s => s.UpdateAsync(user))
                .ReturnsAsync(0);
            _configMock.Setup(x => x.Value).Returns(_databaseMessages);
            var service = new UserService(_userRepositoryMock.Object, _configMock.Object);

            Assert.ThrowsAsync<UserDatabaseException>(async () => await service.UpdateUserAsync(user));
        }

        [Test]
        public async Task DeleteUser_OnSuccess_ReturnsUserObject()
        {
            User user = UserTestData.GenerateDeletedUser();
            _userRepositoryMock.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(user);
            _userRepositoryMock.Setup(s => s.DeleteAsync(user))
                .ReturnsAsync(1);
            _configMock.Setup(x => x.Value).Returns(_databaseMessages);
            var service = new UserService(_userRepositoryMock.Object, _configMock.Object);

            var result = await service.DeleteUserAsync(1);

            result.Should().NotBeNull();
            result.Should().BeOfType<User>();
        }

        [Test]
        public async Task DeleteUser_OnUserNotFound_ReturnsNull()
        {
            User user = UserTestData.GenerateDeletedUser();
            _userRepositoryMock.Setup(s => s.GetByIdAsync(2))
                .ReturnsAsync(It.IsAny<User>());
            _userRepositoryMock.Setup(s => s.DeleteAsync(user))
                .ReturnsAsync(1);
            _configMock.Setup(x => x.Value).Returns(_databaseMessages);
            var service = new UserService(_userRepositoryMock.Object, _configMock.Object);

            var result = await service.DeleteUserAsync(1);

            result.Should().BeNull();
        }

        [Test]
        public async Task DeleteUser_OnDatabaseError_ReturnsException()
        {
            User user = UserTestData.CreateUser();
            _userRepositoryMock.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(user);
            _userRepositoryMock.Setup(s => s.DeleteAsync(user))
                .ReturnsAsync(0);
            _configMock.Setup(x => x.Value).Returns(_databaseMessages);
            var service = new UserService(_userRepositoryMock.Object, _configMock.Object);

            Assert.ThrowsAsync<UserDatabaseException>(async () => await service.DeleteUserAsync(1));
        }
    }
}
