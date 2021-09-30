using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineEventBookingSystemUI.Models
{
    public class EventDetailViewModel
    {
        public EventDetailViewModel()
        {
            EventTypeList = new List<SelectListItem>();
            EventList = new List<EventLocationViewModel>();
        }
        [Display(Name = "ID ")]
        public int Event_Id { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = " Event Name ")]
        public string Event_Name { get; set; }

        [Display(Name = " Event Type ")]
        public string Event_Type { get; set; }

       
        [Display(Name = " Description ")]
        [DataType(DataType.MultilineText)]
        public string Event_Description { get; set; }

 
        [Display(Name = " Picture ")]
        public string Event_Picture { get; set; }

        public List<EventLocationViewModel> EventList { get; set; } = new List<EventLocationViewModel>();
        public List<SelectListItem> EventTypeList { get; set; }
    }

    public enum EventTypes
    {
        [Display(Name = "Movie")]
        Movie,

        [Display(Name = "Stand Up Comedy")]
        StageShow,

        [Display(Name = "Musical")]
        Music,

        [Display(Name = "Play")]
        Theater
    }
}