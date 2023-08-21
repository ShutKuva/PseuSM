using DAL.Entities;
using DAL.Repositories.BaseRepositories;

namespace DAL.Repositories
{
    public class VideoRepository : BaseRepository<Video>
    {
        public VideoRepository(MainContext context) : base(context)
        {
        }
    }
}
