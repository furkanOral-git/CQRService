using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace CQRService.MiddlewareContainer.Entities
{
    internal class ServiceInstance : ContainerEntity
    {
        public object Instance { get; set; }
        public Guid RequestId { get; internal set; }

        public ServiceInstance(Guid id, Guid requestId)
        {
            InstanceId = id;
            RequestId = requestId;
        }

    }
}