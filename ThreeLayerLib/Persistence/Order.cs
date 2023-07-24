namespace Persistence;


public static class OrderStatus
{
    public const int CREATE_NEW_ORDER = 1;
    public const int ORDER_INPROGRESS = 2;
}
public class Order
{
    public int OrderID { get; set; }
    public Customer? Customer { get; set; }
    public Customer? OrderCustomer { set; get; }
    public Staff? Staff { get; set; }
    public DateTime OrderDate { get; set; }
    public Book[]? Book { get; set; }
    public int status { get; set; }
    public List<Book>? BooksList { set; get; }
    public Book? this[int index]
    {
        get
        {
            if (BooksList == null || BooksList.Count == 0 || index < 0 || BooksList.Count < index) return null;
            return BooksList[index];
        }
        set
        {
            if (BooksList == null) BooksList = new List<Book>();
            if (value == null) return;
            BooksList.Add(value);
        }
    }
}
