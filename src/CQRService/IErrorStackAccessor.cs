using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.Entities.ExceptionHandling;

namespace CQRService
{
    public interface IErrorStackAccessor
    {
        public bool TryGetErrorsBySender<TSender>(out ErrorResult[] results);
        public bool TryGetErrorByType<TException>(out ErrorResult result)
        where TException : Exception;
    }
}