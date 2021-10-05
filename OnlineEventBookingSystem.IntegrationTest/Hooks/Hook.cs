using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace OnlineEventBookingSystem.IntegrationTest.Hooks
{
    [Binding]
    public sealed class Hook:DriverHelper
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            ChromeOptions option = new ChromeOptions();
            Driver = new ChromeDriver();
            Wait= new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Driver.Quit();
        }
    }
}
