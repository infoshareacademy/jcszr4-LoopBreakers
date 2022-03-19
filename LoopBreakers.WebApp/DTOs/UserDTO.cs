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

        [Display(Name = "identityNumber", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string IdentityNumber { get; set; }
        [Display(Name = "isActive", ResourceType = typeof(Resources.DTOs.DTOs))]
        public bool IsActive { get; set; }

        [Required]
        [Range(0,99999999)]
        [Display(Name = "balance", ResourceType = typeof(Resources.DTOs.DTOs))]
        public decimal Balance { get; set; }

        [Required]
        [Display(Name = "currency", ResourceType = typeof(Resources.DTOs.DTOs))]
        public Currency Currency { get; set; }

        [Required]
        [Range(18, 130, ErrorMessage = "Age must be between 18 and 130")]
        [Display(Name = "age", ResourceType = typeof(Resources.DTOs.DTOs))]
        public int Age { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "firstname", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "lastname", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "gender", ResourceType = typeof(Resources.DTOs.DTOs))]
        public Gender Gender { get; set; }
        [Display(Name = "company", ResourceType = typeof(Resources.DTOs.DTOs))]
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
