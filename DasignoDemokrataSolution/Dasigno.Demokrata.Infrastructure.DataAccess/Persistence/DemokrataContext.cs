using Dasigno.Demokrata.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dasigno.Demokrata.Infrastructure.DataAccess.Persistence
{
    public class DemokrataContext : DbContext
    {
        public DemokrataContext(DbContextOptions<DemokrataContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}