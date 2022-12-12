using System.Net;



namespace CQRService.Middleware.Responses.SuccessResults
{
    public class MiddlewareSuccessResponse : MiddlewareBaseResponse, IMiddlewareResponse
    {


        public MiddlewareSuccessResponse(string title) : base(true, title, HttpStatusCode.OK)
        {

        }

        public bool HasData()
        {
            return false;
        }
    }
}