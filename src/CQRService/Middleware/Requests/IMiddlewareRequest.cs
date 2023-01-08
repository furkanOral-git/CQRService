using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.Middleware.Responses;
using CQRService.Middleware.States;

namespace CQRService.Middleware.Requests
{
    internal interface IMiddlewareRequest
    {
        public void TransitionTo(MiddlewareState state);
        public void SetRequestResponse(IMiddlewareResponse response);
        public IMiddlewareResponse GetRequestResponse();
        
    }
}