namespace CQRService.Middleware.Responses
{
    public interface IMiddlewareResponse
    {
        public bool HasData();

    }
    public interface IMiddlewareDataResponse
    {
        public bool IsDataTypeOf<TResponse>();
    }

}