using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineEventBookingSystemAPI.Models;
using OnlineEventBookingSystemBL;
using OnlineEventBookingSystemBL.Interface;
using AutoMapper;
using OnlineEventBookingSystemDomain;


namespace OnlineEventBookingSystemAPI.Controllers
{
    public class UserDetailsController : ApiController
    {

        // GET: api/UserDetails

        private IOnlineEventBusiness userBusiness;
        private MapperConfiguration config;
        private Mapper mapper;

        public UserDetailsController(IOnlineEventBusiness _userBusiness)
        {
             userBusiness = _userBusiness;
             config = new MapperConfiguration(x =>x.CreateMap<UserRegistrationDomainModel, UserRegistrationModel>().ReverseMap());
             mapper = new Mapper(config);
        }
        public List<UserRegistrationModel> GetUserDetails()
        {
            
            List<UserRegistrationDomainModel> list = userBusiness.GetAllUsers();
            List<UserRegistrationModel> listViewModel = new List<UserRegistrationModel>();
            var user = mapper.Map(list, listViewModel);          
            return listViewModel;
        }

        //public UserLoginModel CheckAdminAndLogin(UserLoginModel model)
        //{
        //    var check = userBusiness.CheckLogin(model.User_Name, model.User_Password);
        //    if (check != null)
        //    {
        //        if(check.)
        //        return  model;
        //    }

        //    return null;



        //}
        //{

        //    List<UserRegistrationDomainModel> list = userBusiness.GetAllUsers();
        //    List<UserRegistrationModel> listViewModel = new List<UserRegistrationModel>();
        //    var user = mapper.Map(list, listViewModel);
        //    return listViewModel;
        //}


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
            // var check = userBusiness.CheckLogin(userDetailModel.User_Name, userDetailModel.User_Password);

            var check = userBusiness.CheckLogin(userDomainModel);
            //mapper.Map(check, userDetailModel);
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


        public IHttpActionResult GetDetails(int id)
        {
            UserRegistrationModel userRegistrationModel = new UserRegistrationModel();
            if (id == 0)
            {
                return BadRequest();
            }
            UserRegistrationDomainModel userDetail = userBusiness.FindUser(id);
            if (userDetail == null)
            {
                return BadRequest();
            }
            mapper.Map(userDetail, userRegistrationModel);
            return Ok(userRegistrationModel);
        }

        [HttpPost]
        // PUT: api/UserDetails
        [ResponseType(typeof(UserRegistrationModel))]
        public IHttpActionResult UserDetail(UserRegistrationModel userDetailModel)
        {
            UserRegistrationDomainModel userDomainModel = new UserRegistrationDomainModel();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var check = userBusiness.FindUser(userDetailModel.User_Id);

            if (check != null)
            {
                mapper.Map(userDetailModel, userDomainModel);
                userBusiness.UpdateUser(userDomainModel);
                return Ok("inserted");
            }

            else
            {
                return BadRequest();
            }
        
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UserDetailExists(userDetailModel.User_Id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

           // return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            var check = userBusiness.FindUser(id);

            if (check != null)
            {
               // mapper.Map(userDetailModel, userDomainModel);
                userBusiness.DeleteUser(id);
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

    }
}