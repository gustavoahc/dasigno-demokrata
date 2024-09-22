using Dasigno.Demokrata.Core.Application.Parameters.Messages;
using Dasigno.Demokrata.Core.Domain.Entities;
using Dasigno.Demokrata.Core.Domain.Exceptions.UserExceptions;
using Microsoft.Extensions.Options;

namespace Dasigno.Demokrata.Core.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly DatabaseMessages _messages;

        public UserService(IUserRepository userRepository, IOptions<DatabaseMessages> messages)
        {
            _userRepository = userRepository;
            _messages = messages.Value;
        }

        public async Task<List<User>> GetUsersAsync() => await _userRepository.GetAllAsync();

        public async Task<User> GetUserAsync(int id) => await GetUser(id);

        public async Task<User> InsertUserAsync(User user)
        {
            await ValidateUserAsync(user);
            user.CreationDate = DateTime.Now;
            User insertUser = await _userRepository.InsertAsync(user);
            if (insertUser is null || insertUser.Id <= 0)
            {
                throw new UserDatabaseException(_messages.InsertingErrorMessage);
            }
            return insertUser;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            User currentUser = await GetUser(user.Id);
            if (currentUser is null)
            {
                return currentUser;
            }

            await ValidateUserAsync(user);
            user.CreationDate = currentUser.CreationDate;
            user.ModificationDate = DateTime.Now;
            int updateResult = await _userRepository.UpdateAsync(user);
            if (updateResult > 0)
            {
                return user;
            }
            else
            {
                throw new UserDatabaseException(_messages.UpdatingErrorMessage);
            }
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            User currentUser = await GetUser(id);
            if (currentUser is null)
            {
                return currentUser;
            }

            int deleteResult = await _userRepository.DeleteAsync(currentUser);
            if (deleteResult > 0)
            {
                return currentUser;
            }
            else
            {
                throw new UserDatabaseException(_messages.DeletingErrorMessage);
            }
        }

        public async Task<List<User>> SearchUsersAsync(string text, int pageNumber, int pageSize)
        {
            if (String.IsNullOrEmpty(text))
            {
                return await GetUsersAsync();
            }

            return await _userRepository.SearchAsync(text, pageNumber, pageSize);
        }

        private async Task<User> GetUser(int id) => await _userRepository.GetByIdAsync(id);

        private static async Task ValidateUserAsync(User user)
        {
            UserValidator userValidation = new UserValidator();
            var validationResult = await userValidation.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                throw new UserValidationException(validationResult.Errors);
            }
        }
    }
}
