using News360.Tests.Common;
using OpenQA.Selenium;

namespace News360.Tests.Pages
{
    public class MenuBar
    {
        protected readonly IWebDriver _driver;

        public MenuBar(IWebDriver driver)
        {
            _driver = driver;
        }

        public string Username => _driver.FindElementText(By.XPath("//a[@class='user default']/span"));

    }
}
