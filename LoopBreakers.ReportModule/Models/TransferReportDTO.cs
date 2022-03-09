using LoopBreakers.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.ReportModule.Models
{
    public class TransferReportDTO
    {
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public Currency Currency { get; set; }
        [Required]
        public Decimal Amount { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(2)]
        [Display(Name = "Country code")]
        public string CountryCode { get; set; }
    }
}
