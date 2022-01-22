using LoopBreakers.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.DTOs
{
    public class TransferDTO
    {
        public int Id { get; set; }
        public string Iban { get; set; }
        [Display(Name="First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
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
