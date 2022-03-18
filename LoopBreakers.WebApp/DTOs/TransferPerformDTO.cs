using LoopBreakers.DAL.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace LoopBreakers.WebApp.DTOs
{
    public class TransferPerformDTO
    {
        [Required]
        [MinLength(28)]
        [MaxLength(28)]
        public string Iban { get; set; }
        [Required]
        [Display(Name = "reciverFirstname", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "reciverLastname", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string LastName { get; set; }
        public string FromId { get; set; }
        public TransferType Type { get; set; }
        [Required]
        [Range(0.01, 20000, ErrorMessage = "The amount must be between 0.01 and 20000")]

        [Display(Name = "amount", ResourceType = typeof(Resources.DTOs.DTOs))]
        public decimal Amount { get; set; }
        [Display(Name = "createdDate", ResourceType = typeof(Resources.DTOs.DTOs))]
        public DateTime Created { get; set; }
        [Required]
        [Display(Name = "reference", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string Reference { get; set; }
        [Display(Name = "currency", ResourceType = typeof(Resources.DTOs.DTOs))]
        public Currency Currency { get; set; }
    }
}
