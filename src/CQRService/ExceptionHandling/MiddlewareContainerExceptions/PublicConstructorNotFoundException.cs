using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.ExceptionHandling.MiddlewareContainerExceptions
{
    public class PublicConstructorNotFoundException : Exception
    {
        
        public PublicConstructorNotFoundException(string message) : base(message)
        {
           
        }
    }
}