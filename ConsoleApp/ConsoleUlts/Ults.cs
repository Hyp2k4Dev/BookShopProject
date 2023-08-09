using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BL;
using Persistence;
using Spectre.Console;
using UI;
using System.Linq;
using System.Globalization;


namespace Utilities
{

    class Ults
    {
        int booksPerPage = 5;
        int currentPage = 1;
        ConsoleUI consoleUI = new ConsoleUI();
        string[] mainMenu = { ". CREATE ORDER ", ". LOGOUT" };
        string[] coMenu = { ". SEARCH BOOK BY NAME ", ". SEARCH BOOK BY CODE ", ". CREATE ORDER", ". PAYMENT", ". BACK TO MAIN MENU" };
        BookBL bBL = new BookBL();
        Staff? loginStaff1;
        CustomerBL cBL = new CustomerBL();
        StaffBL staffBL = new StaffBL();
        OrderBL oBL = new OrderBL();
        List<Book>? lst;

        [Obsolete]
        public void Main()
        {
            while (true)
            {

                Console.WriteLine(@"                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                                    [LOGIN]                                          │
                    └─────────────────────────────────────────────────────────────────────────────────────┘   
");
                LoginAccount();
            }
        }

        [Obsolete]
        public void LoginAccount()
        {
            loginStaff1 = staffBL.LoginAccount();
            if (loginStaff1 != null)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("                                            [ STAFF USING: " + loginStaff1!.StaffName + " ]");
                Console.ResetColor();
                int mainMenuChoice = consoleUI.Menu(@"
                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                                [MAIN MENU]                                          │
                    └─────────────────────────────────────────────────────────────────────────────────────┘", mainMenu);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("                                            [ STAFF USING: " + loginStaff1!.StaffName + " ]");
                Console.ResetColor();
                do
                {
                    switch (mainMenuChoice)
                    {
                        case 1:
                            CreateOrderMenu();
                            break;
                        case 2:
                            Main();
                            break;
                    }
                } while (mainMenuChoice != mainMenu.Length);
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("INVALID USER NAME/PASSWORD!! PLEASE ENTER VALID USER NAME/PASSWORD");
            Console.ForegroundColor = ConsoleColor.White;
        }

        [Obsolete]
        public void MainMenu()
        {

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("                                            [ STAFF USING: " + loginStaff1!.StaffName + " ]");
            Console.ResetColor();
            int mainMenuChoice = consoleUI.Menu(@"
                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                                [MAIN MENU]                                          │
                    └─────────────────────────────────────────────────────────────────────────────────────┘", mainMenu);
            do
            {
                switch (mainMenuChoice)
                {
                    case 1:
                        CreateOrderMenu();
                        break;
                    case 2:
                        return;
                }
            } while (mainMenuChoice != mainMenu.Length);
        }

        [Obsolete]
        public void CreateOrderMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                              [ STAFF USING: " + loginStaff1!.StaffName + " ]");
            Console.ResetColor();
            int coChoose = consoleUI.Menu(@"                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                             [CREATE ORDER MENU]                                     │
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
                        CreateOrder();
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
            Console.WriteLine(@"                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                            [SEARCH BOOK BY ISBN]                                    │
                    └─────────────────────────────────────────────────────────────────────────────────────┘   
");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                            [ STAFF USING: " + loginStaff1!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;
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
                        Console.ForegroundColor = ConsoleColor.White;
                        table.AddRow("" + b.BookID, "" + b.BookName, "" + b.BookCategory!.CategoryName, "" + b.PublishYear, "" + b.Description, "" + b.BookAuthor!.AuthorName,
                                    "" + b.BookPublisher!.PublisherName, "" + b.Price.ToString("C"), "" + b.Amount);

                        AnsiConsole.Write(table);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("PRESS ESC TO BACK TO CREATE ORDER MENU OR PRESS ENTER TO CONTINUE SEARCH!!!");
                        Console.ForegroundColor = ConsoleColor.White;
                        var key = Console.ReadKey(true).Key;
                        if (key == ConsoleKey.Escape)
                        {
                            BackToCreateOrderMenu();
                        }
                    }
                    else
                    {
                        table.AddEmptyRow();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("                                               BOOK NOT FOUND WITH CODE:  " + isbn);
                        Console.ForegroundColor = ConsoleColor.White;
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
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("INVALID CODE!! PLEASE INPUT VALID CODE TO SEARCH!!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        [Obsolete]
        public void SearchBookByName()
        {
            Console.Clear();
            Console.WriteLine(@"                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                            [SEARCH BOOK BY NAME]                                    │
                    └─────────────────────────────────────────────────────────────────────────────────────┘   
");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                            [ STAFF USING: " + loginStaff1!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("INPUT NAME OF BOOK YOU ARE SEARCHING FOR: ");
            string n = Console.ReadLine() ?? "";
            Console.Clear();
            lst = bBL.GetByName(n);
            currentPage = 1;

            while (true)
            {
                ShowBookName($"{n}", lst, currentPage);

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    BackToCreateOrderMenu();
                    break;
                }
                else if (key == ConsoleKey.Enter)
                {
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
                                 "" + b.Price.ToString("C"), "" + b.Amount);
                }
                else
                {
                    table.AddEmptyRow();
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" PRESS ESCAPE TO BACK TO CREATE ORDER MENU!!! ");
            Console.ForegroundColor = ConsoleColor.White;
            AnsiConsole.Render(table);
            Console.WriteLine($"                                                         Page {currentPage}/{totalPages}");
            return;
        }


        [Obsolete]
        public void BackToCreateOrderMenu()
        {
            Console.WriteLine("GOING BACK TO CREATE ORDER MENU...");
            Console.Clear();
            CreateOrderMenu();
        }
        private int totalPages
        {
            get
            {
                return (int)Math.Ceiling((double)lst!.Count / booksPerPage);
            }
        }
        public void AddCustomer()
        {
            Console.WriteLine(@"                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                             [ADD CUSTOMER INFO]                                     │
                    └─────────────────────────────────────────────────────────────────────────────────────┘   
");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                            [ STAFF USING: " + loginStaff1!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("CUSTOMER NAME: ");
            string name = Console.ReadLine() ?? "no name";
            Console.Write("PHONE NUMBER: ");
            string phone = Console.ReadLine() ?? "No Phone Number";
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
            Console.Write("Input ID to search: ");
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

        [Obsolete]
        public void CreateOrder()
        {
            Order o = new Order();
            ConsoleKey answer;
            var table = new Table();
            do
            {

                Console.WriteLine(@"                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                             [ADD BOOK TO ORDER]                                     │
                    └─────────────────────────────────────────────────────────────────────────────────────┘");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("                                            [ STAFF USING: " + loginStaff1!.StaffName + " ]");
                Console.ForegroundColor = ConsoleColor.White;
                int isbn = 0;
                Console.Write("INPUT BOOK CODE: ");
                if (Int32.TryParse(Console.ReadLine(), out isbn))
                {
                    Book book = bBL.GetBookByISBN(isbn);

                    if (book != null)
                    {
                        if (book.BookStatus == 1 && book.Amount > 0)
                        {
                            Console.WriteLine("BOOK NAME: " + book.BookName);
                            Console.WriteLine("PRICE: " + FormatCurrencyToVND(book.Price));
                            Console.WriteLine("QUANTITY: " + book.Amount);

                            do
                            {
                                Console.Write("ENTER QUANTITY: ");
                                int.TryParse(Console.ReadLine(), out int quantity); // Fixed this line

                                if (quantity <= 0 || quantity > book.Amount)
                                {
                                    Console.WriteLine("INVALID QUANTITY!");
                                }
                                else
                                {
                                    book.Amount = quantity;
                                    o.Quantity = quantity; // Assign quantity to the order's Quantity property
                                    o.BooksList.Add(book);
                                    Console.WriteLine("ADD TO ORDER COMPLETED");
                                }
                            } while (o.Quantity <= 0 || o.Quantity > book.Amount); // Fixed this line
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("THIS BOOK IS OUT OF STOCK!! PLEASE CHOOSE ANOTHER BOOK");
                            Console.ForegroundColor = ConsoleColor.White;
                            return;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("BOOK NOT FOUND!");
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("NOT FOUND BOOK WITH THIS CODE");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("IF YOU WANT TO ADD ANOTHER BOOK PRESS <ESCAPE> OR PRESS <ENTER> TO GO TO ADD CUSTOMER?: ");
                Console.ForegroundColor = ConsoleColor.White;
                answer = Console.ReadKey(true).Key;
                if (answer == ConsoleKey.Enter) break;
            } while (answer == ConsoleKey.Escape);
            Console.Clear();
            Console.WriteLine(@"                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                             [ADD NEW CUSTOMER]                                      │
                    └─────────────────────────────────────────────────────────────────────────────────────┘");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                           [ STAFF USING: " + loginStaff1!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("CUSTOMER NAME: ");
            string name = Console.ReadLine() ?? "no name";
            string phone = "";
            Console.Write("PHONE NUMBER: ");
            do
            {
                phone = Console.ReadLine() ?? "";
                if (!string.IsNullOrEmpty(phone) && phone.Length == 10 && phone.All(char.IsDigit))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("ADD PHONE NUMBER SUCCESS");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("INVALID PHONE NUMBER! PLEASE CHOOSE AND INPUT CUSTOMER INFORMATION AGAIN!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (!string.IsNullOrEmpty(phone) && phone.Length == 10 && phone.All(char.IsDigit));
            Console.Write("CUSTOMER ADDRESS: ");
            string address = Console.ReadLine() ?? "";
            o.OrderCustomer = new Customer { CustomerName = name, PhoneNumber = phone, CustomerAddress = address };
            Console.Clear();
            o.OrderStaff = loginStaff1;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                         [ STAFF CREATE ORDER: " + loginStaff1!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("CUSTOMER NAME: " + o.OrderCustomer!.CustomerName);
            Console.WriteLine("PHONE NUMBER: " + o.OrderCustomer.PhoneNumber);
            Console.WriteLine("CUSTOMER ADDRESS: " + o.OrderCustomer.CustomerAddress);
            table.AddColumns("BOOK NAME ", "PRICE ", "AMOUNT ", "TOTAL PRICE ");
            foreach (Book b in o.BooksList)
            {
                table.AddRow(
                    "" + b.BookName,
                    "" + FormatCurrencyToVND(b.Price), // Format the price to VND format
                    "" + b.Amount,
                    "" + FormatCurrencyToVND(b.Price * b.Amount)// Format the total price to VND format
                );
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("CREATE ORDER: " + (oBL.SaveOrder(o) ? "COMPLETED!" + " WITH ORDER ID: " + o.OrderID : "NOT COMPLETED!"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n    PRESS ESCAPE TO BACK TO CREATE ORDER MENU...");
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Escape)
            {
                Console.Clear();
                BackToCreateOrderMenu();
                Console.ReadKey();
            }
            else if (key == ConsoleKey.Enter)
            {
                Console.ReadKey();
            }

        }

        [Obsolete]
        public void Payment()
        {
            var table = new Table();
            decimal totalAmount = 0;
            Console.WriteLine(@"                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                                 [PAYMENT]                                           │
                    └─────────────────────────────────────────────────────────────────────────────────────┘");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                         [ STAFF CREATE ORDER: " + loginStaff1!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;
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


                    table.AddColumns("BOOK NAME", "PRICE", "AMOUNT", "TOTAL PRICE", "TOTAL AMOUNT");

                    foreach (Book b in o.BooksList)
                    {
                        decimal totalPriceForBook = b.Price * b.Amount;
                        totalAmount += totalPriceForBook;
                        table.AddRow(
                            b.BookName,
                            FormatCurrencyToVND(b.Price),
                            b.Amount.ToString(),
                            FormatCurrencyToVND(totalPriceForBook)
                        );
                    }
                    table.AddRow("", "", "", "", FormatCurrencyToVND(totalAmount));
                    AnsiConsole.Write(table);
                    Console.WriteLine("\n    PRESS ESCAPE TO BACK TO CREATE ORDER MENU...");
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        BackToCreateOrderMenu();
                        Console.ReadKey();
                    }
                    else if (key == ConsoleKey.Enter)
                    {
                        Console.ReadKey();
                    }
                }
                else
                {
                    table.AddColumn("ORDER NOT FOUND WITH ID : " + orderID);

                }
            }
        }
        public string FormatCurrencyToVND(decimal amount)
        {
            CultureInfo culture = new CultureInfo("vi-VN"); // Vietnamese culture
            string formattedAmount = string.Format(culture, "{0:C0}", amount); // C0 format specifier for currency without decimal places

            formattedAmount = formattedAmount.Replace(culture.NumberFormat.CurrencySymbol, "VND");

            return formattedAmount;
        }
    }
}
