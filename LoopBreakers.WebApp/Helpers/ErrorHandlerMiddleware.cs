using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.DAL.Entities;

namespace LoopBreakers.WebApp.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context, IBaseRepository<AppError> errorsRepository)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                AppError err = new AppError()
                {
                    Created = DateTime.UtcNow,
                    Source = error.Source,
                    StatusCode = context.Response.StatusCode,
                    RequestPath = context.Request.PathBase,
                    Method = context.Request.Method,
                    ExceptionMessage = error.Message
            };
                await errorsRepository.Create(err);


                await response.WriteAsync(result);
            }
        }
    }
}
