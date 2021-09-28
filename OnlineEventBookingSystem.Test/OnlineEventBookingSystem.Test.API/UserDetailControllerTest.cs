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
    /// <summary>
    /// Summary description for UserDetailsControllerTest
    /// </summary>
    [TestClass]
    public class UserDetailsControllerTest
    {
        #region Variables
        private List<UserRegistrationDomainModel> userDetailDomainModels;
        private UserRegistrationDomainModel userDetailDomainModel;
        private List<UserRegistrationModel> userDetails;
        private UserRegistrationModel userDetail;

        #endregion


        [TestMethod]
        public void GetUserDetailsList_ShouldReturnAllUsersDetail()
        {
            //Arrange
            userDetailDomainModels = DataInitializer.GetAllUsers();
            userDetails = DataInitializerAPIUserDetailModels.GetAllUserDetailModel();
            var mockUserBusiness = new Mock<IUserBusiness>();

            //Act
            mockUserBusiness.Setup(x => x.GetAllUsers()).Returns(userDetailDomainModels);
            var controller = new UserDetailsController(mockUserBusiness.Object);
            var result = controller.GetUserDetails() as List<UserRegistrationModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userDetails.Count, result.Count);
        }

        [TestMethod]
        public void GetDetails_ShouldReturnTheUserDetail()
        {
            //Arrange

            userDetailDomainModels = DataInitializer.GetAllUsers();
            userDetailDomainModel = userDetailDomainModels[0];
            userDetails = DataInitializerAPIUserDetailModels.GetAllUserDetailModel();
            userDetail = userDetails[0];
            var mockUserBusiness = new Mock<IUserBusiness>();
            
            //Act
            mockUserBusiness.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(userDetailDomainModel);
            var controller = new UserDetailsController(mockUserBusiness.Object);
            var result = controller.GetDetails(10) as OkNegotiatedContentResult<UserRegistrationModel>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userDetail.User_Name, result.Content.User_Name);
        }

        [TestMethod]
        public void UpdateUserDetail_ShouldUpdateCorrectUser()
        {
            //Arrange

            userDetailDomainModels = DataInitializer.GetAllUsers();
            userDetailDomainModel = userDetailDomainModels[0];
            userDetails = DataInitializerAPIUserDetailModels.GetAllUserDetailModel();
            userDetail = userDetails[0];
            var mockUserBusiness = new Mock<IUserBusiness>();

            //Act
            mockUserBusiness.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(userDetailDomainModel);
            mockUserBusiness.Setup(x => x.UpdateUser(It.IsAny<UserRegistrationDomainModel>())).Returns("Updated");
            var controller = new UserDetailsController(mockUserBusiness.Object);
            var result = controller.UpdateUserDetail(userDetail) as OkNegotiatedContentResult<string>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated", result.Content);
        }

        [TestMethod]
        public void Delete_ShouldDeleteCorrectUser()
        {

            //Arrange
            userDetailDomainModels = DataInitializer.GetAllUsers();
            userDetailDomainModel = userDetailDomainModels[0];
            var mockUserBusiness = new Mock<IUserBusiness>();

            //Act
            mockUserBusiness.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(userDetailDomainModel);
            mockUserBusiness.Setup(x => x.DeleteUser(It.IsAny<int>())).Returns(true);
            var controller = new UserDetailsController(mockUserBusiness.Object);
            var result = controller.Delete(10) as OkNegotiatedContentResult<bool>;
           
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Content);
        }
    }
}