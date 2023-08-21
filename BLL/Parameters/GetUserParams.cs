using System.Linq.Expressions;
using DALUser = DAL.Entities.User;

namespace BLL.Parameters
{
    public class GetUserParams
    {
        internal GetUserParams()
        {

        }

        public Expression<Func<DALUser, bool>> Predicate { get; set; } = null!;
    }
}
