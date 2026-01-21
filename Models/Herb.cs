namespace Herb_Track_Bulgaria_Server.Models
{
    public class Herb
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? LatinName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}