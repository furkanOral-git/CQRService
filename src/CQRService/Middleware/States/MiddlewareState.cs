

using CQRService.ExceptionHandling;
using CQRService.MiddlewareContainer.DistributionControllers;
using CQRService.Runtime;

namespace CQRService.Middleware.States
{
    internal abstract class MiddlewareState
    {
        internal RequestMiddleware _middleware;
        protected StateArguments _arguments;
        protected static IExceptionHandler _exceptionHandler;
        protected static IRuntimeDistributionController _serviceProvider;


        public MiddlewareState(StateArguments arguments)
        {
            _middleware = RequestMiddleware.Get();
            _arguments = arguments;

        }
        static MiddlewareState()
        {
            _serviceProvider = (ContainerDistributionController)BaseContainerDistributionController
            .GetServiceStatic(typeof(ContainerDistributionController));
            _exceptionHandler = (IExceptionHandler)_serviceProvider.GetServiceOnRuntime(typeof(IExceptionHandler));
        }

        public abstract void Main();

    }
}