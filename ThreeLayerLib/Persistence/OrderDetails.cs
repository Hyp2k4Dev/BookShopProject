namespace Persistence;

public class OrderDetails
{
    public int OrderID { get; set; }
    public string BookName { get; set; } = "Unprocessed";
    public int BookQuantity { get; set; }
    public decimal Price { get; set; }
}