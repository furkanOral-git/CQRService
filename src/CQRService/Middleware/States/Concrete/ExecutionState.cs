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
            var invocation = InvocationProvider.CreateInvocation(invocationArguments, _serviceProvider, _errorStack, _resultStack);

            try
            {
                invocation.Invoke();
                invocation.Results.IsOperationSuccess = true;
            }
            catch (TargetInvocationException e)
            {
                var exception = e.InnerException;
                    
                if (exception?.GetType() == typeof(ExitFromProcess))
                {
                    invocation.Results.IsOperationSuccess = false;
                }
                else
                {
                    _errorStack.AddErrorResult("Throwed Exception", e, "ExecutionState");
                    invocation.Results.IsOperationSuccess = false;
                }
            }

            this._arguments.SetOperationResult(invocation.Results);
            _middleware.TransitionTo(new MiddlewareResponseState(this._arguments));
        }
    }
}