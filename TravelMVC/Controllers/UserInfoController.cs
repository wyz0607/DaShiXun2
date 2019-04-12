using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelMVC.Models;
using Newtonsoft.Json;

namespace TravelMVC.Controllers
{
    public class UserInfoController : Controller
    {
        #region 登录
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public void Login(string UserName = "", string UserPwd = "")
        {
            if (UserName == "")
            {
                Response.Write("<script>alert('用户名不可为空,请重新登录');location.href='/UserInfo/Login';</script>");
            }
            else if (UserPwd == "")
            {
                Response.Write("<script>alert('密码不可为空,请重新登录');location.href='/UserInfo/Login';</script>");
            }
            else
            {
                string jsonResult = HttpClientHelper.Send("get", $"api/UserInfoApi/GetLoginUser?UserName={UserName}&UserPwd={UserPwd}");
                UserInfo user = JsonConvert.DeserializeObject<UserInfo>(jsonResult);
                if (user == null)
                {
                    Response.Write("<script>alert('用户名或密码有误,请重新登录');location.href='/UserInfo/Login';</script>");
                }
                else
                {
                    Session["User"] = user;
                    Response.Write("<script>location.href='/UserInfo/Login';</script>");
                }
            }
        }
        #endregion
        #region UserInfo列表、添加管理员
        //获取所有下拉列表的数据方法
        public string BindSelect(int Id = 0)
        {
            string jsonResult = HttpClientHelper.Send("get", $"api/UserInfoApi/ShowRegion/{Id}");
            List<Region> list = JsonConvert.DeserializeObject<List<Region>>(jsonResult);
            return JsonConvert.SerializeObject(list);
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(UserInfo user)
        {
            return View();
        }
        #endregion
    }
}