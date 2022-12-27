using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CQRService.Entities.ExceptionHandling;

namespace CQRService
{
    public interface IErrorResultStack 
    {
        public int Count { get;}
        public void AddErrorAndContinue(Exception e, HttpStatusCode status = HttpStatusCode.BadRequest);
        public void AddErrorAndExit(Exception e,HttpStatusCode status = HttpStatusCode.BadRequest);
    }
}