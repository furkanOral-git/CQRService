using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.ExceptionHandling.RuntimeExceptions
{
    public class ReturnValueTypeDidNotMatch : Exception
    {
        public ReturnValueTypeDidNotMatch(string message) : base(message)
        {
            
        }
    }
}