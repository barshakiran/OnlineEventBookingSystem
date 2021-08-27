using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineEventBookingSystemAPI.Models;
using OnlineEventBookingSystemBL.Interface;
using AutoMapper;
using OnlineEventBookingSystemDomain;
using OnlineEventBookingSystemAPI.Security;

namespace OnlineEventBookingSystemAPI.Controllers
{
    public class UserDetailsController : ApiController
    {

        // GET: api/UserDetails
        private IUserBusiness userBusiness;
        private MapperConfiguration config;
        private Mapper mapper;

        public UserDetailsController(IUserBusiness _userBusiness)
        {
             userBusiness = _userBusiness;
             config = new MapperConfiguration(x =>x.CreateMap<UserRegistrationDomainModel, UserRegistrationModel>().ReverseMap());
             mapper = new Mapper(config);
        }

        [BasicAuthentication]
        public List<UserRegistrationModel> GetUserDetails()
        {
            
            List<UserRegistrationDomainModel> list = userBusiness.GetAllUsers();
            List<UserRegistrationModel> listViewModel = new List<UserRegistrationModel>();
            var user = mapper.Map(list, listViewModel);          
            return listViewModel;
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

        // DELETE: api/UserDetails
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