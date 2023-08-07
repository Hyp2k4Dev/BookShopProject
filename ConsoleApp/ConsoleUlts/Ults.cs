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
        int amount;
        int booksPerPage = 5;
        int currentPage = 1;
        ConsoleUI consoleUI = new ConsoleUI();
        string[] mainMenu = { ". CREATE ORDER ", ". LOGOUT" };
        string[] coMenu = { ". SEARCH BOOK BY NAME ", ". SEARCH BOOK BY CODE ", ". CREATE ORDER", ". PAYMENT", ". BACK TO MAIN MENU" };
        BookBL bBL = new BookBL();
        Staff? loginStaff;
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
            loginStaff = staffBL.LoginAccount();
            if (loginStaff != null)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("                                            [ STAFF USING: " + loginStaff!.StaffName + " ]");
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
                Console.Clear();
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
            Console.WriteLine("                                            [ STAFF USING: " + loginStaff!.StaffName + " ]");
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
            Console.Clear();
            do
            {
                switch (mainMenuChoice)
                {
                    case 1:
                        CreateOrderMenu();
                        break;
                    case 2:
                        break;
                }
            } while (mainMenuChoice != mainMenu.Length);
        }

        [Obsolete]
        public void CreateOrderMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                            [ STAFF USING: " + loginStaff!.StaffName + " ]");
            Console.ResetColor();
            int coChoose = consoleUI.Menu(@"                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                                [MAIN MENU]                                          │
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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                            [ STAFF USING: " + loginStaff!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                            [SEARCH BOOK BY ISBN]                                    │
                    └─────────────────────────────────────────────────────────────────────────────────────┘   
");
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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                            [ STAFF USING: " + loginStaff!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                            [SEARCH BOOK BY NAME]                                    │
                    └─────────────────────────────────────────────────────────────────────────────────────┘   
");
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
            Console.WriteLine("ADD NEW CUSTOMER");
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
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("                                            [ STAFF USING: " + loginStaff!.StaffName + " ]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(@"                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                             [ADD BOOK TO ORDER]                                     │
                    └─────────────────────────────────────────────────────────────────────────────────────┘");


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
                            Console.WriteLine("PRICE: " + book.Price);
                            Console.WriteLine("AMOUNT: " + book.Amount);
                            do
                            {
                                Console.Write("ENTER QUANTITY: ");
                                int.TryParse(Console.ReadLine(), out amount);
                                if (amount <= 0 || amount > bBL.GetBookByISBN(isbn).Amount)
                                {
                                    Console.WriteLine("INVALID AMOUNT!");
                                }
                                else
                                {
                                    book.Amount = amount;
                                    o.BooksList.Add(book);
                                    Console.WriteLine("ADD TO ORDER COMPLETED");
                                }
                            } while (amount <= 0 || amount > bBL.GetBookByISBN(isbn).Amount);
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("THIS BOOK IS OUT OF STOCK!!PLEASE CHOOSE ANOTHER BOOK");
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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                           [ STAFF USING: " + loginStaff!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                         ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("                         |               ADD NEW CUSTOMER             |");
            Console.WriteLine("                         ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.Write("CUSTOMER NAME: ");
            string name = Console.ReadLine() ?? "no name";
            string phone = "";
            Console.WriteLine("PHONE NUMBER: ");
            do
            {
                phone = Console.ReadLine() ?? "";
                if (!string.IsNullOrEmpty(phone) && phone.Length == 10 && phone.All(char.IsDigit))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ADD PHONE NUMBER SUCCESS");
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
            o.OrderStaff = loginStaff;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                         [ STAFF CREATE ORDER: " + loginStaff!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("CUSTOMER NAME: " + o.OrderCustomer!.CustomerName);
            Console.WriteLine("PHONE NUMBER: " + o.OrderCustomer.PhoneNumber);
            Console.WriteLine("CUSTOMER ADDRESS: " + o.OrderCustomer.CustomerAddress);
            table.AddColumns("BOOK NAME ", "PRICE ", "AMOUNT ", "TOTAL PRICE ");
            foreach (Book b in o.BooksList)
            {
                table.AddRow("" + b.BookName, "" + b.Price.ToString("C"), "" + b.Amount, "" + (b.Price * b.Amount).ToString("C"));
            }
            AnsiConsole.Write(table);
            Console.WriteLine("CREATE ORDER: " + (oBL.SaveOrder(o) ? "COMPLETED!" + " WITH ORDER ID: " + o.OrderID : "NOT COMPLETED!"));
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

                    var table = new Table();
                    table.AddColumns("BOOK NAME ", "PRICE ", "AMOUNT ", "TOTAL PRICE ");

                    foreach (Book b in o.BooksList)
                    {
                        table.AddRow("" + b.BookName, "" + b.Price.ToString("C"), "" + b.Amount, "" + (b.Price * b.Amount).ToString("C"));
                    }

                    AnsiConsole.Write(table);
                }
                else
                {
                    Console.WriteLine(@"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                    Console.WriteLine("| Order Not Found With ID: " + orderID + " ┃");
                    Console.WriteLine(@"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                }
            }
        }
    }
}
// mới thêm được 1 sản phẩm vào order, sau khi thêm book 1 thêm book 2 thì chỉ nhận thông tin book2 không thấy thông tin book 1
