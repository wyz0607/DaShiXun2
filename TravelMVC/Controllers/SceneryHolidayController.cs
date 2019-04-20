using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelMVC.Models;
using Newtonsoft.Json;
using System.IO;

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
        public string Upload(HttpPostedFileBase file)
        {
            string Name = file.FileName;
            string FileName = Server.MapPath("~/Upload");
            if (!Directory.Exists(FileName))
            {
                Directory.CreateDirectory(FileName);
            }
            file.SaveAs(Path.Combine(FileName, Name));
            return JsonConvert.SerializeObject(new { path = "~/Upload/" + file.FileName, code = 0 });
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
            return View();
        }
        [HttpGet]
        public ActionResult ShowScenery1()
        {
            string result = HttpClientHelper.Send("get", "api/SceneryHolidayApi/ShowAllScenery");
            return Content(result);
        }
        public ActionResult ShowRegion(int Id)
        {
            string result = HttpClientHelper.Send("get", "api/UserInfoApi/ShowRegion?Id=" + Id);
            return Content(result);
        }
        [HttpGet]
        public ActionResult ShowSceneryType()
        {
            string result = HttpClientHelper.Send("get", "api/SceneryHolidayApi/ShowSceneryType");
            return Content(result);
        }
        [HttpGet]
        public ActionResult ShowHoliday1()
        {
            string result = HttpClientHelper.Send("get", "api/SceneryHolidayApi/ShowHoliday"); 
            return Content(result);
        }
        [HttpGet]
        public ActionResult ShowParticipation1()
        {
            string result = HttpClientHelper.Send("get", "api/SceneryHolidayApi/ShowAllParticipation"); 
            return Content(result);
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
        public ActionResult DelScenery(int S_Id)
        {
            string result = HttpClientHelper.Send("delete", "api/SceneryHolidayApi/DelScenery?S_Id=" + S_Id, null);
            if (result.Contains("成功"))
            {
                return Content("删除成功");
            }
            else
            {
                return Content("删除失败");
            }
        }
        [HttpPost]
        public ActionResult UptScenery(Scenery scenery)
        {
            string result = HttpClientHelper.Send("put", "api/SceneryHolidayApi/UptScenery", JsonConvert.SerializeObject(scenery));
            if (result.Contains("成功"))
            {
                return Content("修改成功");
            }
            else
            {
                return Content("修改失败");
            }
        }
        [HttpGet]
        public ActionResult ShowHoliday()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ShowParticipation()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DelParticipation(int P_Id)
        {
            string result = HttpClientHelper.Send("delete", "api/SceneryHolidayApi/DelParticipation?P_Id=" + P_Id, null);
            if (result.Contains("成功"))
            {
                return Content("删除成功");
            }
            else
            {
                return Content("删除失败");
            }
        }
    }
}