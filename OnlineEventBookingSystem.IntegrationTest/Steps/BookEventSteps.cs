using NUnit.Framework;
using OnlineEventBookingSystem.IntegrationTest;
using OnlineEventBookingSystem.IntegrationTest.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace OnlineEventBookingSystem.IntegrationTset.Steps
{
    [Binding]
    public class BookEventSteps : DriverHelper
    {

        EventBookingPage eventBookingPage = new EventBookingPage();
        EventPage eventPage = new EventPage();
        LoginPage loginPage = new LoginPage();

        [Given(@"I navigate to the application")]
        public void GivenINavigateToTheApplication()
        {
            Driver.Navigate().GoToUrl("http://localhost:49732/");

        }

        [Given(@"I click the Login link")]
        public void GivenICliclTheLoginLink()
        {
            eventPage.Click_Login();
        }


        [Given(@"I enter username and password")]
        public void GivenIEnterUsernameAndPassword(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            loginPage.Login((string)data.Username, (string)data.Password);
        }

        [Given(@"I click login")]
        public void GivenIClickLogin()
        {
            loginPage.Click_Login();
        }

        [Then(@"I should see user login to the application")]
        public void ThenIShouldSeeUserLoginToTheApplication()
        {
            Assert.That(eventPage.IsLogOffExits(), Is.True, "Log Off link does not exist");
                
        }


        [Then(@"I click the Book link")]
        public void ThenIClickTheBookLink()
        {
            eventPage.LinkBook();
        }

        [Then(@"I enter number of tickets")]
        public void ThenIEnterNumberOfTickets(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            eventBookingPage.GetData((int)data.TicketCount);
        }

        [Then(@"I click on calculate button")]
        public void ThenIClickOnCalculateButton()
        {
           eventBookingPage.Click_Calc();
        }

        [Then(@"I selected the payment mode from the drop down")]
        public void ThenISelectedThePaymentModeFromTheDropDown(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            eventBookingPage.SelectPaymentMode((string)data.PaymentMode);
        }

        [Then(@"I click on the book event button")]
        public void ThenIClickOnTheBookEventButton()
        {
            eventBookingPage.Click_BookEvent();
        }

        [Then(@"Booked event confirmation page will be displayed")]
        public void ThenBookedEventConfirmationPageWillBeDisplayed()
        {
            Assert.That(eventBookingPage.CheckConfirmationPage(), Is.True, "Event already booked or  expired");
           
        }

    }
}
