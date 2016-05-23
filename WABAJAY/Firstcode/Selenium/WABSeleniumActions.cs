using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestBaseUtility;

namespace WebAppCommonSeleniumActions
{
    public static class WABSeleniumActions
    {

        #region Home
        public static void ClickContinueBtn(IWebDriver driver)
        {
            AppCommonUtility.FindElementDynamically(driver, "ContinueBtn");

            //  Click on continue button 
            IWebElement continueBtn = driver.FindElement(By.ClassName("ContinueBtn"));
            continueBtn.Click();
        }

        public static void EnterUserCredential(IWebDriver driver, string userName, string password)
        {
            AppCommonUtility.FindElementDynamically(driver, "user_username", "Id");
            IWebElement agolUserName = driver.FindElement(By.Id("user_username"));
            agolUserName.SendKeys(userName);

            IWebElement agolPassword = driver.FindElement(By.Id("user_password"));
            agolPassword.SendKeys(password);

            IWebElement signInBtn = driver.FindElement(By.Id("signIn"));
            signInBtn.Click();

        }
        #endregion

        #region Create App

        public static void NetworkTraceCreateNewApp(IWebDriver driver)
        {
            AppCommonUtility.FindElementDynamically(driver, "create-button");

            //  Click on continue button 
            IWebElement createButton = driver.FindElement(By.ClassName("create-button"));
            createButton.Click();
        }
        public static void NextButtonClickCreateAppPanel(IWebDriver driver)
        {
            AppCommonUtility.FindElementDynamically(driver, "button-container");
            IWebElement nextBtnDiv = driver.FindElement(By.ClassName("button-container"));
            IList<IWebElement> btnList = nextBtnDiv.FindElements(By.ClassName("jimu-float-trailing"));
            AppCommonUtility.ListClickActionByName(btnList,"Next");
            

        }

        public static void FillDetailsCreateNewApp(IWebDriver driver, TestContext context)
        {
            AppCommonUtility.FindElementDynamically(driver, "jimu-popup");

            IWebElement appTitle = driver.FindElement(By.ClassName("dijitInputInner"));
            appTitle.SendKeys(context.DataRow["appData"].ToString() + "_" + DateTime.Now);

            IWebElement appDescription = driver.FindElement(By.ClassName("dijitTextArea"));
            appDescription.SendKeys(context.DataRow["appDescription"].ToString());

            CreateNewClickOKCancel(driver, context);

        }

        private static void CreateNewClickOKCancel(IWebDriver driver, TestContext context)
        {
            // Need to insert condition in Excel
            bool check = Convert.ToBoolean(context.DataRow["checkBtn"].ToString());

            IWebElement buttonContainer = driver.FindElement(By.ClassName("button-container"));
            IList<IWebElement> buttonCollection = buttonContainer.FindElements(By.TagName("div"));

            if (check)
            {
                AppCommonUtility.ListClickActionByName(buttonCollection, "OK");
            }
            else
            {
                AppCommonUtility.ListClickActionByName(buttonCollection, "Cancel");
            }
        }

        public static string GetAppNumberFromURL(IWebDriver driver)
        {
            string[] splitStringArray = driver.Url.Split('=');
            return splitStringArray[1];
        }

        #endregion

        #region Left Panl
        public static void ClickMenuOption(IWebDriver driver, TestContext context)
        {
            AppCommonUtility.FindElementDynamically(driver, "config-section");

            IWebElement controlTable = driver.FindElement(By.ClassName("config-section"));
            IList<IWebElement> menuOptions = controlTable.FindElements(By.ClassName("tab-item-title"));

            //  Need to insert Data in excel
            AppCommonUtility.ListClickActionByName(menuOptions, context.DataRow["menuOption"].ToString());
        }
        #endregion
               
        #region Choose web Map
        public static void ClickChooseMapButton(IWebDriver driver)
        {
            Thread.Sleep(9000);
            AppCommonUtility.FindElementDynamically(driver, "choose-map-btn");
            AppCommonUtility.ClickDisplayedAndEnableElement(driver.FindElement(By.ClassName("choose-map-btn")));
            
        }

        public static void SearchForWebMap(IWebDriver driver, TestContext context)
        {
            AppCommonUtility.FindElementDynamically(driver, "jimu-input");
            IWebElement searchTextBox = driver.FindElement(By.ClassName("jimu-input"));
            //  Need to insert value in excel
           // searchTextBox.SendKeys(context.DataRow["searchForWebMap"].ToString());
            searchTextBox.SendKeys("automation007");
            IWebElement searchBtn = driver.FindElement(By.ClassName("search-btn"));
            searchBtn.Click();
        }

        public static void SelectMapByName(IWebDriver driver, TestContext context)
        {
            AppCommonUtility.FindElementDynamically(driver, "maps-table-div");

            //  Parent class 
            IList<IWebElement> mapDiv = driver.FindElements(By.ClassName("maps-table-div"));
            foreach (IWebElement item in mapDiv)
            {
                //  Flag
                int i = 0;
                if (item.Displayed)
                {
                    //  Map div
                    IList<IWebElement> mapItems = driver.FindElements(By.ClassName("map-item-div"));
                    foreach (IWebElement map in mapItems)
                    {
                        if (map.Displayed)
                        {
                            //  Name of web map
                            string mapName = map.FindElement(By.TagName("span")).Text;

                            //  Need to insert value in excel
                           // if (mapName == context.DataRow["mapName"].ToString())
                            if (mapName == "Early-Voting webmap AUTO1")
                            {
                                i++;

                                //  Click on map
                                IWebElement mapContent = map.FindElement(By.ClassName("jimu-auto-vertical"));
                                mapContent.Click();
                                break;
                            }
                        }
                    }

                    if (i > 0)
                    {
                        break;
                    }
                }
            }

            //  Click on OK button
            CreateNewClickOKCancel(driver, context);
            Thread.Sleep(9000);
        }
        #endregion

        #region Getting WebmapId Using Browser Console

        public static string GettingWebmapId(IWebDriver driver)
        {
             string Javascript = "return appConfig.map.itemId;";
             string webmapid = ((IJavaScriptExecutor)driver).ExecuteScript(Javascript).ToString();
             return webmapid;
        }
        #endregion
        
        
        #region Widget

        public static void ClickAddWidgetIcon(IWebDriver driver)
        {
            AppCommonUtility.FindElementDynamically(driver, "tab-item-title");
            IList<IWebElement> ele = driver.FindElements(By.ClassName("tab-item-title"));
            AppCommonUtility.ListClickActionByName(ele, "Widget");
            //IWebElement addIcon = driver.FindElement(By.ClassName("", "Widget"));
            //addIcon.Click();
        }
        public static void ClickSetWidgetController(IWebDriver driver)
        {
            IWebElement controllerPanel = driver.FindElement(By.ClassName("action-node"));
            controllerPanel.Click();
            IWebElement addWidgetNode = driver.FindElement(By.ClassName("add-widget-node"));
            AppCommonUtility.ClickDisplayedAndEnableElement(addWidgetNode);
        }
        public static void SelectWidgetFormList(IWebDriver driver, TestContext context)
        {
            

            IList<IWebElement> widgetList = driver.FindElements(By.ClassName("label"));

            AppCommonUtility.ListClickActionByName(widgetList, "Early Voting");

          
            //IWebElement widgetContainer = driver.FindElement(By.ClassName("widgetList"));
            //IList<IWebElement> widgetList1 = widgetContainer.FindElements(By.ClassName("label"));

            //AppCommonUtility.ListClickActionByName(widgetList1, context.DataRow["NetworkTrace"].ToString());

            //  Click on OK button
            CreateNewClickOKCancel(driver, context);
        }

        public static void ConfigureNetworkSetUrl(IWebDriver driver, TestContext context)
        {
            AppCommonUtility.FindElementDynamically(driver, "esriSetButton");

            IWebElement setButton = driver.FindElement(By.ClassName("esriSetButton"));
            setButton.Click();
            
        }

        public static void SetTaskSelectRadioButton(IWebDriver driver, TestContext context)
        {
            AppCommonUtility.FindElementDynamically(driver, "url-radio");

            //  Need to insert boolean value in the Excel
            if (Convert.ToBoolean(context.DataRow["checkRadioButton"].ToString()))
            {
                IWebElement addServiceUrl = driver.FindElement(By.ClassName("url-radio"));
                addServiceUrl.Click();
            }
        }

        public static void EnterGeoProcessingServiceUrl(IWebDriver driver, TestContext context)
        {
            AppCommonUtility.FindElementDynamically(driver, "jimu-url-input");

            IWebElement geoProcessingServiceUrl = driver.FindElement(By.ClassName("jimu-url-input"));
            IList<IWebElement> serviceTextBox = geoProcessingServiceUrl.FindElements(By.TagName("input"));

            //  Need to insert data column in Excel
            serviceTextBox[1].SendKeys(context.DataRow["gpTaskUrl"].ToString());
        }

        public static void GeoProcessingServiceUrlClickValidationBtn(IWebDriver driver)
        {
            //validate-btn
            AppCommonUtility.FindElementDynamically(driver, "validate-btn");

            IWebElement validateButton = driver.FindElement(By.ClassName("validate-btn"));
            AppCommonUtility.ClickDisplayedAndEnableElement(validateButton);
           
        }

        public static void ValidateGeoProcessingServiceUrlClickOK(IWebDriver driver, TestContext context)
        {
            Thread.Sleep(1000);
            AppCommonUtility.FindElementDynamically(driver, "operations");
            IWebElement popupOfWidgetSelect = driver.FindElement(By.ClassName("operations"));
            IList<IWebElement> OKBtnOftask = popupOfWidgetSelect.FindElements(By.ClassName("jimu-float-trailing"));
            AppCommonUtility.ListClickActionByName(OKBtnOftask, "OK");
            //try
            //{
            //    AppCommonUtility.FindElementDynamically(driver, "operations");
            //}
            //catch (Exception)
            //{
            //    // URL is not valid
            //}

            //  Click on OK button
           // CreateNewClickOKCancel(driver, context);
        }
        #endregion

        #region ConfigureNetworkTrace

        public static void FillDataInConfigureNetworkTraceInputOutput(IWebDriver driver, TestContext context)
        {
            AppCommonUtility.FindElementDynamically(driver, "esriCTTaskData");

            IWebElement esriCTTaskData = driver.FindElement(By.ClassName("esriCTTaskData"));
            IList<IWebElement> taskDataList = esriCTTaskData.FindElements(By.ClassName("dijitTitlePaneTitle"));

            foreach (IWebElement item in taskDataList)
            {
                switch (item.FindElement(By.ClassName("dijitTitlePaneTextNode")).Text)
                {
                    case "Input":
                        FillDataInTaskData(driver, context);
                        item.Click();
                        break;
                    case "Output":
                        item.Click();
                        FillDataOutputTaskPanel(driver, context);
                        item.Click();
                        break;
                    //case "Overview":
                    //    item.Click();
                    //    FillDataOverViewPanel(driver, Convert.ToBoolean(context.DataRow["chkVisibleOverview"].ToString()), context.DataRow["bufferDistanceTextOverview"].ToString(), context.DataRow["ddlTextUnitOverview"].ToString(), context.DataRow["minScaleTextOverview"].ToString(), context.DataRow["maxScaleTextOverview"].ToString(), Convert.ToBoolean(context.DataRow["chkSaveToLayerOverView"].ToString()), context.DataRow["ddlTextTargetLayerOverview"].ToString(), context.DataRow["ddlTextIsolatingValvesOverview"].ToString(), context.DataRow["ddlTextIsolatedHydrantsOverview"].ToString(), context.DataRow["ddlTextIsolatedCustomersOverview"].ToString());                                                
                    //    break;
                    //case "Other":
                    //    item.Click();
                    //    FillDataOtherPanel(driver, context.DataRow["iamgeHeightTextOther"].ToString(), context.DataRow["imageWidthTextOther"].ToString(), context.DataRow["timeoutTextOther"].ToString(), context.DataRow["displayTextOther"].ToString());
                    //    break;
                }
                
            }
            //CreateNewClickOKCancel(driver, context);
        }

        public static void FillDataInOverviewOther(IWebDriver driver, TestContext context)
        {
            IWebElement esriCTTaskData = driver.FindElement(By.ClassName("esriCTTaskData"));
            IList<IWebElement> taskDataList = esriCTTaskData.FindElements(By.TagName("div"));
            foreach (IWebElement item in taskDataList)
            {
                switch (item.Text)
                {
                    case "Overview":
                        item.Click();
                        FillDataOverViewPanel(driver, Convert.ToBoolean(context.DataRow["chkVisibleOverview"].ToString()), context.DataRow["bufferDistanceTextOverview"].ToString(), context.DataRow["ddlTextUnitOverview"].ToString(), context.DataRow["minScaleTextOverview"].ToString(), context.DataRow["maxScaleTextOverview"].ToString(), Convert.ToBoolean(context.DataRow["chkSaveToLayerOverView"].ToString()), context.DataRow["ddlTextTargetLayerOverview"].ToString(), context.DataRow["ddlTextIsolatingValvesOverview"].ToString(), context.DataRow["ddlTextIsolatedHydrantsOverview"].ToString(), context.DataRow["ddlTextIsolatedCustomersOverview"].ToString());
                        break;
                    case "Other":
                        item.Click();
                        FillDataOtherPanel(driver, context.DataRow["iamgeHeightTextOther"].ToString(), context.DataRow["imageWidthTextOther"].ToString(), context.DataRow["timeoutTextOther"].ToString(), context.DataRow["displayTextOther"].ToString());
                        break;
                }
            }

            Thread.Sleep(3000);
            CreateNewClickOKCancel(driver, context);
        }

        public static void FillDataInTaskData(IWebDriver driver, TestContext context)
        {
            // Input container
            IWebElement inputContainer = driver.FindElement(By.ClassName("dijitTitlePaneContentOuter"));
            IList<IWebElement> inputContainerOption = inputContainer.FindElements(By.ClassName("inputValue"));

            foreach (IWebElement item in inputContainerOption)
            {
                switch (item.Text)
                {
                    case "Name:Input locations for Start":
                        FillDataInputInPanel(driver, context.DataRow["tooltipTextInputLocationForStart"].ToString(), context.DataRow["ddlTypeTextInputLocationForStart"].ToString());
                        break;
                    case "Name:Block locations":
                        item.Click();
                        Thread.Sleep(1000);
                        FillDataInputInPanel(driver, context.DataRow["tooltipTextBlockLocation"].ToString(), context.DataRow["ddlTypeTextBlockLocation"].ToString());
                        break;
                    case "Name:Skip Features":
                        item.Click();
                        FillDataInputInPanel(driver, context.DataRow["tooltipTextSkipFeature"].ToString(), context.DataRow["ddlTypeTextSkipFeature"].ToString());
                        break;
                    
                }
            }
        }

        public static void FillDataOutputTaskPanel(IWebDriver driver, TestContext context)
        {
            IWebElement inputContainer = driver.FindElement(By.ClassName("outputContainer"));
            IList<IWebElement> inputContainerOption = inputContainer.FindElements(By.ClassName("inputValue"));

             foreach (IWebElement item in inputContainerOption)
            {
                switch (item.Text)
                {
                    case "Name:Isolating Valves":
                        item.Click();
                        FillDataOutputPanel(driver, context.DataRow["labelTextIsolatingValves"].ToString(), Convert.ToBoolean(context.DataRow["chkSkippableIsolatingValves"].ToString()), context.DataRow["ddlUniqueIdFieldIsolatingValves"].ToString(), context.DataRow["tooltipTextIsolatingValves"].ToString(), context.DataRow["summaryTextIsolatingValves"].ToString(), context.DataRow["displayTextIsolatingValves"].ToString(), context.DataRow["minScaleTextIsolatingValves"].ToString(), context.DataRow["maxScaleIsolatingValves"].ToString(), Convert.ToBoolean(context.DataRow["chkExportToCsvIsolatingValves"].ToString()), Convert.ToBoolean(context.DataRow["chkSaveToLayerIsolatingValves"].ToString()), context.DataRow["ddlTargetLayerIsolatingValves"].ToString());                        
                        break;
                    case "Name:Isolated Hydrants":
                        item.Click();
                        FillDataOutputPanel(driver, context.DataRow["labelTextIsolatedHydrants"].ToString(), Convert.ToBoolean(context.DataRow["chkSkippableIsolatedHydrants"].ToString()), context.DataRow["ddlUniqueIdFieldIsolatedHydrant"].ToString(), context.DataRow["tooltipTextIsolatedHydrants"].ToString(), context.DataRow["summaryTextIsolatedHydrants"].ToString(), context.DataRow["displayTextIsolatedHydrants"].ToString(), context.DataRow["minScaleIsolatedHydrants"].ToString(), context.DataRow["maxScaleIsolatedHydrants"].ToString(), Convert.ToBoolean(context.DataRow["chkExportToCsvIsolatedHydrants"].ToString()), Convert.ToBoolean(context.DataRow["chkSaveToLayerIsolatedHydrants"].ToString()), context.DataRow["ddlTargetLayerIsolatedHydrants"].ToString());                      
                        break;
                    case "Name:Isolated Customers":
                        item.Click();
                        FillDataOutputPanel(driver, context.DataRow["labelTextIsolatedCustomers"].ToString(), Convert.ToBoolean(context.DataRow["chkSkippableIsolatedCustomers"].ToString()), context.DataRow["ddlTypeUniqueIdFieldIsolatedCustomers"].ToString(), context.DataRow["tooltipTextIsolatedCustomers"].ToString(), context.DataRow["summaryTextIsolatedCustomers"].ToString(), context.DataRow["displayTextIsolatedCustomers"].ToString(), context.DataRow["minScaleTextIsolatedCustomers"].ToString(), context.DataRow["maxScaleIsolatedCustomers"].ToString(), Convert.ToBoolean(context.DataRow["chkExportToCsvIsolatedCustomers"].ToString()), Convert.ToBoolean(context.DataRow["chkSaveToLayerIsolatedCustomers"].ToString()), context.DataRow["ddlTextTargetLayerIsolatedCustomers"].ToString());                       
                        break;
                }
            }
        }
        
        private static void FillDataInputInPanel(IWebDriver driver, string fillDataToolTip, string ddlOptionName)
        {
            IList<IWebElement> inputContainer = driver.FindElements(By.ClassName("field"));
            AppCommonUtility.FillDataTextBoxUsingLabelName(inputContainer, "Tooltip", fillDataToolTip);

            DropDownSelect(driver, "Type", ddlOptionName);

            AppCommonUtility.FindElementDynamically(driver, "symbol-td-item");
            IWebElement iconPanel = driver.FindElement(By.ClassName("symbol-td-item"));
            AppCommonUtility.ClickAndSelectFeature(iconPanel);
        }

        private static void FillDataOutputPanel(IWebDriver driver, string fillDataLabel, bool skippableCheck, string ddlOptionName, string fillDataTooltip, string fillDataSummaryText, string fillDataDisplayText, string fillDataMinScale, string fillDataMaxScale, bool exportToCsvCheck, bool saveToLayerCheck, string ddlOptionNameTargetLayer)
        {
            IList<IWebElement> outputContainer = driver.FindElements(By.ClassName("field"));
            AppCommonUtility.FillDataTextBoxUsingLabelName(outputContainer, "Label", fillDataLabel);

            if (skippableCheck)
            {
                AppCommonUtility.ClickCheckBoxUsingLabelName(outputContainer, "Skippable");
                Thread.Sleep(200);

                DropDownSelect(driver, "Unique ID field", ddlOptionName);
            }

            AppCommonUtility.FillDataTextBoxUsingLabelName(outputContainer, "Tooltip", fillDataTooltip);

            AppCommonUtility.FillDataTextBoxUsingLabelName(outputContainer, "Summary text", fillDataSummaryText);

            AppCommonUtility.FillDataTextBoxUsingLabelName(outputContainer, "Display text", fillDataDisplayText);

            AppCommonUtility.FillDataTextBoxUsingLabelName(outputContainer, "Min scale", fillDataMinScale);

            AppCommonUtility.FillDataTextBoxUsingLabelName(outputContainer, "Max scale", fillDataMaxScale);

            if (exportToCsvCheck)
            {
                AppCommonUtility.ClickCheckBoxUsingLabelName(outputContainer, "Export to CSV");                          
            }

            if (saveToLayerCheck)
            {
                AppCommonUtility.ClickCheckBoxUsingLabelName(outputContainer, "Save to layer");
                Thread.Sleep(200);

                DropDownSelect(driver, "Target layer", ddlOptionNameTargetLayer);
            }

            IWebElement iconPanel = driver.FindElement(By.ClassName("symbol-td-item"));
            AppCommonUtility.ClickAndSelectFeature(iconPanel);
        }

        private static void FillDataOverViewPanel(IWebDriver driver, bool visibleCheck, string fillDataBufferDistance, string ddlOptionName, string fillDataMinScale, string fillDataMaxScale, bool saveToLayerCheck, string ddlOptionNameTargetLayer, string ddlOptionNameIsolatingValve, string ddlOptionNameIsolatedHydrants, string ddlOptionNameIsolatedCustomers)
        {
            IList<IWebElement> overviewContainer = driver.FindElements(By.ClassName("field"));
            if (visibleCheck)
            {
                AppCommonUtility.ClickCheckBoxUsingLabelName(overviewContainer, "Visible");
            }

            AppCommonUtility.FillDataTextBoxUsingLabelName(overviewContainer, "Buffer distance", fillDataBufferDistance);

            DropDownSelect(driver, "Unit", ddlOptionName);

            AppCommonUtility.FillDataTextBoxUsingLabelName(overviewContainer, "Min scale", fillDataMinScale);

            AppCommonUtility.FillDataTextBoxUsingLabelName(overviewContainer, "Max scale", fillDataMaxScale);

            if (saveToLayerCheck)
            {
                AppCommonUtility.ClickCheckBoxUsingLabelName(overviewContainer, "Save to layer");
                Thread.Sleep(200);

                DropDownSelect(driver, "Target layer", ddlOptionNameTargetLayer);

                DropDownSelectOverview(driver, "Isolating_Valves", ddlOptionNameIsolatingValve);

                DropDownSelectOverview(driver, "Isolated_Hydrants", ddlOptionNameIsolatedHydrants);

                DropDownSelectOverview(driver, "Isolated_Customers", ddlOptionNameIsolatedCustomers);
            }                              
        }

        private static void FillDataOtherPanel(IWebDriver driver, string fillDataImageHeight, string fillDataImageWidth, string fillDataTimeout, string fillDataDisplayText)
        {
            IList<IWebElement> otherContainer = driver.FindElements(By.ClassName("field"));

            AppCommonUtility.FillDataTextBoxUsingLabelName(otherContainer, "Image height", fillDataImageHeight);

            AppCommonUtility.FillDataTextBoxUsingLabelName(otherContainer, "Image width", fillDataImageWidth);

            AppCommonUtility.FillDataTextBoxUsingLabelName(otherContainer, "Timeout", fillDataTimeout);

            AppCommonUtility.FillDataTextBoxUsingLabelName(otherContainer, "Display text", fillDataDisplayText);
        }

        public static void ClickConfigSaveButton(IWebDriver driver)
        {
            AppCommonUtility.FindElementDynamically(driver, "save-enable");

            IWebElement SaveButton = driver.FindElement(By.ClassName("save-enable"));
            SaveButton.Click();
        }
        #endregion

        #region Select drop down using Lable name
        public static void DropDownSelect(IWebDriver driver, string labelName, string optionName, string className = "dijitArrowButtonInner")
        {
            IList<IWebElement> dropDownValue = driver.FindElements(By.ClassName("field"));
            foreach (IWebElement item in dropDownValue)
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
                    int flag = 0;
                    
                    //  Click on arrow button 
                    IWebElement arrowButton = item.FindElement(By.ClassName(className));
                    arrowButton.Click();
                    Thread.Sleep(1500);
                    IList<IWebElement> menuOptions = driver.FindElements(By.ClassName("dijitMenuItemLabel"));
                    foreach (IWebElement test in menuOptions)
                    {
                        if (test.Displayed && string.Equals(test.Text, optionName))
                        {
                            flag++;
                            test.Click();
                            break;
                        }
                    }
                    if (flag > 0)
                    {
                        break; 
                    }
                  
                }               
            }      
        }

        public static void DropDownSelectOverview(IWebDriver driver, string labelName, string optionName, string className = "dijitArrowButtonInner")
        {
            IList<IWebElement> dropDownValue = driver.FindElements(By.ClassName("esriCTOutageFieldParams"));
            foreach (IWebElement item in dropDownValue)
            {
                IWebElement labelContainer = null;
                try
                {
                    labelContainer = item.FindElement(By.ClassName("esriCTFieldName"));
                }
                catch (Exception)
                {
                    continue;
                }
                if (string.Equals(labelContainer.Text, labelName))
                {
                    int flag = 0;

                    //  Click on arrow button 
                    IWebElement arrowButton = item.FindElement(By.ClassName(className));
                    arrowButton.Click();
                    Thread.Sleep(1500);
                    IList<IWebElement> menuOptions = driver.FindElements(By.ClassName("dijitMenuItemLabel"));
                    foreach (IWebElement test in menuOptions)
                    {
                        if (test.Displayed && string.Equals(test.Text, optionName))
                        {
                            flag++;
                            test.Click();
                            break;
                        }
                    }
                    if (flag > 0)
                    {
                        break;
                    }

                }
            }
        }
        #endregion


        #region Widget Mode
        // Click on Network trace icon 
        public static void NetworkTraceIconClick(IWebDriver driver, string networkTrace = "NetworkTrace")
        {
            IWebElement iconNode = driver.FindElement(By.ClassName("container-section"));
            IList<IWebElement> widgetsIcon = iconNode.FindElements(By.ClassName("icon-node"));

            foreach (IWebElement item in widgetsIcon)
            {
                if (string.Equals(item.GetAttribute("title").ToString(), networkTrace))
                {
                    item.Click();
                    break;
                }
            }
        }

        //  Click on Flag Icon using name
        public static void NetworkTraceClickFlagIconUsingName(IWebDriver driver, string flagName)
        {
            AppCommonUtility.FindElementDynamically(driver, "traceControls");

            IWebElement DivControl = driver.FindElement(By.ClassName("traceControls"));
            IList<IWebElement> flagContainer = DivControl.FindElements(By.TagName("div"));
            int i = 0;

            foreach (IWebElement item in flagContainer)
            {
                if (string.Equals(item.Text, flagName))
                {
                    flagContainer[i + 1].Click();
                }
                i++;
            }
        }
        
        //  Get Icon Name
        public static void NetworkTraceGetFlagName(IWebDriver driver, List<string> iconNames)
        {
            AppCommonUtility.FindElementDynamically(driver, "traceControls");
            IWebElement DivControl = driver.FindElement(By.ClassName("traceControls"));
            IList<IWebElement> flagContainer = DivControl.FindElements(By.TagName("label"));

            foreach (IWebElement item in flagContainer)
            {
                iconNames.Add(item.Text);
            }
        }

        // Get Tootips for Input from Widget mode
        public static List<string> NetworkTraceGetInputTooltipNames(IWebDriver driver, List<string> tooltipNetWorkTraceWidget)
        {
            //jimu-widget-NetworkTrace
            IWebElement tooltipInputLocation = driver.FindElement(By.ClassName("flagbutton"));
            tooltipNetWorkTraceWidget.Add(tooltipInputLocation.GetAttribute("title").ToString());

            IWebElement tooltipBlockLocation = driver.FindElement(By.ClassName("barrierButton"));
            tooltipNetWorkTraceWidget.Add(tooltipBlockLocation.GetAttribute("title").ToString());

            return tooltipNetWorkTraceWidget;
        }
        // Get Output Name from application
        public static List<string> NetworkTraceGetOutputNames(IWebDriver driver, List<string> outputNames)
        {
            AppCommonUtility.FindElementDynamically(driver, "resultPanelContainer");
            Thread.Sleep(5000);
            IList<IWebElement> outputpanelLabel = driver.FindElements(By.ClassName("dijitTitlePaneTitleFocus"));
            foreach (var item in outputpanelLabel)
            {
                
                string getTextOutputPanellabel = item.Text;
                string[] getSplitTextOutputPanel = getTextOutputPanellabel.Split('(');
                string getSplitTextOutputPanelIndex = getSplitTextOutputPanel[0];
                string getSplitTextOutputPanelTrimmed = getSplitTextOutputPanelIndex.TrimEnd();
                outputNames.Add(getSplitTextOutputPanelTrimmed);
            }
            outputNames.Remove(outputNames[0]);

            return outputNames;
        }


        // Get Output Tooltip from application
        public static void NetworkTraceGetOutputTooltips(IWebDriver driver, List<string> outputTooltips)
        {
            IList<IWebElement> tool = driver.FindElements(By.ClassName("dijitTitlePaneTitle"));
            foreach (var item in tool)
            {

                string outputTooltipContainer = item.GetAttribute("title").ToString();
                outputTooltips.Add(outputTooltipContainer);
            }
            outputTooltips.Remove(outputTooltips[0]);
        }

        public static void ClickOnZoonIn(IWebDriver driver, int clickCount)
        {
            AppCommonUtility.FindElementDynamically(driver, "esriSimpleSliderIncrementButton");

            for (int i = 0; i < clickCount; i++)
            {
                //esriSimpleSliderDecrementButton esriSimpleSliderIncrementButton
                IWebElement zoomIn = driver.FindElement(By.ClassName("esriSimpleSliderDecrementButton"));
                zoomIn.Click();
                Thread.Sleep(2000);
            }
        }

        // Click on map
        public static void ClickOnMap(IWebDriver driver) 
        {
            // map_WaterDistributionNetwork_9024
            IWebElement clickOnMap = driver.FindElement(By.Id("map_WaterDistributionNetwork_9024"));
            clickOnMap.Click();
            Thread.Sleep(2000);
        }



        // Click on RunOrClear Button
        public static void NetworkTraceClickRunOrClear(IWebDriver driver, bool flag = true)
        {
            IWebElement networkTraceContainer = driver.FindElement(By.ClassName("traceButtons"));
            IList<IWebElement> btnContainerNetworkTrace = networkTraceContainer.FindElements(By.ClassName("jimu-btn")); 
            if (flag)
            {
                btnContainerNetworkTrace[0].Click();
            }
            else
            {
                btnContainerNetworkTrace[1].Click();
            }
        }
        #endregion

    }
}
