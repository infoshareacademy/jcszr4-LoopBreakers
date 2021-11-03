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
                Console.Write("Type number of recipient to remove: ");
                Program.GetChosenOption(out int choosenRecipient, 1, listOfRecipientsToRemove.Count);
                usersRepository.RemoveRecipient(choosenRecipient);
                Console.Write("\nChosen recipient was removed.");
            }
        }
    }
}