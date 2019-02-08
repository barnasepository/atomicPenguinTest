using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PcmiTestSolutionBarnas.Helpers;

namespace PcmiTestSolutionBarnas.Pages
{
    public class LoginPage
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public IWebDriver Driver { get; set; }

        private readonly string alertMessageXpath = "//label[contains(@id, 'dialogText')] [contains(text(),'Password is required.')]";
        private readonly string loginButtonXpath = "//*[@id='ucTopBar_ASPxRoundPanel2_ucLogin_cbLogin_pnl_btnLoginNew_CD']/span";
        private readonly string closeButtonXpath = "//span[contains(@class,'ui-button-text')] [contains(text(),'OK')]";
        private readonly string userLocator = "ucTopBar_ASPxRoundPanel2_ucLogin_cbLogin_pnl_txtLoginNew_I";
        private readonly string passwordLocator = "ucTopBar_ASPxRoundPanel2_ucLogin_cbLogin_pnl_txtPasswordNew_I";
        private readonly string expectedAllertMessage = "Password is required.";
        private readonly string newLoginDivXpath = "//div[@class='NewLogin']";

        public LoginPage()
        {
            Driver = WebDriverHelper.Driver;
        }        

        public LoginPage(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public LoginPage CleanCredentialsFields()
        {
            Driver.FindElement(By.Id(userLocator)).Clear();
            Driver.FindElement(By.Id(passwordLocator)).Clear();
            return this;
        }

        public LoginPage VerifyLoginPageIsOpened()
        {
            WaitHelper.WaitForElement(By.XPath(newLoginDivXpath), 5);
            Driver.FindElement(By.XPath(newLoginDivXpath));
            return this;
        }
        public PcmiUserPanelPage LoginWithCorrectCredentials(LoginPage User)
        {
            CleanCredentialsFields();
            Driver.FindElement(By.Id(userLocator)).SendKeys(User.Username);
            Driver.FindElement(By.Id(passwordLocator)).SendKeys(User.Password);
            Driver.FindElement(By.XPath(loginButtonXpath)).Click();
            return new PcmiUserPanelPage();
        }

        public LoginPage TryLoginWithoutPassword(LoginPage user)
        {
            CleanCredentialsFields();
            Driver.FindElement(By.Id(userLocator)).SendKeys(user.Username);
            Driver.FindElement(By.Id(passwordLocator)).SendKeys(user.Password);            
            Driver.FindElement(By.XPath(loginButtonXpath)).Click();
            return this;
        }

        public LoginPage VerifyPasswordIsRequiredAllert()
        {
            WaitHelper.WaitForElement(By.XPath(alertMessageXpath), 10);
            string allertMessage = Driver.FindElement(By.XPath(alertMessageXpath)).Text;
            Assert.AreEqual(expectedAllertMessage, allertMessage);
            Driver.FindElement(By.XPath(closeButtonXpath)).Click();
            return this;
        }
    }
}
