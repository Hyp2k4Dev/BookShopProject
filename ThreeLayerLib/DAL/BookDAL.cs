using MySqlConnector;
using Persistence;

namespace DAL;

public class BookDAL
{
    private string query = "";
    private MySqlConnection connection = DbConfig.GetConnection();
    public List<Book> GetBookDetails()
    {
        Book book = new Book();
        List<Book> bookList = new List<Book>();
        try
        {
            query = @"select * from Books;";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                book = GetBook(reader);
                bookList.Add(book);
            }
            reader.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return bookList;
    }
    public Book GetBookByID(int bookID)
    {
        Book book = new Book();
        try
        {
            query = @"select * from Books WHERE book_ID = @BookId;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@bookId", bookID);
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
    public Book GetBook(MySqlDataReader reader)
    {
        Book book = new Book();
        book.BookID = reader.GetInt32("book_ID");
        book.Title = reader.GetString("title");
        book.AuthorName = reader.GetString("");
        product.Price = reader.GetDecimal("price");
        return product;
    }
}
