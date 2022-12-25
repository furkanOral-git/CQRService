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
        public List<ScopedServiceInstance> ScopedInstances { get; init; }
        private static MiddlewareServiceContainer? _instance;

        private MiddlewareServiceContainer()
        {
            RegisteredTypes = new();
            Instances = new();
            ScopedInstances = new();
        }
        internal static MiddlewareServiceContainer InitContainer()
        {
            if (_instance is null) _instance = new MiddlewareServiceContainer();
            return _instance;
        }


    }
}