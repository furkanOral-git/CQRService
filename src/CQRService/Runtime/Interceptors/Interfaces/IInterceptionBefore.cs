using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.Runtime.Interceptors
{
    public interface IInterceptionBefore : IInterception
    {
        public void ReturnAsResponse(object returnValue);
    }
}