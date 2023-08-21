using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.MappedAbstractions;
using BLL.Parameters;
using BLL.Services.BaseServices;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using System.Linq.Expressions;
using DALUser = DAL.Entities.User;

namespace BLL.Services
{
    public class UserService : ServiceBase<User, DALUser>, IUserService<GetUserParams>, IProGetterService<User, DALUser>
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<User> GetUserByCredentialsAsync(string login, string password)
        {
            IEnumerable<DALUser> usersWithSameCredentials = await GetUsersThatFitPredicate(user => user.Login == login && user.Password == password);

            if (!usersWithSameCredentials.Any())
            {
                throw new ArgumentException("There is no user with this credentials");
            }

            return _mapper.Map<User>(usersWithSameCredentials.First());
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            IEnumerable<DALUser> usersWithSameID = await GetUsersThatFitPredicate(user => user.Id == id);

            if (!usersWithSameID.Any())
            {
                throw new ArgumentException("There is no user with this id");
            }

            return _mapper.Map<User>(usersWithSameID.First());
        }

        public async Task<IEnumerable<User>> GetEntitiesByPredicateAsync(GetUserParams parameters)
        {
            return _mapper.Map<IEnumerable<User>>(await GetUsersThatFitPredicate(parameters.Predicate));
        }

        public async Task<IEnumerable<User>> GetEntitiesByPredicateAsync(Expression<Func<DALUser, bool>> predicate)
        {
            return _mapper.Map<IEnumerable<User>>(await GetUsersThatFitPredicate(predicate));
        }

        public Task<User> CreateUserAsync(User user)
        {
            DALUser dalUser = _mapper.Map<DALUser>(user);

            return CreateEntityAsync(dalUser) ;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            DALUser dalUser = _mapper.Map<DALUser>(user);

            await UpdateDalEntityAsync(dalUser);

            return _mapper.Map<User>(dalUser);
        }

        public Task DeleteUserAsync(User user)
        {
            return DeleteEntityAsync(user.Id);
        }

        private async Task<IEnumerable<DALUser>> GetUsersThatFitPredicate(Expression<Func<DALUser, bool>> predicate)
        {
            IRepository<DALUser, int, Expression<Func<DALUser, bool>>>? userRepository = await GetRepositoryAsync();

            return await userRepository.GetEntitiesByPredicateAsync(predicate);
        }
    }
}
