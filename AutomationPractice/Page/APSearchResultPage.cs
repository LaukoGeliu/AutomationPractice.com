using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPractice.Page
{
    public class APSearchResultPage : BasePage
    {
        private IReadOnlyCollection<IWebElement> _searchResults => Driver.FindElements(By.XPath("//div[@class='product-container']//a[@class='product-name']"));
        private IReadOnlyCollection<IWebElement> _inStockNotifications => Driver.FindElements(By.ClassName("available-now"));
        private IReadOnlyCollection<IWebElement> _addToCartButtons => Driver.FindElements(By.XPath("//*[@title='Add to cart']"));
        private IWebElement _successMessageToCart => Driver.FindElement(By.XPath("//div[@class='layer_cart_product col-xs-12 col-md-6']//h2"));
        private IWebElement _productNameInCart => Driver.FindElement(By.Id("layer_cart_product_title"));
        private IWebElement _proceedToCheckoutButton => Driver.FindElement(By.CssSelector(".btn.btn-default.button.button-medium"));
        private IWebElement _productAmountInCartIcon => Driver.FindElement(By.CssSelector(".ajax_cart_quantity.unvisible"));
        public APSearchResultPage(IWebDriver webdriver) : base(webdriver)
        { }

        public void VerifySearchResult(string searchedProduct)
        {
            foreach (IWebElement result in _searchResults)
            {
                Assert.IsTrue(result.Text.ToLower().Contains(searchedProduct.ToLower()), $"Search result '{result.Text.ToLower()}' does not contain product name '{searchedProduct.ToLower()}'");
            }

        }

        public void AddITemToCart(int productIndex)
        {
            IWebElement product = _inStockNotifications.ElementAt(productIndex);
            Actions action = new Actions(Driver);
            action.MoveToElement(product);
            action.Build().Perform();
            IWebElement addToCartButton = _addToCartButtons.ElementAt(productIndex);
            addToCartButton.Click();
            GetWait().Until(d => _successMessageToCart.Displayed);
        }

        public void VerifyAddToCart(string succesMessage, int productIndex, string productAmount)
        {
            Assert.AreEqual(succesMessage, _successMessageToCart.Text);
            Assert.AreEqual(_searchResults.ElementAt(productIndex).Text, _productNameInCart.Text, "Wrong product added to cart");
            Assert.AreEqual(productAmount, _productAmountInCartIcon.Text, "Wrong amount in cart");
        }

        public void ProceedToCheckout()
        {
            _proceedToCheckoutButton.Click();
        }
    }
}


