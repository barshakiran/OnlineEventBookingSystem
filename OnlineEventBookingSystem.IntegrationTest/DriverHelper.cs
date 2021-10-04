using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace OnlineEventBookingSystem.IntegrationTest
{
    [Binding]
    public class DriverHelper
    {
        public static IWebDriver Driver { get; set; }
        public static WebDriverWait Wait { get; set; }

    }
}
