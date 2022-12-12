using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.Entities;

namespace CQRService.MiddlewareContainer.RegistrationControllers
{
    internal class ContainerRegistrationController : BaseContainerController, IRegistrationController
    {

        public ContainerRegistrationController()
        {
            
        }

        public ServiceRegister CreateServiceRegisterIfNotExistAndReturn(Guid instanceId, Type sourceType, Type impType, RegistrationType registerType)
        {
            ServiceRegister register;
            if (!TryGetServiceRegister(instanceId, out register))
            {
                register = new ServiceRegister
                (
                    instanceId,
                    sourceType,
                    impType,
                    registerType
                );
                _container.RegisteredTypes.Add(register);
            }
            return register;
        }

        public void UpdateServiceRegister(Guid instanceId, Type impType, RegistrationType registerType)
        {
            var register = _container.RegisteredTypes.Single(i => i.InstanceId == instanceId);
            register.UpdateImplementationType(impType);
            register.UpdateRegistrationType(registerType);
        }

        private bool TryGetServiceRegister(Guid instanceId, out ServiceRegister register)
        {
            bool isExist = _container.RegisteredTypes.Any(i => i.InstanceId == instanceId);
            if (isExist)
            {
                register = _container.RegisteredTypes.Single(i => i.InstanceId == instanceId);
                return true;
            }
            register = null;
            return false;
        }
        public bool TryGetServiceRegister(Type sourceType, out ServiceRegister register)
        {
            if (IsExistServiceRegister(sourceType))
            {
                register = _container.RegisteredTypes.Single(i => i.SourceType == sourceType);
                return true;
            }
            register = null;
            return false;
        }

        public bool IsExistServiceRegister(Type sourceType)
        {
            return _container.RegisteredTypes.Any(i => i.SourceType == sourceType); ;
        }

        
    }
}