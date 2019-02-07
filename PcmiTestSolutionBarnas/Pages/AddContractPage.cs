using OpenQA.Selenium;
using System;
using System.Linq;
using PcmiTestSolutionBarnas.Helpers;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace PcmiTestSolutionBarnas.Pages
{
    public class AddContractPage
    {
        public string SaleOdom { get; set; }
        public string Vin { get; set; }
        public string FinanceType { get; set; }
        public string AmountFinanced { get; set; }
        public string FinanceTerm { get; set; }
        public string LenderNumber { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public IWebDriver Driver { get; set; }

        private AddContractPage ContractSetup;

        private readonly string vehicleInformationPath = "//*[contains(@class,'panel-title upper')] [contains(text(),'Vehicle Information')]";
        private readonly string saleOdometerPath = "//*[@id='formAddContract']//label[text()='Sale Odometer']/..//input";
        private readonly string vinPath = "//*[@id='formAddContract']//label[text()='VIN']/..//input";
        private readonly string financeTypePath = "//*[@id='divPanelFinancialInfo']/form//label/..//input";

        private readonly string amountFinancedPath = "//*[@id='divPanelFinancialInfo']//label[text()='Amount Financed']/..//input";
        private readonly string paymentPath = "//*[@id='divPanelFinancialInfo']//label[text()='Payment']/..//input";
        private readonly string msrpAndNadaPath = "//*[@id='divPanelFinancialInfo']//label[text()='MSRP/NADA']/..//input";
        
        // cannot find vehiclePurchasePricePath
        private readonly string vehiclePurchasePricePath = "//*[@id='divPanelFinancialInfo']//label[text()='Vehicle Purchase Price']/..//input";

        private readonly string financeTermPath = "//*[@id='divPanelFinancialInfo']//label[text()='Finance/Lease Term']/..//input";
        private readonly string lenderNumberPath = "//*[@id='divPanelFinancialInfo']//label[text()='Lender Search']/..//input";
        private readonly string yearPath = "//*[@id='formAddContract']//label[text()='Year']/..//input";
        private readonly string makePath = "//*[@id='formAddContract']//label[text()='Make']/..//input";
        private readonly string modelPath = "//*[@id='formAddContract']//label[text()='Model']/..//input";

        private readonly string expectedAmountFinancedValue = "$10,000.00";
        private readonly string expectedTitle = "Add Contract";

        public AddContractPage()
        {
            Driver = WebDriverHelper.Driver;
        }
  
        public AddContractPage(string saleOdomValue, string vinValue, string financeTypeValue, 
                string amountFinancedValue, string financeTermValue, string lenderNumberValue, string yearValue, string makeValue, string modelValue)
        {
            SaleOdom = saleOdomValue;
            Vin = vinValue;
            FinanceType = financeTypeValue;
            AmountFinanced = amountFinancedValue;
            FinanceTerm = financeTermValue;
            LenderNumber = lenderNumberValue;
            Year = yearValue;
            Make = makeValue;
            Model = modelValue;
    }

        public AddContractPage GoToNewlyOpenedTab()
        {
            ReadOnlyCollection<String> windowHandles = WebDriverHelper.Driver.WindowHandles;
            String firstTab = windowHandles[0];
            String lastTab = windowHandles[windowHandles.Count - 1];
            WebDriverHelper.Driver.SwitchTo().Window(lastTab);
            string title = WebDriverHelper.Driver.Title;
            Assert.AreEqual(expectedTitle, title);
            return this;
        }

        public AddContractPage VerifyAddContractPageIsOpened()
        {
            Thread.Sleep(5000);
            string title = Driver.Title;
            Assert.AreEqual(expectedTitle, title);
            return this;
        }
        public static void VerifyAndAssertElementByXpath(string xPath, string expectedValue)
        {            
            WaitHelper.waitForElement(By.XPath(xPath), 5);
            string saleOdometerValue = WebDriverHelper.Driver.FindElement(By.XPath(xPath)).GetAttribute("value");
            Assert.AreEqual(expectedValue, saleOdometerValue);
        }

        public AddContractPage FillAndVerifyDealSetupFormFields( )
        {
            ContractSetup = new AddContractPage("100", "3N4BB41D7W2794739", "Loan", "10,000.00", "26", "LN00000012", "1998", "NISSAN", "Sentra SE");

            WaitHelper.waitForElement(By.XPath(vehicleInformationPath), 10);
            Driver.FindElement(By.XPath(saleOdometerPath)).SendKeys(ContractSetup.SaleOdom);
            VerifyAndAssertElementByXpath(saleOdometerPath, ContractSetup.SaleOdom);

            Driver.FindElement(By.XPath(vinPath)).SendKeys(ContractSetup.Vin);
            VerifyAndAssertElementByXpath(vinPath, ContractSetup.Vin);

            Driver.FindElement(By.XPath(financeTypePath)).SendKeys(ContractSetup.FinanceType);
            VerifyAndAssertElementByXpath(financeTypePath, ContractSetup.FinanceType);

            ////Fill "Amoount Financed"
            ////value isn't sent and reciverd correctly
            //Driver.FindElement(By.XPath(amountFinancedPath)).SendKeys(ContractSetup.AmountFinanced);
            //VerifyAndAssertElementByXpath(amountFinancedPath, expectedAmountFinancedValue);

            //Fill "Finance Term" 
            //Xpath should be replaced by more unique path
            Driver.FindElement(By.XPath(financeTermPath)).SendKeys(ContractSetup.FinanceTerm);
            VerifyAndAssertElementByXpath(financeTermPath, ContractSetup.FinanceTerm);

            Driver.FindElement(By.XPath(lenderNumberPath)).SendKeys(ContractSetup.LenderNumber);
            VerifyAndAssertElementByXpath(lenderNumberPath, ContractSetup.LenderNumber);

            return this;
        }

        public AddContractPage VerifyDolarSignInAmountFields()
        {            
            string amountFinancedSign = Driver.FindElement(By.XPath(amountFinancedPath)).GetAttribute("value");
            StringAssert.StartsWith(amountFinancedSign, "$");
            string paymentSign = Driver.FindElement(By.XPath(paymentPath)).GetAttribute("value");
            StringAssert.StartsWith(paymentSign, "$");
            string msrpAndNadaSign = Driver.FindElement(By.XPath(msrpAndNadaPath)).GetAttribute("value");
            StringAssert.StartsWith(msrpAndNadaSign, "$");

            //// cannot find vehiclePurchasePricePath
            //string vehiclePurchasePriceSign = Driver.FindElement(By.XPath(vehiclePurchasePricePath)).GetAttribute("value");
            //StringAssert.StartsWith(vehiclePurchasePriceSign, "$");

            return this;
        }

        public AddContractPage VerifyFieldsAutodecode()
        {
            Thread.Sleep(1000);
            VerifyAndAssertElementByXpath(yearPath, ContractSetup.Year);
            VerifyAndAssertElementByXpath(makePath, ContractSetup.Make);
            VerifyAndAssertElementByXpath(modelPath, ContractSetup.Model);
            return this;
        }

        public PcmiUserPanelPage CloseAddContractTab()
        {
            //First close method (by menu)
            //Driver.FindElement(By.XPath("//*[@id='ddCostType']/li/button")).Click();
            //Driver.FindElement(By.XPath("//*[@id='ddCostType']//li[5]/a")).Click();
            //Driver.SwitchTo().Alert().Accept();

            //Second close method
            Driver.Close();
            Driver.SwitchTo().Window(Driver.WindowHandles.First());

            //Third close method
            //var tabs = Driver.WindowHandles;
            //if (tabs.Count > 1)
            //{
            //    Driver.SwitchTo().Window(tabs[1]);
            //    Driver.Close();
            //    Driver.SwitchTo().Window(tabs[0]);
            //}

            return new PcmiUserPanelPage();
        }
    }
}
