using OnlineEventBookingSystemDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHelper
{
    public class DataInitializer
    {
        public static UserLoginDomainModel GetUserLoginData()
        { 
            var  loginData = new UserLoginDomainModel()
                             { User_Id = 11, User_Password = "test1", User_Name = "TestUser1" };

            return loginData;
        }

        public static UserRegistrationDomainModel GetUserRegistrationData()
        {
            var regData = new UserRegistrationDomainModel()
            {
                User_Id = 10,
                User_Password = "test",
                User_Name = "TestUser",
                User_Address = "India",
                User_Email = "testc@123",
                User_PhoneNo = "1223456789",
                IsAdmin = false
            };

            return regData;
        }

        public static List<UserRegistrationDomainModel> GetAllUsers()
        {
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
                } 
            };
            return dbdata;
        }
    }
}
