using CQRService.ExceptionHandling.MiddlewareExceptions;
using CQRService.Middleware.Requests;
using CQRService.Middleware.Responses;
using CQRService.Middleware.States;
using CQRService.MiddlewareContainer;

namespace CQRService.Middleware
{
    internal sealed class RequestMiddleware
    {
        private static RequestMiddleware? _instance;
        public IDiServiceProvider Provider { get; init; }

        private RequestMiddleware()
        {
            Provider = ContainerServiceProvider.GetProvider();
        }
        public static RequestMiddleware Get()
        {
            if (_instance is null)
            {
                _instance = new RequestMiddleware();
            }
            return _instance;
        }
        public MiddlewareRequest CreateNewRequest(IDiServiceProvider provider)
        {
            //This uses for indicate scope service life time of begin  
            var requestId = Provider.NewRequestId();
            return new MiddlewareRequest(provider, requestId);
        }
       




    }
}