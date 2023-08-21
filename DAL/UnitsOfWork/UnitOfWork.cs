using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using DAL.Entities;
using DAL.Entities.Cloudinary;
using DAL.Repositories;
using System.Linq.Expressions;
using System.Reflection;

namespace DAL.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainContext _context;
        private readonly Dictionary<Type, object> _createdRepositories = new();

        public UnitOfWork(MainContext context)
        {
            _context = context;
        }

        public Task CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<IRepository<TEntity, TId, TPredicate>?> GetRepositoryAsync<TEntity, TId, TPredicate>()
            where TEntity : class
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            Type? repositoryType = currentAssembly.GetTypes().FirstOrDefault(type => type.IsAssignableTo(typeof(IRepository<TEntity, TId, TPredicate>)));

            if (repositoryType is not null)
            {
                return Task.FromResult(TryToRetrieveSavedRepositoryOrSaveNew<IRepository<TEntity, TId, TPredicate>>(repositoryType));
            }

            return Task.FromResult((IRepository<TEntity, TId, TPredicate>?)null);
        }

        private TGenericRepository? TryToRetrieveSavedRepositoryOrSaveNew<TGenericRepository>(Type implementedRepository) 
            where TGenericRepository : class
        {
            if (_createdRepositories.ContainsKey(typeof(TGenericRepository)))
            {
                return (TGenericRepository)_createdRepositories[typeof(TGenericRepository)];
            }

            ConstructorInfo? constructorInfo = implementedRepository.GetConstructor(new [] {typeof(MainContext)});

            if (constructorInfo != null)
            {
                object instanceOfRepositoryImplementation = constructorInfo.Invoke(new[] {_context});
                _createdRepositories.Add(typeof(TGenericRepository), instanceOfRepositoryImplementation);

                return (TGenericRepository)instanceOfRepositoryImplementation;
            }

            return null;
        }
    }
}

//return Task.FromResult(typeof(IRepository<TEntity, TId, TPredicate>) switch
//{
//    Type type when type == typeof(IRepository<User, int, Expression<Func<User, bool>>>) =>
//        (IRepository<TEntity, TId, TPredicate>)TryToRetrieveSavedRepositoryOrSaveNew<IRepository<User, int, Expression<Func<User, bool>>>, UserRepository>()!,
//    Type type when type == typeof(IRepository<Image, int, Expression<Func<Image, bool>>>) =>
//        (IRepository<TEntity, TId, TPredicate>)TryToRetrieveSavedRepositoryOrSaveNew<IRepository<Image, int, Expression<Func<Image, bool>>>, ImageRepository>()!,
//    Type type when type == typeof(IRepository<ImagePlaceholder, int, Expression<Func<ImagePlaceholder, bool>>>) =>
//        (IRepository<TEntity, TId, TPredicate>)TryToRetrieveSavedRepositoryOrSaveNew<IRepository<ImagePlaceholder, int, Expression<Func<ImagePlaceholder, bool>>>, ImagePlaceholderRepository>()!,
//    Type type when type == typeof(IRepository<CloudinaryEntity, int, Expression<Func<CloudinaryEntity, bool>>>) =>
//        (IRepository<TEntity, TId, TPredicate>)TryToRetrieveSavedRepositoryOrSaveNew<IRepository<CloudinaryEntity, int, Expression<Func<CloudinaryEntity, bool>>>, CloudinaryImageRepository>()!,
//    _ => null
//});