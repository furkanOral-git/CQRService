
using CQRService.Entities.Middleware;
using CQRService.ExceptionHandling.MiddlewareExceptions;
using CQRService.Runtime;

namespace CQRService.Middleware.States
{
    internal class StateArguments
    {
        private InvocationArguments _invocationArguments;
        private OperationResult? _result;

        public StateArguments(InvocationArguments invocationArguments)
        {
            _invocationArguments = invocationArguments;
        }

        public void SetOperationResult(OperationResult result)
        {
            _result = result;
        }
        public OperationResult GetOperationResult()
        {
            return _result ?? throw new NotInitiliazedOperationResultException();
        }
        public InvocationArguments GetInvocationArguments()
        {
            return this._invocationArguments;
        }



    }
}