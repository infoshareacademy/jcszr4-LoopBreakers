using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.DTOs
{
    public class RecipientViewDTO
    {
        public IEnumerable<RecipientDTO> Recipient { get; set; }
        public SearchViewModel SearchFilter { get; set; }
    }
}
