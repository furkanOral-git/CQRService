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
        public void AddErrorAndContinue(string title, Exception e, string sender, HttpStatusCode status = HttpStatusCode.BadRequest);
        public void AddErrorAndContinue(ErrorResult error);
        public void AddErrorAndExit(ErrorResult error);
        public void AddErrorAndExit(string title, Exception e, string sender, HttpStatusCode status = HttpStatusCode.BadRequest);
        public ErrorResult CreateError(string title, Exception e, string sender, HttpStatusCode status = HttpStatusCode.BadRequest);
        public int Count { get;}
    }
}