using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CQRService.Entities.BaseInterfaces;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.Entities.Middleware;
using CQRService.ExceptionHandling;
using CQRService.ExceptionHandling.RuntimeExceptions;
using CQRService.MiddlewareContainer.DistributionControllers;
using CQRService.Runtime.Interceptors;

namespace CQRService.Runtime
{
    internal sealed class Invocation : IInterceptionAfter, IInterceptionBefore, IInterceptionException, IInterceptionSuccess
    {
        public object Request { get; init; }
        public MethodInfo HandleMethod { get; init; }
        public object HandlerObject { get; init; }
        public OperationResult Results { get; init; }
        private IExceptionHandler _handler;
        private IRuntimeDistributionController _serviceProvider;

        public Invocation(object request, object handler, MethodInfo handleMethod, IRuntimeDistributionController serviceProvider)
        {
            Request = request;
            HandlerObject = handler;
            HandleMethod = handleMethod;
            Results = new OperationResult();
            _serviceProvider = serviceProvider;
            _handler = (IExceptionHandler)_serviceProvider.GetServiceOnRuntime(typeof(IExceptionHandler));
        }
        public void Invoke()
        {
            var response = HandleMethod.Invoke(HandlerObject, new object[] { Request });
            if (response is not null)
            {
                Results.Response = response;
            }
            Results.IsReturned = true;
        }
        public void ReturnAsResponse(object returnValue)
        {
            var type = HandleMethod.ReturnType;
            var returnType = returnValue.GetType();
            if (type == returnType)
            {
                Results.Response = returnValue;
                Results.IsReturned = true;
            }
            else
            {
                throw new ReturnValueTypeDidNotMatch(RuntimeExceptionMessages.ReturnValueTypeDidNotMatch);
            }
        }
        public void Return(InterceptorResult interceptorResult)
        {
            Results.AspectResults.ToList().Add(interceptorResult);
        }
        public void ThrowAndExit(ErrorResult error)
        {
            ContinueWith(error);
            throw new ExitFromProcess(error);
        }
        public TRequest GetRequestForControl<TRequest>()
        where TRequest : class, IRequestQueryBase, new()
        {
            return (TRequest)Request;
        }
        public void ContinueWith(ErrorResult e)
        {
            _handler.ContinueWith(e);
            Results.IsOperationSuccess = false;
        }

        public TSource GetService<TSource>() where TSource : class
        {
            return (TSource)_serviceProvider.GetServiceOnRuntime(typeof(TSource));
        }
    }
}