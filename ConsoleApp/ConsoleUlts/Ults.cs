using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BL;
using Persistence;
using Spectre.Console;
using UI;

namespace Utilities
{
    public class Ults
    {
        ConsoleUI consoleUI = new ConsoleUI();
        string[] LoginMenu = { "| Login" };
        string[] mainMenu = { "| Create Order", "| Exit" };
        string[] coMenu = { "| Search By Book Name ", "| Get By ISBN ", "| Payment", "| Back To Main Menu" };
        BookBL bBL = new BookBL();
        Staff? orderStaff;
        CustomerBL cBL = new CustomerBL();
        StaffBL staffBL = new StaffBL();
        OrderBL oBL = new OrderBL();
        List<Book>? lst;
        public void Login()
        {
            while (true)
            {
                Console.WriteLine(@"
╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩
");
                consoleUI.Line();
                consoleUI.Title(@"                       
                                ╦  ╔═╗╔═╗╦╔╗╔
                                ║  ║ ║║ ╦║║║║
                                ╩═╝╚═╝╚═╝╩╝╚╝
");
                consoleUI.centreLine();
                orderStaff = staffBL.Login();
                if (orderStaff != null)
                {

                    consoleUI.Line();
                    int mainMenuChoice = consoleUI.Menu(@"
╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩
", mainMenu);
                    consoleUI.centreLine();
                    do
                    {
                        switch (mainMenuChoice)
                        {
                            case 1:
                                CreateOrder();
                                break;
                            case 2:
                                break;
                        }
                    } while (mainMenuChoice != mainMenu.Length);
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid User Name/Password ");
                    consoleUI.PressAnyKeyToContinue();
                }
            }
        }
        public void Title(string title)
        {
            Console.WriteLine(title);
        }

        public async void ProgressAsync()
        {
            await AnsiConsole.Progress().StartAsync(async ctx =>
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
        public void CreateOrder()
        {

            Console.WriteLine(@"
╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩
");
            consoleUI.Line();
            int createOrderChoose = consoleUI.Menu(@"                 
                ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔╦╗╔═╗╔╗╔╦ ╦
                ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ║║║║╣ ║║║║ ║
                ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╩ ╩╚═╝╝╚╝╚═╝
", coMenu);
            consoleUI.centreLine();
            do
            {
                switch (createOrderChoose)
                {
                    case 1:
                        SearchBookByName();
                        break;
                    case 2:
                        SearchBookByISBN();
                        break;
                    case 3:
                        // Payment;
                        break;
                }
            } while (createOrderChoose != coMenu.Length);
        }
        public void SearchBookByISBN()
        {
            Console.WriteLine("Input book Code to search: ");
            int isbn;
            if (Int32.TryParse(Console.ReadLine(), out isbn))
            {
                Book b = bBL.GetBookByISBN(isbn);
                if (b != null)
                {
                    if (b.BookStatus == 1)
                    {
                        // Console.WriteLine("Book ID: " + b.BookID);
                        // // Console.WriteLine("ISBN: " + b.ISBN);
                        // Console.WriteLine("Book Name: " + b.BookName);
                        // Console.WriteLine("Book Category: " + b.BookCategory!.CategoryName);
                        // Console.WriteLine("Publish year: " + b.PublishYear);
                        // Console.WriteLine("Author Name: " + b.BookAuthor!.AuthorName);
                        // Console.WriteLine("Publisher Name: " + b.BookPublisher!.PublisherName);
                        // Console.WriteLine("Book Price: " + b.UnitPrice);
                        // Console.WriteLine("Amount: " + b.Amount);
                        // Console.WriteLine("Description: " + b.Description);
                        Console.WriteLine(@"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
┃ Book ID ┃ Book name ┃   Category ┃ publish Year ┃    Description   ┃   Author  ┃  Publisher  ┃ Unit Price ┃  Amount  ┃
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                        Console.WriteLine("┃ {0, 7:N0} ┃ {1, -9} ┃ {2, -9} ┃ {3, 12} ┃ {4, -16} ┃ {5, -9} ┃ {6, -9} ┃ {7, 10:N2} ┃ {8, 8} ┃",
                        b.BookID, b.BookName, b.BookCategory!.CategoryName, b.PublishYear, b.Description, b.BookAuthor!.AuthorName,
                        b.BookPublisher!.PublisherName, b.UnitPrice, b.Amount);
                        Console.WriteLine(@"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                    }
                    else
                    {
                        Console.WriteLine(@"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                        Console.WriteLine("|Book Not Found With code: " + isbn + "┃");
                        Console.WriteLine(@"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                    }
                }
                // else
                // {
                //     Console.WriteLine("Please enter a valid ISBN or Book Title!");
                // }
            }
            Console.WriteLine("\n    Press Enter key to back menu...");
            Console.ReadLine();

        }
        public void SearchBookByName()
        {
            Console.Write("Input book name to search: ");
            string n = Console.ReadLine() ?? "";
            lst = bBL.GetByName(n);
            ShowBookName($"Book Count By Name: {n}", lst);

        }

        static void ShowBookName(string title, List<Book> lst)
        {
            if (lst.Count() > 0)
            {
                Console.WriteLine(title);
                Console.WriteLine(@"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
┃ Book ID ┃ Book name ┃   Category ┃ publish Year ┃    Description   ┃   Author  ┃  Publisher  ┃ Unit Price ┃  Amount  ┃
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                foreach (Book b in lst)
                {
                    if (b.BookStatus == 1)
                    {
                        Console.WriteLine("┃ {0, 7:N0} ┃ {1, -9} ┃ {2, -9} ┃ {3, 12} ┃ {4, -16} ┃ {5, -9} ┃ {6, -9} ┃ {7, 10:N2} ┃ {8, 8} ┃",
                        b.BookID, b.BookName, b.BookCategory!.CategoryName, b.PublishYear, b.Description, b.BookAuthor!.AuthorName,
                        b.BookPublisher!.PublisherName, b.UnitPrice, b.Amount);
                    }
                }
                Console.WriteLine(@"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            }
            else
            {
                Console.WriteLine(" No Books Found.");
            }
        }
    }
}
