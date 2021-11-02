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
            Console.WriteLine("List of your recipients:\n");
            int id = 1;
            Console.WriteLine($"| {"ID",3} | {"FIRST NAME",15} | {"LAST NAME",15} | {"IBAN",30} | {"ADDRESS",35} |");
            foreach (var recipient in listOfRecipients)
            {
                Console.WriteLine($"| {id,3} | {recipient.FirstName,15} | {recipient.LastName,15} | {recipient.Iban,30} | {recipient.Address,35} |");
                id++;
            }
        }


    }
}