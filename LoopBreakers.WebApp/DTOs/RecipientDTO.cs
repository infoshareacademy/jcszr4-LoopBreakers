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
        [Required]
        [MinLength(2)]
        [Display(Name = "firstname", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "lastname", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "email", ResourceType = typeof(Resources.DTOs.DTOs))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "address", ResourceType=typeof(Resources.DTOs.DTOs))]
        public string Address { get; set; }

        [Required]
        [MinLength(28)]
        [MaxLength(28)]
        public string Iban { get; set; }
        [Display(Name = "createdDate", ResourceType = typeof(Resources.DTOs.DTOs))]
        public DateTime Created { get; set; }
    }
}
