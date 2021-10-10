using System;
using System.Collections.Generic;

namespace LoopBreakers.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int chosenOption;
            int menuOptionsCount;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to Bank transfer application!");
                Console.WriteLine("_____________________________________");
                List<string> menuOptions = new List<string>();
                menuOptions.Add("1. Find user by name.");
                menuOptions.Add("2. Find transfer by date.");
                menuOptions.Add("3. Find transfer by name and date.");
                menuOptions.Add("4. Add new bank transfer.");
                menuOptions.Add("5. Add new clint.");
                menuOptions.Add("6. Edit client.");
                menuOptions.Add("7. Exit.");

                menuOptionsCount = menuOptions.Count;

                foreach (var option in menuOptions)
                {
                    Console.WriteLine($"{option}");
                }

                Console.WriteLine("_____________________________________");
                Console.WriteLine();
                Console.Write("Enter your selection: ");

                GetChosenOption(out chosenOption, menuOptionsCount);
                Console.WriteLine();
                Console.WriteLine($"Your chose: \t{menuOptions[chosenOption - 1]}");

                switch (chosenOption)
                {
                    case 1:
                        // Find user by name();
                        break;
                    case 2:
                        // Find transfer by date();
                        break;
                    case 3:
                        // Find transfer by name and date();
                        break;
                    case 4:
                        // Add new bank transfer();
                        break;
                    case 5:
                        // Add new clint();
                        break;
                    case 6:
                        // Edit client();
                        break;
                }
            } while (chosenOption < menuOptionsCount);
        }

        private static int GetChosenOption(out int chosenOption, int menuOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out chosenOption))
            {
                Console.Write("Wrong value! Enter your selection: ");
                GetChosenOption(out chosenOption, menuOptionsCount);
            }
            else if (chosenOption > menuOptionsCount || chosenOption <= 0)
            {
                Console.Write("Wrong value! Enter your selection: ");
                GetChosenOption(out chosenOption, menuOptionsCount);
            }
            return (chosenOption);
        }
    }
}

