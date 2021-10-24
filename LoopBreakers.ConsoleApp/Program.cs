using System;
using System.Threading.Tasks;
using LoopBreakers.Logic.Data;
using System.Collections;
using System.Collections.Generic;
using LoopBreakers.Logic.Static;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

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
                menuOptions.Add("7. Add fovourite client.");
                menuOptions.Add("8. Exit.");

                menuOptionsCount = menuOptions.Count;

                foreach (var option in menuOptions)
                {
                    Console.WriteLine($"{option}");
                }

                Console.WriteLine("_____________________________________");
                Console.WriteLine();
                Console.Write("Enter your selection: ");

                int minOptionMenu = 1;
                GetChosenOption(out chosenOption, minOptionMenu,  menuOptionsCount);
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
                    case 7:
                        // Add fovourite client();
                        Console.Clear();
                        Console.Write("Introduce the surname of user which you wish to find: ");
                        var entryName2 = Console.ReadLine();

                        int id = 1;
                        List<User> FoundedUsers = new List<User>();
                        if (TemporaryCollections.Users.Any())
                        {
                            foreach (var user in Search.NameSearch(entryName2))
                            {
                                Console.WriteLine($"\n{id}. {user.FirstName} {user.LastName}\r\nAddress: {user.Address}\r\nAge: {user.Age}");
                                id++;
                                FoundedUsers.Add(user);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No users in the scope");
                        }

                        int index;
                        var save=-1;
                        if (FoundedUsers.Count > 1)
                        {
                            Console.Write("\nType numer of user to add this item to fovourite user: ");

                            GetChosenOption(out index, 1, FoundedUsers.Count);
                            Console.Write($"If you want add user {FoundedUsers[index - 1].FirstName.ToUpper()} {FoundedUsers[index - 1].LastName.ToUpper()} to list of fovourite users press \"y\": ");
                            save = char.Parse(Console.ReadLine());
                            if (save == 'y')
                                TemporaryCollections.FovouriteUsers.Add(FoundedUsers[index - 1]);
                        }
                        else if (FoundedUsers.Count == 1)
                        {
                            Console.Write($"\nIf you want add user {FoundedUsers[0].FirstName.ToUpper()} {FoundedUsers[0].LastName.ToUpper()} to list of fovourite users press \"y\", if not press any key: ");
                            GetChosenOption(out index, 1, FoundedUsers.Count);
                            if (save == 'y')
                                TemporaryCollections.FovouriteUsers.Add(FoundedUsers[0]);
                        }
                        else
                        {
                            Console.WriteLine("User not founded in database.");
                        }
                        


                        Console.WriteLine("\nList of fovourite users:");
                        foreach (var user in TemporaryCollections.FovouriteUsers)
                        {
                            Console.WriteLine($"{user.FirstName} {user.LastName}");
                        }



                        break;
                }
                Console.WriteLine("\nEnter any key to return");
                Console.ReadKey();

            } while (chosenOption < menuOptionsCount);
        }

        private static int GetChosenOption(out int chosenOption, int minOption, int maxOption)
        {
            if (!int.TryParse(Console.ReadLine(), out chosenOption))
            {
                Console.Write("Wrong value! Only numbers: ");
                GetChosenOption(out chosenOption, minOption, maxOption);
            }
            else if (chosenOption > maxOption || chosenOption < minOption)
            {
                Console.Write("Wrong value! Enter your selection: ");
                GetChosenOption(out chosenOption, minOption, maxOption);
            }
            Console.Clear();
            return (chosenOption);
        }
    }
}

