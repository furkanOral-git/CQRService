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
    public record InterceptorResultStack : IInterceptorResultStack, IResultStackAccessor
    {
        public int Count { get { return Results.Length; } }
        public InterceptorResult[] Results { get; private set; }
        public InterceptorResult this[int indx] { get { return Results[indx]; } }

        public InterceptorResultStack()
        {
            Results = Array.Empty<InterceptorResult>();
        }
        public void AddInterceptorResult(string sender, object data)
        {
            InterceptorResult result = new InterceptorResult(sender, data);
            var array = Results;
            Array.Resize<InterceptorResult>(ref array, array.Length + 1);
            array[array.Length - 1] = result;
            Results = array;
        }
        public bool TryGetFirstDataBySender(string senderName, out object data)
        {
            var result = Results.FirstOrDefault(r => r.Sender == senderName);
            if (result != default)
            {
                data = result.AspectData;
                return (data is null) ? false : true;
            }
            data = default;
            return false;
        }

        public bool TryGetAllDataBySender(string senderName, out object[] data)
        {
            var results = Results.Where(r => r.Sender == senderName);
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