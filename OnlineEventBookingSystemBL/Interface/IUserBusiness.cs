using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineEventBookingSystemDomain;

namespace OnlineEventBookingSystemBL.Interface
{
  public  interface IUserBusiness
    {
        //User CRUD operations
        List<UserRegistrationDomainModel> GetAllUsers();
        string AddUser(UserRegistrationDomainModel user);
        UserRegistrationDomainModel GetUserByName(string username);
        UserRegistrationDomainModel CheckLogin(UserRegistrationDomainModel userRegistrationDomainModel);
        UserRegistrationDomainModel GetUserById(int id);
        bool DeleteUser(int id);
        string UpdateUser(UserRegistrationDomainModel userDModel);
    }
}
