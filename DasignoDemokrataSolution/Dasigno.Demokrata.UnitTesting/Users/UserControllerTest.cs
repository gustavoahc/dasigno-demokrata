using AutoMapper;
using Dasigno.Demokrata.Core.Application.Services.Users;
using Dasigno.Demokrata.Core.Domain.Entities;
using Dasigno.Demokrata.Presentation.WebApi.Controllers;
using Dasigno.Demokrata.Presentation.WebApi.Helpers.Mapping;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Dasigno.Demokrata.UnitTesting.Users
{
    internal class UserControllerTest
    {
        private Mock<IUserService> _userServiceMock;
        private readonly IMapper _mapper;

        public UserControllerTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingConfiguration());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
        }

        [Test]
        public async Task GetUsers_OnSuccess_ReturnsOkResponse()
        {
            _userServiceMock.Setup(s => s.GetUsersAsync())
                .ReturnsAsync(UserTestData.GetUsers());
            var controller = new UsersController(_userServiceMock.Object, _mapper);

            var result = (OkObjectResult)await controller.Get();

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task GetUserById_OnSuccess_ReturnsOkResponse()
        {
            _userServiceMock.Setup(s => s.GetUserAsync(1))
                .ReturnsAsync(UserTestData.GetUser());
            var controller = new UsersController(_userServiceMock.Object, _mapper);

            var result = (OkObjectResult)await controller.Get(1);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task SearchUsers_OnSuccess_ReturnsOkResponse()
        {
            _userServiceMock.Setup(s => s.SearchUsersAsync("user", 1, 1))
                .ReturnsAsync(UserTestData.GetUsers());
            var controller = new UsersController(_userServiceMock.Object, _mapper);

            var result = (OkObjectResult)await controller.Search("user", 1, 1);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task GetUserById_OnNotFound_ReturnsNotFoundResponse()
        {
            _userServiceMock.Setup(s => s.GetUserAsync(1))
                .ReturnsAsync(UserTestData.GetUser());
            var controller = new UsersController(_userServiceMock.Object, _mapper);

            var result = (NotFoundResult)await controller.Get(2);

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
            result.StatusCode.Should().Be(404);
        }

        [Test]
        public async Task CreateUser_OnSuccess_ReturnsCreatedAtRouteResponse()
        {
            User user = UserTestData.CreateUser();
            _userServiceMock.Setup(s => s.InsertUserAsync(It.IsAny<User>()))
                .ReturnsAsync(user);
            var controller = new UsersController(_userServiceMock.Object, _mapper);

            var result = (CreatedAtRouteResult)await controller.Post(UserTestData.CreateUserModel());

            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtRouteResult>();
            result.StatusCode.Should().Be(201);
        }

        [Test]
        public async Task UpdateUser_OnSuccess_ReturnsReturnsNoContentResponse()
        {
            _userServiceMock.Setup(s => s.UpdateUserAsync(It.IsAny<User>()))
                .ReturnsAsync(UserTestData.GenerateUpdatedUser());
            var controller = new UsersController(_userServiceMock.Object, _mapper);

            var result = (NoContentResult)await controller.Put(UserTestData.GenerateUserModelUpdated());

            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
            result.StatusCode.Should().Be(204);
        }

        [Test]
        public async Task UpdateUser_OnNotFound_ReturnsNotFoundResponse()
        {
            _userServiceMock.Setup(s => s.UpdateUserAsync(UserTestData.GenerateUpdatedUser()))
                .ReturnsAsync(UserTestData.CreateUser());
            var controller = new UsersController(_userServiceMock.Object, _mapper);

            var result = (NotFoundResult)await controller.Put(UserTestData.GenerateUserModelUpdated());

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
            result.StatusCode.Should().Be(404);
        }

        [Test]
        public async Task DeleteUser_OnSuccess_ReturnsNoContentResponse()
        {
            _userServiceMock.Setup(s => s.DeleteUserAsync(1))
                .ReturnsAsync(UserTestData.GenerateDeletedUser());
            var controller = new UsersController(_userServiceMock.Object, _mapper);

            var result = (NoContentResult)await controller.Delete(1);

            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
            result.StatusCode.Should().Be(204);
        }

        [Test]
        public async Task DeleteUser_OnNotFound_ReturnsNotFoundResponse()
        {
            _userServiceMock.Setup(s => s.DeleteUserAsync(1))
                .ReturnsAsync(UserTestData.GenerateDeletedUser());
            var controller = new UsersController(_userServiceMock.Object, _mapper);

            var result = (NotFoundResult)await controller.Delete(2);

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
            result.StatusCode.Should().Be(404);
        }
    }
}