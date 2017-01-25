using System;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace News360.Tests.Common
{
    [TestFixture]
    public abstract class TestBase
    {
        protected IWebDriver _driver;
        private readonly string _homePageUrl;

        protected TestBase()
        {
            _homePageUrl = new Uri(ConfigurationManager.AppSettings["siteBaseUrl"]).ToString();
        }

        [OneTimeSetUp]
        public void BeforeAllSetUp()
        {
            BeforeAll();
        }

        public virtual void BeforeAll()
        {
            _driver = CreateChromeDriver();
            _driver.Url = _homePageUrl;
            _driver.Manage().Window.Size = new Size(1500, 900);
        }

        [OneTimeTearDown]
        public virtual void AfterAll()
        {
            _driver.Quit();
        }

        [TearDown]
        public virtual void AfterEach(){}

        protected static IWebDriver CreateChromeDriver()
        {
            string chromeDriverPath = GetAssemblyLocation();
            chromeDriverPath += @"..\..\..\packages\Selenium.WebDriver.ChromeDriver.2.27.0\driver";
            return new ChromeDriver(chromeDriverPath);
        }

        protected static string GetAssemblyLocation()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var codebase = new Uri(assembly.CodeBase);
            var path = codebase.LocalPath;
            var pathParts = path.Split('\\');
            path = path.Replace(pathParts.Last(), string.Empty);
            return path;
        }
    }
}
