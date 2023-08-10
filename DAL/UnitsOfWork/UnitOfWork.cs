using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using DAL.Entities;
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
            return Task.FromResult(typeof(IRepository<TEntity, TId, TPredicate>) switch
            {
                Type type when type == typeof(IRepository<User, int, Expression<Func<User, bool>>>) =>
                    (IRepository<TEntity, TId, TPredicate>)TryToRetrieveSavedRepositoryOrSaveNew<IRepository<User, int, Expression<Func<User, bool>>>, UserRepository>()!,
                _ => null
            });
        }

        private TGenericRepository? TryToRetrieveSavedRepositoryOrSaveNew<TGenericRepository, TImplementedRepository>() 
            where TGenericRepository : class
            where TImplementedRepository : TGenericRepository
        {
            if (_createdRepositories.ContainsKey(typeof(TGenericRepository)))
            {
                return (TGenericRepository)_createdRepositories[typeof(TGenericRepository)];
            }

            ConstructorInfo? constructorInfo = typeof(TImplementedRepository).GetConstructor(new [] {typeof(MainContext)});

            if (constructorInfo != null)
            {
                object instanceOfRepositoryImplementation = constructorInfo.Invoke(new[] {_context});
                _createdRepositories.Add(typeof(TGenericRepository), instanceOfRepositoryImplementation);

                return (TImplementedRepository)instanceOfRepositoryImplementation;
            }

            return null;
        }
    }
}
