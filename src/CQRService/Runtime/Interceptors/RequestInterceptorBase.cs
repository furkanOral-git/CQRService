using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.ExceptionHandling;
using CQRService.MiddlewareContainer.DistributionControllers;

namespace CQRService.Runtime.Interceptors
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class RequestInterceptorBase : Attribute
    {
        public virtual int PriortyIndex { get; set; }
        internal IExceptionHandler ExceptionHandler { get; init; }

        protected internal RequestInterceptorBase()
        {
            if (ExceptionHandler is null)
            {
                ExceptionHandler = (IExceptionHandler)BaseContainerDistributionController
                .GetServiceOnRuntimeStatic(typeof(IExceptionHandler));
            }
        }
    }
}