using System;
using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDAL;
using AutoMapper;
using OnlineEventBookingSystemDomain;
using System.Collections.Generic;
using System.Linq;
using OnlineEventBookingSystemDAL.Infrastructure;

namespace OnlineEventBookingSystemBL
{
   public class UserEventDetailsBusiness:IUserEventDetailsBusiness
    {
        private IEventDetailDataHandler eventDetailDataHandler;
        private IBookingDetailDataHandler bookingDetailDataHandler;
        private ILocationDataHandler locationDataHandler;
        private IUserDataHandler userDataHandler;
        private MapperConfiguration configuration;
        private Mapper mapper;
       
        public UserEventDetailsBusiness(IUserDataHandler _userDataHandler,IBookingDetailDataHandler _bookingDetailDataHandler, IEventDetailDataHandler _eventDetailDataHandler, ILocationDataHandler _locationDataHandler)
        {
           
            eventDetailDataHandler = _eventDetailDataHandler;
            bookingDetailDataHandler = _bookingDetailDataHandler;
            locationDataHandler = _locationDataHandler;
            userDataHandler = _userDataHandler;
        }

        public bool SendEmailNotification(BookingDetailDomainModel bookingDetailDomainModel)
        {

            try
            {

               EmailHelper mailHelper = new EmailHelper(EmailHelper.EMAIL_SENDER, EmailHelper.SMTP_CLIENT, EmailHelper.EMAIL_CREDENTIALS);
               var eventName = eventDetailDataHandler.SingleOrDefault(x => x.Event_Id == bookingDetailDomainModel.Event_Id).Event_Name;
               var emailBody = EmailHelper.EMAIL_BODY+ Environment.NewLine+ bookingDetailDomainModel.Booking_TicketCount +" "+ " tickets for the event " + eventName + " is booked."
                    + Environment.NewLine + "For ticket detail login to the website.";
                if (mailHelper.SendEMail(bookingDetailDomainModel.Email, EmailHelper.EMAIL_SUBJECT, emailBody))
                {
                    bookingDetailDomainModel.IsConfirmationSent = true;
                }
            }
            catch (Exception ex)
            {
               // throw ex;
                return false;
            }
            return true;
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

        public UserEventDetailsDomainModel DisplayUserBookingEventDetails(int eventId,int locationId)
        {

            try
            {
                EventDetail eventDetail = new EventDetail();
                UserEventDetailsDomainModel userEvents = new UserEventDetailsDomainModel();
                if (eventId != 0)
                {
                    eventDetail = eventDetailDataHandler.SingleOrDefault(e => e.Event_Id == eventId);
                }
                var mapper = InitializeAutomapperForUser();
                userEvents = mapper.Map<UserEventDetailsDomainModel>(eventDetail);
                userEvents.Booking_Loc = locationDataHandler.SingleOrDefault(x => x.Location_Id == locationId).City;
                userEvents.EventLocation_Price = userEvents.EventList[0].EventLocation_Price;
                userEvents.Booking_Date = userEvents.EventList[0].EventLocation_DateAndTime;               
                return userEvents; 
            }
            catch (System.Exception msg)
            {
                throw msg;
            }
        }

        public BookingDetailDomainModel DisplayUserBookedEventDetails(int bookingId)
        {

            try
            {
                BookingDetail bookedEventDetail = new BookingDetail();
                BookingDetailDomainModel bookingDetailDomainModel = new BookingDetailDomainModel();
                if (bookingId != 0)
                {
                    bookedEventDetail = bookingDetailDataHandler.SingleOrDefault(e => e.Booking_Id == bookingId);
                }
                if (bookedEventDetail == null)
                {
                    return null;
                }
                configuration = new MapperConfiguration(x => x.CreateMap<BookingDetail, BookingDetailDomainModel>().ReverseMap());
                mapper = new Mapper(configuration);
                mapper.Map(bookedEventDetail, bookingDetailDomainModel);
                bookingDetailDomainModel.UserName = userDataHandler.SingleOrDefault(x => x.User_Id == bookedEventDetail.User_Id).User_Name;
                bookingDetailDomainModel.Event_Name = eventDetailDataHandler.SingleOrDefault(x => x.Event_Id == bookingDetailDomainModel.Event_Id).Event_Name;
                return bookingDetailDomainModel;
               
            }
            catch (System.Exception msg)
            {
                throw msg;
            }
        }

        public List<BookingDetailDomainModel> DisplayUserBookedEventsList(string userName)
        {

            try
            {
               List<BookingDetail> bookedEventDetail = new List<BookingDetail>();
                int userId = 0;
                List<BookingDetailDomainModel> bookingDetailDomainModel = new List<BookingDetailDomainModel>();
                if (!string.IsNullOrEmpty(userName))
                {
                    userId = userDataHandler.SingleOrDefault(x => x.User_Name == userName).User_Id;
                    bookedEventDetail = bookingDetailDataHandler.GetAll(e => e.User_Id == userId).ToList();
                }
                configuration = new MapperConfiguration(x => x.CreateMap<BookingDetail, BookingDetailDomainModel>().ReverseMap());
                mapper = new Mapper(configuration);
                mapper.Map(bookedEventDetail, bookingDetailDomainModel);
               
                foreach (var item in bookingDetailDomainModel)
                {
                    item.UserName = userName;
                    item.Event_Name = eventDetailDataHandler.SingleOrDefault(x => x.Event_Id == item.Event_Id).Event_Name;
                }
               
                return bookingDetailDomainModel;
            }
            catch (System.Exception msg)
            {
                throw msg;
            }
        }

        public int AddUserBookingEventDetails(BookingDetailDomainModel bookingDetailDomainModel)
        {
            BookingDetail bookingDetail ;
            int bookingId = 0;
           // string email = string.Empty;
            try
            {
                if (bookingDetailDomainModel != null)
                {
                    bookingDetail = new BookingDetail();
                   
                    configuration = new MapperConfiguration(x => x.CreateMap<BookingDetailDomainModel, BookingDetail>().ReverseMap());
                    mapper = new Mapper(configuration);
                    mapper.Map(bookingDetailDomainModel, bookingDetail);
                    bookingDetail.User_Id = userDataHandler.SingleOrDefault(x => x.User_Name == bookingDetailDomainModel.UserName).User_Id;

                    var userBookedEventList = bookingDetailDataHandler.GetAll(e => e.Event_Id == bookingDetailDomainModel .Event_Id && e.User_Id == bookingDetail.User_Id ).ToList();
                   if(userBookedEventList.Count == 0 && bookingDetailDomainModel.Booking_Date > System.DateTime.Now)
                    {
                       
                        bookingDetailDataHandler.Insert(bookingDetail);
                        bookingId = bookingDetail.Booking_Id;
                        bookingDetailDomainModel.Email = userDataHandler.SingleOrDefault(x => x.User_Id == bookingDetail.User_Id).User_Email;
                        if (SendEmailNotification(bookingDetailDomainModel) == true)
                        {
                            bookingDetail.IsConfirmationSent = true;
                            bookingDetailDataHandler.Update(bookingDetail);
                        }
                    }

                    return bookingId;
                }
                else
                {
                    return bookingId;
                }
            }
            catch (Exception msg)
            {
                throw msg;
            }
        }

        public bool DeleteBookedEvent(int id)
        {
            BookingDetailDomainModel bookedEventDetailDomainModel;
            try
            {
                bookedEventDetailDomainModel = new BookingDetailDomainModel();
                if (id == 0)
                {
                    return false;
                }
                else
                {
                    bookingDetailDataHandler.Delete(s => s.Booking_Id == id);                    
                    return true;
                }
            }
            catch (Exception msg)
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

        static Mapper InitializeAutomapperForUser()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EventLocation, EventLocationDomainModel>();
                cfg.CreateMap<EventDetail, UserEventDetailsDomainModel>()
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
