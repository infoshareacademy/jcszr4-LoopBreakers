using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBreakers.DAL.Entities
{
    public class AppError : Entity
    {
        public string Source { get; set; }
        public string Method { get; set; }

        public int StatusCode { get; set; }
        public string RequestPath { get; set; }
        public string ExceptionMessage { get; set; }
        public string UserId { get; set; }
    }
}
