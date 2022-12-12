using System.Net;
using CQRService.Entities.Middleware;
using CQRService.Middleware.Responses;
using CQRService.Middleware.Responses.ErrorResults;
using CQRService.Middleware.Responses.SuccessResults;

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
                    response = CreateSuccessDataResponse(results);
                }
                else
                {
                    response = CreateSuccessResponse(results);
                }
            }
            else
            {
                response = CreateErrorResponse(results);
            }

            this._middleware.SetMiddlewareResponse(response);
            this._middleware.ClearState();
        }
        private MiddlewareErrorResponse CreateErrorResponse(OperationResult result)
        {
            var response = new MiddlewareErrorResponse("Failed Request", HttpStatusCode.BadRequest);
            response.AspectResults = result.AspectResults.ToArray();
            response.Errors = result.Errors.ToArray();
            return response;
        }
        private MiddlewareSuccessResponse CreateSuccessResponse(OperationResult result)
        {
            var response = new MiddlewareSuccessResponse("Succeed Request");
            response.AspectResults = result.AspectResults.ToArray();
            response.Errors = result.Errors.ToArray();
            return response;
        }
        private MiddlewareSuccessDataResponse CreateSuccessDataResponse(OperationResult result)
        {
            var response = new MiddlewareSuccessDataResponse(result.Response, "Succeed Request");
            response.AspectResults = result.AspectResults.ToArray();
            response.Errors = result.Errors.ToArray();
            return response;
        }
    }

}