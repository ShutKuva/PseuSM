using DAL.Entities;
using DAL.Repositories.BaseRepositories;

namespace DAL.Repositories
{
    public class PostRepository : BaseRepository<Post>
    {
        public PostRepository(MainContext context) : base(context)
        {
        }
    }
}
