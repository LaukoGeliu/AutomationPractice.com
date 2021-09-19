using AutomationPractice.Driver;
using AutomationPractice.Page;
using AutomationPractice.Tools;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace AutomationPractice.Test
{
    public class BaseTest
    {
        public static IWebDriver driver;
        public static APHomePage _aPHomePage;
        public static APSearchResultPage _aPSearchResultPage;
        public static APShoppingCartPage _aPShoppingCartPage;
        public static APCreateAccountPage _aPCreateAccountPage;

       [OneTimeSetUp]
        public static void SetUp()
        {
            driver = CustomDriver.GetChromeDriver();
            _aPHomePage = new APHomePage(driver);
            _aPSearchResultPage = new APSearchResultPage(driver);
            _aPShoppingCartPage = new APShoppingCartPage(driver);
            _aPCreateAccountPage = new APCreateAccountPage(driver);
        }

        [TearDown]
        public static void TakeScreenshot()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                MyScreenshot.MakeScreeshot(driver);
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            driver.Quit();
        }
    }
}