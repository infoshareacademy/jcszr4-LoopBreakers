using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.DTOs
{
    public class AppErrorDTO
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
    }
}
