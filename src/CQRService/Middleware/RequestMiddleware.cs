using CQRService.ExceptionHandling.MiddlewareExceptions;
using CQRService.Middleware.Responses;
using CQRService.Middleware.States;

namespace CQRService.Middleware
{
    internal sealed class RequestMiddleware
    {
        private static RequestMiddleware? _instance;
        private MiddlewareState? _state;
        private IMiddlewareResponse? _middlewareResponse;

        private RequestMiddleware()
        {
            _middlewareResponse = default;
        }
        public static RequestMiddleware Get()
        {
            if (_instance is null)
            {
                _instance = new RequestMiddleware();
            }
            return _instance;
        }
        internal void SetMiddlewareResponse(IMiddlewareResponse response)
        {
            this._middlewareResponse = response;
        }
        public IMiddlewareResponse GetMiddlewareResponse()
        {
            var response = this._middlewareResponse;
            ClearResponse();
            return response ?? throw new MiddlewareResponseNotExistException();
        }
        internal void ClearState()
        {
            this._state = null;
        }
        private void ClearResponse()
        {
            this._middlewareResponse = null;
        }
        public void TransitionTo(MiddlewareState state)
        {
            _state = state;
            _state.Main();
        }


    }
}