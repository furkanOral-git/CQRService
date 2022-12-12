using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer;
using CQRService.MiddlewareContainer.InjectionModules;

namespace CQRService.Extensions
{
    public static class ModuleRegistrationService
    {
        public static IDiServiceCollection AddModule<TModule>(this IDiServiceCollection services)
        where TModule : InjectionModuleBase, new()
        {
            if (!InjectionModuleBase.IsAddedCQRService)
                InjectionModuleBase.AddCQRService(services);

            var module = (TModule)Activator.CreateInstance(typeof(TModule));
            if (module is not null) module.LoadServices(services);
            return services;
        }
    }

}