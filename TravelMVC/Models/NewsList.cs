using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelMVC.Models
{
    public class NewsList
    {
        public int N_Id { get; set; }
        public int N_TypeId { get; set; }
        public string N_MainTitle { get; set; }
        public string N_Author { get; set; }
        public int N_Category { get; set; }
        public string N_Content { get; set; }
        public string N_DateTime { get; set; }
        public string N_Name { get; set; }
        public string  N_Photo { get; set; }
        public int N_Num { get; set; }//点赞数
        public bool Is_Love { get; set; }

    }
}