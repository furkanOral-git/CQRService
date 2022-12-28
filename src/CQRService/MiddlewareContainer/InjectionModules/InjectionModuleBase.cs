using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService.MiddlewareContainer.InjectionModules
{
    public abstract class InjectionModuleBase
    {
        public abstract void LoadServices(IDiServiceCollection services);
        private static void AddCQRService()
        {
            var services = (IDiServiceCollection)ContainerServiceCollection.InitServiceCollection();
            services.AddScoped<IErrorResultStack, ErrorStack>();
            services.AddScoped<IInterceptorResultStack, InterceptorResultStack>();
        }
        static InjectionModuleBase()
        {
            AddCQRService();
        }
    }
}