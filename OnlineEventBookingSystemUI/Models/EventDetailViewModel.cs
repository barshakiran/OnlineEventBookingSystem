using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineEventBookingSystemUI.Models
{
    public class EventDetailViewModel
    {
        public int Event_Id { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = " Event Name :  ")]
        public string Event_Name { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = " Event Type :  ")]
        public string Event_Type { get; set; }

       
        [Display(Name = " Description :  ")]
        [DataType(DataType.MultilineText)]
        public string Event_Description { get; set; }

       
        [Display(Name = " Picture :  ")]
        public string Event_Picture { get; set; }
    }
}