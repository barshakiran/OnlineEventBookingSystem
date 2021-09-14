using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineEventBookingSystemDAL;
using OnlineEventBookingSystemAPI.Models;
using OnlineEventBookingSystemBL.Interface;
using AutoMapper;
using OnlineEventBookingSystemDomain;

namespace OnlineEventBookingSystemAPI.Controllers
{
    public class UserEventDetailsController : ApiController
    {
        private IUserEventDetailsBusiness userEventDetailBusiness;
        private MapperConfiguration config;
        private Mapper mapper;


        public UserEventDetailsController(IUserEventDetailsBusiness _userEventDetailBusiness)
        {
            userEventDetailBusiness = _userEventDetailBusiness;
            config = new MapperConfiguration(x => x.CreateMap<UserEventDetailsModel, UserEventDetailsDomainModel>().ReverseMap());
            mapper = new Mapper(config);
        }

        [HttpPost]
        // : api/EventDetails
        public List<UserEventDetailsModel> UserEventDetails(UserEventDetailsModel model)
        {
            List<UserEventDetailsDomainModel> list = userEventDetailBusiness.DisplayAllUserEvent(model.Event_Type, model.City);
            List<UserEventDetailsModel> listViewModel;
            if (list != null)
            {
                listViewModel = new List<UserEventDetailsModel>();
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

        public IHttpActionResult GetLocationDetailList()
        {
            // List<EventLocationModel> listViewModel ;
            var locationDomainModelList = userEventDetailBusiness.DisplayCityList();
            List<LocationModel> locationModel;
            if (locationDomainModelList != null)
            {
                locationModel = new List<LocationModel>();
                config = new MapperConfiguration(x => x.CreateMap<LocationDomainModel, LocationModel>().ReverseMap());
                mapper = new Mapper(config);
                mapper.Map(locationDomainModelList, locationModel);
                return Ok(locationModel);
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Data not found")),
                    ReasonPhrase = "Data not found"
                };
                throw new HttpResponseException(response);
            }
        }

        //[HttpPost]
        //// POST: api/EventDetails
        //[ResponseType(typeof(UserEventDetailsModel))]
        //public IHttpActionResult PostEventDetail(UserEventDetailsModel eventDetail)
        //{
        //    UserEventDetailsDomainModel eventDetailDModel;
        //    if (!ModelState.IsValid)
        //    {
        //        var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
        //        {
        //            Content = new StringContent(string.Format("Server error.")),
        //            ReasonPhrase = "Server error."
        //        };
        //        throw new HttpResponseException(response);
        //        // return BadRequest(ModelState);
        //    }
        //    else
        //    {
        //        eventDetailDModel = new UserEventDetailsDomainModel();
        //        mapper.Map(eventDetail, eventDetailDModel);
        //        userEventDetailBusiness.AddEventDetails(eventDetailDModel);
        //        return Ok("Inserted");
        //    }
        //}
    }
}
