using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.Entities;
using CQRService.MiddlewareContainer.Factories.Abstract;

namespace CQRService.MiddlewareContainer.Factories.Concrete
{
    internal class ServiceInstanceFactory : BaseFactory, IServiceInstanceFactory
    {
        private static IServiceInstanceFactory _instance;
        private ServiceInstanceFactory()
        {

        }
        public static IServiceInstanceFactory GetFactory()
        {
            if (_instance is null)
            {
                _instance = new ServiceInstanceFactory();
            }
            return _instance;
        }



        public ServiceInstance GetServiceInstance(Guid id)
        {
            var serviceInstance = Services.Instances.SingleOrDefault(i => i.InstanceId == id);
            if (serviceInstance is null)
            {
                serviceInstance = new ServiceInstance
                (
                    id
                );
                Services.Instances.Add(serviceInstance);
                
            }
            return serviceInstance;
        }

    }
}