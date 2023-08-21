using BLL.Abstractions.Services;
using BLL.Abstractions.Services.DalServices;
using BLL.Abstractions.Services.Externals;
using BLL.Abstractions.Tools;
using BLL.Entities;
using BLL.MappedAbstractions;
using BLL.Parameters;
using BLL.Services;
using BLL.Services.DalServices;
using BLL.Services.Externals;
using BLL.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DALUser = DAL.Entities.User;
using DALPost = DAL.Entities.Post;

namespace BLL
{
    public static class DependencyInjector
    {
        public static void Inject(IServiceCollection serviceCollection, IConfiguration configurations, bool isDevelopment)
        {
            serviceCollection.AddScoped<IJwtService, JwtService>();
            serviceCollection.AddScoped<IUserService<GetUserParams>, UserService>();
            serviceCollection.AddScoped<IImageService, ImageService>();
            serviceCollection.AddScoped(typeof(IDalEntityService<>), typeof(DalEntityService<>));
            serviceCollection.AddScoped(typeof(ICloudinaryMetadataService<,>), typeof(CloudinaryEntityService<,>));
            serviceCollection.AddScoped<IImagePlaceholderService, ImagePlaceholderService>();
            serviceCollection.AddScoped<ICloudinaryService, CloudinaryService>();
            serviceCollection.AddScoped<IFollowService, FollowService>();
            serviceCollection.AddScoped<IFriendService, FriendService>();
            serviceCollection.AddScoped<IFollowListService, FollowListService>();
            serviceCollection.AddScoped<IFriendListService, FriendListService>();
            serviceCollection.AddScoped<IParamsResolver<GetUserParams, GetFriendParamsResolverParams>, GetFriendParamsResolver>();
            serviceCollection.AddScoped<IParamsResolver<string, CloudinaryParameters>, CloudinaryNameResolver>();
            serviceCollection.AddScoped<IProGetterService<User, DALUser>, UserService>();
            serviceCollection.AddScoped<IProGetterService<Post, DALPost>, PostService>();
            serviceCollection.AddScoped<ISearchService, SearchService>();

            serviceCollection.AddAutoMapper(typeof(DependencyInjector));

            DAL.DependencyInjector.Inject(serviceCollection, configurations, isDevelopment);
        }
    }
}
