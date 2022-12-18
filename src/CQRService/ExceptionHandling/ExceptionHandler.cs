using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using CQRService.Entities.ExceptionHandling;

namespace CQRService.ExceptionHandling
{
    public sealed class ExceptionHandler : IExceptionHandler
    {
        internal ErrorResult[] Errors { get; set; }

        public ExceptionHandler()
        {
            Errors = Array.Empty<ErrorResult>();
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
            var array = Errors;
            Array.Resize<ErrorResult>(ref array, array.Length + 1);
            array[array.Length - 1] = error;
            Errors = array;
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

        public ErrorResult[] GetErrors()
        {
            return Errors;
        }
    }
}