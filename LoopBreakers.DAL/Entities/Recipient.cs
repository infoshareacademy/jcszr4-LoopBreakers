using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBreakers.DAL.Entities
{
    public class Recipient
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Iban { get; set; }
    }
}
