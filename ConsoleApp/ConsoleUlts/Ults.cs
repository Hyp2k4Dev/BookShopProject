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
        string[] coMenu = { ". SEARCH BOOK BY NAME ", ". SEARCH BOOK BY CODE ", ". ADD TO ORDER", ". PAYMENT", ". BACK TO MAIN MENU" };
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
                Console.Clear();
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
            Console.Clear();
        }
        public void MainMenu()
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
            Console.Clear();
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
                        AddBooks();
                        break;
                    case 4:
                        Payment();
                        break;
                    default:
                        MainMenu();
                        break;
                }
            } while (coChoose != coMenu.Length);
        }

        [Obsolete]
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
                                    "" + b.BookPublisher!.PublisherName, "" + b.Price, "" + b.Amount);

                        AnsiConsole.Write(table);
                        Console.WriteLine("PRESS ESC TO BACK TO CREATE ORDER MENU OR PRESS ENTER TO CONTINUE SEARCH!!!");
                        var key = Console.ReadKey(true).Key;
                        if (key == ConsoleKey.Escape)
                        {
                            BackToCreateOrderMenu();
                        }
                    }
                    else
                    {
                        table.AddEmptyRow();
                        Console.WriteLine("                                               BOOK NOT FOUND WITH CODE:  " + isbn);
                        AnsiConsole.Write(table);
                        Console.WriteLine("PRESS ESC TO BACK TO CREATE ORDER MENU OR PRESS ENTER TO CONTINUE SEARCH!!!");
                        var key = Console.ReadKey(true).Key;
                        if (key == ConsoleKey.Escape)
                        {
                            BackToCreateOrderMenu();
                        }
                    }
                }
            }
            else
            {
            }
        }


        [Obsolete]
        public void SearchBookByName()
        {
            Console.WriteLine("INPUT NAME OF BOOK YOU ARE SEARCHING FOR: ");
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
                    BackToCreateOrderMenu();
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
            int itemsPerPage = 5;
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
                                 "" + b.Price, "" + b.Amount);
                }
                else
                {
                    table.AddEmptyRow();
                }
            }
            Console.WriteLine(" PRESS ESCAPE TO BACK TO CREATE ORDER MENU!!! ");
            AnsiConsole.Render(table);
            Console.WriteLine($"Page {currentPage}/{totalPages}");
        }


        [Obsolete]
        public void BackToCreateOrderMenu()
        {
            Console.WriteLine("GOING BACK TO CREATE ORDER MENU...");
            Console.Clear();
            CreateOrder();
        }



        private int totalPages
        {
            get
            {
                return (int)Math.Ceiling((double)lst.Count / booksPerPage);
            }
        }
        public void AddCustomer()
        {
            Console.WriteLine("ADD NEW CUSTOMER");
            Console.Write("CUSTOMER NAME: ");
            string name = Console.ReadLine() ?? "no name";
            Console.WriteLine("PHONE NUMBER: ");
            int phone = Convert.ToInt32(Console.ReadLine() ?? "No Phone Number");
            Console.Write("CUSTOMER ADDRESS: ");
            string address = Console.ReadLine() ?? "";
            Customer c = new Customer { CustomerName = name, PhoneNumber = phone, CustomerAddress = address };
            c.CustomerID = cBL.AddCustomer(c);
            if (c.CustomerID > 0)
            {
                Console.WriteLine($"ADD CUSTOMER COMPLETED WITH ID:  {c.CustomerID}");
            }
        }
        public void SearchCustomer()
        {
            Console.WriteLine("Input ID to search: ");
            int c;
            if (Int32.TryParse(Console.ReadLine(), out c))
            {
                Customer? ctr = cBL.GetCustomerById(c);
                if (ctr != null)
                {

                    // Console.WriteLine("Book ID: " + ctr.CustomerID);
                    Console.WriteLine("CUSTOMER NAME: " + ctr.CustomerName);
                    Console.WriteLine("PHONE NUMBER: " + ctr.PhoneNumber);
                    Console.WriteLine("CUSTOMER ADDRESS: " + ctr.CustomerAddress);

                }
            }
            else
            {
                Console.WriteLine("PLEASE ENTER VALID CUSTOMER NAME!");
            }
        }

        public void AddBooks()
        {
            var table = new Table();
            Order o = new Order();
            bool addAnotherBook = true;

            while (addAnotherBook)
            {
                int isbn;
                Console.WriteLine("INPUT BOOK CODE: ");
                if (Int32.TryParse(Console.ReadLine(), out isbn))
                {
                    Book book = bBL.GetBookByISBN(isbn);
                    if (book != null)
                    {
                        // table.AddColumns("BOOK CODE", "BOOK NAME", "BOOK PRICE");
                        if (book.BookStatus == 1)
                        {
                            // table.AddRow("" + book.ISBN, "" + book.BookName, "" + book.Price);
                            Console.WriteLine($"BOOK NAME: {book.BookName}");
                            Console.WriteLine($"BOOK CATEGORY: {book.BookCategory!.CategoryName}");
                            Console.WriteLine($"BOOK DESCRIPTION: {book.Description}");
                            Console.WriteLine($"BOOK AUTHOR: {book.BookAuthor!.AuthorName}");
                            Console.WriteLine($"BOOK QUANTITY: {book.Amount}");
                            Console.WriteLine($"PRICE: {book.Price}");
                            Console.WriteLine();
                            int amount;
                            Console.WriteLine("INPUT BOOK QUANTITY:");
                            if (Int32.TryParse(Console.ReadLine(), out amount))
                            {
                                Console.WriteLine($"ADD BOOK COMPLETED: {amount} ");
                                book.Amount = amount; // Set the amount for the book.
                                o.BooksList.Add(book);
                            }
                            else
                            {
                                Console.WriteLine("INVALID QUANTITY INPUT.BOOK ADD FAILED");
                            }
                        }
                        else
                        {
                            Console.WriteLine("THIS BOOK IS OUT OF STOCK!!PLEASE CHOOSE ANOTHER BOOK");
                            AddBooks();
                        }
                    }
                    // else
                    // {
                    //     Console.WriteLine("BOOK NOT FOUND!");
                    // }
                }
                else
                {
                    Console.WriteLine("INVALID CODE. BOOK NOT FOUND!");
                    AddBooks();
                }

                Console.Write("DO YOU WANT TO ADD ANOTHER BOOK? (Y/N): ");
                Console.WriteLine();
                string? answer = Console.ReadLine();
                if (answer?.Trim().ToLower() != "y")
                {
                    addAnotherBook = false;
                }
            }
            Console.WriteLine("                         ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("                         |               ADD NEW CUSTOMER             |");
            Console.WriteLine("                         ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.Write("CUSTOMER NAME: ");
            string name = Console.ReadLine() ?? "no name";
            Console.WriteLine("PHONE NUMBER: ");
            int phone = Convert.ToInt32(Console.ReadLine().Trim() ?? "0");
            Console.Write("CUSTOMER ADDRESS: ");
            string address = Console.ReadLine() ?? "";
            o.OrderCustomer = new Customer { CustomerName = name, PhoneNumber = phone, CustomerAddress = address };
            Console.Clear();
            o.OrderStaff = loginStaff;
            Console.WriteLine("                        ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("                             STAFF CREATE ORDER: " + o.OrderStaff!.StaffName);
            Console.WriteLine("                        ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("CUSTOMER NAME: " + o.OrderCustomer!.CustomerName);
            Console.WriteLine("PHONE NUMBER: " + o.OrderCustomer.PhoneNumber);
            Console.WriteLine("CUSTOMER ADDRESS: " + o.OrderCustomer.CustomerAddress);
            table.AddColumns("BOOK NAME ", "PRICE ", "AMOUNT ", "TOTAL PRICE ");
            foreach (Book b in o.BooksList)
            {
                table.AddRow("" + b.BookName, "" + b.Price, "" + b.Amount, "" + b.Price * b.Amount);
            }
            AnsiConsole.Write(table);
            Console.WriteLine("CREATE ORDER: " + (oBL.SaveOrder(o) ? "COMPLETED!" : "NOT COMPLETED!"));
            Console.WriteLine("\n    PRESS ENTER TO CONTINUE...");
            Console.ReadLine();
        }



        public void Payment()
        {
            Console.WriteLine("INPUT ORDER ID: ");
            if (Int32.TryParse(Console.ReadLine(), out int orderID))
            {
                Order o = oBL.GetOrderByID(orderID);
                if (o != null)
                {
                    Console.WriteLine("ORDER DATE: " + o.OrderDate);
                    Console.WriteLine("CUSTOMER NAME: " + o.OrderCustomer!.CustomerName);
                    Console.WriteLine("PHONE NUMBER: " + o.OrderCustomer.PhoneNumber);
                    Console.WriteLine("CUSTOMER ADDRESS: " + o.OrderCustomer.CustomerAddress);
                    Console.WriteLine("\t");
                    foreach (Book b in o.BooksList)
                    {

                        Console.WriteLine("BOOK NAME: " + b.BookName);
                        Console.WriteLine("PRICE: " + b.Price);
                        Console.WriteLine("AMOUNT: " + b.Amount);
                        Console.WriteLine($"TOTAL: {b.Price * b.Amount}");
                    }
                }
                else
                {
                    Console.WriteLine(@"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                    Console.WriteLine("| Order Not Found With ID: " + orderID + " ┃");
                    Console.WriteLine(@"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                }
            }

            Console.WriteLine("\n    PRESS ESCAPE TO BACK TO CREATE ORDER MENU...");
            Console.ReadLine();
        }
    }
}
