using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineEventBookingSystemUI.Models
{
  
    
    public class UserEventDetailViewModel
    {
        public List<SelectListItem> EventTypeList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> CityList { get; set; } = new List<SelectListItem>();
        public string City { get; set; }
        public string Event_Type { get; set; }
      //  public string TestData { get; set; }
        // public List<SelectListItem> DateList { get; set; } = new List<SelectListItem>();
    }



}