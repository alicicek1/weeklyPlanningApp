using System.Net;

namespace toDoApp.Api.Model.CommonResutlts
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, true, message,
            HttpStatusCode.OK)
        {
        }

        public SuccessDataResult(T data) : base(data, true, "Successful operation.", HttpStatusCode.OK)
        {
        }
    }
}