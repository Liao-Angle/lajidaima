using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;
using com.dsc.kernal.utility;
using com.dsc.kernal.document;

public partial class SmpProgram_Form_SPIT002_ViewInfoDemand : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string infoDemandGUID = (string)getSession((string)Session["UserID"], "InfoDemandGUID");
        string ParentPanelID = "1";
        string DataListID = "ListTable";
        string ParentPageUID = Request.QueryString["ParentPageUID"];
        string UIType = "Process";
        string ObjectGUID = infoDemandGUID;
        string HistoryGUID = "";
        string FlowGUID = "";
        string ACTID = "";
        string PDID = "";
        string PDVer = "";
        string ACTName = "";
        string UIStatus = "12";
        string WorkItemOID = "";
        string TargetWorkItemOID = "";
        string workAssignmentOID = "";
        string assignmentType = "";
        string reassignmentType = "";
        string manualReassignType = "";
        string IsAllowWithDraw = "true";
        string IsMaintain = "N";
        string CurPanelID = (Int16.Parse(Request.QueryString["CurPanelID"]) + 1) + "";

        string curPanelID = Request.QueryString["CurPanelID"];
        string sql = "select SMWYAAA001, SMWYAAA003, SMWYAAA005 from SMWYAAA where SMWYAAA019='" + infoDemandGUID + "'";
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            PDID = ds.Tables[0].Rows[0][1].ToString();
            FlowGUID = ds.Tables[0].Rows[0][2].ToString();
        }

        engine.close();

        string url = "../SPIT001/Form.aspx?ParentPanelID=" + ParentPanelID + "&DataListID=" + DataListID + "&ParentPageUID=" + ParentPageUID + "&UIType=" + UIType + "&ObjectGUID=" + ObjectGUID + "&HistoryGUID=" + HistoryGUID + "&FlowGUID=" + FlowGUID + "&ACTID=" + ACTID + "&PDID=" + PDID + "&PDVer=" + PDVer + "&ACTName=" + ACTName + "&UIStatus=" + UIStatus + "&WorkItemOID=" + WorkItemOID + "&TargetWorkItemOID=" + TargetWorkItemOID + "&workAssignmentOID=" + workAssignmentOID + "&assignmentType=" + assignmentType + "&reassignmentType=" + reassignmentType + "&manualReassignType=" + manualReassignType + "&IsAllowWithDraw=" + IsAllowWithDraw + "&IsMaintain=" + IsMaintain + "&CurPanelID=" + CurPanelID;
        Response.Redirect(url);
    }
}