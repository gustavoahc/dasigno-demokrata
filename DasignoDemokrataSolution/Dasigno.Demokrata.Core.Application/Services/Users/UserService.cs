using Dasigno.Demokrata.Core.Domain.Entities;

namespace Dasigno.Demokrata.Core.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsersAsync() => await _userRepository.GetAllAsync();

        public async Task<User> GetUserAsync(int id) => await GetUser(id);

        public async Task<User> InsertUserAsync(User user)
        {
            user.CreationDate = DateTime.Now;
            User insertUser = await _userRepository.InsertAsync(user);
            return insertUser;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            User currentUser = await GetUser(user.Id);
            if (currentUser is null)
            {
                return currentUser;
            }
            user.CreationDate = currentUser.CreationDate;
            user.ModificationDate = DateTime.Now;
            int updateResult = await _userRepository.UpdateAsync(user);
            if (updateResult > 0)
            {
                return user;
            }
            else
            {
                return null;
                //throw exception de que no se pudo actualizar el usuario
            }
        }

        private async Task<User> GetUser(int id) => await _userRepository.GetByIdAsync(id);
    }
}
