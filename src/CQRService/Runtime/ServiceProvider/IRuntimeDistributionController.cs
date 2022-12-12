using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.DistributionControllers;

namespace CQRService.Runtime
{
    internal interface IRuntimeDistributionController : IDistributionController
    {
         internal protected object GetServiceOnRuntime(Type sourceType);
    }
}