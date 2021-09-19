using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;

namespace AutomationPractice.Page
{
    public class APHomePage : BasePage
    {
        private const string PageAddress = "http://automationpractice.com/index.php";
        private IWebElement _searchField => Driver.FindElement(By.Id("search_query_top"));
        private IWebElement _searchButton => Driver.FindElement(By.XPath("//button[@name='submit_search']"));
        private IWebElement _logOutButton => Driver.FindElement(By.CssSelector(".logout"));
        private IReadOnlyCollection<IWebElement> _bestSellers => Driver.FindElements(By.XPath("//a[@class='product-name']"));
        private IReadOnlyCollection<IWebElement> _addToCartButtons => Driver.FindElements(By.XPath("//*[@title='Add to cart']"));

        public APHomePage(IWebDriver webdriver) : base(webdriver)
        { }

        public void NavigateToPage()
        {
            if (Driver.Url != PageAddress)
                Driver.Url = PageAddress;
        }

        public void EnterProductNameIntoSearchField(string product)
        {
            _searchField.SendKeys(product);
            _searchButton.Click();
        }

        public void LogOut()
        {
            _logOutButton.Click();
        }

        public void AddProductToCartFromBestSellers(int productIndex)
        {

            IWebElement product = _bestSellers.ElementAt(productIndex);
            Actions action = new Actions(Driver);
            action.MoveToElement(product);
            action.Build().Perform();
            IWebElement addToCartButton = _addToCartButtons.ElementAt(productIndex);
            addToCartButton.Click();
        }
    }
}
