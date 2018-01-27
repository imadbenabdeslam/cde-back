using System;

namespace CoreTest.Core
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public ApiException(string message,
                            int statusCode = 500) :
            base(message)
        {
            StatusCode = statusCode;
        }
        public ApiException(Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }

    public class ApiError
    {
        public string message { get; set; }
        public bool isError { get; set; }
        public string detail { get; set; }

        public ApiError(string message)
        {
            this.message = message;
            isError = true;
        }
    }
}
