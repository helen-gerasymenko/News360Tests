using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using News360.Tests.Common;
using News360.Tests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace News360.Tests
{
    public class SignupTests : TestBase
    {
        private HomePage _homePage;

        public override void BeforeAll()
        {
            base.BeforeAll();
            _homePage = new HomePage(_driver);
            _driver.Manage().Cookies.DeleteAllCookies();
        }

        [Test]
        public void Can_sign_up_with_valid_email_and_6character_password()
        {
            //set up
            var randomString = _driver.RandomString(length: 4);
            var email = "user-" + randomString + "@gmail.com";
            var password = "ps" + randomString;

            //act
            var loginForm = _homePage.OpenLoginForm();
            var signupForm = loginForm.OpenSignupForm();
            var confirmationPage = signupForm.Signup(email, password);
            
            //assert
            Assert.AreEqual("Welcome to News360", confirmationPage.PageHeader);
            confirmationPage.GoToReadingList();

            //clean
            DeleteAccount(password);
        }

#region methods to clean data

        private void DeleteAccount(string password)
        {
            //click username to open Setting page
            var username = FindDisplayedUsernameOrNull();
            username?.Click();
            _driver.ScrollPage(x:0, y:800);
            var deleteAccountBtn =
                _driver.FindElementWait(By.XPath("//div[@class='ebutton delete start ng-binding']"));
            deleteAccountBtn.Click();

            //click Delete Account button
            var confirmBtn =
                _driver.FindElementWait(
                    By.XPath("//div[contains(@class, 'start')]//div[text()='Delete account']"));
            confirmBtn.Click();

            //enter password and delete account
            var passwordField =
                _driver.FindElementWait(By.XPath("//div[@class='confirmpassword']/input[@placeholder='Password']"));
            passwordField.Clear();
            passwordField.SendKeys(password);
            confirmBtn =
                _driver.FindElementWait(By.XPath("//div[contains(@class, 'permanently')]//div[text()='Delete Account']"));
            confirmBtn.Click();

            //wait until you are redirected to home page and see a slogan 
            const string sloganXPath = "//div[@class='bSlogan']/span[contains(., 'News360 is an app that')]";
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(sloganXPath)));
        }

        private IWebElement FindDisplayedUsernameOrNull()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IEnumerable<IWebElement> foundElements = null;
            try
            {
                wait.Until(d =>
                {
                    foundElements = _driver.FindElements(By.XPath("//a[@class='user default']")).Where(x => x.Displayed);
                    return foundElements.Any();
                });
            }
            catch (Exception)
            {
                //if username is not displayed
                return null;
            }
            return foundElements.First();
        }

#endregion

    }
}
