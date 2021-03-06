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

public partial class Program_DSCGPFlowService_Maintain_SMWK_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "";

	//客製程式碼
        string GUID = Utility.CheckCrossSiteScripting(Request.QueryString["ObjectGUID"].ToString());
        string PPUID = Utility.CheckCrossSiteScripting(Request.QueryString["HistoryGUID"].ToString());
        string ListID = Utility.CheckCrossSiteScripting(Request.QueryString["DataListID"].ToString());
        DataObjectSet pdos = (DataObjectSet)Session[PPUID + "_CURLIST"];
        if (pdos != null)
        {
            string SheetNo = Convert.ToString(engine.executeScalar("select SMWYAAA002 from SMWYAAA where SMWYAAA019='" + GUID + "'"));
            for (int i = 0; i < pdos.getAvailableDataObjectCount(); i++)
            {
                //Response.Write(pdos.getAvailableDataObject(0).getData("GUID") + ";" + GUID);
                //Response.End();
                if (pdos.getAvailableDataObject(i).getData("SMWYAAA002").Equals(SheetNo))
                {
                    //Response.Write(SheetNo + "<br>");
                    Session["clickRow"] = i;
                    break;
                    //Response.Write(i.ToString());
                    //Response.End();
                }
            }
        }

        string urlP = Request.QueryString.ToString();
        //根據
        string url = "";
        sql = "select SMWAAAA005 from SMWAAAA where SMWAAAA001='" + Utility.filter((string)Session["tempSMWK"]) + "'";

        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            url = ds.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            engine.close();
            Response.Write(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "ResponseMsg", "找不到作業處理畫面. 請洽系統管理員"));
            Response.End();
        }
        url = "~/" + url + "?" + urlP;

        engine.close();

        Response.Redirect(url);
    }
}
