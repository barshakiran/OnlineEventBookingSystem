using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEventBookingSystemUI.Models
{
    public class UserEventViewModel
    {
        public string Event_Name { get; set; }
        public string Event_Type { get; set; }
        public string Event_Description { get; set; }
        public string Event_Picture { get; set; }


        public string City { get; set; }

        public decimal EventLocation_Price { get; set; }


        public DateTime EventLocation_DateAndTime { get; set; }
    }
}