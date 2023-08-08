namespace Persistence;


public static class OrderStatus
{
    public const int CREATE_NEW_ORDER = 1;
    public const int ORDER_INPROGRESS = 2;
}
public class Order
{
    public int OrderID { get; set; }
    public Customer? OrderCustomer { set; get; }
    public Staff? OrderStaff { get; set; }
    public DateTime OrderDate { get; set; }
    public int OrderStatus { get; set; }
    public List<Book> BooksList { set; get; }
    public int Quantity { get; set; }

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

    public Order()
    {
        BooksList = new List<Book>();
        OrderID = 0;
        OrderStatus = 0;
    }
}
