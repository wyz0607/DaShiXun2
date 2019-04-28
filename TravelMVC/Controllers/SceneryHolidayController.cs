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
            string code = "";
            var ck = Request.Cookies.Get("MyCookie");
            if (ck != null)
            {
                code = ck.Values["Code"];
            }
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (var item in typeof(Scenery).GetProperties())
            {
                data.Add(item.Name, item.GetValue(scenery).ToString());
            }
            //singTrue = code + staffId + data; staffId是只有服务器和客户端知道的私钥
            string singtrue = DataTransfer.GetMD5Staff(data, code);
            string json = JsonConvert.SerializeObject(scenery);
            string result = HttpApiSecretHelper.Send("post", "api/SceneryHolidayApi/AddScenery", json, singtrue, code);
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
            var code = "";
            Dictionary<string, string> keys = new Dictionary<string, string>();
            var ck = Request.Cookies.Get("MyCookie");
            if (ck != null)
            {
                code = ck.Values["Code"];
            }

            var singTrue = DataTransfer.GetMD5Staff(keys, code);
            string result = HttpApiSecretHelper.Send("get", "api/SceneryHolidayApi/ShowAllScenery","",singTrue,code);
            return Content(result);
        }
        public ActionResult ShowRegion(int Id)
        {
            var code = "";
            Dictionary<string, string> keys = new Dictionary<string, string>();
            keys.Add("Id", Id.ToString());
            var ck = Request.Cookies.Get("MyCookie");
            if (ck != null)
            {
                code = ck.Values["Code"];
            }

            var singTrue = DataTransfer.GetMD5Staff(keys, code);
            string result = HttpApiSecretHelper.Send("get", "api/UserInfoApi/ShowRegion?Id=" + Id,JsonConvert.SerializeObject(Id),singTrue,code);
            return Content(result);
        }
        [HttpGet]
        public ActionResult ShowSceneryType()
        {
            var code = "";
            Dictionary<string, string> keys = new Dictionary<string, string>();

            var ck = Request.Cookies.Get("MyCookie");
            if (ck != null)
            {
                code = ck.Values["Code"];
            }
            var singTrue = DataTransfer.GetMD5Staff(keys, code);
            string result = HttpApiSecretHelper.Send("get", "api/SceneryHolidayApi/ShowSceneryType", "", singTrue, code);
            return Content(result);
        }
        [HttpGet]
        public ActionResult ShowHoliday1()
        {
            var code = "";
            Dictionary<string, string> keys = new Dictionary<string, string>();

            var ck = Request.Cookies.Get("MyCookie");
            if (ck != null)
            {
                code = ck.Values["Code"];
            }
            var singTrue = DataTransfer.GetMD5Staff(keys, code);
            string result = HttpApiSecretHelper.Send("get", "api/SceneryHolidayApi/ShowHoliday", "", singTrue, code);
            return Content(result);
        }
        [HttpGet]
        public ActionResult ShowParticipation1()
        {
            string code = "";
            var ck = Request.Cookies.Get("MyCookie");
            if (ck != null)
            {
                code = ck.Values["Code"];
            }
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
            string code = "";
            var ck = Request.Cookies.Get("MyCookie");
            if (ck != null)
            {
                code = ck.Values["Code"];
            }
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (var item in typeof(Holiday).GetProperties())
            {
                data.Add(item.Name, item.GetValue(holiday).ToString());
            }
            //singTrue = code + staffId + data; staffId是只有服务器和客户端知道的私钥
            string singtrue = DataTransfer.GetMD5Staff(data, code);
            string json = JsonConvert.SerializeObject(holiday);
            string result = HttpApiSecretHelper.Send("post", "api/SceneryHolidayApi/AddHoliday", json, singtrue, code);
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
            var code = "";
            Dictionary<string, string> keys = new Dictionary<string, string>();
            keys.Add("S_Id", S_Id.ToString());
            var ck = Request.Cookies.Get("MyCookie");
            if (ck != null)
            {
                code = ck.Values["Code"];
            }

            var singTrue = DataTransfer.GetMD5Staff(keys, code);
            string result = HttpApiSecretHelper.Send("delete", "api/SceneryHolidayApi/DelScenery?S_Id=" + S_Id, JsonConvert.SerializeObject(S_Id), singTrue, code);
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
            string code = "";
            var ck = Request.Cookies.Get("MyCookie");
            if (ck != null)
            {
                code = ck.Values["Code"];
            }
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (var item in typeof(Scenery).GetProperties())
            {
                data.Add(item.Name, item.GetValue(scenery).ToString());
            }
            //singTrue = code + staffId + data; staffId是只有服务器和客户端知道的私钥
            string singtrue = DataTransfer.GetMD5Staff(data, code);
            string json = JsonConvert.SerializeObject(scenery);
            string result = HttpApiSecretHelper.Send("put", "api/SceneryHolidayApi/UptScenery", JsonConvert.SerializeObject(scenery),singtrue,code);
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
            var code = "";
            Dictionary<string, string> keys = new Dictionary<string, string>();
            keys.Add("P_Id", P_Id.ToString());
            var ck = Request.Cookies.Get("MyCookie");
            if (ck != null)
            {
                code = ck.Values["Code"];
            }

            var singTrue = DataTransfer.GetMD5Staff(keys, code);
            string result = HttpApiSecretHelper.Send("delete", "api/SceneryHolidayApi/DelScenery?P_Id=" + P_Id, JsonConvert.SerializeObject(P_Id), singTrue, code);
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