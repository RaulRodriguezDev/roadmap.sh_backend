using BloggingPlatform.Models;

namespace BloggingPlatform.Repository
{
    public interface IRepository
    {
        Task<Post> CreatPost(Post post);
        Task<List<Post>> GetPosts();
        Task<Post> GetPostById(int id);
        Task<Post>UpdatePost(int id);
        Task<Post> DeletePost(int id);
    }
}
