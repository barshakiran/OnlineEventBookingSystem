using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineEventBookingSystemUI.Models
{
    public class EventLocationViewModel
    {
        public int Location_Id { get; set; }

        [Display(Name = " City :  ")]
        public string City { get; set; }

        [Display(Name = " Price :  ")]
        public decimal EventLocation_Price { get; set; }

        [Display(Name = " Date :  ")]
        public System.DateTime EventLocation_DateAndTime { get; set; }

    }
}