using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.ExceptionHandling.MiddlewareContainerExceptions
{
    public class NotRegisteredTypeException : Exception
    {
        public NotRegisteredTypeException(string message) : base(message)
        {
            
        }
    }
}