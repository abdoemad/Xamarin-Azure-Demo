using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EShope.Services.Infra.Imp
{
    public class APIConsumer : IAPIConsumer
    {
        string _defaultEndPoint = "http://eshopemobile.azurewebsites.net/api";
        public APIConsumer()
        {
        }

        public string DefaultEndPoint
        {
            get
            {
#if Debug
                return "http://127.0.0.1:5500/api";
#else
                return _defaultEndPoint;
#endif
            }
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            HttpResponseMessage response = await httpClient.GetAsync(uri);
            string serialized = await response.Content.ReadAsStringAsync();

            var result = await Task.Run(() =>
               JsonConvert.DeserializeObject<T>(serialized));

            return result;
        }

        public async Task<TResult> PostAsync<T, TResult>(string uri, T data)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized));

            return result;
        }
    }
}
