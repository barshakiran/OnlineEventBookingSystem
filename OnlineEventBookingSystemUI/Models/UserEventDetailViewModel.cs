using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OnlineEventBookingSystemUI.Models
{
  
    
    public class UserEventDetailViewModel:EventDetailViewModel
    {
        public List<SelectListItem> CityList { get; set; } = new List<SelectListItem>();

        [Display(Name = " Location :  ")]
        public string Booking_Loc { get; set; }

        [Display(Name = " Price :  ")]
        public decimal EventLocation_Price { get; set; }

        [Display(Name = " Date & Time :  ")]
        public DateTime Booking_Date { get; set; }

    }



}