using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace OnlineEventBookingSystem.IntegrationTest.Pages
{
    [Binding]
    public class UserBookEventPage:DriverHelper
    {           
        public IWebElement txtNoOfTickets => Driver.FindElement(By.Name("Booking_TicketCount"));
        public void Click_Calc()
        {
            
            IWebElement lnkCalculate = Wait.Until(x => x.FindElement(By.Id("btnCalculate")));
            lnkCalculate.Click();
        }

        public void Click_BookEvent()
        {
            IWebElement lnkBookEvent = Wait.Until(x => x.FindElement(By.Id("btnBookEvent")));
            lnkBookEvent.Click();
        }

        public void GetData(int ticketCount)
        {
            txtNoOfTickets.Clear();
            txtNoOfTickets.SendKeys(ticketCount.ToString());        
        }

        public void SelectPaymentMode(string paymentMode)
        {
            Wait.Until(x => x.FindElement(By.Id("Payment_Mode"))).SendKeys(paymentMode);           
        }
        public bool CheckConfirmationPage()
        {
               var txtBookingNo = Wait.Until(x => x.FindElement(By.Id("BookingDetails")));
               return txtBookingNo.Displayed;
        }

        public bool CheckUserCannotBookEventPage()
        {
           
            var txtErrorMessage = Driver.Title.Contains("IIS 10.0 Detailed Error - 500.0 - The Event already booked or event date is expired");
            if (txtErrorMessage)
                return true;
            else
                return false;
        }

    }
}
    
   


