using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.Entities;

namespace CQRService.MiddlewareContainer.InstanceControllers
{
    internal class ContainerInstanceController : BaseContainerController, IInstanceController
    {

        public ContainerInstanceController()
        {
            
        }

        public void CreateServiceInstanceClass(Guid instanceId, object instance)
        {
            var serv = new ServiceInstance(instanceId, instance);
            _container.Instances.Add(serv);
        }
        public void UpdateServiceInstanceClass(Guid instanceId, object instance)
        {
            var service = _container.Instances.Single(i => i.InstanceId == instanceId);
            service.UpdateInstance(instance);
        }
        public bool TryGetServiceInstanceClass(Guid instanceId, out ServiceInstance service)
        {
            bool isExist = _container.Instances.Any(i => i.InstanceId == instanceId);

            if (isExist)
            {
                service = _container.Instances.Single(i => i.InstanceId == instanceId);
                return true;
            }
            service = null;
            return false;
        }


    }
}