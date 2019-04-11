using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelMVC.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public void Login(string UserName="",string UserPwd="")
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
                string jsonResult = HttpClientHelper.Send("get", "");
            }
        }
    }
}