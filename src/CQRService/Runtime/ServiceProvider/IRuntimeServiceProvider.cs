using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer;

namespace CQRService.Runtime
{
    internal interface IRuntimeServiceProvider : IDiServiceProvider
    {
        internal object? GetServiceOnRuntime(Type sourceType, string callingTarget = "");
    }
}