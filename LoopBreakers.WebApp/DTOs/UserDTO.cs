using LoopBreakers.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Display(Name = "Identity number")]
        public string IdentityNumber { get; set; }
        public bool IsActive { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public int Age { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Company { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Registered { get; set; }
        public string Iban { get; set; }
        public DateTime Created { get; set; }
    }
}
