namespace Persistence;

public class Book
{
    public int BookID { get; set; }
    public required string Title { get; set; }

    public Author? AuthorName { get; set; }
    public Publisher? PublisherName { get; set; }
    public int PublishingYear { get; set; }

    public Author? Author { get; set; }

    public decimal Price { get; set; }
    public int Status { get; set; }

}