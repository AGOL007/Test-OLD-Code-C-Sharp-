using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;

namespace WAPTestBase
{
    [TestClass()]
    public abstract class TestBaseWAB
    {
        private static string baseURL = null;
        private static string seleniumCommonFilesFolder = null;
        protected static ClientOperatingSystem operatingSystem;
        protected static ClientBrowser browser;
        protected IWebDriver driver;

        [TestInitialize]
        public virtual void Setup()
        {
            // Get the settings from the app.config.
            baseURL = ConfigurationManager.AppSettings["BaseURL"];
            seleniumCommonFilesFolder = ConfigurationManager.AppSettings["SeleniumCommonFilesFolder"];

            // Get the browser from the app.config.
            if (!Enum.TryParse(ConfigurationManager.AppSettings["OperatingSystem"], out operatingSystem))
                throw new Exception("Unknown operating system specified in app.config.");

            // Get the browser from the app.config.
            if (!Enum.TryParse(ConfigurationManager.AppSettings["Browser"], out browser))
                throw new Exception("Unknown browser specified in app.config.");

            //  Launch browser
            TimeSpan timeSpan = new TimeSpan(0, 0, 300);
            switch (browser)
            {
                case ClientBrowser.Chrome:
                    driver = new ChromeDriver(seleniumCommonFilesFolder, new ChromeOptions(), timeSpan);
                    break;
                case ClientBrowser.Firefox:
                    driver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), timeSpan);
                    break;
                case ClientBrowser.InternetExplorer:
                    driver = new InternetExplorerDriver(seleniumCommonFilesFolder, new InternetExplorerOptions(), timeSpan);
                    break;
                case ClientBrowser.Safari:
                    driver = new SafariDriver();
                    break;
                default:
                    throw new Exception("Unknown browser");
            }

            driver.Navigate().GoToUrl("about:blank");
            driver.Manage().Window.Maximize();

            // Go to the url.
            driver.Navigate().GoToUrl(baseURL);
        }

        [TestCleanup]
        public virtual void TearDown()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
    }
}
