using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.DTOs
{
    public class ClientViewDTO
    {
        public IEnumerable<UserDTO> Client { get; set; }
        public SearchViewModel SearchFilter { get; set; }
    }
}
