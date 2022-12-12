using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.ExceptionHandling.MiddlewareContainerExceptions
{
    public class AbstractImplementationTypeException : Exception
    {
        public AbstractImplementationTypeException(string message) : base(message)
        {

        }
    }
}