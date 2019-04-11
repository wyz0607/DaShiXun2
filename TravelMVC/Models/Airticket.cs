using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelMVC.Models
{
    public class Airticket
    {
        public int A_Id { get; set; }
        public string A_CourseStart { get; set; }
        public string A_CourseEnd { get; set; }
        public string A_Date { get; set; }
        public double A_Price { get; set; }
        public double A_Expenses { get; set; }
        public string A_Meals { get; set; }
        public string A_Flight { get; set; }
        public int A_Num { get; set; }
    }
}