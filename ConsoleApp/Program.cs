using System;
using System.Collections.Generic;
using Spectre.Console;
using System.IO;
using System.Text;
using Persistence;
using BL;

namespace ConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            short mainChoose = 0, coChoose;
            string[] mainMenu = { "|Create Order", "|Payment", "|Exit" };
            string[] coMenu = { "|Show All Items ", "|Search By Book Name ", "|Get By Book Id ", "|Back To Main Menu" };
            BookBL bookBL = new BookBL();
            Staff? orderStaff;
            CustomerBL customerBL = new CustomerBL();
            StaffBL staffBL = new StaffBL();
            OrderBL orderBL = new OrderBL();
            List<Book> lst;
            do
            {
                mainChoose = Menu(@"                                     
                                    ╦  ╔═╗╔═╗╦╔╗╔
                                    ║  ║ ║║ ╦║║║║
                                    ╩═╝╚═╝╚═╝╩╝╚╝
", mainMenu);
                orderStaff = staffBL.Login();
                if (orderStaff != null)
                {
                    switch (mainChoose)
                    {
                        case 1:
                            do
                            {
                                coChoose = Menu(@"                  
                  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔╦╗╔═╗╔╗╔╦ ╦
                  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ║║║║╣ ║║║║ ║
                  ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╩ ╩╚═╝╝╚╝╚═╝
", coMenu);
                                {
                                    switch (coChoose)
                                    {
                                        case 1:
                                            Console.Write("\nInput Book Id: ");
                                            int bookId;
                                            if (Int32.TryParse(Console.ReadLine(), out bookId))
                                            {
                                                Book b = bookBL.GetBookById(bookId);
                                                if (b != null)
                                                {
                                                    // Console.WriteLine("Item ID: " + b.ItemId);
                                                    Console.WriteLine("Book Name: " + b.BookName);
                                                    Console.WriteLine("Book Price: " + b.Price);
                                                    Console.WriteLine("Quantity: " + b.Quantity);
                                                    Console.WriteLine("Book Status: " + b.Status);
                                                    Console.WriteLine("Item Description: " + b.Description);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("There is no book with id " + bookId);
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Your Choose is wrong!");
                                            }
                                            Console.WriteLine("\n    Press Enter key to back menu...");
                                            Console.ReadLine();
                                            break;
                                        case 2:
                                            lst = bookBL.GetAll();
                                            if (lst.Count > 0)
                                            {
                                                ShowBooks("-> All items:", lst);
                                            }
                                            Console.WriteLine("\n    Press Enter key to back menu...");
                                            Console.ReadLine();
                                            break;
                                    }
                                }
                            } while (mainChoose != mainMenu.Length);
                            break;
                        case 2:
                            Console.WriteLine("-> New Customer");
                            Console.Write("Customer Name: ");
                            string name = Console.ReadLine() ?? "no name";
                            Console.Write("Customer Address: ");
                            string address = Console.ReadLine() ?? "";
                            Customer c = new Customer { CustomerName = name, CustomerAddress = address };
                            c.CustomerId = customerBL.AddCustomer(c);
                            if (c.CustomerId > 0)
                            {
                                Console.WriteLine($"Add customer completed with customer id {c.CustomerId}");
                            }
                            Console.WriteLine("\n    Press Enter key to back menu...");
                            Console.ReadLine();
                            break;
                        case 3:
                            await AnsiConsole.Progress()
    .StartAsync(async ctx =>
    {
        // Define tasks
        var task1 = ctx.AddTask("[green]Exiting[/]");
        // var task2 = ctx.AddTask("Done!!!");

        while (!ctx.IsFinished)
        {
            // Simulate some work
            await Task.Delay(100);

            // Increment
            task1.Increment(3);
            // task2.Increment(2);
        }
    });
                            break;

                    };
                } while (mainChoose != mainMenu.Length) ;
                static void ShowBooks(string title, List<Book> lst)
                {
                    Console.WriteLine(title);
                    Console.WriteLine(@"+---------+-----------+------------+--------+-------------+------------------+
| book_id | book_name | price | quantity | book_status | book_description |
+---------+-----------+------------+--------+-------------+------------------+");
                    foreach (Book book in lst)
                    {
                        Console.WriteLine("| {0, 7:N0} | {1, -9} | {2, 10:N2} | {3, 6:N0} | {4, -11} | {5, -16} |",
                        book.BookID, book.BookName, book.Price, book.Quantity, book.Status == 1 ? "Active" : "Inactive", book.Description);
                    }
                    Console.WriteLine(@"+---------+-----------+------------+--------+-------------+------------------+");
                }
                static short Menu(string title, string[] menuBooks)
                {
                    var LoginLogo = new Table();
                    LoginLogo.AddColumn(@"
╔╗ ╔═╗╔═╗╦╔═  ╔═╗╦ ╦╔═╗╔═╗  ╔═╗╦═╗╔═╗╔═╗╔╦╗╔═╗  ╔═╗╦═╗╔╦╗╔═╗╦═╗  ╔═╗╦ ╦╔═╗╔╦╗╔═╗╔╦╗
╠╩╗║ ║║ ║╠╩╗  ╚═╗╠═╣║ ║╠═╝  ║  ╠╦╝║╣ ╠═╣ ║ ║╣   ║ ║╠╦╝ ║║║╣ ╠╦╝  ╚═╗╚╦╝╚═╗ ║ ║╣ ║║║
╚═╝╚═╝╚═╝╩ ╩  ╚═╝╩ ╩╚═╝╩    ╚═╝╩╚═╚═╝╩ ╩ ╩ ╚═╝  ╚═╝╩╚══╩╝╚═╝╩╚═  ╚═╝ ╩ ╚═╝ ╩ ╚═╝╩ ╩
");
                    AnsiConsole.Write(LoginLogo);
                    short choose = 0;
                    string line = "=======================================================================================";
                    Console.WriteLine(line);
                    Console.WriteLine(" " + title);
                    Console.WriteLine(line);
                    for (int i = 0; i < menuBooks.Length; i++)
                    {
                        Console.WriteLine(" " + (i + 1) + ". " + menuBooks[i]);
                    }
                    Console.WriteLine(line);
                    do
                    {
                        Console.WriteLine("Click 1 to CREATE ORDER, 2 to PAYMENT, 3 to EXIT APP ");
                        Console.Write("Your choice: ");
                        try
                        {
                            choose = Int16.Parse(Console.ReadLine() ?? "0");
                        }
                        catch
                        {
                            Console.WriteLine("Your Choose is wrong!");
                            continue;
                        }
                    } while (choose <= 0 || choose > menuBooks.Length);
                    return choose;
                }
            }
        }
    }
}


