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

        public async Task<User> GetUserAsync(int id) => await _userRepository.GetByIdAsync(id);

        public async Task<User> InsertUserAsync(User user)
        {
            user.CreationDate = DateTime.Now;
            User insertUser = await _userRepository.InsertAsync(user);
            return insertUser;
        }
    }
}
