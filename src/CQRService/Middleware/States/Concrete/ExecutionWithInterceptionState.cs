using CQRS.Runtime.Extensions;
using CQRService.ExceptionHandling;
using CQRService.Middleware.Requests;
using CQRService.Runtime;

namespace CQRService.Middleware.States.Concrete
{
    internal sealed class ExecutionWithInterceptionState : MiddlewareState
    {

        public ExecutionWithInterceptionState(StateArguments args) : base(args)
        {

        }
        public override void Main(MiddlewareRequest request)
        {
            var invocationArguments = this._arguments.GetInvocationArguments();
            var aspects = invocationArguments.Interceptors;
            var invocation = InvocationProvider.CreateInvocation(invocationArguments, request.Provider, _errorStack, _resultStack);


            invocation.Results.IsOperationSuccess = true;
            try
            {
                aspects.StartOperation(ref invocation);
            }
            catch (ExitFromProcess)
            {
                invocation.Results.IsOperationSuccess = false;
            }
            catch (Exception e)
            {
                _errorStack.AddErrorResult(e, "Throwed Exception", nameof(ExecutionWithInterceptionState));
                invocation.Results.IsOperationSuccess = false;
            }

            this._arguments.SetOperationResult(invocation.Results);
            request.TransitionTo(new MiddlewareResponseState(this._arguments));

        }




    }
}