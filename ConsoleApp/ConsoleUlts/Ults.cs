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
            while (true)
            {
                consoleUI.Line();
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
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
", mainMenu);
                    consoleUI.Line();
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
            var table = new Table();
            table.AddColumns("ID     ", "Name     ", "Category  ", " Pyear ", "Description ", "Author           ", "Publisher        ", "Unit Price   ", "Amount ");
            table.BorderColor(Color.PaleGreen1);
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
            var table = new Table();
            table.AddColumns("ID     ", "Name     ", "Category  ", " Pyear ", "Description ", "Author           ", "Publisher        ", "Unit Price   ", "Amount ");
            table.BorderColor(Color.PaleGreen1);
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
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("                                                      BOOK FOUND");
                AnsiConsole.Write(table);
                // else
                // {
                //     Console.WriteLine(" No Books Found.");
                // }
            }
        }
    }
}
