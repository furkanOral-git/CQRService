using System.Net;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService.Middleware.Responses.SuccessResults
{
    public class MiddlewareSuccessResponse : MiddlewareBaseResponse, IMiddlewareResponse
    {


        public MiddlewareSuccessResponse(string title) : base(true, title, HttpStatusCode.OK)
        {

        }

        public IErrorResultStack ErrorStack => this.ErrorStack;
        public IInterceptorResultStack ResultStack => this.ResultStack;

        public bool HasData()
        {
            return false;
        }

        bool IMiddlewareResponse.HasData()
        {
            throw new NotImplementedException();
        }
    }
}