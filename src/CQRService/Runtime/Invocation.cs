using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CQRService.Entities.BaseInterfaces;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.Entities.Middleware;
using CQRService.ExceptionHandling.RuntimeExceptions;
using CQRService.MiddlewareContainer;
using CQRService.Runtime.Interceptors;

namespace CQRService.Runtime
{
    internal sealed record Invocation : IInterceptionAfter, IInterceptionBefore, IInterceptionException, IInterceptionSuccess
    {
        public InvocationArguments Args { get; init; }
        public OperationResult Results { get; init; }
        public InterceptorResultStack ResultStack { get; init; }
        public ErrorStack ErrorStack { get; init; }
        private IDiServiceProvider _serviceProvider;

        public Invocation(InvocationArguments args, IDiServiceProvider serviceProvider, ErrorStack erStack, InterceptorResultStack reStack)
        {
            Args = args;
            Results = new OperationResult();
            _serviceProvider = serviceProvider;
            ErrorStack = erStack;
            ResultStack = reStack;
        }
        public void Invoke()
        {
            var response = Args.HandleMethod?.Invoke(Args.Handler, new object[] { Args.Request, ErrorStack, ResultStack });
            if (response is not null)
            {
                Results.Response = response;
            }
            Results.IsReturned = true;
        }

        public TRequest GetRequestForControl<TRequest>()
        where TRequest : class, IRequestQueryBase, new()
        {
            return Args.Request as TRequest ?? throw new UnaccessedRequestException(RuntimeExceptionMessages.UnaccessedRequestExceptionMessage);
        }

        public TSource? GetService<TSource>() where TSource : class
        {
            return _serviceProvider.GetService<TSource>(Args.RequestId);
        }
    }
}