using System.Reflection;
using CQRService.Entities.ExceptionHandling;
using CQRService.ExceptionHandling;
using CQRService.Runtime;

namespace CQRService.Middleware.States.Concrete
{

    internal sealed class ExecutionState : MiddlewareState
    {
        
        public ExecutionState(StateArguments args) : base(args)
        {
            
        }
        public override void Main()
        {

            var invocationArguments = this._arguments.GetInvocationArguments();
            var invocation = InvocationProvider.CreateInvocation(invocationArguments,_serviceProvider);

            try
            {
                invocation.Invoke();
                invocation.Results.IsOperationSuccess = true;
            }
            catch (TargetInvocationException e)
            {
                var exception = e.InnerException;
                ErrorResult error;

                if (exception.GetType() == typeof(ExitFromProcess))
                {
                    var exit = (ExitFromProcess)exception;
                    error = exit.Error;
                }
                else
                {
                    error = _exceptionHandler.CreateErrorResult("Throwed Exception", e, "ExecutionState");

                }
                invocation.ContinueWith(error);
            }

            if (_exceptionHandler.HasError())
                invocation.Results.Errors = _exceptionHandler.GetErrorsAndClear().ToList();
            invocation.Results.IsOperationSuccess = false;


            this._arguments.SetOperationResult(invocation.Results);
            _middleware.TransitionTo(new MiddlewareResponseState(this._arguments));
        }
    }
}