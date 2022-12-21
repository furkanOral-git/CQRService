using System.Net;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService.Middleware.Responses.ErrorResults
{
    public class MiddlewareErrorResponse : MiddlewareBaseResponse, IMiddlewareResponse
    {
        public MiddlewareErrorResponse(string title, HttpStatusCode status) : base(false, title, status)
        {

        }

        public IErrorStackAccessor ErrorStack => this.ErrorStack;
        public IResultStackAccessor ResultStack => this.ResultStack;

        public bool HasData()
        {
            return false;
        }
    }
}