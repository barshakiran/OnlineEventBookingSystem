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
        private EventBookingSystemEntities db = new EventBookingSystemEntities();

        // GET: api/UserDetails

        private IOnlineEventBusiness userBusiness;
        private MapperConfiguration config;
        private Mapper mapper;

        public UserDetailsController(IOnlineEventBusiness _userBusiness)
        {
             userBusiness = _userBusiness;
             config = new MapperConfiguration(x =>x.CreateMap<UserDomainModel, UserDetailModel>().ReverseMap());
             mapper = new Mapper(config);
        }
        public List<UserDetailModel> GetUserDetails()
        {
            
            List<UserDomainModel> list = userBusiness.GetAllUsers();
            List<UserDetailModel> listViewModel = new List<UserDetailModel>();
            var user = mapper.Map(list, listViewModel);          
            return listViewModel;
        }

        // GET: api/UserDetails/5
        [ResponseType(typeof(UserDetail))]
        public IHttpActionResult GetUserDetail(int id)
        {
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return NotFound();
            }

            return Ok(userDetail);
        }



        // POST: api/UserDetails
        [ResponseType(typeof(UserDetailModel))]
        public IHttpActionResult PostUserDetail(UserDetailModel userDetailModel)
        {
            UserDomainModel userDomainModel = new UserDomainModel();
            mapper.Map(userDetailModel, userDomainModel);
            userBusiness.AddUser(userDomainModel);
            return Ok("inserted");
        }

        // PUT: api/UserDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserDetail(int id, UserDetail userDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userDetail.User_Id)
            {
                return BadRequest();
            }

            db.Entry(userDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // DELETE: api/UserDetails/5
        [ResponseType(typeof(UserDetail))]
        public IHttpActionResult DeleteUserDetail(int id)
        {
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return NotFound();
            }

            db.UserDetails.Remove(userDetail);
            db.SaveChanges();

            return Ok(userDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserDetailExists(int id)
        {
            return db.UserDetails.Count(e => e.User_Id == id) > 0;
        }
    }
}