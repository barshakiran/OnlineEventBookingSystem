using System;
using OnlineEventBookingSystemDAL;

namespace OnlineEventBookingSystemDomain
{
    public class EventLocationDomainModel
    {
        public int Location_Id { get; set; }
        public string City { get; set; }
       // public int Event_Id { get; set; }
        public decimal EventLocation_Price { get; set; }
        public DateTime EventLocation_DateAndTime { get; set; }


    }
}
