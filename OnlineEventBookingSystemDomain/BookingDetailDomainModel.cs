using OnlineEventBookingSystemDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEventBookingSystemDomain
{
    public class BookingDetailDomainModel:UserEventDetailsDomainModel
    {
        public int Booking_Id { get; set; }
        //public int User_Id { get; set; }
        public string UserName { get; set; }
        //public DateTime Booking_Date { get; set; }
        public int Booking_TicketCount { get; set; }
        public decimal Booking_TotalAmount { get; set; }
        public bool IsConfirmationSent { get; set; }
       // public string Booking_Loc { get; set; }
        public string Payment_Mode { get; set; }
    }
}
