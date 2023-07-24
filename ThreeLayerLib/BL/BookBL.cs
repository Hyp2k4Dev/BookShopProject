namespace BL;

using DAL;
using Persistence;


public class BookBL
{
    private BookDAL bookDAL = new BookDAL();
    public Book GetBookById(int bookId)
    {
        return bookDAL.GetBookByID(bookId);
    }
    public List<Book> GetAll()
    {
        return bookDAL.GetBooks(BookFilter.GET_ALL, new Book());
    }
}
