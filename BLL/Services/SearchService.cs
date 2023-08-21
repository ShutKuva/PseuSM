using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.MappedAbstractions;
using System.Linq.Expressions;
using DALPost = DAL.Entities.Post;
using DALUser = DAL.Entities.User;

namespace BLL.Services
{
    public class SearchService : ISearchService
    {
        private readonly IProGetterService<User, DALUser> _userService;
        private readonly IProGetterService<Post, DALPost> _postService;

        public SearchService(
            IProGetterService<User, DALUser> userService,
            IProGetterService<Post, DALPost> postService
            )
        {
            _userService = userService;
            _postService = postService;
        }

        public async Task<SearchResult> SearchAsync(string searchString)
        {
            var result = new SearchResult();

            foreach (string subString in searchString.Split(" "))
            {
                if (int.TryParse(subString, out int id))
                {
                    await SearchUsersByPredicateAsync(user => user.Id == id, result);
                    await SearchPostsByPredicateAsync(post => post.Id == id, result);
                } 
                else
                {
                    await SearchUsersByPredicateAsync(user => user.Login!.Contains(subString) || user.Description!.Contains(subString) || user.Name.Contains(subString), result);
                }
            }

            return result;
        }

        private async Task SearchUsersByPredicateAsync(Expression<Func<DALUser, bool>> predicate, SearchResult result)
        {
            IEnumerable<User> usersThatFitPredicate = await _userService.GetEntitiesByPredicateAsync(predicate);

            foreach (User user in usersThatFitPredicate)
            {
                result.Users.Add(user);
            }
        }

        private async Task SearchPostsByPredicateAsync(Expression<Func<DALPost, bool>> predicate, SearchResult result)
        {
            IEnumerable<Post> postsThatFitPredicate = await _postService.GetEntitiesByPredicateAsync(predicate);

            foreach (Post post in postsThatFitPredicate)
            {
                result.Posts.Add(post);
            }
        }
    }
}
