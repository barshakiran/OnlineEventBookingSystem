using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineEventBookingSystem.Models
{
    public class LoginViewModel
    {
        

        public int User_Id { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = " Username :  ")]
        public string User_Name { get; set; }

        [Display(Name = " Password :  ")]
        [Required(ErrorMessage = "This field is Required")]
        [DataType(DataType.Password)]
        public string User_Password { get; set; }
        public bool IsAdmin { get; set; }
    }

}
