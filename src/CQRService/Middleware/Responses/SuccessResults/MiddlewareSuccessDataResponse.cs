using System.Net;


namespace CQRService.Middleware.Responses.SuccessResults
{
    public class MiddlewareSuccessDataResponse : MiddlewareBaseResponse, IMiddlewareResponse, IMiddlewareDataResponse
    {
        public object Data { get; init; }

        public MiddlewareSuccessDataResponse(object data, string title) : base(true, title, HttpStatusCode.OK)
        {
            Data = data;
        }

        public bool HasData()
        {
            return true;
        }

        public bool IsDataTypeOf<TResponse>()
        {
            if (Data.GetType() == typeof(TResponse))
            {
                return true;
            }
            return false;
        }
    }
}