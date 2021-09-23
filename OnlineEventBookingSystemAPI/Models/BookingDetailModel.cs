using OnlineEventBookingSystemDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEventBookingSystemAPI.Models
{
    public class BookingDetailModel:UserEventDetailsModel
    {
        public int Booking_Id { get; set; }
        public int Location_Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Booking_TicketCount { get; set; }
        public decimal Booking_TotalAmount { get; set; }
        public string Payment_Mode { get; set; }
        public bool IsConfirmationSent { get; set; }
    }
}