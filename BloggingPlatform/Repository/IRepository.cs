using BloggingPlatform.Models;
using BloggingPlatform.Repository.OEMs;

namespace BloggingPlatform.Repository
{
    public interface IRepository
    {
        Task<Post> CreatPost(Post post);
        Task<bool> DeletePost(int id);
        Task<Post> GetPostById(int id);
        Task<List<Post>> GetPosts();
        Task<Post>UpdatePost(int id);
        
    }
}
