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

        public List<UserEventDetailsDomainModel> DisplayAllUserEvent(string eventType, string city)
        {

            try
            {
                List<EventLocation> userEventList = new List<EventLocation>();


                if (!string.IsNullOrEmpty(eventType) && !string.IsNullOrEmpty(city))
                {
                    userEventList = eventLocDataHandler.GetAll(e => e.EventDetail.Event_Type == eventType && e.Location.City == city).ToList();
                }
                else if (!string.IsNullOrEmpty(eventType))
                {
                    userEventList = eventLocDataHandler.GetAll(e => e.EventDetail.Event_Type == eventType).ToList();
                }
                else if (!string.IsNullOrEmpty(city))
                {
                    userEventList = eventLocDataHandler.GetAll(e => e.Location.City == city).ToList();
                }
                else
                {
                    userEventList = eventLocDataHandler.GetAll().ToList();
                }
               

                var userEvents = userEventList.Select(e=>
                                   new UserEventDetailsDomainModel
                                  {
                                      Event_Name = e.EventDetail.Event_Name,
                                      Event_Type = e.EventDetail.Event_Type,
                                      Event_Description = e.EventDetail.Event_Description,
                                      Event_Picture = e.EventDetail.Event_Picture,
                                      EventLocation_DateAndTime = e.EventLocation_DateAndTime,
                                      EventLocation_Price = e.EventLocation_Price, 
                                      City = e.Location.City,
                                     // Location_Address = e.LocationData.Location_Address
                                  }).ToList();

              

                return userEvents;

            }
            catch (System.Exception msg)
            {
                throw msg;
            }
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

        //public string AddEventDetails(UserEventDetailsDomainModel eventDModel)
        //{
        //    EventLocation newEventLoc;
        //    try
        //    {
        //        if (eventDModel != null)
        //        {
        //            newEventLoc = new EventLocation();
        //            mapper.Map(eventDModel, newEventLoc);
        //            eventLocDataHandler.Insert(newEventLoc);
        //            return "Inserted";
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception msg)
        //    {
        //        throw msg;
        //    }
        //}

    }
}
