using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.Runtime.Interceptors
{
    public interface IInterceptorResultStack
    {
        public bool TryGetFirstDataBySender(string senderName, out object data);
    }
}