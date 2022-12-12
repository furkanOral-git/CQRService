using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.Entities;

namespace CQRService.MiddlewareContainer.InstanceControllers
{
    internal interface IInstanceController
    {
        
        public void CreateServiceInstanceClass(Guid instanceId, object instance);
        public void UpdateServiceInstanceClass(Guid instanceId, object instance);
        public bool TryGetServiceInstanceClass(Guid id, out ServiceInstance service);


    }
}