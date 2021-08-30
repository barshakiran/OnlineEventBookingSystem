using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineEventBookingSystemAPI.Models;
using Moq;
using TestHelper;
using OnlineEventBookingSystemAPI.Controllers;
using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDomain;
using System.Web.Http.Results;

namespace OnlineEventBookingSystem.Test.API
{
    /// <summary>
    /// Summary description for LoginControllerTest
    /// </summary>
    [TestClass]
    public class LoginControllerTest
    {
        #region Variables
        private MapperConfiguration configuration;
        private Mapper mapper;
        #endregion

        [TestMethod]
        public void PostUserDetail_ShouldInsertUserDetail()
        {
            UserRegistrationModel user = new UserRegistrationModel();
            configuration = new MapperConfiguration(x => x.CreateMap<UserRegistrationDomainModel, UserRegistrationModel>().ReverseMap());
            mapper = new Mapper(configuration);
            var testUsers = DataInitializer.GetAllUsers();
            var mockUserBusiness = new Mock<IUserBusiness>();
            //smockUserBusiness.Setup(x => x.WhereUser(It.IsAny<string>())).Returns(newUser);
            mockUserBusiness.Setup(x => x.AddUser(It.IsAny<UserRegistrationDomainModel>())).Returns("inserted");
            var controller = new LoginController(mockUserBusiness.Object);
            mapper.Map(testUsers[0], user);
            var result = controller.PostUserDetail(user) as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(result);
            Assert.AreEqual("inserted", result.Content);
        }

        [TestMethod]
        public void UserLogin_ShouldLoginUSer()
        {
            UserLoginModel userLoginModel = new UserLoginModel();
            var testUser = DataInitializer.GetUserLoginData();
            var mockUserBusiness = new Mock<IUserBusiness>();           
            mockUserBusiness.Setup(x => x.CheckLogin(It.IsAny<UserLoginDomainModel>())).Returns(testUser);
            var controller = new LoginController(mockUserBusiness.Object);
            configuration = new MapperConfiguration(x => x.CreateMap<UserLoginDomainModel, UserLoginModel>().ReverseMap());
            mapper = new Mapper(configuration);
            mapper.Map(testUser, userLoginModel);
            var result = controller.UserLogin(userLoginModel);
            Assert.IsNotNull(result);
            Assert.AreEqual(userLoginModel.User_Id, result.User_Id);
        }
    }
}