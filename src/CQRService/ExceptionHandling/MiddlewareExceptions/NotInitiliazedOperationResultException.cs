using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.ExceptionHandling.MiddlewareExceptions
{
    public class NotInitiliazedOperationResultException : Exception
    {
            public NotInitiliazedOperationResultException()
            {
                
            }
    }
}