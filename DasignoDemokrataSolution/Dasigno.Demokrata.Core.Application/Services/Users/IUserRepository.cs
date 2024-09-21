using Dasigno.Demokrata.Core.Domain.Entities;

namespace Dasigno.Demokrata.Core.Application.Services.Users
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();

        Task<User> GetByIdAsync(int id);

        Task<User> InsertAsync(User user);
    }
}
