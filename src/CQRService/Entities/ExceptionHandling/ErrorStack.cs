using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CQRService.ExceptionHandling;

namespace CQRService.Entities.ExceptionHandling
{
    public record ErrorStack : IErrorResultStack, IErrorStackAccessor
    {
        public int Count { get { return Errors.Length; } }
        public ErrorResult[] Errors { get; private set; }
        public ErrorResult this[int indx] { get { return Errors[indx]; } }
        public ErrorStack()
        {
            Errors = Array.Empty<ErrorResult>();
        }
        public void AddErrorResult(string title, Exception e, string sender, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            var error = CreateError(title, e, sender, status);
            AddErrorResult(error);
        }
        public ErrorResult CreateError(string title, Exception e, string sender, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            ErrorResult error = new ErrorResult
            (
                title,
                sender,
                e.Message,
                e.GetType().Name,
                status
            );
            return error;
        }
        public void AddErrorResult(ErrorResult error)
        {
            var array = Errors;
            Array.Resize<ErrorResult>(ref array, array.Length + 1);
            array[array.Length - 1] = error;
            Errors = array;
        }
        public bool TryGetErrorsBySender(string sender, out ErrorResult[] results)
        {
            var errors = Errors.Where(e => e.Sender == sender).ToArray();
            if (errors != default)
            {
                results = errors;
                return true;
            }
            results = default;
            return false;
        }
        public ErrorResult[] GetErrors()
        {
            return Errors;
        }

        public void AddErrorAndContinue(string title, Exception e, string sender, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            AddErrorResult(title, e, sender, status);
        }

        public void AddErrorAndExit(string title, Exception e, string sender, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            var error = CreateError(title, e, sender, status);
            AddErrorResult(error);
            throw new ExitFromProcess(error);
        }

        public void AddErrorAndContinue(ErrorResult error)
        {
            AddErrorResult(error);
        }

        public void AddErrorAndExit(ErrorResult error)
        {
            AddErrorResult(error);
            throw new ExitFromProcess(error);
        }


    }
}