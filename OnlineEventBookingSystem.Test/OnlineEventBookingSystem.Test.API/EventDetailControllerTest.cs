using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using TestHelper;
using OnlineEventBookingSystemAPI.Controllers;
using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDomain;
using OnlineEventBookingSystemAPI.Models;
using System.Web.Http.Results;
using AutoMapper;
namespace OnlineEventBookingSystem.Test.API
{
    [TestClass]
    public class EventDetailControllerTest
    {
        #region Variables
        private MapperConfiguration configuration;
        private Mapper mapper;
        #endregion

        [TestMethod]
        public void GetEventDetails_ShouldReturnAllEvents()
        {
            var testUsers = DataInitializer.GetAllEventDetails();
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();
            mockEventDetailBusiness.Setup(x => x.DisplayEventDetails()).Returns(testUsers);
            var controller = new EventDetailsController(mockEventDetailBusiness.Object);
            var result = controller.GetEventDetails() as List<EventDetailModel>;
            Assert.AreEqual(testUsers.Count, result.Count);
        }

        [TestMethod]
        public void GetEventDetail_ShouldReturnCorrectEvent()
        {
            var testUsers = DataInitializer.GetAllEventDetails();
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();
            mockEventDetailBusiness.Setup(x => x.DisplayEvent(It.IsAny<int>())).Returns(testUsers[0]);
            var controller = new EventDetailsController(mockEventDetailBusiness.Object);
            var result = controller.GetEventDetail(101) as OkNegotiatedContentResult<EventDetailModel>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testUsers[0].Event_Name, result.Content.Event_Name);
        }

        [TestMethod]
        public void UpdateEventDetail_ShouldUpdateCorrectEvent()
        {
            var user = new EventDetailModel();
            configuration = new MapperConfiguration(x => x.CreateMap<EventDetailDomainModel, EventDetailModel>().ReverseMap());
            mapper = new Mapper(configuration);
            var testUsers = DataInitializer.GetAllEventDetails();
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();
            mockEventDetailBusiness.Setup(x => x.DisplayEvent(It.IsAny<int>())).Returns(testUsers[0]);
            mockEventDetailBusiness.Setup(x => x.UpdateEventDetails(It.IsAny<EventDetailDomainModel>())).Returns("Updated");
            var controller = new EventDetailsController(mockEventDetailBusiness.Object);
            mapper.Map(testUsers[0], user);
            var result = controller.UpdateEventDetail(user) as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated", result.Content);
        }

        [TestMethod]
        public void Delete_ShouldDeleteCorrectEvent()
        {
            var testUsers = DataInitializer.GetAllEventDetails();
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();
            mockEventDetailBusiness.Setup(x => x.DisplayEvent(It.IsAny<int>())).Returns(testUsers[0]);
            mockEventDetailBusiness.Setup(x => x.DeleteEvent(It.IsAny<int>())).Returns(true);
            var controller = new EventDetailsController(mockEventDetailBusiness.Object);
            var result = controller.Delete(10) as OkNegotiatedContentResult<bool>;
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Content);
        }
    }
}