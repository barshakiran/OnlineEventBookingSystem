using NUnit.Framework;
using OnlineEventBookingSystem.IntegrationTest.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace OnlineEventBookingSystem.IntegrationTest.Steps
{
   public  class AdminBookEventSteps:DriverHelper
    {
        AdminEventBookingPage adminLoginPage = new AdminEventBookingPage();

        [Then(@"I should see admin login to the application")]
        public void ThenIShouldSeeAdminLoginToTheApplication()
        {
            Assert.That(adminLoginPage.CheckAdminLogin(), Is.True, "Log Off link does not exist");
        }

        [Then(@"I click Event link")]
        public void ThenIClickEventLink()
        {
            adminLoginPage.Click_BrowseEvent();
        }

    }
}
