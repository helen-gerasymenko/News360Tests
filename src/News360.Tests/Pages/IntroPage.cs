using News360.Tests.Common;
using OpenQA.Selenium;

namespace News360.Tests.Pages
{
    public class IntroPage : PageBase
    {
        public IntroPage(IWebDriver driver) : base(driver) { }

        public string PageHeader => _driver.FindElementText(By.XPath("//h2[contains(., 'Welcome to')]"));

        public void GoToReadingList()
        {
            var startReadingButton = _driver.FindElementWait(By.XPath("//div[@class='startReading ng-binding show']"));
            startReadingButton.Click();
        }
    }
}