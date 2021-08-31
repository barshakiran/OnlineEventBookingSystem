using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDomain;
using System.Linq;
using System.Collections.Generic;
using TestHelper;

namespace OnlineEventBookingSystem.Tests.BL
{
    /// <summary>
    /// Summary description for EventDetail BusinessTest
    /// </summary>
    [TestClass]
    public class EventDetailBusinessTest
    {
        #region Variables
        private List<EventDetailDomainModel> _event;
        private EventDetailDomainModel eventDModel;
        #endregion
        public EventDetailBusinessTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [TestMethod]
        public void AddEventDetails_ShouldAddEvent()
        {
            //Arrange
            _event = DataInitializer.GetAllEventDetails();

            //Act
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();
            mockEventDetailBusiness.Setup(m => m.AddEventDetails(It.IsAny<EventDetailDomainModel>())).Returns("Inserted");
            var res = mockEventDetailBusiness.Object.AddEventDetails(_event[0]);

            //Assert
            Assert.AreEqual(res, "Inserted");
        }

        [TestMethod]
        public void AddEventDetails_ShouldReturnNull()
        {
            //Arrange
            eventDModel = new EventDetailDomainModel();

            //Act
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();
            mockEventDetailBusiness.Setup(m => m.AddEventDetails(It.IsAny<EventDetailDomainModel>())).Returns("null");
            var res = mockEventDetailBusiness.Object.AddEventDetails(eventDModel);

            //Assert
            Assert.AreEqual(res, "null");
        }

        [TestMethod]
        public void AddEventDetails_ShouldReturnAllEvents()
        {
            //Arrange
            _event = DataInitializer.GetAllEventDetails();
            //Act
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();
            mockEventDetailBusiness.Setup(m => m.DisplayEventDetails()).Returns(_event);
            var res = mockEventDetailBusiness.Object.DisplayEventDetails();

            //Assert
            Assert.AreEqual(res, _event);
        }

        [TestMethod]
        public void AddEventDetails_ShouldReturnTheEvent()
        {

            //Arrange
            _event = DataInitializer.GetAllEventDetails();

            //Act
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();
            mockEventDetailBusiness.Setup(m => m.DisplayEvent(It.IsAny<int>())).Returns(_event[0]);
            var res = mockEventDetailBusiness.Object.DisplayEvent(101);

            //assert
            Assert.AreEqual(_event[0].Event_Name, res.Event_Name);
        }

        [TestMethod]
        public void AddEventDetails_ShouldUpdateTheEvent()
        {

            //Arrange
            _event = DataInitializer.GetAllEventDetails();

            //Act
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();
            mockEventDetailBusiness.Setup(m => m.UpdateEventDetails(It.IsAny<EventDetailDomainModel>())).Returns("Updated");
            var res = mockEventDetailBusiness.Object.UpdateEventDetails(_event[0]);

            //assert
            Assert.AreEqual("Updated", res);
        }

        [TestMethod]
        public void DeleteTest_ShouldDeleteTheEvent()
        {
            //Arrange

            //Act
            var mockUserBusiness = new Mock<IEventDetailBusiness>();
            mockUserBusiness.Setup(m => m.DeleteEvent(It.IsAny<int>())).Returns(true);

            //assert
            var res = mockUserBusiness.Object.DeleteEvent(101);
        }
    }
}

