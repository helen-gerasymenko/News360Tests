using News360.Tests.Common;
using OpenQA.Selenium;

namespace News360.Tests.Pages
{
    public class SignupForm : PageBase
    {
        public SignupForm(IWebDriver driver): base(driver){ }

        private const string FormXPath = "//form[@class='signup ng-pristine ng-valid' and @style='display: block;']";

        public IntroPage Signup(string email, string password)
        {
            var emailField = _driver.FindElementWait(By.Id("signupemail"));
            emailField.SendKeys(email);

            var passwordField = _driver.FindElementWait(By.Id("password"));
            passwordField.SendKeys(password);

            var confirmPasswordField = _driver.FindElementWait(By.XPath(FormXPath + "//input[@class='confirmpassword textbox']"));
            confirmPasswordField.SendKeys(password);

            var signupButton = _driver.FindElementWait(By.XPath(FormXPath + "//button[text()='Sign up']"));
            signupButton.Click();
            var page = new IntroPage(_driver);
            return page;
        }
    }
}