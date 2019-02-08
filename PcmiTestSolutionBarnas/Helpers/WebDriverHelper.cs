using OpenQA.Selenium;
using System;
using PcmiTestSolutionBarnas.Pages;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PcmiTestSolutionBarnas.Helpers
{
    public class WebDriverHelper
    {
        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver != null)
                {
                    return _driver;
                }
                else
                {
                    _driver = new ChromeDriver();
                    return _driver;
                }
            }
        }

        public static void GoToNewlyOpenedTab(string expectedTitle)
        {
            ReadOnlyCollection<String> windowHandles = Driver.WindowHandles;
            String firstTab = windowHandles[0];
            String lastTab = windowHandles[windowHandles.Count - 1];
            Driver.SwitchTo().Window(lastTab);
            string title = Driver.Title;
            Assert.AreEqual(expectedTitle, title);            
        }

        public static LoginPage GoToLoginPage()
        {
            string loginPageUrl = "https://qa-alpha-auto.pcrsdev.com";
            Driver.Navigate().GoToUrl(loginPageUrl);
            return new LoginPage();
        }

        public static void Close()
        {
            Driver.Close();
        }
    }
}
