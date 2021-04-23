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
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using com.dsc.flow.data;
using com.dsc.flow.server;
using com.dsc.kernal.agent;
using WebServerProject.flow.SMWY;
using WebServerProject.flow.SMWG;
using WebServerProject;
using DSCWebControl;

public partial class Program_DSCGPFlowService_Maintain_SMWO_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "";

        string urlP = Request.QueryString.ToString();
        //根據
        string url = "";
        sql = "select SMWAAAA005 from SMWAAAA inner join SMWYAAA on SMWYAAA018=SMWAAAA001 where SMWYAAA019='" + Utility.filter(Request.QueryString["ObjectGUID"]) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            url = ds.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            engine.close();
            Response.Write(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwo_detail_aspx.language.ini", "message", "ResponseMsg", "找不到作業處理畫面. 請洽系統管理員"));
            Response.End();
        }
        url = "~/" + url + "?" + urlP;

        engine.close();

        Response.Redirect(url);
    }
}
