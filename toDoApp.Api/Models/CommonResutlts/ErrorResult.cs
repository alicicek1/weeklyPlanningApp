using System.Net;

namespace toDoApp.Api.Model.CommonResutlts
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message, string errorCode, HttpStatusCode statusCode)
            : base(false, message,
                errorCode, statusCode)
        {
        }
    }
}