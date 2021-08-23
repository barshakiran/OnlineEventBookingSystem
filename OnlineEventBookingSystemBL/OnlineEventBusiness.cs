using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineEventBookingSystemDAL;
using OnlineEventBookingSystemDAL.Infrastructure.Contract;
using AutoMapper;
using System.Web.Mvc;
using System.Net;

namespace OnlineEventBookingSystemBL
{
    public class OnlineEventBusiness : IOnlineEventBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserRepository userRepository;
        private  MapperConfiguration configuration;
        private Mapper mapper;
        public OnlineEventBusiness(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            userRepository = new UserRepository(unitOfWork);
            configuration = new MapperConfiguration(x => x.CreateMap<UserRegistrationDomainModel, UserDetail>().ReverseMap());
            mapper = new Mapper(configuration);
        }


        List<UserRegistrationDomainModel> IOnlineEventBusiness.GetAllUsers()
        {

            
            List<UserRegistrationDomainModel> list = userRepository.GetAll().Select(m => new UserRegistrationDomainModel { User_Name = m.User_Name, User_Password = m.User_Password,
                                                                                    User_Id = m.User_Id ,User_Address= m.User_Address,User_Email = m.User_Email,
                                                                                       User_PhoneNo = m.User_PhoneNo ,IsAdmin = m.IsAdmin}).ToList();

            return list;
        }



        public string AddUser(UserRegistrationDomainModel userDModel)
        {
            UserDetail user = new UserDetail();
            user.User_Name = userDModel.User_Name;
            user.User_Password = userDModel.User_Password;
            user.User_Email = userDModel.User_Email;
            user.User_Address = userDModel.User_Address;
            user.User_PhoneNo = userDModel.User_PhoneNo;
            user.IsAdmin = userDModel.IsAdmin;
            userRepository.Insert(user);
            return "Inserted";

        }

        public UserRegistrationDomainModel WhereUser(string username)
        {
            UserRegistrationDomainModel userDModel;
            var data = userRepository.SingleOrDefault(s => s.User_Name == username);
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
            UserDetail user = new UserDetail();
            mapper.Map(userDModel ,user);
            userRepository.Update(user);
            return "Inserted";
        }


        public UserLoginDomainModel CheckLogin(UserLoginDomainModel model)
        {
            var data = userRepository.Where(s => s.User_Name == model.User_Name & s.User_Password == model.User_Password);
            model.IsAdmin = data.First().IsAdmin;
            return model;
        }

        public UserRegistrationDomainModel FindUser(int id)
        {
            UserRegistrationDomainModel userDModel = new UserRegistrationDomainModel();
            if (id == 0)
            {
                return null;
            }
                var data = userRepository.SingleOrDefault(s => s.User_Id == id);
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
                userRepository.Delete(s => s.User_Id == id);
                return true;
            }
            
        }

        //bool IsAdmin(string username)
        //{
        //    var data = userRepository.Fi(id);

        //}

    }
}
