using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEventBookingSystemAPI.Models
{
    public class EventLocationModel
    {
        public int Location_Id { get; set; }
        public string City { get; set; }
        public decimal EventLocation_Price { get; set; }
        public DateTime EventLocation_DateAndTime { get; set; }
    }
}