using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using CQRService.ExceptionHandling;

namespace CQRService.Entities.ExceptionHandling
{
    public record ErrorStack : IErrorResultStack, IErrorStackAccessor
    {
        public int Count { get { return (Errors is null) ? 0 : Errors.Length; } }
        public ErrorResult[]? Errors { get; private set; }
        public ErrorResult this[int indx] { get { return (Errors is null) ? default : Errors[indx]; } }
        public ErrorStack()
        {
           
        }
        internal void AddErrorResult(Exception e, string title, string sender, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            Errors ??= Array.Empty<ErrorResult>();
            var error = CreateError(e, title, sender, status);
            AddErrorResult(error);
        }
        public ErrorResult CreateError(Exception e, string title, string sender, HttpStatusCode status = HttpStatusCode.BadRequest)
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
        public bool TryGetErrorByType<TException>(out ErrorResult result) where TException : Exception
        {
            result = Errors.SingleOrDefault(e => e.ExceptionType == nameof(TException));
            return (result != default) ? false : true;
        }
        public bool TryGetErrorsBySender<TSender>(out ErrorResult[] results)
        {
            results = Errors.Where(e => e.Sender == nameof(TSender)).ToArray();
            return (results is null) ? false : true;
        }
        public void AddErrorAndContinue(Exception e, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            AddErrorResult(e, "An Error Occured And Continue", GetSender(), status);
        }
        private string GetSender()
        {
            StackTrace st = new StackTrace();
            var senderMethod = st.GetFrame(2).GetMethod();
            return string.Format("{0}.{1}()", senderMethod?.DeclaringType?.Name, senderMethod?.Name);
        }
        public void AddErrorAndExit(Exception e, HttpStatusCode status = HttpStatusCode.BadRequest)
        {

            var error = CreateError(e, "Exit With Error", GetSender(), status);
            AddErrorResult(error);
            throw new ExitFromProcess(error);
        }

    }
}