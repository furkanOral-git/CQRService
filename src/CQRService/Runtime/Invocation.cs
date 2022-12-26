using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CQRService.Entities.BaseInterfaces;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.Entities.Middleware;
using CQRService.ExceptionHandling.RuntimeExceptions;
using CQRService.Runtime.Interceptors;

namespace CQRService.Runtime
{
    internal sealed record Invocation : IInterceptionAfter, IInterceptionBefore, IInterceptionException, IInterceptionSuccess
    {
        public object? Request { get; init; }
        public MethodInfo? HandleMethod { get; init; }
        public object? HandlerObject { get; init; }
        public OperationResult Results { get; init; }
        public InterceptorResultStack ResultStack { get; init; }
        public ErrorStack ErrorStack { get; init; }
        private IRuntimeServiceProvider _serviceProvider;

        public Invocation(object? request, object? handler, MethodInfo? handleMethod, IRuntimeServiceProvider serviceProvider, ErrorStack erStack, InterceptorResultStack reStack)
        {
            Request = request;
            HandlerObject = handler;
            HandleMethod = handleMethod;
            Results = new OperationResult();
            _serviceProvider = serviceProvider;
            ErrorStack = erStack;
            ResultStack = reStack;
        }
        public void Invoke()
        {
            var response = HandleMethod?.Invoke(HandlerObject, new object[] { Request, ErrorStack, ResultStack });
            if (response is not null)
            {
                Results.Response = response;
            }
            Results.IsReturned = true;
        }

        public TRequest GetRequestForControl<TRequest>()
        where TRequest : class, IRequestQueryBase, new()
        {
            return Request as TRequest ?? throw new UnaccessedRequestException(RuntimeExceptionMessages.UnaccessedRequestExceptionMessage);
        }

        public TSource? GetService<TSource>() where TSource : class
        {
            var handlerName = this.HandlerObject?.GetType().Name;
            return _serviceProvider.GetServiceOnRuntime(typeof(TSource), callingTarget: handlerName) as TSource ?? null;
        }
    }
}