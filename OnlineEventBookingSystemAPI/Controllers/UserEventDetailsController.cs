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
        }

        [HttpPost]
        public List<EventDetailModel> GetUserEventsDetailList(UserEventDetailsModel model)
        {
            List<EventDetailDomainModel> eventDetailDomainList = userEventDetailBusiness.DisplayAllUserEvent(model.Event_Type, model.Booking_Loc);
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
   
        public List<BookingDetailModel> GetUserBookedEventsDetailList(string id)
        {
            List<BookingDetailDomainModel> bookedUserEventsDetailDomainList = userEventDetailBusiness.DisplayUserBookedEventsList(id);
            List<BookingDetailModel> bookedUserEventDetailModelList;
            if (bookedUserEventsDetailDomainList != null)
            {


                bookedUserEventDetailModelList = new List<BookingDetailModel>();
                config = new MapperConfiguration(x => x.CreateMap<BookingDetailDomainModel, BookingDetailModel>().ReverseMap());
                mapper = new Mapper(config);
                mapper.Map(bookedUserEventsDetailDomainList, bookedUserEventDetailModelList);
                return bookedUserEventDetailModelList;
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

        public IHttpActionResult GetUserBookingEventDetail(int id, int locationId)
        {
            {
                UserEventDetailsModel userEventDetailModel;

                UserEventDetailsDomainModel userEventDetailDomain = userEventDetailBusiness.DisplayUserBookingEventDetails(id, locationId);
                if (userEventDetailDomain != null)
                {
                    userEventDetailModel = new UserEventDetailsModel(); 
                    var mapper = InitializeAutomapperForUser();
                    userEventDetailModel = mapper.Map<UserEventDetailsModel>(userEventDetailDomain);
                    return Ok(userEventDetailModel);
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
        }

        public IHttpActionResult GetUserBookedEventDetail(int id)
        {
            {
                BookingDetailModel bookedEventDetailModel;

                BookingDetailDomainModel bookedEventDetailDomain = userEventDetailBusiness.DisplayUserBookedEventDetails(id);
                if (bookedEventDetailDomain != null)
                {
                    bookedEventDetailModel = new BookingDetailModel();
                    config = new MapperConfiguration(x => x.CreateMap<BookingDetailDomainModel, BookingDetailModel>().ReverseMap());
                    mapper = new Mapper(config);
                    mapper.Map(bookedEventDetailDomain, bookedEventDetailModel);
                    return Ok(bookedEventDetailModel);
                }
                else
                {
                   // return NotFound();
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("this user does not exist")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(response);
                }
            }
        }

        public IHttpActionResult PostUserBookingEventDetail(BookingDetailModel bookingDetailModel)
        {
            BookingDetailDomainModel bookingDetailDomainModel;
            if (!ModelState.IsValid)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Server error.")),
                    ReasonPhrase = "Server error."
                };
                throw new HttpResponseException(response);
            }
            else
            {
                bookingDetailDomainModel = new BookingDetailDomainModel();
                config = new MapperConfiguration(x => x.CreateMap<BookingDetailModel, BookingDetailDomainModel>().ReverseMap());
                mapper = new Mapper(config);
                mapper.Map(bookingDetailModel, bookingDetailDomainModel);
                int bookingId = userEventDetailBusiness.AddUserBookingEventDetails(bookingDetailDomainModel);
                return Ok(bookingId);
            }
        }

        //DELETE: api/UserEventDetails/5
        //[ResponseType(typeof(UserEventDetail))]
        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            if (userEventDetailBusiness.DeleteBookedEvent(id) == true)
            {
                return Ok(true);
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

        static Mapper InitializeAutomapperForUser()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EventLocationDomainModel, EventLocationModel>();
                cfg.CreateMap<UserEventDetailsDomainModel, UserEventDetailsModel>()
                .ForMember(dest => dest.EventList, act => act.MapFrom(src => src.EventList));
            });
            var mapper = new Mapper(config);
            return mapper;
        }

        public IHttpActionResult GetLocationDetailList()
        {
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
