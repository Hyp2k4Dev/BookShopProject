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
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public int BookStatus { get; set; }


        public Book()
        {
            BookAuthor = new Author();
            BookID = 0;
            BookStatus = 0;
        }

        public override int GetHashCode()
        {
            return BookID.GetHashCode();
        }
    }
}
