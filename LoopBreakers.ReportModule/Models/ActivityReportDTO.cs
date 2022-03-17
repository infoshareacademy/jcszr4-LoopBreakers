using LoopBreakers.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.ReportModule.Models
{
    public class ActivityReportDTO
    {
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required]
        public ActivityEvents ActivityType { get; set; }
    }
}
