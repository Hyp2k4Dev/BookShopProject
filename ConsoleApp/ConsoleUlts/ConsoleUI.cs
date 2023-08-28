using Persistence;
using Spectre.Console;
using BL;
using System.Globalization;
using Utilities;

namespace UI
{
    public class ConsoleUI
    {
        BookBL bBL = new BookBL();

        public void PressAnyKeyToContinue()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public int Menu(string? title, string[] menuBooks)
        {
            int i = 0;
            int choice;
            if (title != null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Title(title);
            }
            for (i = 0; i < menuBooks.Count(); i++)
            {
                Console.WriteLine(" " + (i + 1) + menuBooks[i]);
            }
            do
            {
                Console.WriteLine("YOUR CHOOSE: ");
                int.TryParse(Console.ReadLine(), out choice);
            } while (choice <= 0 || choice > menuBooks.Count());
            return choice;
        }

        public void Title(string title)
        {
            Console.WriteLine(title);
        }

        public void PrintBooks(List<Book> lst)
        {
            foreach (var book in lst)
            {
                PrintBooks(lst);
            }
        }

        public void centreLine()
        {
            Console.WriteLine(
                "              ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━                "
            );
        }

        [Obsolete]
        public void PrintBooksInShop(List<Book> lst)
        {
            int pageSize = 5;
            int currentPage = 1;
            while (true)
            {
                Order o = new Order();
                var showAllTable = new Table();
                showAllTable.AddColumn("ID");
                showAllTable.AddColumn(new TableColumn("Book Name").Centered());
                showAllTable.AddColumn("Category");
                showAllTable.AddColumn(new TableColumn("Publish Year").Centered());
                showAllTable.AddColumn(new TableColumn("Description").Centered());
                showAllTable.AddColumn("Author");
                showAllTable.AddColumn("Publisher");
                showAllTable.AddColumn(new TableColumn("Price").RightAligned());
                showAllTable.AddColumn(new TableColumn(" Quantity").Centered());
                int startIndex = (currentPage - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, lst.Count - 1);
                for (int i = startIndex; i <= endIndex; i++)
                {
                    showAllTable.AddRow(
                        "" + lst[i].BookID,
                        "" + lst[i].BookName,
                        "" + lst[i].BookCategory!.CategoryName,
                        "" + lst[i].PublishYear,
                        "" + lst[i].Description,
                        "" + lst[i].BookAuthor!.AuthorName,
                        "" + lst[i].BookPublisher!.PublisherName,
                        "" + FormatCurrencyToVND(lst[i].Price),
                        "" + lst[i].Amount
                    );
                }
                AnsiConsole.Render(showAllTable);
                Console.WriteLine(
                    "                                                 Page: "
                        + currentPage
                        + "/"
                        + Math.Ceiling((double)lst.Count / pageSize)
                );
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(
                    "Press '->' to next page or '<-' to previous page or Press 'Spacebar' to Continue or 'BackSpace' to return Main Menu"
                );
                Console.ForegroundColor = ConsoleColor.White;
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (currentPage > 1)
                    {
                        Console.Clear();
                        currentPage--;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (currentPage < Math.Ceiling((double)lst.Count / pageSize))
                    {
                        Console.Clear();
                        currentPage++;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Directing to search book...");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(3000);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Success !!! Press Enter to input code ! ");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Exiting...");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(3000);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Press BackSpace to back Main Menu");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid key, please try again!");
                }
            }
        }

        public string FormatCurrencyToVND(decimal amount)
        {
            CultureInfo culture = new CultureInfo("vi-VN");
            string formattedAmount = string.Format(culture, "{0:C0}", amount);

            formattedAmount = formattedAmount.Replace(culture.NumberFormat.CurrencySymbol, "VND");

            return formattedAmount;
        }
    }
}
