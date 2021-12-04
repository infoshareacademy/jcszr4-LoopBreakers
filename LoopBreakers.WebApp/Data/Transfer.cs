using LoopBreakers.Logic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Data
{
    [Table("Transfers")]
    public class Transfer
    {
        public int Id { get; set; }
        public string Iban { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FromId { get; set; }
        public TransferType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created { get; set; }
        public string Reference { get; set; }
        public Currency Currency { get; set; }
    }
}
