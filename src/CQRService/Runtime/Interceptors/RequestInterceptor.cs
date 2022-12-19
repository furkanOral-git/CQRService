using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CQRService.Runtime.Interceptors
{
    public abstract class RequestInterceptor : RequestInterceptorBase
    {
        public RequestInterceptor() : base()
        {

        }
        public virtual void OnBefore(IInterceptionBefore invocation) { }
        public virtual void OnAfter(IInterceptionAfter invocation) { }
        public virtual void OnSuccess(IInterceptionSuccess invocation) { }
        public virtual void OnException(IInterceptionException invocation, Exception e) { }
    }
}