﻿using System;
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
            string str = HttpClientHelper.Send("get", "api/Values/get",null);
            HttpCookie cookie = new HttpCookie("MyCookie");
            cookie.Expires = DateTime.Now.AddMinutes(20);
            cookie["Code"] = str;
            Response.Cookies.Add(cookie);

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
                string jsonResult;
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("UserName", UserName);
                data.Add("UserPwd", UserPwd);

                string code = "";
                var ck = Request.Cookies.Get("MyCookie");
                if (ck != null)
                {
                    code = ck.Values["Code"];
                }

                jsonResult = HttpClientHelper.Send("get", $"api/Values/GetLoginUser?UserName={UserName}&UserPwd={UserPwd}");
                UserInfo user = JsonConvert.DeserializeObject<UserInfo>(jsonResult);



                if (user == null || user.User_Role != "管理员")
                {
                    Response.Write("<script>alert('用户名或密码有误,请重新登录');location.href='/UserInfo/Login';</script>");
                }
                else
                {
                    Session["User"] = user;
                    Response.Write("<script>location.href='/UserInfo/ShowAdmin';</script>");
                }
            }
        }
        #endregion
        #region 添加管理员
        //绑定第一个下拉框方法
        public void BindSelect(int Id = 0)
        {
            string jsonResult = HttpClientHelper.Send("get", $"api/UserInfoApi/ShowRegion/{Id}");
            List<Region> list = JsonConvert.DeserializeObject<List<Region>>(jsonResult);
            SelectList s = new SelectList(list, "R_Id", "R_Name");
            ViewBag.s = s;
        }
        //其它下拉框绑定数据电泳方法
        public string BindSelect2(int Id)
        {
            string jsonResult = HttpClientHelper.Send("get", $"api/UserInfoApi/ShowRegion/{Id}");
            List<Region> list = JsonConvert.DeserializeObject<List<Region>>(jsonResult);
            return JsonConvert.SerializeObject(list);
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            BindSelect();
            return View();
        }
        [HttpPost]
        public void AddUser(UserInfo user)
        {
            BindSelect();
            user.User_Role = "管理员";
            string json = JsonConvert.SerializeObject(user);
            string jsonResult = HttpClientHelper.Send("post", "api/UserInfoApi/AddUser", json);
            int Result = JsonConvert.DeserializeObject<int>(jsonResult);
            if (Result > 0)
            {
                Response.Write("<script>alert('添加成功');location.href='/UserInfo/AddUser';</script>");
            }
            else
            {
                Response.Write("<script>alert('添加失败');location.href='/UserInfo/AddUser';</script>");
            }
        }
        #endregion
        #region UserInfo列表显示
        //获取所有用户方法
        public string GetAllUser(string Role_Name, string UserPhone = "", string UserName = "")
        {
            var code = "";
            Dictionary<string, string> keys = new Dictionary<string, string>();

            var ck = Request.Cookies.Get("MyCookie");
            if (ck != null)
            {
                 code = ck.Values["Code"];
            }

            var singTrue= DataTransfer.GetMD5Staff(keys,code);
            string jsonResult = HttpApiSecretHelper.Send("get", "api/UserInfoApi/ShowUser","",singTrue,code);
            List<UserInfo> list = JsonConvert.DeserializeObject<List<UserInfo>>(jsonResult);
            List<UserInfo> ListAdmin = new List<UserInfo>();
            List<UserInfo> ListCommon = new List<UserInfo>();
            if (UserPhone != "")
            {
                list = list.Where(s => s.UserPhone.Contains(UserPhone)).ToList();
            }
            if (UserName != "")
            {
                list = list.Where(s => s.UserName.Contains(UserName)).ToList();
            }
            foreach (var item in list)
            {
                if (item.User_Role.Equals("管理员"))
                {
                    ListAdmin.Add(item);
                }
                else
                {
                    ListCommon.Add(item);
                }
            }
            if (Role_Name == "管理员")
            {
                return JsonConvert.SerializeObject(ListAdmin);
            }
            else
            {
                return JsonConvert.SerializeObject(ListCommon);
            }
        }
        //显示管理员页面
        public ActionResult ShowAdmin()
        {

            return View();
        }
        //显示用户页面
        public ActionResult ShowCommon()
        {
            return View();
        }
        #endregion
        #region 删除用户
        //删除用户的信息
        public void DelUserInfo(int Id)
        {
            string jsonResult = HttpClientHelper.Send("delete", $"api/UserInfoApi/DelUser/{Id}");
            if (JsonConvert.DeserializeObject<int>(jsonResult) > 0)
            {
                Response.Write("<script>alert('删除成功');location.href='/UserInfo/ShowCommon';</script>");
            }
            else
            {
                Response.Write("<script>alert('删除失败');location.href='/UserInfo/ShowCommon';</script>");
            }
        }
        #endregion
        #region 个人资料
        public ActionResult MyPage()
        {
            UserInfo user = Session["User"] as UserInfo;
            return View(user);
        }
        #endregion
        #region 修改密码
        [HttpGet]
        public ActionResult UpdPwd()
        {
            return View();
        }
        public void ChangePwd(string NewPwd)
        {
            UserInfo user = Session["User"] as UserInfo;
            user.UserPwd = NewPwd;
            string json = JsonConvert.SerializeObject(user);
            string jsonResult = HttpClientHelper.Send("put", $"api/UserInfoApi/UpdUser", json);
            int result = JsonConvert.DeserializeObject<int>(jsonResult);
            if (result > 0)
            {
                Response.Write("<script>location.href='/UserInfo/Login';</script>");
            }
            else
            {
                Response.Write("<script>alert('修改失败,请重试');location.href='/UserInfo/UpdPwd';</script>");
            }
        }
        //判断输入的原密码是否正确
        public int HaveOrNo(string InputPwd)
        {
            UserInfo user = Session["User"] as UserInfo;
            if (InputPwd == user.UserPwd)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        #endregion
        #region 列表某人的详情显示
        public ActionResult ShowSomeOne(int Id)
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();
            keys.Add("Id", Id.ToString());
            string jsonResult = HttpClientHelper.Send("get", "api/UserInfoApi/ShowUser", "");
            List<UserInfo> list = JsonConvert.DeserializeObject<List<UserInfo>>(jsonResult);
            UserInfo user = list.Where(s => s.UserID == Id).FirstOrDefault();
            return View(user);
        }
        #endregion
    }
}