using BloggingPlatform.Models;
using BloggingPlatform.Repository.OEMs;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Repository
{
    public class BlogginPlatformDbContext : DbContext
    {
        public BlogginPlatformDbContext(DbContextOptions<BlogginPlatformDbContext> options) : base(options)
        {
        }
        public DbSet<PostOem> Posts { get; set; }
        public DbSet<TagOem> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostOem>()
                .HasMany(t => t.Tags)
                .WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>
                (
                    "PostTag",
                    j => j
                        .HasOne<TagOem>()
                        .WithMany()
                        .HasForeignKey("TagId"),
                    j => j
                        .HasOne<PostOem>()
                        .WithMany()
                        .HasForeignKey("PostId"),
                    j =>
                    {
                        j.HasKey("PostId", "TagId");
                        j.ToTable("PostTag");
                    }
                );
        }
    }
}
