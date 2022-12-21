using System.Net;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService.Middleware.Responses.SuccessResults
{
    public class MiddlewareSuccessDataResponse : MiddlewareBaseResponse, IMiddlewareDataResponse
    {
        public object Data { get; init; }

        public IErrorStackAccessor ErrorStack => this.ErrorStack;
        public IResultStackAccessor ResultStack => this.ResultStack;

        public MiddlewareSuccessDataResponse(object data, string title) : base(true, title, HttpStatusCode.OK)
        {
            Data = data;
        }

        public bool HasData()
        {
            return true;
        }

        public bool IsDataTypeOf<TResponse>()
        {
            if (Data.GetType() == typeof(TResponse))
            {
                return true;
            }
            return false;
        }
    }
}