using BL;
using Persistence;
using Spectre.Console;
using UI;
using System.Globalization;

namespace Utilities
{

    class Ults
    {

        ConsoleUI consoleUI = new ConsoleUI();
        string[] mainMenu = { ". MAIN MENU ", ". CHECK ORDER BILL", ". LOGOUT" };
        string[] coMenu = { ". CREATE ORDER", ". PAYMENT", ". BACK TO MAIN MENU" };
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

                Console.WriteLine(@"┌─────────────────────────────────────────────────────────────────────────────────────┐
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

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("[ STAFF USING: " + loginStaff1!.StaffName + " ]");
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
                            ViewOrdersStatus();
                            break;
                        case 3:
                            break;

                    }
                } while (mainMenuChoice != mainMenu.Length);
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("INVALID USER NAME/PASSWORD!! PLEASE ENTER VALID USER NAME/PASSWORD");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        [Obsolete]
        public void MainMenu()
        {

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("[ STAFF USING: " + loginStaff1!.StaffName + " ]");
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
                        ViewOrdersStatus();
                        break;
                    case 3:
                        break;
                }
            } while (mainMenuChoice != mainMenu.Length);
        }

        [Obsolete]
        public void CreateOrderMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("[ STAFF USING: " + loginStaff1!.StaffName + " ]");
            Console.ResetColor();
            int coChoose = consoleUI.Menu(@"┌─────────────────────────────────────────────────────────────────────────────────────┐
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
                        CreateOrder();
                        break;
                    case 2:
                        Payment();
                        break;
                    default:
                        MainMenu();
                        break;
                }
            } while (coChoose != coMenu.Length);
        }

        [Obsolete]
        public void ViewOrdersStatus()
        {
            decimal totalAmount = 0;
            Console.WriteLine(@"┌─────────────────────────────────────────────────────────────────────────────────────┐
│                                                                                     │
│ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
│ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
│ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
│─────────────────────────────────────────────────────────────────────────────────────│
│                               [CHECK ORDER BILL]                                    │
└─────────────────────────────────────────────────────────────────────────────────────┘");
            var check = new Table();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"[ STAFF USING: {loginStaff1!.StaffName} ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("INPUT ORDER ID: ");
            if (Int32.TryParse(Console.ReadLine(), out int orderID))
            {
                Order o = oBL.GetOrderByID(orderID);
                if (o != null)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(@"
                            ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔╗ ╦╦  ╦  
                            ║ ║╠╦╝ ║║║╣ ╠╦╝  ╠╩╗║║  ║  
                            ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝╩╩═╝╩═╝");
                    Console.WriteLine("                           ─────────────────────────────");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("SHOP: H&T BOOK SHOP");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\t");
                    Console.WriteLine();
                    Console.WriteLine("                         ORDER DATE: " + o.OrderDate);

                    if (o.OrderCustomer != null)
                    {
                        Console.WriteLine("CUSTOMER NAME: " + o.OrderCustomer.CustomerName);
                        Console.WriteLine("PHONE NUMBER: " + o.OrderCustomer.PhoneNumber);
                        Console.WriteLine("CUSTOMER ADDRESS: " + o.OrderCustomer.CustomerAddress);

                    }

                    Console.WriteLine("\t");

                    check.AddColumns("BOOK NAME", "PRICE", "AMOUNT", "TOTAL PRICE", "TOTAL AMOUNT");

                    if (o.BooksList != null)
                    {
                        foreach (Book b in o.BooksList)
                        {
                            decimal totalPriceForBook = b.Price * b.Amount;
                            totalAmount += totalPriceForBook;
                            check.AddRow(
                                b.BookName,
                                FormatCurrencyToVND(b.Price),
                                b.Amount.ToString(),
                                FormatCurrencyToVND(totalPriceForBook)
                            );
                        }

                        check.AddRow("", "", "", "", FormatCurrencyToVND(totalAmount));
                        AnsiConsole.Render(check);
                    }
                    else
                    {
                        Console.WriteLine("No books found in the order.");
                    }
                }
                else
                {
                    Console.WriteLine($"Order with ID {orderID} not found.");
                }
            }
            Console.WriteLine("\nPress ESC to go back or ENTER to search another id...");
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    MainMenu();
                    Console.ReadKey();
                    break;
                }
                else if (key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    return;
                }
            }
        }


        [Obsolete]
        public void CreateOrder()
        {
            Order o = new Order();
            ConsoleKey answer;
            var showAllTable = new Table();
            Console.WriteLine(@"┌─────────────────────────────────────────────────────────────────────────────────────┐
│                                                                                     │
│ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
│ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
│ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
│─────────────────────────────────────────────────────────────────────────────────────│
│                               [CREATE ORDER]                                        │
└─────────────────────────────────────────────────────────────────────────────────────┘");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("[ STAFF USING: " + loginStaff1!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;

            // Display list of books here
            List<Book> availableBooks = bBL.GetAllBooks(""); // Get the list of all available books
            showAllTable.AddColumns("ID", "NAME", "CATEGORY", "PUBLISHING YEAR", "DESCRIPTION", "AUTHOR", "PUBLISHER", "PRICE", "AMOUNT");
            foreach (Book availableBook in availableBooks)
            {
                showAllTable.AddRow("" + availableBook.BookID, "" + availableBook.BookName, "" + availableBook.BookCategory!.CategoryName, "" + availableBook.PublishYear,
                                 "" + availableBook.Description, "" + availableBook.BookAuthor!.AuthorName, "" + availableBook.BookPublisher!.PublisherName,
                                 "" + FormatCurrencyToVND(availableBook.Price), "" + availableBook.Amount);
            }
            AnsiConsole.Render(showAllTable);
            Console.WriteLine("PRESS <ENTER> TO INPUT BOOK CODE OR PRESS <ESCAPE> TO STOP SEARCHING ");
            var stopSearch = Console.ReadKey(true).Key;
            if (stopSearch == ConsoleKey.Escape)
            {
                Console.Clear();
                CreateOrderMenu();
                Console.ReadKey();
            }
            else if (stopSearch == ConsoleKey.Enter)
            {
                do
                {
                    int isbn = 0;
                    Console.Write("INPUT BOOK CODE TO SEARCHING: ");
                    if (Int32.TryParse(Console.ReadLine(), out isbn))
                    {
                        Book book = bBL.GetBookByISBN(isbn);

                        if (book != null)
                        {
                            if (book.BookStatus == 1 && book.Amount > 0)
                            {
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
                    Console.WriteLine("IF YOU WANT TO ADD ANOTHER BOOK PRESS <ESCAPE> OR PRESS <ANY KEY> TO GO TO ADD CUSTOMER?: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    answer = Console.ReadKey(true).Key;
                    if (answer == ConsoleKey.Enter) break;
                } while (answer == ConsoleKey.Escape);
                Console.Clear();
                Console.WriteLine(@"┌─────────────────────────────────────────────────────────────────────────────────────┐
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
                string? name;
                do
                {
                    Console.Write("CUSTOMER NAME: ");
                    name = Console.ReadLine()?.Trim();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("NAME CANNOT BE EMPTY.PLEASE TRY AGAIN.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("ADD CUSTOMER NAME SUCCESS !");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                } while (string.IsNullOrWhiteSpace(name));
                string phone = "";
                Console.Write("PHONE NUMBER: ");
                do
                {
                    Console.Write("PHONE NUMBER: ");
                    phone = Console.ReadLine() ?? "";

                    if (string.IsNullOrEmpty(phone) || phone.Length != 10 || !phone.All(char.IsDigit))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("INVALID PHONE NUMBER! PLEASE CHOOSE AND INPUT CUSTOMER INFORMATION AGAIN!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("ADD CUSTOMER PHONE NUMBER SUCCESS !");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                } while (string.IsNullOrEmpty(phone) || phone.Length != 10 || !phone.All(char.IsDigit));
                string address;
                do
                {
                    Console.Write("CUSTOMER ADDRESS: ");
                    address = Console.ReadLine() ?? "";
                    if (string.IsNullOrWhiteSpace(address))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("CUSTOMER ADDRESS CANNOT BE EMPTY. PLEASE TRY AGAIN.");
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else
                    {

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("ADD CUSTOMER ADDRESS SUCCESS !");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                } while (string.IsNullOrWhiteSpace(address));
                o.OrderCustomer = new Customer { CustomerName = name, PhoneNumber = phone, CustomerAddress = address };
                Console.Clear();
                o.OrderStaff = loginStaff1;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("[ STAFF CREATE ORDER: " + loginStaff1!.StaffName + " ]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("CUSTOMER NAME: " + o.OrderCustomer!.CustomerName);
                Console.WriteLine("PHONE NUMBER: " + o.OrderCustomer.PhoneNumber);
                Console.WriteLine("CUSTOMER ADDRESS: " + o.OrderCustomer.CustomerAddress);
                var addCustomerTable = new Table();
                addCustomerTable.AddColumns("BOOK NAME ", "PRICE ", "QUANTITY ", "TOTAL AMOUNT ");
                foreach (Book b in o.BooksList)
                {
                    addCustomerTable.AddRow(
                        "" + b.BookName,
                        "" + FormatCurrencyToVND(b.Price), // Format the price to VND format
                        "" + b.Amount,
                        "" + FormatCurrencyToVND(b.Price * b.Amount)// Format the total price to VND format
                    );
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Press <C> to Confirm Order, <X> to Cancel Order...");
                Console.ForegroundColor = ConsoleColor.White;
                var reqKey = Console.ReadKey(true).Key;
                if (reqKey == ConsoleKey.C)
                {
                    // Proceed with order confirmation
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Order Confirmed!");
                    Console.WriteLine("CREATE ORDER: " + (oBL.SaveOrder(o) ? "COMPLETED!" + " WITH ORDER ID: " + o.OrderID : "NOT COMPLETED!"));
                    Console.ForegroundColor = ConsoleColor.White;
                    // ... (Rest of your code for displaying and confirming the order)
                }
                else if (reqKey == ConsoleKey.X)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Order Cancelled!");
                    Console.ForegroundColor = ConsoleColor.White;
                    // Handle order cancellation here (e.g., remove added books)
                    CreateOrderMenu();
                    Console.ReadKey();
                }
                Console.WriteLine("\n    PRESS ESCAPE TO BACK TO CREATE ORDER MENU...");
                var backKey = Console.ReadKey(true).Key;
                if (backKey == ConsoleKey.Escape)
                {
                    Console.Clear();
                    CreateOrderMenu();
                    Console.ReadKey();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("INVALID CHOOSE!!!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        [Obsolete]
        public void Payment()
        {
            var table = new Table();
            decimal totalAmount = 0;

            Console.WriteLine(@"┌─────────────────────────────────────────────────────────────────────────────────────┐
│                                                                                     │
│ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
│ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
│ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
│─────────────────────────────────────────────────────────────────────────────────────│
│                                 [PAYMENT]                                           │
└─────────────────────────────────────────────────────────────────────────────────────┘");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" [ STAFF CREATE ORDER: " + loginStaff1!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("PRESS <ENTER> INPUT ORDER ID OR PRESS <ESCAPE> TO STOP PAYING: ");
            var stopPayment = Console.ReadKey(true).Key;
            if (stopPayment == ConsoleKey.Escape)
            {
                Console.Clear();
                CreateOrderMenu();
                Console.ReadKey();
            }
            else if (stopPayment == ConsoleKey.Enter)
            {
                Console.WriteLine("INPUT ORDER ID: ");
                if (Int32.TryParse(Console.ReadLine(), out int orderID))
                {
                    Order o = oBL.GetOrderByID(orderID);
                    if (o != null)
                    {

                        Console.WriteLine("ORDER DATE: " + o.OrderDate);
                        Console.WriteLine("ORDER STATUS: " + o.OrderStatus);
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
                        AnsiConsole.Render(table);

                        Console.WriteLine("\nTOTAL PRICE: " + FormatCurrencyToVND(totalAmount));
                        Console.Write("ENTER PRICE PAID: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal amountPaid))
                        {
                            if (amountPaid >= totalAmount)
                            {
                                decimal change = amountPaid - totalAmount;
                                Console.WriteLine("EXCESS MONEY: " + FormatCurrencyToVND(change));
                                Console.WriteLine("PAYMENT SUCCESSFUL!");
                                o.OrderStatus = 1;
                                oBL.GetOrder(o.OrderID);
                            }
                            else
                            {
                                decimal remainingAmount = totalAmount - amountPaid;
                                Console.WriteLine("REMAINING AMOUNT PAID: " + FormatCurrencyToVND(remainingAmount));
                                Console.WriteLine("PAYMENT INCOMPLETE !");
                            }

                            Console.WriteLine("\nPRESS ESCAPE TO BACK TO CREATE ORDER MENU...");
                            var key = Console.ReadKey(true).Key;
                            if (key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                CreateOrderMenu();
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Amount Paid.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Order Not Found with ID : " + orderID);
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("INVALID INPUT. PLEASE TRY AGAIN.");
                Console.ForegroundColor = ConsoleColor.White;
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
