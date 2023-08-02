using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BL;
using Persistence;
using Spectre.Console;
using UI;
using System.Linq;


namespace Utilities
{

    class Ults
    {

        int booksPerPage = 5;
        int currentPage = 1;
        ConsoleUI consoleUI = new ConsoleUI();
        string[] mainMenu = { ". CREATE ORDER ", ". LOGOUT" };
        string[] coMenu = { ". SEARCH BOOK BY NAME ", ". SEARCH BOOK BY ISBN ", ". ADD TO ORDER", ". PAYMENT", ". BACK TO MAIN MENU" };
        BookBL bBL = new BookBL();
        Staff? loginStaff;
        Customer? GetCustomer;
        CustomerBL cBL = new CustomerBL();
        StaffBL staffBL = new StaffBL();
        OrderBL oBL = new OrderBL();
        List<Book>? lst;

        [Obsolete]
        public void Main()
        {
            while (true)
            {
                Console.WriteLine(@"            
┌─────────────────────────────────────────────────────────────────────────────────────┐
│                                                                                     │
│ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
│ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
│ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
│                                                                                     │
│                              │ ╦  ╔═╗╔═╗╦╔╗╔ │                                      │
│                              │ ║  ║ ║║ ╦║║║║ │                                      │
│                              │ ╩═╝╚═╝╚═╝╩╝╚╝ │                                      │   
└─────────────────────────────────────────────────────────────────────────────────────┘
");
                LoginAccount();
            }
        }

        [Obsolete]
        public void LoginAccount()
        {
            loginStaff = staffBL.LoginAccount();
            if (loginStaff != null)
            {

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("                        ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                Console.WriteLine("                                 STAFF USING: " + loginStaff!.StaffName);
                Console.WriteLine("                        ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                Console.ResetColor();
                int mainMenuChoice = consoleUI.Menu(@"
┌─────────────────────────────────────────────────────────────────────────────────────┐
│                                                                                     │
│ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
│ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
│ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
│                                                                                     │
└─────────────────────────────────────────────────────────────────────────────────────┘", mainMenu);
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
        }

        [Obsolete]
        public void CreateOrder()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                        ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("                                 STAFF USING: " + loginStaff!.StaffName);
            Console.WriteLine("                        ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.ResetColor();
            int coChoose = consoleUI.Menu(@"┌─────────────────────────────────────────────────────────────────────────────────────┐
│                                                                                     │
│ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
│ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
│ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
│              │╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔╦╗╔═╗╔╗╔╦ ╦│                    │
│              │║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ║║║║╣ ║║║║ ║│                    │
│              │╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╩ ╩╚═╝╝╚╝╚═╝│                    │
└─────────────────────────────────────────────────────────────────────────────────────┘   
", coMenu);

            do
            {
                switch (coChoose)
                {
                    case 1:
                        SearchBookByName();
                        break;
                    case 2:
                        SearchBookByISBN();
                        break;
                    case 3:
                        // AddOrder();
                        // Add Book To Order
                        break;
                    case 4:

                        // Payment;
                        break;
                    default:
                        break;
                }
            } while (coChoose != coMenu.Length);
        }

        public void SearchBookByISBN()
        {
            var table = new Table();
            table.AddColumns("ID     ", "NAME     ", "CATEGORY  ", " PUBLISHING YEAR ", " DESCRIPTION ", " AUTHOR  ", "    PUBLISHER     ", "  PRICE   ", "AMOUNT ");
            Console.WriteLine("INPUT BOOK'S CODE TO SEARCH: ");
            int isbn;
            if (Int32.TryParse(Console.ReadLine(), out isbn))
            {
                Book b = bBL.GetBookByISBN(isbn);
                if (b != null)
                {
                    if (b.BookStatus == 1)
                    {
                        Console.WriteLine("                                                           BOOK FOUND", Console.ForegroundColor = ConsoleColor.Red);
                        Console.ResetColor();
                        table.AddRow("" + b.BookID, "" + b.BookName, "" + b.BookCategory!.CategoryName, "" + b.PublishYear, "" + b.Description, "" + b.BookAuthor!.AuthorName,
                                                "" + b.BookPublisher!.PublisherName, "" + b.UnitPrice, "" + b.Amount);

                    }
                    else
                    {
                        table.AddEmptyRow();
                        Console.WriteLine("                                               BOOK NOT FOUND WITH CODE:  " + isbn);
                    }
                }
                AnsiConsole.Write(table);
            }
            else
            {
                // Console.WriteLine("\n    PRESS ENTER TO BACK TO CREATE ORDER MENU");
                // while (true)
                // {
                //     Console.ReadKey(true);
                //     if (Console.ReadKey().Key == ConsoleKey.Enter)
                //     {
                //         Console.WriteLine("ADD TO ORDER SUCCESS !");
                //         break;
                //     }
                // }
            }
        }

        [Obsolete]
        public void SearchBookByName()
        {
            Console.WriteLine("INPUT NAME OF BOOK YOU ARE SEARCHING FOR OR PRESS ESCAPE TO BACK TO CREATE ORDER MENU!!! ");
            string n = Console.ReadLine() ?? "";

            lst = bBL.GetByName(n);
            currentPage = 1;

            while (true)
            {
                Console.Clear();
                ShowBookName($"{n}", lst, currentPage);

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    EscToCreateOrderMenu();
                    break;
                }
                else if (key == ConsoleKey.Enter)
                {
                    // Handle Enter key if needed, e.g., show more details about the selected book.
                    Console.ReadKey();
                }
                else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.UpArrow)
                {
                    if (currentPage > 1)
                    {
                        currentPage--;
                    }
                }
                else if (key == ConsoleKey.RightArrow || key == ConsoleKey.DownArrow)
                {
                    // Go to next page if possible
                    if (currentPage < totalPages)
                    {
                        currentPage++;
                    }
                }
            }
        }

        [Obsolete]
        void ShowBookName(string title, List<Book> lst, int currentPage)
        {
            int itemsPerPage = 3;
            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, lst.Count);

            var table = new Table();
            table.AddColumns("ID", "NAME", "CATEGORY", "PUBLISHING YEAR", "DESCRIPTION", "AUTHOR", "PUBLISHER", "PRICE", "AMOUNT");

            for (int i = startIndex; i < endIndex; i++)
            {
                Book b = lst[i];
                if (b.BookStatus == 1)
                {
                    table.AddRow("" + b.BookID, "" + b.BookName, "" + b.BookCategory!.CategoryName, "" + b.PublishYear,
                                 "" + b.Description, "" + b.BookAuthor!.AuthorName, "" + b.BookPublisher!.PublisherName,
                                 "" + b.UnitPrice, "" + b.Amount);
                }
                else
                {
                    table.AddEmptyRow();
                }
            }

            AnsiConsole.Render(table);
            Console.WriteLine($"Page {currentPage}/{totalPages}");
        }


        [Obsolete]
        public void EscToCreateOrderMenu()
        {
            Console.WriteLine("GOING BACK TO CREATE ORDER MENU...");
            Console.Clear();
            CreateOrder();
        }

        
        public void AddBookToOrder(Book book)
        {
            if (book != null)
            {
                // Add the selected book to the order
                oBL.AddBookToOrder(book);

                // For simplicity, let's assume we have access to the current customer.
                // Replace "GetCustomer" with the method or property that retrieves the current customer.
                Customer customer = GetCustomer;

                // Assuming you have a method to get the current order from the OrderBL class.
                Order currentOrder = oBL.GetCurrentOrder(customer);

                // Print both customer and order details
                Console.WriteLine($"Customer: {customer.CustomerName}");
                Console.WriteLine("Current Order:");
                Console.WriteLine($"Order ID: {currentOrder.OrderID}");
                Console.WriteLine($"Order Date: {currentOrder.OrderDate}");
                // Add more order details as needed

                // You can also print the details of the book added to the order
                Console.WriteLine("Book Added to Order:");
                Console.WriteLine($"Book ID: {book.BookID}");
                Console.WriteLine($"Book Name: {book.BookName}");
                // Add more book details as needed

                // Show some message or confirmation that the book is added to the order.
                Console.WriteLine($"Book '{book.BookName}' added to the order.");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }
        private int totalPages
        {
            get
            {
                return (int)Math.Ceiling((double)lst.Count / booksPerPage);
            }
        }
    }
}
