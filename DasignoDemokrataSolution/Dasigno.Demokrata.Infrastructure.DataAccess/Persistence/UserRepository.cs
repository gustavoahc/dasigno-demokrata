using Dasigno.Demokrata.Core.Application.Services.Users;
using Dasigno.Demokrata.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dasigno.Demokrata.Infrastructure.DataAccess.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly DemokrataContext _context;

        public UserRepository(DemokrataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync() => await _context.Users.ToListAsync();

        public async Task<User> GetByIdAsync(int id) => await _context.Users.FindAsync(id);

        public async Task<User> InsertAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<int> UpdateAsync(User user)
        {
            _context.ChangeTracker.Clear();
            _context.Entry(user).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(User user)
        {
            _context.Remove(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<User>> SearchAsync(string text, int pageNumber, int pageSize)
        {
            IQueryable<User> queryUsers = _context.Users.Where(n => n.FirstName.Contains(text)
            || n.LastName.Contains(text));

            return await queryUsers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
