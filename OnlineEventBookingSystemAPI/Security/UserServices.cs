using OnlineEventBookingSystemAPI.Models;
using OnlineEventBookingSystemBL;
using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDAL.Infrastructure;
using OnlineEventBookingSystemDAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEventBookingSystemAPI.Security
{
    public class UserServices: IUserServices
    {
      
        private readonly IUserBusiness userBusiness;

        /// <summary>  
        /// Public constructor.  
        /// </summary>  
        public UserServices(IUserBusiness _userBusiness)
        {
            userBusiness = _userBusiness;
        }

        /// <summary>  
        /// Public method to authenticate user by user name and word.  
        /// </summary>  
        /// <param name="userName"></param>  
        /// <param name="word"></param>  
        /// <returns></returns>  
        public bool Authenticate(string userName, string word)
        {
            var user = userBusiness.GetAllUsers();

           return user.Any(x => x.User_Name.Equals(userName, StringComparison.OrdinalIgnoreCase) && x.User_Password == word);
            
        }
    }
    
}