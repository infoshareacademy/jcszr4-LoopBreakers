using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.DAL;
using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.DAL.Entities;

namespace LoopBreakers.WebApp.Helpers
{
    public class AppException : Exception
    {
        private readonly ILogger _logger;
        private readonly IBaseRepository<AppError> _errorsRepository;

        public AppException(ILogger logger, IBaseRepository<AppError> errorsRepository) : base()
        {
            _logger = logger;
            _errorsRepository = errorsRepository;
        }
        public AppException(ILogger logger, string message) : base(message) 
        {
            _logger = logger;
            AppError error = new AppError();
            error.Created = DateTime.UtcNow;
            error.Source = base.Source;
            error.AppMessage = message;
            error.ExceptionMessage = base.Message;
            _errorsRepository.Create(error);
        }
        public AppException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
