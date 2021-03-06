using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineEventBookingSystemAPI.Models
{
   public class UserRegistrationModel
    {
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Password { get; set; }
        public string User_PhoneNo { get; set; }
        public string User_Email { get; set; }
        public string User_Address { get; set; }
        public bool IsAdmin { get; set; }
    }
}
