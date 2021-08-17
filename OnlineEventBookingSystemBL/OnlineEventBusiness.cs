using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineEventBookingSystemDAL;
using System.Threading.Tasks;
using OnlineEventBookingSystemDAL.Infrastructure.Contract;


namespace OnlineEventBookingSystemBL
{
    public class OnlineEventBusiness : IOnlineEventBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserRepository userRepository;
       public OnlineEventBusiness(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            userRepository = new UserRepository(unitOfWork);
        }
             
        public string GetUser(int UserId)
        {
            return "Barsha" + UserId;
        }

        List<UserDomainModel> IOnlineEventBusiness.GetAllUsers()
        {

           // EventBookingSystemEntities1 dbContext = new EventBookingSystemEntities1();
         // List<UserDomainModel> list = dbContext.UserDetails.Select(m => new UserDomainModel { User_Name = m.User_Name, User_Password = m.User_Password, User_Id = m.User_Id }).ToList();
          List<UserDomainModel> list = userRepository.GetAll().Select(m => new UserDomainModel { User_Name = m.User_Name, User_Password = m.User_Password, User_Id = m.User_Id }).ToList();

            return list;
        }

       public string AddUser(UserDomainModel userDModel)
        {
            UserDetail user = new UserDetail();
            user.User_Name = userDModel.User_Name;
            user.User_Password = userDModel.User_Password;
            user.User_Email = userDModel.User_Email;
            user.User_Address = userDModel.User_Address;
            user.User_PhoneNo = userDModel.User_PhoneNo;
            user.IsAdmin = false;
            userRepository.Insert(user);
            return "Inserted";

        }
    }
}
