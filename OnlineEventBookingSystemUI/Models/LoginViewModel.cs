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
        public string User_Name { get; set; }
        public string User_Password { get; set; }
        public bool IsAdmin { get; set; } 
    }

}
