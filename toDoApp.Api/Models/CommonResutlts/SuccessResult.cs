using System.Net;

namespace toDoApp.Api.Model.CommonResutlts
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message, HttpStatusCode.OK)
        {
        }

        public SuccessResult() : base(true, "Successful operation.", HttpStatusCode.OK)
        {
        }
    }
}