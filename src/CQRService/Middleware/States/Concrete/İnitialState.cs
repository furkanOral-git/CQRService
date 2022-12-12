

using CQRService.Runtime;
using CQRService.Runtime.Interceptors;

namespace CQRService.Middleware.States.Concrete
{
   
    internal sealed class İnitialState : MiddlewareState
    {

        public İnitialState(StateArguments args) : base(args)
        {

        }
        public override void Main()
        {
            var invocationArguments = this._arguments.GetInvocationArguments();

            var handlerType = invocationArguments.RequestType.GetNestedTypes()[0];
            invocationArguments.HandlerType = handlerType;
            invocationArguments.HandleMethod = handlerType.GetMethod("Handle");

            IfHasAspectSetTrue(invocationArguments);

            this._middleware.TransitionTo(new BuildState(this._arguments));
        }

        private void IfHasAspectSetTrue(InvocationArguments arguments)
        {
            var requestType = arguments.RequestType;
            var isExist = requestType.GetCustomAttributesData().Any(c => c.AttributeType.IsAssignableTo(typeof(RequestInterceptor)));
            arguments.HasInterceptors = isExist;
        }


    }
}