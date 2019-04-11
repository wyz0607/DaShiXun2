using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelMVC.Models
{
    public class Participation
    {
        public int P_Id { get; set; }
        public int U_Id { get; set; }
        public int P_HolidayId { get; set; }
    }
}