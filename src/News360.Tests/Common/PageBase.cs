using News360.Tests.Pages;
using OpenQA.Selenium;

namespace News360.Tests.Common
{
    public abstract class PageBase
    {
        protected IWebDriver _driver;

        protected PageBase(IWebDriver driver)
        {
            _driver = driver;
        }

        public MenuBar MenuBar => new MenuBar(_driver);
    }
}
