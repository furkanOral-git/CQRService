using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CQRService.Runtime.Interceptors;

namespace CQRService.Runtime
{
    internal class InvocationArguments
    {
        public object? Request { get; set; }
        public object? Handler { get; set; }
        public MethodInfo? HandleMethod { get; set; }
        public Type? RequestType { get; set; }
        public Type? HandlerType { get; set; }
        public RequestInterceptor[] Interceptors { get; set; }
        public bool HasHandlerConstructorTypes { get; set; }
        public bool HasInterceptors { get; set; }

        public InvocationArguments()
        {
            Interceptors = Array.Empty<RequestInterceptor>();
        }
    }
}