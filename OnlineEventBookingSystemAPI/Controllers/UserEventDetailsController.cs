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
            config = new MapperConfiguration(x => x.CreateMap<EventDetailModel, EventDetailDomainModel>().ReverseMap());
            mapper = new Mapper(config);
        }

        [HttpPost]
        public List<EventDetailModel> UserEventDetails(UserEventDetailsModel model)
        {
            List<EventDetailDomainModel> eventDetailDomainList = userEventDetailBusiness.DisplayAllUserEvent(model.Event_Type, model.City);
            List<EventDetailModel> eventDetailModelList;
            if (eventDetailDomainList != null)
            {
               

                eventDetailModelList = new List<EventDetailModel>();
                var mapper = RInitializeAutomapper();
                var eventDetailModel = mapper.Map<List<EventDetailModel>>(eventDetailDomainList);
                return eventDetailModel;
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


        static Mapper RInitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EventLocationDomainModel, EventLocationModel>();
                cfg.CreateMap<EventDetailDomainModel, EventDetailModel>()
                .ForMember(dest => dest.EventList, act => act.MapFrom(src => src.EventList));
            });
            var mapper = new Mapper(config);
            return mapper;
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
    }
}
