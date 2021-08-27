using OnlineEventBookingSystemAPI.Models;
using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;

namespace OnlineEventBookingSystemAPI.Controllers
{
    public class LoginController : ApiController
    {
        private IUserBusiness userBusiness;
        private MapperConfiguration config;
        private Mapper mapper;

        public LoginController(IUserBusiness _userBusiness)
        {
            userBusiness = _userBusiness;
            config = new MapperConfiguration(x => x.CreateMap<UserRegistrationDomainModel, UserRegistrationModel>().ReverseMap());
            mapper = new Mapper(config);
        }

        // POST: api/UserDetails
        [ResponseType(typeof(UserRegistrationModel))]
        public IHttpActionResult PostUserDetail(UserRegistrationModel userDetailModel)
        {
            UserRegistrationDomainModel userDomainModel = new UserRegistrationDomainModel();
            var check = userBusiness.WhereUser(userDetailModel.User_Name);

            if (check == null)
            {
                mapper.Map(userDetailModel, userDomainModel);
                userBusiness.AddUser(userDomainModel);
                return Ok("inserted");
            }

            else
            {
                return BadRequest();
            }
        }

        // POST: api/UserDetails
        [ResponseType(typeof(UserLoginDomainModel))]
        public UserLoginModel UserLogin(UserLoginModel userDetailModel)
        {
            UserLoginDomainModel userDomainModel = new UserLoginDomainModel();
            config = new MapperConfiguration(x => x.CreateMap<UserLoginModel, UserLoginDomainModel>().ReverseMap());
            mapper = new Mapper(config);
            mapper.Map(userDetailModel, userDomainModel);
            var check = userBusiness.CheckLogin(userDomainModel);
            userDetailModel.IsAdmin = check.IsAdmin;
            if (check == null)
            {
                return null;
            }

            else
            {
                return userDetailModel;
            }
        }
    }
}
