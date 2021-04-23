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

public partial class ECPEF_SMWK_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "";
        string ef2kwebsite = "";
        string rrid = "";
        string url = "";

        string ObjectGUID = Utility.CheckCrossSiteScripting(Request.QueryString["ObjectGUID"]);

        if (!ObjectGUID.Equals("QQ19"))
        {
            string urlP = Request.QueryString.ToString();
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
        }
        else
        {
            SysParam sp = new SysParam(engine);
            ef2kwebsite = sp.getParam("EF2KWebSite");
            rrid = (string)Session["UserID"];
        }
        engine.close();

        if (!ObjectGUID.Equals("QQ19"))
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
            //Response.Write("SP8 Form<br>");
            //Response.Write("FormID:" + Request.QueryString["PDID"]+"<br>");
            //Response.Write("SheetNo:" + Request.QueryString["FlowGUID"] + "<br>");
            //Response.Write("FlowNo:" + Request.QueryString["WorkItemOID"] + "<br>");
            //Response.Write("BranchNo:" + Request.QueryString["TargetWorkItemOID"] + "<br>");
            //Response.Write("SerialNo:" + Request.QueryString["WorkAssignmentOID"] + "<br>");
            string formid = Utility.CheckCrossSiteScripting(Request.QueryString["PDID"]);
            string sheetno = Utility.CheckCrossSiteScripting(Request.QueryString["FlowGUID"]);
            string flowno = "";
            string branchno = "";
            string serialno = "";
            string FormStatus = "2";

            //Response.Write("<script language=javascript>");
            //Response.Write(str);
            //Response.Write("window.showModalDialog('" + ef2kwebsite + "?Action=DirectMail&FormID="+formid+"&SheetNo="+sheetno+"&FlowNo="+flowno+"&BranchNo="+branchno+"&SerialNo="+serialno+"&FormStatus=8','','dialogLeft:0;dialogTop:0;dialogWidth:1000;dialogHeight:750');");
            //Response.Write("closeRefreshSilence();");
            //Response.Write("</script>");

            //這裡後面要串上EF站台的電腦名稱
            string url2 = ef2kwebsite + "?ParentPanelID=" + Utility.CheckCrossSiteScripting(Request.QueryString["ParentPanelID"]) + "&CurPanelID=" + Utility.CheckCrossSiteScripting(Request.QueryString["CurPanelID"]) + "&Action=DirectMail&FormID=" + formid + "&SheetNo=" + sheetno + "&FlowNo=" + flowno + "&BranchNo=" + branchno + "&SerialNo=" + serialno + "&FormStatus=" + FormStatus + "&rrid=" + rrid + "&FromECP=1";

            //showPanelWindow("TEST", url2, 0, 0, "", true, false);
            Response.Redirect(url2);
            //Response.Write(url2);
        }
    }
}
