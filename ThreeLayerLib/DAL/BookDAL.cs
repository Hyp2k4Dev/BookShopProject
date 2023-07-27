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

        public Book GetBookById(int bookId)
        {
            Book book = new Book();
            try
            {
                query = @"select b.book_ID, b.ISBN, b.book_name, c.category_name, b.publish_year, 
	                b.book_description, a.author_name, p.publisher_name, b.unit_price, b.amount, b.book_status
                    from Books b inner join CategoryDetails cd on b.book_ID=cd.book_ID 
	                inner join Categories c on cd.category_ID=cd.category_ID 
	                inner join Authors_Books ab on b.book_ID = ab.book_ID 
	                inner join Authors a on ab.author_ID = a.author_ID 
	                inner join Publishers p on b.publisher_ID = p.publisher_ID                    
                    where b.book_ID=@bookId;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@bookId", bookId);
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
            // book.BookCategory!.CategoryName = reader.GetString("category_name");
            book.PublishYear = reader.GetInt32("publish_year");
            book.Description = reader.GetString("book_description");
            // book.BookAuthor!.AuthorName = reader.GetString("author_name");
            // book.BookPublisher!.PublisherName = reader.GetString("publisher_name");
            book.UnitPrice = reader.GetDecimal("unit_price");
            book.Amount = reader.GetInt32("amount");
            book.BookStatus = reader.GetInt16("book_status");
            return book;
        }
        public List<Book> GetBooks(int bookFilter, Book book)
        {
            List<Book> lb = new List<Book>();
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                switch (bookFilter)
                {
                    case BookFilter.GET_ALL:
                        query = @"select * from Books;";
                        break;
                    case BookFilter.FILTER_BY_BOOK_NAME:
                        query = @"select * from Books
                        where book_status = 1 and book_name like @bookName order by book_ID asc;";
                        // command.Parameters.AddWithValue("@bookName", book.BookName);
                        break;
                }

                command.CommandText = query;
                if (bookFilter == BookFilter.FILTER_BY_BOOK_NAME)
                {
                    command.Parameters.AddWithValue("@bookName", "%" + book.BookName + "%");
                }
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
            // List<Book> output = new List<Book>();
            // foreach (var i in lb)
            // {
            //     int count = 0;
            //     foreach (var o in output)
            //     {
            //         if (o.BookName == i.BookName) count++;
            //     }
            //     if (count == 0)
            //     {
            //         Console.WriteLine(i.BookName);
            //         output.Add(i);
            //     }
            // }
            // return output;
            return lb;
        }
    }
}

