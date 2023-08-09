using DAL.Abstractions.Repository;
using DAL.Entities;
using DAL.Repositories.BaseRepositories;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User, int, Expression<Func<User, int, Expression<Func<User, bool>>>>>
    {
        public UserRepository(MainContext context) : base(context)
        {

        }
    }
}
