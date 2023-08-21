using DAL.Entities;
using DAL.Repositories.BaseRepositories;

namespace DAL.Repositories
{
    public class FollowListRepository : BaseRepository<FollowList>
    {
        public FollowListRepository(MainContext context) : base(context)
        {
        }
    }
}
