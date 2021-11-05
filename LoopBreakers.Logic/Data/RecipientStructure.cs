using System;

namespace LoopBreakers.Logic.Data
{
    public class RecipientStructure
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Iban { get; set; }

        public RecipientStructure() { }
        public RecipientStructure(string firstName, string lastName, string address, string iban)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Iban = iban;
            Guid = Guid.NewGuid();
        }
    }
}