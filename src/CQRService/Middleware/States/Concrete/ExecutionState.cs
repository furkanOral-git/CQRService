using System.Reflection;
using CQRService.Entities.ExceptionHandling;
using CQRService.ExceptionHandling;
using CQRService.Middleware.Requests;
using CQRService.Runtime;

namespace CQRService.Middleware.States.Concrete
{

    internal sealed class ExecutionState : MiddlewareState
    {

        public ExecutionState(StateArguments args) : base(args)
        {

        }
        public override void Main(MiddlewareRequest request)
        {

            var invocationArguments = this._arguments.GetInvocationArguments();
            var invocation = InvocationProvider.CreateInvocation(invocationArguments, request.Provider, _errorStack, _resultStack);

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
                    _errorStack.AddErrorResult(e, "Throwed Exception", nameof(ExecutionState));
                    invocation.Results.IsOperationSuccess = false;
                }
            }

            this._arguments.SetOperationResult(invocation.Results);
            request.TransitionTo(new MiddlewareResponseState(this._arguments));
        }
    }
}