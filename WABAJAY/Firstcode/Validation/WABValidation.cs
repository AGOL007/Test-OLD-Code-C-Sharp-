using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TestBase.Validation;

namespace WebAppFramework.Validation
{
    public static class WABValidation
    {
        public static void GetIdFromLayers(string json)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(JsonToXmlConversion.JsonToXml(json));

            XmlNodeList nodeList = xmldoc.GetElementsByTagName("operationalLayers");
        }
        public static NameValueCollection GetIdFromWebmap(IWebDriver driver, string  json, NameValueCollection layerName)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(JsonToXmlConversion.JsonToXml(json));

            XmlNodeList nodeList = xmldoc.GetElementsByTagName("operationalLayers");
            
            foreach (XmlNode node in nodeList)
            {
                if (node["layerType"].InnerText == "ArcGISFeatureLayer")
                {
                    string nodeurl = node["url"].InnerText + "?f=pjson";
                    IWebElement body1 = driver.FindElement(By.TagName("body"));
                    string LayerJson = body1.Text;
                    body1.SendKeys(Keys.Control + 't');
                    driver.Navigate().GoToUrl(nodeurl);
                    XmlDocument xmldoclayer = new XmlDocument();
                    xmldoclayer.LoadXml(JsonToXmlConversion.JsonToXml(json));
                    XmlNodeList nodeList1 = xmldoclayer.GetElementsByTagName("capabilities");
                //layerName.Add(node["title"].InnerText, node["layerType"].InnerText);
                }
            }
            return layerName;
            
        }

        public static void GetValuesFromInput(string json, List<string> type, List<string> tooltip, List<string> displayName = null)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(JsonToXmlConversion.JsonToXml(json));

            XmlNodeList nodeList = xmldoc.GetElementsByTagName("inputs");
            foreach (XmlNode node in nodeList)
            {
                type.Add(node["type"].InnerText);
                tooltip.Add(node["toolTip"].InnerText);  
                displayName.Add(node["displayName"].InnerText);
            }
        }
        public static void GetValuesFromOutputWidget(string json, List<string> label, List<string> tooltip)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(JsonToXmlConversion.JsonToXml(json));

            XmlNodeList nodeList = xmldoc.GetElementsByTagName("outputs");
            foreach (XmlNode node in nodeList)
            {
                label.Add(node["panelText"].InnerText);
                tooltip.Add(node["toolTip"].InnerText);
              
            }
        }

        public static void GetValuesFromOutput(string json, NameValueCollection keyPairSkipable, NameValueCollection keyPairExportToCsv, NameValueCollection keyPairSaveToLayer, NameValueCollection keyPairOutputLabelText, NameValueCollection keyPairTooltipText, NameValueCollection keyPairSummaryText, NameValueCollection keyPairDisplayText, NameValueCollection keyPairMinScale, NameValueCollection keyPairMaxScale, NameValueCollection keyPairSkipableField)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(JsonToXmlConversion.JsonToXml(json));

            XmlNodeList nodeList = xmldoc.GetElementsByTagName("outputs");
            //int outputCount = nodeList.Count;
            //for (int i = 0; i < outputCount; i++)
            //{
                foreach (XmlNode node in nodeList)
                {
                    
                    keyPairSkipable.Add((node["bypassDetails"].ChildNodes[0].InnerText), node["paramName"].InnerText);
                    //keyPairExportToCsv.Add(node["paramName"].InnerText, node["skipable"].InnerText);
                    keyPairExportToCsv.Add(node["paramName"].InnerText, node["exportToCSV"].InnerText);
                    keyPairSaveToLayer.Add(node["paramName"].InnerText, node["saveToLayer"].InnerText);
                    keyPairOutputLabelText.Add(node["paramName"].InnerText, node["panelText"].InnerText);
                    keyPairTooltipText.Add(node["paramName"].InnerText, node["toolTip"].InnerText);
                    keyPairSummaryText.Add(node["paramName"].InnerText, node["summaryText"].InnerText);
                    keyPairDisplayText.Add(node["paramName"].InnerText, node["displayText"].InnerText);
                    keyPairMinScale.Add(node["paramName"].InnerText, node["MinScale"].InnerText);
                    keyPairMaxScale.Add(node["paramName"].InnerText, node["MaxScale"].InnerText);
                    if (Convert.ToBoolean(node["bypassDetails"].ChildNodes[0].InnerText))
                    {
                        keyPairSkipableField.Add(node["paramName"].InnerText, (node["bypassDetails"].ChildNodes[1].InnerText));
                    }
                }
                
            
        //}
            
        }
        public static void GetValuesFromOverview(string json, List<string> overviewStrings,  List<bool> overviewBoolean, NameValueCollection keyPairOverviewFieldMap)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(JsonToXmlConversion.JsonToXml(json));
            
            XmlNodeList nodeList = xmldoc.GetElementsByTagName("overview");
           
            foreach (XmlNode node in nodeList)
            {
                
                overviewStrings.Add(node["BufferDistance"].InnerText);
                overviewStrings.Add(node["MinScale"].InnerText);
                overviewStrings.Add(node["MaxScale"].InnerText);
                //unitOverview = node["Unit"].InnerText;
                overviewBoolean.Add(Convert.ToBoolean(node["visibility"].InnerText));
                 //overviewBoolean.Add(Convert.ToBoolean(node["saveToLayer"].InnerText));
            }
            XmlNodeList nodeListFieldmap = xmldoc.GetElementsByTagName("fieldMap");
            foreach (XmlNode node in nodeListFieldmap)
            {
                keyPairOverviewFieldMap.Add( node["paramName"].InnerText, node["fieldName"].InnerText);
               
            }
        }
        public static void GetValuesFromOther(string json, List<string> otherField)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(JsonToXmlConversion.JsonToXml(json));
            XmlNodeList otherList = xmldoc.GetElementsByTagName("highlighterDetails");
            foreach (XmlNode node in otherList)
            {
               otherField.Add(node["height"].InnerText);
               otherField.Add(node["width"].InnerText);
              otherField.Add(node["timeout"].InnerText);
            }
            XmlNodeList displayRunText = xmldoc.GetElementsByTagName("displayTextForRunButton");
            foreach (XmlNode node in displayRunText)
            {
                otherField.Add(node.InnerText);
            }

        }
        
    }
}
