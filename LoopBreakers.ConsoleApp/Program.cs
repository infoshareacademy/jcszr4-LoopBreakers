using System;
using System.Threading.Tasks;
using LoopBreakers.Logic.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LoopBreakers.Logic;
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
                List<string> menuOptions = new List<string>
                {
                    "1. Find user by name.",
                    "2. Find transfer by date.",
                    "3. Find transfer by name and date.",
                    "4. Add new bank transfer.",
                    "5. Add new clint.",
                    "6. Edit client.",
                    "7. Add recipient.",
                    "8. Exit."
                };

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
                        Console.Clear();
                        Console.WriteLine("Add Recipient\n");

                        Console.Write("Type first name: ");
                        var firstName = GetTextWithoutNumbers(2, 20);

                        Console.Write("Type last name: ");
                        var lastName = GetTextWithoutNumbers(2, 20);

                        Console.Write("Type address: ");
                        var address = GetText(8, 40);

                        Console.Write("Type Iban: ");
                        var iban = GetTextIban();

                        Recipient newRecipient = new Recipient(firstName, lastName, address, iban);
                        usersRepository.AddRecipient(newRecipient);

                        List<Recipient> listOfRecipients = new List<Recipient>();
                        listOfRecipients = usersRepository.GetRecipient;
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

        private static string GetText( int minLenght, int maxLenght)
        {
            string textFromUser = Console.ReadLine();
            if (textFromUser.Length < minLenght || textFromUser.Length > maxLenght)
            {
                Console.Write($"Wrong value (min: {minLenght}, max: {maxLenght} sign). Type again: ");
                GetText(minLenght, maxLenght);
            }
            return textFromUser;
        }

        private static string GetTextWithoutNumbers( int minLenght, int maxLenght)
        {
            string textFromUser = GetText(minLenght, maxLenght);
            bool TextWithNumer = false;
            foreach (char sign in textFromUser)
            {
                if (char.IsDigit(sign))
                {
                    TextWithNumer = true;
                    break;
                }
            }

            if (TextWithNumer)
            {
                Console.Write($"Wrong value. Type again without numbers: ");
                GetTextWithoutNumbers( minLenght, maxLenght);
            }
            return textFromUser;
        }

        private static string GetTextIban()
        {
            string iban = GetText(28, 28);
            if (!iban.ToUpper().StartsWith("PL"))
            {
                Console.Write("Wrong range - full polish iban has 28 characters, iban without country code at the beginning has 26. So range should be 28. Type again: ");
                GetTextIban();
            }
            return iban;
        }
    }
}

