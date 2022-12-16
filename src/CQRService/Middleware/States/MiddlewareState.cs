
using CQRService.ExceptionHandling;
using CQRService.MiddlewareContainer;
using CQRService.Runtime;

namespace CQRService.Middleware.States
{
    internal abstract class MiddlewareState
    {
        protected static RequestMiddleware _middleware;
        protected StateArguments _arguments;
        protected static IExceptionHandler _exceptionHandler;
        protected static IRuntimeServiceProvider _serviceProvider;


        public MiddlewareState(StateArguments arguments)
        {

            _arguments = arguments;

        }
        static MiddlewareState()
        {
            _middleware = RequestMiddleware.Get();
            _serviceProvider = ContainerServiceProvider.GetProvider();
            _exceptionHandler = (IExceptionHandler)_serviceProvider.GetServiceOnRuntime(typeof(IExceptionHandler));
        }

        public abstract void Main();

    }
}