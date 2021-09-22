using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEventBookingSystemAPI.Models
{
    public class UserEventDetailsModel : EventDetailModel
    {
        public string Booking_Loc { get; set; }
        public decimal EventLocation_Price { get; set; }
        public DateTime Booking_Date { get; set; }
    }
}