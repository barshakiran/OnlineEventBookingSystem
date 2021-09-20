using System;
using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDAL;
using AutoMapper;
using OnlineEventBookingSystemDomain;
using System.Collections.Generic;
using System.Linq;


namespace OnlineEventBookingSystemBL
{
   public class UserEventDetailsBusiness:IUserEventDetailsBusiness
    {
        private IEventDetailDataHandler eventDetailDataHandler;
        private IEventLocationDataHandler eventLocDataHandler;
        private ILocationDataHandler locationDataHandler;
        private MapperConfiguration configuration;
        private Mapper mapper;
        public UserEventDetailsBusiness(IEventLocationDataHandler _eventLocDataHandler, IEventDetailDataHandler _eventDetailDataHandler, ILocationDataHandler _locationDataHandler)
        {
            eventDetailDataHandler = _eventDetailDataHandler;
            eventLocDataHandler = _eventLocDataHandler;
            locationDataHandler = _locationDataHandler;
            configuration = new MapperConfiguration(x => x.CreateMap<EventLocationDomainModel, Location>().ReverseMap());
            mapper = new Mapper(configuration);
        }

        public List<EventDetailDomainModel> DisplayAllUserEvent(string eventType, string city)
        {

            try
            {
                List<EventDetail> userEventList = new List<EventDetail>();
                List<EventDetailDomainModel> userEventsTemp = new List<EventDetailDomainModel>();
                List<EventDetailDomainModel> userEvents = new List<EventDetailDomainModel>();
                int locationId;
                if (!string.IsNullOrEmpty(eventType) && !string.IsNullOrEmpty(city))
                {
                    locationId = locationDataHandler.SingleOrDefault(e => e.City == city).Location_Id;
                    userEventList = eventDetailDataHandler.GetAll(e => e.Event_Type == eventType).Select(m => new EventDetail
                    {
                        Event_Name = m.Event_Name,
                        Event_Description = m.Event_Description,
                        Event_Type = m.Event_Type,
                        Event_Picture = m.Event_Picture,
                        Event_Id = m.Event_Id,
                        EventLocations = m.EventLocations.Where(x => x.Location_Id == locationId).ToList()
                    }).ToList();

                }
                else if (!string.IsNullOrEmpty(eventType))
                {
                    userEventList = eventDetailDataHandler.GetAll(e => e.Event_Type == eventType).Select(m => new EventDetail
                    {
                        Event_Name = m.Event_Name,
                        Event_Description = m.Event_Description,
                        Event_Type = m.Event_Type,
                        Event_Picture = m.Event_Picture,
                        Event_Id = m.Event_Id,
                        EventLocations = m.EventLocations
                    }).ToList();
                }
                else if (!string.IsNullOrEmpty(city))
                {
                    locationId = locationDataHandler.SingleOrDefault(e => e.City == city).Location_Id;
                    userEventList = eventDetailDataHandler.GetAll().Select(m => new EventDetail
                    {
                        Event_Name = m.Event_Name,
                        Event_Description = m.Event_Description,
                        Event_Type = m.Event_Type,
                        Event_Picture = m.Event_Picture,
                        Event_Id = m.Event_Id,
                        EventLocations = m.EventLocations.Where(x => x.Location_Id == locationId).ToList()
                    }).ToList();
                }
                else
                {
                    userEventList = eventDetailDataHandler.GetAll().Select(m => new EventDetail
                    {
                        Event_Name = m.Event_Name,
                        Event_Description = m.Event_Description,
                        Event_Type = m.Event_Type,
                        Event_Picture = m.Event_Picture,
                        Event_Id = m.Event_Id,
                        EventLocations = m.EventLocations
                    }).ToList();
                }

                var cityList = DisplayCityList();
                var mapper = InitializeAutomapper();
                var cityName = mapper.Map<List<LocationDomainModel>>(cityList);
                userEventsTemp = mapper.Map<List<EventDetailDomainModel>>(userEventList);
                foreach (var item in userEventsTemp)
                {
                    if(item.EventList.Count!=0)
                    {
                        foreach (var cities in item.EventList)
                        {
                            cities.City = locationDataHandler.SingleOrDefault(x => x.Location_Id == cities.Location_Id).City;
                        }
                        userEvents.Add(item);
                    }
                    
                }
                return userEvents;                
            }
            catch (System.Exception msg)
            {
                throw msg;
            }
        }


        static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EventLocation, EventLocationDomainModel>();
                cfg.CreateMap<EventDetail, EventDetailDomainModel>()
                .ForMember(dest => dest.EventList, act => act.MapFrom(src => src.EventLocations));
                cfg.CreateMap<Location, LocationDomainModel>()
                .ForMember(dest => dest.City, act => act.MapFrom(src => src.City));
            });
            var mapper = new Mapper(config);
            return mapper;
        }
       
        public List<LocationDomainModel> DisplayCityList()
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

    }
}
