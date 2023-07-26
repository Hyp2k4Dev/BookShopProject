namespace Persistence
{

public class Book
{
    public int BookID { get; set; }
    public int ISBN { get; set; }
    public string? BookName { get; set; }
    public Category? BookCategory { get; set; }
    public Author? BookAuthor { get; set; }
    public Publisher? BookPublisher { get; set; }
    public int PublishYear { get; set; }
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int Amount { get; set; }
    public int BookStatus { get; set; }


    public Book()
        {
            BookAuthor = new Author();
            BookID = 0;
            BookStatus = 0;
        }

        // public override bool Equals(object? obj)
        // {
        //     if (obj == null) return false;
        //     if (obj is Book)
        //     {
        //         return ((Book)obj).BookID.Equals(BookID);
        //     }
        //     return false;
        // }

        public override int GetHashCode()
        {
            return BookID.GetHashCode();
        }
}
}
