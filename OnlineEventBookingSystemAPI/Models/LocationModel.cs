using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEventBookingSystemAPI.Models
{
    public class LocationModel
    {
        public int Location_Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}