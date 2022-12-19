using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.Entities.BaseInterfaces;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestQueryHandler
    where TQuery : class, IQueryBase, new()
    where TResponse : class, new()
    {
        public TResponse Handle(TQuery query, IErrorResultStack erStack, IInterceptorResultStack reStack);
    }
}