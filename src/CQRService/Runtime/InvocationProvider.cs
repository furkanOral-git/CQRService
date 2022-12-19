using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.ExceptionHandling;

namespace CQRService.Runtime
{
    internal class InvocationProvider
    {
        public static Invocation CreateInvocation(InvocationArguments args, IRuntimeServiceProvider serviceProvider, ErrorStack exStack, InterceptorResultStack reStack)
        {
            var invocation = new Invocation(
                args.Request,
                args.Handler,
                args.HandleMethod,
                serviceProvider,
                exStack,
                reStack
            );
            return invocation;
        }

    }
}