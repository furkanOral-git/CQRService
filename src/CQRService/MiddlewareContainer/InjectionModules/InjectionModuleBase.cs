using CQRService.ExceptionHandling;


namespace CQRService.MiddlewareContainer.InjectionModules
{
    public abstract class InjectionModuleBase
    {
        public abstract void LoadServices(IDiServiceCollection services);
        internal static void AddCQRService()
        {
            var services = (IDiServiceCollection)DiServiceCollection.InitServiceCollection();
            services.AddTransient<IExceptionHandler, ExceptionHandler>();
        }
        static InjectionModuleBase()
        {
            AddCQRService();
        }
    }
}