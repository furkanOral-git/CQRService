using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.Entities.BaseInterfaces;

namespace CQRService
{
    public interface IRequestHandler<TRequest> : IRequestQueryHandler
    where TRequest : class, IRequestBase, new()
    {
        public void Handle(TRequest request, IErrorResultStack erStack, IResultStackAccessor reStack);
    }
    
}