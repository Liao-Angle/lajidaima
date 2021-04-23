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
        //string connectString = (string)Session["connectString"];
        //string engineType = (string)Session["engineType"];
        //IOFactory factory = new IOFactory();
        //AbstractEngine engine = factory.getEngine(engineType, connectString);
        //string sql = "";

        //string urlP = Request.QueryString.ToString();
        ////根據
        //string url = "";
        //sql = "select SMWAAAA005 from SMWAAAA inner join SMWYAAA on SMWYAAA018=SMWAAAA001 where SMWYAAA019='" + Utility.filter(Request.QueryString["ObjectGUID"]) + "'";
        //DataSet ds = engine.getDataSet(sql, "TEMP");
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    url = ds.Tables[0].Rows[0][0].ToString();
        //}
        //else
        //{
        //    //參考流程發起的
        //    sql = "select SMWAAAA005 from SMWAAAA inner join FORMRELATION on CURPAGETYPE=SMWAAAA002 where CURGUID='" + Utility.filter(Request.QueryString["ObjectGUID"]) + "'";
        //    ds = engine.getDataSet(sql, "TEMP");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        url = ds.Tables[0].Rows[0][0].ToString();
        //    }
        //    else
        //    {
        //        engine.close();
        //        Response.Write(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwq_detail_aspx.language.ini", "message", "ResponseMsg", "找不到作業處理畫面. 請洽系統管理員"));
        //        Response.End();
        //    }
        //}
        //url = "~/" + url + "?" + urlP;

        //engine.close();

        //Response.Redirect(url);
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "";
        string dfgs = Utility.CheckCrossSiteScripting(Request.QueryString["ObjectGUID"].ToString());
        string ef2kwebsite = "";
        string rrid = "";
        string url = "";

        if (!Utility.CheckCrossSiteScripting(Request.QueryString["ObjectGUID"]).Equals(""))
        {
            string urlP = Utility.CheckCrossSiteScripting(Request.QueryString.ToString());
            //不能以原稿來找作業畫面, 因為會有ProcessNew的問題
            sql = "select SMWAAAA005 from SMWAAAA inner join SMWDAAA on SMWDAAA005=SMWAAAA001 inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.CheckCrossSiteScripting(Utility.filter(Request.QueryString["PDID"])) + "' and SMWDAAA004='" + Utility.CheckCrossSiteScripting(Utility.filter(Request.QueryString["ACTName"])) + "' and SMWAAAA005 not like N'%Maintain%'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                url = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                sql = "select SMWAAAA005 from SMWAAAA inner join SMWDAAA on SMWDAAA005=SMWAAAA001 inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.CheckCrossSiteScripting(Utility.filter(Request.QueryString["PDID"])) + "' and SMWDAAA006='Default'";
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
        }
        else
        {
            SysParam sp = new SysParam(engine);
            ef2kwebsite = sp.getParam("EF2KWebSite");
            rrid = (string)Session["UserID"];
        }
        engine.close();

        if (!Utility.CheckCrossSiteScripting(Request.QueryString["ObjectGUID"]).Equals(""))
        {
            Response.Redirect(url);
        }
        else
        {
            string str = "";
            str += "function closeRefreshSilence(){";
            str += "try{";
            //國昌:先不要往上帶, 否則萬一此時有人點其他的功能, 畫面會被蓋掉
            //str += "window.parent.setZIndex('" + (string)getSession("ParentPanelID") + "');"; //這行可以把指定的panelID帶到最前端
            str += "wobj=window.parent.getPanelWindowObject('" + Utility.CheckCrossSiteScripting(Request.QueryString["ParentPanelID"]) + "');"; //這行可以取得該panelID的PanelWindow中代表內容的window HTML物件
            str += "wobj.refreshDataList('ListTable');"; //這行可以呼叫該視窗的refreshDataList方法, 此方法為WebFormBasePage提供
            str += "window.parent.Panel_Close_Silence('" + Utility.CheckCrossSiteScripting(Request.QueryString["CurPanelID"]) + "');"; //這行可以直接關閉目前視窗
            str += "}catch(e){};";
            str += "}";
            string formid = Utility.CheckCrossSiteScripting(Request.QueryString["PDID"]);
            string sheetno = Utility.CheckCrossSiteScripting(Request.QueryString["FlowGUID"]);
            string flowno = Utility.CheckCrossSiteScripting(Request.QueryString["WorkItemOID"]);
            string branchno = Utility.CheckCrossSiteScripting(Request.QueryString["TargetWorkItemOID"]);
            string serialno = Utility.CheckCrossSiteScripting(Request.QueryString["WorkAssignmentOID"]);
            string FormStatus = Utility.CheckCrossSiteScripting(Request.QueryString["assignmentType"]);


            //這裡後面要串上EF站台的電腦名稱
            string url2 = ef2kwebsite + "?ParentPanelID=" + Utility.CheckCrossSiteScripting(Request.QueryString["ParentPanelID"]) + "&CurPanelID=" + Utility.CheckCrossSiteScripting(Request.QueryString["CurPanelID"]) + "&Action=DirectMail&FormID=" + formid + "&SheetNo=" + sheetno + "&FlowNo=" + flowno + "&BranchNo=" + branchno + "&SerialNo=" + serialno + "&FormStatus=" + FormStatus + "&rrid=" + rrid + "&FromECP=1";

            Response.Redirect(url2);
        }

    }
}
