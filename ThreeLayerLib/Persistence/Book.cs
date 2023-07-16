namespace Persistence;

public class Book
{
    public int BookID { get; set; }
    public int ISBN { get; set; }
    public string? Title { get; set; }
    public Author? BookAuthor { get; set; }
    public Publisher? BookPublisher { get; set; }
    public int PublishYear { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int Status { get; set; }
}