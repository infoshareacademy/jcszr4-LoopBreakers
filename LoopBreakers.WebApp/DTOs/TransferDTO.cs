using LoopBreakers.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.DTOs
{
    public class TransferDTO
    {
        public int Id { get; set; }
        public string Iban { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string FromId { get; set; }
        public TransferType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created { get; set; }
        public string Reference { get; set; }
        public Currency Currency { get; set; }

    }
}
