
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
        protected InterceptorResultStack _resultStack;
        protected static IRuntimeServiceProvider _serviceProvider;


        public MiddlewareState(StateArguments arguments)
        {

            _arguments = arguments;

        }
        static MiddlewareState()
        {
            _middleware = RequestMiddleware.Get();
            _serviceProvider = ContainerServiceProvider.GetProvider();
            
        }

        public abstract void Main();

    }
}