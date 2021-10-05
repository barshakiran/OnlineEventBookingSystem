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
    public class AdminEventBookingPage:DriverHelper
    {
        public bool CheckAdminLogin()
        {
            var lnkAdminLogin = Wait.Until(x => x.FindElement(By.LinkText("Events")));
            return lnkAdminLogin.Displayed;
        }

        public void Click_BrowseEvent()
        {
            IWebElement lnkBrowseEvent = Wait.Until(x => x.FindElement(By.LinkText("Browse Events")));
            lnkBrowseEvent.Click();
        }
    }
}
