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

            serviceCollection.Configure<CloudinarySettings>(settings =>
            {
                settings.CloudName = isDevelopment ? configuration["CloudinarySettings:CloudName"]! : configuration["CLOUDINARY_CLOUD_NAME"]!;
                settings.ApiKey = isDevelopment ? configuration["CloudinarySettings:ApiKey"]! : configuration["CLOUDINARY_API_KEY"]!;
                settings.ApiSecret = isDevelopment ? configuration["CloudinarySettings:ApiSecret"]! : configuration["CLOUDINARY_API_SECRET"]!;
            });
        }
    }
}
