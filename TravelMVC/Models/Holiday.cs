using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelMVC.Models
{
    public class Holiday
    {
        public int H_Id { get; set; }
        public string H_Theme { get; set; }
        public string H_Destination { get; set; }
        public double H_RetailPrice { get; set; }
        public double H_FormerPrice { get; set; }
        public string H_Data { get; set; }
        public string H_Traffic { get; set; }
        public int H_TravelDays { get; set; }
        public string H_Participant { get; set; }
        public string H_Explain { get; set; }
        public string H_Photo { get; set; }
    }
}