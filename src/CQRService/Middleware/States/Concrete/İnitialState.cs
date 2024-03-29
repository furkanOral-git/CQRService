using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.ExceptionHandling.MiddlewareExceptions;
using CQRService.Middleware.Requests;
using CQRService.MiddlewareContainer;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService.Middleware.States.Concrete
{

    internal sealed class İnitialState : MiddlewareState
    {

        public İnitialState(StateArguments args) : base(args)
        {
            
        }
        public override void Main(MiddlewareRequest request)
        {
            InitStacks(request.Provider, request._providerRequestId);
            var invocationArguments = this._arguments.GetInvocationArguments();

            var handlerType = invocationArguments.RequestType?.GetNestedTypes()[0];
            invocationArguments.HandlerType = handlerType;
            invocationArguments.HandleMethod = handlerType?.GetMethod("Handle");

            IfHasAspectSetTrue(invocationArguments);

            request.TransitionTo(new BuildState(this._arguments));
        }

        private void IfHasAspectSetTrue(InvocationArguments arguments)
        {
            var requestType = arguments.RequestType;
            var isExist = requestType?.GetCustomAttributesData().Any(c => c.AttributeType.IsAssignableTo(typeof(RequestInterceptor)));
            arguments.HasInterceptors = isExist ?? throw new NotSetRequestTypeException();
        }
        private void InitStacks(IDiServiceProvider provider, Guid providerRequestId)
        {
            _errorStack = provider.GetService<ErrorStack>(providerRequestId);
            _resultStack = provider.GetService<InterceptorResultStack>(providerRequestId);
        }

    }
}