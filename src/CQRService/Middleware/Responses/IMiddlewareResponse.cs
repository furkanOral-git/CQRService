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
    public interface IMiddlewareDataResponse : IMiddlewareResponse
    {
        public bool IsDataTypeOf<TResponse>();
    }

}