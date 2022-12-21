using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService
{
    public interface IResultStackAccessor
    {
        public bool TryGetFirstDataBySender(string senderName, out object data);
        public bool TryGetAllDataBySender(string senderName, out object[] data);
    }
}