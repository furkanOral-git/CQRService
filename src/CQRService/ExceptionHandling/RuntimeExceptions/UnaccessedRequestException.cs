using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.ExceptionHandling.RuntimeExceptions
{
    public class UnaccessedRequestException : Exception
    {
        public UnaccessedRequestException(string message) : base(message)
        {
            
        }
    }
}