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
using WebServerProject.maintain.SMVU;
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using WebServerProject.system.login;
using com.dsc.kernal.logon;

public partial class ChangeUser : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                NewUser.clientEngineType = engineType;
                NewUser.connectDBString = connectString;

            }
        }
    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        LogonFactory lfac = new LogonFactory();
        //string loginStr = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "LogonClass");
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string loginStr = sp.getParam("LogonClass");
        if (loginStr.Equals(""))
        {
            loginStr = "WebServerProject.system.login.GPUserLogin";
        }
        string loginAsm = loginStr.Split(new char[] { '.' })[0].Trim();
        AbstractLogon log = lfac.getLogonObject(loginAsm, loginStr);

        string[] ugroup = log.getAuth(engine, "", NewUser.ValueText, "");

        string sql = "select * from Users where id='" + Utility.filter(NewUser.ValueText) + "'";
        DataSet us = engine.getDataSet(sql, "TEMP");

        Session["UserGUID"] = us.Tables[0].Rows[0]["OID"].ToString();
        Session["UserID"] = us.Tables[0].Rows[0]["id"].ToString();
        Session["UserName"] = us.Tables[0].Rows[0]["userName"].ToString();
        Session["Locale"] = us.Tables[0].Rows[0]["localeString"].ToString();

        if (log.UserCode.ToLower().Equals("administrator"))
        {
            Session["IsSysAdmin"] = true;
        }
        else
        {
            Session["IsSysAdmin"] = false;
        }

        Session["usergroup"] = ugroup;

        //初始化com.dll
        GlobalCache.setValue("UserGUID", (string)Session["UserGUID"]);


        //判定是否需新增UserSetting檔
        sql = "select * from USERSETTING where USERGUID='" + Utility.filter((string)Session["UserGUID"]) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count == 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["GUID"] = IDProcessor.getID("");
            dr["USERGUID"] = (string)Session["UserGUID"];
            dr["VRGUID"] = "";
            dr["RECEIVEMSG"] = "N";
            dr["RECEIVEMAIL"] = "N";
            dr["LASTPWDCHANGE"] = "";
            ds.Tables[0].Rows.Add(dr);

            engine.updateDataSet(ds);
        }

        engine.close();

        Response.Redirect(getLayoutFrame((string)Session["layoutType"])+"/MainFrame.aspx?" + (string)Session["PortalLink"]);

    }
    protected void Button2_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(getLayoutFrame((string)Session["layoutType"]) + "/MainFrame.aspx?" + (string)Session["PortalLink"]);
    }
    private void showMessage(string msg)
    {
        Response.Write("<script language=javascript>");
        Response.Write("alert('" + msg.Replace(System.Environment.NewLine, "\\n") + "');");
        Response.Write("</script>");
    }
    private string getLayoutFrame(string layoutType)
    {
        return GlobalProperty.getProperty("layout", layoutType).Split(new char[] { ';' })[4];

    }
}
