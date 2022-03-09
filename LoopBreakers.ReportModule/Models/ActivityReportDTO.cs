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
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
