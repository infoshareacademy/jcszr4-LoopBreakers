using System;
using System.Collections.Generic;

namespace LoopBreakers.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bank transfer application!");
            Console.WriteLine("_____________________________________");
            List<string> menuOptions = new List<string>();
            menuOptions.Add("1. Find user by name.");
            menuOptions.Add("2. Find transfer by date.");
            menuOptions.Add("3. Find transfer by name and date.");
            menuOptions.Add("4. Add new bank transfer.");
            menuOptions.Add("5. Exit.");
            
            int menuOptionsCount = menuOptions.Count;

            foreach (var option in menuOptions)
            {
                Console.WriteLine($"{option}");
            }
            Console.WriteLine("_____________________________________");
            Console.WriteLine();
            Console.Write("Enter your selection: ");

            int chosenOption;
            
            GetChosenOption(out chosenOption, menuOptionsCount);
            chosenOption--;
            Console.WriteLine();
            Console.WriteLine($"Your chose: \t{menuOptions[chosenOption]}");

        }

        private static int GetChosenOption(out int chosenOption, int menuOptionsCount)
        {
            if (!Int32.TryParse(Console.ReadLine(), out chosenOption))
            {
                Console.Write("Wrong value! Enter your selection: ");
                GetChosenOption(out chosenOption, menuOptionsCount);
            }
            else if (chosenOption > menuOptionsCount || chosenOption <=0)
            {
                Console.Write("Wrong value! Enter your selection: ");
                GetChosenOption(out chosenOption, menuOptionsCount);
            }

            
            return (chosenOption);
        }
    }
    }

