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
        string[] mainMenu = { "| Create Order", "| Logout" };
        string[] coMenu = { "| Search By Book Name ", "| Get By ISBN ", "| Payment", "| Back To Main Menu" };
        BookBL bBL = new BookBL();
        Staff? orderStaff;
        CustomerBL cBL = new CustomerBL();
        StaffBL staffBL = new StaffBL();
        OrderBL oBL = new OrderBL();
        List<Book>? lst;

        public void Login()
        {
            var table = new Table().Centered();
            while (true)
            {
                Console.WriteLine(@"            
                                  ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
                                  ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
                                  ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩
");
                consoleUI.Title(@"          
                                                                  ╦  ╔═╗╔═╗╦╔╗╔
                                                                  ║  ║ ║║ ╦║║║║
                                                                  ╩═╝╚═╝╚═╝╩╝╚╝
");
                orderStaff = staffBL.Login();
                Console.Clear();
                table.BorderColor(Color.NavajoWhite3);
                table.AddColumn("STAFF USING: " + orderStaff!.StaffName);
                Console.ResetColor();
                AnsiConsole.Write(table);
                if (orderStaff != null)
                {
                    int mainMenuChoice = consoleUI.Menu(@"
                                  ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
                                  ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
                                  ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩
", mainMenu);
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
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid User Name/Password ");
                    consoleUI.PressAnyKeyToContinue();
                }
            }
        }
        public void CreateOrder()
        {
            var table = new Table().Centered();
            table.BorderColor(Color.NavajoWhite3);
            table.AddColumn("STAFF USING: " + orderStaff!.StaffName);
            Console.ResetColor();
            AnsiConsole.Write(table);
            Console.WriteLine(@"
                                  ╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
                                  ╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
                                  ╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩
");
            int createOrderChoose = consoleUI.Menu(@"   
                                                  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔╦╗╔═╗╔╗╔╦ ╦
                                                  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ║║║║╣ ║║║║ ║
                                                  ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╩ ╩╚═╝╝╚╝╚═╝
", coMenu);
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
            var table = new Table();
            table.AddColumns("ID     ", "NAME     ", "CATEGORY  ", " PUBLISHING YEAR ", " DESCRIPTION ", " AUTHOR  ", "    PUBLISHER     ", "  PRICE   ", "AMOUNT ");
            table.BorderColor(Color.NavajoWhite1);
            Console.WriteLine("Input book Code to search: ");
            int isbn;
            if (Int32.TryParse(Console.ReadLine(), out isbn))
            {
                Book b = bBL.GetBookByISBN(isbn);
                if (b != null)
                {
                    if (b.BookStatus == 1)
                    {
                        table.AddRow("" + b.BookID, "" + b.BookName, "" + b.BookCategory!.CategoryName, "" + b.PublishYear, "" + b.Description, "" + b.BookAuthor!.AuthorName,
                                                "" + b.BookPublisher!.PublisherName, "" + b.UnitPrice, "" + b.Amount);
                    }
                    else
                    {
                        table.AddEmptyRow();
                    }
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("                                               BOOK NOT FOUND WITH CODE:  " + isbn);
                }
                AnsiConsole.Write(table);
            }
            else
            {
                Console.WriteLine("\n    Press Enter key to back menu...");
                Console.ReadLine();
                Console.Clear();

            }

        }
        public void SearchBookByName()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Click <ENTER> to Display All Book or <Input Book Name> you are searching for!!! ");
            string n = Console.ReadLine() ?? "";
            lst = bBL.GetByName(n);
            ShowBookName($"{n}", lst);
        }

        static void ShowBookName(string title, List<Book> lst)
        {
            var table = new Table();
            table.BorderColor(Color.NavajoWhite1);
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
                AnsiConsole.Write(table);
            }
            else
            {
                Console.WriteLine(" No result found!", title, Console.ForegroundColor = ConsoleColor.Red);
            }
        }
        public void EscPress()
        {
            Console.ReadKey();
        }
    }
}
