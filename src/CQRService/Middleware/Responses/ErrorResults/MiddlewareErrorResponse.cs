using System.Net;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService.Middleware.Responses.ErrorResults
{
    public class MiddlewareErrorResponse : MiddlewareBaseResponse, IMiddlewareErrorResponse
    {
        public MiddlewareErrorResponse(string title, HttpStatusCode status) : base(false, title, status)
        {

        }


        public override bool HasData()
        {
            return false;
        }
    }
}