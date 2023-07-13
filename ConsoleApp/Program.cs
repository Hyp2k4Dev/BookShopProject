using System.Text;
using System.Data.SqlTypes;
using System;
using System.IO;
using Spectre.Console;
using MySqlConnector;

namespace BookShopSystem
{

    class Program
    {

        static async Task Main(string[] args)
        {
            // MySqlConnection connection = new MySqlConnection
            // {
            //     ConnectionString = @"server=localhost;userid=root;password=hiepnb2004;port=3306;database=employees;"
            // };
            // connection.Open();
            var table = new Table();
            table.AddColumn(@"╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩");
            // Console.WriteLine(@"    __  __  __       __     __  __    __ __  __   ___ __   __  __  __  __ __    __    _____ __     ");
            // Console.WriteLine(@"   |__)/  \/  \|_/  (_ |__|/  \|__)  /  |__)|_  /\ | |_   /  \|__)|  \|_ |__)  (_ \_/(_  | |_ |\/| ");
            // Console.WriteLine(@"   |__)\__/\__/| \  __)|  |\__/|     \__| \ |__/--\| |__  \__/| \ |__/|__| \   __) | __) | |__|  | ");
            table.AddRow(@"                         ╦  ╔═╗╔═╗╦╔╗╔
                         ║  ║ ║║ ╦║║║║
                         ╩═╝╚═╝╚═╝╩╝╚╝");
            AnsiConsole.Write(table);
            // Console.WriteLine(@"                                      _                 _       
            //                          | |               (_)      
            //                          | |     ___   __ _ _ _ __  
            //                          | |    / _ \ / _` | | '_ \ 
            //                          | |___| (_) | (_| | | | | |
            //                          \_____/\___/ \__, |_|_| |_|
            //                                          __/ |    
            //                                         |____/         ");
            Console.WriteLine("Please enter your username and password to log in !!!✉");
            Console.WriteLine("Please enter your account: ");
            string? username = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            string? password = GetPassword();
            await AnsiConsole.Progress()
    .StartAsync(async ctx =>
    {
        // Define tasks
        var task1 = ctx.AddTask("[green]Progress[/]");
        // var task2 = ctx.AddTask("Done!!!");

        while (!ctx.IsFinished)
        {
            // Simulate some work
            await Task.Delay(100);

            // Increment
            task1.Increment(4.5);
            // task2.Increment(2);
        }
    });

            if (username == "staff" && password == "staff" || username == "staff1" && password == "staff1" || username == "staff2" && password == "staff2" || username == "staff3" && password == "staff3" || username == "staff4" && password == "staff4" || username == "staff5" && password == "staff5")
            {
                // Console.WriteLine(@"                             ___  ___      _        ___  ___                 
                //              |  \/  |     (_)       |  \/  |                 
                //              | .  . | __ _ _ _ __   | .  . | ___ _ __  _   _ 
                //              | |\/| |/ _` | | '_ \  | |\/| |/ _ \ '_ \| | | |
                //              | |  | | (_| | | | | | | |  | |  __/ | | | |_| |
                //              \_|  |_/\__,_|_|_| |_| \_|  |_/\___|_| |_|\__,_|

                //                                 ");
                // Console.WriteLine("Login Success!!!");
                // Console.WriteLine("CHOOSE THE OPTIONS BELOW ☟ !!!");
                // Console.WriteLine("|1. CREATE ORDER\n|2. PAYMENT\n|3. EXIT APP\n"
                //     + "Enter your choice: ");
                mainMenu();
                string? choice = Console.ReadLine();

                // Implement the appropriate functionality based on the user's choice.
                if (choice == "1")
                {
                    //Create order
                    createOrder();
                    //                     table.AddRow(@"                     ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗
                    //                      ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝
                    //                      ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═");
                    //                     AnsiConsole.Write(table);
                    //                     Console.WriteLine("Book's Name you want to search: ");
                    //                     string? nameOfBook = Console.ReadLine();
                    //                     Console.WriteLine("Book's ID you want to search: ");
                    //                     int idOfBook = Console.Read();
                    //                     Console.WriteLine("Do you want to accept this order? Y/N");
                    //                     string? chooseY = Console.ReadLine();
                    //                     string? chooseN = Console.ReadLine();
                    //                     if (chooseY == "y" || chooseY == "Y")
                    //                     {
                    //                         Console.WriteLine("ACCEPTED ORDER !!!");
                    //                     }
                    //                     else if (chooseN == "n" || chooseN == "N")
                    //                     {
                    //                         Console.WriteLine("DEFUSED ORDER !!!");
                    //                     }
                    //                     else
                    //                     {
                    //                         Console.WriteLine("Invalid choice !!!");
                    //                     }
                    //                     await AnsiConsole.Progress()
                    // .StartAsync(async ctx =>
                    // {
                    //     // Define tasks
                    //     var task1 = ctx.AddTask("[green]Progress[/]");
                    //     // var task2 = ctx.AddTask("Done!!!");

                    //     while (!ctx.IsFinished)
                    //     {
                    //         // Simulate some work
                    //         await Task.Delay(100);

                    //         // Increment
                    //         task1.Increment(4.5);
                    //         // task2.Increment(2);
                    //     }
                    // });
                    // Console.WriteLine("Enter the book's ISBN number: ");
                    // string? ISBN = Console.ReadLine();
                    // Console.WriteLine("Enter the price: ");
                    // decimal? price = decimal.Parse(Console.ReadLine() ?? "");

                    // Add the book record to the database.
                    // mainMenu();
                    // choice = Console.ReadLine();

                }
                else if (choice == "2")
                {
                    //Payment 
                    payment();

                }
                else if (choice == "3")
                {
                    //exit
                    Console.WriteLine("Exiting ... Exited");
                    // Implement the inventory functionality.
                }
                await AnsiConsole.Progress()
.StartAsync(async ctx =>
{
    // Define tasks
    var task1 = ctx.AddTask("[green]Progress[/]");
    // var task2 = ctx.AddTask("Done!!!");

    while (!ctx.IsFinished)
    {
        // Simulate some work
        await Task.Delay(100);

        // Increment
        task1.Increment(4.5);
        // task2.Increment(2);
    }
});
            }
            else
            {
                Console.WriteLine("Check and re-enter your right account");
                // Login();
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
        public static void mainMenu()
        {
            var table = new Table();
            table.AddColumn(@"╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩");
            table.AddRow(@"               ╔╦╗╔═╗╦╔╗╔  ╔╦╗╔═╗╔╗╔╦ ╦               
               ║║║╠═╣║║║║  ║║║║╣ ║║║║ ║               
               ╩ ╩╩ ╩╩╝╚╝  ╩ ╩╚═╝╝╚╝╚═╝               ");
            Console.WriteLine(@"Click '1' to Create the order, Click '2' to Accept payment, Click '3' to exit app");
            table.AddRow(@"|1.Create order");
            table.AddEmptyRow();
            table.AddRow(@"|2.Payment");
            table.AddEmptyRow();
            table.AddRow(@"|3.Exit");
            AnsiConsole.Write(table);

        }
        public static void payment()
        {
            var table = new Table();
            table.AddColumn(@"╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩");
            table.AddRow(@"                 ╔═╗╔═╗╦ ╦╔╦╗╔═╗╔╗╔╔╦╗
                 ╠═╝╠═╣╚╦╝║║║║╣ ║║║ ║ 
                 ╩  ╩ ╩ ╩ ╩ ╩╚═╝╝╚╝ ╩ ");
            table.AddRow(@"|1.Input ID to paid");
            table.AddEmptyRow();
            table.AddRow(@"|2.Back to main menu");
            table.AddEmptyRow();
            AnsiConsole.Write(table);
            // Console.WriteLine("Which id order do you want to process?✅ : ");
            Console.WriteLine("Your choice: ");
            int choice = Console.Read();
            if (true)
            {
                BackToMainMenu();
                mainMenu();
            }


            // Console.WriteLine("Here are the results:" + chooseOrder);
            // Console.WriteLine();
            // Search for the book in the database and display the results.
            // mainMenu();
        }


        public static async void createOrder()
        {
            var table = new Table();
            //Create order
            table.AddColumn(@"╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩");
            table.AddRow(@"              ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗             
              ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝            
              ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═");
            AnsiConsole.Write(table);
            Console.WriteLine("Book's Name you want to search: ");
            string? nameOfBook = Console.ReadLine();
            Console.WriteLine("Book's ID you want to search: ");
            int idOfBook = Console.Read();
            Console.WriteLine("Do you want to accept this order? Y/N");
            string? chooseY = Console.ReadLine();
            string? chooseN = Console.ReadLine();
            if (chooseY == "y" || chooseY == "Y")
            {
                Console.WriteLine("ACCEPTED ORDER !!!");
            }
            else if (chooseN == "n" || chooseN == "N")
            {
                Console.WriteLine("DEFUSED ORDER !!!");
            }
            else
            {
                Console.WriteLine("Invalid choice !!!");
            }
            await AnsiConsole.Progress()
.StartAsync(async ctx =>
{
    // Define tasks
    var task1 = ctx.AddTask("[green]Progress[/]");
    // var task2 = ctx.AddTask("Done!!!");

    while (!ctx.IsFinished)
    {
        // Simulate some work
        await Task.Delay(100);

        // Increment
        task1.Increment(4.5);
        // task2.Increment(2);
    }
    BackToMainMenu();
});
        }
        public static void BackToMainMenu()
        {
            Console.WriteLine("You have returned to the main menu!");
            Console.ReadLine();
        }
    }
}
// public static void Login()
// {
//     // Console.WriteLine(@"    __  __  __       __     __  __    __ __  __   ___ __   __  __  __  __ __    __    _____ __     ");
//     // Console.WriteLine(@"   |__)/  \/  \|_/  (_ |__|/  \|__)  /  |__)|_  /\ | |_   /  \|__)|  \|_ |__)  (_ \_/(_  | |_ |\/| ");
//     // Console.WriteLine(@"   |__)\__/\__/| \  __)|  |\__/|     \__| \ |__/--\| |__  \__/| \ |__/|__| \   __) | __) | |__|  | ");
//     Console.WriteLine(@"                                      _                 _       
//                              | |               (_)      
//                              | |     ___   __ _ _ _ __  
//                              | |    / _ \ / _` | | '_ \ 
//                              | |___| (_) | (_| | | | | |
//                              \_____/\___/ \__, |_|_| |_|
//                                              __/ |    
//                                             |____/         ");
//     Console.WriteLine("Please enter your username and password to log in !!!✉");
//     Console.WriteLine("Please enter your account: ");
//     string? username = Console.ReadLine();
//     Console.WriteLine("Please enter your password: ");
//     string? password = GetPassword();

// }
