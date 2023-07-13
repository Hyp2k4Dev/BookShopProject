namespace Persistence;

public class Book
{
    public int BookID { get; set; }
    public required string Title { get; set; }
    public Auhtor? Author { get; set; }
    public decimal Price { get; set; }

}