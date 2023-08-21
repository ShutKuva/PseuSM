using System.Linq.Expressions;
using DALPost = DAL.Entities.Post;

namespace BLL.Parameters
{
    public class GetPostsParams
    {
        public Expression<Func<DALPost, bool>> Predicate { get; set; } = null!;
    }
}
