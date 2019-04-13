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
            var result = HttpClientHelper.Send("get", "api/OrderApi/ShowOrder",null);
            var list = JsonConvert.DeserializeObject<List<Order>>(result);
            return View(list);
        }
    }
}