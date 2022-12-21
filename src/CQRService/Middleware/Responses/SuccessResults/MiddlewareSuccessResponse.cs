using System.Net;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService.Middleware.Responses.SuccessResults
{
    public class MiddlewareSuccessResponse : MiddlewareBaseResponse, IMiddlewareSuccessResponse
    {


        public MiddlewareSuccessResponse(string title) : base(true, title, HttpStatusCode.OK)
        {

        }

        public override bool HasData()
        {
            return false;
        }

        
    }
}