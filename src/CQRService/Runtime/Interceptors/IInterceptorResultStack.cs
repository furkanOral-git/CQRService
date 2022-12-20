using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.Runtime.Interceptors
{
    public interface IInterceptorResultStack
    {
        public void AddInterceptorResult(string sender, object data);
        public bool TryGetFirstDataBySender(string senderName, out object data);
        public bool TryGetAllDataBySender(string senderName, out object[] data);
    }
}