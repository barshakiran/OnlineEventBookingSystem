using System;
using System.Net.Mail;
using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDAL;
using AutoMapper;
using OnlineEventBookingSystemDomain;
using System.Collections.Generic;
using System.Linq;
using OnlineEventBookingSystemDAL.Infrastructure;
using System.Net;

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
            bool mailSent = false;
            try
            {
                var eventName = EventDetailList().SingleOrDefault(x => x.Event_Id == bookingDetailDomainModel.Event_Id).Event_Name;
                var emailBody = "Event is booked " + bookingDetailDomainModel.Booking_TicketCount +" "+ " tickets for the event " + eventName + " is booked."
                    + Environment.NewLine + "For ticket detail login to the website.";
                string subject = "Event Booking Confirmation";
                bool sent = SendEMail(bookingDetailDomainModel.Email, subject, emailBody);
                if (sent)
                {
                    mailSent = true;
                    bookingDetailDomainModel.IsConfirmationSent = true;
                }
            }
            catch (Exception ex)
            {
                // throw ex;
                mailSent = false;
                return mailSent;
            }
            return mailSent;
        }

        public bool SendEMail(string recipient, string subject, string message)
        {
            bool isMessageSent = false;
            string clientAddress = "smtp.gmail.com";
            string senderAddress = "usert4185@gmail.com";
            string netPassword = "usertest@4185";
            //Intialise Parameters  
            SmtpClient client = new System.Net.Mail.SmtpClient(clientAddress);
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            NetworkCredential credentials = new NetworkCredential(senderAddress, netPassword);
            client.EnableSsl = true;
            client.Credentials = credentials;
            try
            {
                var mail = new System.Net.Mail.MailMessage(senderAddress.Trim(), recipient.Trim());
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                client.Send(mail);
                isMessageSent = true;
            }
            catch (Exception ex)
            {
                isMessageSent = false;
            }
            return isMessageSent;
        }

        public List<EventDetailDomainModel> DisplayAllUserEvent(string eventType, string city)
        {

            try
            {
                //<EventDetail> userEventList = new List<EventDetail>();
                List<EventDetailDomainModel> userEventsTemp = new List<EventDetailDomainModel>();
                List<EventDetailDomainModel> userEvents = new List<EventDetailDomainModel>();
                var cityList = DisplayCityList();
                var eventList = EventDetailList();

                int locationId;
                if (!string.IsNullOrEmpty(eventType) && !string.IsNullOrEmpty(city))
                {
                    //locationId = cityList.SingleOrDefault(e => e.City == city).Location_Id;
                    //userEventList = eventDetailDataHandler.GetAll(e => e.Event_Type == eventType).Select(m => new EventDetail

                    locationId = cityList.SingleOrDefault(e => e.City == city).Location_Id;
                    userEventsTemp = eventList.Where(e => e.Event_Type == eventType).Select(m => new EventDetailDomainModel
                    {
                        Event_Name = m.Event_Name,
                        Event_Description = m.Event_Description,
                        Event_Type = m.Event_Type,
                        Event_Picture = m.Event_Picture,
                        Event_Id = m.Event_Id,
                        EventList = m.EventList.Where(x => x.Location_Id == locationId).ToList()
                    }).ToList();

                }
                else if (!string.IsNullOrEmpty(eventType))
                {
                    userEventsTemp = eventList.Where(e => e.Event_Type == eventType).Select(m => new EventDetailDomainModel
                    {
                        Event_Name = m.Event_Name,
                        Event_Description = m.Event_Description,
                        Event_Type = m.Event_Type,
                        Event_Picture = m.Event_Picture,
                        Event_Id = m.Event_Id,
                        EventList = m.EventList
                    }).ToList();
                }
                else if (!string.IsNullOrEmpty(city))
                {
                    locationId = cityList.SingleOrDefault(e => e.City == city).Location_Id;
                    userEventsTemp = eventList.Select(m => new EventDetailDomainModel
                    {
                        Event_Name = m.Event_Name,
                        Event_Description = m.Event_Description,
                        Event_Type = m.Event_Type,
                        Event_Picture = m.Event_Picture,
                        Event_Id = m.Event_Id,
                        EventList = m.EventList.Where(x => x.Location_Id == locationId).ToList()
                    }).ToList();
                }
                else
                {
                    userEventsTemp = eventList.Select(m => new EventDetailDomainModel
                    {
                        Event_Name = m.Event_Name,
                        Event_Description = m.Event_Description,
                        Event_Type = m.Event_Type,
                        Event_Picture = m.Event_Picture,
                        Event_Id = m.Event_Id,
                        EventList = m.EventList
                    }).ToList();
                }

               
               // var mapper = InitializeAutomapper();
               // var cityName = mapper.Map<List<LocationDomainModel>>(cityList);
               // userEventsTemp = mapper.Map<List<EventDetailDomainModel>>(userEventList);
                foreach (var item in userEventsTemp)
                {
                    if(item.EventList.Count!=0)
                    {
                        foreach (var cities in item.EventList)
                        {
                            //cities.City = locationDataHandler.SingleOrDefault(x => x.Location_Id == cities.Location_Id).City;
                            cities.City = cityList.SingleOrDefault(x => x.Location_Id == cities.Location_Id).City;
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
                UserEventDetailsDomainModel userEvents = new UserEventDetailsDomainModel();
                if (eventId != 0)
                {
                    userEvents = EventDetailList().SingleOrDefault(e => e.Event_Id == eventId);
                }
                userEvents.Booking_Loc = DisplayCityList().SingleOrDefault(x => x.Location_Id == locationId).City;
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
                bookingDetailDomainModel.UserName = UserDetailList().SingleOrDefault(x => x.User_Id == bookedEventDetail.User_Id).User_Name;
                bookingDetailDomainModel.Event_Name = EventDetailList().SingleOrDefault(x => x.Event_Id == bookingDetailDomainModel.Event_Id).Event_Name;
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
                    //userId = userDataHandler.SingleOrDefault(x => x.User_Name == userName).User_Id;
                    userId = UserDetailList().SingleOrDefault(x => x.User_Name == userName).User_Id;
                    bookedEventDetail = bookingDetailDataHandler.GetAll(e => e.User_Id == userId).ToList();
                }
                configuration = new MapperConfiguration(x => x.CreateMap<BookingDetail, BookingDetailDomainModel>().ReverseMap());
                mapper = new Mapper(configuration);
                mapper.Map(bookedEventDetail, bookingDetailDomainModel);
                var eventList = EventDetailList();
                foreach (var item in bookingDetailDomainModel)
                {
                   
                    item.UserName = userName;
                   // item.Event_Name = eventDetailDataHandler.SingleOrDefault(x => x.Event_Id == item.Event_Id).Event_Name;
                    item.Event_Name = eventList.SingleOrDefault(x => x.Event_Id == item.Event_Id).Event_Name;
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
            try
            {
                if (bookingDetailDomainModel != null)
                {
                    bookingDetail = new BookingDetail();
                    var userList = UserDetailList();
                    configuration = new MapperConfiguration(x => x.CreateMap<BookingDetailDomainModel, BookingDetail>().ReverseMap());
                    mapper = new Mapper(configuration);
                    mapper.Map(bookingDetailDomainModel, bookingDetail);
                    bookingDetail.User_Id = userList.SingleOrDefault(x => x.User_Name == bookingDetailDomainModel.UserName).User_Id;

                    var userBookedEventList = bookingDetailDataHandler.GetAll(e => e.Event_Id == bookingDetailDomainModel .Event_Id && e.User_Id == bookingDetail.User_Id ).ToList();
                   if(userBookedEventList.Count == 0 && bookingDetailDomainModel.Booking_Date > System.DateTime.Now)
                    {                       
                        bookingDetailDataHandler.Insert(bookingDetail);
                        bookingId = bookingDetail.Booking_Id;
                        bookingDetailDomainModel.Email = userList.SingleOrDefault(x => x.User_Id == bookingDetail.User_Id).User_Email;
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

        public List<UserEventDetailsDomainModel> EventDetailList()
        {
            List<UserEventDetailsDomainModel> userEventDetailDomainList;
            try
            {
                userEventDetailDomainList = new List<UserEventDetailsDomainModel>();
                var eventDetailList = eventDetailDataHandler.GetAll().ToList();
                var mapper = InitializeAutomapperForUser(); 
                userEventDetailDomainList = mapper.Map<List<UserEventDetailsDomainModel>>(eventDetailList);
                return userEventDetailDomainList;
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

    }
}
