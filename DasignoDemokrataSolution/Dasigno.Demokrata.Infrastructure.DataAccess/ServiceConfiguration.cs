using Dasigno.Demokrata.Core.Application.Services.Users;
using Dasigno.Demokrata.Infrastructure.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dasigno.Demokrata.Infrastructure.DataAccess
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DemokrataContext>(
                options => options.UseSqlServer(connectionString)
            );

            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
