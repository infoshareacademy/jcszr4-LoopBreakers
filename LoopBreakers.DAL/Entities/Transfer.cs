using LoopBreakers.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LoopBreakers.DAL.Entities
{
    [Table("Transfers")]
    public class Transfer : Entity
    {
        public string Iban { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FromId { get; set; }
        public TransferType Type { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public Currency Currency { get; set; }
    }
}
