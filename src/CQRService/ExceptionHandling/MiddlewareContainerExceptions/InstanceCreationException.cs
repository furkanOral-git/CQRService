using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.ExceptionHandling.MiddlewareContainerExceptions
{
    public class InstanceCreationException : Exception
    {
        public InstanceCreationException(string message) : base(message )
        {
            
        }
    }
}