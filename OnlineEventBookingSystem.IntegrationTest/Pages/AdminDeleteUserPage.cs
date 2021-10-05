using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineEventBookingSystem.IntegrationTest.Pages
{
    public class AdminDeleteUserPage:DriverHelper
    {
       
        public void Click_UsersLink()
        {
            IWebElement lnkUsers = Wait.Until(x => x.FindElement(By.LinkText("Users")));
            lnkUsers.Click();
        }

        public void Click_Delete()
        {
            IWebElement trDelete = Wait.Until(x => x.FindElement(By.Id("1034")));
            var lnkDelete=  trDelete.FindElement(By.LinkText("Delete"));
            lnkDelete.Click();
        }

        public void AlertPopup()
        {    
           Wait.Until(x => x.SwitchTo().Alert()).Accept();   
        }

        public bool CheckIfUserExists()
        {
            var lnkUsers = Wait.Until(x => x.FindElements(By.Id("1034")));
            if(lnkUsers.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //public bool CheckUserExists()
        //{
        //    IWebElement lnkUsers = Wait.Until(x => x.FindElement(By.Id("1047")));
        //    return lnkUsers.Displayed;
        //}


        //String alertMessage = driver.switchTo().alert().getText
    }
}
