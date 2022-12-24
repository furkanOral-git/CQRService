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
        public InterceptorResultStack ResultStack { get; init; }
        public ErrorStack ErrorStack { get; init; }
        public TRequest GetRequestForControl<TRequest>()
        where TRequest : class, IRequestQueryBase, new();
        public TSource? GetService<TSource>() where TSource : class;
    }
}