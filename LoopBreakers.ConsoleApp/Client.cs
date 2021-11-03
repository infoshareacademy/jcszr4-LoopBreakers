using LoopBreakers.Logic;
using LoopBreakers.Logic.Data;
using LoopBreakers.Logic.Enums;
using System;
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
                Console.WriteLine($"Transfer will be send from: {client.FirstName} {client.LastName}");

                Transfer transfer = new Transfer();
                Console.Write("\nType recipient IBAN: ");
                transfer.Iban = Program.GetTextIban();
                Console.Write("\nType recipient first name: ");
                transfer.FirstName = Program.GetTextWithoutNumbers(2, 20);
                Console.Write("\nType recipient last name: ");
                transfer.LastName = Program.GetTextWithoutNumbers(2, 20);
                Console.Write("\nType reference: ");
                transfer.Reference = Program.GetText(1, 100);
                Console.Write("\nType amout: ");
                transfer.Amount = Program.GetDecimal();
                Console.WriteLine("\nSelect currency: ");
                transfer.Currency = (Currency)Program.GetEnum<Currency>().chosenOption;
                transfer.Created = DateTime.Now;
                transfer.Type = TransferType.Payment;

                usersRepository.AddTransfer(transfer);
                Console.WriteLine("\nNew transfer created!");
            }
            else
            {
                Console.WriteLine("There are no clients in the database");
            }
        }
    }
}