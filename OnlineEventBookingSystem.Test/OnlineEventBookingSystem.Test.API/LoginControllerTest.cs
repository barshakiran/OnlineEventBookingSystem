using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineEventBookingSystemAPI.Models;
using Moq;
using TestHelper;
using OnlineEventBookingSystemAPI.Controllers;
using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDomain;
using System.Web.Http.Results;
using System.Collections.Generic;

namespace OnlineEventBookingSystem.Test.API
{
    /// <summary>
    /// Summary description for LoginControllerTest
    /// </summary>
    [TestClass]
    public class LoginControllerTest
    {
        #region Variables
        private List<UserRegistrationDomainModel> userDetailDomainModels;
        private UserRegistrationDomainModel userDetailDomainModel;
        private List<UserRegistrationModel> userDetails;
        private UserRegistrationModel userDetail;

        #endregion

        [TestMethod]
        public void PostUserDetail_ShouldInsertUserDetail()
        {
            //Arrange

            userDetails = DataInitializerAPIUserDetailModels.GetAllUserDetailModel();
            userDetail = userDetails[0];
            var mockUserBusiness = new Mock<IUserBusiness>();

            //Act
            mockUserBusiness.Setup(x => x.AddUser(It.IsAny<UserRegistrationDomainModel>())).Returns("inserted");
            var controller = new LoginController(mockUserBusiness.Object);
            var result = controller.AddUserDetail(userDetail) as OkNegotiatedContentResult<string>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("inserted", result.Content);
        }

        [TestMethod]
        public void UserLogin_ShouldLoginUSer()
        {
            //Arrange

            userDetails = DataInitializerAPIUserDetailModels.GetAllUserDetailModel();
            userDetail = userDetails[0];
            userDetailDomainModels = DataInitializer.GetAllUsers();
            userDetailDomainModel = userDetailDomainModels[0];
            var mockUserBusiness = new Mock<IUserBusiness>();

            //Act
            mockUserBusiness.Setup(x => x.CheckLogin(It.IsAny<UserRegistrationDomainModel>())).Returns(userDetailDomainModel);
            var controller = new LoginController(mockUserBusiness.Object);
            var result = controller.UserLogin(userDetail);
           
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userDetail.User_Id, result.User_Id);
        }
    }
}