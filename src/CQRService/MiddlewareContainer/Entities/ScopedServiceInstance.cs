using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.MiddlewareContainer.Entities
{
    internal class ScopedServiceInstance : ServiceInstance
    {
        public Guid ScopeId { get; init; }

        private ScopedServiceInstance(Guid instanceId) : base(instanceId)
        {
            InstanceId = instanceId;
        }
        public ScopedServiceInstance(Guid instanceId, Guid scopeId) : this(instanceId)
        {
            ScopeId = scopeId;
        }
    }
}