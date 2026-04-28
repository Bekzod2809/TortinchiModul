namespace dars8.Entities;

public class Post
{
    public long PostId { get; set; }
    public long ChatId { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpadateAt { get; set; } = DateTime.UtcNow;
}
