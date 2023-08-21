using DAL.Entities.Cloudinary;
using DAL.Repositories.BaseRepositories;

namespace DAL.Repositories
{
    public class CloudinaryImageRepository : BaseRepository<CloudinaryEntity>
    {
        public CloudinaryImageRepository(MainContext context) : base(context)
        {
        }
    }
}
