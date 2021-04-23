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

public partial class ProjectBaseWebUI_WebiReportParameter : System.Web.UI.MasterPage
{
    /// <summary>
    /// 報表BO物件代號
    /// </summary>
    public string reportID = "";

    /// <summary>
    /// 程式代碼
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
    /// 設定是否顯示報表類型
    /// </summary>
    public bool isShowReportType = true;

    public delegate string composeSearchStringEvent();
    public event composeSearchStringEvent composeSearchString;

    public void InitData()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);


        AUTHAgent authagent = new AUTHAgent();
        authagent.engine = engine;

        int auth = authagent.getAuth(maintainIdentity, (string)Session["UserID"], (string[])Session["usergroup"]);

        engine.close();

        if (auth == 0)
        {
            Response.Redirect("~/NoAuth.aspx");
        }
        string[,] ids = new string[,]{
            {"H","HTML"},
            {"P","PDF"},
            {"E","EXCEL"},
            {"W","WORD"}
        };
        ReportFormat.setListItem(ids);

    }
    
    protected override void OnInit(EventArgs e)
    {
        if (!isShowReportType)
        {
            DSCLabel1.Visible = false;
            ReportFormat.Visible = false;
        }
        base.OnInit(e);
    }
    protected void ViewReportButton_Click(object sender, EventArgs e)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;

        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            engine = factory.getEngine(engineType, connectString);

            string url = WebiURL.composeWebiURL(engine, (string)Session["UserID"], (string)Session["UserPWD"], (string[])Session["UserGroup"], reportID, "");

            engine.close();


            url += "&sOutputFormat=" + ReportFormat.ValueText;

            if (composeSearchString != null)
            {
                url += composeSearchString();
            }
            
            //Response.Write("<script language=javascript>");
            Response.Write("window.open('" + url + "','_blank','height=600,width=800,status=no,toolbar=no,menubar=no,location=no,top=0,left=0,resizable=1');");
            Session["IsLogonWebi"] = true;

            //Response.Write("</script>");
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            Page.Response.Write("alert('" + te.Message + "');");
            writeLog(te);

        }
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

        WebServerProject.Audit au = new WebServerProject.Audit(engine);
        bool res = au.writeLog(ApplicationID, ModuleID, maintainIdentity, ProgramName, errorLevel, e.Message, e.StackTrace, (string)Session["UserID"], (string)Session["UserName"], Request.ServerVariables["REMOTE_ADDR"], Request.ServerVariables["HTTP_USER_AGENT"]);

        engine.close();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string js = "";
            js += "<script src=\"" + Page.ResolveClientUrl("~/JS/ShareScript.js") + "\" language=\"javascript\"></script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "shareScript", js, false);
        }
    }
}
