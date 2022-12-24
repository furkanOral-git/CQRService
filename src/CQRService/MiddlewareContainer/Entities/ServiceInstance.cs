using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace CQRService.MiddlewareContainer.Entities
{
    internal class ServiceInstance : ContainerEntity
    {
        public object? Instance { get; private set; }
        
        public ServiceInstance(Guid id)
        {
            InstanceId = id;
        }
        public void UpdateInstance(object? newInstance)
        {
            Instance = newInstance;
        }
    }
}