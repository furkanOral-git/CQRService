
using CQRService.ExceptionHandling.MiddlewareExceptions;
using CQRService.Middleware.Requests;
using CQRService.MiddlewareContainer;
using CQRService.Runtime.Interceptors;

namespace CQRService.Middleware.States.Concrete
{

    internal sealed class BuildState : MiddlewareState
    {

        public BuildState(StateArguments args) : base(args)
        {

        }
        public override void Main(MiddlewareRequest request)
        {
            var invocationArguments = this._arguments.GetInvocationArguments();
            var casted = request.Provider as ContainerServiceProvider;
            invocationArguments.Handler = casted?.GetService
            (
                invocationArguments?.HandlerType
                ?? throw new NotSetHandlerTypeException(),
                request._providerRequestId
            );

            var requestType = invocationArguments.RequestType;
            invocationArguments.Interceptors = (invocationArguments.HasInterceptors)
            ? GetAspects(requestType ?? throw new NotSetRequestTypeException())
            : Array.Empty<RequestInterceptor>();

            if (invocationArguments.HasInterceptors)
                request.TransitionTo(new ExecutionWithInterceptionState(this._arguments));
            else request.TransitionTo(new ExecutionState(this._arguments));

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