using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.Entities.BaseInterfaces;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;

namespace CQRService.Runtime.Interceptors
{
    public interface IInterception
    {
        public IInterceptorResultStack ResultStack { get; init; }
        public IErrorResultStack ErrorStack { get; init; }
        public TRequest GetRequestForControl<TRequest>()
        where TRequest : class, IRequestQueryBase, new();
        public TSource GetService<TSource>() where TSource : class;
    }
}