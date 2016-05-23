using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDataSource
{
    public static class WabDataSource
    {
        public static void DataSource(TestContext context, NameValueCollection wabData)
        {
            
            //Data Source for Input Location For Start
            wabData.Add("tooltipTextInputLocationForStart", context.DataRow["TooltipTextInputLocationForStart"].ToString());
            wabData.Add("ddlTypeTextInputLocationForStart", context.DataRow["DDLTypeTextInputLocationForStart"].ToString());

            //Data Source for Block Location
            wabData.Add("tooltipTextBlockLocation", context.DataRow["TooltipTextBlockLocation"].ToString());
            wabData.Add("ddlTypeTextBlockLocation", context.DataRow["DDLTypeTextBlockLocation"].ToString());

            //Data Source for Skip Feature
            wabData.Add("tooltipTextSkipFeature", context.DataRow["TooltipTextSkipFeature"].ToString());
            wabData.Add("ddlTypeTextSkipFeature", context.DataRow["DDLTypeTextSkipFeature"].ToString());

            //Data Source for Isolating Valves
            wabData.Add("labelTextIsolatingValves", context.DataRow["LabelTextIsolatingValves"].ToString());
            wabData.Add("chkSkippableIsolatingValves", context.DataRow["ChkSkippableIsolatingValves"].ToString());
            wabData.Add("ddlUniqueIdFieldIsolatingValves", context.DataRow["DDLUniqueIdFieldIsolatingValves"].ToString());
            wabData.Add("tooltipTextIsolatingValves", context.DataRow["TooltipTextIsolatingValves"].ToString());
            wabData.Add("summaryTextIsolatingValves", context.DataRow["SummaryTextIsolatingValves"].ToString());
            wabData.Add("displayTextIsolatingValves", context.DataRow["DisplayTextIsolatingValves"].ToString());
            wabData.Add("minScaleTextIsolatingValves", context.DataRow["MinScaleTextIsolatingValves"].ToString());
            wabData.Add("maxScaleIsolatingValves", context.DataRow["MaxScaleIsolatingValves"].ToString());
            wabData.Add("chkExportToCsvIsolatingValves", context.DataRow["ChkExportToCsvIsolatingValves"].ToString());
            wabData.Add("chkSaveToLayerIsolatingValves", context.DataRow["ChkSaveToLayerIsolatingValves"].ToString());
            wabData.Add("ddlTargetLayerIsolatingValves", context.DataRow["DDLTargetLayerIsolatingValves"].ToString());
                        
            //Data Source for Isolating Hydrants
            wabData.Add("labelTextIsolatedHydrants", context.DataRow["LabelTextIsolatedHydrants"].ToString());
            wabData.Add("chkSkippableIsolatedHydrants", context.DataRow["ChkSkippableIsolatedHydrants"].ToString());
            wabData.Add("ddlUniqueIdFieldIsolatedHydrant", context.DataRow["DDLUniqueIdFieldIsolatedHydrant"].ToString());
            wabData.Add("tooltipTextIsolatedHydrants", context.DataRow["TooltipTextIsolatedHydrants"].ToString());
            wabData.Add("summaryTextIsolatedHydrants", context.DataRow["SummaryTextIsolatedHydrants"].ToString());
            wabData.Add("displayTextIsolatedHydrants", context.DataRow["DisplayTextIsolatedHydrants"].ToString());
            wabData.Add("minScaleIsolatedHydrants", context.DataRow["MinScaleIsolatedHydrants"].ToString());
            wabData.Add("maxScaleIsolatedHydrants", context.DataRow["MaxScaleIsolatedHydrants"].ToString());
            wabData.Add("chkExportToCsvIsolatedHydrants", context.DataRow["ChkExportToCsvIsolatedHydrants"].ToString());
            wabData.Add("chkSaveToLayerIsolatedHydrants", context.DataRow["ChkSaveToLayerIsolatedHydrants"].ToString());
            wabData.Add("ddlTargetLayerIsolatedHydrants", context.DataRow["DDLTargetLayerIsolatedHydrants"].ToString());
                       
            //Data Source for Isolating Customers
            wabData.Add("labelTextIsolatedCustomers", context.DataRow["LabelTextIsolatedCustomers"].ToString());
            wabData.Add("chkSkippableIsolatedCustomers", context.DataRow["ChkSkippableIsolatedCustomers"].ToString());            
            wabData.Add("ddlTypeUniqueIdFieldIsolatedCustomers", context.DataRow["DDLTypeUniqueIdFieldIsolatedCustomers"].ToString());
            wabData.Add("tooltipTextIsolatedCustomers", context.DataRow["TooltipTextIsolatedCustomers"].ToString());
            wabData.Add("summaryTextIsolatedCustomers", context.DataRow["SummaryTextIsolatedCustomers"].ToString());
            wabData.Add("displayTextIsolatedCustomers", context.DataRow["DisplayTextIsolatedCustomers"].ToString());
            wabData.Add("minScaleTextIsolatedCustomers", context.DataRow["MinScaleTextIsolatedCustomers"].ToString());
            wabData.Add("maxScaleIsolatedCustomers", context.DataRow["MaxScaleIsolatedCustomers"].ToString());
            wabData.Add("chkExportToCsvIsolatedCustomers", context.DataRow["ChkExportToCsvIsolatedCustomers"].ToString());
            wabData.Add("chkSaveToLayerIsolatedCustomers", context.DataRow["ChkSaveToLayerIsolatedCustomers"].ToString());
            wabData.Add("ddlTextTargetLayerIsolatedCustomers", context.DataRow["DDLTextTargetLayerIsolatedCustomers"].ToString());
                       
            //Data Source for Overview
            wabData.Add("chkVisibleOverview", context.DataRow["ChkVisibleOverview"].ToString());
            wabData.Add("bufferDistanceTextOverview", context.DataRow["BufferDistanceTextOverview"].ToString());
            wabData.Add("ddlTextUnitOverview", context.DataRow["DDLTextUnitOverview"].ToString());
            wabData.Add("minScaleTextOverview", context.DataRow["MinScaleTextOverview"].ToString());
            wabData.Add("maxScaleTextOverview", context.DataRow["MaxScaleTextOverview"].ToString());
            wabData.Add("chkSaveToLayerOverView", context.DataRow["ChkSaveToLayerOverView"].ToString());
            wabData.Add("ddlTextTargetLayerOverview", context.DataRow["DDLTextTargetLayerOverview"].ToString());
            wabData.Add("ddlTextIsolatingValvesOverview", context.DataRow["DDLTextIsolatingValvesOverview"].ToString());
            wabData.Add("ddlTextIsolatedHydrantsOverview", context.DataRow["DDLTextIsolatedHydrantsOverview"].ToString());
            wabData.Add("ddlTextIsolatedCustomersOverview", context.DataRow["DDLTextIsolatedCustomersOverview"].ToString());
            
            //Data source for Other Other
            wabData.Add("iamgeHeightTextOther", context.DataRow["IamgeHeightTextOther"].ToString());
            wabData.Add("imageWidthTextOther", context.DataRow["ImageWidthTextOther"].ToString());
            wabData.Add("timeoutTextOther", context.DataRow["TimeoutTextOther"].ToString());
            wabData.Add("displayTextOther", context.DataRow["DisplayTextOther"].ToString());

            //Data source for application data
            wabData.Add("appData", context.DataRow["AppData"].ToString());
            wabData.Add("appDescription", context.DataRow["AppDescription"].ToString());

            //Data source for Button value check
            wabData.Add("checkBtn", context.DataRow["CheckBtn"].ToString());

            //Data source for menu option
            wabData.Add("menuOption", context.DataRow["MenuOption"].ToString());

            //Data source to search webmap
            wabData.Add("searchForWebMap", context.DataRow["SearchForWebMap"].ToString());

            //Data source for radio button value check 
            wabData.Add("checkRadioButton", context.DataRow["CheckRadioButton"].ToString());

            //Data source for GpTask
            wabData.Add("gpTaskUrl", context.DataRow["GpTaskUrl"].ToString());

            wabData.Add("mapName", context.DataRow["MapName"].ToString());


           
        }
    }
}
