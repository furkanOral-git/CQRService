using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.MiddlewareContainer
{
    internal interface IDiServiceProvider
    {
        public TService GetService<TService>(Guid providerRequestId) where TService : class;
        public Guid NewRequestId();
        


    }
}