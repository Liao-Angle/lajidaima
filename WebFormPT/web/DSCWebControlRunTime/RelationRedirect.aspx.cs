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

public partial class DSCWebControlRunTime_RelationRedirect : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string GUID = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["GUID"]);
        string URL = "";
        string PageType = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["PAGETYPE"]);

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);

        string sql = "select * from SMWAAAA where SMWAAAA002='" + Utility.filter(PageType) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        sql = "select FLOWGUID, FLOWID from FORMRELATION where ORIGUID='" + Utility.filter(GUID) + "' or CURGUID='"+Utility.filter(GUID)+"'";
        DataSet ds2 = engine.getDataSet(sql, "TEMP");


        URL = Page.ResolveClientUrl("~/"+ds.Tables[0].Rows[0]["SMWAAAA005"].ToString());
        URL += "?ParentPanelID=&DataListID=&UIType=Process&ObjectGUID=" + GUID + "&HistoryGUID=&FlowGUID=" + ds2.Tables[0].Rows[0][0].ToString() + "&ACTID=&PDID="+ds2.Tables[0].Rows[0][1].ToString()+"&PDVer=&ACTName=&UIStatus=3&WorkItemOID=&TargetWorkItemOID=&IsAllowWithDraw=false&CurPanelID=" + CurPanelID;

        engine.close();
        Response.Redirect(URL);        
    }
}
