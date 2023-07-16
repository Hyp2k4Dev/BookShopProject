using System;
using System.Collections.Generic;
using Persistence;
using BL;
using MySqlConnector;
using Spectre.Console;
using System.IO;
using System.Text;

namespace ConsoleApp
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
            table.AddRow(@"                         ╦  ╔═╗╔═╗╦╔╗╔
                         ║  ║ ║║ ╦║║║║
                         ╩═╝╚═╝╚═╝╩╝╚╝");
            AnsiConsole.Write(table);
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
                mainMenu();
                string? choice = Console.ReadLine();

                // Implement the appropriate functionality based on the user's choice.
                if (choice == "1")
                {
                    //Create order
                    createOrder();


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
                Console.WriteLine("YOUR USERNAME OR PASSWORD IS WRONG !!! PLEASE RE-ENTER!!!");
                _ = LoginAsync();
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
                mainMenu();
            }
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
});
        }
        public static async Task LoginAsync()
        {
            Console.WriteLine("Please enter your account: ");
            string? username = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            string? password = GetPassword();
            if (username == "staff" && password == "staff" || username == "staff1" && password == "staff1" || username == "staff2" && password == "staff2" || username == "staff3" && password == "staff3" || username == "staff4" && password == "staff4" || username == "staff5" && password == "staff5")
            {
                mainMenu();
                string? choice = Console.ReadLine();

                // Implement the appropriate functionality based on the user's choice.
                if (choice == "1")
                {
                    //Create order
                    createOrder();


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
    var task1 = ctx.AddTask("[green]Progress[/]");

    while (!ctx.IsFinished)
    {
        await Task.Delay(100);

        task1.Increment(4.5);
    }
});
            }
            else
            {
                Console.WriteLine("YOUR USERNAME OR PASSWORD IS WRONG !!! PLEASE RE-ENTER!!!");
                _ = LoginAsync();
            }
        }
    }
}