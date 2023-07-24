using MySqlConnector;
using Persistence;

namespace DAL;
public static class BookFilter
{
    public const int GET_ALL = 0;
    public const int FILTER_BY_BOOK_NAME = 1;
}

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
            query = @"select * from Books WHERE book_ID = @book_ID;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@book_ID", bookID);
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
        book.AuthorID = reader.GetInt32("author_ID");
        book.Title = reader.GetString("title");
        book.Price = reader.GetDecimal("price");

        return book;
    }

    public List<Book> GetBooks(int bookFilter, Book book)
    {
        List<Book> books = new List<Book>();
        try
        {
            MySqlCommand command = new MySqlCommand("", connection);
            switch (bookFilter)
            {
                case BookFilter.GET_ALL:
                    query = @"select book_id, book_name, price, book_status, ifnull(book_description, '') as book_description from Books";
                    break;
                case BookFilter.FILTER_BY_BOOK_NAME:
                    query = @"select book_id, book_name, price, book_status, ifnull(book_description, '') as book_description from Books
                                where book_name like concat('%',@bookName,'%');";
                    command.Parameters.AddWithValue("@bookName", book.BookName);
                    break;
            }
            command.CommandText = query;
            MySqlDataReader reader = command.ExecuteReader();
            books = new List<Book>();
            while (reader.Read())
            {
                books.Add(GetBook(reader));
            }
            reader.Close();
        }
        catch { }
        return books;
    }

}

