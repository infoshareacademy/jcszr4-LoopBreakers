using System;
using System.Threading.Tasks;
using LoopBreakers.Logic.Data;
using System.Collections;
using System.Collections.Generic;
using LoopBreakers.Logic.Static;
using System.Linq;
using System.Collections.Generic;

namespace LoopBreakers.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {

            TemporaryCollections.InitializeCollections();
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
                Console.WriteLine($"Your chose: \t{menuOptions[chosenOption - 1]}");

                switch (chosenOption)
                {
                    case 1:
                        Console.WriteLine("Introduce the surname of user which you wish to find");
                        var entryName1 = Console.ReadLine();
                        if (TemporaryCollections.Users.Any())
                        {
                            foreach (var user in Search.NameSearch(entryName1))
                            {
                                Console.WriteLine($"\n{user.FirstName} {user.LastName}\r\nBalance: {user.Balance} {user.Currency}\r\nAddress: {user.Address}\r\nAge: {user.Age}\r\nCompany: {user.Company}\r\nE-mail: {user.Email}\r\nGender: {user.Gender}\r\nId: {user.Id}\r\nisActive?: {user.IsActive}\r\nPhone Number: {user.Phone}\r\nDate of Reg: {user.Registered}\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No users in the scope");
                        }
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
                Console.WriteLine("Enter any key to return");
                Console.ReadKey();

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
            Console.Clear();
            return (chosenOption);
        }
    }
}

