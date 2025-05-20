using BloggingPlatform.Models;
using BloggingPlatform.Repository.OEMs;
using Microsoft.Extensions.Primitives;

namespace BloggingPlatform.Repository
{
    public interface IRepository
    {
        Task<Post> CreatPost(Post post);
        Task<bool> DeletePost(int id);
        Task<Post> GetPostById(int id);
        Task<List<Post>> GetPosts(List<KeyValuePair<string,StringValues>> query);
        Task<Post>UpdatePost(int id, Post post);
        
    }
}
