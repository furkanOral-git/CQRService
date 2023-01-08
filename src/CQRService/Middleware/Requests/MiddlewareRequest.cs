using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.ExceptionHandling.MiddlewareExceptions;
using CQRService.Middleware.Responses;
using CQRService.Middleware.States;
using CQRService.MiddlewareContainer;

namespace CQRService.Middleware.Requests
{
    internal class MiddlewareRequest : IMiddlewareRequest
    {
        private MiddlewareState? _state;
        private IMiddlewareResponse? _middlewareResponse;
        public IDiServiceProvider Provider { get; init; }

        public MiddlewareRequest(IDiServiceProvider provider)
        {
            _middlewareResponse = default;
            Provider = provider;
        }
        public void TransitionTo(MiddlewareState state)
        {
            _state = state;
            _state.Main();
        }
        public void SetRequestResponse(IMiddlewareResponse response)
        {
            throw new NotImplementedException();
        }

        public IMiddlewareResponse GetRequestResponse()
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
    }
}