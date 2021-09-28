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
    /// Summary description for UserBusinessTest
    /// </summary>
    [TestClass]
    public class UserBusinessTest
    {
        #region Variables
        private UserRegistrationDomainModel userDetailDomainModel;
        private List<UserDetail> userDetails;
        private UserDetail userDetail;
        private Mock<IUserDataHandler> mockUserDataHandler;
        private UserBusiness userBusiness;

        #endregion
        public UserBusinessTest()
        {
            mockUserDataHandler = new Mock<IUserDataHandler>();
            userBusiness = new UserBusiness(mockUserDataHandler.Object);
        }
       [TestMethod]
        public void DisplayAllUserDetail_ShouldReturnAllUserDetail()
        {
            //Arrange
            userDetails = DataInitializer.GetAllDbUsers();
            userDetail = userDetails[0];

            //Act
            mockUserDataHandler.Setup(m => m.GetAll()).Returns(userDetails);
            var res = userBusiness.GetAllUsers();

            //Assert
            Assert.AreEqual(userDetail.User_Id, res[0].User_Id);
        }
       
        [TestMethod]
        public void AddUserDetails_ShouldAddUser()
        {
            //Arrange
            userDetailDomainModel = DataInitializer.GetUserRegistrationData();

            //Act
            mockUserDataHandler.Setup(m => m.Insert(userDetail));         
            var res = userBusiness.AddUser(userDetailDomainModel);

            //assert
            Assert.AreEqual(res, "Inserted");
        }

        [TestMethod]
        public void DisplayUserDetailByName_ShouldReturnUserName()
        {
            //Arrange
            userDetailDomainModel = DataInitializer.GetUserRegistrationData();
            userDetails = DataInitializer.GetAllDbUsers();
            userDetail = userDetails[0];
            string username = userDetail.User_Name;

            //Act
            mockUserDataHandler.Setup(m => m.SingleOrDefault(s => s.User_Name == username)).Returns(userDetail);
            var res = userBusiness.GetUserByName(userDetailDomainModel.User_Name);

            //assert
            Assert.AreEqual(userDetailDomainModel.User_Id, res.User_Id);
        }

        [TestMethod]
        public void UpdateUserDetails_ShouldUpdateTheUser()
        {
            //Arrange
            userDetailDomainModel = DataInitializer.GetUserRegistrationData();
            userDetails = DataInitializer.GetAllDbUsers();
            userDetail = userDetails[0];

            //Act
            mockUserDataHandler.Setup(m => m.Update(userDetail));
            var res = userBusiness.UpdateUser(userDetailDomainModel);

            //assert
            Assert.AreEqual("Updated", res);
        }

        [TestMethod]
        public void CheckLoginForUser_ShouldReturnTrueForValidUser()
        {
            //Arrange
            userDetailDomainModel = DataInitializer.GetUserRegistrationData();
            userDetails = DataInitializer.GetAllDbUsers();
            userDetail = userDetails[0];
            string username = userDetail.User_Name;
            string password = userDetail.User_Password;

            //Act
            mockUserDataHandler.Setup(m => m.SingleOrDefault(s => s.User_Name == username & s.User_Password == password)).Returns(userDetail);
            var res = userBusiness.CheckLogin(userDetailDomainModel);

            //assert
            Assert.AreEqual(userDetailDomainModel.User_Password, res.User_Password);
        }

        [TestMethod]
        public void DeleteUser_ShouldDeleteTheUser()
        {
            //Arrange
            userDetailDomainModel = DataInitializer.GetUserRegistrationData();
            userDetails = DataInitializer.GetAllDbUsers();
            userDetail = userDetails[0];
            int userId = userDetail.User_Id;

            //Act
            mockUserDataHandler.Setup(m => m.Delete(s => s.User_Id == userId));
            var res = userBusiness.DeleteUser(userDetailDomainModel.User_Id);

            //assert
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void DisplayUserDetailById_ShouldReturnUserId()
        {
            //Arrange
            userDetailDomainModel = DataInitializer.GetUserRegistrationData();
            userDetails = DataInitializer.GetAllDbUsers();
            userDetail = userDetails[0];
            int userId = userDetail.User_Id;

            //Act
            mockUserDataHandler.Setup(m => m.SingleOrDefault(s => s.User_Id == userId)).Returns(userDetail);
            var res = userBusiness.GetUserById(userDetailDomainModel.User_Id);

            //assert
            Assert.AreEqual(userDetailDomainModel.User_Name, res.User_Name);
        }
    }
}
