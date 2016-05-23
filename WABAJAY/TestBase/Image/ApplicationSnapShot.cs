using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Microsoft.Test.VisualVerification;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities;
using System.ComponentModel;
using TestBaseUtility;

namespace Utilities
{
    public class ApplicationSnapShot
    {
        //  Assertion with three validation
        public static void GetApplicationSnapShot(IWebDriver webdriver, ClientOperatingSystem OperatingSystem, ClientBrowser browser, string uniqueRowIdentifier = null)
        {
            int left, top, width, height;
            // Allow time for all elements on the map to be fully displayed.
            Thread.Sleep(5000);

            // Use calling method to form a unique file name for the baseline data.
            StackTrace stackTrace = new StackTrace();
            string uniqueIdentifier = uniqueRowIdentifier + OperatingSystem.ToString() + browser.ToString();
            string uniqueFileName = AppCommonUtility.GetUniqueFileName(uniqueIdentifier, stackTrace);


            CreateAppSnap(webdriver, out left, out top, out width, out height);

            // Crop the screenshot and convert it to a snapshot type.
            Rectangle rectangle = new Rectangle(left, top, width, height);
            Snapshot snapshot = GetSnapshotOfMap(webdriver);

            CreateSnapShot.AppCreateSnapShot(snapshot, uniqueFileName, rectangle);

        }

        public static Snapshot GetSnapshotOfMap(IWebDriver webdriver)
        {
            Bitmap screenshotAsBitmap = null;

            // Use the webdriver internal screenshot capabilites if they are implemented.
            if (webdriver is ITakesScreenshot)
            {
                Screenshot screenShot = ((ITakesScreenshot)webdriver).GetScreenshot();
                byte[] screenshotAsByteArray = screenShot.AsByteArray;
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                screenshotAsBitmap = (Bitmap)tc.ConvertFrom(screenshotAsByteArray);
            }
            else
            {
                // Get the rectangle containing the browser client area.
                int browserPositionX = webdriver.Manage().Window.Position.X;
                int browserPositionY = webdriver.Manage().Window.Position.Y;
                int browserFullWidth = webdriver.Manage().Window.Size.Width;
                int browserFullHeight = webdriver.Manage().Window.Size.Height;
                IJavaScriptExecutor js = webdriver as IJavaScriptExecutor;
                int browserClientWidth = (int)(long)js.ExecuteScript("return document.body.clientWidth");
                int browserClientHeight = (int)(long)js.ExecuteScript("return document.body.clientHeight");
                Point point = new Point(browserPositionX + browserFullWidth - browserClientWidth, browserPositionY + browserFullHeight - browserClientHeight);
                Size size = new Size(browserClientWidth, browserClientHeight);
                Rectangle bounds = new Rectangle(point, size);
                Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }
                screenshotAsBitmap = bitmap;
            }
            return Snapshot.FromBitmap(screenshotAsBitmap);
        }

        
        public static void CreateAppSnap(IWebDriver driver, out int left, out int top, out int width, out int height)
        {
            IWebElement mapRectangle = driver.FindElement(By.Id("main-page"));
            ILocatable location = (ILocatable)mapRectangle;
            System.Drawing.Point scrPos = location.Coordinates.LocationInViewport;
            //location.Coordinates.LocationOnScreen;
            left = scrPos.X;
            top = scrPos.Y;
            System.Drawing.Size size = mapRectangle.Size;
            height = size.Height;
            width = size.Width;
        }
    }
}
