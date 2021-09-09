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
                        item.Location_Id = locationDataHandler.SingleOrDefault(e => e.City == item.City).Location_Id;
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

        public List<EventDetailDomainModel> DisplayEventDetails()
        {
            List<EventDetailDomainModel> list;
            try
            {
                list = eventDetailDataHandler.GetAll().Select(m => new EventDetailDomainModel
                {
                    Event_Id = m.Event_Id,
                    Event_Name = m.Event_Name,
                    Event_Type = m.Event_Type,
                    Event_Description = m.Event_Description,
                    Event_Picture = m.Event_Picture
                }).ToList();

                return list;
            }
            catch(Exception msg)
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

        public string UpdateEventDetails(EventDetailDomainModel eventDModel)
        {
            EventDetail user;
            try
            {
                if (eventDModel != null)
                {
                    user = new EventDetail();
                    mapper.Map(eventDModel, user);
                    eventDetailDataHandler.Update(user);
                    return "Updated";
                }
                else
                {
                    return null;
                }
            }
            catch(Exception msg)
            {
                throw (msg);

            }
            
        }

        public bool DeleteEvent(int id)
        {
            try
            {
                if (id == 0)
                {
                    return false;
                }
                else
                {
                    eventDetailDataHandler.Delete(s => s.Event_Id == id);
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
