using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.ExceptionHandling.MiddlewareExceptions
{
    public class MissingCollectiveArgumentException : Exception
    {
        public MissingCollectiveArgumentException(string message) : base(message)
        {
            
        }
    }
}