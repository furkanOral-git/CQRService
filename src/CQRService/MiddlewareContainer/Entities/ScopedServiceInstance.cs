using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.MiddlewareContainer.Entities
{
    internal class ScopedServiceInstance : ServiceInstance
    {
        public string Target { get; init; }

        private ScopedServiceInstance(Guid instanceId) : base(instanceId)
        {
            InstanceId = instanceId;
        }
        public ScopedServiceInstance(Guid instanceId, string target) : this(instanceId)
        {
            Target = target;
        }
    }
}