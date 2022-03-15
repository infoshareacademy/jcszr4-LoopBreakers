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
        [Display(Name = "Receiver First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Receiver Last Name")]
        public string LastName { get; set; }
        public string FromId { get; set; }
        public TransferType Type { get; set; }
        [Required]
        [Range(0.01, 20000, ErrorMessage = "The amount must be between 0.01 and 20000")]
        public decimal Amount { get; set; }
        public DateTime Created { get; set; }
        [Required]
        public string Reference { get; set; }
        public Currency Currency { get; set; }
    }
}
