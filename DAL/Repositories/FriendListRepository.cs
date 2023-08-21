using DAL.Entities;
using DAL.Repositories.BaseRepositories;

namespace DAL.Repositories
{
    public class FriendListRepository : BaseRepository<FriendList>
    {
        public FriendListRepository(MainContext context) : base(context)
        {
        }
    }
}
