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
       
        public EventDetailBusiness(IEventDetailDataHandler _eventDetailDataHandler , IEventLocationDataHandler _eventLocationDataHandler, ILocationDataHandler _locationDataHandler)
        {
            locationDataHandler = _locationDataHandler;
            eventDetailDataHandler = _eventDetailDataHandler;
            eventLocationDataHandler = _eventLocationDataHandler;
            configuration = new MapperConfiguration(x => x.CreateMap<EventDetailDomainModel, EventDetail>().ReverseMap());
            mapper = new Mapper(configuration);
        }
        
        public string AddEventDetails(EventDetailDomainModel eventDModel)
        {
            EventDetail newEvent;
            EventLocation newEventLocation;
            List<EventLocation> list;

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
                    list = new List<EventLocation>();
                   
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
                            list.Add(newEventLocation);
                        }
                        
                    }
                    eventLocationDataHandler.InsertAll(list);

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
                userEventList = eventDetailDataHandler.GetAll().ToList();
                              
                var cityList = LocationDetailList();
                var mapper = InitializeAutomapper();
                var city = mapper.Map<List<LocationDomainModel>>(cityList);
                userEvents = mapper.Map<List<EventDetailDomainModel>>(userEventList);
                foreach(var item in userEvents)
                {
                    foreach( var cities in item.EventList)
                    {
                        cities.City = locationDataHandler.SingleOrDefault(x => x.Location_Id == cities.Location_Id).City;
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
                List<EventDetail> userEventList = new List<EventDetail>();
                EventDetailDomainModel userEvents = new EventDetailDomainModel();
                if (eventId != 0 && locationId != 0)
                {
                    userEventList = eventDetailDataHandler.GetAll(e => e.Event_Id == eventId).Select(m => new EventDetail
                    {
                        Event_Name= m.Event_Name,
                        Event_Description = m.Event_Description,
                        Event_Type = m.Event_Type,
                        Event_Picture = m.Event_Picture,
                        Event_Id = m.Event_Id,
                        EventLocations = m.EventLocations.Where(x => x.Location_Id == locationId).ToList()
                    }).ToList();

                }
                else if (eventId != 0)
                {
                    userEventList = eventDetailDataHandler.GetAll(e => e.Event_Id == eventId).ToList();
                }
                else
                {
                    userEventList = eventDetailDataHandler.GetAll().ToList();
                }
                var cityList = LocationDetailList();
                var mapper = InitializeAutomapper();
                var city = mapper.Map<List<LocationDomainModel>>(cityList);
                userEvents = mapper.Map<EventDetailDomainModel>(userEventList[0]);
                foreach (var item in userEvents.EventList)
                {
                    //foreach (var cities in item.EventList)
                    //{
                        item.City = locationDataHandler.SingleOrDefault(x => x.Location_Id == item.Location_Id).City;
                   // }
                }
                return userEvents;
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

        public EventDetailDomainModel DisplayEvent(int eventId)
        {
            EventDetailDomainModel eventDModel;

            try 
            {
                if (eventId == 0)
                {
                    return null;
                }
                eventDModel = new EventDetailDomainModel();
                var data = eventDetailDataHandler.SingleOrDefault(s => s.Event_Id== eventId);
                if (data == null)
                {
                    return null;
                }
                mapper.Map(data, eventDModel);
                return eventDModel;
            }
            catch(Exception msg)
            {
                throw msg;
            }
          
        }

        public string UpdateEventDetails(EventDetailDomainModel eventDomainModel)
        {
            EventDetail newEvent;
            EventLocation newEventLocation;
            List<EventLocation> list;

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
                    list = new List<EventLocation>();

                    foreach (var item in eventDomainModel.EventList)
                    {
                        newEventLocation = new EventLocation();
                        var userEventList = eventLocationDataHandler.GetAll(e => e.Location_Id == item.Location_Id && e.Event_Id == newEvent.Event_Id).ToList();
                        newEventLocation.EventLocation_DateAndTime = item.EventLocation_DateAndTime;
                        newEventLocation.EventLocation_Price = item.EventLocation_Price;
                        newEventLocation.Location_Id = item.Location_Id;
                        newEventLocation.Event_Id = newEvent.Event_Id;
                            list.Add(newEventLocation);

                    }
                    eventLocationDataHandler.UpdateAll(list);

                    return "Inserted";
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

    
    }
}
