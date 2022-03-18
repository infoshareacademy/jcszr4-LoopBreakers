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
        
        [Display(Name="firstname", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string FirstName { get; set; }
        [Display(Name = "lastname", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string LastName { get; set; }
        [Display(Name = "displayName", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string DisplayName { get; set; }
        [Display(Name = "lastname", ResourceType = typeof(Resources.DTOs.DTOs))]

        public string FromId { get; set; }
        [Display(Name = "transferType", ResourceType = typeof(Resources.DTOs.DTOs))]
        public TransferType Type { get; set; }
        [Display(Name = "amount", ResourceType = typeof(Resources.DTOs.DTOs))]
        public decimal Amount { get; set; }
        [Display(Name = "createdDate", ResourceType = typeof(Resources.DTOs.DTOs))]
        public DateTime Created { get; set; }
        [Display(Name = "reference", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string Reference { get; set; }
        [Display(Name = "currency", ResourceType = typeof(Resources.DTOs.DTOs))]
        public Currency Currency { get; set; }
    }
}
