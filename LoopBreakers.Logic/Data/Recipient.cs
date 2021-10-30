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

        public Recipient(string firstName, string lastName, string address, string iban )
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Iban = iban;
        }
    }
}