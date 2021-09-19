using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutomationPractice.Page
{
    public class APShoppingCartPage : BasePage
    {
        private IWebElement _proceedToCheckoutButton => Driver.FindElement(By.CssSelector(".button.btn.btn-default.standard-checkout.button-medium"));
        private IWebElement _proceedToCheckoutButtonInAddress => Driver.FindElement(By.XPath("//button[@name='processAddress']"));
        private IWebElement _proceedToCheckoutButtonInShipping => Driver.FindElement(By.XPath("//button[@name='processCarrier']"));
        private IWebElement _increaseProductAmountButtonInSummary => Driver.FindElement(By.Id("cart_quantity_up_2_7_0_0"));
        private IWebElement _productQuantityFieldInSummary => Driver.FindElement(By.XPath("//input[@name='quantity_2_7_0_0_hidden']"));
        private IWebElement _productUnitPriceFieldInSummary => Driver.FindElement(By.Id("product_price_2_7_0"));
        private IWebElement _totalPriceFieldInSummary => Driver.FindElement(By.Id("total_product_price_2_7_0"));
        private IWebElement _totalProductsFieldInSummary => Driver.FindElement(By.Id("total_product"));
        private IWebElement _deleteCartButtonInSummary => Driver.FindElement(By.CssSelector(".cart_quantity_delete"));

        private IWebElement _emailFieldToCreateAccountInSignIn => Driver.FindElement(By.Id("email_create"));
        private IWebElement _createAccountButtonInSignIn => Driver.FindElement(By.Id("SubmitCreate"));

        private IWebElement _emailFieldToLogInInSignIn => Driver.FindElement(By.Id("email"));
        private IWebElement _passwordFieldToLogInInSignIn => Driver.FindElement(By.Id("passwd"));
        private IWebElement _logInButtonInSignIn => Driver.FindElement(By.Id("SubmitLogin"));

        private IWebElement _firstNameField => Driver.FindElement(By.Id("customer_firstname"));
        private IWebElement _lastNameField => Driver.FindElement(By.Id("customer_lastname"));
        private IWebElement _passwordField => Driver.FindElement(By.Id("passwd"));
        private IWebElement _addressField => Driver.FindElement(By.Id("address1"));
        private IWebElement _cityField => Driver.FindElement(By.Id("city"));
        private IWebElement _postalCodeField => Driver.FindElement(By.Id("postcode"));
        private IWebElement _phoneField => Driver.FindElement(By.Id("phone_mobile"));
        private SelectElement _stateSelectionField => new SelectElement(Driver.FindElement(By.Id("id_state")));
        private IWebElement _submitAccountButton => Driver.FindElement(By.Id("submitAccount"));

        private IWebElement _termsDocument => Driver.FindElement(By.CssSelector(".iframe"));
        private IWebElement _termsDocumentCloseButton => Driver.FindElement(By.CssSelector(".fancybox-item.fancybox-close"));
        private IWebElement _termsCheckbox => Driver.FindElement(By.Id("cgv"));

        private IWebElement _paymentByBankWire => Driver.FindElement(By.CssSelector(".bankwire"));
        private IWebElement _paymentByCheck => Driver.FindElement(By.CssSelector(".cheque"));

        private IWebElement _selectedPaymentTypeIcon => Driver.FindElement(By.CssSelector(".navigation_page"));
        private IWebElement _selectedPaymentTypeDescription => Driver.FindElement(By.CssSelector(".page-subheading"));

        private IWebElement _confirmOrderButton => Driver.FindElement(By.XPath(".//span[contains(text(),'I confirm my order')]"));

        private IWebElement _messageAboutOrderStatus => Driver.FindElement(By.XPath("//p[@class='cheque-indent']//strong[@class='dark']"));

        public APShoppingCartPage(IWebDriver webdriver) : base(webdriver)
        { }

        public void ProceedToCheckoutFromCart()
        {
            _proceedToCheckoutButton.Click();
        }

        public void ProceedToCheckoutFromAddress()
        {

            _proceedToCheckoutButtonInAddress.Click();
        }

        public void ProceedToCheckoutFromShipping()
        {

            _proceedToCheckoutButtonInShipping.Click();
        }

        public void IncreaseProductAmountUpToXUnits(int amount)
        {
            for (int i = 2; i <= amount; i++)
            {
                _increaseProductAmountButtonInSummary.Click();
                GetWait().Until(d => _productQuantityFieldInSummary.GetAttribute("value").Equals(i.ToString()));
            }
        }

        public void VerifyIncreasedProductAMountAndSum(int amount)
        {
            double expectedPrice = (Convert.ToDouble(_productUnitPriceFieldInSummary.Text.Trim('$'))) * amount;
            double priceInTotalPriceField = Convert.ToDouble(_totalPriceFieldInSummary.Text.Trim('$'));
            double priceInTotalProductsField = Convert.ToDouble(_totalProductsFieldInSummary.Text.Trim('$'));
            Assert.AreEqual(expectedPrice, priceInTotalPriceField, "Wrong sum is given after amount increasing");
            Assert.AreEqual(expectedPrice, priceInTotalProductsField, "Wrong sum is given after amount increasing");
        }


        public void InsertEmailToCreateAccount(string emailAddress)
        {
            _emailFieldToCreateAccountInSignIn.SendKeys(emailAddress);
            _createAccountButtonInSignIn.Click();
        }

        public void InsertNewUserDataAndCreateAccount(string firstName, string lastName, string password, string address, string city, string state, string postalCode, string phone)
        {
            _firstNameField.SendKeys(firstName);
            _lastNameField.SendKeys(lastName);
            _passwordField.SendKeys(password);
            _addressField.SendKeys(address);
            _cityField.SendKeys(city);
            _stateSelectionField.SelectByText(state);
            _postalCodeField.SendKeys(postalCode);
            _phoneField.SendKeys(phone);
            _submitAccountButton.Click();
        }

        public void SignIn(string emailAddress, string password)
        {
            _emailFieldToLogInInSignIn.SendKeys(emailAddress);
            _passwordFieldToLogInInSignIn.SendKeys(password);
            _logInButtonInSignIn.Click();
        }

        public void ReadAndAcceptTerms()
        {
            _termsDocument.Click();
            _termsDocumentCloseButton.Click();
            if (!_termsCheckbox.Selected)
            {
                _termsCheckbox.Click();
            }
        }

        public void SelectPaymentByBankWire()
        {
            _paymentByBankWire.Click();
        }

        public void VerifySelectedPaymentByBankWire()
        {
            Assert.IsTrue(_selectedPaymentTypeIcon.Text.Contains("wire"), $"Selected payment should be by bank wire, but is {_selectedPaymentTypeIcon.Text}");
            Assert.IsTrue(_selectedPaymentTypeDescription.Text.Contains("WIRE"), $"Selected payment in description should be by wire, but is {_selectedPaymentTypeDescription.Text}");
        }

        public void SelectPaymentByCheck()
        {
            _paymentByCheck.Click();
        }

        public void VerifySelectedPaymentByCheck()
        {
            Assert.IsTrue(_selectedPaymentTypeIcon.Text.Contains("Check"), $"Selected payment in icon should be by check, but is {_selectedPaymentTypeIcon.Text}");
            Assert.IsTrue(_selectedPaymentTypeDescription.Text.Contains("CHECK"), $"Selected payment in description should be by check, but is {_selectedPaymentTypeDescription.Text}");
        }

        public void ConfirmOrder()
        {
            _confirmOrderButton.Click();
        }

        public void VerifyOrderIsComplete()
        {
            Assert.IsTrue(_messageAboutOrderStatus.Text.Contains("complete"), $"Order should be completed, but status is {_messageAboutOrderStatus.Text}");
        }

        public void DeleteCart()
        {
            _deleteCartButtonInSummary.Click();
        }

    }
}
