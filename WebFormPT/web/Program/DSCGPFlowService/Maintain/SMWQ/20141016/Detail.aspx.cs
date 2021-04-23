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

public partial class Program_DSCGPFlowService_Maintain_SMWQ_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "";

        //CL_Chang 2013/07/15 客製程式碼, 上下一筆功能
        string GUID = Utility.CheckCrossSiteScripting(Request.QueryString["ObjectGUID"].ToString());
        string PPUID = Utility.CheckCrossSiteScripting(Request.QueryString["HistoryGUID"].ToString());
        string ListID = Utility.CheckCrossSiteScripting(Request.QueryString["DataListID"].ToString());
        DataObjectSet pdos = (DataObjectSet)Session["SMWQ_CURLIS"];
        if (pdos != null)
        {
            string serialNo = Convert.ToString(engine.executeScalar("select SMWYAAA005 from SMWYAAA where SMWYAAA019='" + GUID + "'"));
            for (int i = 0; i < pdos.getAvailableDataObjectCount(); i++)
            {
                if (pdos.getAvailableDataObject(i).getData("SMWQAAA005").Equals(serialNo))
                {
                    Session["clickRow"] = i;
                    break;
                }
            }
        }

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
            //參考流程發起的
            sql = "select SMWAAAA005 from SMWAAAA inner join FORMRELATION on CURPAGETYPE=SMWAAAA002 where CURGUID='" + Utility.filter(Request.QueryString["ObjectGUID"]) + "'";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                url = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                engine.close();
                Response.Write(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwq_detail_aspx.language.ini", "message", "ResponseMsg", "找不到作業處理畫面. 請洽系統管理員"));
                Response.End();
            }
        }
        url = "~/" + url + "?" + urlP;

        engine.close();

        Response.Redirect(url);
    }
}
