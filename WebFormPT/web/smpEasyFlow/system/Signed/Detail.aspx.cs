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

public partial class smpEasyFlow_system_Signed_Detail : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "";

        string url = "";
        string ef2kwebsite = "";

        //========修改的部分=======
        string ef2kwebdomain = "";
        string rrid = "";
        string sp7str = "";
        //========修改的部分=======

        SysParam sp = new SysParam(engine);
        ef2kwebsite = sp.getParam("EF2KWebSite");

        //========修改的部分=======
        ef2kwebdomain = sp.getParam("EF2KWebDomain");
        sp7str = sp.getParam("EF2KWebDB");
        AbstractEngine sp7engine = factory.getEngine(EngineConstants.SQL, sp7str);
        rrid = mappingUserID(engine, sp7engine);
        //========修改的部分=======

        engine.close();

        string str = "";
        str += "function closeRefreshSilence(){";
        str += "try{";
        //國昌:先不要往上帶, 否則萬一此時有人點其他的功能, 畫面會被蓋掉
        //str += "window.parent.setZIndex('" + (string)getSession("ParentPanelID") + "');"; //這行可以把指定的panelID帶到最前端
        str += "wobj=window.parent.getPanelWindowObject('" + Request.QueryString["ParentPanelID"] + "');"; //這行可以取得該panelID的PanelWindow中代表內容的window HTML物件
        str += "wobj.refreshDataList('ListTable');"; //這行可以呼叫該視窗的refreshDataList方法, 此方法為WebFormBasePage提供
        str += "window.parent.Panel_Close_Silence('" + Request.QueryString["CurPanelID"] + "');"; //這行可以直接關閉目前視窗
        str += "}catch(e){};";
        str += "}";
        //Response.Write("SP7 Form<br>");
        //Response.Write("FormID:" + Request.QueryString["PDID"]+"<br>");
        //Response.Write("SheetNo:" + Request.QueryString["FlowGUID"] + "<br>");
        //Response.Write("FlowNo:" + Request.QueryString["WorkItemOID"] + "<br>");
        //Response.Write("BranchNo:" + Request.QueryString["TargetWorkItemOID"] + "<br>");
        //Response.Write("SerialNo:" + Request.QueryString["WorkAssignmentOID"] + "<br>");
        string formid = Request.QueryString["PDID"];
        string sheetno = Request.QueryString["FlowGUID"];
        string flowno = Request.QueryString["WorkItemOID"];
        string branchno = Request.QueryString["TargetWorkItemOID"];
        string serialno = Request.QueryString["WorkAssignmentOID"];

        //Response.Write("<script language=javascript>");
        //Response.Write(str);
        //Response.Write("window.showModalDialog('" + ef2kwebsite + "?Action=DirectMail&FormID="+formid+"&SheetNo="+sheetno+"&FlowNo="+flowno+"&BranchNo="+branchno+"&SerialNo="+serialno+"&FormStatus=8','','dialogLeft:0;dialogTop:0;dialogWidth:1000;dialogHeight:750');");
        //Response.Write("closeRefreshSilence();");
        //Response.Write("</script>");

        string url2 = ef2kwebsite + "?ParentPanelID=" + Request.QueryString["ParentPanelID"] + "&CurPanelID=" + Request.QueryString["CurPanelID"] + "&Action=DirectMail&FormID=" + formid + "&SheetNo=" + sheetno + "&FlowNo=" + flowno + "&BranchNo=" + branchno + "&SerialNo=" + serialno + "&FormStatus=16&rrid=" + ef2kwebdomain + "\\" + rrid;

        //showPanelWindow("TEST", url2, 0, 0, "", true, false);
        Response.Redirect(url2);
    }
    //========修改的部分=======
    private string mappingUserID(AbstractEngine engine, AbstractEngine sp7engine)
    {
        string ret = "";
        ret = Convert.ToString(sp7engine.executeScalar("select resak004 from resak with(nolock) where resak001='" + Convert.ToString(Session["UserID"]) + "'"));
        if (ret != "")
        {
            return ret;
        }
        else
        {
            return Convert.ToString(Session["UserID"]);
        }
    }
    //========修改的部分=======
}
