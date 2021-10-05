using NUnit.Framework;
using OnlineEventBookingSystem.IntegrationTest.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace OnlineEventBookingSystem.IntegrationTest.Steps
{
    [Binding]
    public class AdminDeleteUserSteps : DriverHelper
    {
        AdminEventBookingPage adminEventBookingPage = new AdminEventBookingPage();
        AdminDeleteUserPage adminDeleteEventPage = new AdminDeleteUserPage();

       
        [Given(@"I should see admin login to the application")]
        public void GivenIShouldSeeAdminLoginToTheApplication()
        {
            Assert.That(adminEventBookingPage.CheckAdminLogin(), Is.True, "Admin is unable to login");
            Thread.Sleep(1000);
        }

        [Given(@"I click User link")]
        public void GivenIClickUserLink()
        {
            adminDeleteEventPage.Click_UsersLink();
            Thread.Sleep(1000);
        }

        [Given(@"I click Delete link")]
        public void GivenIClickDeleteLink()
        {
            adminDeleteEventPage.Click_Delete();
            Thread.Sleep(1000);
        }

        [Given(@"I click OK button on the alert message box")]
        public void GivenIClickOKButtonOnTheAlertMessageBox()
        {
            adminDeleteEventPage.AlertPopup();
            Thread.Sleep(1000);
        }

        [Then(@"The user get deleted from the user list")]
        public void ThenTheUserGetDeletedFromTheUserList()
        {
            Assert.That(adminDeleteEventPage.CheckIfUserExists(), Is.False, "User still exists");
            Thread.Sleep(1000);
        }

        [Then(@"The user exists in the user list")]
        public void ThenTheUserExistsInTheUserList()
        {
            Assert.That(adminDeleteEventPage.CheckIfUserExists(), Is.True, "User deleted from the list");
            Thread.Sleep(1000);
        }

    }
}
