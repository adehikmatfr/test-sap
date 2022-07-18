# how to using http client
package required :
- Newtonsoft.Json

how to intall package Newtonsoft ?
1. click reight in your project
2. select manage Nuget Package

![Alt text](https://i.im.ge/2022/07/18/FuEWKJ.png)

3. select browse and search Newtonsoft

![Alt text](https://i.im.ge/2022/07/18/FuEnSX.png)

4. install this package

#example GET
```C#
 public class Something
 {
            public DateTimeOffset Date { get; set; }
            public int TemperatureCelsius { get; set; }
            public string Summary { get; set; }
 }
```
```C#
HttpClientService c = new HttpClientService();
HttpRequestResponseDTO<string> apiCall = await c.ApiCall("http://reqbin.com/echo/get/json", HttpClientService.RequestMethod.GET);
PayloadCreateSomething res = JsonConvert.DeserializeObject<Something>(apiCall.Result);
```

#example POST 
```C#
var payload = new PayloadCreateSomething
            {
                Date = DateTime.Parse("2019-08-01"),
                TemperatureCelsius = 25,
                Summary = "Hot"
            };
string jsonString = JsonConvert.SerializeObject(payload);
HttpRequestResponseDTO<string> post = await c.ApiCall("http://reqbin.com/echo/get/json", HttpClientService.RequestMethod.POST, jsonString);
```
