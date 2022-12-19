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
            var invocation = InvocationProvider.CreateInvocation(invocationArguments, _serviceProvider, _errorStack, _resultStack);


            invocation.Results.IsOperationSuccess = true;
            try
            {
                aspects.StartOperation(ref invocation);
            }
            catch (ExitFromProcess e)
            {
                invocation.Results.IsOperationSuccess = false;
            }
            catch (Exception e)
            {
                _errorStack.AddErrorResult("Throwed Exception", e, nameof(ExecutionWithInterceptionState));
                invocation.Results.IsOperationSuccess = false;
            }

            this._arguments.SetOperationResult(invocation.Results);
            _middleware.TransitionTo(new MiddlewareResponseState(this._arguments));

        }




    }
}