using Spectre.Console;
using Persistence;


namespace UI
{
    class UI
    {
        public int Menu(string? title, string[] item)
        {
            Console.Clear();
            int i = 0;
            int choice;
            if (title != null)
            {
                Title(title);
            }
            for (i = 0; i < item.Count(); i++)
            {
                Console.WriteLine(" " + (i + 1) + "." + item[i]);
            }
            do
            {
                Console.Write("#YOUR CHOICE: ");
                int.TryParse(Console.ReadLine(), out choice);
            }
            while (choice <= 0 || choice > item.Count());
            return choice;
        }
        public void Title(string title)
        {
            Console.Clear();
            Console.WriteLine(" " + title);
        }
    }
}