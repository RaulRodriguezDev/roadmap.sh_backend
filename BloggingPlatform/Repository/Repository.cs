using BloggingPlatform.Models;
using BloggingPlatform.Repository.OEMs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

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

        public async Task<bool> DeletePost(int id)
        {
            var post = await _context.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return false;
            }

            else
            {
                post.Tags.Clear();
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return true;
            }
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

        public async Task<List<Post>> GetPosts(List<KeyValuePair<string, StringValues>> query)
        {
            var posts = await _context.Posts
                    .Include(p => p.Tags)
                    .Select(posts => new Post
                    {
                        Category = posts.Category,
                        Content = posts.Content,
                        CreatedAt = posts.CreatedAt,
                        Id = posts.Id,
                        Tags = posts.Tags.Select(t => t.Name).ToArray(),
                        Title = posts.Title,
                        UpdatedAt = posts.UpdatedAt
                    }).ToListAsync();

            if (query.Count == 0)
            {
                return posts;
            }

            else
            {
                var postsFiltered = posts.Select(post =>
                {
                    var postFiltered = post;
                    foreach (var queryParam in query)
                    {
                        if (queryParam.Key == "category" && post.Category.ToLower() != queryParam.Value.ToString().ToLower())
                        {
                            postFiltered = null;
                            break;
                        }
                        else if (queryParam.Key == "term")
                        {
                            var tag = post.Tags.FirstOrDefault(t => t == queryParam.Value.ToString());
                            if (tag == null)
                            {
                                postFiltered = null;
                                break;
                            }
                        }
                    }
                    return postFiltered;
                }).Where(p => p != null).ToList();

                return postsFiltered;
            }
        }

        public async Task<Post> UpdatePost(int id, Post post)
        {
            var postOem = await _context.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);

            if (postOem == null)
            {
                return null;
            }

            else
            {
                postOem.Title = post.Title;
                postOem.Content = post.Content;
                postOem.Category = post.Category;
                postOem.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

                if (post.Tags != null)
                {
                    foreach (var tag in post.Tags)
                    {
                        var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tag);
                        if (existingTag == null)
                        {
                            var newTag = new TagOem { Name = tag };
                            _context.Tags.Add(newTag);
                            postOem.Tags.Add(newTag);
                        }
                        else
                        {
                            postOem.Tags.Add(existingTag);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                await _context.SaveChangesAsync();
                return new Post
                {
                    Category = postOem.Category,
                    Content = postOem.Content,
                    CreatedAt = postOem.CreatedAt,
                    Id = postOem.Id,
                    Tags = postOem.Tags.Select(t => t.Name).ToArray(),
                    Title = postOem.Title,
                    UpdatedAt = postOem.UpdatedAt
                };
            }
        }
    }
}
