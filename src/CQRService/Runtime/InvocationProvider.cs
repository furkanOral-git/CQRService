using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.DistributionControllers;

namespace CQRService.Runtime
{
    internal class InvocationProvider
    {
        public static Invocation CreateInvocation(InvocationArguments args, IRuntimeDistributionController serviceProvider)
        {
            var invocation = new Invocation(
                args.Request,
                args.Handler,
                args.HandleMethod,
                serviceProvider
            );
            return invocation;
        }

    }
}