using LoopBreakers.DAL.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace LoopBreakers.WebApp.DTOs
{
    public class UserProfileDTO
    {
        public int Id { get; set; }

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
        [Display(Name = "phone", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string Phone { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "address", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string Address { get; set; }
    }
}
