using AutoMapper;
using OnlineEventBookingSystemDAL;
using OnlineEventBookingSystemDomain;
using OnlineEventBookingSystemBL.Interface;


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
    }
}
