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


            var result = HttpClientHelper.Send("get", "api/OrderApi/ShowOrder","");
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
    }
}