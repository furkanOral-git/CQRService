using System.Linq.Expressions;
using System.Text.Json;
using CQRService.Entities.BaseInterfaces;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.Middleware;
using CQRService.Middleware.Responses;
using CQRService.Middleware.Responses.ErrorResults;
using CQRService.Middleware.Responses.SuccessResults;
using CQRService.Middleware.States;
using CQRService.Middleware.States.Concrete;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService.RequestSenderService
{
    public static class RequestSenderService
    {
        public static IMiddlewareResponse Send<TEntity>(this IRequestQueryBase<TEntity> request)
        where TEntity : class, new()
        {
            RequestMiddleware _middleware = RequestMiddleware.Get();
            var arg = new StateArguments
            (
                new InvocationArguments()
                {
                    Request = request,
                    RequestType = request.GetType()
                }
            );

            _middleware.TransitionTo(new İnitialState(arg));
            return _middleware.GetMiddlewareResponse();
        }
        public static JsonElement GetAsJSON(this IMiddlewareResponse response)
        {
            var jsonElement = JsonSerializer.SerializeToElement(response, typeof(MiddlewareBaseResponse)
            , new JsonSerializerOptions() { WriteIndented = true }
            );
            return jsonElement;
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