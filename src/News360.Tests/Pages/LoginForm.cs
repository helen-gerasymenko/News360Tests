using System;
using News360.Tests.Common;
using OpenQA.Selenium;

namespace News360.Tests.Pages
{
    public class LoginForm : PageBase
    {
        public LoginForm(IWebDriver driver): base(driver){ }

        private const string FormXPath = "(//form[@class='signin ng-pristine ng-valid'])[1]";

        public SignupForm OpenSignupForm()
        {
            var signupLink = _driver.FindElementWait(By.XPath("(//form[@class='signin ng-pristine ng-valid'])[1]//a[@href='#signup-box']"));
            signupLink.Click();

            var form = new SignupForm(_driver);
            return form;
        }

        public ReadingListPage Login(string email, string password)
        {
            var emailField = _driver.FindElementWait(By.Id("signinemail"));
            emailField.Clear();
            emailField.SendKeys(email);

            var passwordField = _driver.FindElementWait(By.XPath(FormXPath + "//input[@type='password']"));
            passwordField.Clear();
            passwordField.SendKeys(password);

            var signinButton = _driver.FindElementWait(By.XPath(FormXPath + "//button[text()='Sign in']"));
            signinButton.Click();
            var page = new ReadingListPage(_driver);
            return page;
        }
    }
}
