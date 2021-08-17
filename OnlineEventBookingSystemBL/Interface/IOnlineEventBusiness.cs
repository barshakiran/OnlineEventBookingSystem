using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineEventBookingSystemDomain;

namespace OnlineEventBookingSystemBL.Interface
{
  public  interface IOnlineEventBusiness
    {

         string GetUser(int userId);
        List<UserDomainModel> GetAllUsers();
         string AddUser(UserDomainModel user);
    }
}
