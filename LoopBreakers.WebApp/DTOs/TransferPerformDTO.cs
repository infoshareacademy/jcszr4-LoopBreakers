using LoopBreakers.DAL.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace LoopBreakers.WebApp.DTOs
{
    public class TransferPerformDTO
    {
        [Display(Name ="User Suranme")]
        [Required]
        public string UserSurname { get; set; }
        [Required]
        [MinLength(26)]
        [MaxLength(26)]
        public string Iban { get; set; }

        [Display(Name = "Reciever First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Reciever Last Name")]
        public string LastName { get; set; }
        public string FromId { get; set; }
        public TransferType Type { get; set; }
        [Range(0.1, 20000, ErrorMessage = "Kwota musi byc w zakresie od 0.1 do 20000")]
        public decimal Amount { get; set; }

        public DateTime Created { get; set; }
        public string Reference { get; set; }
        public Currency Currency { get; set; }
    }
}
