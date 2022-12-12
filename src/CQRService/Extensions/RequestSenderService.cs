using System.Linq.Expressions;
using System.Text.Json;
using CQRService.Entities.BaseInterfaces;
using CQRService.Entities.ExceptionHandling;
using CQRService.Middleware;
using CQRService.Middleware.Responses;
using CQRService.Middleware.Responses.ErrorResults;
using CQRService.Middleware.Responses.SuccessResults;
using CQRService.Middleware.States;
using CQRService.Middleware.States.Concrete;
using CQRService.Runtime;

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
        public static bool TryGetErrorResultByCatcher<T>(this IMiddlewareResponse response, out ErrorResult? error)
        where T : class
        {

            if (response.TryGetErrorResultBase(e => e.CatcherType == nameof(T), out error))
            {
                return true;
            }
            return false;
        }
        private static bool TryGetErrorResultBase(this IMiddlewareResponse response, Expression<Func<ErrorResult, bool>> filter, out ErrorResult? error)
        {
            var castedResponse = (MiddlewareErrorResponse)response;
            var errors = castedResponse.Errors;
            if (errors.Any(filter.Compile()))
            {
                error = errors.First(filter.Compile());
                return true;
            }
            error = null;
            return false;
        }
        private static bool TryGetErrorResultsBase(this IMiddlewareResponse response, Expression<Func<ErrorResult, bool>> filter, out ErrorResult[]? errors)
        {
            var castedResponse = (MiddlewareErrorResponse)response;
            var errorss = castedResponse.Errors;
            if (errorss.Any(filter.Compile()))
            {
                errors = errorss.Where(filter.Compile()).ToArray();
                return true;
            }
            errors = null;
            return false;
        }
        public static bool TryGetErrorResultsByCatcher<T>(this IMiddlewareResponse response, out ErrorResult[]? errors)
        {
            if (response.TryGetErrorResultsBase(e => e.CatcherType == nameof(T), out errors))
            {
                return true;
            }
            return false;
        }
        public static bool TryGetErrorResultByException<TException>(this IMiddlewareResponse response, out ErrorResult? error)
        where TException : Exception
        {
            if (response.TryGetErrorResultBase(e => e.ExceptionType == nameof(TException), out error))
            {
                return true;
            }
            return false;
        }
        public static ErrorResult[] GetErrors(this IMiddlewareDataResponse response)
        {
            var castedResponse = (MiddlewareErrorResponse)response;
            return castedResponse.Errors;
        }

        
    
    
    
    }

}