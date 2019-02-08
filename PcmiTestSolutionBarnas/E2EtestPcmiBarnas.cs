using Microsoft.VisualStudio.TestTools.UnitTesting;
using PcmiTestSolutionBarnas.Helpers;
using PcmiTestSolutionBarnas.Pages;

namespace PcmiTestSolutionBarnas
{
    [TestClass]
    public class E2EtestPcmiBarnas : WebDriverHelper
    {
        private readonly AddContractPage ContractSetup = new AddContractPage("100", "3N4BB41D7W2794739", "Loan", "10,000.00", "26", "LN00000012", "1998", "NISSAN", "Sentra SE");
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
            VerifyLoginPageIsOpened().
            TryLoginWithoutPassword(IncorrectUser).
            VerifyPasswordIsRequiredAllert().
            LoginWithCorrectCredentials(CorrectUser).
            VerifyPcmiUserPanelPageIsOpened().
            ClickOnAddContractLink().
            GoToNewlyOpenedTab().
            VerifyAddContractPageIsOpened().
            FillAndVerifyDealSetupFormFields(ContractSetup).
            VerifyFieldsAutodecode(ContractSetup).
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
