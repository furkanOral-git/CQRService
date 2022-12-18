using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CQRService.Entities.ExceptionHandling;
using CQRService.MiddlewareContainer.Entities;

namespace CQRService.ExceptionHandling
{
    public interface IExceptionHandler : IContainerService
    {
        public ErrorResult CreateErrorResult(string title, Exception e, string catcherType, HttpStatusCode status = HttpStatusCode.BadRequest);
        public void ContinueWith(ErrorResult error);
        public void ThrowAndExit(ErrorResult error);
        public bool HasError();
        public ErrorResult[] GetErrors();

    }
}