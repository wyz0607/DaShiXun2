using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelMVC.Models;
using Newtonsoft.Json;

namespace TravelMVC.Controllers
{
    public class SceneryHolidayController : Controller
    {
        // GET: SceneryHoliday
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPhoto(HttpPostedFileBase file)
        {
            if (file != null)
            {
                //获取images绝对路径
                string path = Server.MapPath("/Images/");
                string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + file.FileName;
                file.SaveAs(path + fileName);
                return Content("http://localhost:54970/Images/" + fileName);
            }
            return View();
        }
        [HttpGet]
        public ActionResult AddScenery()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddScenery(Scenery scenery)
        {
            string json = JsonConvert.SerializeObject(scenery);
            string result=HttpClientHelper.Send("post", "api/SceneryHolidayApi/AddScenery", json);
            if (result.Contains("成功"))
            {
                return Content("添加成功");
            }
            else
            {
                return Content("添加失败");
            }
        }
        [HttpGet]
        public ActionResult ShowScenery()
        {
            string json=HttpClientHelper.Send("get", "api/SceneryHolidayApi/ShowScenery",null);
            return View(JsonConvert.DeserializeObject<List<Scenery>>(json));
        }
        [HttpGet]
        public ActionResult AddHoliday()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddHoliday(Holiday holiday)
        {
            string json = JsonConvert.SerializeObject(holiday);
            string result = HttpClientHelper.Send("post", "api/SceneryHolidayApi/AddHoliday", json);
            if (result.Contains("成功"))
            {
                return Content("添加成功");
            }
            else
            {
                return Content("添加失败");
            }
        }
        [HttpGet]
        public ActionResult ShowHoliday()
        {
            string json = HttpClientHelper.Send("get", "api/SceneryHolidayApi/ShowHoliday",null);
            return View(JsonConvert.DeserializeObject<List<Holiday>>(json));
        }
        [HttpGet]
        public ActionResult ShowParticipation()
        {
            string json = HttpClientHelper.Send("get", "api/SceneryHolidayApi/ShowParticipation", null);
            return View(JsonConvert.DeserializeObject<List<Participation>>(json));
        }
    }
}