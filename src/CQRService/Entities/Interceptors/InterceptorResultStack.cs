using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using System.Security.AccessControl;
using System.Threading.Tasks;
using CQRService.Middleware.Responses;
using CQRService.Runtime.Interceptors;

namespace CQRService.Entities.Interceptors
{
    public record InterceptorResultStack : IInterceptorResultStack
    {
        private InterceptorResult[] _results;
        public InterceptorResult this[int indx] { get { return _results[indx]; } }
        public int Count { get { return _results.Length; } }

        public InterceptorResultStack()
        {
            _results = Array.Empty<InterceptorResult>();
        }
        public void AddInterceptorResult(string sender, object data)
        {
            InterceptorResult result = new InterceptorResult(sender, data);
            var array = _results;
            Array.Resize<InterceptorResult>(ref array, array.Length + 1);
            array[array.Length - 1] = result;
            _results = array;
        }
        public bool TryGetFirstDataBySender(string senderName, out object data)
        {
            var result = _results.FirstOrDefault(r => r.Sender == senderName);
            if (result != default)
            {
                data = result.AspectData;
                return true;
            }
            data = null;
            return false;
        }
        
        public bool TryGetAllDataBySender(string senderName, out object[] data)
        {
            var results = _results.Where(r => r.Sender == senderName);
            if (results is not null)
            {
                data = results.Select(r => r.AspectData).ToArray();
                return true;
            }
            data = null;
            return false;
        }

       
    }
}