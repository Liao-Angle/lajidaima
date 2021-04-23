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
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using WebServerProject.auth;
using WebServerProject;

public partial class ProjectBaseWebUI_ReportingServiceMaintain : System.Web.UI.MasterPage
{
    /// <summary>
    /// 程式代號
    /// </summary>
    public string maintainIdentity = "";
    /// <summary>
    /// 應用程式代號(稽核模組使用)
    /// </summary>
    public string ApplicationID = "";

    /// <summary>
    /// 模組代號(稽核模組使用)
    /// </summary>
    public string ModuleID = "";

    /// <summary>
    /// 預設系統錯誤層級
    /// </summary>
    public int errorLevel = 5;

    /// <summary>
    /// 報表類別
    /// </summary>
    public string classID = "";
    /// <summary>
    /// 報表代號
    /// </summary>
    public string reportID = "";

    public delegate string GatherParameterEvent();
    public event GatherParameterEvent GatherParameter;

    public delegate void ClearParameterEvent();
    public event ClearParameterEvent ClearParameter;

    protected void SendButton_Click(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        SysParam sp = new SysParam(engine);
        string reportURL = sp.getParam("ReportURL");
        engine.close();

        string pam = "";
        if (GatherParameter != null)
        {
            pam = GatherParameter();
        }
        else
        {
            pam = "";
        }
        string url = getReportHTML(reportURL, classID, reportID);
        if (!pam.Equals(""))
        {
            url += "&" + pam;
        }

        Response.Write("window.open('" + url + "','_blank', 'channelmode=0,directories=0,menubar=0,resizable=1,status=0,titlebar=0,toolbar=0');");
    }
    private void writeLog(Exception e)
    {
        //這裡要整合稽核模組

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "select SMVAAAB003 from SMVAAAB where SMVAAAB002='" + Utility.filter(maintainIdentity) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        string ProgramName = "未知的程式名稱";
        if (ds.Tables[0].Rows.Count > 0)
        {
            ProgramName = ds.Tables[0].Rows[0][0].ToString();
        }

        WebServerProject.Audit au = new Audit(engine);
        bool res = au.writeLog(ApplicationID, ModuleID, maintainIdentity, ProgramName, errorLevel, e.Message, e.StackTrace, (string)Session["UserID"], (string)Session["UserName"], Request.ServerVariables["REMOTE_ADDR"], Request.ServerVariables["HTTP_USER_AGENT"]);

        engine.close();

    }
    protected string getReportHTML(string reportURL, string classID, string reportID)
    {
        string outStr = reportURL;
        outStr += "?%2f" + classID + "%2f" + reportID + "&rs%3aClearSession=true&rs%3aFormat=HTML4.0&rs%3aCommand=Render&rc%3aArea=Toolbar&rc%3aLinkTarget=_top&rc%3aJavaScript=True&rc%3aToolbar=True&rc%3aParameters=False";
        return outStr;
    }
    /*
    protected string getReportXLS(string reportURL, string classID, string reportID)
    {
        string outStr = reportURL;
        outStr += "?%2f" + classID + "%2f" + reportID + "&rs%3aCommand=Render&rs%3AFormat=EXCEL";
        return outStr;

    }
    */
    protected void ClearButton_Click(object sender, EventArgs e)
    {
        if (ClearParameter != null)
        {
            try
            {
                ClearParameter();
            }
            catch (Exception te)
            {
                Response.Write("<script language=javascript>alert('" + te.Message.Replace("\n", "\\n") + "');</script>");
            }
        }
    }
}
