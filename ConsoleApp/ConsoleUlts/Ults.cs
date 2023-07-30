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
    public class Ults
    {
        ConsoleUI consoleUI = new ConsoleUI();
        string[] mainMenu = { ". CREATE ORDER ", ". LOGOUT" };
        string[] coMenu = { ". SEARCH BOOK BY NAME ", ". SEARCH BOOK BY ISBN ", ". ADD TO ORDER", ". PAYMENT", ". BACK TO MAIN MENU" };
        BookBL bBL = new BookBL();
        Staff? loginStaff;
        CustomerBL cBL = new CustomerBL();
        StaffBL staffBL = new StaffBL();
        OrderBL oBL = new OrderBL();
        List<Book>? lst;

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
                else
                {
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("INVALID USER NAME OR PASSWORD !! PLEASE ENTER TRUE YOUR ACCOUNT !");
                    Console.ResetColor();
                    consoleUI.PressAnyKeyToContinue();
                }
            }
        }
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
        public void SearchBookByName()
        {
            Console.WriteLine("CLICK <ENTER> TO DISPLAY ALL BOOK OR INPUT NAME OF BOOK YOU ARE SEARCHING FOR OR PRESS ESCAPE TO BACK TO CREATE ORDER MENU!!! ");
            string n = Console.ReadLine() ?? "";

            lst = bBL.GetByName(n);
            ShowBookName($"{n}", lst);

        }
        static void ShowBookName(string title, List<Book> lst)
        {
            var table = new Table();
            table.AddColumns("ID     ", "NAME     ", "CATEGORY  ", " PUBLISHING YEAR ", " DESCRIPTION ", " AUTHOR  ", "    PUBLISHER     ", "  PRICE   ", "AMOUNT ");
            if (lst.Count() > 0)
            {
                foreach (Book b in lst)
                {
                    if (b.BookStatus == 1)
                    {
                        {
                            table.AddRow("" + b.BookID, "" + b.BookName, "" + b.BookCategory!.CategoryName, "" + b.PublishYear, "" + b.Description, "" + b.BookAuthor!.AuthorName,
                                        "" + b.BookPublisher!.PublisherName, "" + b.UnitPrice, "" + b.Amount);
                        }
                    }
                    else
                    {
                        table.AddEmptyRow();
                    }
                }
                Console.WriteLine("                                                           BOOK FOUND", Console.ForegroundColor = ConsoleColor.Red);
                Console.ResetColor();
                AnsiConsole.Write(table);
                Console.WriteLine("ENTER THE BOOK ID TO ADD TO ORDER: ");
                int addOrder;
                foreach (Book book in lst)
                {
                    if (Int32.TryParse(Console.ReadLine(), out addOrder))
                    {
                        Console.WriteLine("ADD BOOK " + book.BookName + " SUCCESS");
                    }
                    else
                    {
                        Console.WriteLine("UNKNOWN BOOK TO ADD IN ");
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("INVALID ID PLEASE ENTER VALID ID");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(" BOOK NOT FOUND ", Console.ForegroundColor = ConsoleColor.Red);
                Console.ResetColor();
            }
        }
        public void PressEsc()
        {
            while (true)
            {
                Console.ReadKey(true);
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("You pressed Escape!");
                    CreateOrder();
                    break;
                }
            }
        }
    }
}