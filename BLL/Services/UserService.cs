using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.Services.BaseServices;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using System.Linq.Expressions;
using DALUser = DAL.Entities.User;

namespace BLL.Services
{
    public class UserService : ServiceBase<DALUser>, IUserService
    {
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<User> GetUserByCredentialsAsync(string login, string password)
        {
            IEnumerable<DALUser> usersWithSameCredentials = await GetUsersThatFitPredicate(user => user.Login == login && user.Password == password);

            if (!usersWithSameCredentials.Any())
            {
                throw new ArgumentException("There is no user with this credentials");
            }

            return _mapper.Map<DALUser, User>(usersWithSameCredentials.First());
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            IEnumerable<DALUser> usersWithSameID = await GetUsersThatFitPredicate(user => user.Id == id);

            if (!usersWithSameID.Any())
            {
                throw new ArgumentException("There is no user with this id");
            }

            return _mapper.Map<DALUser, User>(usersWithSameID.First());
        }

        public async Task CreateUserAsync(User user)
        {
            IRepository<DALUser, int, Expression<Func<DALUser, bool>>>? userRepository = await GetRepositoryAsync();

            await userRepository.CreateEntityAsync(_mapper.Map<User, DALUser>(user));

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            IRepository<DALUser, int, Expression<Func<DALUser, bool>>>? userRepository = await GetRepositoryAsync();

            await userRepository.UpdateEntityAsync(_mapper.Map<User, DALUser>(user));

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            IRepository<DALUser, int, Expression<Func<DALUser, bool>>>? userRepository = await GetRepositoryAsync();

            await userRepository.DeleteEntityAsync(user.Id);

            await _unitOfWork.CommitAsync();
        }

        private async Task<IEnumerable<DALUser>> GetUsersThatFitPredicate(Expression<Func<DALUser, bool>> predicate)
        {
            IRepository<DALUser, int, Expression<Func<DALUser, bool>>>? userRepository = await GetRepositoryAsync();

            return await userRepository.GetEntitiesByPredicateAsync(predicate);
        }
    }
}
