using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.DTOs
{
    public class SearchViewModel
    {
        public string SearchText { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? EmailSend { get; set; }
        public bool RegisterActivity { get; set; }
        public bool LoginActivity { get; set; }
        public bool TransferActivity { get; set; }
        public string EmailAddress { get; set; }
    }
}
