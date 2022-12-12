using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.ExceptionHandling.MiddlewareContainerExceptions
{
    public class InstanceControllerException : Exception
    {
        public InstanceControllerException(string message) : base(message)
        {
            
        }
    }
}