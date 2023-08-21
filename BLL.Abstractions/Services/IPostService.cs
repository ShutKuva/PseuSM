using BLL.Entities;

namespace BLL.Abstractions.Services
{
    public interface IPostService<TParams> : ISequenceGetterService<Post, TParams>
    {
        Task<Post> CreatePost(Post newPost);
        Task<Post> CreatePost(string description, User user);
        Task DeletePost(Post deletePost);
    }
}
