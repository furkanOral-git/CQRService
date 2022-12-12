using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace CQRService.MiddlewareContainer.Entities
{
    internal class ServiceInstance
    {
        public Guid InstanceId { get; private set; }
        public object Instance { get; private set; }
        
        public ServiceInstance(Guid id, object instance)
        {
            Instance = instance;
            InstanceId = id;
        }
        public void UpdateInstance(object newInstance)
        {
            Instance = newInstance;
        }
    }
}