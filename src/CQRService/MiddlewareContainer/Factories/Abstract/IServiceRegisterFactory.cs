using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.Entities;

namespace CQRService.MiddlewareContainer.Factories.Abstract
{
    internal interface IServiceRegisterFactory : IFactory<ServiceRegister>
    {
        //Eğer container da varsa döndürür yoksa oluşturur ekler ve döndürür.
        public ServiceRegister GetServiceRegister(Guid instanceId, Type? sourceType, Type impType, RegistrationType registerType, out bool IsItNewProduction);
        public void UpdateServiceRegister(ServiceRegister register);
    }
}