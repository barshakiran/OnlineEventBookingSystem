using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineEventBookingSystem.Models
{
    public class RegistrationViewModel
    {

        public int User_Id { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = " Username :  ")]
        public string User_Name { get; set; }

        [Display(Name = " Password :  ")]
        [Required(ErrorMessage = "This field is Required")]
       
        public string User_Password { get; set; }

        [Display(Name = " E-Mail :  ")]
        [Required(ErrorMessage = "This field is Required")]
        [DataType(DataType.EmailAddress)]
        public string User_Email { get; set; }

        [Display(Name = " Address :  ")]
        [DataType(DataType.MultilineText)]
        public string User_Address { get; set; }

        [Display(Name = " Phone No :  ")]
        [DataType(DataType.PhoneNumber)]
        public string User_PhoneNo { get; set; }
        public string IsAdmin { get; set; }


    }
}