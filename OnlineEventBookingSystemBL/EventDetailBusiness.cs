using AutoMapper;
using OnlineEventBookingSystemDAL;
using OnlineEventBookingSystemDomain;
using OnlineEventBookingSystemBL.Interface;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OnlineEventBookingSystemBL
{
    public class EventDetailBusiness : IEventDetailBusiness
    {
        private MapperConfiguration configuration;
        private Mapper mapper;
        private IEventDetailDataHandler eventDetailDataHandler;
        private IEventLocationDataHandler eventLocationDataHandler;
        private ILocationDataHandler locationDataHandler;
        private IUserDataHandler userDataHandler;
        private IBookingDetailDataHandler bookingDetailDataHandler;
        public EventDetailBusiness(IBookingDetailDataHandler _bookingDetailDataHandler,IUserDataHandler _userDataHandler,IEventDetailDataHandler _eventDetailDataHandler , IEventLocationDataHandler _eventLocationDataHandler, ILocationDataHandler _locationDataHandler)
        {
            locationDataHandler = _locationDataHandler;
            eventDetailDataHandler = _eventDetailDataHandler;
            eventLocationDataHandler = _eventLocationDataHandler;
            userDataHandler =  _userDataHandler;
            bookingDetailDataHandler = _bookingDetailDataHandler;
            configuration = new MapperConfiguration(x => x.CreateMap<EventDetailDomainModel, EventDetail>().ReverseMap());
            mapper = new Mapper(configuration);
        }
        
        public string AddEventDetails(EventDetailDomainModel eventDModel)
        {
            EventDetail newEvent;
            EventLocation newEventLocation;
            List<EventLocation> eventLocationlist;

            try
            {
                if (eventDModel != null)
                {
                    newEvent = new EventDetail();
                    newEvent.Event_Name = eventDModel.Event_Name;
                    newEvent.Event_Type = eventDModel.Event_Type;
                    newEvent.Event_Description = eventDModel.Event_Description;
                    newEvent.Event_Picture = eventDModel.Event_Picture;
                    eventDetailDataHandler.Insert(newEvent);
                    eventLocationlist = new List<EventLocation>();
                   
                    foreach (var item in eventDModel.EventList)
                    {
                        newEventLocation = new EventLocation();
                        var userEventList =  eventLocationDataHandler.GetAll(e => e.Location_Id == item.Location_Id && e.Event_Id == newEvent.Event_Id).ToList();
                        newEventLocation.EventLocation_DateAndTime = item.EventLocation_DateAndTime; 
                        newEventLocation.EventLocation_Price = item.EventLocation_Price;
                        newEventLocation.Location_Id = item.Location_Id;
                        newEventLocation.Event_Id = newEvent.Event_Id;
                        if (userEventList.Count == 0)
                        {
                            eventLocationlist.Add(newEventLocation);
                        }
                        
                    }
                    eventLocationDataHandler.InsertAll(eventLocationlist);

                    return "Inserted";
                }
                else
                {
                    return null;
                }
            }
            catch(Exception msg)
            {
                throw msg;
            }           
        }

        static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EventLocation,EventLocationDomainModel>();
                cfg.CreateMap<EventDetail, EventDetailDomainModel>()
                .ForMember(dest => dest.EventList, act => act.MapFrom(src => src.EventLocations));
                cfg.CreateMap<Location, LocationDomainModel>()
                .ForMember(dest => dest.City, act => act.MapFrom(src => src.City));
            });
            var mapper = new Mapper(config);
            return mapper;
        }

        public List<EventDetailDomainModel> DisplayEventDetailList()
        {

            try
            {
                List<EventDetail> userEventList = new List<EventDetail>();              
                List<EventDetailDomainModel> userEvents = new List<EventDetailDomainModel>();               
                //userEventList = eventDetailDataHandler.GetAll().ToList();
                userEvents = EventDetailList();
                var cityList = LocationDetailList();              
                foreach(var item in userEvents)
                {
                    int index = 0;
                    foreach ( var cities in item.EventList)
                    {
                        cities.City = cityList[index].City;
                        index++;
                    }                    
                }
                return userEvents;
            }
            catch (System.Exception msg)
            {
                throw msg;
            }
        }

        public EventDetailDomainModel DisplayEventDetail(int eventId, int locationId)
        {

            try
            {
                EventDetail userEvent = new EventDetail();
                EventDetailDomainModel userEventDomainModel = new EventDetailDomainModel();
                List<LocationDomainModel> cityList = new List<LocationDomainModel>();
                if (eventId !=0)
                {
                    userEventDomainModel = EventDetailList().FirstOrDefault(e => e.Event_Id == eventId);
                    cityList = LocationDetailList();
                }
               
                if (eventId != 0 && locationId != 0)
                {
                    userEventDomainModel.EventList.RemoveAll(x => x.Location_Id != locationId);
                    foreach (var item in userEventDomainModel.EventList)
                    {
                            int index = cityList.FindIndex(x => x.Location_Id == locationId);
                            item.City = cityList[index].City;
                    }
                }
                else
                {
                    int index = 0;
                    foreach (var item in userEventDomainModel.EventList)
                    {                       
                            item.City = cityList[index].City;
                            index++;
                    }
                }
                return userEventDomainModel;
            }
            catch (System.Exception msg)
            {
                throw msg;
            }
        }
       
        public List<LocationDomainModel> LocationDetailList()
        {
            List<LocationDomainModel> CityList;
            
            try
            {
                configuration = new MapperConfiguration(x => x.CreateMap<LocationDomainModel, Location>().ReverseMap());
                mapper = new Mapper(configuration);
                CityList = new List<LocationDomainModel>();

                var city = locationDataHandler.GetAll().ToList();
                mapper.Map(city, CityList);
                return CityList;

            }
            catch (System.Exception msg)
            {
                throw msg;
            }
        }

        public List<UserRegistrationDomainModel> UserDetailList()
        {
            List<UserRegistrationDomainModel> userDetailDomainList;

            try
            {
                configuration = new MapperConfiguration(x => x.CreateMap<UserDetail, UserRegistrationDomainModel>().ReverseMap());
                mapper = new Mapper(configuration);
                userDetailDomainList = new List<UserRegistrationDomainModel>();

                var userDetailList = userDataHandler.GetAll().ToList();
                mapper.Map(userDetailList, userDetailDomainList);
                return userDetailDomainList;

            }
            catch (System.Exception msg)
            {
                throw msg;
            }
        }

        public List<EventDetailDomainModel> EventDetailList()
        {
            List<EventDetailDomainModel> eventDetailDomainList;
            try
            {
                eventDetailDomainList = new List<EventDetailDomainModel>();
                var eventDetailList = eventDetailDataHandler.GetAll().ToList();
                var mapper = InitializeAutomapper();
                eventDetailDomainList = mapper.Map<List<EventDetailDomainModel>>(eventDetailList);
                return eventDetailDomainList;
            }
            catch (System.Exception msg)
            {
                throw msg;
            }
        }

        public string UpdateEventDetails(EventDetailDomainModel eventDomainModel)
        {
            EventDetail newEvent;
            EventLocation newEventLocation;
            List<EventLocation> eventLocationList;

            try
            {
                if (eventDomainModel != null)
                {
                    newEvent = new EventDetail();
                    newEvent.Event_Id = eventDomainModel.Event_Id;
                    newEvent.Event_Name = eventDomainModel.Event_Name;
                    newEvent.Event_Type = eventDomainModel.Event_Type;
                    newEvent.Event_Description = eventDomainModel.Event_Description;
                    newEvent.Event_Picture = eventDomainModel.Event_Picture;
                    eventDetailDataHandler.Update(newEvent);
                    eventLocationList = new List<EventLocation>();

                    foreach (var item in eventDomainModel.EventList)
                    {
                        newEventLocation = new EventLocation();
                        newEventLocation.EventLocation_DateAndTime = item.EventLocation_DateAndTime;
                        newEventLocation.EventLocation_Price = item.EventLocation_Price;
                        newEventLocation.Location_Id = item.Location_Id;
                        newEventLocation.Event_Id = newEvent.Event_Id;
                        eventLocationList.Add(newEventLocation);
                    }
                    eventLocationDataHandler.UpdateAll(eventLocationList);

                    return "Updated";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception msg)
            {
                throw msg;
            }

        }

        public bool DeleteEvent(int id ,int locationId)
        {
            EventDetailDomainModel eventDetailDomainModels;
            try
            {
                eventDetailDomainModels = new EventDetailDomainModel();
                if (id == 0)
                {
                    return false;
                }
                else
                {
                    eventLocationDataHandler.Delete(s => s.Event_Id == id & s.Location_Id == locationId);
                    eventDetailDomainModels = DisplayEventDetail(id, 0);
                    if(eventDetailDomainModels!= null && eventDetailDomainModels.EventList.Count==0)
                    {
                        eventDetailDataHandler.Delete(x => x.Event_Id == id);
                    }
                    return true;
                }
            }
            catch(Exception msg)
            {
                throw msg;
            }
           

        }

        public List<BookingDetailDomainModel> DisplayBookedEventsList()
        {
            try
            {
                List<BookingDetail> bookedEventDetail = new List<BookingDetail>();
                List<BookingDetailDomainModel> bookingDetailDomainModel = new List<BookingDetailDomainModel>();
                bookedEventDetail = bookingDetailDataHandler.GetAll().ToList();
                if (bookedEventDetail != null)
                {
                    var NameList = UserDetailList();
                    List<string> Names = new List<string>();
                    foreach (var item in bookedEventDetail)
                    {
                        int index = NameList.FindIndex(x => x.User_Id == item.User_Id);
                        Names.Add(NameList[index].User_Name);
                    }
                    configuration = new MapperConfiguration(x => x.CreateMap<BookingDetail, BookingDetailDomainModel>().ReverseMap());
                    mapper = new Mapper(configuration);
                    mapper.Map(bookedEventDetail, bookingDetailDomainModel);
                    var EventList = EventDetailList();
                    int j = 0;
                    foreach (var item in bookingDetailDomainModel)
                    {                     
                        item.UserName = Names[j];
                        int index = EventList.FindIndex(x => x.Event_Id == item.Event_Id);
                        item.Event_Name = EventList[index].Event_Name;
                        j++;
                    }
                    return bookingDetailDomainModel;
                }
                else
                    return null;
            }
            catch (System.Exception msg)
            {
                throw msg;
            }
        }


    }
}
