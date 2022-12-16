using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.Entities;
using CQRService.MiddlewareContainer.Factories.Abstract;

namespace CQRService.MiddlewareContainer.Factories.Concrete
{
    internal class ServiceRegisterFactory : BaseFactory, IServiceRegisterFactory
    {
        private static IServiceRegisterFactory _instance;
        private ServiceRegisterFactory()
        {

        }
        public static IServiceRegisterFactory GetFactory()
        {
            if (_instance is null)
            {
                _instance = new ServiceRegisterFactory();
            }
            return _instance;
        }
        public ServiceRegister GetServiceRegister(Guid instanceId, Type sourceType, Type impType, RegistrationType registerType, out bool IsItNewProduction)
        {
            var serviceRegister = Services.RegisteredTypes.SingleOrDefault(s => s.InstanceId == instanceId);
            if (serviceRegister is null)
            {
                serviceRegister = new ServiceRegister
                (
                    instanceId,
                    sourceType,
                    impType,
                    registerType
                );
                Services.RegisteredTypes.Add(serviceRegister);
                IsItNewProduction = true;
                return serviceRegister;
            }
            IsItNewProduction = false;
            return serviceRegister;
        }

        public void UpdateServiceRegister(ServiceRegister register)
        {
            var serviceRegister = Services.RegisteredTypes.First(r => r.InstanceId == register.InstanceId);
            serviceRegister.UpdateImplementationType(register.ImplementationType);
            serviceRegister.UpdateRegistrationType(register.RegistrationType);
        }
    }
}