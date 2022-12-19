using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.Entities.ExceptionHandling;

namespace CQRService.ExceptionHandling
{
    public class ExitFromProcess : Exception
    {
        public ErrorResult Error { get; init; }
        public ExitFromProcess(string message) : base(message)
        {

        }
        public ExitFromProcess(ErrorResult error) 
        {
            Error = error;
        }
    }
}