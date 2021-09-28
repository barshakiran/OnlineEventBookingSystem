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
    public class EventDetailsController : ApiController
    {
        private IEventDetailBusiness eventDetailBusiness;
        private MapperConfiguration configEvent;
        private Mapper mapperEvent;


        public EventDetailsController(IEventDetailBusiness _eventDetailBusiness)
        {
            eventDetailBusiness = _eventDetailBusiness;
            configEvent = new MapperConfiguration(x => x.CreateMap<EventDetailModel, EventDetailDomainModel>().ReverseMap());
            mapperEvent = new Mapper(configEvent);
        }
        //GET: api/EventDetails
        public List<EventDetailModel> GetEventDetailList()
        {

            List<EventDetailDomainModel> eventDetailDomainList = eventDetailBusiness.DisplayEventDetailList();
            List<EventDetailModel> eventDetailModelList;
            if (eventDetailDomainList != null)
            {
                eventDetailModelList = new List<EventDetailModel>();
                var mapper = RInitializeAutomapper();
                var  eventDetailModel = mapper.Map<List<EventDetailModel>>(eventDetailDomainList);
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
                cfg.CreateMap<EventLocationDomainModel,EventLocationModel>();
                cfg.CreateMap<EventDetailDomainModel, EventDetailModel>()
                .ForMember(dest => dest.EventList, act => act.MapFrom(src => src.EventList));
            });
            var mapper = new Mapper(config);
            return mapper;
        }

        static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EventLocationModel, EventLocationDomainModel>();
                cfg.CreateMap<EventDetailModel, EventDetailDomainModel>()
                .ForMember(dest => dest.EventList, act => act.MapFrom(src => src.EventList));
            });
            var mapper = new Mapper(config);
            return mapper;
        }

        [HttpPost]
        // POST: api/EventDetails
        [ResponseType(typeof(EventDetailModel))]
        public IHttpActionResult PostEventDetail(EventDetailModel eventDetailModel)
        {
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
                var mapper = InitializeAutomapper();
                var eventDetailDomainModel = mapper.Map<EventDetailDomainModel>(eventDetailModel);
                eventDetailBusiness.AddEventDetails(eventDetailDomainModel);
                return Ok("Inserted");
            }
        }

        public List<BookingDetailModel> GetBookedEventsDetailList()
        {
            List<BookingDetailDomainModel> bookedUserEventsDetailDomainList = eventDetailBusiness.DisplayBookedEventsList();
            List<BookingDetailModel> bookedUserEventDetailModelList;
            if (bookedUserEventsDetailDomainList != null)
            {


                bookedUserEventDetailModelList = new List<BookingDetailModel>();
                configEvent = new MapperConfiguration(x => x.CreateMap<BookingDetailDomainModel, BookingDetailModel>().ReverseMap());
                mapperEvent = new Mapper(configEvent);
                mapperEvent.Map(bookedUserEventsDetailDomainList, bookedUserEventDetailModelList);
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

        [HttpPost]
        // POST: api/EventDetails
        [ResponseType(typeof(EventDetailModel))]
        public IHttpActionResult UpdateEventDetail(EventDetailModel eventDetailModel)
        {
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
                var mapper = InitializeAutomapper();
                var eventDetailDomainModel = mapper.Map<EventDetailDomainModel>(eventDetailModel);
                eventDetailBusiness.UpdateEventDetails(eventDetailDomainModel);
                return Ok("Updated");
            }
        }

        // GET: api/EventDetails/5
        public IHttpActionResult GetEventDetail(int id, int locationId)
        {
            {

                EventDetailDomainModel eventDetailDomain = eventDetailBusiness.DisplayEventDetail(id, locationId);
                EventDetailModel eventDetailModel = new EventDetailModel(); ;
                if (eventDetailDomain != null)
                {
                   
                    var mapper = RInitializeAutomapper();
                     eventDetailModel = mapper.Map <EventDetailModel>(eventDetailDomain);
                    return Ok(eventDetailModel);
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

        //DELETE: api/EventDetails/5
        //[ResponseType(typeof(EventDetail))]
        [HttpPost]
        public IHttpActionResult Delete(int id ,int locationId)
        {
                if (eventDetailBusiness.DeleteEvent(id,locationId) == true)
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
                    //return Ok(false);
                    throw new HttpResponseException(response);
                }
        }

        // Get: api/EventDetails
        public IHttpActionResult GetLocationDetailList()
        {
            var locationDomainModelList = eventDetailBusiness.LocationDetailList();
            List<LocationModel> locationModel;
            if (locationDomainModelList != null)
            {
                locationModel = new List<LocationModel>();
                configEvent = new MapperConfiguration(x => x.CreateMap<LocationDomainModel,LocationModel>().ReverseMap());
                mapperEvent = new Mapper(configEvent);
                mapperEvent.Map(locationDomainModelList, locationModel);
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