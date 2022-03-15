using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.DTOs
{
    public class TransferViewDTO
    {
        public IEnumerable<TransferDTO> Transfer { get; set; }
        public SearchViewModel SearchFilter { get; set; }
    }
}
