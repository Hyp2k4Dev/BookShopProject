namespace Persistence;

public class Customer
{
    public int CustomerID { get; set; }
    public string? CustomerName { get; set; }
    public int PhoneNumber { get; set; }
    public string? CustomerAddress { get; set; }

    public Customer()
    {
        CustomerID = 0;
        CustomerName = "";
        PhoneNumber = 0;
        CustomerAddress = "";
    }
}