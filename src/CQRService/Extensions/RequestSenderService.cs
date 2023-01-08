using CQRService.Entities.BaseInterfaces;
using CQRService.Middleware;
using CQRService.Middleware.Responses;
using CQRService.Middleware.Responses.SuccessResults;
using CQRService.Middleware.States;
using CQRService.Middleware.States.Concrete;
using CQRService.Runtime;

namespace CQRService.RequestSenderService
{
    public static class RequestSenderService
    {
        private static RequestMiddleware _middleware;
        static RequestSenderService()
        {
            _middleware = RequestMiddleware.Get();
        }
        public static IMiddlewareResponse Send<TEntity>(this IRequestQueryBase<TEntity> request)
        where TEntity : class, new()
        {
            var arg = new StateArguments
            (
                new InvocationArguments()
                {
                    Request = request,
                    RequestType = request.GetType()
                }
            );

            var middlewareRequest = _middleware.CreateNewRequest(_middleware.Provider);
            middlewareRequest.TransitionTo(new İnitialState(arg));
            var response = middlewareRequest.GetRequestResponse();
            middlewareRequest.End();
            return response;
        }
        public static bool TryGetData<TResponse>(this IMiddlewareResponse response, out TResponse? data)
        {
            data = default;
            if (response.HasData())
            {
                var castedResponse = (MiddlewareSuccessDataResponse)response;
                var isCorrect = castedResponse.IsDataTypeOf<TResponse>();
                if (isCorrect)
                {
                    data = (TResponse)castedResponse.Data;
                    return true;
                }
                return false;
            }
            return false;
        }









    }

}