using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.Entities;
using CQRService.MiddlewareContainer.Factories.Abstract;

namespace CQRService.MiddlewareContainer.Factories.Concrete
{
    internal class ServiceInstanceFactory : BaseFactory, IServiceInstanceFactory
    {
        private readonly object _lockObject;
        private static IServiceInstanceFactory? _instance;
        private ServiceInstanceFactory()
        {
            _lockObject = new object();
        }
        public static IServiceInstanceFactory GetFactory()
        {
            if (_instance is null)
            {
                _instance = new ServiceInstanceFactory();
            }
            return _instance;
        }

        public ServiceInstance GetServiceInstance(Guid id, Guid providerRequestId)
        {
            lock (_lockObject)
            {
                bool IsExist = Services.Instances.Any(i => i.InstanceId == id);
                if (!IsExist)
                {
                    var serviceInstance = new ServiceInstance
                    (
                        id,
                        providerRequestId
                    );
                    Services.Instances.Add(serviceInstance);
                    return serviceInstance;
                }
                
            }
            return Services.Instances.Single(i => i.InstanceId == id);
        }


    }
}