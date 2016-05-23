using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading;
using System.Xml;
using TestBase.Log;
using TestBase.Validation;
using TestBaseUtility;
using Utilities;
using WABTest;
using Newtonsoft.Json;
using WebAppCommonSeleniumActions;
using WebAppDataSource;
using WebAppFramework.Validation;

namespace WebAppTest
{
    /// <summary>
    /// Test class for Network Configuration
    /// </summary>
    [TestClass()]
    public class NetworkConfiguration : WABTestBase
    {
        private TestContext context;
        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }
        }
       
        /// <summary>
        /// Test case to configure the Network
        /// </summary>
        [DataSource("System.Data.OleDb", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\\\CSSLESXI2-VM9\\Users\\Administrator\\Desktop\\DataSource\\DataSourceTest1.xlsx; Persist Security Info=False;Extended Properties='Excel 12.0 Xml;HDR=YES'", "Sheet1$", DataAccessMethod.Sequential), TestMethod()]
        public void WAB_NetworkConfiguration()
        {
            // Set Data Source
            NameValueCollection webData = new NameValueCollection();
            WabDataSource.DataSource(context, webData);

            //  Click on continue button
            WABSeleniumActions.ClickContinueBtn(driver);

            //  Enter Login credentials
            WABSeleniumActions.EnterUserCredential(driver, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString());

            //  Click on Create new app button
            WABSeleniumActions.NetworkTraceCreateNewApp(driver);

            WABSeleniumActions.NextButtonClickCreateAppPanel(driver);

            //  Fill data in Create new app Pop up box
            WABSeleniumActions.FillDetailsCreateNewApp(driver, context);

            //  Get App number - Used for Validation
            string applicationNumber = WABSeleniumActions.GetAppNumberFromURL(driver);

            //  Click on Menu option
            WABSeleniumActions.ClickMenuOption(driver, context);

            //  Click on choose map option
            WABSeleniumActions.ClickChooseMapButton(driver);

            //  Search for web map
            WABSeleniumActions.SearchForWebMap(driver, context);

            //  Select web Map by name
            WABSeleniumActions.SelectMapByName(driver, context);

            //Cick on Save Button after Map Load
            WABSeleniumActions.ClickConfigSaveButton(driver);

            string webMapId = WABSeleniumActions.GettingWebmapId(driver);

            string WebmapJson = "http://www.arcgis.com/sharing/rest/content/items/" + webMapId + "/data?f=pjson";
            IWebElement body = driver.FindElement(By.TagName("body"));
            string webmapJson = body.Text;
            body.SendKeys(Keys.Control + 't');
            driver.Navigate().GoToUrl(WebmapJson);

             NameValueCollection webmapLayerName = new NameValueCollection();
             WABValidation.GetIdFromWebmap(driver , AppCommonUtility.GetBodyText(driver), webmapLayerName);
            
        
            //  Click on Menu option
            WABSeleniumActions.ClickMenuOption(driver, context);

            //  Click on add widget icon
            WABSeleniumActions.ClickAddWidgetIcon(driver);

            //Click on SetButton Controller
            WABSeleniumActions.ClickSetWidgetController(driver);

            //  Select widget and click OK button
            WABSeleniumActions.SelectWidgetFormList(driver, context);

            ////Cick on Save Button after Map Load
            //WABSeleniumActions.ClickConfigSaveButton(driver);

            //  Click on set button
            WABSeleniumActions.ConfigureNetworkSetUrl(driver, context);

            //  Select 'Add service URL' radio button
            WABSeleniumActions.SetTaskSelectRadioButton(driver, context);

            //  Enter GeoProcessing Service Url
            WABSeleniumActions.EnterGeoProcessingServiceUrl(driver, context);

            //  Click validate button
            WABSeleniumActions.GeoProcessingServiceUrlClickValidationBtn(driver);

            //  Validate URL and click ok
            WABSeleniumActions.ValidateGeoProcessingServiceUrlClickOK(driver, context);

            //  Configure network trace 
            WABSeleniumActions.FillDataInConfigureNetworkTraceInputOutput(driver, context);

            WABSeleniumActions.FillDataInOverviewOther(driver, context);
          
            //  Click on Configure network trace Save button
            WABSeleniumActions.ClickConfigSaveButton(driver);

            ApplicationSnapShot.GetApplicationSnapShot(driver, ClientOperatingSystem.Windows7, ClientBrowser.Firefox, "TestRun_" + context.DataRow["Index"].ToString());

            //  Selenium driver switch
            AppCommonUtility.SwitchIFrame(driver);

            //  Click on Network Trace menu icon
            WABSeleniumActions.NetworkTraceIconClick(driver);

            //  Click on Flag icon using name
            WABSeleniumActions.NetworkTraceClickFlagIconUsingName(driver, "Input locations for Start");

            //  Get Flag Names
            List<string> flagIconName = new List<string>();
            WABSeleniumActions.NetworkTraceGetFlagName(driver, flagIconName);
 
            // Zoom in to map

            WABSeleniumActions.ClickOnZoonIn(driver, 1);

            // Click on map
            WABSeleniumActions.ClickOnMap(driver);

            // Click on Run or Cancel button for Network Trace
            WABSeleniumActions.NetworkTraceClickRunOrClear(driver);

            //AppCommonUtility.WaitToLoadElement(driver, 30);

            //  Selenium driver switch
            //AppCommonUtility.SwitchIFrame(driver);
            Thread.Sleep(10000);

            
            ApplicationSnapShot.GetApplicationSnapShot(driver, ClientOperatingSystem.Windows7, ClientBrowser.Firefox, "NT_Phase2_" + context.DataRow["Index"].ToString());

            List<string> labelActualWidget = new List<string>();
            List<string> labelActual = new List<string>();

            List<string> tooltipActual = new List<string>();
            List<string> tooltipActualWidget = new List<string>();

            labelActual.Add(context.DataRow["labelTextIsolatingValves"].ToString());
            labelActual.Add(context.DataRow["labelTextIsolatedHydrants"].ToString());
            labelActual.Add(context.DataRow["labelTextIsolatedCustomers"].ToString());
            WABSeleniumActions.NetworkTraceGetOutputNames(driver, labelActualWidget);

            tooltipActual.Add(context.DataRow["tooltipTextInputLocationForStart"].ToString());
            tooltipActual.Add(context.DataRow["tooltipTextBlockLocation"].ToString());
            tooltipActual.Add(context.DataRow["tooltipTextSkipFeature"].ToString());
            WABSeleniumActions.NetworkTraceGetInputTooltipNames(driver, tooltipActualWidget);


            bool checkValidOutputLabelWidget = ValidateResult.AssertForAttributesWidget(labelActualWidget, labelActual);
            bool checkValidInputTooltipWidget = ValidateResult.AssertForAttributesWidget(tooltipActualWidget, tooltipActual);

            try
            {
                //  Get Config JSON URL
                string configJsonUrl = "http://www.arcgis.com/sharing/rest/content/items/" + applicationNumber + "/configs/NetworkTrace/config_NetworkTrace.json";

                //  Open config URL
                driver.Navigate().GoToUrl(configJsonUrl);
                driver.SwitchTo().Alert().Accept();
            }
            catch (System.Exception)
            {

            }

            Thread.Sleep(3000);

            List<string> typeExpected = new List<string>();
            List<string> typeActual = new List<string>();

            List<string> tooltipExpected = new List<string>();

            List<string> labelExpected = new List<string>();

            List<string> outputTooltipExpected = new List<string>();                            
            List<string> outputTooltipActual = new List<string>();

            List<string> displayName = new List<string>();

            NameValueCollection fieldMapActualOverviewExpected = new NameValueCollection();
           
           // string unitActual = null;
            List<string> OverviewStringsExpected = new List<string>();
            List<bool> OverviewBooleanExpected = new List<bool>();

            NameValueCollection fieldMapActualOverviewActual = new NameValueCollection();
            List<string> OverviewStringsActual = new List<string>();
            List<bool> OverviewBooleanActual = new List<bool>();
            List<string> OtherPanelActual = new List<string>();
            List<string> OtherPanelExpected= new List<string>();

            // Collection for Output Types Actual
           NameValueCollection outputSkippableActual = new NameValueCollection();
            NameValueCollection outputExporttoCSVActual = new NameValueCollection();
            NameValueCollection outputSaveToLayerActual = new NameValueCollection();
            NameValueCollection outputOutputLabelTextActual = new NameValueCollection();
            NameValueCollection outputTooltipTextActual = new NameValueCollection();
            NameValueCollection outputSummaryTextActual = new NameValueCollection();
            NameValueCollection outputDisplayTextActual = new NameValueCollection();
            NameValueCollection outputMinScaleActual = new NameValueCollection();
            NameValueCollection outputMaxScaleActual = new NameValueCollection();
             NameValueCollection outputSkipableFieldActual = new NameValueCollection();

            // Collection for Output Types Expected 
            NameValueCollection outputSkippableExpected =  new NameValueCollection();
            NameValueCollection outputExporttoCSVExpected = new NameValueCollection();
            NameValueCollection outputSaveToLayerExpected = new NameValueCollection();
            NameValueCollection outputOutputLabelTextExpected = new NameValueCollection();
            NameValueCollection outputTooltipTextExpected = new NameValueCollection();
            NameValueCollection outputSummaryTextExpected = new NameValueCollection();
            NameValueCollection outputDisplayTextExpected = new NameValueCollection();
            NameValueCollection outputMinScaleExpected = new NameValueCollection();
            NameValueCollection outputMaxScaleExpected = new NameValueCollection();
             NameValueCollection outputSkipableFieldExpected = new NameValueCollection();


            //getting actual values for Skippable in output
            outputSkippableActual.Add(context.DataRow["ChkSkippableIsolatingValves"].ToString(), "Isolating_Valves");
            outputSkippableActual.Add(context.DataRow["ChkSkippableIsolatedHydrants"].ToString(),"Isolated_Hydrants");
            outputSkippableActual.Add(context.DataRow["ChkSkippableIsolatedCustomers"].ToString(),"Isolated_Customers");

            // getting EXPORT to CSV
            outputExporttoCSVActual.Add("Isolating_Valves", context.DataRow["ChkExportToCsvIsolatingValves"].ToString().ToLower());
            outputExporttoCSVActual.Add("Isolated_Hydrants", context.DataRow["ChkExportToCsvIsolatedHydrants"].ToString().ToLower());
            outputExporttoCSVActual.Add("Isolated_Customers", context.DataRow["ChkExportToCsvIsolatedCustomers"].ToString().ToLower());

            //getting Save To Layer 
            outputSaveToLayerActual.Add("Isolating_Valves", context.DataRow["ChkSaveToLayerIsolatingValves"].ToString().ToLower());
            outputSaveToLayerActual.Add("Isolated_Hydrants", context.DataRow["ChkSaveToLayerIsolatedHydrants"].ToString().ToLower());
            outputSaveToLayerActual.Add("Isolated_Customers", context.DataRow["ChkSaveToLayerIsolatedCustomers"].ToString().ToLower());

            //getting LAbel for Output 
            outputOutputLabelTextActual.Add("Isolating_Valves", context.DataRow["LabelTextIsolatingValves"].ToString());
            outputOutputLabelTextActual.Add("Isolated_Hydrants", context.DataRow["LabelTextIsolatedHydrants"].ToString());
            outputOutputLabelTextActual.Add("Isolated_Customers", context.DataRow["LabelTextIsolatedCustomers"].ToString());

            // getting LAbel for outputTooltip Text
            outputTooltipTextActual.Add("Isolating_Valves", context.DataRow["TooltipTextIsolatingValves"].ToString());
            outputTooltipTextActual.Add("Isolated_Hydrants", context.DataRow["TooltipTextIsolatedHydrants"].ToString());
            outputTooltipTextActual.Add("Isolated_Customers", context.DataRow["TooltipTextIsolatedCustomers"].ToString());

            // getting output Summary Text
            outputSummaryTextActual.Add("Isolating_Valves", context.DataRow["SummaryTextIsolatingValves"].ToString());
            outputSummaryTextActual.Add("Isolated_Hydrants", context.DataRow["SummaryTextIsolatedHydrants"].ToString());
            outputSummaryTextActual.Add("Isolated_Customers", context.DataRow["SummaryTextIsolatedCustomers"].ToString());

            // getting output Display Text
            outputDisplayTextActual.Add("Isolating_Valves", context.DataRow["DisplayTextIsolatingValves"].ToString());
            outputDisplayTextActual.Add("Isolated_Hydrants", context.DataRow["DisplayTextIsolatedHydrants"].ToString());
            outputDisplayTextActual.Add("Isolated_Customers", context.DataRow["DisplayTextIsolatedCustomers"].ToString());

            // getting output Min Scale Text
            outputMinScaleActual.Add("Isolating_Valves", context.DataRow["MinScaleTextIsolatingValves"].ToString());
            outputMinScaleActual.Add("Isolated_Hydrants", context.DataRow["MinScaleIsolatedHydrants"].ToString());
            outputMinScaleActual.Add("Isolated_Customers", context.DataRow["MinScaleTextIsolatedCustomers"].ToString());

            // getting output Max scale  Text
            outputMaxScaleActual.Add("Isolating_Valves", context.DataRow["MaxScaleIsolatingValves"].ToString());
            outputMaxScaleActual.Add("Isolated_Hydrants", context.DataRow["MaxScaleIsolatedHydrants"].ToString());
            outputMaxScaleActual.Add("Isolated_Customers", context.DataRow["MaxScaleIsolatedCustomers"].ToString());

            // Getting Skipable Fields Added
            if (Convert.ToBoolean( context.DataRow["ChkSkippableIsolatingValves"].ToString()))
            {
                outputSkipableFieldActual.Add("Isolating_Valves", context.DataRow["DDLUniqueIdFieldIsolatingValves"].ToString());
            }
            if (Convert.ToBoolean(context.DataRow["ChkSkippableIsolatedHydrants"].ToString()))
            {
                outputSkipableFieldActual.Add("Isolated_Hydrants", context.DataRow["DDLUniqueIdFieldIsolatedHydrant"].ToString());
            }
            if (Convert.ToBoolean(context.DataRow["ChkSkippableIsolatedCustomers"].ToString()))
            {
                outputSkipableFieldActual.Add("Isolated_Customers", context.DataRow["DDLTypeUniqueIdFieldIsolatedCustomers"].ToString());
            }
           
           
            
            

            //getting actual values of input type
            typeActual.Add(context.DataRow["ddlTypeTextInputLocationForStart"].ToString());
            typeActual.Add(context.DataRow["ddlTypeTextBlockLocation"].ToString());
            typeActual.Add(context.DataRow["ddlTypeTextSkipFeature"].ToString());
            
            //getting actual values of overview Field map
            fieldMapActualOverviewExpected.Add("Isolating_Valves", context.DataRow["ddlTextIsolatingValvesOverview"].ToString());
            fieldMapActualOverviewExpected.Add("Isolated_Hydrants", context.DataRow["ddlTextIsolatedHydrantsOverview"].ToString());
            fieldMapActualOverviewExpected.Add("Isolated_Customers", context.DataRow["ddlTextIsolatedCustomersOverview"].ToString());

            //getting actual values of output tooltip
            outputTooltipActual.Add(context.DataRow["tooltipTextIsolatingValves"].ToString());
            outputTooltipActual.Add(context.DataRow["tooltipTextIsolatedHydrants"].ToString());
            outputTooltipActual.Add(context.DataRow["tooltipTextIsolatedCustomers"].ToString());
            
            //getting actual values for Overview MinScale, maxscale, buffer distance and boolean variables
            OverviewStringsExpected.Add(context.DataRow["bufferDistanceTextOverview"].ToString());
            OverviewStringsExpected.Add(context.DataRow["minScaleTextOverview"].ToString());
            OverviewStringsExpected.Add(context.DataRow["maxScaleTextOverview"].ToString());

            OverviewBooleanExpected.Add(Convert.ToBoolean(context.DataRow["chkVisibleOverview"].ToString()));
            //OverviewBooleanExpected.Add(Convert.ToBoolean(context.DataRow["chkSaveToLayerOverView"]));
            
            // Getting Other Panel Values Added
            OtherPanelExpected.Add(context.DataRow["iamgeHeightTextOther"].ToString());
            OtherPanelExpected.Add(context.DataRow["imageWidthTextOther"].ToString());
            OtherPanelExpected.Add(context.DataRow["timeoutTextOther"].ToString());
            OtherPanelExpected.Add(context.DataRow["displayTextOther"].ToString());
            // getting Overview values 
            WABValidation.GetValuesFromInput(AppCommonUtility.GetBodyText(driver), typeExpected, tooltipExpected, displayName);

          //  bool checkValidType = ValidateResult.AssertForAttributesWidget(typeActual, typeExpected);
            bool checkValidToolTip = ValidateResult.AssertForAttributesWidget(tooltipActual, tooltipExpected);
            bool checkDisplayName = ValidateResult.AssertForAttributesWidget(flagIconName, displayName);
           
            WABValidation.GetValuesFromOutputWidget(AppCommonUtility.GetBodyText(driver), labelExpected, outputTooltipExpected);
            WABValidation.GetValuesFromOutput(AppCommonUtility.GetBodyText(driver), outputSkippableExpected, outputExporttoCSVExpected, outputSaveToLayerExpected, outputOutputLabelTextExpected, outputTooltipTextExpected, outputSummaryTextExpected, outputDisplayTextExpected, outputMinScaleExpected, outputMaxScaleExpected, outputSkipableFieldExpected);

            bool checkOutputPanelLabel = ValidateResult.CheckAttributesInNameValueCollection(outputOutputLabelTextActual, outputOutputLabelTextExpected);
            bool checkOutputPanelTootip = ValidateResult.CheckAttributesInNameValueCollection(outputTooltipTextActual, outputTooltipTextExpected);
            bool checkOutputPanelSkipable = ValidateResult.CheckAttributesInNameValueCollection(outputSkippableActual, outputSkippableExpected);
            bool checkOutputPanelExportToCSV = ValidateResult.CheckAttributesInNameValueCollection(outputExporttoCSVActual, outputExporttoCSVExpected);
            //bool checkOutputPanelSaveToLayer = ValidateResult.CheckAttributesInNameValueCollection(outputSaveToLayerActual, outputSaveToLayerExpected);
            bool checkOutputPanelSummaryText = ValidateResult.CheckAttributesInNameValueCollection(outputSummaryTextActual, outputSummaryTextExpected);
            bool checkOutputPanelDisplayText = ValidateResult.CheckAttributesInNameValueCollection(outputDisplayTextActual, outputDisplayTextExpected);
            bool checkOutputPanelMinScale = ValidateResult.CheckAttributesInNameValueCollection(outputMinScaleActual, outputMinScaleExpected);
            bool checkOutputPanelMaxScale = ValidateResult.CheckAttributesInNameValueCollection(outputMaxScaleActual, outputMaxScaleExpected);
            bool checkOutputPanelSkipableFields = ValidateResult.CheckAttributesInNameValueCollection(outputSkipableFieldActual, outputSkipableFieldExpected);

            WABValidation.GetValuesFromOverview(AppCommonUtility.GetBodyText(driver), OverviewStringsActual, OverviewBooleanActual, fieldMapActualOverviewActual);
            
            WABValidation.GetValuesFromOther(AppCommonUtility.GetBodyText(driver), OtherPanelActual);
            
            bool checkValidOutputLabel = ValidateResult.AssertForAttributesWidget(labelActual, labelExpected);
            bool checkValidOutputTooltip = ValidateResult.AssertForAttributesWidget(outputTooltipActual, outputTooltipExpected);

            bool checkOverViewAttributesStrings = ValidateResult.CheckAttributesInList(OverviewStringsActual, OverviewStringsExpected);
            bool checkOverViewAttributesBoolean = ValidateResult.CheckAttributesInBooleanList(OverviewBooleanActual,OverviewBooleanExpected);

            bool checkOverViewAttributesFieldMap = ValidateResult.CheckAttributesInNameValueCollection(fieldMapActualOverviewActual, fieldMapActualOverviewExpected);
            bool checkOtherpanelAttributes = ValidateResult.CheckAttributesInList(OtherPanelActual, OtherPanelExpected);
            
            if (checkValidToolTip && true)
            {
                CreateLog.CreateApplicationLog(applicationNumber, "Values  matches with the config file");
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Values not matches with the config file");
            }

            if (checkValidOutputLabel && checkValidOutputTooltip)
            {
                CreateLog.CreateApplicationLog(applicationNumber, "Config Values  matches with the application tags");
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Config Values not matches with the application tags");
            }
            if (checkOverViewAttributesStrings && checkOverViewAttributesBoolean && checkOverViewAttributesFieldMap)
            {
                CreateLog.CreateApplicationLog(applicationNumber, "Values  matches with the config file for Overview Panel");
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Values not matches with the config file");
            }
            if (checkOtherpanelAttributes)
            {
                CreateLog.CreateApplicationLog(applicationNumber, "Values  matches with the config file for Other Panel");
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Values not matches with the config file");
            }

            if (checkValidOutputLabelWidget && checkValidInputTooltipWidget)
            {
                CreateLog.CreateApplicationLog(applicationNumber, "Widget Values  matches with the config file");
                Assert.IsTrue(true);
            }
            else
            {
                CreateLog.CreateApplicationLog(applicationNumber, "Widget Values are not matched with the config file");
                Assert.Fail("Widget Values are not matched with the config file");
            }

            if (checkOutputPanelLabel && checkOutputPanelTootip && checkOutputPanelSkipable && checkOutputPanelExportToCSV && checkOutputPanelSummaryText && checkOutputPanelDisplayText && checkOutputPanelMinScale && checkOutputPanelMaxScale && checkOutputPanelSkipableFields)
            {
                CreateLog.CreateApplicationLog(applicationNumber, "OUTPUT Widget Values  matches with the config file");
                Assert.IsTrue(true);
            }
            else
            {
                CreateLog.CreateApplicationLog(applicationNumber, "Widget Values are not matched with the config file");
                Assert.Fail("Widget Values are not matched with the config file");
            }
        }

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }
    }
}
