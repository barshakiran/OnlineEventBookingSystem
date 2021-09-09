using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineEventBookingSystemUI.App_Code
{


    public enum EventTypes
    {
        [Display(Name = "Movie")]
        Movie,

        [Display(Name = "Stand Up Comedy")]
        Stand_up_comedy,

        [Display(Name = "Music")]
        Musical,

        [Display(Name = "Play")]
        Theater
    }
}