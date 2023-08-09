using System.Collections.Generic;
using DAL;
using Persistence;

namespace BL
{
    public class BookBL
    {
        private BookDAL bDAL = new BookDAL();
        public Book GetBookByISBN(int isbn)
        {
            return bDAL.GetBookByISBN(isbn);
        }

        public List<Book> GetByName(string bookName)
        {
            return bDAL.GetBookByName(bookName);
        }

        public List<Book> GetAllBooks(string bookName)
        {
            return bDAL.GetAllBooks(bookName);
        }
    }
}
