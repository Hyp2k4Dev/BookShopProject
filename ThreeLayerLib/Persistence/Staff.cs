namespace Persistence;

public class Staff
{
    public int StaffID { get; internal set; }
    public string? StaffName { get; set; }
    public string UserName { get; set; } = "staff";
    public string Password { get; set; } = "staff";

}
