using System.Net;



namespace CQRService.Middleware.Responses.ErrorResults
{
    public class MiddlewareErrorResponse : MiddlewareBaseResponse, IMiddlewareResponse
    {
        public MiddlewareErrorResponse(string title, HttpStatusCode status) : base(false, title, status)
        {   
            
        }

        public bool HasData()
        {
            return false;
        }
    }
}