using System;
using System.Collections.Generic;
using System.Linq;
using LoopBreakers.ConsoleApp;

namespace LoopBreakers.Logic.Data
{
    public class Recipient
    {
        public static void PrintRecipient(List<RecipientStructure> listOfRecipients)
        {
            Console.WriteLine("List of your recipients:\n");
            int id = 1;
            Console.WriteLine($"| {"ID",3} | {"FIRST NAME",15} | {"LAST NAME",15} | {"IBAN",30} | {"ADDRESS",35} |");
            foreach (var recipient in listOfRecipients)
            {
                Console.WriteLine($"| {id++,3} | {recipient.FirstName,15} | {recipient.LastName,15} | {recipient.Iban,30} | {recipient.Address,35} |");
            }
        }

        public static void AddNewRecipient(UsersLocalFileRepository usersRepository) 
        {
            Console.Clear();
            Console.WriteLine("Add Recipient\n");
            var newRecipient = new RecipientStructure();
            Console.Write("Type first name: ");
            newRecipient.FirstName = Program.GetTextWithoutNumbers(2, 20);

            Console.Write("Type last name: ");
            newRecipient.LastName = Program.GetTextWithoutNumbers(2, 20);

            Console.Write("Type address: ");
            newRecipient.Address = Program.GetText(5, 40);

            Console.Write("Type Iban: ");
            newRecipient.Iban = Program.GetTextIban();

            usersRepository.AddRecipient(newRecipient);
        }

        public static void EditRecipient(UsersLocalFileRepository usersRepository) 
        
        {
            Console.Clear();
            Console.WriteLine("Edit Recipient\n");

            List<RecipientStructure> listOfRecipients = usersRepository.GetRecipient;

            if (!listOfRecipients.Any())
            {
                Console.WriteLine("You don't have any recipients!");
            }
            else
            {
                Recipient.PrintRecipient(listOfRecipients);

                Console.Write("\nType number of recipient to edit:");
                Program.GetChosenOption(out int choosenRecipient, 1, listOfRecipients.Count);
                RecipientStructure recipientToEdit = listOfRecipients[choosenRecipient - 1];

                Console.WriteLine($"\nCurrent first name: {recipientToEdit.FirstName}");
                Console.Write("Type new first name: ");
                var newFirstName = Program.GetTextWithoutNumbers(2, 20);

                Console.WriteLine($"\nCurrent last name: {recipientToEdit.LastName}");
                Console.Write("Type new last name: ");
                var newLastName = Program.GetTextWithoutNumbers(2, 20);

                Console.WriteLine($"\nCurrent address: {recipientToEdit.Address}");
                Console.Write("Type new address: ");
                var newAddress = Program.GetText(5, 40);

                Console.WriteLine($"\nCurrent iban: {recipientToEdit.Iban}");
                Console.Write("Type new iban: ");
                var newIban = Program.GetTextIban();

                usersRepository.EditRecipient(choosenRecipient, newFirstName, newLastName, newAddress, newIban);
            }
        }

        public static void RemoveRecipient(UsersLocalFileRepository usersRepository) 
        {
            Console.Clear();
            Console.WriteLine("Remove recipient\n");
            List<RecipientStructure> listOfRecipientsToRemove = usersRepository.GetRecipient;

            if (!listOfRecipientsToRemove.Any())
            {
                Console.WriteLine("You don't have any recipients!");
            }
            else
            {
                Recipient.PrintRecipient(listOfRecipientsToRemove);
                Console.Write("Type number of recipient to remove: ");
                Program.GetChosenOption(out int choosenRecipient, 1, listOfRecipientsToRemove.Count);
                usersRepository.RemoveRecipient(choosenRecipient);
                Console.Write("\nChosen recipient was removed.");
            }
        }









        internal static void SendTransfer(UsersLocalFileRepository usersRepository)
        {
            Console.Clear();
            Console.WriteLine("Send transfert to recipient\n");
            List<RecipientStructure> listOfRecipientsForTransfer = usersRepository.GetRecipient;
            
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
                int choosenRecipient = 0;
                if (!listOfRecipientsForTransfer.Any())
                {
                    Console.WriteLine("You don't have any recipients!");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    
                    Recipient.PrintRecipient(listOfRecipientsForTransfer);
                    Console.Write("Type number of recipient for transfer: ");
                    Program.GetChosenOption(out  choosenRecipient, 1, listOfRecipientsForTransfer.Count);
                    //string guid = usersRepository.GetGuidOfRecipient(choosenRecipient);
                }

                transfer.FirstName = listOfRecipientsForTransfer[choosenRecipient-1].FirstName;
                transfer.LastName = listOfRecipientsForTransfer[choosenRecipient-1].LastName;
                transfer.Iban = listOfRecipientsForTransfer[choosenRecipient-1].Iban;
                transfer.FromId = listOfRecipientsForTransfer[choosenRecipient-1].Guid.ToString();



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
    }
}