using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.MiddlewareContainer.Entities
{
    internal class ServiceRegister : ContainerEntity
    {
        public Type SourceType { get; init; }
        public Type ImplementationType { get; private set; }
        public RegistrationType RegistrationType { get; private set; }

        public ServiceRegister(
        Guid instanceId,
        Type? sourceType,
        Type implementationType,
        RegistrationType registrationType)
        {
            InstanceId = instanceId;
            SourceType = sourceType ?? implementationType;
            ImplementationType = implementationType;
            RegistrationType = registrationType;
        }

        public void UpdateImplementationType(Type newType)
        {
            ImplementationType = newType;
        }
        public void UpdateRegistrationType(RegistrationType newType)
        {
            RegistrationType = newType;
        }
        public bool IsSourceTypeEqualImplementation()
        {
            if (SourceType == ImplementationType)
            {
                return true;
            }
            return false;
        }
    }

}