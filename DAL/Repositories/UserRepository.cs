using DAL.Abstractions.Repository;
using DAL.Entities;
using DAL.Repositories.BaseRepositories;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class UserRepository : 
        BaseRepository<User>
    {
        public UserRepository(MainContext context) : base(context)
        {

        }
    }
}
