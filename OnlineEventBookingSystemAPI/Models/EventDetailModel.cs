using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEventBookingSystemAPI.Models
{
    public class EventDetailModel
    {
        public int Event_Id { get; set; }
        public string Event_Name { get; set; }
        public string Event_Type { get; set; }
        public string Event_Description { get; set; }
        public string Event_Picture { get; set; }
    }
}