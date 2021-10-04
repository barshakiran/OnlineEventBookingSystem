using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineEventBookingSystemAPI.Models;
using OnlineEventBookingSystemBL.Interface;
using AutoMapper;
using OnlineEventBookingSystemDomain;
using OnlineEventBookingSystemAPI.Security;
using OnlineEventBookingSystemAPI.CustomException;
using System.Net;
using System.Net.Http;

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
            List<UserRegistrationModel> listViewModel;
            if(list !=null)
            {
                listViewModel = new List<UserRegistrationModel>();
                var user = mapper.Map(list, listViewModel);
                return listViewModel;
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Data not found in the table")),
                    ReasonPhrase = "Data not found"
                };
                throw new HttpResponseException(response);
            }
        }

        public IHttpActionResult GetDetails(int id)
        {
            UserRegistrationModel userRegistrationModel = new UserRegistrationModel();
            if (id == 0)
            {
                return BadRequest();
            }
            UserRegistrationDomainModel userDetail = userBusiness.GetUserById(id);
            if (userDetail == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Data not found for the Id: {0}",id)),
                    ReasonPhrase = "Data not found"
                };

                throw new HttpResponseException(response);
                //return NotFound();
            }
            mapper.Map(userDetail, userRegistrationModel);
            return Ok(userRegistrationModel);
        }

        [HttpPut]
        // PUT: api/UserDetails
        [ResponseType(typeof(UserRegistrationModel))]
        public IHttpActionResult UpdateUserDetail(UserRegistrationModel userDetailModel)
        {
            UserRegistrationDomainModel userDomainModel = new UserRegistrationDomainModel();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var check = userBusiness.GetUserById(userDetailModel.User_Id);

            if (check != null)
            {
                mapper.Map(userDetailModel, userDomainModel);
                userBusiness.UpdateUser(userDomainModel);
                return Ok("Updated");
            }

            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Data not found for the Id: {0}", userDetailModel.User_Id)),
                    ReasonPhrase = "Data not found"
                };

                throw new HttpResponseException(response);
            }
        }

        // DELETE: api/UserDetails
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var check = userBusiness.GetUserById(id);
            //bool isDeleted = false;
            if (check != null)
            {
               
               if(userBusiness.DeleteUser(id) == true)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Data not found for the Id: {0}", id)),
                    ReasonPhrase = "Data not found"
                };

                throw new HttpResponseException(response);
            }
        }
    }
}