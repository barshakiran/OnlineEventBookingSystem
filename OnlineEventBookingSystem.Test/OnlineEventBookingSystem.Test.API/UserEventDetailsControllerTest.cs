using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineEventBookingSystemAPI.Controllers;
using OnlineEventBookingSystemAPI.Models;
using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDomain;
using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using TestHelper;

namespace OnlineEventBookingSystem.Test.API
{
    [TestClass]
    public class UserEventDetailsControllerTest
    {
        #region Variables
        private List<EventDetailDomainModel> eventDetailDomainModels;

        private List<UserEventDetailsModel> userEventDetailModels;
        private UserEventDetailsModel userEventDetailModel;
        private List<BookingDetailModel> bookingDetailModels;
        private BookingDetailModel bookingDetailModel ;
        private List<UserEventDetailsDomainModel> userEventDetailsDomainModels;
        private UserEventDetailsDomainModel userEventDetailsDomainModel;
        private List<BookingDetailDomainModel> bookingDetailDomainModels;
        private BookingDetailDomainModel bookingDetailDomainModel;
        #endregion

        [TestMethod]
        public void GetUserEventsDetailList_ShouldReturnAllUserEventDetails()
        {
            //Arrange
            eventDetailDomainModels = DataInitializerEventDetail.GetAllEventDetailDomainModel();
            userEventDetailModels = DataInitializerAPIUserEventDetailModels.GetAllUserEventBookingDetailModel();
            userEventDetailModel = userEventDetailModels[0];
            var mockUserEventDetailBusiness = new Mock<IUserEventDetailsBusiness>();

            //Act

            mockUserEventDetailBusiness.Setup(x => x.DisplayAllUserEvent(It.IsAny<string>(), It.IsAny<string>())).Returns(eventDetailDomainModels);
            var controller = new UserEventDetailsController(mockUserEventDetailBusiness.Object);
            var result = controller.GetUserEventsDetailList(userEventDetailModel) as List<EventDetailModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(eventDetailDomainModels.Count, result.Count);
        }

        [TestMethod]
        public void GetUserBookedEventsDetailList_ShouldReturnTheBookedUserEventDetails()
        {
            //Arrange

            bookingDetailDomainModels = DataInitializerBookedEventDetail.GetAllBookedEventDetailDomainModel();
            bookingDetailModels = DataInitializerAPIBookingDetailModels.GetAllBookedEventDetailModel();
            var mockUserEventDetailBusiness = new Mock<IUserEventDetailsBusiness>();

            //Act
            mockUserEventDetailBusiness.Setup(x => x.DisplayUserBookedEventsList(It.IsAny<string>())).Returns(bookingDetailDomainModels);
            var controller = new UserEventDetailsController(mockUserEventDetailBusiness.Object);
            var result = controller.GetUserBookedEventsDetailList("TestUSer") as List<BookingDetailModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(bookingDetailModels.Count, result.Count);
        }

        [TestMethod]
        public void GetUserBookingEventDetail_ShouldReturnBookingEventDetailsOfTheUser()
        {
            //Arrange
            userEventDetailModels = DataInitializerAPIUserEventDetailModels.GetAllUserEventBookingDetailModel();
            userEventDetailModel = userEventDetailModels[0];
            userEventDetailsDomainModels = DataInitializerUserEventDetails.GetAllUserEventDetailDomainModel();
            userEventDetailsDomainModel = userEventDetailsDomainModels[0];
            var mockUserEventDetailBusiness = new Mock<IUserEventDetailsBusiness>();

            //Act
            mockUserEventDetailBusiness.Setup(x => x.DisplayUserBookingEventDetails(It.IsAny<int>(), It.IsAny<int>())).Returns(userEventDetailsDomainModel);
            var controller = new UserEventDetailsController(mockUserEventDetailBusiness.Object);
            var result = controller.GetUserBookingEventDetail(101, 10) as OkNegotiatedContentResult<UserEventDetailsModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userEventDetailModel.Event_Id, result.Content.Event_Id);
        }

        [TestMethod]
        public void GetUserBookedEventDetail_ShouldReturnBookedEventDetailsOfTheUser()
        {
            //Arrange

            bookingDetailDomainModels = DataInitializerBookedEventDetail.GetAllBookedEventDetailDomainModel();
            bookingDetailModels = DataInitializerAPIBookingDetailModels.GetAllBookedEventDetailModel();
            bookingDetailModel = bookingDetailModels[0];
            bookingDetailDomainModels = DataInitializerBookedEventDetail.GetAllBookedEventDetailDomainModel();
            bookingDetailDomainModel = bookingDetailDomainModels[0];
            var mockUserEventDetailBusiness = new Mock<IUserEventDetailsBusiness>();

            //Act
            mockUserEventDetailBusiness.Setup(x => x.DisplayUserBookedEventDetails(It.IsAny<int>())).Returns(bookingDetailDomainModel);
            var controller = new UserEventDetailsController(mockUserEventDetailBusiness.Object);
            var result = controller.GetUserBookedEventDetail(101) as OkNegotiatedContentResult<BookingDetailModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(bookingDetailModel.Booking_Id, result.Content.Booking_Id);
        }

        [TestMethod]
        public void AddUserBookingEventDetail_ShouldAddBookingEventDetailsOfTheUser()
        {
            //Arrange
            bookingDetailModels = DataInitializerAPIBookingDetailModels.GetAllBookedEventDetailModel();
            bookingDetailModel = bookingDetailModels[0];
            bookingDetailDomainModels = DataInitializerBookedEventDetail.GetAllBookedEventDetailDomainModel();
            bookingDetailDomainModel = bookingDetailDomainModels[0];
            var mockUserEventDetailBusiness = new Mock<IUserEventDetailsBusiness>();

            //Act
            mockUserEventDetailBusiness.Setup(x => x.AddUserBookingEventDetails(It.IsAny<BookingDetailDomainModel>())).Returns(bookingDetailDomainModel.Booking_Id);
            var controller = new UserEventDetailsController(mockUserEventDetailBusiness.Object);
            var result = controller.AddUserBookingEventDetail(bookingDetailModel) as OkNegotiatedContentResult<int>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(bookingDetailModel.Booking_Id, result.Content);
        }


        [TestMethod]
        public void Delete_ShouldDeleteBookedEventDetailsOfTheUser()
        {
            //Arrange

            bookingDetailModels = DataInitializerAPIBookingDetailModels.GetAllBookedEventDetailModel();
            bookingDetailModel = bookingDetailModels[0];
            var mockUserEventDetailBusiness = new Mock<IUserEventDetailsBusiness>();
            mockUserEventDetailBusiness.Setup(x => x.DeleteBookedEvent(It.IsAny<int>())).Returns(true);

            //Act
            var controller = new UserEventDetailsController(mockUserEventDetailBusiness.Object);
            var result = controller.Delete(bookingDetailModel.Booking_Id) as OkNegotiatedContentResult<bool>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Content);
        }
    }
}
