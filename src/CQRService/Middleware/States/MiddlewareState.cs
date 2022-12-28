
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.ExceptionHandling;
using CQRService.MiddlewareContainer;
using CQRService.Runtime;

namespace CQRService.Middleware.States
{
    internal abstract class MiddlewareState
    {
        protected static RequestMiddleware _middleware;
        protected StateArguments _arguments;
        protected static ErrorStack _errorStack;
        protected static InterceptorResultStack _resultStack;
        protected static IDiServiceProvider _serviceProvider;
        

        public MiddlewareState(StateArguments arguments)
        {
            _arguments = arguments;
        }
        static MiddlewareState()
        {
            _middleware = RequestMiddleware.Get();
            _serviceProvider = (IDiServiceProvider)_middleware.Provider;
        }

        public abstract void Main();

    }
}