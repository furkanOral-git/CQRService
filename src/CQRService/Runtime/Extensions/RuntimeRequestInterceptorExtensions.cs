using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CQRService.ExceptionHandling;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRS.Runtime.Extensions
{
    internal static class RuntimeRequestInterceptorExtensions
    {
        public static void OnBeforeOperations(this RequestInterceptor[] interceptors, IInterceptionBefore interception)
        {
            for (int i = 0; i < interceptors.Length; i++)
            {
                interceptors[i].OnBefore(interception);
                var invocation = (Invocation)interception;
                if (!invocation.Results.IsOperationSuccess) throw new ExitFromProcess("Continue With Error And Exit !");
            }
        }
        public static void OnAfterOperations(this RequestInterceptor[] interceptors, IInterceptionAfter interception)
        {
            for (int i = 0; i < interceptors.Length; i++)
            {
                interceptors[i].OnAfter(interception);
                var invocation = (Invocation)interception;
                if (!invocation.Results.IsOperationSuccess) throw new ExitFromProcess("Continue With Error And Exit !");
            }
        }
        public static void OnExceptionOperations(this RequestInterceptor[] interceptors, IInterceptionException interception, Exception e)
        {
            for (int i = 0; i < interceptors.Length; i++)
            {
                interceptors[i].OnException(interception, e);
                var invocation = (Invocation)interception;
                if (!invocation.Results.IsOperationSuccess) throw new ExitFromProcess("Continue With Error And Exit !");
            }
        }
        public static void OnSuccessOperations(this RequestInterceptor[] interceptors, IInterceptionSuccess interception)
        {
            for (int i = 0; i < interceptors.Length; i++)
            {
                interceptors[i].OnSuccess(interception);
                var invocation = (Invocation)interception;
                if (!invocation.Results.IsOperationSuccess) throw new ExitFromProcess("Continue With Error And Exit !");
            }
        }
        public static void StartOperation(this RequestInterceptor[] aspects, ref Invocation invocation)
        {
            bool isSucces = true;
            aspects.OnBeforeOperations((IInterceptionBefore)invocation);
            try
            {
                if (!invocation.Results.IsReturned)
                    invocation.Invoke();
            }
            catch (Exception e)
            {
                isSucces = false;
                aspects.OnExceptionOperations((IInterceptionException)invocation, e);
            }
            finally
            {
                if (isSucces)
                    aspects.OnSuccessOperations((IInterceptionSuccess)invocation);
            }
            aspects.OnAfterOperations((IInterceptionAfter)invocation);


        }




    }
}