using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using test_sap.HttpClient.DTO;

namespace test_sap.HttpClient
{
    public class HttpClientService
    {
        public enum RequestMethod
        {
            POST,
            GET,
            PUT,
            PATCH,
            DELETE
        }
        public HttpRequestResponseDTO<T> PopulateRequestError<T>(WebException webEx, string requestPayload)
        {
            var result = new HttpRequestResponseDTO<T>();

            string errResponse = null;
            var errStream = webEx.Response?.GetResponseStream();
            if (errStream != null)
            {
                using (var sr = new StreamReader(errStream))
                {
                    errResponse = sr.ReadToEnd();
                }
            }

            result.IsSuccess = false;
            result.Exception = webEx;
            result.RequestPayload = requestPayload;
            result.ResponseContent = errResponse;
            result.StatusCode = ((HttpWebResponse)webEx.Response)?.StatusCode;
            result.ErrorData = !string.IsNullOrWhiteSpace(errResponse) ? errResponse : webEx.Message;

            return result;
        }
        public async Task<HttpRequestResponseDTO<string>> ApiCall(string url, RequestMethod method, string payload="")
        {
            HttpRequestResponseDTO<string> result;

            using (var client = new WebClient())
            {
                try
                {
                    var response = "";
                    var stringMethod = "";
                    client.Headers.Add("Accept", "application/json");
                    client.Headers.Add("Content-Type", "application/json");
                    client.UseDefaultCredentials = true;

                    switch (method)
                    {
                        case RequestMethod.GET:
                            var address = new Uri(url);
                            response = await client.DownloadStringTaskAsync(address);
                            break;
                        case RequestMethod.PATCH:
                        case RequestMethod.POST:
                        case RequestMethod.PUT:
                        case RequestMethod.DELETE:
                            stringMethod = method == RequestMethod.PATCH ? "PATCH": method == RequestMethod.POST ? "POST": method == RequestMethod.PUT? "PUT": method == RequestMethod.DELETE ? "DELETE": "";

                            client.Encoding = Encoding.UTF8;
                            response = await client.UploadStringTaskAsync(url, stringMethod,
                                !string.IsNullOrWhiteSpace(payload) ? payload : string.Empty);
                            break;
                    }


                    result = new HttpRequestResponseDTO<string>
                    {
                        IsSuccess = true,
                        Result = response,
                        ResponseContent = response
                    };
                }
                catch (WebException webEx)
                {
                    result = PopulateRequestError<string>(webEx, string.Empty);
                }
            }
            return result;
        }
    }
}
