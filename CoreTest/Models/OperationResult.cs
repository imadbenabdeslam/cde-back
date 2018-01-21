using System.Net;

namespace CoreTest.Models
{
    public class OperationResult
    {
        public OperationResult()
        {
            StatusCode = HttpStatusCode.OK;
        }

        public string ErrorMessage { get; set; }

        public object Result { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
