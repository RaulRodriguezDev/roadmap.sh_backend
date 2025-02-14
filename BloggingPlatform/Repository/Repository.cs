using BloggingPlatform.Models;
using BloggingPlatform.Repository.OEMs;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Repository
{
    public class Repository(BlogginPlatformDbContext context) : IRepository
    {
        private readonly BlogginPlatformDbContext _context = context;

        public async Task<Post> CreatPost(Post post)
        {
            var newPostOem = new PostOem
            {
                Title = post.Title,
                Content = post.Content,
                Category = post.Category,
                CreatedAt = post.CreatedAt ?? DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = post.UpdatedAt ?? DateOnly.FromDateTime(DateTime.Now)
            };

            if (post.Tags != null)
            {
                foreach (var tag in post.Tags)
                {
                    var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tag);

                    if (existingTag == null)
                    {
                        var newTag = new TagOem { Name = tag };
                        _context.Tags.Add(newTag);
                        newPostOem.Tags.Add(newTag);
                    }
                    else
                    {
                        newPostOem.Tags.Add(existingTag);
                    }

                    await _context.SaveChangesAsync();
                }
            }

            var postOemCreated = _context.Posts.Add(newPostOem);
            await _context.SaveChangesAsync();

            post.Id = postOemCreated.Entity.Id;
            post.CreatedAt = postOemCreated.Entity.CreatedAt;
            post.UpdatedAt = postOemCreated.Entity.UpdatedAt;

            return post;

        }

        public Task<Post> DeletePost(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Post> GetPostById(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);

            return new Post 
            { 
                Category = post.Category, 
                Content = post.Content, 
                CreatedAt = post.CreatedAt, 
                Id = post.Id, 
                Tags = post.Tags.Select(t => t.Name).ToArray(), 
                Title = post.Title, 
                UpdatedAt = post.UpdatedAt 
            };
        }

        public Task<List<Post>> GetPosts()
        {
            throw new NotImplementedException();
        }

        public Task<Post> UpdatePost(int id)
        {
            throw new NotImplementedException();
        }
    }
}
