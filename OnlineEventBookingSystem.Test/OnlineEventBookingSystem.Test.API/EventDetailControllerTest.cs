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
        private List<EventDetailDomainModel> eventDetailDomainModels;
        private List<EventDetailModel> eventDetails;
        private EventDetailDomainModel eventDetailDomainModel;
        private EventDetailModel eventDetail;
        
        #endregion

        [TestMethod]
        public void GetEventDetailList_ShouldReturnAllEventDetails()
        {
            //Arrange

            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();
            mockEventDetailBusiness.Setup(x => x.DisplayEventDetailList()).Returns(eventDetailDomainModels);

            //Act
            var controller = new EventDetailsController(mockEventDetailBusiness.Object);
            var result = controller.GetEventDetailList() as List<EventDetailModel>;

            //Assert
            Assert.AreEqual(eventDetailDomainModels.Count, result.Count);
        }

        [TestMethod]
        public void GetEventDetail_ShouldDisplayTheEventDetails()
        {
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetailDomainModel = eventDetailDomainModels[0];
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();
            mockEventDetailBusiness.Setup(x => x.DisplayEventDetail(It.IsAny<int>(), It.IsAny<int>())).Returns(eventDetailDomainModel);
            var controller = new EventDetailsController(mockEventDetailBusiness.Object);
            var result = controller.GetEventDetail(101,10) as OkNegotiatedContentResult<EventDetailModel>;
            Assert.IsNotNull(result);
            Assert.AreEqual(eventDetailDomainModel.Event_Name, result.Content.Event_Name);
        }

        [TestMethod]
        public void UpdateEventDetail_ShouldUpdateTheEvent()
        {
            //Arrange

            eventDetails = DataInitializerAPIEventDetailModels.GetAllEventDetailModel();
            eventDetail = eventDetails[0];
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetailDomainModel = eventDetailDomainModels[0];
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();

            //Act
            mockEventDetailBusiness.Setup(x => x.DisplayEventDetail(It.IsAny<int>(), It.IsAny<int>())).Returns(eventDetailDomainModel);
            mockEventDetailBusiness.Setup(x => x.UpdateEventDetails(It.IsAny<EventDetailDomainModel>())).Returns("Updated");
            var controller = new EventDetailsController(mockEventDetailBusiness.Object);
            var result = controller.UpdateEventDetail(eventDetail) as OkNegotiatedContentResult<string>;
           
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated", result.Content);
        }

        [TestMethod]
        public void Delete_ShouldDeleteTheEvent()
        {
            //Arrange

            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetailDomainModel = eventDetailDomainModels[0];
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();

            //Act
            mockEventDetailBusiness.Setup(x => x.DisplayEventDetail(It.IsAny<int>(), It.IsAny<int>())).Returns(eventDetailDomainModel);
            mockEventDetailBusiness.Setup(x => x.DeleteEvent(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            var controller = new EventDetailsController(mockEventDetailBusiness.Object);
            var result = controller.Delete(10,101) as OkNegotiatedContentResult<bool>;
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Content);
        }

        [TestMethod]
        public void AddEventDetail_ShouldAddTheEvent()
        {
            //Arrange

            eventDetails = DataInitializerAPIEventDetailModels.GetAllEventDetailModel();
            eventDetail = eventDetails[0];
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            eventDetailDomainModel = eventDetailDomainModels[0];
            var mockEventDetailBusiness = new Mock<IEventDetailBusiness>();

            //Act
            mockEventDetailBusiness.Setup(x => x.AddEventDetails(It.IsAny<EventDetailDomainModel>())).Returns("Inserted");
            var controller = new EventDetailsController(mockEventDetailBusiness.Object);
            var result = controller.AddEventDetail(eventDetail) as OkNegotiatedContentResult<string>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Inserted", result.Content);
        }
    }
}