using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelMVC.Models;
using System.Data;
using Newtonsoft.Json;

namespace TravelMVC.Controllers
{
    public class NewsController : Controller
    {
        public static List<NewsList> sList;
        //添加新闻
        [HttpGet]
        public ActionResult AddNews()
        {
            string result = HttpClientHelper.Send("get", "api/NewsApi/ShowNewTypes", null);
            var ntype = JsonConvert.DeserializeObject<List<Newstype>>(result);
            SelectList slist = new SelectList(ntype, "T_Id", "N_Name");
            ViewBag.slist = slist;

            return View();
        }
        [HttpPost]
        public ActionResult AddNews(NewsList sce)
        {
            string strJson = JsonConvert.SerializeObject(sce);
            string result = HttpClientHelper.Send("post", "api/NewsApi/AddNews", strJson);
            if (result.Contains("成功"))
            {
                return Redirect("/News/ShowNews");
            }
            else
            {
                return Content("<script>alert('" + result + "')</script>");

            }

        }
     


        //显示新闻
        [HttpGet]
        public ActionResult ShowNews(int pageindex = 1)
        {
            string result = HttpClientHelper.Send("get", "api/NewsApi/ShowNews", null);
            List<NewsList> sce = JsonConvert.DeserializeObject<List<NewsList>>(result);
            sList = sce;
            ViewBag.currentindex = pageindex;
            ViewBag.totaldata = sce.Count;
            ViewBag.totalpage = Math.Round((sce.Count() * 1.0) / 6);
            return View(sce.Skip((pageindex - 1) * 6).Take(6).ToList());

        }
        //分页
        public ActionResult News(int pageIndex, int pageSize = 6)
        {
            ViewBag.pIndex = pageIndex;
            string json = HttpClientHelper.Send("get", "api/NewsApi/ShowNews", null);
            List<NewsList> menu = JsonConvert.DeserializeObject<List<NewsList>>(json);
            string json1 = JsonConvert.SerializeObject(menu.Skip((pageIndex - 1) * pageSize).Take(pageSize));
            return Content(json1);
        }

        //删除新闻
        public ActionResult DelNews(int id)
        {
            string strId = id.ToString();
            //string str = "";
            //string[] ids = strId.Split(',');
            //foreach (var item in ids)
            //{
            int result = Convert.ToInt32(HttpClientHelper.Send("delete", "api/NewsApi/DelNews?Id=" + strId));
            //}

            if (result>0)
            {
                return Redirect("/News/ShowNews");

            }
            else
            {
                return Content("删除失败");
            }
        }
        //修改
        [HttpGet]
        public ActionResult UptNews(int id)
        {
            Session["id"] = id;
            string result = HttpClientHelper.Send("get", "api/NewsApi/ShowNewTypes", null);
            var ntype = JsonConvert.DeserializeObject<List<Newstype>>(result);
            SelectList slist = new SelectList(ntype, "T_Id", "N_Name");
            ViewBag.slist = slist;
            string str2 = HttpClientHelper.Send("get", "api/NewsApi/UptNews?id=" + id);
            List<NewsList> list = JsonConvert.DeserializeObject<List<NewsList>>(str2);
            //NewsList list1 = list.Where(c => c.N_Id == id).FirstOrDefault();
            return View(list);
        }
        [HttpPost]
        public ActionResult UptNews(NewsList kit)
        {
            kit.N_Id = Convert.ToInt32(Session["id"]);
            string jsonstr = JsonConvert.SerializeObject(kit);
            string str = HttpClientHelper.Send("post", "api/NewsApi/UptNews", jsonstr);
            if (str.Contains("成功"))
            {
                return Redirect("/News/ShowNews");
            }
            else
            {
                return Content("修改失败");
            }
        }

    }
}