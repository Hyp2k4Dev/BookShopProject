namespace Persistence;

public class Customer
{
    public int CustomerID { get; set; }
    public string? CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public string? CustomerAddress { get; set; }

    public Customer()
    {
        CustomerID = 0;
        CustomerName = "";
        PhoneNumber = "";
        CustomerAddress = "";
    }
}