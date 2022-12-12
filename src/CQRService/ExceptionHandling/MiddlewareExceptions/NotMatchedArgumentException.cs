using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.ExceptionHandling.MiddlewareExceptions
{
    public class NotMatchedArgumentException : Exception
    {
        public NotMatchedArgumentException(string message) : base(message)
        {
            
        }
    }
}