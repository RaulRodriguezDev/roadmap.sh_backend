namespace BloggingPlatform.Repository.OEMs
{
    public class PostOem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Category { get; set; }
        public ICollection<TagOem> Tags { get; set; } = new List<TagOem>();
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
    }
}
