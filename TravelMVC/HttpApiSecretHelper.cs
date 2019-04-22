using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace TravelMVC
{
    public class HttpApiSecretHelper
    {
        public static string Send(string methed, string apimethed, string data ="", string singTrue="",string code="")
        {
            Uri uri = new Uri("http://localhost:61521/");
            HttpClient client = new HttpClient();
            client.BaseAddress = uri;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //将时间戳,随机数,令牌 添加到请求头中
            client.DefaultRequestHeaders.Add("code", code);
            client.DefaultRequestHeaders.Add("singtrue", singTrue);

            HttpResponseMessage sponse = null;
            switch (methed.ToLower())
            {
                case "get":
                    sponse = client.GetAsync(apimethed).Result;
                    break;
                case "post":
                    HttpContent content = new StringContent(data);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    sponse = client.PostAsync(apimethed, content).Result;
                    break;
                case "put":
                    HttpContent content1 = new StringContent(data);
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