using Dasigno.Demokrata.Core.Domain.Entities;
using Dasigno.Demokrata.Presentation.WebApi.ApiModels.Requests;

namespace Dasigno.Demokrata.UnitTesting.Users
{
    internal static class UserTestData
    {
        public static User GetUser()
        {
            return new User
            {
                Id = 1,
                FirstName = "Test User Name",
                LastName = "Test User LastName",
                BirthDate = new DateOnly(1990, 12, 22),
                Salary = 1000000,
                CreationDate = DateTime.Now
            };
        }

        public static List<User> GetUsers()
        {
            return new List<User> { GetUser() };
        }

        public static User CreateUser()
        {
            return new User
            {
                Id = 1,
                FirstName = "Test User Name",
                MiddleName = "Test User MiddleName",
                LastName = "Test User LastName",
                SurName = "Test User SurName",
                BirthDate = new DateOnly(1990, 12, 22),
                Salary = 1000000,
                CreationDate = DateTime.Now
            };
        }

        public static User CreateUserNotValidated()
        {
            return new User
            {
                Id = 1,
                FirstName = "Test User Name 1",
                MiddleName = "Test User MiddleName 1",
                LastName = "Test User LastName 1",
                SurName = "Test User SurName 1",
                BirthDate = new DateOnly(1990, 12, 22),
                Salary = 1000000,
                CreationDate = DateTime.Now
            };
        }

        public static User CreateUserErrorFromDatabase()
        {
            return new User
            {
                Id = 0,
                FirstName = "Test User Name",
                MiddleName = "Test User MiddleName",
                LastName = "Test User LastName",
                SurName = "Test User SurName",
                BirthDate = new DateOnly(1990, 12, 22),
                Salary = 1000000,
                CreationDate = DateTime.Now
            };
        }

        public static UserCreationRequestModel CreateUserModel()
        {
            return new UserCreationRequestModel("Test User Name", "", "Test User LastName", "", new DateOnly(1990, 12, 22), 1000000);
        }

        public static User GenerateUpdatedUser()
        {
            return new User
            {
                Id = 1,
                FirstName = "Test User Name Updated",
                LastName = "Test User LastName Updated",
                BirthDate = new DateOnly(1990, 12, 22),
                Salary = 1000000,
                CreationDate = DateTime.Now
            };
        }

        public static User GenerateDeletedUser()
        {
            return new User
            {
                Id = 1,
                FirstName = "Test User Name",
                LastName = "Test User LastName",
                BirthDate = new DateOnly(1990, 12, 22),
                Salary = 1000000
            };
        }

        public static UserUpdateRequestModel GenerateUserModelUpdated()
        {
            return new UserUpdateRequestModel(1, "Test User Name Updated", "", "Test User LastName Updated", "", new DateOnly(1990, 12, 22), 1000000);
        }
    }
}
