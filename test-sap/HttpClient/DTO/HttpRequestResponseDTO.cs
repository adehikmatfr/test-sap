using System;
using System.Net;

namespace test_sap.HttpClient.DTO
{
    public class HttpRequestResponseDTO<T>
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public string RequestPayload { get; set; }
        public string ResponseContent { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public string ErrorData { get; set; }
        public Exception Exception { get; set; }
    }
}
