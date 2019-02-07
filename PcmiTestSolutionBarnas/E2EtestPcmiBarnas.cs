using Microsoft.VisualStudio.TestTools.UnitTesting;
using PcmiTestSolutionBarnas.Helpers;
using PcmiTestSolutionBarnas.Pages;

namespace PcmiTestSolutionBarnas
{
    [TestClass]
    public class E2EtestPcmiBarnas : WebDriverHelper
    {
        //private string userName = "tester@pcmi.com";
        //private string correctUserPassword = "qQadfi4$2";
        //private readonly string incorrectUserPassword = "";

        private readonly LoginPage CorrectUser = new LoginPage("tester@pcmi.com", "qQadfi4$2");
        private readonly LoginPage IncorrectUser = new LoginPage("tester@pcmi.com", "");


        [TestInitialize]
        public void SetupTest()
        {
        }

        [TestMethod]
        public void E2ETestBarnas()
        {
            GoToLoginPage().
            TryLoginWithoutPassword(IncorrectUser).
            VerifyPassworIsdRequiredAllert().
            LoginWithCorrectCredentials(CorrectUser).
            VerifyPcmiUserPanelPageIsOpened().
            ClickOnAddContractLink().
            GoToNewlyOpenedTab().
            VerifyAddContractPageIsOpened().
            FillAndVerifyDealSetupFormFields().
            VerifyFieldsAutodecode().
            VerifyDolarSignInAmountFields().
            CloseAddContractTab().
            VerifyPcmiUserPanelPageIsOpened().
            LogoutFromPcmiUserPanelPage().
            VerifyLoginPageIsOpened();
        }

        [TestCleanup]
        public void CleanUp()
        {
            Close();
        }
    }
}
