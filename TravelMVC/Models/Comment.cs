using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelMVC.Models
{
    public class Comment
    {

        public int C_CommentId { get; set; }
        public int C_UserId { get; set; }
        public int C_GoodsId { get; set; }
        public string C_GoodsType { get; set; }
        public string C_Comments { get; set; }
        public int C_ReviewState { get; set; }
        public string C_Ctime { get; set; }
        public string C_Cname { get; set; }
        public double C_Score { get; set; }
    }
}