namespace Persistence;

public class Book
{
    public int BookID { get; set; }
    public int ISBN { get; set; }
    public string? Title { get; set; }
    public Author? AuthorID { get; set; }
    public Publisher? PublisherID { get; set; }
    public int PublishYear { get; set; }
    public decimal Price { get; set; }
    public int Status { get; set; }
}