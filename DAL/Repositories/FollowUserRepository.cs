using DAL.Entities;
using DAL.Repositories.BaseRepositories;

namespace DAL.Repositories
{
    public class FollowUserRepository : BaseRepository<FollowUser>
    {
        public FollowUserRepository(MainContext context) : base(context)
        {
        }
    }
}
