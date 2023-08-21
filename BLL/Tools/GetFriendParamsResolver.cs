using BLL.Abstractions.Tools;
using BLL.Entities;
using BLL.Parameters;
using Core.Enums;
using System.Linq.Expressions;
using DALUser = DAL.Entities.User;

namespace BLL.Tools
{
    public class GetFriendParamsResolver : IParamsResolver<GetUserParams, GetFriendParamsResolverParams>
    {
        public Task<GetUserParams> CreateParamsAsync(GetFriendParamsResolverParams parameters)
        {
            Expression<Func<DALUser, bool>> baseExpression = user => user.FriendList.FriendUsers.Any(friendUser => friendUser.UserId == parameters.UserId);

            ParameterExpression userParameter = baseExpression.Parameters.First();

            Expression left = baseExpression.Body;
            Expression right;

            switch (parameters.Filter)
            {
                case FriendsFilters.Offline:
                    right = Expression.Equal(Expression.Property(userParameter, nameof(User.Status)), Expression.Constant(UserStatuses.Offline));
                    break;
                case FriendsFilters.Online:
                    right = Expression.Equal(Expression.Property(userParameter, nameof(User.Status)), Expression.Constant(UserStatuses.Online));
                    break;
                default:
                    return Task.FromResult(new GetUserParams() { Predicate = baseExpression });
            }

            return Task.FromResult(new GetUserParams
            {
                Predicate = Expression.Lambda<Func<DALUser, bool>>(Expression.AndAlso(left, right), userParameter)
            });
        }
    }
}
