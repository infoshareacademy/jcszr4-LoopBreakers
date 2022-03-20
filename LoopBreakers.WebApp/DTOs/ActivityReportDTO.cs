using LoopBreakers.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.DTOs
{
    public class ActivityReportDTO
    {
        [Required]
        [Display(Name = "createdDate", ResourceType = typeof(Resources.DTOs.DTOs))]
        public DateTime Created { get; set; }
        [Required]
        [Display(Name = "description", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string Description { get; set; }
        [Required]
        [Display(Name = "firstname", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "lastname", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "email", ResourceType = typeof(Resources.DTOs.DTOs))]
        public string Email { get; set; }
        [Required]
        [Display(Name = "activityName", ResourceType = typeof(Resources.DTOs.DTOs))]
        public ActivityEvents ActivityType { get; set; }
    }
}
