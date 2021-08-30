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
    /// Summary description for UserBusinessTest
    /// </summary>
    [TestClass]
    public class UserBusinessTest
    {
        #region Variables
        private List<UserRegistrationDomainModel> _users;
        private UserRegistrationDomainModel userDModel;
        private UserLoginDomainModel userDLoginModel;
        #endregion

        [TestMethod]
        public void FindUserTest()
        {
            //Arrange
            _users = DataInitializer.GetAllUsers();
            userDModel = _users.SingleOrDefault(s => s.User_Id == 10);     
            
            //Act
            var mockUserBusiness = new Mock<IUserBusiness>();
            mockUserBusiness.Setup(m => m.FindUser(It.IsAny<int>())).Returns(userDModel);
            var res = mockUserBusiness.Object.FindUser(10);

            //assert
            Assert.AreEqual(userDModel.User_Name, res.User_Name);
        }

        [TestMethod]
        public void AddUserTest()
        {
            //Arrange
            userDModel = DataInitializer.GetUserRegistrationData();

            //Act
            var mockUserBusiness = new Mock<IUserBusiness>();
            mockUserBusiness.Setup(m => m.AddUser(It.IsAny<UserRegistrationDomainModel>())).Returns("Inserted");
            var res = mockUserBusiness.Object.AddUser(userDModel);

            //assert
            Assert.AreEqual(res, "Inserted");
        }

        [TestMethod]
        public void WhereUserTest()
        {
            //Arrange
            userDModel = DataInitializer.GetUserRegistrationData();

            //Act
            var mockUserBusiness = new Mock<IUserBusiness>();
            mockUserBusiness.Setup(m => m.WhereUser(It.IsAny<string>())).Returns(userDModel);
            var res = mockUserBusiness.Object.WhereUser("TestUser1");

            //assert
            Assert.AreEqual(userDModel.User_Id, res.User_Id);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            //Arrange
            userDModel = DataInitializer.GetUserRegistrationData();

            //Act
            var mockUserBusiness = new Mock<IUserBusiness>();
            mockUserBusiness.Setup(m => m.UpdateUser(It.IsAny<UserRegistrationDomainModel>())).Returns("Updated");
            var res = mockUserBusiness.Object.UpdateUser(userDModel);

            //assert
            Assert.AreEqual(res, "Updated");
        }

        [TestMethod]
        public void CheckLoginTest()
        {
            //Arrange
            userDLoginModel = DataInitializer.GetUserLoginData();

            //Act
            var mockUserBusiness = new Mock<IUserBusiness>();
            mockUserBusiness.Setup(m => m.CheckLogin(It.IsAny<UserLoginDomainModel>())).Returns(userDLoginModel);

            //assert
            var res = mockUserBusiness.Object.CheckLogin(userDLoginModel);
        }

        [TestMethod]
        public void DeleteTest()
        {
            //Arrange

            //Act
            var mockUserBusiness = new Mock<IUserBusiness>();
            mockUserBusiness.Setup(m => m.DeleteUser(It.IsAny<int>())).Returns(true);

            //assert
            var res = mockUserBusiness.Object.DeleteUser(10);
        }
    }
}
