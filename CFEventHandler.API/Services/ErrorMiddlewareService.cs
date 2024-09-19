using CFEventHandler.Exceptions;
using CFEventHandler.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CFEventHandler.API.Services
{
    /// <summary>
    /// Error middleware service
    /// </summary>
    internal class ErrorMiddlewareService
    {        
        private readonly RequestDelegate _next;

        public ErrorMiddlewareService(RequestDelegate next)
        {            
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }
        
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            try
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                
                var returnObject = GetReturnObject(exception, context.Response.StatusCode);

                return context.Response.WriteAsync(JSONUtilities.SerializeToString(returnObject, JSONUtilities.DefaultJsonSerializerOptions));                
            }
            catch (Exception ex)
            {
                // This has been seen trying to set Response.ContentType. Setting other properties (E.g. Setting response body)
                // may fail also and so we just throw the original exception.              

                throw exception;    // Original exception
            }
        }
   
        private static ProblemDetails GetReturnObject(Exception exception, int statusCode)
        {
            switch (exception)
            {                
                case GeneralException generalException:
                    return new ProblemDetails
                    {
                        Status = statusCode,
                        Title = generalException.Message
                    };                
                case ApplicationException exception1:
                    {
                        var applicationException = exception1;
                        return new ProblemDetails
                        {
                            Status = statusCode,
                            Title = applicationException.Message
                        };
                    }
                default:
                    // Assume Exception.Message is not human friendly
                    return new ProblemDetails
                    {
                        Status = statusCode,
                        Title = "There was an unexpected error",
                        Detail = exception.Message
                    };
            }
        }
    }
}
