using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelMVC.Models
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public string  UserName { get; set; }
        public string UserPhone { get; set; }
        public string UserPwd { get; set; }
        public string UserSex { get; set; }
        public string UserBirth { get; set; }
        public int User_Loc_Prov { get; set; }
        public int User_Loc_City { get; set; }
        public int User_Loc_Coun { get; set; }
        public string User_DocumentType { get; set; }
        public string User_IDNumber { get; set; }
        public string User_Role { get; set; }
     
    }
}