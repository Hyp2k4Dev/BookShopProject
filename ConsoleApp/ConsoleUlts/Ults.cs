using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BL;
using Persistence;
using UI;

namespace Utilities
{
    public class Ults
    {
        ConsoleUI consoleUI = new ConsoleUI();
        string[] LoginMenu = { "| Login", "| Exit" };
        string[] mainMenu = { "| Create Order", "| Payment", "| Exit" };
        string[] coMenu = { "|Show All Items ", "|Search By Book Name ", "|Get By Book Id ", "|Back To Main Menu" };
        BookBL bookBL = new BookBL();
        Staff? orderStaff;
        CustomerBL customerBL = new CustomerBL();
        StaffBL staffBL = new StaffBL();
        OrderBL orderBL = new OrderBL();
        List<Book>? lst;
        int LoginChoice = 0;

        public void Bonjour()
        {

            do
            {
                consoleUI.Line();
                LoginChoice = consoleUI.Menu(@"
╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩
", LoginMenu);
                switch (LoginChoice)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        // ProgressAsync();
                        break;
                }
            } while (LoginChoice != LoginMenu.Length);
        }


        public void Login()
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
                switch (mainMenuChoice)
                {
                    case 1:
                        CreateOrder();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Invalid User Name/Password ");
                consoleUI.PressAnyKeyToContinue();
            }
        }
        public void CreateOrder()
        {
            int bookId;
            consoleUI.centreLine();
            consoleUI.Title(@"
╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔╦╗╔═╗╔╗╔╦ ╦
║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ║║║║╣ ║║║║ ║
╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╩ ╩╚═╝╝╚╝╚═╝
");
            lst = bookBL.GetAll();
            consoleUI.PrintBooks(lst);
            bool ShowBooks = false;
            do
            {
                if (ShowBooks == true) Console.WriteLine("Invalid input");
                Console.Write("Choose id to add product to order: ");
                _ = int.TryParse(Console.ReadLine(), out bookId);
                if (bookId < 0 || bookId > lst.Count()) ShowBooks = true;
            } while (bookId < 0 || bookId > lst.Count());
        }
    }
}