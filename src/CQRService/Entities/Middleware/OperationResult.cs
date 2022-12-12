using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;

namespace CQRService.Entities.Middleware
{
    public sealed class OperationResult
    {
        public object? Response { get; set; }
        public bool IsReturned { get; set; }
        public bool IsOperationSuccess { get; set; }
        public IEnumerable<InterceptorResult> AspectResults { get; set; }
        public IEnumerable<ErrorResult> Errors { get; set; }

        public OperationResult()
        {
            AspectResults = Enumerable.Empty<InterceptorResult>();
            Errors = Enumerable.Empty<ErrorResult>();
        }
    }
}