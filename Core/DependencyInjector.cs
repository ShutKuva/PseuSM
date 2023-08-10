using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DependencyInjector
    {
        public static void Inject(IServiceCollection serviceCollection, IConfiguration configuration, bool isDevelopment)
        {
            serviceCollection.Configure<JwtProperties>(properties =>
            {
                properties.Issuer = isDevelopment ? configuration["JwtOptions:Issuer"]! : configuration["JWT_ISSUER"]!;
                properties.Audience = isDevelopment ? configuration["JwtOptions:Audience"]! : configuration["JWT_AUDIENCE"]!;
                properties.SecretKey = isDevelopment ? configuration["JwtOptions:SecretKey"]! : configuration["JWT_SEWCRET_KEY"]!;
            });
        }
    }
}
