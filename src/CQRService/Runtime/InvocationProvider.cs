using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.Runtime
{
    internal class InvocationProvider
    {
        public static Invocation CreateInvocation(InvocationArguments args, IRuntimeServiceProvider serviceProvider)
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