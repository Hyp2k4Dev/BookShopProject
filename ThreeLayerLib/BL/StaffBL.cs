using System.Text;
using DAL;
using Persistence;
using Spectre.Console;

namespace BL
{
    public class StaffBL
    {
        StaffDAL staffDAL = new StaffDAL();

        public Staff? LoginAccount()
        {
            string userName;
            string password;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("ENTER YOUR ACCOUNT TO LOGIN INTO SYSTEM !!!");
            Console.ResetColor();
            Console.Write("│ USER NAME │: ");
            userName = Console.ReadLine() ?? "";
            Console.WriteLine();
            Console.Write("│ PASSWORD │: ");
            password = GetPassword();
            Staff staff = staffDAL.GetStaffAccount(userName);
            if (staff.Password == staffDAL.CreateMD5(password) && staff.StaffStatus == 1)
            {
                return staff;
            }
            else
            {
                return null;
            }
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
        public void ProgressAsync()
        {
            AnsiConsole.Progress().StartAsync(async ctx =>
               {
                   var task1 = ctx.AddTask("[green]Progress[/]");

                   while (!ctx.IsFinished)
                   {
                       await Task.Delay(20);

                       task1.Increment(4.5);
                   }
               });
        }
    }
}
