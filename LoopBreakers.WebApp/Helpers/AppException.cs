using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Helpers
{
    public class AppException : Exception
    {
        private readonly ILogger _logger;

        public AppException(ILogger logger) : base()
        {
            _logger = logger;
        }
        public AppException(ILogger logger, string message) : base(message) 
        {
            _logger = logger;
            _logger.Information("MAJLA HI");
        }
        public AppException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
