using OnlineEventBookingSystemDAL;
using OnlineEventBookingSystemDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHelper
{
    public class DataInitializer
    {
        public static UserLoginDomainModel GetUserLoginData()
        {
            var loginData = new UserLoginDomainModel()
            { User_Id = 2, User_Password = "test1", User_Name = "TestUser1", IsAdmin =false };

            return loginData;
        }

        public static UserRegistrationDomainModel GetUserRegistrationData()
        {
            var regData = new UserRegistrationDomainModel()
            {
                User_Id = 1,
                User_Password = "test",
                User_Name = "TestUser",
                User_Address = "India",
                User_Email = "testc@123",
                User_PhoneNo = "1223456789",
                IsAdmin = false
            };

            return regData;
        }

        public static List<UserRegistrationDomainModel> GetAllUsers()
        {
            var dbdata = new List<UserRegistrationDomainModel>()
            {
                new UserRegistrationDomainModel {
                    User_Id = 1,
                    User_Password = "test",
                    User_Name = "TestUser",
                    User_Address = "India",
                    User_Email = "testc@123",
                    User_PhoneNo = "1223456789",
                    IsAdmin = false
                },
                new UserRegistrationDomainModel {
                    User_Id = 2,
                    User_Password = "test1",
                    User_Name = "TestUser1",
                    User_Address = "India",
                    User_Email = "testc@123",
                    User_PhoneNo = "1223456799",
                    IsAdmin = false
                }
            };
            return dbdata;
        }

        public static List<UserDetail> GetAllDbUsers()
        {
            var dbdata = new List<UserDetail>()
            {
                new UserDetail {
                    User_Id = 1,
                    User_Password = "test",
                    User_Name = "TestUser",
                    User_Address = "India",
                    User_Email = "testc@123",
                    User_PhoneNo = "1223456789",
                    IsAdmin = false
                },
                new UserDetail {
                    User_Id = 2,
                    User_Password = "test1",
                    User_Name = "TestUser1",
                    User_Address = "India",
                    User_Email = "testc@123",
                    User_PhoneNo = "1223456799",
                    IsAdmin = false
                }
            };
            return dbdata;
        }

        public static List<EventDetail> GetAllEventDetail()
        {

            DateTime eventDate = new DateTime(2021, 11, 30, 5, 10, 20);
            List<EventLocation> newEventList = new List<EventLocation>
              {
                  new EventLocation { Location_Id =10,Event_Id= 101,EventLocation_Price= 200,EventLocation_DateAndTime= eventDate},
                  new EventLocation { Location_Id =11,Event_Id= 101,EventLocation_Price= 300,EventLocation_DateAndTime= eventDate}
              };
                var eventData = new List<EventDetail>()
                {
                    new EventDetail
                    {
                        Event_Id = 101,
                        Event_Name = "Soul",
                        Event_Type = "Movie",
                        Event_Description = "Soul",
                        Event_Picture = "~/Images/soul.jfif",
                        EventLocations =newEventList
                    },

                    new EventDetail
                    {
                        Event_Id = 102,
                        Event_Name = "Lord Of Rings",
                        Event_Type = "Movie",
                        Event_Description = "Lord Of Rings",
                        Event_Picture = "~/Images/Lord.jfif",
                        EventLocations =newEventList
                    }
                           };
            return eventData;
        }

        public static List<EventLocation> GetAllEventLocation()
        {
            DateTime eventDate = new DateTime(2021, 11, 30, 5, 10, 20);
            List<EventLocation> newEventList = new List<EventLocation>
              {
                  new EventLocation{ Location_Id =10,EventLocation_Price= 200,EventLocation_DateAndTime= eventDate},
                  new EventLocation { Location_Id =11,EventLocation_Price= 300,EventLocation_DateAndTime= eventDate}
              };

            return newEventList;
        }

        public static List<Location> GetAllDBLocation()
        {
            DateTime eventDate = new DateTime(2021, 11, 30, 5, 10, 20);
            List<Location> newEventList = new List<Location>
              {
                  new Location{ Location_Id =10,City = "Bangalore",Address="Karnataka"},
                  new Location{ Location_Id =11,City = "Delhi",Address="Delhi"}

              };

            return newEventList;
        }

        public static List<LocationDomainModel> GetAllLocation()
        {
            List<LocationDomainModel> newEventList = new List<LocationDomainModel>
              {
                  new LocationDomainModel{ Location_Id =10,City = "Bangalore",Address="Karnataka"},
                  new LocationDomainModel{ Location_Id =11,City = "Delhi",Address="Delhi"}

              };

            return newEventList;
        }
      
        public static List<BookingDetail> GetAllBookedEventDetail()
        {

            DateTime eventDate = new DateTime(2021, 11, 30, 5, 10, 20);
            List<BookingDetail> bookedEventList = new List<BookingDetail>
              {
                  new BookingDetail { Booking_Id =1001,User_Id= 1,Event_Id= 101,Booking_Date= eventDate,Booking_TicketCount= 3,
                                      Booking_TotalAmount = 900,IsConfirmationSent = true,Booking_Loc= "Bangalore",Payment_Mode= "Debit Card"}
              };
           
            return bookedEventList;
        }
    }

    public class DataInitializerEventDetail
    {
        public static List<EventDetailDomainModel> GetAllEventDetailDomainModel()
        {
            DateTime eventDate = new DateTime(2021, 11, 30, 5, 10, 20);
            List<EventLocationDomainModel> newEventList = new List<EventLocationDomainModel>
              {
                  new EventLocationDomainModel { Location_Id =10,City="Bangalore",EventLocation_Price= 200,EventLocation_DateAndTime= eventDate},
                  new EventLocationDomainModel { Location_Id =11,City="Delhi",EventLocation_Price= 300,EventLocation_DateAndTime= eventDate}
              };


            var eventData = new List<EventDetailDomainModel>()
                {
                    new EventDetailDomainModel
                    {
                        Event_Id = 101,
                        Event_Name = "Soul",
                        Event_Type = "Movie",
                        Event_Description = "Soul",
                        Event_Picture = "~/Images/soul.jfif",
                        EventList = newEventList
                    },

                    new EventDetailDomainModel
                    {
                        Event_Id = 102,
                        Event_Name = "Lord Of Rings",
                        Event_Type = "Movie",
                        Event_Description = "Lord Of Rings",
                        Event_Picture = "~/Images/Lord.jfif",
                        EventList = newEventList
                    }
                           };
            return eventData;
        }
    }

    public class DataInitializerUserEventBookingDetail: DataInitializerEventDetail
    {
        public static List<UserEventDetailsDomainModel> GetAllUserEventBookingDetailDomainModel()
        {
            DateTime eventDate = new DateTime(2021, 11, 30, 5, 10, 20);
            List<UserEventDetailsDomainModel> newBookedEventList = new List<UserEventDetailsDomainModel>
              {
                  new UserEventDetailsDomainModel { Booking_Loc ="Bangalore",EventLocation_Price= 300,Booking_Date = eventDate}
              };

            return newBookedEventList;
        }

    }

    public class DataInitializerBookedEventDetail:DataInitializerUserEventBookingDetail
    {
        public static List<BookingDetailDomainModel> GetAllBookedEventDetailDomainModel()
        {
            
            List<BookingDetailDomainModel> newBookedEventList = new List<BookingDetailDomainModel>
              {
                  new BookingDetailDomainModel { Booking_Id =1001,UserName = "TestUser",Event_Id= 101,Email= "testc@123",Booking_TicketCount= 3,
                                      Booking_TotalAmount = 900,IsConfirmationSent = true,Payment_Mode= "Debit Card"}
              };

            return newBookedEventList;
        }
    }
}
