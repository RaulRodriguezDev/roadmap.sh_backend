namespace BloggingPlatform.Repository.OEMs
{
    public class TagOem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<PostOem> Posts { get; set; } = new List<PostOem>();
    }
}
