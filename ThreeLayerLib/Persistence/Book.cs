namespace Persistence;

public class Book
{
    internal int AuthorID;

    public int BookID { get; set; }
    public int ISBN { get; set; }
    public string? Title { get; set; }
    public Author? BookAuthor { get; set; }
    public Publisher? BookPublisher { get; set; }
    public int PublishYear { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int Status { get; set; }

    public void SearchBookName(string? bookName)
    {
        Console.WriteLine("You have searched for book name: {0}", bookName);
        Console.ReadLine();
    }

    public bool SearchForBookByID(int bookId)
    {
        Console.WriteLine("Book ID: {0} not found.", bookId);
        return true;
    }
}