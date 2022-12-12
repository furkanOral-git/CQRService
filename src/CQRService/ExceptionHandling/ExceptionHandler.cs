using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CQRService.Entities.ExceptionHandling;

namespace CQRService.ExceptionHandling
{
    public sealed class ExceptionHandler : IExceptionHandler
    {
        internal IEnumerable<ErrorResult> Errors { get; set; }

        public ExceptionHandler()
        {
            Errors = Enumerable.Empty<ErrorResult>();
        }


        public ErrorResult CreateErrorResult(string title, Exception e, string catcherType, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            ErrorResult error = new ErrorResult
            (
                title,
                catcherType,
                e.Message,
                e.GetType().Name,
                status
            );
            return error;
        }
        public void ContinueWith(ErrorResult error)
        {
            Errors.ToList().Add(error);
        }
        public void ThrowAndExit(ErrorResult error)
        {
            throw new ExitFromProcess(error);
        }
        public bool HasError()
        {
            if (Errors.Count() > 0) return true;
            return false;
        }
        public IEnumerable<ErrorResult> GetErrorsAndClear()
        {
            var list = new List<ErrorResult>();
            foreach (var err in Errors)
            {
                list.Add(err);
            }
            Errors.ToList().Clear();
            return list;
        }



    }
}