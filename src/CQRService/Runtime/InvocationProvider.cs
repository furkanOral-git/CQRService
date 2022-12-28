using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.ExceptionHandling;
using CQRService.MiddlewareContainer;

namespace CQRService.Runtime
{
    internal class InvocationProvider
    {
        public static Invocation CreateInvocation(InvocationArguments args, IDiServiceProvider serviceProvider, ErrorStack exStack, InterceptorResultStack reStack)
        {
            var invocation = new Invocation(
                args.Request ?? null,
                args.Handler ?? null,
                args.HandleMethod ?? null,
                serviceProvider,
                exStack,
                reStack
            );
            return invocation;
        }

    }
}