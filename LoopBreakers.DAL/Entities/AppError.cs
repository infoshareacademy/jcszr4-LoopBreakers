using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBreakers.DAL.Entities
{
    public class AppError : Entity
    {
        public string Source { get; set; }
        public string AppMessage { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
