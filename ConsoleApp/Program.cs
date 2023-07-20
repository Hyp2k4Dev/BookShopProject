using System;
using System.Collections.Generic;
using Spectre.Console;
using System.IO;
using System.Text;
using Persistence;

namespace ConsoleApp
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var table = new Table();
            table.AddColumn(@"╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩");
            table.AddRow(@"                         ╦  ╔═╗╔═╗╦╔╗╔
                         ║  ║ ║║ ╦║║║║
                         ╩═╝╚═╝╚═╝╩╝╚╝");
            AnsiConsole.Write(table);
            Console.WriteLine("Please enter your username and password to log in !!!");
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
                    Console.WriteLine("Exiting ... Exited");
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
            table.AddRow(@"|1.Create order");
            table.AddEmptyRow();
            table.AddRow(@"|2.Payment");
            table.AddEmptyRow();
            table.AddRow(@"|3.Exit");
            AnsiConsole.Write(table);
            Console.WriteLine(@"Click '1' to Create the order, Click '2' to Accept payment, Click '3' to exit app");
            Console.WriteLine("Your Choice: ");
            string? choice = Console.ReadLine();
            if (choice == "1")
            {
                createOrderMain();
            }
            else if (choice == "2")
            {
                // Payment();
            }


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
            //   1.searchBookValue
            string? choice = Console.ReadLine();
            if (choice == "1")
            {
                _ = createOrderMain();
            }
            if (choice == "2")
            {
                // addBookToOrder();
            }
            else if (choice == "3")
            {
                mainMenu();
            }

            //      inputBookValue:DOne => displayBookDetail
            //   2.addBookToOrder
            //      displayAllBook => inputBookId => displayBookDetail => inputQuantity => Message: Success => addMore 1.Yes(Add new) 2.No (=> Input Cus If => Message: Success) => Main

            AnsiConsole.Write(table);
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
                createOrder();
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
                else
                {
                    Console.WriteLine("Invalid choice!!!");
                    mainMenu();
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



        public static Task createOrderMain()
        {
            var table = new Table();
            table.AddColumn(@"╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩");
            table.AddRow(@"         ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔╦╗╔═╗╔╗╔╦ ╦
         ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ║║║║╣ ║║║║ ║
         ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╩ ╩╚═╝╝╚╝╚═╝");
            table.AddRow(@"|1.Search Book");
            table.AddEmptyRow();
            table.AddRow(@"|2.Add Book To Order");
            table.AddEmptyRow();
            table.AddRow(@"|3.Back To Main Menu");
            AnsiConsole.Write(table);
            string? choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.WriteLine("Enter the book name you are searching for:");
                string bookName = Console.ReadLine() ?? "";
                Book searchBook = new Book();
                searchBook.SearchBookName(bookName);
                if (searchBook.SearchBookName(bookName))
                {
                    Console.WriteLine($"Book ID: {bookId} found.");
                }
                else
                {
                    Console.WriteLine("Book ID not found.");
                }

            }
            else if (choice == "2")
            {
                Console.WriteLine("Enter the book ID number:");
                int bookId = int.Parse(Console.ReadLine() ?? "");
                Console.WriteLine("Enter the book name:");
                string name = Console.ReadLine() ?? "";
                Console.WriteLine("Enter the book price:");
                decimal price = int.Parse(Console.ReadLine() ?? "");
                Book searchBook = new Book();
                if (searchBook.SearchForBookByID(bookId))
                {
                    Console.WriteLine($"Book ID: {bookId} found.");
                }
                else
                {
                    Console.WriteLine("Book ID not found.");
                }

                Console.WriteLine("Press enter to continue");
                Console.ReadKey();
                mainMenu();
            }


            return Task.CompletedTask;
        }
    }
}