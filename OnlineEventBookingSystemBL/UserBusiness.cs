using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDomain;
using System.Collections.Generic;
using System.Linq;
using OnlineEventBookingSystemDAL;
using OnlineEventBookingSystemDAL.Infrastructure.Contract;
using AutoMapper;

namespace OnlineEventBookingSystemBL
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserDataHandler userDataHandler;
        private  MapperConfiguration configuration;
        private Mapper mapper;

        public UserBusiness(IUserDataHandler _userDataHandler)
        {
            userDataHandler = _userDataHandler;
            configuration = new MapperConfiguration(x => x.CreateMap<UserRegistrationDomainModel, UserDetail>().ReverseMap());
            mapper = new Mapper(configuration);
        }

       public List<UserRegistrationDomainModel> GetAllUsers()
        {
            
            
            List<UserRegistrationDomainModel> list = userDataHandler.GetAll().Select(m => new UserRegistrationDomainModel { User_Name = m.User_Name, User_Password = m.User_Password,
                                                                                    User_Id = m.User_Id ,User_Address= m.User_Address,User_Email = m.User_Email,
                                                                                       User_PhoneNo = m.User_PhoneNo ,IsAdmin = m.IsAdmin}).ToList();

            return list;
        }

        public string AddUser(UserRegistrationDomainModel userDModel)
        {
            UserDetail user;
            if (userDModel != null)
            {
                user = new UserDetail();
                mapper.Map(userDModel, user);
                userDataHandler.Insert(user);
                return "Inserted";
            }
            else
            {
                return null;
            }
        }
      
        public UserRegistrationDomainModel WhereUser(string username)
        {
            UserRegistrationDomainModel userDModel;
            var data = userDataHandler.SingleOrDefault(s => s.User_Name == username);
            if (data == null)
            {
                return null;
            }
            else
            {
                userDModel = new UserRegistrationDomainModel();
                mapper.Map(data, userDModel);
                return userDModel ;                
            }
        }

        public string UpdateUser(UserRegistrationDomainModel userDModel)
        { 
            UserDetail user;
            if (userDModel != null)
            {
                user = new UserDetail();
                mapper.Map(userDModel, user);
                userDataHandler.Update(user);
                return "Updated";
            }
            else
            {
                return null;
            }
        }

        public UserLoginDomainModel CheckLogin(UserLoginDomainModel model)
        {
            var data = userDataHandler.Where(s => s.User_Name == model.User_Name & s.User_Password == model.User_Password);
            if(data.Count() != 0)
            {
                model.IsAdmin = data.First().IsAdmin;
                return model;
            }
            else
            {
                return null;
            }
        }

        public UserRegistrationDomainModel FindUser(int id)
        {
            UserRegistrationDomainModel userDModel = new UserRegistrationDomainModel();
            if (id == 0)
            {
                return null;
            }
                var data = userDataHandler.SingleOrDefault(s => s.User_Id == id);
                if (data == null)
                {
                    return null;
                }
            mapper.Map(data, userDModel);
            return userDModel;
        }

        public bool DeleteUser(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                userDataHandler.Delete(s => s.User_Id == id);
                return true;
            }
            
        }     
    }
}