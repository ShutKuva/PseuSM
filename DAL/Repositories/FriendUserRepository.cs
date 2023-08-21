using DAL.Entities;
using DAL.Repositories.BaseRepositories;

namespace DAL.Repositories
{
    public class FriendUserRepository : BaseRepository<FriendUser>
    {
        public FriendUserRepository(MainContext context) : base(context)
        {
        }
    }
}
