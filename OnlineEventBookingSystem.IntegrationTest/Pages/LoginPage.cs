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
    public class LoginPage:DriverHelper
    {

        public IWebElement txtUserName => Driver.FindElement(By.Name("User_Name"));
        public IWebElement txtPassword => Driver.FindElement(By.Name("User_Password"));
        public IWebElement btnLogin => Driver.FindElement(By.Id("btnLogin"));
        public IWebElement lnkEventDetails => Driver.FindElement(By.LinkText("EventDetails"));
        public void Login(string username, string password)
        {
            txtUserName.SendKeys(username);
            txtPassword.SendKeys(password);
        }
        public void Click_LoginButton() => btnLogin.Click();
    }
}
