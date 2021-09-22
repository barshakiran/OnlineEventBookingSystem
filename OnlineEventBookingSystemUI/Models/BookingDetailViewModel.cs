using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OnlineEventBookingSystemUI.Models
{
    public class BookingDetailViewModel:UserEventDetailViewModel
    {

        [Display(Name = " Booking No :  ")]
        public int Booking_Id { get; set; }
        public int Location_Id { get; set; }

        [Display(Name = " Name :  ")]
        public string userName { get; set; }

        //[Required(ErrorMessage = "This field is Required", AllowEmptyStrings = false)]
        [Range(1, 10, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = " No of Tickets :  ")]
        public int Booking_TicketCount { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = " Total Amount(in Rs) :  ")]
        public decimal Booking_TotalAmount { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = " Payment Mode :  ")]
        public string Payment_Mode { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = " Confirmed? :  ")]
        public bool IsConfirmationSent { get; set; }

        public List<SelectListItem> PaymentModeList { get; set; }
    }

    public enum PaymentModes
    {
        [Display(Name = "Credit Card")]
        Credit_Card,

        [Display(Name = "Debit Card")]
        Debit_Card,

        [Display(Name = "Online Banking")]
        Online_Payment,

        [Display(Name = "Cash On Delivery")]
        Cash_On_Delivery
    }
}