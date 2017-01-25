using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace News360.Tests.Common
{
    public static class ExtensionMethods
    {
        public static IWebElement FindElementWait(this IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IEnumerable<IWebElement> foundElements = null;
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            wait.Until(d =>
            {
                try
                {
                    foundElements = driver.FindElements(by).Where(x => x.Displayed && x.Enabled);
                    return foundElements.Any();
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
            return foundElements.First();
        }

        public static string FindElementText(this IWebDriver driver, By by)
        {
            var element = FindElementWait(driver, by);
            return element.Text;
        }

        public static string RandomString(this IWebDriver driver, int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray()
                );
        }

        public static void ScrollPage(this IWebDriver driver, int x, int y)
        {
            var coordinatesToScroll = $"scroll('{x}', '{y}')";
            var jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript(coordinatesToScroll);
        }
    }
}
