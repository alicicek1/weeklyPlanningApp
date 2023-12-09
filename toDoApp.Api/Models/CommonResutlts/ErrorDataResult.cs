using System.Net;

namespace toDoApp.Api.Model.CommonResutlts
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T? data, string message, string errorCode,
            HttpStatusCode statusCode) : base(data, false,
            message, errorCode, statusCode)
        {
        }
    }
}