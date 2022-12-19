using System.Net;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;

namespace CQRService.Middleware.Responses
{
    public abstract class MiddlewareBaseResponse
    {
        public string Title { get; init; }
        public bool IsSuccess { get; init; }
        public HttpStatusCode Status { get; init; }
        public ErrorStack Errors { get; set; }
        public InterceptorResultStack AspectResults { get; set; }

        public MiddlewareBaseResponse(bool isSuccess, string title, HttpStatusCode status)
        {
            IsSuccess = isSuccess;
            Title = title;
            Status = status;
        }
    }
}