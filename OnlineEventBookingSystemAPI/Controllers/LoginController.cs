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
using OnlineEventBookingSystemAPI.CustomException;
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
        public IHttpActionResult AddUserDetail(UserRegistrationModel userDetailModel)
        {
            UserRegistrationDomainModel userDomainModel = new UserRegistrationDomainModel();
            if (userDetailModel == null)
            {
                var message = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Data not found")),
                    ReasonPhrase = "Data not found"
                };
                throw new HttpResponseException(message);
            }
            else
            {
                var check = userBusiness.GetUserByName(userDetailModel.User_Name);

                if (check == null)
                {
                    mapper.Map(userDetailModel, userDomainModel);
                    userBusiness.AddUser(userDomainModel);
                    return Ok("inserted");
                }

                else
                {
                    //return BadRequest();
                    var response = new HttpResponseMessage(HttpStatusCode.Found)
                    {
                        Content = new StringContent(string.Format("Username  {0} already exists ", userDetailModel.User_Name)),
                        ReasonPhrase = "Data already exists"
                    };
                    throw new HttpResponseException(response);
                }
            }
                
        }

        // POST: api/UserDetails
        [ResponseType(typeof(UserRegistrationModel))]
        public UserRegistrationModel UserLogin(UserRegistrationModel userDetailModel)
        {
            UserRegistrationDomainModel userDomainModel = new UserRegistrationDomainModel();
            mapper.Map(userDetailModel, userDomainModel);
            var check = userBusiness.CheckLogin(userDomainModel);
            
            if (check == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Username or password is wrong")),
                    ReasonPhrase = "Data not found"
                };
                throw new HttpResponseException(response);
                //return NotFound();
            }

            else
            {
                userDetailModel.IsAdmin = check.IsAdmin;
                userDetailModel.User_Id = check.User_Id;
                return userDetailModel;
            }
        }
    }
}
