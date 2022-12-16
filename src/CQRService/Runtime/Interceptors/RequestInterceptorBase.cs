using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.ExceptionHandling;
using CQRService.MiddlewareContainer;

namespace CQRService.Runtime.Interceptors
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class RequestInterceptorBase : Attribute
    {
        public virtual int PriortyIndex { get; set; }
        internal IExceptionHandler ExceptionHandler { get; private set; }

        protected RequestInterceptorBase()
        {
            var provider = (IRuntimeServiceProvider)ContainerServiceProvider.GetProvider();
            ExceptionHandler = (IExceptionHandler)provider.GetServiceOnRuntime(typeof(IExceptionHandler));
        }   

    }
}