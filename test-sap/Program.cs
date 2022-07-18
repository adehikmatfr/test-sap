using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using test_sap.HttpClient;
using test_sap.HttpClient.DTO;
using Newtonsoft.Json;

namespace test_sap
{
    static class Program
    {
        public class PayloadCreateSomething
        {
            public DateTimeOffset Date { get; set; }
            public int TemperatureCelsius { get; set; }
            public string Summary { get; set; }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            HttpClientService c = new HttpClientService();
            HttpRequestResponseDTO<string> get = await c.ApiCall("http://reqbin.com/echo/get/json", HttpClientService.RequestMethod.GET);

            var payload = new PayloadCreateSomething
            {
                Date = DateTime.Parse("2019-08-01"),
                TemperatureCelsius = 25,
                Summary = "Hot"
            };

            string jsonString = JsonConvert.SerializeObject(payload);

            HttpRequestResponseDTO<string> post = await c.ApiCall("http://reqbin.com/echo/get/json", HttpClientService.RequestMethod.GET, jsonString);

            PayloadCreateSomething res = JsonConvert.DeserializeObject<PayloadCreateSomething>(post.Result);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
