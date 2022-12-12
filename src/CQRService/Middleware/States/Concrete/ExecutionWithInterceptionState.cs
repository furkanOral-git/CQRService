using CQRS.Runtime.Extensions;
using CQRService.ExceptionHandling;
using CQRService.Runtime;

namespace CQRService.Middleware.States.Concrete
{
    internal sealed class ExecutionWithInterceptionState : MiddlewareState
    {

        public ExecutionWithInterceptionState(StateArguments args) : base(args)
        {

        }
        public override void Main()
        {
            var invocationArguments = this._arguments.GetInvocationArguments();
            var aspects = invocationArguments.Interceptors;
            var invocation = InvocationProvider.CreateInvocation(invocationArguments, _serviceProvider);
            

            invocation.Results.IsOperationSuccess = true;
            try
            {
                aspects.StartOperation(ref invocation);
            }
            catch (ExitFromProcess e)
            {
                invocation.ContinueWith(e.Error);
            }
            catch (Exception e)
            {
                var error = _exceptionHandler.CreateErrorResult("Throwed Exception", e, "ExecutionWithInterceptionState");
                invocation.ContinueWith(error);
            }
            if (_exceptionHandler.HasError())
                invocation.Results.Errors = _exceptionHandler.GetErrorsAndClear().ToList();

            this._arguments.SetOperationResult(invocation.Results);
            this._middleware.TransitionTo(new MiddlewareResponseState(this._arguments));

        }




    }
}