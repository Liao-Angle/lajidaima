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
using com.dsc.kernal.utility;
using com.dsc.kernal.factory;

public partial class _Default : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
                com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
                com.dsc.kernal.db.AbstractConnectString acs=cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
                acs.getConnectionString();

                Session["connectString"] = acs.connectString;
                Session["engineType"] = acs.engineType;

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(acs.engineType, acs.connectString);

                //設定DebugPage
                string sql = "select SMVPAAA009, SMVPAAA014, SMVPAAA021, SMVPAAA022, SMVPAAA030, SMVPAAA031 from SMVPAAA";
                DataSet ds = engine.getDataSet(sql, "TEMP");

                if (ds.Tables[0].Rows[0]["SMVPAAA030"].ToString().Equals("Y"))
                {
                    //開始限制IP判斷
                    DataSet ds2 = engine.getDataSet("select * from SMVPAAB", "TEMP");
                    if (!checkIP(Request.ServerVariables["REMOTE_ADDR"], ds.Tables[0].Rows[0]["SMVPAAA031"].ToString(), ds2))
                    {
                        Response.Redirect("NoAuth.aspx");
                    }
                }
                //else  //啟用限制IP判斷，也要設定 session 值 (因啟用限制IP，允許使用的IP也需有 session 才能正常運作)  hjlin 2009/11/30
                //{
                    if (ds.Tables[0].Rows[0][0].ToString().Equals("Y"))
                    {
                        Session["DebugPage"] = true;
                    }
                    else
                    {
                        Session["DebugPage"] = false;
                    }
                    Session["MaxRecordCount"] = ds.Tables[0].Rows[0][1];
                    Session["FlowProcessCount"] = ds.Tables[0].Rows[0][2]; //流程引擎呼叫處理次數
                    Session["FlowProcessWaiting"] = ds.Tables[0].Rows[0][3]; //流程引擎呼叫錯誤時等待毫秒

                    WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
                    Session["FlowAdapter"] = sp.getParam("FlowAdapter");
                    Session["NaNaWebService"] = sp.getParam("NaNaWebService");
                    Session["DotJWebService"] = sp.getParam("DotJWebService");
                    Session["FlowAccount"] = sp.getParam("FlowAccount");
                    Session["FlowPassword"] = sp.getParam("FlowPassword");
                //}
                engine.close();

                Session["IsEmbeddedPortal"] = false;
                if (!Request.QueryString.ToString().Equals(""))
                {
                    Session["IsEmbeddedPortal"] = true;
                }
                //重新導向到登入頁面
                Response.Redirect("Login.aspx?"+Request.QueryString);
            }
        }
    }
    private bool checkIP(string curIP, string mode, DataSet ds)
    {
        string[] curPart = curIP.Split(new char[] { '.' });

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string[] second = ds.Tables[0].Rows[i]["SMVPAAB002"].ToString().Split(new char[] { '.' });
            bool isM = isMatch(curPart, second);
            if (mode.Equals("0"))
            {
                if (isM == true)
                {
                    return false;
                }
            }
            else if (mode.Equals("1"))
            {
                if (isM == true)
                {
                    return true;
                }
            }
        }
        if (mode.Equals("0"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool isMatch(string[] first, string[] pattern)
    {
        if ((first.Length != 4) || (pattern.Length != 4))
        {
            return false;
        }

        for (int i = 0; i < 4; i++)
        {
            if (pattern[i].Equals("*"))
            {
                continue;
            }
            if (int.Parse(first[i]) != int.Parse(pattern[i]))
            {
                return false;
            }
        }
        return true;
    }
}
