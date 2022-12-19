using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.Entities.BaseInterfaces;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService
{
    public interface IRequestHandler<TRequest> : IRequestQueryHandler
    where TRequest : class, IRequestBase, new()
    {
        public void Handle(TRequest request, IErrorResultStack erStack, IInterceptorResultStack reStack);
    }
    
}