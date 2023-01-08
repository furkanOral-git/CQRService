using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.ExceptionHandling.MiddlewareExceptions;
using CQRService.Middleware.Requests;
using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService.Middleware.States.Concrete
{

    internal sealed class İnitialState : MiddlewareState
    {

        public İnitialState(StateArguments args) : base(args)
        {
            _errorStack = _serviceProvider.GetService<ErrorStack>();
            _resultStack = _serviceProvider.GetService<InterceptorResultStack>();
        }
        public override void Main(MiddlewareRequest request)
        {
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


    }
}