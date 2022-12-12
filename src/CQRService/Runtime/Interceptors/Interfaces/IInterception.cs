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
        public void Return(InterceptorResult InterceptorResult);
        public void ThrowAndExit(ErrorResult e);
        public void ContinueWith(ErrorResult e);
        public TRequest GetRequestForControl<TRequest>()
        where TRequest : class, IRequestQueryBase, new();

        public TSource GetService<TSource>() where TSource : class;
    }
}