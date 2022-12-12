
using CQRService.Runtime.Interceptors;

namespace CQRService.Middleware.States.Concrete
{

    internal sealed class BuildState : MiddlewareState
    {

        public BuildState(StateArguments args) : base(args)
        {

        }
        public override void Main()
        {
            var invocationArguments = this._arguments.GetInvocationArguments();
            invocationArguments.Handler = _serviceProvider.GetService(invocationArguments.HandlerType);

            var requestType = invocationArguments.RequestType;
            invocationArguments.Interceptors = (invocationArguments.HasInterceptors)
            ? GetAspects(requestType)
            : Array.Empty<RequestInterceptor>();

            if (invocationArguments.HasInterceptors)
                this._middleware.TransitionTo(new ExecutionWithInterceptionState(this._arguments));
            else this._middleware.TransitionTo(new ExecutionState(this._arguments));

        }

        private RequestInterceptor[] GetAspects(Type type)
        {
            var Aspects = type.GetCustomAttributes(true)
            .Where(a => a.GetType().IsAssignableTo(typeof(RequestInterceptor)))
            .Select(a => (RequestInterceptor)a)
            .OrderBy(i => i.PriortyIndex).ToArray();
            return Aspects;
        }
    }
}