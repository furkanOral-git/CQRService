using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.Entities;

namespace CQRService.MiddlewareContainer
{
    internal class MiddlewareServiceContainer
    {
        public List<ServiceRegister> RegisteredTypes { get; init; }
        public List<ServiceInstance> Instances { get; init; }

        internal MiddlewareServiceContainer()
        {
            RegisteredTypes = new();
            Instances = new();
        }
        

    }
}