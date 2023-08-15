using BL;
using Persistence;
using Spectre.Console;
using UI;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Utilities
{

    class Ults
    {

        ConsoleUI consoleUI = new ConsoleUI();
        string[] mainMenu = { ". CREATE ORDER ", ". VIEW REVENUE OF STAFF IN DAY", ". LOGOUT" };
        BookBL bBL = new BookBL();
        Staff? loginStaff1;
        CustomerBL cBL = new CustomerBL();
        StaffBL staffBL = new StaffBL();
        OrderBL oBL = new OrderBL();

        [Obsolete]
        public void Main()
        {
            while (true)
            {

                Console.WriteLine(@"                            ┌─────────────────────────────────────────────────────────────────────────────────────┐
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
                Console.Write("                                                       [ Staff using: " + loginStaff1!.StaffName + " ]");
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
                            CreateOrder();
                            break;
                        case 2:
                            ViewStaffRevenueInDay();
                            break;
                        case 3:
                            MainMenu();
                            break;

                    }
                } while (mainMenuChoice != mainMenu.Length);
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid User Name/ Password! Please try again.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        [Obsolete]
        public void MainMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("[ Staff using: " + loginStaff1!.StaffName + " ]");
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
                        CreateOrder();
                        break;
                    case 2:
                        ViewStaffRevenueInDay();
                        break;
                    case 3:
                        LoginAccount();
                        break;
                }
            } while (mainMenuChoice != mainMenu.Length);
        }


        [Obsolete]
        public void CreateOrder()
        {
            Console.Clear();
            decimal totalAmount = 0;
            Order o = new Order();
            ConsoleKey answer;
            var showAllTable = new Table();
            Console.Clear();
            Console.WriteLine(@"                             ┌─────────────────────────────────────────────────────────────────────────────────────┐
                            │                                                                                     │
                            │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                            │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                            │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                            │─────────────────────────────────────────────────────────────────────────────────────│
                            │                               [CREATE ORDER]                                        │
                            └─────────────────────────────────────────────────────────────────────────────────────┘");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                                  [ Staff using: " + loginStaff1!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;

            // Display list of books here
            List<Book> availableBooks = bBL.GetAllBooks(""); // Get the list of all available books
            showAllTable.AddColumn("ID");
            showAllTable.AddColumn(new TableColumn("Book Name").Centered());
            showAllTable.AddColumn("Category");
            showAllTable.AddColumn(new TableColumn("Publish Year").Centered());
            showAllTable.AddColumn(new TableColumn("Description").Centered());
            showAllTable.AddColumn("Author");
            showAllTable.AddColumn("Publisher");
            showAllTable.AddColumn(new TableColumn("Price").RightAligned());
            showAllTable.AddColumn(new TableColumn(" Quantity").Centered());
            foreach (Book availableBook in availableBooks)
            {
                showAllTable.AddRow("" + availableBook.BookID, "" + availableBook.BookName, "" + availableBook.BookCategory!.CategoryName, "" + availableBook.PublishYear,
                                 "" + availableBook.Description, "" + availableBook.BookAuthor!.AuthorName, "" + availableBook.BookPublisher!.PublisherName,
                                 "" + FormatCurrencyToVND(availableBook.Price), "" + availableBook.Amount);
            }
            AnsiConsole.Render(showAllTable);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press< ENTER > to search book by Code or < ESC > to return Main Menu");
            Console.ForegroundColor = ConsoleColor.White;
            var stopSearch = Console.ReadKey(true).Key;
            if (stopSearch == ConsoleKey.Escape)
            {
                Console.Clear();
                MainMenu();
                Console.ReadKey();
            }
            else if (stopSearch == ConsoleKey.Enter)
            {
                do
                {
                    int isbn = 0;
                    Console.Write("Input book's code to searching: ");
                    if (Int32.TryParse(Console.ReadLine(), out isbn))
                    {
                        Book book = bBL.GetBookByISBN(isbn);

                        if (book != null)
                        {
                            if (book.BookStatus == 1 && book.Amount > 0)
                            {
                                do
                                {
                                    Console.Write("Enter quantity: ");
                                    int.TryParse(Console.ReadLine(), out int quantity); // Fixed this line

                                    if (quantity <= 0 || quantity > book.Amount)
                                    {
                                        Console.WriteLine("Invalid quantity!");
                                    }
                                    else
                                    {
                                        book.Amount = quantity;
                                        o.Quantity = quantity; // Assign quantity to the order's Quantity property
                                        o.BooksList.Add(book);
                                        Console.WriteLine("Add to order completed! ");
                                    }
                                } while (o.Quantity <= 0 || o.Quantity > book.Amount); // Fixed this line
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("This book is out of stock! Please choose another book.");
                                Console.ForegroundColor = ConsoleColor.White;
                                return;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Book not found!");
                            Console.ForegroundColor = ConsoleColor.White;
                            return;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Book not found with this code!");
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Press < ESC > to add another book or < Enter > to add new customer: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    answer = Console.ReadKey(true).Key;
                    if (answer == ConsoleKey.Enter) break;
                } while (answer == ConsoleKey.Escape);
                Console.Clear();
                Console.WriteLine(@"                       ┌─────────────────────────────────────────────────────────────────────────────────────┐
                    │                                                                                     │
                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                    │─────────────────────────────────────────────────────────────────────────────────────│
                    │                             [ADD NEW CUSTOMER]                                      │
                    └─────────────────────────────────────────────────────────────────────────────────────┘");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("                                        [ Staff using: " + loginStaff1!.StaffName + " ]");
                Console.ForegroundColor = ConsoleColor.White;
                string? name;
                do
                {
                    Console.Write("Customer's name: ");
                    name = Console.ReadLine()?.Trim();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Name cannot be empty! Please Try again.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Add name Successful !");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                } while (string.IsNullOrWhiteSpace(name));
                string phone = "";
                do
                {
                    Console.Write("Phone number: ");
                    phone = Console.ReadLine() ?? "";

                    if (string.IsNullOrEmpty(phone) || phone.Length != 10 || !phone.All(char.IsDigit) || !IsValidPhoneNumber(phone))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid phone number! Please try again");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Add Customer's Phone Successful!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                } while (string.IsNullOrEmpty(phone) || phone.Length != 10 || !phone.All(char.IsDigit) || !IsValidPhoneNumber(phone));
                string address;
                do
                {
                    Console.Write("Customer's Address: ");
                    address = Console.ReadLine() ?? "";
                    if (string.IsNullOrWhiteSpace(address))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Customer's address cannot be Empty. Please try again.");
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else
                    {

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Add Customer address Successful !");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                } while (string.IsNullOrWhiteSpace(address));
                o.OrderCustomer = new Customer { CustomerName = name, PhoneNumber = phone, CustomerAddress = address };
                Console.Clear();
                o.OrderStaff = loginStaff1;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("                              [ Staff Create Order: " + loginStaff1!.StaffName + " ]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("Customer Name: " + o.OrderCustomer!.CustomerName);
                Console.WriteLine("Phone Number: " + o.OrderCustomer.PhoneNumber);
                Console.WriteLine("Customer Address: " + o.OrderCustomer.CustomerAddress);
                var addCustomerTable = new Table();
                addCustomerTable.AddColumn(new TableColumn("Book Name").LeftAligned());
                addCustomerTable.AddColumn(new TableColumn("Price").Centered());
                addCustomerTable.AddColumn(new TableColumn("Quantity").Centered());
                addCustomerTable.AddColumn(new TableColumn("Total Price").RightAligned());
                addCustomerTable.AddColumn(new TableColumn("[yellow]Total Amount[/]").RightAligned());
                foreach (Book b in o.BooksList)
                {
                    decimal totalPriceForBook = b.Price * b.Amount;
                    totalAmount += totalPriceForBook;
                    addCustomerTable.AddRow(
                        b.BookName,
                        FormatCurrencyToVND(b.Price),
                        b.Amount.ToString(),
                        FormatCurrencyToVND(totalPriceForBook)
                    );
                }
                addCustomerTable.AddRow("", "", "", "", $"[yellow]{FormatCurrencyToVND(totalAmount)}[/]");
                AnsiConsole.Render(addCustomerTable);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Press <C> to Confirm Order, <X> to Cancel Order...");
                Console.ForegroundColor = ConsoleColor.White;
                var reqKey = Console.ReadKey(true).Key;
                if (reqKey == ConsoleKey.C)
                {
                    // Proceed with order confirmation
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Order Confirmed!");
                    Console.WriteLine("Create order: " + (oBL.SaveOrder(o) ? "Completed!" + " with order ID: " + o.OrderID : "Uncompleted!"));
                    Console.ForegroundColor = ConsoleColor.White;
                    Payment();
                    // ... (Rest of your code for displaying and confirming the order)
                }
                else if (reqKey == ConsoleKey.X)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Order Cancelled!");
                    Console.ForegroundColor = ConsoleColor.White;
                    // Handle order cancellation here (e.g., remove added books)
                    MainMenu();
                    Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Choose!!! Try Again");
                    Console.ForegroundColor = ConsoleColor.White;
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

            Console.WriteLine(@"                            ┌─────────────────────────────────────────────────────────────────────────────────────┐
                            │                                                                                     │
                            │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                            │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                            │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                            │─────────────────────────────────────────────────────────────────────────────────────│
                            │                                 [PAYMENT]                                           │
                            └─────────────────────────────────────────────────────────────────────────────────────┘");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                         [ Staff created the order: " + loginStaff1!.StaffName + " ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press< ENTER > to input Order ID or < ESC > to return Create Order: ");
            var stopPayment = Console.ReadKey(true).Key;
            if (stopPayment == ConsoleKey.Escape)
            {
                Console.Clear();
                CreateOrder();
                Console.ReadKey();
            }
            else if (stopPayment == ConsoleKey.Enter)
            {
                Console.WriteLine("Input order ID: ");
                if (Int32.TryParse(Console.ReadLine(), out int orderID))
                {
                    Order o = oBL.GetOrderByID(orderID);
                    if (o != null)
                    {

                        Console.WriteLine("Customer's name: " + o.OrderCustomer!.CustomerName);
                        Console.WriteLine("Phone number: " + o.OrderCustomer.PhoneNumber);
                        Console.WriteLine("Customer's address: " + o.OrderCustomer.CustomerAddress);
                        Console.WriteLine("\t");

                        table.AddColumn(new TableColumn("Book Name").LeftAligned());
                        table.AddColumn(new TableColumn("Price").Centered());
                        table.AddColumn(new TableColumn("Quantity").Centered());
                        table.AddColumn(new TableColumn("Total Price").RightAligned());
                        table.AddColumn(new TableColumn("[yellow]Total Amount[/]").RightAligned());

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

                        table.AddRow("", "", "", "", $"[yellow]{FormatCurrencyToVND(totalAmount)}[/]");
                        AnsiConsole.Render(table);
                        Console.Write("Enter Price Paid: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal amountPaid))
                        {
                            if (amountPaid >= totalAmount)
                            {
                                decimal change = amountPaid - totalAmount;
                                Console.WriteLine("Excess Money: " + FormatCurrencyToVND(change));
                                Console.WriteLine("Payment Successful!");
                                o.OrderStatus = 1;
                                oBL.GetOrder(o.OrderID);

                                if (change > 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("PRESS ENTER TO PAID EXCESS MONEY...");
                                    Console.ReadKey();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("PAID EXCESS MONEY SUCCESSFUL!!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("PRESS < ANY KEY > TO EXPORT INVOICE!!!");
                                    Console.ReadKey();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    ViewOrdersStatus(o.OrderID);
                                }
                                else
                                {
                                    Console.WriteLine("PRESS < ANY KEY > TO EXPORT INVOICE!!!");
                                    Console.ReadKey();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    ViewOrdersStatus(o.OrderID);
                                }
                            }
                            else
                            {
                                decimal remainingAmount = totalAmount - amountPaid;
                                Console.WriteLine("REMAINING AMOUNT PAID: " + FormatCurrencyToVND(remainingAmount));
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("PAYMENT INCOMPLETE !");
                                Console.ForegroundColor = ConsoleColor.White;
                                return;
                            }
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nPress < ESC > to return Create Order ...");
                            Console.ForegroundColor = ConsoleColor.White;
                            var key = Console.ReadKey(true).Key;
                            if (key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                CreateOrder();
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Amount Paid.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Order not found with Id : " + orderID);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input! Please try again.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        [Obsolete]
        public void ViewOrdersStatus(int orderID)
        {
            Console.Clear();
            decimal totalAmount = 0;
            var check = new Table();

            Order o = oBL.GetOrderByID(orderID);
            if (o != null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"
                                    ╦╔╗╔╦  ╦╔═╗╦╔═╗╔═╗╔═╗
                                    ║║║║╚╗╔╝║ ║║║  ║╣ ╚═╗
                                    ╩╝╚╝ ╚╝ ╚═╝╩╚═╝╚═╝╚═╝
");
                Console.WriteLine("                                    ─────────────────────                             ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");
                Console.WriteLine($"                                                                Number: {o.OrderID}");
                Console.WriteLine("                                                                 Order date: " + o.OrderDate);
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Shop: VTC BOOK SHOP");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("ADDRESS: 4th floor, VTC Online Buildings, 18 Tam Trinh st., Hai Ba Trung, Ha Noi");
                Console.WriteLine("Hotline: 0971443180 ");
                Console.WriteLine();
                Console.WriteLine("Seller: " + loginStaff1!.StaffName);
                Console.WriteLine();
                if (o.OrderCustomer != null)
                {
                    Console.WriteLine("Customer's Name: " + o.OrderCustomer.CustomerName);

                    Console.WriteLine("Customer's Phone: " + o.OrderCustomer.PhoneNumber);

                    Console.WriteLine("Customer's Address: " + o.OrderCustomer.CustomerAddress);

                    Console.WriteLine();
                }
                check.AddColumn(new TableColumn("  Book's name  ").LeftAligned());
                check.AddColumn(new TableColumn("  Price  ").Centered());
                check.AddColumn(new TableColumn("  Quantity  ").Centered());
                check.AddColumn(new TableColumn("  Total Price  ").RightAligned());
                check.AddColumn(new TableColumn("[yellow]    Total Amount     [/]").RightAligned());
                if (o.BooksList.Count() != 0)
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

                    check.AddRow("", "", "", "", $"[yellow]{FormatCurrencyToVND(totalAmount)}[/]");
                    AnsiConsole.Render(check);
                    Console.WriteLine();
                }
                else if (o.BooksList!.Count() == 0)
                {
                    Console.Clear();
                    Console.WriteLine($"Order ID {o.OrderID} Does'nt exist");
                }
            }
            Console.WriteLine("\nPress < ESC > to return to Main Menu");
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
            }
        }


        [Obsolete]


        public void ViewStaffRevenueInDay()
        {
            Console.Clear();
            Console.WriteLine(@"                                    ┌─────────────────────────────────────────────────────────────────────────────────────┐
                                    │                                                                                     │
                                    │ ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗ │
                                    │ ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║ │
                                    │ ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩ │
                                    │─────────────────────────────────────────────────────────────────────────────────────│
                                    │                         [VIEW STAFF REVENUE IN DAY]                                 │
                                    └─────────────────────────────────────────────────────────────────────────────────────┘");

            int staffId = loginStaff1!.StaffID;

            if (loginStaff1 != null)
            {
                if (loginStaff1.StaffID == staffId)
                {
                    decimal totalRevenue = 0;
                    List<Order> staffOrders = oBL.GetOrdersByStaffID(staffId);
                    var revenueTable = new Table();
                    revenueTable.AddColumn("Order ID");
                    revenueTable.AddColumn("Order Time ");
                    revenueTable.AddColumn(new TableColumn("[yellow]Total Amount[/]").RightAligned());

                    foreach (Order order in staffOrders)
                    {

                        decimal orderTotal = 0;
                        foreach (Book book in order.BooksList)
                        {

                            orderTotal += book.Price * book.Amount;
                        }

                        revenueTable.AddRow("" + order.OrderID, "" + order.OrderDate, "" + $"[yellow]{FormatCurrencyToVND(orderTotal)}[/]");
                        totalRevenue += orderTotal;
                    }

                    AnsiConsole.Render((revenueTable).Centered());
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"                                                Total Revenue for Staff {loginStaff1.StaffName}: {FormatCurrencyToVND(totalRevenue / 2)}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("                                               Press < ENTER > to return to Main Menu.");
                    Console.ForegroundColor = ConsoleColor.White;
                    var input = Console.ReadKey(true).Key;
                    if (input == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        MainMenu();

                    }
                    else
                    {
                        Console.WriteLine("INVALID CHOOSE!!!");
                    }
                }
                else
                {
                    Console.WriteLine($"Staff with ID {staffId} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid Staff ID.");
            }
        }


        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Regular expression pattern to match a phone number with '0' as the first digit
            string pattern = @"^0\d{9,10}$";

            return Regex.IsMatch(phoneNumber, pattern);
        }



        public string FormatCurrencyToVND(decimal amount)
        {
            CultureInfo culture = new CultureInfo("vi-VN"); // Vietnamese culture
            string formattedAmount = string.Format(culture, "{0:C0}", amount); // C0 format specifier for currency without decimal places

            formattedAmount = formattedAmount.Replace(culture.NumberFormat.CurrencySymbol, "VNĐ");

            return formattedAmount;
        }

    }
}
