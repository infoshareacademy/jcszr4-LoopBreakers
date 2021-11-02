using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopBreakers.Logic.Data
{
    public class Recipient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Iban { get; set; }

        public Recipient(string firstName, string lastName, string address, string iban)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Iban = iban;
        }

        public static void PrintRecipient(List<Recipient> listOfRecipients)
        {
            Console.WriteLine("List of your recipients:");
            int id = 1;
            Console.WriteLine($"| {"ID",3} | {"FirstName",15} | {"LastName",15} | {"Iban",30} | {"Address",35} |");
            foreach (var recipient in listOfRecipients)
            {
                Console.WriteLine($"| {id,3} | {recipient.FirstName,15} | {recipient.LastName,15} | {recipient.Iban,30} | {recipient.Address,35} |");
                id++;
            }
        }

        public static void AddRecipient(UsersLocalFileRepository usersRepository)
        {
            Console.Clear();
            Console.WriteLine("Add Recipient\n");

            Console.Write("Type first name: ");
            //var firstName = GetTextWithoutNumbers(2, 20);

            Console.Write("Type last name: ");
            //var lastName = GetTextWithoutNumbers(2, 20);

            Console.Write("Type address: ");
            //var address = GetText(5, 40);

            Console.Write("Type Iban: ");
            //var iban = GetTextIban();

            //Recipient newRecipient = new Recipient(firstName, lastName, address, iban.ToUpper());
            //usersRepository.AddRecipient(newRecipient);
        }
    }
}