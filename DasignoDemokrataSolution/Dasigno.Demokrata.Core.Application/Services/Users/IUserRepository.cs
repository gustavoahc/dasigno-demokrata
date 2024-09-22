using Dasigno.Demokrata.Core.Domain.Entities;

namespace Dasigno.Demokrata.Core.Application.Services.Users
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();

        Task<User> GetByIdAsync(int id);

        Task<User> InsertAsync(User user);

        Task<int> UpdateAsync(User user);

        Task<int> DeleteAsync(User user);

        Task<List<User>> SearchAsync(string text, int pageNumber, int pageSize);
    }
}
