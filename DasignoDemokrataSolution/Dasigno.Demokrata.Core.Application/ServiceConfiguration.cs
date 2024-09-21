using Dasigno.Demokrata.Core.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Dasigno.Demokrata.Core.Application
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
