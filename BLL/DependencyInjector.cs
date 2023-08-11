using BLL.Abstractions.Services;
using BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class DependencyInjector
    {
        public static void Inject(IServiceCollection serviceCollection, IConfiguration configurations, bool isDevelopment)
        {
            serviceCollection.AddScoped<IJwtService, JwtService>();
            serviceCollection.AddScoped<IUserService, UserService>();

            serviceCollection.AddAutoMapper(typeof(DependencyInjector));

            DAL.DependencyInjector.Inject(serviceCollection, configurations, isDevelopment);
        }
    }
}
