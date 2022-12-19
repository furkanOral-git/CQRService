using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService.MiddlewareContainer.InjectionModules
{
    public abstract class InjectionModuleBase
    {
        public abstract void LoadServices(IDiServiceCollection services);
        internal static void AddCQRService()
        {
            var services = (IDiServiceCollection)DiServiceCollection.InitServiceCollection();
            services.AddTransient<IErrorResultStack, ErrorStack>();
            services.AddTransient<IInterceptorResultStack, InterceptorResultStack>();
        }
        static InjectionModuleBase()
        {
            AddCQRService();
        }
    }
}