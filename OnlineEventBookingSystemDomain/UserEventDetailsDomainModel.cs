﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineEventBookingSystemDAL;

namespace OnlineEventBookingSystemDomain
{
    public class UserEventDetailsDomainModel:EventDetailDomainModel
    {
        public string Booking_Loc { get; set; }
        public decimal EventLocation_Price { get; set; }
        public DateTime Booking_Date { get; set; }      
    }
}
