using Dasigno.Demokrata.Core.Domain.Entities;

namespace Dasigno.Demokrata.Core.Application.Services.Users
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();

        Task<User> InsertUserAsync(User user);
    }
}
