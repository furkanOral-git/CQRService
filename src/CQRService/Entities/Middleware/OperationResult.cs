using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;

namespace CQRService.Entities.Middleware
{
    internal sealed class OperationResult
    {
        public object? Response { get; set; }
        public bool IsReturned { get; set; }
        public bool IsOperationSuccess { get; set; }
        public InterceptorResult[] AspectResults { get; set; }
        public ErrorResult[] Errors { get; set; }

        public OperationResult()
        {
            AspectResults = Array.Empty<InterceptorResult>();
            Errors = Array.Empty<ErrorResult>();
        }
        


    }
}