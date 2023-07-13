namespace Persistence;

public class Order
{
    public int OrderID { get; set; }
    public int StaffID { set; get; }
    public string status { get; set; } = "default";
}