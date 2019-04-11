using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelMVC.Models
{
    public class Order
    {
        public int O_OrderID { get; set; }
        public int O_UserID { get; set; }
        public int O_GoodsID { get; set; }
        public string O_GoodsType { get; set; }
        public string O_OrderDate { get; set; }
        public double O_TotalPrice { get; set; }
        public int O_OrderState { get; set; }
      
    }
}