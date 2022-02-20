using LoopBreakers.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.ReportModule.Models
{
    public class TransferReportDTO
    {
        public DateTime Created { get; set; }
        public Currency Currency { get; set; }
        public Decimal Amount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
