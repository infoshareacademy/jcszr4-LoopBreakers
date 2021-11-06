using System;
using LoopBreakers.Logic.Data;
using System.Collections.Generic;
using System.Linq;
using LoopBreakers.Logic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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
                Console.WriteLine("==================================================================");
                Console.WriteLine("           Welcome to Bank transfer application!\n");
                Console.WriteLine("You can menage clients, make transfers and check transfers history");
                Console.WriteLine("==================================================================");
                Console.WriteLine("Created by: Małgorzata Łukasik, Marcel Olkowski, ");
                Console.WriteLine("            Rafał Szczerba, Tadeusz Trojan, Bartłomiej Zieliński");
                Console.WriteLine("==================================================================");
                Console.WriteLine("\nSelect an option:\n");
                List<string> menuOptions = new List<string>
                {
                    "  1. Find client by name.",
                    "  2. Find transfer by name and date.",
                    "  3. Find transfer by date.",
                    "  4. Add new bank transfer.",
                    "  5. Add new client.",
                    "  6. Edit client.",
                    "  7. Add new recipient.",
                    "  8. Edit recipient.",
                    "  9. Remove recipient.",
                    " 10. Transfer to recipient.",
                    " 11. Exit."
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
                GetChosenOption(out chosenOption, minOptionMenu, menuOptionsCount);
                Console.WriteLine($"Your chose: \t{menuOptions[chosenOption - 1]}");

                switch (chosenOption)
                {
                    case 1:
                        Client.SearchTransfersBySurname(usersRepository);
                        break;
                    case 2:
                        Client.SearchTransfersBySurnameAndDate(usersRepository);
                        break;
                    case 3:
                        Client.SearchTransfersByDate(usersRepository);
                        break;
                    case 4:
                        Client.SendTransfer(usersRepository);
                        break;
                    case 5:
                        usersRepository.AddUser(Client.AddNew());
                        break;
                    case 6:
                        Client.Edit(usersRepository);
                        break;
                    case 7:
                        Recipient.AddNewRecipient(usersRepository);
                        break;
                    case 8:
                        Recipient.EditRecipient(usersRepository);
                        break;
                    case 9:
                        Recipient.RemoveRecipient(usersRepository);
                        break;
                    case 10:
                        Recipient.SendTransfer(usersRepository);
                        break;
                }
                Console.Write("\nEnter any key to return:");
                Console.ReadKey();

            } while (chosenOption < menuOptionsCount);
        }
      
        public static int GetChosenOption(out int chosenOption, int minOption, int maxOption)
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
            return (chosenOption);
        }

        public static string GetText(int minLenght, int maxLenght)
        {
            string textFromUser = Console.ReadLine().Trim();
            if (textFromUser.Length < minLenght || textFromUser.Length > maxLenght)
            {
                if (minLenght == maxLenght)
                {
                    Console.Write($"Wrong value ({maxLenght} characters are required). Type again: ");
                }
                else
                {
                    Console.Write($"Wrong value (min: {minLenght}, max: {maxLenght} sign). Type again: ");
                }
                textFromUser = GetText(minLenght, maxLenght);
            }
            return textFromUser;
        }

        public static int GetNumber(int minValue, int maxValue)
        {
            string textFromUser = Console.ReadLine();
            int intFromUser;
            if (int.TryParse(textFromUser, out intFromUser))
            {
                if ((intFromUser < minValue) || (intFromUser > maxValue))
                {
                    Console.Write($"Value is out of range: {minValue} - {maxValue}. Type again: ");
                    intFromUser = GetNumber(minValue, maxValue);
                }
            }
            else
            {
                Console.Write($"Wrong value. Type again: ");
                intFromUser = GetNumber(minValue, maxValue);
            }
            return intFromUser;
        }

        public static decimal GetDecimal()
        {
            string textFromUser = Console.ReadLine();
            decimal decimalFromUser;
            if (!decimal.TryParse(textFromUser, out decimalFromUser))
            {
                Console.Write($"Wrong value. Type again: ");
                decimalFromUser = GetDecimal();
            }
            return decimalFromUser;
        }

        public static string GetEmail(int minLenght, int maxLenght)
        {
            string textFromUser = GetText(minLenght, maxLenght);
            var emailChecker = new EmailAddressAttribute();

            if (!emailChecker.IsValid(textFromUser))
            {
                Console.Write("Entered e-mail is invalid. Type again: ");
                textFromUser = GetEmail(minLenght, maxLenght);
            }
            return textFromUser;
        }

        public static (string stringFromEnum, int chosenOption) GetEnum<T>()
        {
            string stringFromEnum = "";
            int count = 1;
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine($"{count++}: {item.ToString()}");
            }
            Console.Write("Select an option: ");
            GetChosenOption(out int chosenOption, 1, count - 1);
            stringFromEnum = Enum.GetName(typeof(T), chosenOption - 1);
            return (stringFromEnum, chosenOption - 1);
        }

        public static bool GetBool()
        {
            Console.WriteLine("1: Yes");
            Console.WriteLine("2: No");
            Console.Write("Select an option: ");
            GetChosenOption(out int chosenOption, 1, 2);
            return chosenOption == 1 ? true : false;
        }

        public static string GetTextWithoutNumbers(int minLenght, int maxLenght)
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
                textFromUser = GetTextWithoutNumbers(minLenght, maxLenght);
            }
            return textFromUser;
        }

        public static string GetTextIban()
        {
            string iban = GetText(28, 28);
            if (!iban.ToUpper().StartsWith("PL"))
            {
                Console.Write("Wrong range - full polish iban has 28 characters, iban without country code at the beginning has 26. So range should be 28. Type again: ");
                iban = GetTextIban();
            }
            return iban.ToUpper();
        }
    }
}