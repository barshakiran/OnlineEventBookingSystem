using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        private MapperConfiguration configuration;
        private Mapper mapper;
        #endregion

        public UserDetailsControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetUserDetails_ShouldReturnAllUsers()
        {
            var testUsers = DataInitializer.GetAllUsers();
            var mockUserBusiness = new Mock<IUserBusiness>();
            mockUserBusiness.Setup(x => x.GetAllUsers()).Returns(testUsers);
            var controller = new UserDetailsController(mockUserBusiness.Object);
            var result = controller.GetUserDetails() as List<UserRegistrationModel>;
            Assert.AreEqual(testUsers.Count, result.Count);
        }

        [TestMethod]
        public void GetUser_ShouldReturnCorrectUser()
        {
            var testUsers = DataInitializer.GetAllUsers();
            var mockUserBusiness = new Mock<IUserBusiness>();
            mockUserBusiness.Setup(x => x.FindUser(It.IsAny<int>())).Returns(testUsers[0]);
            var controller = new UserDetailsController(mockUserBusiness.Object);
            var result = controller.GetDetails(10) as OkNegotiatedContentResult<UserRegistrationModel>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testUsers[0].User_Name, result.Content.User_Name);
        }

        [TestMethod]
        public void UpdateUserDetail_ShouldUpdateCorrectUser()
        {
            UserRegistrationModel user = new UserRegistrationModel();
            configuration = new MapperConfiguration(x => x.CreateMap<UserRegistrationDomainModel, UserRegistrationModel>().ReverseMap());
            mapper = new Mapper(configuration);
            var testUsers = DataInitializer.GetAllUsers();
            var mockUserBusiness = new Mock<IUserBusiness>();
            mockUserBusiness.Setup(x => x.FindUser(It.IsAny<int>())).Returns(testUsers[0]);
            mockUserBusiness.Setup(x => x.UpdateUser(It.IsAny<UserRegistrationDomainModel>())).Returns("Updated");
            var controller = new UserDetailsController(mockUserBusiness.Object);
            mapper.Map(testUsers[0], user);
            var result = controller.UpdateUserDetail(user) as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated", result.Content);
        }

        [TestMethod]
        public void Delete_ShouldDeleteCorrectUser()
        {
            var testUsers = DataInitializer.GetAllUsers();
            var mockUserBusiness = new Mock<IUserBusiness>();
            mockUserBusiness.Setup(x => x.FindUser(It.IsAny<int>())).Returns(testUsers[0]);
            mockUserBusiness.Setup(x => x.DeleteUser(It.IsAny<int>())).Returns(true);
            var controller = new UserDetailsController(mockUserBusiness.Object);           
            var result = controller.Delete(10) as OkNegotiatedContentResult<bool>;
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Content);
        }
    }
}