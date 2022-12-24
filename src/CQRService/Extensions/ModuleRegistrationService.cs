using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer;
using CQRService.MiddlewareContainer.InjectionModules;

namespace CQRService.ModuleRegistrationService
{
    public static class ModuleRegistrationService
    {
        public static IDiServiceCollection AddModule<TModule>(this IDiServiceCollection services)
        where TModule : InjectionModuleBase, new()
        {
            var module = Activator.CreateInstance(typeof(TModule)) as TModule;
            if (module is not null) module.LoadServices(services);
            return services;
        }
    }

}