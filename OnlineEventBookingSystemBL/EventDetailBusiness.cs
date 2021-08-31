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

        public EventDetailBusiness(IEventDetailDataHandler _eventDetailDataHandler)
        {
            eventDetailDataHandler = _eventDetailDataHandler;
            configuration = new MapperConfiguration(x => x.CreateMap<EventDetailDomainModel, EventDetail>().ReverseMap());
            mapper = new Mapper(configuration);
        }

        public string AddEventDetails(EventDetailDomainModel eventDModel)
        {
            EventDetail newEvent;
            try
            {
                if (eventDModel != null)
                {
                    newEvent = new EventDetail();
                    mapper.Map(eventDModel, newEvent);
                    eventDetailDataHandler.Insert(newEvent);
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
