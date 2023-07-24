using System.Text;
using DAL;
using Persistence;

namespace BL
{
    public class StaffBL
    {
        public Staff? Login()
        {
            StaffDAL staffDAL = new StaffDAL();
            string userName;
            string password;
            Console.WriteLine("User Name: ");
            userName = Console.ReadLine() ?? "";
            Console.WriteLine("Password: ");
            password = GetPassword();
            Staff staff = new Staff();
            staff = staffDAL.GetStaffAccount(userName);
            if (staff.Password == password && staff.StaffStatus == 1)
            {

                return staff;
            }
            return null;
        }
        public static string GetPassword()
        {
            StringBuilder pass = new StringBuilder();
            while (true)
            {
                int x = Console.CursorLeft;
                int y = Console.CursorTop;
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass.Remove(pass.Length - 1, 1);
                    Console.SetCursorPosition(x - 1, y);
                    Console.Write(" ");
                    Console.SetCursorPosition(x - 1, y);
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    pass.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            return pass.ToString();
        }
    }
}
