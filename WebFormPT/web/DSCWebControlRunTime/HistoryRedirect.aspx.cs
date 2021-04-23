using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class DSCWebControlRunTime_HistoryRedirect : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string GUID = Request.QueryString["GUID"];
        string URL = Request.QueryString["PageURL"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);

        //根據GUID取得ObjectGUID
        string sql = "select * from DATAHISTORY where GUID='" + Utility.filter(GUID) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        URL += "?ParentPanelID=&DataListID=&UIType=Process&ObjectGUID=" + ds.Tables[0].Rows[0]["GUID"].ToString() + "&HistoryGUID=&FlowGUID=&ACTID=&PDID=&PDVer=&ACTName=&UIStatus=3&WorkItemOID=&TargetWorkItemOID=&IsAllowWithDraw=false&CurPanelID=" + CurPanelID;

        engine.close();
        Response.Redirect(URL);        
    }
}
