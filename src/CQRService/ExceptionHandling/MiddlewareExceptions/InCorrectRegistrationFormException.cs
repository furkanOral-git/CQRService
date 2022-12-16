using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.ExceptionHandling.MiddlewareExceptions
{
    public class InCorrectRegistrationFormException : Exception
    {
        public InCorrectRegistrationFormException(string message) : base(message)
        {
            
        }
    }
}