using NUnit.Framework;
using OnlineEventBookingSystem.IntegrationTest;
using OnlineEventBookingSystem.IntegrationTest.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace OnlineEventBookingSystem.IntegrationTset.Steps
{
    [Binding]
    public class UserBookEventSteps : DriverHelper
    {

        UserBookEventPage eventBookingPage = new UserBookEventPage();
        EventPage eventPage = new EventPage();
        LoginPage loginPage = new LoginPage();

        [Given(@"I navigate to the application")]
        public void GivenINavigateToTheApplication()
        {
            Driver.Navigate().GoToUrl("http://localhost:49732/");
            Thread.Sleep(1000);

        }

        [Given(@"I click the Login link")]
        public void GivenICliclTheLoginLink()
        {
            eventPage.Click_LoginLink();
            Thread.Sleep(1000);
        }


        [Given(@"I enter username and password")]
        public void GivenIEnterUsernameAndPassword(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            loginPage.Login((string)data.Username, (string)data.Password);
            Thread.Sleep(1000);
        }

        [Given(@"I click login")]
        public void GivenIClickLogin()
        {
            loginPage.Click_LoginButton();
            Thread.Sleep(1000);
        }

        [Then(@"I should see user login to the application")]
        public void ThenIShouldSeeUserLoginToTheApplication()
        {
            Assert.That(eventPage.IsLogOffExits(), Is.True, "Log Off link does not exist");
            Thread.Sleep(1000);
        }


        [Then(@"I click the Book link")]
        public void ThenIClickTheBookLink()
        {
            eventPage.LinkBook();
            Thread.Sleep(1000);
        }

        [Then(@"I enter number of tickets")]
        public void ThenIEnterNumberOfTickets(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            eventBookingPage.GetData((int)data.TicketCount);
            Thread.Sleep(1000);
        }

        [Then(@"I click on calculate button")]
        public void ThenIClickOnCalculateButton()
        {
           eventBookingPage.Click_Calc();
            Thread.Sleep(1000);
        }

        [Then(@"I selected the payment mode from the drop down")]
        public void ThenISelectedThePaymentModeFromTheDropDown(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            eventBookingPage.SelectPaymentMode((string)data.PaymentMode);
            Thread.Sleep(1000);
        }

        [Then(@"I click on the book event button")]
        public void ThenIClickOnTheBookEventButton()
        {
            eventBookingPage.Click_BookEvent();
            Thread.Sleep(1000);
        }

        [Then(@"Booked event confirmation page will be displayed")]
        public void ThenBookedEventConfirmationPageWillBeDisplayed()
        {
            Assert.That(eventBookingPage.CheckConfirmationPage(), Is.True, "Event already booked or  expired");
            Thread.Sleep(1000);
        }

        [Then(@"User cannot book event page will be displayed")]
        public void ThenUserCannotBookEventPageWillBeDisplayed()
        {
            Assert.That(eventBookingPage.CheckUserCannotBookEventPage(), Is.True, "Event is booked");
            Thread.Sleep(1000);
        }
    }
}
