using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.Entities;

namespace CQRService.MiddlewareContainer.RegistrationControllers
{
    internal interface IRegistrationController
    {
        

        public bool TryGetServiceRegister(Type sourceType, out ServiceRegister register);
        public bool IsExistServiceRegister(Type sourceType);
        public ServiceRegister CreateServiceRegisterIfNotExistAndReturn(Guid instanceId, Type sourceType, Type impType, RegistrationType registerType);
        public void UpdateServiceRegister(Guid instanceId, Type impType, RegistrationType registerType);
       


    }
}