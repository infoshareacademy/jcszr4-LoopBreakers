using LoopBreakers.Logic;
using LoopBreakers.Logic.Data;
using LoopBreakers.Logic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoopBreakers.ConsoleApp
{
    public static class Client
    {
        public static User AddNew()
        {
            var client = new User(); 
            Console.Clear();
            Console.WriteLine("Add new Client\n");
            Console.Write("First name: ");
            client.FirstName = Program.GetTextWithoutNumbers(2, 20);
            Console.Write("Last name: ");
            client.LastName = Program.GetTextWithoutNumbers(2, 20);
            Console.Write("Age: ");
            client.Age = Program.GetNumber(18, 120);
            Console.Write("E-mail: ");
            client.Email = Program.GetEmail(5, 50);
            Console.Write("Company name: ");
            client.Company = Program.GetText(1, 100);
            Console.Write("Phone: ");
            client.Phone = Program.GetText(9, 20);
            Console.Write("Address: ");
            client.Address = Program.GetText(5, 100);
            Console.WriteLine("Select currency:");
            client.Currency = Program.GetEnum<Currency>().stringFromEnum.ToUpper();
            Console.WriteLine("Select gender:");
            client.Gender = (Gender)Program.GetEnum<Gender>().chosenOption;
            Console.WriteLine("Is Client active?:");
            client.IsActive = Program.GetBool();
            Console.Write("Balance: ");
            client.Balance = Program.GetDecimal();

            client.Id = Guid.NewGuid().ToString();
            client.Registered = DateTime.Now;
            Console.WriteLine("\nNew client added!");
            return client;
        }

        public static void Edit(UsersLocalFileRepository usersRepository)
        {
            Console.Clear();
            Console.WriteLine("Edit Client information\n");
            
            if (usersRepository.GetUsers.Any())
            {
                int i = 1;
                usersRepository.GetUsers.ForEach(c => Console.WriteLine($"{i++,-3} {c.FirstName,-15} {c.LastName}"));
                Console.Write("Enter your selection: ");
                Program.GetChosenOption(out int chosenOption, 1, usersRepository.GetUsers.Count());
                chosenOption--;

                User client = usersRepository.GetUsers[chosenOption];

                Console.WriteLine($"\nCurrent first name: {client.FirstName}");
                Console.Write("Type new first name: ");
                client.FirstName = Program.GetTextWithoutNumbers(2, 20);

                Console.WriteLine($"\nCurrent last name: {client.LastName}");
                Console.Write("Type new last name: ");
                client.LastName = Program.GetTextWithoutNumbers(2, 20);

                Console.WriteLine($"\nCurrent age: {client.Age}");
                Console.Write("Type new age: ");
                client.Age = Program.GetNumber(18, 120);

                Console.WriteLine($"\nCurrent e-mail: {client.Email}");
                Console.Write("Type new e-mail: ");
                client.Email = Program.GetEmail(5, 50);

                Console.WriteLine($"\nCurrent company name: {client.Company}");
                Console.Write("Type new company name: ");
                client.Company = Program.GetText(1, 100);

                Console.WriteLine($"\nCurrent phone: {client.Phone}");
                Console.Write("Type new phone: ");
                client.Phone = Program.GetText(9, 20);

                Console.WriteLine($"\nCurrent address: {client.Address}");
                Console.Write("Type new address: ");
                client.Address = Program.GetText(5, 100);

                Console.WriteLine($"\nCurrent currency: {client.Currency.ToUpper()}");
                Console.WriteLine("Select new currency: ");
                client.Currency = Program.GetEnum<Currency>().stringFromEnum.ToUpper();

                Console.WriteLine($"\nCurrent gender: {client.Gender}");
                Console.WriteLine("Select new gender: ");
                client.Gender = (Gender)Program.GetEnum<Gender>().chosenOption;

                Console.WriteLine($"\nCurrent client \"is active\" status: {client.IsActive}");
                Console.WriteLine("Is new status active?: ");
                client.IsActive = Program.GetBool();

                Console.WriteLine($"\nCurrent balance: {client.Balance}");
                Console.Write("Type new balance: ");
                client.Balance = Program.GetDecimal();

                usersRepository.EditUser(client, chosenOption);
                Console.WriteLine("\nClient information updated!");
            }
            else
            {
                Console.WriteLine("There are no clients in the database");
            }
        }

        public static void SendTransfer(UsersLocalFileRepository usersRepository)
        {
            Console.Clear();
            Console.WriteLine("New bank transfer from client:\n");
            if (usersRepository.GetUsers.Any())
            {
                int i = 1;
                usersRepository.GetUsers.ForEach(c => Console.WriteLine($"{i++,-3} {c.FirstName,-15} {c.LastName}"));
                Console.Write("\nEnter your selection: ");
                Program.GetChosenOption(out int chosenOption, 1, usersRepository.GetUsers.Count());
                chosenOption--;

                User client = usersRepository.GetUsers[chosenOption];
                Console.Clear();
                if (client.Balance == 0)
                {
                    Console.WriteLine($"Client balance is 0 {client.Currency}. Transfer can not be created.");
                    return;
                }
                Console.WriteLine($"Transfer will be send from: {client.FirstName} {client.LastName}");
                Console.WriteLine($"Client current balance: {client.Balance} {client.Currency}");

                Transfer transfer = new Transfer();
                Console.Write("\nType recipient IBAN: ");
                transfer.Iban = Program.GetTextIban();
                Console.Write("\nType recipient first name: ");
                transfer.FirstName = Program.GetTextWithoutNumbers(2, 20);
                Console.Write("\nType recipient last name: ");
                transfer.LastName = Program.GetTextWithoutNumbers(2, 20);
                Console.Write("\nType reference: ");
                transfer.Reference = Program.GetText(1, 100);
                do
                {
                    Console.Write($"\nType amout in {client.Currency}: ");
                    transfer.Amount = Program.GetDecimal();
                }
                while (transfer.Amount > client.Balance);
                client.Balance -= transfer.Amount;

                usersRepository.AddTransfer(transfer);
                Console.WriteLine("\nNew transfer created!");
            }
            else
            {
                Console.WriteLine("There are no clients in the database");
            }
        }

        public static void SearchTransfersBySurnameAndDate(UsersLocalFileRepository usersRepository)
        {
            Console.Clear();
            Console.Write("Enter the client surname: ");
            var userSurname = Program.GetText(3, 20);
            var transferList = usersRepository.GetTransfersForUserBySurname(userSurname);
            if (!transferList.Any())
            {
                Console.WriteLine($"\nThere are no transfers for client with surname: {userSurname}\n");
            }
            else
            {
                Console.WriteLine("Choose the period of transfers performed:");
                var monthsOption = new Dictionary<int, string>()
                {
                    { 1, " 1:  1 month"},
                    { 3, " 2:  3 months"},
                    { 6, " 3:  6 months"},
                    { 9, " 4:  9 months" },
                    { 12, " 5: 12 months"}
                };

                int monthOptionsCount = monthsOption.Count;

                foreach (var option in monthsOption)
                {
                    Console.WriteLine($"{option.Value}");
                }
                Console.Write("\nSelect an opiton: ");
                Program.GetChosenOption(out int chosenOption, 1, monthOptionsCount);
                var dateSearch = DateTime.Now.AddMonths(-monthsOption.Keys.ElementAt(chosenOption - 1));
                var transferListByDate = usersRepository.GetTransfersByDate(transferList, dateSearch);
                if (!transferListByDate.Any())
                {
                    Console.WriteLine("There are no transfers in selected period of time!");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Transfers found from date: {dateSearch}:\n");
                    Console.WriteLine($"{"IBAN Number ",-30} | {"Transfer Type",-15} | {"Date ",-20} | Amount");
                    foreach (var item in transferListByDate)
                    {
                        Console.WriteLine($"{item.Iban,-30} | {item.Type,-15} | {item.Created.Date,-20} | {item.Amount}");
                    }
                }
            }
        }
    }
}