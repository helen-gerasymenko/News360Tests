using News360.Tests.Common;
using News360.Tests.Pages;
using NUnit.Framework;

namespace News360.Tests
{
    public class LoginTests : TestBase
    {
        private HomePage _homePage;

        public override void BeforeAll()
        {
            base.BeforeAll();
            _homePage = new HomePage(_driver);
            _driver.Manage().Cookies.DeleteAllCookies();
        }

        [Test]
        public void Can_login_with_registered_valid_email_and_6characters_password()
        {
            //set up - used a pregistered account
            var email = "valid_registered_user@gmail.com";
            var password = "123456";

            //act
            var loginForm = _homePage.OpenLoginForm();
            var readlingListPage = loginForm.Login(email, password);

            var username = email.Substring(0, 21).ToUpper();
            Assert.AreEqual(username, readlingListPage.MenuBar.Username);
        }
    }
}
