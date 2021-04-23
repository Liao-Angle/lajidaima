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

public partial class Program_DSCGPFlowService_Maintain_SMWL_Detail : System.Web.UI.Page
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
        string PPUID = Utility.CheckCrossSiteScripting(Request.QueryString["ParentPageUID"].ToString());
        string ListID = Utility.CheckCrossSiteScripting(Request.QueryString["DataListID"].ToString());
        DataObjectSet pdos = (DataObjectSet)Session[PPUID + "_CURLIST"];
        if (pdos != null)
        {
            string SheetNo = Convert.ToString(engine.executeScalar("select SMWYAAA002 from SMWYAAA where SMWYAAA019='" + GUID + "'"));
            for (int i = 0; i < pdos.getAvailableDataObjectCount(); i++)
            {
                //Response.Write(pdos.getAvailableDataObject(0).getData("GUID") + ";" + GUID);
                //Response.End();
                if (pdos.getAvailableDataObject(i).getData("SHEETNO").Equals(SheetNo))
                {
                    //Response.Write(SheetNo + "<br>");
                    Session["clickRow"] = i;
                    break;
                    //Response.Write(i.ToString());
                    //Response.End();
                }
            }
        }


        string urlP = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString.ToString());
        //不能以原稿來找作業畫面, 因為會有ProcessNew的問題
        string url = "";
        sql = "select SMWAAAA005 from SMWAAAA inner join SMWDAAA on SMWDAAA005=SMWAAAA001 inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.filter(Request.QueryString["PDID"]) + "' and SMWDAAA004='" + Utility.filter(Request.QueryString["ACTName"]) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            url = ds.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            sql = "select SMWAAAA005 from SMWAAAA inner join SMWDAAA on SMWDAAA005=SMWAAAA001 inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.filter(Request.QueryString["PDID"]) + "' and SMWDAAA006='Default'";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count == 0)
            {
                engine.close();
                Response.Write(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_detail_aspx.language.ini", "message", " ResponseMsg", "找不到作業處理畫面. 請洽系統管理員"));
                Response.End();
            }
            url = ds.Tables[0].Rows[0][0].ToString();
        }
        url = "~/" + url + "?" + urlP;

        engine.close();

        Response.Redirect(url);
    }
}
