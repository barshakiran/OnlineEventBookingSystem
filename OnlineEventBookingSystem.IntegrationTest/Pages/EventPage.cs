using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace OnlineEventBookingSystem.IntegrationTest.Pages
{
    [Binding]
    public class EventPage:DriverHelper
    {
        //UI elements
        public IWebElement lnkLogOff => Driver.FindElement(By.LinkText("LogOut"));
        public bool IsLogOffExits() => lnkLogOff.Displayed;

        public void Click_LoginLink()
        {
            IWebElement lnkLogin = Wait.Until(x => x.FindElement(By.LinkText("Login")));
            lnkLogin.Click();
        }
        public void LinkBook()
        {
            IWebElement lnkBook = Wait.Until(x => x.FindElement(By.Id("3075102")));
            lnkBook.Click();
        }
    }


}
