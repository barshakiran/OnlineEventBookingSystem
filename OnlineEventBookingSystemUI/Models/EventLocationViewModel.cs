using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineEventBookingSystemUI.Models
{
    public class EventLocationViewModel
    {
        [Display(Name = " City :  ")]
        public int? Location_Id { get; set; }

        [Display(Name = " City Name:  ")]
        public string City { get; set; }

        [Display(Name = " Price :  ")]
        public decimal EventLocation_Price { get; set; }

        [Display(Name = " Date :  ")]
        public System.DateTime EventLocation_DateAndTime { get; set; }

        public List<SelectListItem> Cities { get; set; } = new List<SelectListItem>();

    }
}