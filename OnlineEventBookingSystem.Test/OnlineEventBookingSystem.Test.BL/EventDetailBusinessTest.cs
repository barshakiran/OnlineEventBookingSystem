using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDomain;
using System.Linq;
using System.Collections.Generic;
using TestHelper;
using OnlineEventBookingSystemDAL;
using OnlineEventBookingSystemBL;

namespace OnlineEventBookingSystem.Tests.BL
{
    /// <summary>
    /// Summary description for EventDetail BusinessTest
    /// </summary>
    [TestClass]
    public class EventDetailBusinessTest
    {
        #region Variables
        private List<EventDetailDomainModel> eventDetailDomainModels;
        private List<EventLocation> eventLocations;
        private List<UserRegistrationDomainModel> userDetailDomainModels;
        private List<UserDetail> userDetails;
        private List<EventDetail> eventDetails;
        private List<BookingDetailDomainModel> bookingDetailDomainModels;
        private List<BookingDetail> bookingDetails;
        private EventDetailDomainModel eventDetailDomainModel;
        private EventDetail eventDetail;
        private EventLocation eventLocation;
        private BookingDetailDomainModel bookingDetailDomainModel;
        private UserRegistrationDomainModel userDetailDomainModel;

        private Mock<IBookingDetailDataHandler> mockBookingDetailDataHandler;
        private Mock<IEventDetailDataHandler> mockEventDetailDataHandler;
        private Mock<IUserDataHandler> mockUserDataHandler; 
        private Mock<IEventLocationDataHandler> mockEventLocationDataHandler;
        private Mock<ILocationDataHandler> mockLocationDataHandler;

        private EventDetailBusiness eventDetailBusiness;
        #endregion
        public EventDetailBusinessTest()
        {
            mockEventDetailDataHandler = new Mock<IEventDetailDataHandler>();
            mockBookingDetailDataHandler = new Mock<IBookingDetailDataHandler>();
            mockUserDataHandler = new Mock<IUserDataHandler>();
            mockEventLocationDataHandler = new Mock<IEventLocationDataHandler>();
            mockLocationDataHandler = new Mock<ILocationDataHandler>();
            eventDetailBusiness = new EventDetailBusiness(mockBookingDetailDataHandler.Object, mockUserDataHandler.Object, mockEventDetailDataHandler.Object, mockEventLocationDataHandler.Object, mockLocationDataHandler.Object);
        }

        [TestMethod]
        public void AddEventDetails_ShouldAddEvent()
        {
            //Arrange
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetailDomainModel = eventDetailDomainModels[0];
           // EventDetail eventDetail = new EventDetail();

            //Act
            mockEventDetailDataHandler.Setup(m => m.Insert(It.IsAny<EventDetail>()));
            var res= eventDetailBusiness.AddEventDetails(eventDetailDomainModel);

            //Assert
            Assert.AreEqual(res, "Inserted");

        }
  
        [TestMethod]
        public void AddEventDetails_ShouldReturnNull()
        {
            //Arrange
            EventDetailDomainModel eventDModel = null;

            //Act
            mockEventDetailDataHandler.Setup(m => m.Insert(It.IsAny<EventDetail>()));
            var res = eventDetailBusiness.AddEventDetails(eventDModel);

            //Assert
             Assert.AreEqual(res, null);
        }

        [TestMethod]
        public void DisplayEventDetailList_ShouldReturnAllEvents()
         {
            //Arrange
            eventDetails = DataInitializer.GetAllEventDetail();
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            List<Location> locationDb = DataInitializer.GetAllDBLocation();

            //Act
            mockLocationDataHandler.Setup(m => m.GetAll()).Returns(locationDb);
            mockEventDetailDataHandler.Setup(m => m.GetAll()).Returns(eventDetails);
            var res = eventDetailBusiness.DisplayEventDetailList();

            //Assert
            Assert.AreEqual(eventDetailDomainModels[0].Event_Id, res[0].Event_Id);
        }

        [TestMethod]
        public void DisplayEventDetail_ShouldReturnTheEvent()
        {

            //Arrange
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetails = DataInitializer.GetAllEventDetail();
            eventDetail = eventDetails[0];
            eventDetailDomainModel = eventDetailDomainModels[0];
            int eventId = eventDetail.Event_Id;
            int locationId = eventDetailDomainModel.EventList[0].Location_Id;
            List<Location> locationDb = DataInitializer.GetAllDBLocation();

            //Act

            mockLocationDataHandler.Setup(m => m.GetAll()).Returns(locationDb);
            mockEventDetailDataHandler.Setup(m => m.GetAll()).Returns(eventDetails);
            var res = eventDetailBusiness.DisplayEventDetail(eventId, locationId);

            //assert
            Assert.AreEqual(eventDetailDomainModel.Event_Name, res.Event_Name);
        }

        [TestMethod]
        public void DisplayEventDetail_ShouldReturnTheEventAtAllLoc()
        {

            //Arrange
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetails = DataInitializer.GetAllEventDetail();
            eventDetail = eventDetails[0];
            eventDetailDomainModel = eventDetailDomainModels[0];
            int eventId = eventDetail.Event_Id;
            int locationId = 0;
            List<Location> locationDb = DataInitializer.GetAllDBLocation();

            //Act

            mockLocationDataHandler.Setup(m => m.GetAll()).Returns(locationDb);
            mockEventDetailDataHandler.Setup(m => m.GetAll()).Returns(eventDetails);
            var res = eventDetailBusiness.DisplayEventDetail(eventId, locationId);

            //assert
            Assert.AreEqual(eventDetailDomainModel.Event_Name, res.Event_Name);

        }

        [TestMethod]
        public void LocationDetailList_ShouldReturnLocations()
        {

            //Arrange
            List<Location>locationDb = DataInitializer.GetAllDBLocation();
            List<LocationDomainModel> location = DataInitializer.GetAllLocation();

            //Act
            mockLocationDataHandler.Setup(m => m.GetAll()).Returns(locationDb);
            var res = eventDetailBusiness.LocationDetailList();

            //assert
            Assert.AreEqual(location[0].Location_Id,res[0].Location_Id);
        }

        [TestMethod]
        public void UserDetailList_ShouldReturnUserDetails()
        {

            //Arrange
            userDetails = DataInitializer.GetAllDbUsers();
            userDetailDomainModels = DataInitializer.GetAllUsers();
            userDetailDomainModel = userDetailDomainModels[0];

            //Act
            mockUserDataHandler.Setup(m => m.GetAll()).Returns(userDetails);
            var res = eventDetailBusiness.UserDetailList();

            //assert
            Assert.AreEqual(userDetailDomainModel.User_Id, res[0].User_Id);
        }

        [TestMethod]
        public void EventDetailList_ShouldReturnEvents()
        {

            //Arrange
            eventDetails = DataInitializer.GetAllEventDetail();
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
           

            //Act
            mockEventDetailDataHandler.Setup(m => m.GetAll()).Returns(eventDetails);
            var res = eventDetailBusiness.EventDetailList();

            //assert
            Assert.AreEqual(eventDetailDomainModels[0].Event_Id, res[0].Event_Id);
        }

        [TestMethod]
        public void UpdateEventDetails_ShouldUpdateTheEvent()
        {

            //Arrange
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetails = DataInitializer.GetAllEventDetail();
            eventDetail = eventDetails[1];
            eventDetailDomainModel = eventDetailDomainModels[1];

            //Act
            mockEventDetailDataHandler.Setup(m => m.Update(It.IsAny<EventDetail>()));
            var res = eventDetailBusiness.UpdateEventDetails(eventDetailDomainModel);

            //Assert
            Assert.AreEqual(res, "Updated");
        }

        [TestMethod]
        public void DeleteEvent_ShouldDeleteTheEvent()
        {
            //Arrange
            eventLocations = DataInitializer.GetAllEventLocation();
            eventDetails = DataInitializer.GetAllEventDetail();
            eventDetail = eventDetails[1];
            eventLocation = eventLocations[1];

            //Act//
            mockEventLocationDataHandler.Setup(m => m.Delete(repo => repo.Event_Id == eventDetail.Event_Id && repo.Location_Id == eventLocation.Location_Id ));
            mockEventDetailDataHandler.Setup(m => m.Delete(repo=>repo.Event_Id == eventDetail.Event_Id));
            DisplayEventDetail_ShouldReturnTheEventAtAllLoc();
            var res = eventDetailBusiness.DeleteEvent(eventDetail.Event_Id, eventLocation.Location_Id);

            //assert
            Assert.AreEqual(res, true);
        }

        [TestMethod]
        public void DisplayBookedEventsList_ShouldReturnTheEvent()
        {

            //Arrange
            bookingDetailDomainModels = DataInitializerBookedEventDetail.GetAllBookedEventDetailDomainModel();
            bookingDetailDomainModel = bookingDetailDomainModels[0];
            bookingDetails = DataInitializer.GetAllBookedEventDetail();
            UserDetailList_ShouldReturnUserDetails();
            EventDetailList_ShouldReturnEvents();

            //Act           
            mockBookingDetailDataHandler.Setup(repo => repo.GetAll()).Returns(bookingDetails);
            var res = eventDetailBusiness.DisplayBookedEventsList();

            //assert
            Assert.AreEqual(bookingDetailDomainModel.Event_Id, res[0].Event_Id);
        }
    }
}

