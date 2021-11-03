using System;
using LoopBreakers.Logic.Data;
using System.Collections.Generic;
using System.Linq;
using LoopBreakers.Logic;
using System.ComponentModel.DataAnnotations;

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
                Console.WriteLine("Crated by: Małgorzata £ukasik, Marcel Olkowski, Rafał Szczerba, Tadeusz Trojan, Bartłomiej Zieliński ");
                Console.WriteLine("\nWelcome to Bank transfer application!");
                Console.WriteLine("_____________________________________");
                List<string> menuOptions = new List<string>
                {
                    "1. Find user by name.",
                    "2. Find transfer by date.",
                    "3. Find transfer by name and date.",
                    "4. Add new bank transfer.",
                    "5. Add new client.",
                    "6. Edit client.",
                    "7. Add new recipient.",
                    "8. Edit recipient.",
                    "9. Remove recipient.",
                    "10. Exit."
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
                                $"\n{user.FirstName} {user.LastName}\r\nBalance: {user.Balance} {user.Currency}\r\nAddress: {user.Address}\r\nAge: {user.Age}\r\nCompany: {user.Company}\r\nE-mail: {user.Email}\r\nGender: {user.Gender}\r\nId: {user.Id}\r\nisActive?: {user.IsActive}\r\nPhone Number: {user.Phone}\r\nDate of Reg: {user.Registered}\r\nIBAN: {user.Iban}\n");
                        }

                        break;
                    case 2:
                        // Find transfer by date();
                        break;
                    case 3:
                        Console.Clear();
                        List<Transfer> foundTransfers;                      
                        Console.WriteLine("Choose the period of transfers performed");
                        Console.WriteLine("1. Last month");
                        Console.WriteLine("2. Last 3 month");
                        Console.WriteLine("3. Last 6 month");
                        Console.WriteLine("4. Custom:");
                        if (!int.TryParse(Console.ReadLine(), out int optionChosed) || optionChosed > 4 || optionChosed < 1)
                        {
                            Console.WriteLine("You introduced wrong value");
                            break;
                        }
                        if (optionChosed >= 1 && optionChosed < 4)
                        {
                            ChoosedTimePeriod(optionChosed, out DateTime startDateSearch, out DateTime EndDateSearch);
                            foundTransfers = usersRepository.SortTransfersByDate(startDateSearch, EndDateSearch);
                            Console.Clear();
                            foreach (var item in foundTransfers)
                            {
                                Console.WriteLine($"Iban: {item.Iban}  ||Type of transfer: {item.Type}    ||    Amount:{item.Amount} {item.Currency.ToString().ToUpper()}  ||  Date of Transfer: {item.Created.Date}");
                            }
                        }
                        else if (optionChosed == 4)
                        {
                            Console.Clear();
                            Console.WriteLine("Introduce start date of transfer period [DD/MM/YYYY]");
                            string startDateIntroduced = Console.ReadLine();
                            Console.WriteLine("Introduce end date of transfer period [DD/MM/YYYY]");
                            string endDateIntroduced = Console.ReadLine();
                            ChoosedTimePeriodCustomed(startDateIntroduced, endDateIntroduced, out DateTime startDateConverted, out DateTime endDateConverted);
                            if (startDateConverted.Date==DateTime.Now.AddDays(1).Date)
                            {
                                Console.Clear();
                                Console.WriteLine("Incorrect format of intoduced start/end date of transfer");
                            }
                            else
                            {
                               foundTransfers = usersRepository.SortTransfersByDate(startDateConverted, endDateConverted);
                               Console.Clear();
                               foreach (var item in foundTransfers)
                               {
                                   Console.WriteLine($"Iban: {item.Iban}  ||Type of transfer: {item.Type}    ||    Amount:{item.Amount} {item.Currency.ToString().ToUpper()}  ||  Date of Transfer: {item.Created.Date}");
                               }
                            }
                        }
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

        private static void ChoosedTimePeriod(int userTransferPeriodOption, out DateTime startPeriod, out DateTime endPeriod)
        {
            if (userTransferPeriodOption == 1)
            {
                startPeriod = DateTime.Now.AddDays(-30);
                endPeriod = DateTime.Now;
            }
            else if (userTransferPeriodOption == 2)
            {
                startPeriod = DateTime.Now.AddDays(-90);
                endPeriod = DateTime.Now;
            }
            else if (userTransferPeriodOption == 3)
            {
                startPeriod = DateTime.Now.AddDays(-180);
                endPeriod = DateTime.Now;
            }
            else
            {
                startPeriod = DateTime.Now.AddDays(1);
                endPeriod = DateTime.Now.AddDays(1);
            }
        }
        private static void ChoosedTimePeriodCustomed(string customedStartDate, string customedEndDate, out DateTime startPeriod, out DateTime endPeriod)
        {
            try
            {
                startPeriod = Convert.ToDateTime(customedStartDate);
                if (startPeriod > DateTime.Now)
                {
                    startPeriod = DateTime.Now;
                } 
                endPeriod = Convert.ToDateTime(customedEndDate);
            }
            catch
            {
                startPeriod = DateTime.Now.AddDays(1);
                endPeriod = DateTime.Now.AddDays(1);
            }
        }
    }
}