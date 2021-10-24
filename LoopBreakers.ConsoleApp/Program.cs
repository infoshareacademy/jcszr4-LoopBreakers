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
            var usersRepository = new UsersLocalFileRepository();

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
                        var matchingUsers = usersRepository.GetUsersWithSurnameMatchingFilter(entryName1);
                        if (!matchingUsers.Any())
                        {
                            Console.WriteLine($"No matching users in the scope for surname {entryName1}");
                        }
                        foreach (var user in matchingUsers)
                        {
                            Console.WriteLine(
                                $"\n{user.FirstName} {user.LastName}\r\nBalance: {user.Balance} {user.Currency}\r\nAddress: {user.Address}\r\nAge: {user.Age}\r\nCompany: {user.Company}\r\nE-mail: {user.Email}\r\nGender: {user.Gender}\r\nId: {user.Id}\r\nisActive?: {user.IsActive}\r\nPhone Number: {user.Phone}\r\nDate of Reg: {user.Registered}\n");
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
                        // Add favorite client();
                        Console.Clear();
                        Console.Write("Introduce the surname of user which you wish to find: ");
                        var entryName2 = Console.ReadLine();

                        int id = 1;
                        List<User> usersFound = new List<User>();
                        if (usersRepository.GetUsers.Any())
                        {
                            foreach (var user in usersRepository.GetUsersWithSurnameMatchingFilter(entryName2))
                            {
                                Console.WriteLine($"\n{id}. {user.FirstName} {user.LastName}\r\nAddress: {user.Address}\r\nAge: {user.Age}");
                                id++;
                                usersFound.Add(user);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No users in the scope");
                        }

                        int index;
                        var save=-1;
                        if (usersFound.Count > 1)
                        {
                            Console.Write("\nType number of user to add this item to favorite user: ");

                            GetChosenOption(out index, 1, usersFound.Count);
                            Console.Write($"If you want add user {usersFound[index - 1].FirstName.ToUpper()} {usersFound[index - 1].LastName.ToUpper()} to list of favorite users press \"y\": ");
                            save = char.Parse(Console.ReadLine());
                            if (save == 'y')
                                usersRepository.AddFavoriteUser((usersFound[index - 1]));
                        }
                        else if (usersFound.Count == 1)
                        {
                            Console.Write($"\nIf you want add user {usersFound[0].FirstName.ToUpper()} {usersFound[0].LastName.ToUpper()} to list of favorite users press \"y\", if not press any key: ");
                            save = char.Parse(Console.ReadLine());
                            if (save == 'y')
                                usersRepository.AddFavoriteUser(usersFound[0]);
                        }
                        else
                        {
                            Console.WriteLine("User not founded in database.");
                        }
                        


                        Console.WriteLine("\nList of favorite users:");
                        foreach (var user in usersRepository.GetFavoriteUsers)
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

