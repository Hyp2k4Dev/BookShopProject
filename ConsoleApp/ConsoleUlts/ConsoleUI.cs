using Persistence;
using Spectre.Console;

namespace UI
{
    public class ConsoleUI
    {
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
            }
            while (choice <= 0 || choice > menuBooks.Count());
            return choice;
        }

        public void Title(string title)
        {
            Console.WriteLine(title);
        }

        public void ProgressAsync()
        {
            AnsiConsole.Progress().StartAsync(async ctx =>
               {
                   // Define tasks
                   var task1 = ctx.AddTask("[green]Progress[/]");
                   // var task2 = ctx.AddTask("Done!!!");

                   while (!ctx.IsFinished)
                   {
                       // Simulate some work
                       await Task.Delay(20);

                       // Increment
                       task1.Increment(4.5);
                       // task2.Increment(2);
                       // Console.Clear();
                   }
               });
        }
        public void PrintBooks(List<Book> lst)
        {
            foreach (var book in lst)
            {
                PrintBooks(lst);
            }
        }
        public void Line()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void centreLine()
        {
            Console.WriteLine("              ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━                ");
        }
    }
}