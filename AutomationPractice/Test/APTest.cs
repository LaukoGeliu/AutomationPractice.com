using NUnit.Framework;

namespace AutomationPractice.Test
{
    public class APTest : BaseTest
    {
        [TestCase("blouse", 0, "another1@mail.com", "PASSWORD", TestName = "Test option add to cart and complete order by login old user 'another1@mail.com'")]
        public static void TestNavigationFromCartToCompleteOrderByLogInUser(string productName, int productIndex, string emailAddress, string password)
        {
            _aPHomePage.NavigateToPage();
            _aPHomePage.EnterProductNameIntoSearchField(productName);
            _aPSearchResultPage.AddITemToCart(productIndex);
            _aPSearchResultPage.ProceedToCheckout();
            _aPShoppingCartPage.ProceedToCheckoutFromCart();
            _aPShoppingCartPage.SignIn(emailAddress, password);
            _aPShoppingCartPage.ProceedToCheckoutFromAddress();
            _aPShoppingCartPage.ReadAndAcceptTerms();
            _aPShoppingCartPage.ProceedToCheckoutFromShipping();
            _aPShoppingCartPage.SelectPaymentByBankWire();
            _aPShoppingCartPage.VerifySelectedPaymentByBankWire();
            _aPShoppingCartPage.ConfirmOrder();
            _aPShoppingCartPage.VerifyOrderIsComplete();
            _aPHomePage.LogOut();
        }

        [TestCase(1, "another2@mail.com", "Laura", "Dickens", "PASSWORD", "Fake Street 78", "Illinois", "Illinois", "00147", "+852147963",
            TestName = "Test select product from best sellers and complete order by creating new user 'another2@mail.com'")]
        public static void NavigateFromItemSelectionToCompletedOrder(int productIndex, string emailAddress, string firstName, string lastName, string password,
                                                                     string address, string city, string state, string postalCode, string phone)
        {
            _aPHomePage.NavigateToPage();
            _aPHomePage.AddProductToCartFromBestSellers(productIndex);
            _aPSearchResultPage.ProceedToCheckout();
            _aPShoppingCartPage.ProceedToCheckoutFromCart();
            _aPShoppingCartPage.InsertEmailToCreateAccount(emailAddress);
            _aPShoppingCartPage.InsertNewUserDataAndCreateAccount(firstName, lastName, password, address, city, state, postalCode, phone);
            _aPShoppingCartPage.ProceedToCheckoutFromAddress();
            _aPShoppingCartPage.ReadAndAcceptTerms();
            _aPShoppingCartPage.ProceedToCheckoutFromShipping();
            _aPShoppingCartPage.SelectPaymentByBankWire();
            _aPShoppingCartPage.VerifySelectedPaymentByBankWire();
            _aPShoppingCartPage.ConfirmOrder();
            _aPShoppingCartPage.VerifyOrderIsComplete();
            _aPHomePage.LogOut();
        }

        [TestCase("dress", TestName = "Test product 'dress' search")]
        [TestCase("blouse", TestName = "Test product 'blouse' search")]
        [TestCase("t-shirts", TestName = "Test product 't-shirts' search")]
        public static void TestSearchBySearcFieldFunctionality(string productName)
        {
            _aPHomePage.NavigateToPage();
            _aPHomePage.EnterProductNameIntoSearchField(productName);
            _aPSearchResultPage.VerifySearchResult(productName);
        }


        [TestCase("blouse", 0, "Product successfully added to your shopping cart", "1", TestName = "Test option add 'blouse' to cart")]
        public static void TestAddProductToCartOption(string productName, int productIndex, string succesMessage, string productAmount)
        {
            _aPHomePage.NavigateToPage();
            _aPHomePage.EnterProductNameIntoSearchField(productName);
            _aPSearchResultPage.AddITemToCart(productIndex);
            _aPSearchResultPage.VerifyAddToCart(succesMessage, productIndex, productAmount);
            _aPSearchResultPage.ProceedToCheckout();
            _aPShoppingCartPage.DeleteCart();
        }

        [TestCase("blouse", 0, 3, TestName = "Test option increase product 'blouse' amount to 3 in cart")]
        public static void NavigateFromItemSelectionToCompletedOrder(string productName, int productIndex, int productAmount)
        {
            _aPHomePage.NavigateToPage();
            _aPHomePage.EnterProductNameIntoSearchField(productName);
            _aPSearchResultPage.AddITemToCart(productIndex);
            _aPSearchResultPage.ProceedToCheckout();
            _aPShoppingCartPage.IncreaseProductAmountUpToXUnits(productAmount);
            _aPShoppingCartPage.VerifyIncreasedProductAMountAndSum(productAmount);
            _aPShoppingCartPage.DeleteCart();
        }
    }
}
