using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDomain;
using System.Collections.Generic;
using System.Linq;
using OnlineEventBookingSystemDAL;
using OnlineEventBookingSystemDAL.Infrastructure.Contract;
using AutoMapper;
using System;

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
            // List<UserRegistrationDomainModel> userRegistrationDomainModels = new 


            // List<UserRegistrationDomainModel> list = userDataHandler.GetAll().Select(m => new UserRegistrationDomainModel { User_Name = m.User_Name, User_Password = m.User_Password,
            // User_Id = m.User_Id ,User_Address= m.User_Address,User_Email = m.User_Email,
            //  User_PhoneNo = m.User_PhoneNo ,IsAdmin = m.IsAdmin}).ToList();


            try
            {
                List<UserDetail> userDetailList = new List<UserDetail>();
                List<UserRegistrationDomainModel> userDetailDomainList = new List<UserRegistrationDomainModel>();
                userDetailList = userDataHandler.GetAll().ToList();
                mapper.Map(userDetailList, userDetailDomainList);               
                return userDetailDomainList;
            }
            catch (System.Exception msg)
            {
                throw msg;
            }
        }

        public string AddUser(UserRegistrationDomainModel userDModel)
        {
            UserDetail user;
            try
            {
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
            catch (Exception msg)
            {
                throw msg;
            }
        }
      
        public UserRegistrationDomainModel GetUserByName(string username)
        {
            UserRegistrationDomainModel userDModel;
            try
            {
                var data = userDataHandler.SingleOrDefault(s => s.User_Name == username);
                if (data == null)
                {
                    return null;
                }
                else
                {
                    userDModel = new UserRegistrationDomainModel();
                    mapper.Map(data, userDModel);
                    return userDModel;
                }
            }
            catch (Exception msg)
            {
                throw msg;
            }
        }

        public string UpdateUser(UserRegistrationDomainModel userDModel)
        { 
            UserDetail user;
            try
            {
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
            catch (Exception msg)
            {
                throw msg;
            }
        }

        public UserRegistrationDomainModel CheckLogin(UserRegistrationDomainModel userLoginDomainModel)
        {
            try
            {
                string username = userLoginDomainModel.User_Name;
                string password = userLoginDomainModel.User_Password;
                var data = userDataHandler.SingleOrDefault(s => s.User_Name == username & s.User_Password == password);
                if (data != null)
                {
                    userLoginDomainModel.IsAdmin = data.IsAdmin;
                    userLoginDomainModel.User_Id = data.User_Id;
                    return userLoginDomainModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception msg)
            {
                throw msg;
            }
        }
             
        public UserRegistrationDomainModel GetUserById(int id)
        {
            UserRegistrationDomainModel userDModel;
            try
            {
                userDModel = new UserRegistrationDomainModel();
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
            catch (Exception msg)
            {
                throw msg;
            }

        }

        public bool DeleteUser(int id)
        {
            try
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
            catch (Exception msg)
            {
                throw msg;
            }

        }     
    }
}