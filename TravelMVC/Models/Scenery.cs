using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelMVC.Models
{
    public class Scenery
    {
        public int S_Id { get; set; }
        public string S_Name { get; set; }
        public int S_Genus_Prov { get; set; }
        public int S_Genus_City { get; set; }
        public int S_Genus_Coun { get; set; }
        public string S_Level { get; set; }
        public int S_Type { get; set; }
        public string S_Fit { get; set; }
        public double S_Grade { get; set; }
        public string S_Price { get; set; }
        public string S_Introduction { get; set; }
        public string S_Photo { get; set; }
       
    }
}