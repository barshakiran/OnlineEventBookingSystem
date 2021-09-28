using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineEventBookingSystemBL;
using OnlineEventBookingSystemDAL;
using OnlineEventBookingSystemDomain;
using System;
using System.Collections.Generic;
using TestHelper;

namespace OnlineEventBookingSystem.Test.BL
{
    /// <summary>
    /// Summary description for UserEventDetailsBusinessTest
    /// </summary>
    [TestClass]
    public class UserEventDetailsBusinessTest
    {
        #region Variables
        private List<EventDetailDomainModel> eventDetailDomainModels;
        private List<UserDetail> userDetails;
        private List<UserEventDetailsDomainModel> userEventDetailDomainModels;
        private List<EventDetail> eventDetails;
        private List<BookingDetailDomainModel> bookingDetailDomainModels;
        private List<BookingDetail> bookingDetails;
        private List<LocationDomainModel> locationDomainModels;
        private List<Location> locations;
        private LocationDomainModel locationDomainModel;
        private BookingDetail bookingDetail;
        private EventDetailDomainModel eventDetailDomainModel;
        private EventDetail eventDetail;
        private UserEventDetailsDomainModel userEventDetailDomainModel;
        private BookingDetailDomainModel bookingDetailDomainModel;

        private Mock<IBookingDetailDataHandler> mockBookingDetailDataHandler;
        private Mock<IEventDetailDataHandler> mockEventDetailDataHandler;
        private Mock<IUserDataHandler> mockUserDataHandler;
        private Mock<ILocationDataHandler> mockLocationDataHandler;
        private UserEventDetailsBusiness userEventDetailBusiness;
        #endregion

        public UserEventDetailsBusinessTest()
        {
            mockEventDetailDataHandler = new Mock<IEventDetailDataHandler>();
            mockBookingDetailDataHandler = new Mock<IBookingDetailDataHandler>();
            mockUserDataHandler = new Mock<IUserDataHandler>();
            mockLocationDataHandler = new Mock<ILocationDataHandler>();
            userEventDetailBusiness = new UserEventDetailsBusiness(mockUserDataHandler.Object, mockBookingDetailDataHandler.Object, mockEventDetailDataHandler.Object, mockLocationDataHandler.Object);
        }

        [TestMethod]
        public void DisplayAllUserEvent_ShouldDisplayEventList_ForTheCityAndEventType()
        {
            //Arrange
            //userEventDetailDomainModels = DataInitializerUserEventBookingDetail.GetAllUserEventBookingDetailDomainModel();
            // userEventDetailDomainModel = userEventDetailDomainModels[0];
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetailDomainModel = eventDetailDomainModels[0];
            eventDetails = DataInitializer.GetAllEventDetail();
            locations = DataInitializer.GetAllDBLocation();
            string eventType = "Movie";
            string city = "Bangalore";

            //Act
            mockEventDetailDataHandler.Setup(m => m.GetAll()).Returns(eventDetails);
            mockLocationDataHandler.Setup(m => m.GetAll()).Returns(locations);
            var res = userEventDetailBusiness.DisplayAllUserEvent(eventType, city);

            //Assert
            Assert.AreEqual(eventDetailDomainModel.Event_Id, res[0].Event_Id);

        }

        [TestMethod]
        public void DisplayAllUserEvent_ShouldDisplayEventList_ForTheCity()
        {
            //Arrange
            //userEventDetailDomainModels = DataInitializerUserEventBookingDetail.GetAllUserEventBookingDetailDomainModel();
            // userEventDetailDomainModel = userEventDetailDomainModels[0];
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetailDomainModel = eventDetailDomainModels[0];
            eventDetails = DataInitializer.GetAllEventDetail();
            locations = DataInitializer.GetAllDBLocation();
            string eventType = "";
            string city = "Bangalore";

            //Act
            mockEventDetailDataHandler.Setup(m => m.GetAll()).Returns(eventDetails);
            mockLocationDataHandler.Setup(m => m.GetAll()).Returns(locations);
            var res = userEventDetailBusiness.DisplayAllUserEvent(eventType, city);

            //Assert
            Assert.AreEqual(eventDetailDomainModel.Event_Id, res[0].Event_Id);

        }
        [TestMethod]
        public void DisplayAllUserEvent_ShouldDisplayEventList_ForTheEventType()
        {
            //Arrange
            //userEventDetailDomainModels = DataInitializerUserEventBookingDetail.GetAllUserEventBookingDetailDomainModel();
            // userEventDetailDomainModel = userEventDetailDomainModels[0];
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetailDomainModel = eventDetailDomainModels[0];
            eventDetails = DataInitializer.GetAllEventDetail();
            locations = DataInitializer.GetAllDBLocation();
            string eventType = "Movie";
            string city = "";

            //Act
            mockEventDetailDataHandler.Setup(m => m.GetAll()).Returns(eventDetails);
            mockLocationDataHandler.Setup(m => m.GetAll()).Returns(locations);
            var res = userEventDetailBusiness.DisplayAllUserEvent(eventType, city);

            //Assert
            Assert.AreEqual(eventDetailDomainModel.Event_Id, res[0].Event_Id);

        }
        [TestMethod]
        public void DisplayAllUserEvent_ShouldDisplayEventList()
        {
            //Arrange
            //userEventDetailDomainModels = DataInitializerUserEventBookingDetail.GetAllUserEventBookingDetailDomainModel();
            // userEventDetailDomainModel = userEventDetailDomainModels[0];
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetailDomainModel = eventDetailDomainModels[0];
            eventDetails = DataInitializer.GetAllEventDetail();
            locations = DataInitializer.GetAllDBLocation();
            string eventType = "";
            string city = "";

            //Act
            mockEventDetailDataHandler.Setup(m => m.GetAll()).Returns(eventDetails);
            mockLocationDataHandler.Setup(m => m.GetAll()).Returns(locations);
            var res = userEventDetailBusiness.DisplayAllUserEvent(eventType, city);

            //Assert
            Assert.AreEqual(eventDetailDomainModel.Event_Id, res[0].Event_Id);

        }

        [TestMethod]
        public void DisplayUserBookingEventDetails_ShouldDisplayDetailsOfTheBookingEvent()
        {
            //Arrange
            userEventDetailDomainModels = DataInitializerUserEventDetails.GetAllUserEventDetailDomainModel();
            userEventDetailDomainModel = userEventDetailDomainModels[0];
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetailDomainModel = eventDetailDomainModels[0];
            eventDetails = DataInitializer.GetAllEventDetail();
            locations = DataInitializer.GetAllDBLocation();
            locationDomainModels = DataInitializer.GetAllLocation();
            locationDomainModel = locationDomainModels[0];

            //Act
            mockEventDetailDataHandler.Setup(m => m.GetAll()).Returns(eventDetails);
            mockLocationDataHandler.Setup(m => m.GetAll()).Returns(locations);
            var res = userEventDetailBusiness.DisplayUserBookingEventDetails(eventDetailDomainModel.Event_Id, locationDomainModel.Location_Id);

            //Assert
            Assert.AreEqual(userEventDetailDomainModel.Booking_Loc, res.Booking_Loc);

        }

        [TestMethod]
        public void DisplayUserBookedEventDetails_ShouldDisplayDetailOfTheBookedEvent()
        {
            //Arrange
            bookingDetails = DataInitializer.GetAllBookedEventDetail();
            bookingDetail = bookingDetails[0];
            bookingDetailDomainModels = DataInitializerBookedEventDetail.GetAllBookedEventDetailDomainModel();
            bookingDetailDomainModel = bookingDetailDomainModels[0];
            eventDetails = DataInitializer.GetAllEventDetail();
            userDetails = DataInitializer.GetAllDbUsers();
            int bookingID = bookingDetail.Event_Id;

            //Act
            mockBookingDetailDataHandler.Setup(m => m.SingleOrDefault(e => e.Booking_Id == bookingID)).Returns(bookingDetail);
            mockEventDetailDataHandler.Setup(m => m.GetAll()).Returns(eventDetails);
            mockUserDataHandler.Setup(m => m.GetAll()).Returns(userDetails);
            var res = userEventDetailBusiness.DisplayUserBookedEventDetails(bookingDetailDomainModel.Event_Id);

            //Assert
            Assert.AreEqual(bookingDetailDomainModel.Event_Id, res.Event_Id);

        }

        [TestMethod]
        public void DisplayUserBookedEventsList_ShouldDisplayEventListForTheUser()
        {
            //Arrange
            bookingDetails = DataInitializer.GetAllBookedEventDetail();
            bookingDetail = bookingDetails[0];
            bookingDetailDomainModels = DataInitializerBookedEventDetail.GetAllBookedEventDetailDomainModel();
            bookingDetailDomainModel = bookingDetailDomainModels[0];
            eventDetails = DataInitializer.GetAllEventDetail();
            userDetails = DataInitializer.GetAllDbUsers();
            int userId = bookingDetail.User_Id;

            //Act
            mockBookingDetailDataHandler.Setup(m => m.GetAll(e => e.User_Id == userId)).Returns(bookingDetails);
            mockEventDetailDataHandler.Setup(m => m.GetAll()).Returns(eventDetails);
            mockUserDataHandler.Setup(m => m.GetAll()).Returns(userDetails);
            var res = userEventDetailBusiness.DisplayUserBookedEventsList(bookingDetailDomainModel.UserName);

            //Assert
            Assert.AreEqual(bookingDetailDomainModel.Event_Id, res[0].Event_Id);
        }

        [TestMethod]
        public void AddUserBookingEventDetails_ShouldAddBookingDetails()
        {
            //Arrange
            bookingDetails = DataInitializer.GetAllBookedEventDetail();
            bookingDetail = bookingDetails[0];
            bookingDetailDomainModels = DataInitializerBookedEventDetail.GetAllBookedEventDetailDomainModel();
            bookingDetailDomainModel = bookingDetailDomainModels[0];
            userDetails = DataInitializer.GetAllDbUsers();
            int userId = bookingDetail.User_Id;
            int eventId = bookingDetailDomainModel.Event_Id;
            //Act
            mockBookingDetailDataHandler.Setup(m => m.GetAll(e => e.Event_Id == eventId && e.User_Id == userId)).Returns(bookingDetails);
            mockUserDataHandler.Setup(m => m.GetAll()).Returns(userDetails);
            var res = userEventDetailBusiness.AddUserBookingEventDetails(bookingDetailDomainModel);

            //Assert
            Assert.AreEqual(bookingDetailDomainModel.Booking_Id, res);

        }

        [TestMethod]
        public void DeleteBookedEvent_ShouldDeleteBookedEvent()
        {
            //Arrange
            bookingDetails = DataInitializer.GetAllBookedEventDetail();
            bookingDetail = bookingDetails[0];
            bookingDetailDomainModels = DataInitializerBookedEventDetail.GetAllBookedEventDetailDomainModel();
            bookingDetailDomainModel = bookingDetailDomainModels[0];
            
            //Act
            mockBookingDetailDataHandler.Setup(m => m.Delete(s => s.Booking_Id == bookingDetail.Booking_Id));
          
            var res = userEventDetailBusiness.DeleteBookedEvent(bookingDetailDomainModel.Booking_Id);

            //Assert
            Assert.AreEqual(true, res);

        }


        [TestMethod]
        public void SendEmailNotification_ShouldSendEmailNotificationToTheUser()
        {

            //Arrange
            bookingDetails = DataInitializer.GetAllBookedEventDetail();
            bookingDetail = bookingDetails[0];
            bookingDetailDomainModels = DataInitializerBookedEventDetail.GetAllBookedEventDetailDomainModel();
            bookingDetailDomainModel = bookingDetailDomainModels[0];
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetailDomainModel = eventDetailDomainModels[0];
            eventDetails = DataInitializer.GetAllEventDetail();
            eventDetail = eventDetails[0];
            int eventId = bookingDetailDomainModel.Event_Id;

            //Act
            mockEventDetailDataHandler.Setup(m => m.GetAll()).Returns(eventDetails);
            var res = userEventDetailBusiness.SendEmailNotification(bookingDetailDomainModel);

            //Assert
            Assert.AreEqual(true, res);

        }

    }
}
