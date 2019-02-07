using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace PcmiTestSolutionBarnas.Helpers
{
    public class WaitHelper
    {
        public static void waitForElement(By byLocator, int timeInSeconds)
        {
            var wait = new WebDriverWait(WebDriverHelper.Driver, TimeSpan.FromSeconds(timeInSeconds));
            wait.Until(Driver => Driver.FindElement(byLocator));
        }
    }
}
