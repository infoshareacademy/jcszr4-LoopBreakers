using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.DTOs
{
    public class RecipientDTO
    {
        public int Id { get; set; }
        [Display(Name = "First name")] 
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Iban { get; set; }
        public DateTime Created { get; set; }
    }
}
