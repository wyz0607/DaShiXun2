using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelMVC.Models
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public string  UserName { get; set; }//用户名
        public string UserPhone { get; set; }//手机号
        public string UserPwd { get; set; }//密码
        public string UserSex { get; set; }//性别
        public string UserBirth { get; set; }//出生日期
        public int User_Loc_Prov { get; set; }
        public int User_Loc_City { get; set; }
        public int User_Loc_Coun { get; set; }
        public string User_DocumentType { get; set; }//证件类型
        public string User_IDNumber { get; set; }//证件号
        public string User_Role { get; set; }//角色


        //外键用到字段
        public string RName { get; set; }      //三级联动时显示名称
        public string CityName { get; set; }
        public string CounName { get; set; }
    }
}