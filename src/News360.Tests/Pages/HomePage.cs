using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using News360.Tests.Common;

namespace News360.Tests.Pages
{
    public class HomePage : PageBase
    {
        public HomePage(IWebDriver driver): base(driver){ }

        public LoginForm OpenLoginForm()
        {
            var signinLink = _driver.FindElementWait(By.XPath("//a[@class='fancybox login-signin ng-binding' and @href='#signin-box']"));
            signinLink.Click();

            var form = new LoginForm(_driver);
            return form;
        }
    }
}
