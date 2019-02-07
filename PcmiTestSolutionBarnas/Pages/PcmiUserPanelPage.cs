using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using PcmiTestSolutionBarnas.Helpers;
using System.Threading;

namespace PcmiTestSolutionBarnas.Pages
{
    public class PcmiUserPanelPage
    {
        public IWebDriver Driver { get; set; }
        Actions action = new Actions(WebDriverHelper.Driver);

        private readonly string expectedTitle = "Policy Claim and Reporting Solutions";
        private readonly string element = "//*[@id='divDownArrowUser']";
        private readonly string addContractLink = "//table[@id='navMenu_ITC0i1_ctl00_1_tbMenu_1']//a[@title='Add Contract']";
        private readonly string logoutButton = "//*[@id='ucTopBar_ASPxRoundPanel2_ucLogin_cbLogin_pnl_btnLogout_CD']/span";

        public PcmiUserPanelPage ()
        {
            Driver = WebDriverHelper.Driver;
        }

        public PcmiUserPanelPage VerifyPcmiUserPanelPageIsOpened()
        {
            Thread.Sleep(5000);
            string title = Driver.Title;
            Assert.AreEqual(expectedTitle, title);
            return this;
        }

        public AddContractPage ClickOnAddContractLink()
        {
            WaitHelper.waitForElement(By.XPath(addContractLink), 10);
            Driver.FindElement(By.XPath(addContractLink)).Click();       
            return new AddContractPage();
        }

        public LoginPage LogoutFromPcmiUserPanelPage()
        {
            WaitHelper.waitForElement(By.XPath(element), 5);
            action.MoveToElement(Driver.FindElement(By.XPath(element))).Perform();
            WaitHelper.waitForElement(By.XPath(logoutButton), 5);
            Driver.FindElement(By.XPath(logoutButton)).Click();

            return new LoginPage();
        }
    }
}
