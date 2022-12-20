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

        public IErrorResultStack ErrorStack => this.ErrorStack;
        public IInterceptorResultStack ResultStack => this.ResultStack;

        public bool HasData()
        {
            return false;
        }
    }
}