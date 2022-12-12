using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;

namespace CQRService.Runtime.Interceptors
{
    public static class RequestInterceptorExtensions
    {
        public static InterceptorResult CreateInterceptorResult(this RequestInterceptor interceptor, string message = "", object? data = null)
        {
            InterceptorResult result = new InterceptorResult
            (
                interceptor.GetType().Name,
                message,
                data
            );

            return result;
        }
        public static ErrorResult CreateErrorResult(this RequestInterceptor interceptor, string title, Exception e, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            var error = interceptor.ExceptionHandler.CreateErrorResult
             (
                 title,
                 e,
                 interceptor.GetType().Name,
                 status
             );

            return error;
        }
    }
}