using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.Entities;

namespace CQRService.MiddlewareContainer.Factories.Abstract
{
    internal interface IServiceInstanceFactory : IFactory<ServiceInstance>
    {
        public ServiceInstance GetServiceInstance(Guid id, Guid providerRequestId);

    }
}