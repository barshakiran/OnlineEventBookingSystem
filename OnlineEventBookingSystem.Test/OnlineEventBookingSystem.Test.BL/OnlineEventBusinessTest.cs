using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OnlineEventBookingSystemBL;
using Moq;
using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDomain;
using OnlineEventBookingSystemDAL.Infrastructure.Contract;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using OnlineEventBookingSystemDAL;

namespace OnlineEventBookingSystem.Tests.BL
{
    [TestClass]
    public class OnlineEventBusinessTest
    {

        [TestMethod]
        public void FindUserTest()
        {
            //Arrange           

            var dbdata = new List<UserRegistrationDomainModel>()
            {
                new UserRegistrationDomainModel {
                    User_Id = 10,
                    User_Password = "test",
                    User_Name = "TestUser",
                    User_Address = "India",
                    User_Email = "testc@123",
                    User_PhoneNo = "1223456789",
                    IsAdmin = false
                },
                 new UserRegistrationDomainModel {
                    User_Id = 11,
                    User_Password = "test1",
                    User_Name = "TestUser1",
                    User_Address = "India",
                    User_Email = "testc@123",
                    User_PhoneNo = "1223456799",
                    IsAdmin = false
                } }.AsQueryable();


            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockRepository = new Mock<IOnlineEventBusiness>();
            var userRepositoryMock = new Mock<IBaseRepository<UserRepository>>();
            //mockRepository.Setup(m => m.FindUser(It.IsAny<int>())).Returns(e => dbdata.Where(x => x.User_Id == i).Single());
            mockUnitOfWork.Setup(x => x.Db.Set<UserRegistrationDomainModel>()).Verifiable();
            //Act

            IOnlineEventBusiness sut = new OnlineEventBusiness(mockUnitOfWork.Object);
            var actual = sut.FindUser(10);
            //Assert           
        }
    }


}
