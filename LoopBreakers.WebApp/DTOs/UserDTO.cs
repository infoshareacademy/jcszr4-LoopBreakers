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

        [Required]
        [Range(0,99999999)]
        public decimal Balance { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        [Range(18, 130, ErrorMessage = "Age must be between 18 and 130")]
        public int Age { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }
        public string Company { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [MinLength(3)]
        public string Address { get; set; }
        public DateTime Registered { get; set; }

        [Required]
        [MinLength(28)]
        [MaxLength(28)]
        public string Iban { get; set; }
        public DateTime Created { get; set; }
    }
}
