using MySqlConnector;
using Persistence;

namespace DAL
{
    public static class BookFilter
    {
        public const int GET_ALL = 0;
        public const int FILTER_BY_BOOK_NAME = 1;
    }
    public class BookDAL
    {
        private string query = "";
        private MySqlConnection connection = DbConfig.GetConnection();

        public Book GetBookByISBN(int isbn)
        {
            Book book = new Book();
            try
            {
                query = @"select b.book_ID, b.ISBN, b.book_name, c.category_name, b.publish_year, 
	                    b.book_description, a.author_name, p.publisher_name, b.price, b.amount, b.book_status
                        from Books b inner join CategoryDetails cd on b.book_ID=cd.book_ID 
	                    inner join Categories c on c.category_ID=cd.category_ID 
	                    inner join Authors_Books ab on b.book_ID = ab.book_ID 
	                    inner join Authors a on ab.author_ID = a.author_ID 
	                    inner join Publishers p on b.publisher_ID = p.publisher_ID
                        where b.ISBN=@isbn;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@isbn", isbn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    book = GetBook(reader);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return book;
        }


        internal Book GetBook(MySqlDataReader reader)
        {
            Book book = new Book();
            book.BookID = reader.GetInt32("book_ID");
            book.ISBN = reader.GetInt32("ISBN");
            book.BookName = reader.GetString("book_name");
            book.BookCategory = new Category();
            Category c = book.BookCategory;
            c.CategoryName = reader.GetString("category_name");
            book.PublishYear = reader.GetInt32("publish_year");
            book.Description = reader.GetString("book_description");
            book.BookAuthor = new Author();
            Author a = book.BookAuthor;
            a.AuthorName = reader.GetString("author_name");
            book.BookPublisher = new Publisher();
            Publisher p = book.BookPublisher;
            p.PublisherName = reader.GetString("publisher_name");
            book.UnitPrice = reader.GetDecimal("price");
            book.Amount = reader.GetInt32("amount");
            book.BookStatus = reader.GetInt16("book_status");
            return book;
        }
        public List<Book> GetBookByName(string bookName)
        {
            List<Book> lb = new List<Book>();
            try
            {
                query = @"select b.book_ID, b.ISBN, b.book_name, c.category_name, b.publish_year, 
	                    b.book_description, a.author_name, p.publisher_name, b.price, b.amount, b.book_status
                        from Books b inner join CategoryDetails cd on b.book_ID=cd.book_ID 
	                    inner join Categories c on c.category_ID=cd.category_ID 
	                    inner join Authors_Books ab on b.book_ID = ab.book_ID 
	                    inner join Authors a on ab.author_ID = a.author_ID 
	                    inner join Publishers p on b.publisher_ID = p.publisher_ID
                        where b.book_status = 1 and b.book_name like concat('%',@bookName,'%');";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@bookName", bookName);
                MySqlDataReader reader = command.ExecuteReader();
                lb = new List<Book>();
                while (reader.Read())
                {
                    lb.Add(GetBook(reader));
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lb;
        }
    }
}
