using System.Net;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService.Middleware.Responses
{
    public abstract class MiddlewareBaseResponse : IMiddlewareResponse
    {
        public string Title { get; init; }
        public bool IsSuccess { get; init; }
        public HttpStatusCode Status { get; init; }
        public ErrorStack ErrorStack { get; internal set; }
        public InterceptorResultStack ResultStack { get; internal set; }

        IErrorStackAccessor IMiddlewareResponse.ErrorStack => ErrorStack;
        IResultStackAccessor IMiddlewareResponse.ResultStack => ResultStack;

        public MiddlewareBaseResponse(bool isSuccess, string title, HttpStatusCode status)
        {
            IsSuccess = isSuccess;
            Title = title;
            Status = status;
        }

        public virtual bool HasData() { return default; }

        bool IMiddlewareResponse.HasData()
        {
            throw new NotImplementedException();
        }
    }
}