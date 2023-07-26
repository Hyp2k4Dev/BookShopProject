using System.Collections.Generic;
using DAL;
using Persistence;

namespace BL
{
public class BookBL
{
    private BookDAL bDAL = new BookDAL();
    public Book GetBookById(int bookId)
    {
        return bDAL.GetBookById(bookId);
    }
    public List<Book> GetAll()
    {
        return bDAL.GetBooks(BookFilter.GET_ALL, new Book());
    }
    public List<Book> GetByName(string bookName)
        {
            return bDAL.GetBooks(BookFilter.FILTER_BY_BOOK_NAME, new Book{BookName=bookName});
        }
}
}
