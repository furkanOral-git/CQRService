using System.Net;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.Entities.Middleware;
using CQRService.ExceptionHandling;
using CQRService.Middleware.Responses;
using CQRService.Middleware.Responses.ErrorResults;
using CQRService.Middleware.Responses.SuccessResults;
using CQRService.Runtime;

namespace CQRService.Middleware.States.Concrete
{

    internal sealed class MiddlewareResponseState : MiddlewareState
    {
        public MiddlewareResponseState(StateArguments args) : base(args)
        {

        }
        public override void Main()
        {
            var results = this._arguments.GetOperationResult();

            IMiddlewareResponse response = null;
            if (results.IsOperationSuccess)
            {
                if (results.Response is not null)
                {
                    response = CreateSuccessDataResponse(results.Response, _resultStack);
                }
                else
                {
                    response = CreateSuccessResponse(_resultStack);
                }
            }
            else
            {
                response = CreateErrorResponse(_errorStack);
            }

            _middleware.SetMiddlewareResponse(response);
            _middleware.ClearState();
        }
        private MiddlewareErrorResponse CreateErrorResponse(ErrorStack results)
        {
            var response = new MiddlewareErrorResponse("Failed Request", HttpStatusCode.BadRequest);
            response.Errors = results;
            return response;
        }
        private MiddlewareSuccessResponse CreateSuccessResponse(InterceptorResultStack result)
        {
            var response = new MiddlewareSuccessResponse("Succeed Request");
            response.AspectResults = result;
            return response;
        }
        private MiddlewareSuccessDataResponse CreateSuccessDataResponse(object data, InterceptorResultStack results)
        {
            var response = new MiddlewareSuccessDataResponse(data, "Succeed Request");
            response.AspectResults = results;
            return response;
        }
    }

}