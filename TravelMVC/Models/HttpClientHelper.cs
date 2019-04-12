using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TravelMVC.Models
{
    public class HttpClientHelper
    {
        public static string Send(string methed, string apimethed, string JsonStr)
        {
            Uri uri = new Uri("http://localhost:61521/");
            HttpClient client = new HttpClient();
            client.BaseAddress = uri;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage sponse = null;
            switch (methed)
            {
                case "get":
                    sponse = client.GetAsync(apimethed).Result;
                    break;
                case "post":
                    HttpContent content = new StringContent(JsonStr);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    sponse = client.PostAsync(apimethed, content).Result;
                    break;
                case "put":
                    HttpContent content1 = new StringContent(JsonStr);
                    content1.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    sponse = client.PutAsync(apimethed, content1).Result;
                    break;
                case "delete":
                    sponse = client.DeleteAsync(apimethed).Result;
                    break;

            }
            if (sponse.IsSuccessStatusCode)
            {
                return sponse.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return "未知错误";
            }
        }
    }
}