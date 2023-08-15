namespace Persistence;

public class Staff
{
    public int StaffID { get; internal set; }
    public string? StaffName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public int StaffStatus { get; set; }
    public decimal TotalRevenue { get; set; }

}
