using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPractice.Page
{
    public class APCreateAccountPage : BasePage
    {
        private IWebElement _firstNameField => Driver.FindElement(By.Id("customer_firstname"));
        private IWebElement _lastNameField => Driver.FindElement(By.Id("customer_lastname"));
        private IWebElement _passwordField => Driver.FindElement(By.Id("passwd"));
        private IWebElement _addressField => Driver.FindElement(By.Id("address1"));
        private IWebElement _cityField => Driver.FindElement(By.Id("city"));
        private IWebElement _postalCodeField => Driver.FindElement(By.Id("postcode"));
        private IWebElement _phoneField => Driver.FindElement(By.Id("phone_mobile"));
        private SelectElement _stateSelectionField => new SelectElement(Driver.FindElement(By.Id("id_state")));
        private IWebElement _submitAccountButton => Driver.FindElement(By.Id("submitAccount"));
        public APCreateAccountPage (IWebDriver webdriver) : base(webdriver)
        { }

        public void InsertNewUserDataAndCreateAccount(string firstName, string lastName, string password,string address, string city, string state, string postalCode, string phone)
        {
            _firstNameField.SendKeys(firstName);
            _lastNameField.SendKeys(lastName);
            _passwordField.SendKeys(password);
            _addressField.SendKeys(address);
            _cityField.SendKeys(city);
            _stateSelectionField.SelectByText(state);
            _postalCodeField.SendKeys(postalCode);
            _phoneField.SendKeys(phone);
        }


    }
}
