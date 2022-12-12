using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.ExceptionHandling;
using CQRService.MiddlewareContainer.DistributionControllers;
using CQRService.MiddlewareContainer.InstanceControllers;
using CQRService.MiddlewareContainer.RegistrationControllers;

namespace CQRService.MiddlewareContainer.InjectionModules
{
    public abstract class InjectionModuleBase
    {
        internal static bool IsAddedCQRService { get; private set; }
        public abstract void LoadServices(IDiServiceCollection services);
        public static void AddCQRService(IDiServiceCollection services)
        {
            services.AddTransient<IExceptionHandler, ExceptionHandler>();
            services.AddSingleton<ContainerInstanceController>();
            services.AddSingleton<ContainerRegistrationController>();
            services.AddSingleton<ContainerDistributionController>();
            IsAddedCQRService = true;
        }
    }
}