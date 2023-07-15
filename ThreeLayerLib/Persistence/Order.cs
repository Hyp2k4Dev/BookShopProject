namespace Persistence;

public class Order
{
    public int OrderID { get; set; }
    public Customer? Customer { get; set; }
    public Staff? Staff { get; set; }
    public DateTime OrderDate { get; set; }
    public List<Book>? book { get; set; }
    public int status { get; set; }
}