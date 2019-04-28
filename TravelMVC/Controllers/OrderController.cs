using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TravelMVC.Models;

namespace TravelMVC.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            //Dictionary<string,string> dic= null;
            //string none = DataTransfer.GetNonce().ToString();
            //string timeaTamp= DataTransfer.GetTimeStamp();

            Dictionary<string, string> keys = new Dictionary<string, string>();
            string code = "";
            var ck = Request.Cookies.Get("MyCookie");
            if (ck != null)
            {
                code = ck.Values["Code"];
            }

            var singTrue = DataTransfer.GetMD5Staff(keys, code);

            var result = HttpApiSecretHelper.Send("get", "api/OrderApi/ShowOrder","",singTrue,code);
            if (!result.Contains("错误"))
            {
                var list = JsonConvert.DeserializeObject<List<Order>>(result);
                return View(list);
            }
            else
            {
                return View(result);
            }
            
        }


        public string GetOneUser(int id)
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();
            keys.Add("id",id.ToString());
            string code = "";
            var ck = Request.Cookies.Get("MyCookie");
            if (ck != null)
            {
                code = ck.Values["Code"];
            }

            var singTrue = DataTransfer.GetMD5Staff(keys, code);
            return HttpApiSecretHelper.Send("get", "api/UserInfoApi/GetOneUser?id="+id, "", singTrue, code);
            
        }
    }
}