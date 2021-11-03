using System;
using System.Threading.Tasks;
using LoopBreakers.Logic.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LoopBreakers.Logic;
using Microsoft.VisualBasic.CompilerServices;
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
                Console.WriteLine("Welcome to Bank transfer application!");
                Console.WriteLine("_____________________________________");
                List<string> menuOptions = new List<string>
                {
                    "1. Find user by name.",
                    "2. Find transfer by date.",
                    "3. Find transfer by name and date.",
                    "4. Add new bank transfer.",
                    "5. Add new client.",
                    "6. Edit client.",
                    "7. Add recipient.",
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
                        // Find transfer by name and date();
                        break;
                    case 4:
                        // Add new bank transfer
                        Client.SendTransfer(usersRepository);
                        break;
                    case 5:
                        // Add new clint
                        usersRepository.AddUser(Client.AddNew());
                        break;
                    case 6:
                        // Edit client
                        Client.Edit(usersRepository);
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

                        break;
                    case 8:
                        // Edit recipient();
                        Console.Clear();
                        Console.WriteLine("Edit Recipient\n");

                        List<Recipient> listOfRecipients = usersRepository.GetRecipient;


                        if (!listOfRecipients.Any())
                        {
                            Console.WriteLine("You don't have any recipients :");
                        }
                        else
                        {
                            Console.WriteLine("List of your recipients:");
                            int id = 1;
                            foreach (var recipient in listOfRecipients)
                            {
                                Console.WriteLine($"{id,3}. {recipient.FirstName,15} {recipient.LastName,20} {recipient.Address,40} {recipient.Iban,30}", id, recipient.FirstName);
                                id++;
                            }

                            Console.Write("Type number of recipient to edit:");
                            int choosenRecipient;
                            GetChosenOption(out choosenRecipient, 1, listOfRecipients.Count);

                            Recipient recipientToEdit = listOfRecipients[choosenRecipient - 1];

                            Console.WriteLine($"\nCurrent first name: {recipientToEdit.FirstName}");
                            Console.Write("Type new first name: ");
                            var newFirstName = GetTextWithoutNumbers(2, 20);

                            Console.WriteLine($"\nCurrent last name: {recipientToEdit.LastName}");
                            Console.Write("Type new last name: ");
                            var newLastName = GetTextWithoutNumbers(2, 20);

                            Console.WriteLine($"\nCurrent address: {recipientToEdit.Address}");
                            Console.Write("Type new address: ");
                            var newAddress = GetText(8, 40);

                            Console.WriteLine($"\nCurrent iban: {recipientToEdit.Iban}");
                            Console.Write("Type new iban: ");
                            var newIban = GetTextIban();

                            usersRepository.EditRecipient(choosenRecipient, newFirstName, newLastName, newAddress,
                                newIban);
                        }

                        break;
                    case 9:
                        Console.Clear();
                        Console.WriteLine("Remove recipient\n");

                        List<Recipient> listOfRecipientsToRemove = usersRepository.GetRecipient;

                        if (!listOfRecipientsToRemove.Any())
                        {
                            Console.WriteLine("You don't have any recipients :");
                        }
                        else
                        {
                            Console.WriteLine("List of your recipients:");
                            int id = 1;
                            foreach (var recipient in listOfRecipientsToRemove)
                            {
                                Console.WriteLine(
                                    $"{id,3}. {recipient.FirstName,15} {recipient.LastName,20} {recipient.Address,40} {recipient.Iban,30}",
                                    id, recipient.FirstName);
                                id++;
                            }

                            Console.Write("Type number of recipient to remove: ");
                            int choosenRecipient;
                            GetChosenOption(out choosenRecipient, 1, listOfRecipientsToRemove.Count);
                            usersRepository.RemoveRecipient(choosenRecipient);
                            Console.Write("Chosen recipient was removed.");
                        }
                        break;
                }
                Console.WriteLine("\nEnter any key to return");
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
            else if (textFromUser.Length < minLenght || textFromUser.Length > maxLenght)
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
            return iban;
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