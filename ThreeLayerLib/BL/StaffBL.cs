using System.Text;
using DAL;
using Persistence;
using Spectre.Console;

namespace BL
{
    public class StaffBL
    {
        StaffDAL staffDAL = new StaffDAL();

        public Staff? Login()
        {
            string userName;
            string password;
            Console.WriteLine("User Name: ");
            userName = Console.ReadLine() ?? "";
            Console.WriteLine("Password: ");
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
                   // Define tasks
                   var task1 = ctx.AddTask("[green]Progress[/]");
                   // var task2 = ctx.AddTask("Done!!!");

                   while (!ctx.IsFinished)
                   {
                       // Simulate some work
                       await Task.Delay(20);

                       // Increment
                       task1.Increment(4.5);
                       // task2.Increment(2);
                       // Console.Clear();
                   }
               });
        }
    }
}
