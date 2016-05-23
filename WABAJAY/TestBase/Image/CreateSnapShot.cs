using Microsoft.Test.VisualVerification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Utilities
{
    public class CreateSnapShot
    {
        public static void AppCreateSnapShot(Snapshot image, string uniqueFileName, Rectangle cropingRectangle)
        {
            // Results folders.
            string testProjectFolder = ConfigurationManager.AppSettings["testProjectFolder"];
            string testResultsBaselineFolder = Path.Combine(testProjectFolder, "WAB_Log");
            string testResultsImageBaselineFolder = Path.Combine(testResultsBaselineFolder, "Img_LogFolder");

            string testResultsBaselineFile = Path.Combine(testResultsImageBaselineFolder, uniqueFileName + ".png");


            if (!Directory.Exists(testResultsImageBaselineFolder))
                Directory.CreateDirectory(testResultsImageBaselineFolder);

            // Get the actual image which will be compared to the baseline image.
            Snapshot actual;
            if (cropingRectangle.Width == 0 || cropingRectangle.Height == 0)
                actual = image;
            else
                actual = image.Crop(cropingRectangle);

            if (!File.Exists(testResultsBaselineFile))
            {
                actual.ToFile(testResultsBaselineFile, ImageFormat.Png);
            }
            else
            {
                File.Delete(testResultsBaselineFile);
                actual.ToFile(testResultsBaselineFile, ImageFormat.Png);
            }
        }
    }
}
