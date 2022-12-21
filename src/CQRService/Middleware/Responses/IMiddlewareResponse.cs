using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService.Middleware.Responses
{
    public interface IMiddlewareResponse
    {
        public bool HasData();
        public IErrorStackAccessor ErrorStack { get; }
        public IResultStackAccessor ResultStack { get; }

    }
    public interface IMiddlewareSuccessDataResponse : IMiddlewareResponse
    {
        public bool IsDataTypeOf<TResponse>();
    }
    public interface IMiddlewareSuccessResponse : IMiddlewareResponse
    {

    }
    public interface IMiddlewareErrorResponse : IMiddlewareResponse
    {

    }

}