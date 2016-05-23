using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace TestBaseUtility
{
    public static class AppCommonUtility
    {
        private static int timeOut = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOut"]);

        public static void FindElementDynamically(IWebDriver driver, string strParameter, string elementLocater = "ClassName")
        {
            switch (elementLocater)
            {
                //  Wait for an element using Class name
                case "ClassName":
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.ClassName(strParameter)));
                    break;

                //  Wait for an element using Id
                case "Id":
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id(strParameter)));
                    break;

                //  Wait for an element using Tag name
                case "TagName":
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.TagName(strParameter)));
                    break;

                //  Wait for an element using XPath
                case "XPath":
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath(strParameter)));
                    break;

                //  Wait for an element using CssSelector
                case "CssSelector":
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.CssSelector(strParameter)));
                    break;
            }
        }

        #region Unique File Name
        public static string GetUniqueFileName(string uniqueIdentifier, StackTrace stackTrace, bool isHardwareGraphicsAccelerationEnabled = false)
        {
            MethodBase method;
            if (stackTrace.GetFrame(1).GetMethod().Name == stackTrace.GetFrame(0).GetMethod().Name)
                method = stackTrace.GetFrame(2).GetMethod();
            else
                method = stackTrace.GetFrame(1).GetMethod();
            string testClassName = method.ReflectedType.Name;
            string testMethodName = method.Name;

            string uniqueFileName;
            if (string.IsNullOrEmpty(uniqueIdentifier))
                uniqueFileName = method.ReflectedType.Name + "_" + method.Name;
            else
                uniqueFileName = method.ReflectedType.Name + "_" + method.Name + "_" + uniqueIdentifier;

            return uniqueFileName;
        }
        #endregion

        #region List
        public static void ListClickActionByName(IList<IWebElement> listCollection, string elementName)
        {
            foreach (IWebElement item in listCollection)
            {
                if (item.Displayed && item.Enabled && string.Equals(item.Text, elementName))
                {
                    item.Click();
                    break;
                }
            }
        }
        #endregion

        #region Display and Enable Click

        public static void ClickDisplayedAndEnableElement(IWebElement elemenmt)
        {
            if (elemenmt.Displayed && elemenmt.Enabled )
            {
                elemenmt.Click();
            }
        }
        #endregion

        #region Click On Feature
        public static void ClickAndSelectFeature(IWebElement element, string featureTag = "circle", int featureIndex = 0)
        {
            //IList<IWebElement> iconContainer = element.FindElements(By.TagName(featureTag));
            //iconContainer[featureIndex].Click(); 
        }

        #endregion

        #region Fill Data in Textbox using Lable name
        //  Field class name (Collection) has been used to perform the below activity
        public static void FillDataTextBoxUsingLabelName(IList<IWebElement> element, string labelName, string fillData, string className = "dijitInputInner")
        {
            foreach (IWebElement item in element)
            {
                IWebElement labelContainer = null;
                try
                {
                    labelContainer = item.FindElement(By.TagName("label"));
                }
                catch (Exception)
                {
                    continue;
                }
                if (string.Equals(labelContainer.Text, labelName))
                {
                    IWebElement textBox = item.FindElement(By.ClassName(className));
                    textBox.Clear();
                    textBox.SendKeys(fillData);
                    break;
                }
            }
        }
        #endregion

        #region Click on CheckBox using Lable name

        //  Field class name (Collection) has been used to perform the below activity
        public static void ClickCheckBoxUsingLabelName(IList<IWebElement> element, string labelName, string className = "jimu-float-leading")
        {
            foreach (IWebElement item in element)
            {
                IWebElement labelContainer = null;
                try
                {
                    labelContainer = item.FindElement(By.TagName("label"));
                }
                catch (Exception)
                {
                    continue;
                }
                if (string.Equals(labelContainer.Text, labelName))
                {
                    IWebElement checkBox = item.FindElement(By.ClassName(className));
                    checkBox.Click();
                    break;
                }
            }
        }

        #endregion

        #region Switch Frame
        public static void SwitchIFrame(IWebDriver driver, string frameName = "previewConfigFrame")
        {
            IList<IWebElement> frame = driver.FindElements(By.TagName("iframe"));
            foreach (IWebElement item in frame)
            {
                if (string.Equals(item.GetAttribute("Name").ToString(), frameName))
                {
                    driver.SwitchTo().Frame(item);
                    break;
                }
            }
        }
        #endregion

        #region Get Body Text
        public static string GetBodyText(IWebDriver driver)
        {
            IWebElement body = driver.FindElement(By.TagName("body"));
            return body.Text;
        }
        #endregion

        #region Wait to load

        public static void WaitToLoadElement(IWebDriver driver, int timeOutInSecond = 10)
        {
            driver.SwitchTo().DefaultContent();
            IWebElement loadElemenmt = null;
            int i = 0;

            do
            {
                i++;
                Thread.Sleep(1000);
                loadElemenmt = driver.FindElement(By.ClassName("jimu-loading")); 

            } while ((loadElemenmt.Displayed) && (i <= timeOutInSecond));
        }

        #endregion

    }
}
