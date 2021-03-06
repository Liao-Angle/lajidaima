using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Xml;
using WebServerProject;
using com.dsc.flow.server;
using com.dsc.flow.data;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;

/// <summary>
/// BasicFormPage 的摘要描述
/// </summary>
public class BasicFormPage : BaseWebUI.GeneralWebForm
{
    public BasicFormPage()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //

    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        UserDefineButton1.ImageUrl = "~/DSCWebControlRunTime/DSCWebControlImages/DSCRichEdit/icon_up02.gif";
        UserDefineButton2.ImageUrl = "~/DSCWebControlRunTime/DSCWebControlImages/DSCRichEdit/icon_down02.gif";
        //變更BeforeClick 事件行為 (原本會自動開窗，取消掉)
        UserDefineButton1.BeforeClick = "";
        UserDefineButton2.BeforeClick = "";

        if (Request.QueryString["runMethod"] != null)
        {
            string runMethod = Request.QueryString["runMethod"];
            if (runMethod.Equals("showReadOnlyForm") || runMethod.Equals("showSignForm"))
            {
                //外部連結進來不會有收件匣
                UserDefineButton1.Display = false;
                UserDefineButton2.Display = false;
            }
        }
        if (!IsPostBack)
        {
            //新頁面讀取或是事件進入
            if (!IsProcessEvent)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script language=javascript>");
                //上一筆的行為
                sb.Append(" function PreSign(){");
                sb.Append("try{");
                sb.Append("    window.parent.setZIndex('" + (string)getSession("ParentPanelID") + "');"); //這行可以把指定的panelID帶到最前端
                sb.Append("    wobj=window.parent.getPanelWindowObject('" + (string)getSession("ParentPanelID") + "');"); //這行可以取得該panelID的PanelWindow中代表內容的window HTML物件
                sb.Append("    wobj.refreshDataList('" + (string)getSession("DataListID") + "');"); //這行可以呼叫該視窗的refreshDataList方法, 此方法為WebFormBasePage提供
                sb.Append("    wobj.clickDataList('" + (string)getSession("DataListID") + "', " + getclickRow(0) + ");"); //這行可以呼叫該視窗的clickDataList方法, 此方法為WebFormBasePage提供      
                sb.Append("}catch(e){");
                sb.Append("    alert('已經無上一張可以簽核的表單'+e.message);");
                sb.Append("};");
                //Parent Panel可能不存在 ;因此須區分開來
                sb.Append("    try {");
                sb.Append("        window.parent.Panel_Close_Silence('" + (string)getSession("CurPanelID") + "');"); //這行可以直接關閉目前視窗            
                sb.Append("    }catch(e){};");
                sb.Append("}");
                //下一筆的行為
                sb.Append(" function NextSign(){");
                sb.Append("try{");
                sb.Append("    window.parent.setZIndex('" + (string)getSession("ParentPanelID") + "');"); //這行可以把指定的panelID帶到最前端
                sb.Append("    wobj=window.parent.getPanelWindowObject('" + (string)getSession("ParentPanelID") + "');"); //這行可以取得該panelID的PanelWindow中代表內容的window HTML物件
                sb.Append("    wobj.refreshDataList('" + (string)getSession("DataListID") + "');"); //這行可以呼叫該視窗的refreshDataList方法, 此方法為WebFormBasePage提供
                sb.Append("    wobj.clickDataList('" + (string)getSession("DataListID") + "', " + getclickRow(2) + ");"); //這行可以呼叫該視窗的clickDataList方法, 此方法為WebFormBasePage提供      
                sb.Append("}catch(e){");
                sb.Append("    alert('已經無下一張可以簽核的表單'+e.message);");
                sb.Append("};");
                //Parent Panel可能不存在 ;因此須區分開來
                sb.Append("    try {");
                sb.Append("        window.parent.Panel_Close_Silence('" + (string)getSession("CurPanelID") + "');"); //這行可以直接關閉目前視窗            
                sb.Append("    }catch(e){};");
                sb.Append("}");
                sb.Append("</script>");

                Type ctype = this.GetType();
                ClientScriptManager cm = Page.ClientScript;

                if (!cm.IsStartupScriptRegistered(ctype, "sScript"))
                {
                    cm.RegisterStartupScript(ctype, "sScript", sb.ToString());
                }
            }
        }

    }

    protected override void createCustomControl(string pUID)
    {
        base.createCustomControl(pUID);
        base.attachFile.BeforeDeleteData += new DSCWebControl.FileUpload.BeforeDeleteDataEvent(attachFile_BeforeDeleteData);
    }

    bool attachFile_BeforeDeleteData()
    {
        DSCWebControl.FileItem[] dos = base.attachFile.getSelectedItem();
        for (int i = 0; i < dos.Length; i++)
        {
            DSCWebControl.FileItem tmpDo = dos[i];
            if (
                tmpDo.INSERTUSER != "" &&
            (string)Session["UserGUID"] != tmpDo.INSERTUSER
            )
            {
                MessageBox("刪除檔案中包括：原上傳者才可以刪除的檔案。請重新選擇");
                return false;
            }
        }
        return true;
    }
    protected override string getFormHead(AbstractEngine engine, string guid, string flowOID)
    {
        string sss = "select * from SMWYAAA where SMWYAAA019='" + guid + "' and SMWYAAA005='" + flowOID + "'";
        DataSet oss = engine.getDataSet(sss, "TEMP");

        ///因為不一定會有表單單頭資訊, 所以要判斷
        if (oss.Tables[0].Rows.Count > 0)
        {
            //hw = Header Wording
            string hwProcessName = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header8", "流程名稱"));
            string hwSheetNo = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header9", "單號"));
            string hwStartDate = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header10", "填表日期"));
            string hwProcessCondition = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header11", "流程狀態"));
            string hwImportance = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header12", "重要性"));
            string hwUserId = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header13", "使用者代號"));
            string hwUserName = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header14", "使用者姓名"));
            string hwOrgId = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header15", "填表單位代號"));
            string hwOrgName = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header16", "填表單位名稱"));
            string hwApplierId = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header17", "申請人代號"));
            string hwApplierName = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header18", "申請人姓名"));
            string hwApplierOrgId = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header19", "申請單位代號"));
            string hwApplierOrgName = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header20", "申請單位名稱"));
            string hwSubject = Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header21", "主旨"));

            string header = "<table width=100% border=0 cellspacing=0 id='FormHeadTable' cellpadding=2 style=\"border-left-style:solid;border-top-style:solid;border-width:1px\" class=FormHeadBorder>";
            header += "<tr>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwProcessName + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA004"].ToString() + "</td>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwSheetNo + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA002"].ToString() + "</td>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwStartDate + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA017"].ToString() + "</td>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwProcessCondition + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>";
            if (oss.Tables[0].Rows[0]["SMWYAAA020"].ToString().Equals("I"))
            {
                //header += "進行中";
                header += Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header1", "進行中"));
            }
            else if (oss.Tables[0].Rows[0]["SMWYAAA020"].ToString().Equals("Y"))
            {
                //header += "已結案";
                header += Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header2", "已結案"));
            }
            else if (oss.Tables[0].Rows[0]["SMWYAAA020"].ToString().Equals("N"))
            {
                //header += "已終止";
                header += Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header3", "已終止"));
            }
            else
            {
                //header += "已撤銷";
                header += Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header4", "已撤銷"));
            }
            header += "</td>";
            header += "</tr>";
            header += "<tr>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwImportance + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>";
            if (oss.Tables[0].Rows[0]["SMWYAAA007"].ToString().Equals("0"))
            {
                //header += "低";
                header += Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header5", "低"));
            }
            else if (oss.Tables[0].Rows[0]["SMWYAAA007"].ToString().Equals("1"))
            {
                //header += "中";
                header += Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header6", "中"));
            }
            else
            {
                //header += "高";
                header += Page.Server.HtmlEncode(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "header7", "高"));
            }
            header += "</td>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwSubject + "</td><td colspan=5 style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA006"].ToString() + "</td>";
            header += "</tr>";

            //20121106 marked for JCIC requirements
            //header += "<tr>";
            //header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwUserId + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA008"].ToString() + "</td>";
            //header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwUserName + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA009"].ToString() + "</td>";
            //header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwOrgId + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA010"].ToString() + "</td>";
            //header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwOrgName + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA011"].ToString() + "</td>";
            //header += "</tr>";

            header += "<tr>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwApplierId + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA012"].ToString() + "</td>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwApplierName + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA013"].ToString() + "</td>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwApplierOrgId + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA014"].ToString() + "</td>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwApplierOrgName + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA015"].ToString() + "</td>";
            header += "</tr>";

            header += "</table>";

            return header;
        }
        else
        {
            return "";
        }
    }
    /*
    protected override void changeParameter()
    {
        base.changeParameter();
        string pLink = (string)Session["PortalLink"];
        if (pLink.IndexOf("runMethod=showSignForm") != -1)
        {
            int cid = int.Parse(Request.QueryString["CurPanelID"]) + 1;
            setSession("ParentPanelID", cid.ToString());
            setSession("DataListID", "ListTable");
            showPanelWindow("待處理資料匣", "../../../Program/DSCGPFlowService/Maintain/SMWL/Maintain.aspx?BoxID=InBox", 100, 100, "", true, true);
            Session["PortalLink"] = "";

            string str = "";

            str += "<script language=javascript>";
            str += "function spIN(){";
            str += "window.parent.setZIndex('" + Request.QueryString["CurPanelID"] + "');"; //這行可以把指定的panelID帶到最前端
            str += "}";
            str += "window.setTimeout('spIN()',1000);";
            str += "</script>";

            Type ctype = this.GetType();
            ClientScriptManager cm = Page.ClientScript;

            if (!cm.IsStartupScriptRegistered(ctype, "INSpecialScript"))
            {
                cm.RegisterStartupScript(ctype, "INSpecialScript", str);
            }

        }
    }
    */

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //if (Session["UserID"] != null) { 
        //    writeLog(new Exception("before general Webform Oninit"));
        //}

        //if (Session["UserID"] != null) { 
        //    writeLog(new Exception("after general Webform Oninit"));
        //}

    }


    protected string[] getDeptInfo(AbstractEngine engine, string userGUID)
    {
        string sql = "select u.id, u.organizationUnitName, isMain, u.OID from Functions inner join OrganizationUnit u on organizationUnitOID=u.OID where occupantOID='" + Utility.filter(userGUID) + "' and isMain='1'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[4];
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();
            result[1] = ds.Tables[0].Rows[0][1].ToString();
            result[2] = ds.Tables[0].Rows[0][2].ToString();
            result[3] = ds.Tables[0].Rows[0][3].ToString();
        }
        else
        {
            result[0] = "";
            result[1] = "";
            result[2] = "";
            result[3] = "";
        }
        return result;
    }

    protected override string getSignOpinion(System.Web.HttpServerUtility server, bool mDebugPage, AbstractEngine engine, string processSerialNumber, string opinionType, DataRow opinionSetting)
    {
        //*********************************************************************
        //註冊切換轉派意見的Script
        //*********************************************************************
        StringBuilder sstr = new StringBuilder();
        sstr.Append("<script language=javascript>");
        sstr.Append("function toogleOpinionDetail(workitempanel,event){");
        sstr.Append("   imgobj=(event.srcElement || event.target);;");
        sstr.Append("   workitemobj=eval('document.all.'+workitempanel);");
        sstr.Append("   workitemobj=eval(\"document.getElementById(\\'\" + workitempanel + \"')\");");

        sstr.Append("   if(workitemobj.style.display=='none'){");
        sstr.Append("       workitemobj.style.display='';");
        sstr.Append("       imgobj.src='" + Page.ResolveClientUrl("~/DSCWebControlRunTime/DSCWebControlImages/o.gif") + "';");
        sstr.Append("   }else{");
        sstr.Append("       workitemobj.style.display='none';");
        sstr.Append("       imgobj.src='" + Page.ResolveClientUrl("~/DSCWebControlRunTime/DSCWebControlImages/c.gif") + "';");
        sstr.Append("   }");
        sstr.Append("}");
        sstr.Append("</script>");

        ClientScriptManager cm = Page.ClientScript;
        Type ctype = Page.GetType();
        cm.RegisterStartupScript(ctype, "SignOpinionScript", sstr.ToString());

        //************************************************************************
        //第一步: 透過fetchFullProcInstanceWithSerialNoXML方法取得此流程的XML資訊
        //************************************************************************
        SysParam sp = new SysParam(engine);
        string flowType = getGPParam("FlowAdapter");
        string con1 = getGPParam("NaNaWebService");
        string con2 = getGPParam("DotJWebService");
        string account = getGPParam("FlowAccount");
        string password = getGPParam("FlowPassword");

        //國昌20100614:加入舊資料轉檔工具轉置的XML
        string opinionXML = "";
        bool isFromGP = false;

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
        adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = server.MapPath("~/LogFolder/" + fname + "_flowdata.log");
        adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, mDebugPage);

        try
        {
            //checkPoint("getSignOpinion Inside Start", modeFormLoad);
            opinionXML = adp.fetchFullProcInstanceWithSerialNoXML(processSerialNumber);

            //checkPoint("getSignOpinion Inside Done", modeFormLoad);
            isFromGP = true;
        }
        catch (Exception ue)
        {
            writeLog(ue);
            isFromGP = false;
        }

        adp.logout();

        //isFromGP = false;

        if (!isFromGP)
        {
            //由轉檔工具產生
            string YA003 = (string)getSession("PDID");
            string YA005 = processSerialNumber;

            //國昌20100614:加入舊資料轉檔工具轉置的XML(以下兩行測試用）
            //YA003 = "H3006";
            //YA005 = "0000000449";

            string sql = "select XML from SIGNRECORD inner join SMWYAAA on FORMID=SMWYAAA003 and SHEETNO=SMWYAAA002 where FORMID='" + YA003 + "' and SMWYAAA005 = '" + YA005 + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");

            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("EF SIGNRECORD Data Error");
            }
            opinionXML = ds.Tables[0].Rows[0][0].ToString();

            //writeLog(new Exception(opinionXML));
        }

        //國昌20100715:將opinionXML儲存到Session中
        setSession("OpinionXML", opinionXML);

        //************************************************************************
        //第二步: 將XML轉換成為PerformDetail物件陣列(包含ReassignWorkItem陣列)
        //************************************************************************
        ArrayList performDetailArray = new ArrayList();
        ArrayList temp2 = new ArrayList();
        string[,] Opinion;
        XMLProcessor xp = new XMLProcessor(opinionXML, 1);

        //以下測試用噓刪除
        if (xp.selectSingleNode("com.dsc.nana.services.webservice.ProcessInfo/state") == null)
        {
            writeLog(new Exception("NLLLLLL"));
        }
        string abortComment = "";
        bool isAbort = false;
        if (xp.selectSingleNode("com.dsc.nana.services.webservice.ProcessInfo/state").InnerText.Equals("closed.aborted"))
        {
            isAbort = true;
        }

        if (xp.selectSingleNode("com.dsc.nana.services.webservice.ProcessInfo/abortComment") != null)
        {
            abortComment = xp.selectSingleNodeText("com.dsc.nana.services.webservice.ProcessInfo/abortComment");
        }

        //處理ActInstanceInfo
        XmlNodeList xnl = xp.selectAllNodes("com.dsc.nana.services.webservice.ProcessInfo/actInstanceInfos/com.dsc.nana.services.webservice.ActInstanceInfo");
        foreach (XmlNode actInfo in xnl)
        {
            //確認要不要處理各項關卡設定
            string types = actInfo.SelectSingleNode("performType").InnerText;
            bool isProcess = false;
            if (types.Equals("NORMAL"))
            {
                if (opinionSetting["SMWDAAA301"].ToString().Equals("Y"))
                {
                    isProcess = true;
                }
            }
            else if (types.Equals("NOTICE"))
            {
                if (opinionSetting["SMWDAAA302"].ToString().Equals("Y"))
                {
                    isProcess = true;
                }
            }
            else
            {
                if (opinionSetting["SMWDAAA303"].ToString().Equals("Y"))
                {
                    isProcess = true;
                }
            }
            if (!isProcess)
            {
                continue;
            }

            //處理performInfo
            XmlNodeList xnl2 = actInfo.SelectNodes("performInfos/com.dsc.nana.services.webservice.PerformInfo");
            foreach (XmlNode performInfo in xnl2)
            {
                //處理performDetail
                XmlNodeList xnl3 = performInfo.SelectNodes("performDetails/com.dsc.nana.services.webservice.PerformDetail");
                foreach (XmlNode performDetail in xnl3)
                {
                    PerformDetail pd = new PerformDetail();
                    pd.performType = actInfo.SelectSingleNode("performType").InnerText;
                    pd.activityName = actInfo.SelectSingleNode("activityName").InnerText;
                    pd.performerName = performDetail.SelectSingleNode("performerName").InnerText;
                    string rs = "";
                    if (performDetail.SelectSingleNode("comment") != null)
                    {
                        rs = performDetail.SelectSingleNode("comment").InnerText;
                    }
                    if (rs.Equals(""))
                    {
                        pd.executiveResult = "";
                        pd.comment = "";
                    }
                    else
                    {
                        string[] rss = rs.Split(new string[] { "##" }, StringSplitOptions.None);
                        if (rss.Length == 2)
                        {
                            pd.executiveResult = rss[0];
                            pd.comment = rss[1];
                        }
                        else
                        {
                            pd.executiveResult = "";
                            pd.comment = rs;
                        }
                    }
                    if (performDetail.SelectSingleNode("performedTime") != null)
                    {
                        if (!performDetail.SelectSingleNode("performedTime").InnerText.Equals("") && !performDetail.SelectSingleNode("performedTime").InnerText.Equals("NULL"))
                        {
                            pd.performedTime = DateTimeUtility.convertDateTimeToString(DateTime.Parse(performDetail.SelectSingleNode("performedTime").InnerText));
                        }
                        else
                        {
                            pd.performedTime = "";
                        }
                    }
                    else
                    {
                        pd.performedTime = "";
                    }
                    pd.state = performInfo.SelectSingleNode("state").InnerText;
                    if (performDetail.SelectSingleNode("publicNotifiedName") != null)
                    {
                        pd.notifiedName = performDetail.SelectSingleNode("publicNotifiedName").InnerText;
                        pd.publicNotifiedName = performDetail.SelectSingleNode("publicNotifiedName").InnerText;
                    }
                    else
                    {
                        if (performDetail.SelectSingleNode("NotifiedName") != null)
                        {
                            pd.notifiedName = performDetail.SelectSingleNode("NotifiedName").InnerText;
                            pd.publicNotifiedName = performDetail.SelectSingleNode("NotifiedName").InnerText;
                        }
                    }
                    if (!performDetail.SelectSingleNode("createdTime").InnerText.Equals(""))
                    {
                        pd.createdTime = DateTimeUtility.convertDateTimeToString(DateTime.Parse(performDetail.SelectSingleNode("createdTime").InnerText));
                    }
                    else
                    {
                        pd.createdTime = "";
                    }

                    if (pd.createdTime.Equals(""))
                    {
                        pd.processTime = "0";
                    }
                    else
                    {
                        DateTime startdt = DateTime.Parse(pd.createdTime);
                        DateTime enddt;
                        if (pd.performedTime.Equals(""))
                        {
                            enddt = DateTime.Now;
                        }
                        else
                        {
                            enddt = DateTime.Parse(pd.performedTime);
                        }
                        TimeSpan ts = enddt.Subtract(startdt);
                        pd.processTime = Utility.Round(ts.TotalHours, 2).ToString();
                    }

                    //處理reassignment
                    temp2 = new ArrayList();
                    XmlNodeList xnl4 = performDetail.SelectNodes("reassignWorkItemRecords/com.dsc.nana.data__transfer.ReassignmentInfoForListDTO");
                    foreach (XmlNode workItemRecord in xnl4)
                    {
                        //確認處理各項轉派
                        string type2 = workItemRecord.SelectSingleNode("reassignmentType/value").InnerText;
                        bool isProcess2 = false;
                        if (type2.Equals("0"))
                        {
                            if (opinionSetting["SMWDAAA304"].ToString().Equals("Y"))
                            {
                                isProcess2 = true;
                            }
                        }
                        else if (type2.Equals("1"))
                        {
                            if (opinionSetting["SMWDAAA305"].ToString().Equals("Y"))
                            {
                                isProcess2 = true;
                            }
                        }
                        else if (type2.Equals("2"))
                        {
                            if (opinionSetting["SMWDAAA306"].ToString().Equals("Y"))
                            {
                                isProcess2 = true;
                            }
                        }
                        else if (type2.Equals("3"))
                        {
                            if (opinionSetting["SMWDAAA307"].ToString().Equals("Y"))
                            {
                                isProcess2 = true;
                            }
                        }
                        else if (type2.Equals("4"))
                        {
                            if (opinionSetting["SMWDAAA308"].ToString().Equals("Y"))
                            {
                                isProcess2 = true;
                            }
                        }
                        else if (type2.Equals("5"))
                        {
                            if (opinionSetting["SMWDAAA309"].ToString().Equals("Y"))
                            {
                                isProcess2 = true;
                            }
                        }
                        else if (type2.Equals("6"))
                        {
                            if (opinionSetting["SMWDAAA310"].ToString().Equals("Y"))
                            {
                                isProcess2 = true;
                            }
                        }
                        else if (type2.Equals("7"))
                        {
                            if (opinionSetting["SMWDAAA311"].ToString().Equals("Y"))
                            {
                                isProcess2 = true;
                            }
                        }
                        else
                        {
                            if (opinionSetting["SMWDAAA312"].ToString().Equals("Y"))
                            {
                                isProcess2 = true;
                            }
                        }

                        if (!isProcess2)
                        {
                            continue;
                        }

                        ReAssignedWorkItem rw = new ReAssignedWorkItem();
                        rw.reassignmentType = workItemRecord.SelectSingleNode("reassignmentType/value").InnerText;
                        rw.reassignFromUserId = workItemRecord.SelectSingleNode("reassignFromUserId").InnerText;
                        rw.reassignFromUserName = workItemRecord.SelectSingleNode("reassignFromUserName").InnerText;
                        rw.reassignToUserId = workItemRecord.SelectSingleNode("reassignToUserId").InnerText;
                        rw.reassignToUserName = workItemRecord.SelectSingleNode("reassignToUserName").InnerText;
                        if (workItemRecord.SelectSingleNode("comment") != null)
                        {
                            rw.comment = workItemRecord.SelectSingleNode("comment").InnerText;
                        }
                        else
                        {
                            rw.comment = "";
                        }
                        //國昌20100614:加入舊資料轉檔工具轉置的XML-因為EF不見得有reassignedTime
                        try
                        {
                            rw.reassignedTime = DateTimeUtility.convertDateTimeToString(DateTime.Parse(workItemRecord.SelectSingleNode("reassignedTime").InnerText));
                        }
                        catch
                        {
                            rw.reassignedTime = "";
                        }
                        temp2.Add(rw);
                    }
                    ReAssignedWorkItem[] ary = new ReAssignedWorkItem[temp2.Count];
                    for (int x = 0; x < temp2.Count; x++)
                    {
                        ary[x] = (ReAssignedWorkItem)temp2[x];
                    }
                    pd.record = ary;

                    performDetailArray.Add(pd);
                }
            }
        }

        //************************************************************************
        //第三步: 排序(使用插入排序法)
        //************************************************************************
        //排序PerformDetail
        ArrayList newPerformDetailArray = new ArrayList();
        for (int i = 0; i < performDetailArray.Count; i++)
        {
            PerformDetail cur = (PerformDetail)performDetailArray[i];
            if (newPerformDetailArray.Count == 0)
            {
                newPerformDetailArray.Add(cur);
            }
            else
            {
                int index = -1;
                for (int j = 0; j < newPerformDetailArray.Count; j++)
                {
                    PerformDetail newd = (PerformDetail)newPerformDetailArray[j];
                    DateTime newdt = DateTime.Parse(newd.createdTime);
                    DateTime curdt = DateTime.Parse(cur.createdTime);

                    if (opinionSetting["SMWDAAA315"].ToString().Equals("A"))
                    {
                        //昇冪
                        if (newdt.CompareTo(curdt) > 0)
                        {
                            index = j;
                            break;
                        }
                        else if (newdt.Equals(curdt))
                        {
                            if (!isFromGP)//EasyFlow簽核資料須依照關卡與支號排列 ; 而非使用處理時間
                            {
                                //取關號
                                int newdKuangNum = int.Parse(newd.activityName.Substring(0, 4));
                                int curKuangNum = int.Parse(cur.activityName.Substring(0, 4));
                                //支號
                                int newdBranchNum = int.Parse(newd.activityName.Substring(newd.activityName.IndexOf("支號") + 3, 4));
                                int curBranchNum = int.Parse(cur.activityName.Substring(cur.activityName.IndexOf("支號") + 3, 4));
                                if (curKuangNum < newdKuangNum)
                                {
                                    if (curBranchNum < newdBranchNum)
                                    {
                                        index = j;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (newd.performedTime.Equals(""))
                                {
                                    newdt = DateTime.MaxValue;
                                }
                                else
                                {
                                    newdt = DateTime.Parse(newd.performedTime);
                                }
                                if (cur.performedTime.Equals(""))
                                {
                                    curdt = DateTime.MaxValue;
                                }
                                else
                                {
                                    curdt = DateTime.Parse(cur.performedTime);
                                }

                                if (newdt.CompareTo(curdt) >= 0)
                                {
                                    index = j;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        //降冪
                        if (newdt.CompareTo(curdt) < 0)
                        {
                            index = j;
                            break;
                        }
                        else if (newdt.Equals(curdt))
                        {
                            if (!isFromGP)//EasyFlow簽核資料須依照關卡與支號排列 ; 而非使用處理時間
                            {
                                //取關號
                                int newdKuangNum = int.Parse(newd.activityName.Substring(0, 4));
                                int curKuangNum = int.Parse(cur.activityName.Substring(0, 4));
                                //支號
                                int newdBranchNum = int.Parse(newd.activityName.Substring(newd.activityName.IndexOf("支號") + 3, 4));
                                int curBranchNum = int.Parse(cur.activityName.Substring(cur.activityName.IndexOf("支號") + 3, 4));
                                if (curKuangNum > newdKuangNum)
                                {
                                    index = j;
                                    break;
                                }
                                else if (curKuangNum == newdKuangNum)
                                {
                                    if (curBranchNum > newdBranchNum)
                                    {
                                        index = j;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (newd.performedTime.Equals(""))
                                {
                                    newdt = DateTime.MaxValue;
                                }
                                else
                                {
                                    newdt = DateTime.Parse(newd.performedTime);
                                }
                                if (cur.performedTime.Equals(""))
                                {
                                    curdt = DateTime.MaxValue;
                                }
                                else
                                {
                                    curdt = DateTime.Parse(cur.performedTime);
                                }

                                if (newdt.CompareTo(curdt) <= 0)
                                {
                                    index = j;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (index == -1)
                {
                    newPerformDetailArray.Add(cur);
                }
                else
                {
                    newPerformDetailArray.Insert(index, cur);
                }
            }
        }

        //排序WorkItemRecord
        for (int i = 0; i < newPerformDetailArray.Count; i++)
        {
            PerformDetail pd = (PerformDetail)newPerformDetailArray[i];
            if (pd.record.Length > 0)
            {
                ArrayList temp = new ArrayList();
                for (int j = 0; j < pd.record.Length; j++)
                {
                    ReAssignedWorkItem cur = pd.record[j];
                    if (temp.Count == 0)
                    {
                        temp.Add(cur);
                    }
                    else
                    {
                        int index = -1;
                        for (int x = 0; x < temp.Count; x++)
                        {
                            ReAssignedWorkItem newd = (ReAssignedWorkItem)temp[x];
                            DateTime newdt = DateTime.Parse(newd.reassignedTime);
                            DateTime curdt = DateTime.Parse(cur.reassignedTime);
                            if (opinionSetting["SMWDAAA316"].ToString().Equals("A"))
                            {
                                //昇冪
                                if (newdt.CompareTo(curdt) >= 0)
                                {
                                    index = x;
                                    break;
                                }
                            }
                            else
                            {
                                //降冪
                                if (newdt.CompareTo(curdt) <= 0)
                                {
                                    index = x;
                                    break;
                                }

                            }
                        }
                        if (index == -1)
                        {
                            temp.Add(cur);
                        }
                        else
                        {
                            temp.Insert(index, cur);
                        }
                    }
                }

                ReAssignedWorkItem[] ary = new ReAssignedWorkItem[temp.Count];
                for (int j = 0; j < ary.Length; j++)
                {
                    ary[j] = (ReAssignedWorkItem)temp[j];
                }
                pd.record = ary;
            }
        }

        //Jack 20130418:準備一變數存放流程相關參與者
        string flowPerformers = "";


        //國昌20100715:將newPerformDetailArray儲存到Session中
        setSession("PerformDetailArray", newPerformDetailArray);

        //************************************************************************
        //第四步: 再將PerformDetail物件陣列轉換成為HTML
        //************************************************************************

        //計算會顯示的欄位數
        int columnCount = 0;
        if (opinionSetting["SMWDAAA321"].ToString().Equals("Y"))
        {
            columnCount++;
        }
        if (opinionSetting["SMWDAAA322"].ToString().Equals("Y"))
        {
            columnCount++;
        }
        if (opinionSetting["SMWDAAA323"].ToString().Equals("Y"))
        {
            columnCount++;
        }
        if (opinionSetting["SMWDAAA324"].ToString().Equals("Y"))
        {
            columnCount++;
        }
        if (opinionSetting["SMWDAAA325"].ToString().Equals("Y"))
        {
            columnCount++;
        }
        if (opinionSetting["SMWDAAA326"].ToString().Equals("Y"))
        {
            columnCount++;
        }
        if (opinionSetting["SMWDAAA327"].ToString().Equals("Y"))
        {
            columnCount++;
        }
        if (opinionSetting["SMWDAAA328"].ToString().Equals("Y"))
        {
            columnCount++;
        }
        if (opinionSetting["SMWDAAA329"].ToString().Equals("Y"))
        {
            columnCount++;
        }
        if (opinionSetting["SMWDAAA330"].ToString().Equals("Y"))
        {
            columnCount++;
        }
        if (columnCount < 2)
        {
            columnCount = 1;
        }
        else
        {
            columnCount -= 1;
        }
        StringBuilder sbHtml = new StringBuilder();
        string html = "";

        if (isAbort)
        {
            sbHtml.Append("<table width=100% border=0  cellspacing=0 cellpadding=2 style=\"border-left-style:solid;border-top-style:solid;border-width:1px\" class=OpinionBorder>");
            sbHtml.Append("<tr>");
            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead width=100px>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString1", "流程撤銷意見") + "</td>");
            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(abortComment)) + "</td>");
            sbHtml.Append("</tr>");
            sbHtml.Append("</table>");
            sbHtml.Append("<br>");
        }

        sbHtml.Append("<table width=100% border=0  cellspacing=0 cellpadding=2 style=\"border-left-style:solid;border-top-style:solid;border-width:1px\" class=OpinionBorder>");
        sbHtml.Append("<tr>");
        sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>&nbsp;</td>");
        if (opinionSetting["SMWDAAA321"].ToString().Equals("Y"))
        {
            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString2", "類型") + "</td>");
        }
        if (opinionSetting["SMWDAAA322"].ToString().Equals("Y"))
        {
            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString3", "關卡名稱") + "</td>");
        }
        if (opinionSetting["SMWDAAA323"].ToString().Equals("Y"))
        {
            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString4", "處理者") + "</td>");
        }
        if (opinionSetting["SMWDAAA324"].ToString().Equals("Y"))
        {
            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString5", "處理結果") + "</td>");
        }
        if (opinionSetting["SMWDAAA325"].ToString().Equals("Y"))
        {
            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString6", "處理意見") + "</td>");
        }
        if (opinionSetting["SMWDAAA326"].ToString().Equals("Y"))
        {
            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString7", "處理時間") + "</td>");
        }
        if (opinionSetting["SMWDAAA327"].ToString().Equals("Y"))
        {
            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString8", "狀態") + "</td>");
        }
        if (opinionSetting["SMWDAAA328"].ToString().Equals("Y"))
        {
            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString9", "轉寄") + "</td>");
        }
        if (opinionSetting["SMWDAAA329"].ToString().Equals("Y"))
        {
            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString10", "開始時間") + "</td>");
        }
        if (opinionSetting["SMWDAAA330"].ToString().Equals("Y"))
        {
            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString11", "處理時數") + "</td>");
        }
        sbHtml.Append("</tr>");

        bool isOpen = false;
        if (opinionSetting["SMWDAAA300"].ToString().Equals("Y"))
        {
            isOpen = true;
        }
        Opinion = new string[newPerformDetailArray.Count, 4];
        for (int i = 0; i < newPerformDetailArray.Count; i++)
        {
            PerformDetail pd = (PerformDetail)newPerformDetailArray[i];
            sbHtml.Append("<tr>");

            bool hasChild = false;

            if (pd.record.Length > 0)
            {
                hasChild = true;
            }

            string toogleStr = "&nbsp;";
            if (hasChild)
            {
                toogleStr = "<img src='" + Page.ResolveUrl("~/DSCWebControlRunTime/DSCWebControlImages/");
                if (isOpen)
                {
                    toogleStr += "o.gif' ";
                }
                else
                {
                    toogleStr += "c.gif' ";
                }
                toogleStr += " style=\"cursor:pointer\" onclick='toogleOpinionDetail(\"WORKITEMRECORD_" + i.ToString() + "\",event);'>";

            }
            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail align=center valign=top>" + toogleStr + "</td>");
            if (opinionSetting["SMWDAAA321"].ToString().Equals("Y"))
            {
                if (pd.performType.Equals("NORMAL"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString12", "一般") + "</td>");
                }
                else if (pd.performType.Equals("NOTICE"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString13", "通知") + "</td>");
                }
                else
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString14", "執行") + "</td>");
                }
            }
            if (opinionSetting["SMWDAAA322"].ToString().Equals("Y"))
            {
                //國昌20100715:流程圖
                if ((pd.activityName.Length > 11) && (pd.activityName.IndexOf("(") == 11))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(pd.activityName.Substring(0, 11))) + "</td>");
                }
                else
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(pd.activityName)) + "</td>");
                }
                Opinion[i, 0] = fixNbspS(Page.Server.HtmlEncode(pd.activityName));
            }
            if (opinionSetting["SMWDAAA323"].ToString().Equals("Y"))
            {
                //國昌20100715:流程圖
                if (pd.performerName.IndexOf(":") == 4)
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(pd.performerName.Substring(5, pd.performerName.Length - 5))) + "</td>");
                }
                else
                {
                    if (pd.record.Length > 0)
                    {
                        ReAssignedWorkItem rw = pd.record[0];
                        if (rw.reassignmentType.Equals("1") || rw.reassignmentType.Equals("3") || rw.reassignmentType.Equals("4"))
                        {
                            pd.performerName = pd.performerName + " (代)";
                        }
                    }
                    string pid = pd.performerName.Split('_')[0];
                    string url = getPersonalImage(engine, pid);
                    if (url.Length > 0 && (fixNbspS(Page.Server.HtmlEncode(pd.executiveResult)) == "同意" || fixNbspS(Page.Server.HtmlEncode(pd.executiveResult)) == "退回重辦"))
                    {
                        string image = "<br/><img width=\"150\" height=\"40\" src='" + url + "' /> ";//For Personal Image
                        sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(pd.performerName)) + image + "</td>");
                    }
                    else
                    {
                        sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(pd.performerName)) + "</td>");
                    }
                }
                Opinion[i, 1] = fixNbspS(Page.Server.HtmlEncode(pd.performerName));
                flowPerformers += pd.performerName + ";";
            }
            if (opinionSetting["SMWDAAA324"].ToString().Equals("Y"))
            {
                sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(pd.executiveResult)) + "</td>");
                Opinion[i, 2] = fixNbspS(Page.Server.HtmlEncode(pd.executiveResult));
            }
            if (opinionSetting["SMWDAAA325"].ToString().Equals("Y"))
            {
                sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(pd.comment)) + "</td>");
                Opinion[i, 3] = fixNbspS(Page.Server.HtmlEncode(pd.comment));
            }
            if (opinionSetting["SMWDAAA326"].ToString().Equals("Y"))
            {
                sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(pd.performedTime)) + "</td>");
            }
            if (opinionSetting["SMWDAAA327"].ToString().Equals("Y"))
            {
                //open.not_running.not_started:未開始; open.running:進行中; open.not_running.suspended:已暫停; closed.completed:已完成; closed.aborted:已撤銷; closed.terminated:已中止
                if (pd.state.Equals("open.not_running.not_started"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString15", "未開始") + "</td>");
                }
                else if (pd.state.Equals("open.running"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString16", "進行中") + "</td>");
                }
                else if (pd.state.Equals("open.running.not_performed"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString17", "進行中") + "</td>");
                }
                else if (pd.state.Equals("open.running.performing"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString18", "進行中") + "</td>");
                }
                else if (pd.state.Equals("open.not_running.suspended"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString19", "已暫停") + "</td>");
                }
                else if (pd.state.Equals("closed.completed"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString20", "已完成") + "</td>");
                }
                else if (pd.state.Equals("closed.aborted"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString21", "已撤銷") + "</td>");
                }
                else if (pd.state.Equals("closed.terminated"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString22", "已中止") + "</td>");
                }
                else
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + ShowOtherState(fixNbspS(Page.Server.HtmlEncode(pd.state))) + "</td>");
                }
            }
            if (opinionSetting["SMWDAAA328"].ToString().Equals("Y"))
            {
                sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(pd.publicNotifiedName)) + "</td>");
                flowPerformers += pd.publicNotifiedName + ";";
                flowPerformers += pd.privateNotifiedName + ";";
            }
            if (opinionSetting["SMWDAAA329"].ToString().Equals("Y"))
            {
                sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(pd.createdTime)) + "</td>");
            }
            if (opinionSetting["SMWDAAA330"].ToString().Equals("Y"))
            {
                sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(pd.processTime)) + "</td>");
            }
            sbHtml.Append("</tr>");

            if (pd.record.Length > 0)
            {
                sbHtml.Append("<tbody id='WORKITEMRECORD_" + i.ToString() + "' style=\"display:");
                if (isOpen)
                {
                    sbHtml.Append("inline\">");
                }
                else
                {
                    sbHtml.Append("none\">");
                }
                sbHtml.Append("<tr>");
                sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none;border-right-style:none\" class=OpinionDetail colspan=2>&nbsp;</td>");
                sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail colspan=" + columnCount.ToString() + ">");


                sbHtml.Append("<table width=90% border=0  cellspacing=0 cellpadding=2 style=\"border-left-style:solid;border-top-style:solid;border-width:1px\" class=OpinionBorder>");
                sbHtml.Append("<tr>");

                if (opinionSetting["SMWDAAA331"].ToString().Equals("Y"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString23", "類型") + "</td>");
                }
                if (opinionSetting["SMWDAAA332"].ToString().Equals("Y"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString24", "處理者") + "</td>");
                }
                if (opinionSetting["SMWDAAA333"].ToString().Equals("Y"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString25", "受託者") + "</td>");
                }
                if (opinionSetting["SMWDAAA334"].ToString().Equals("Y"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString26", "處理意見") + "</td>");
                }
                if (opinionSetting["SMWDAAA335"].ToString().Equals("Y"))
                {
                    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionHead>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString27", "處理時間") + "</td>");
                }
                sbHtml.Append("</tr>");

                for (int j = 0; j < pd.record.Length; j++)
                {
                    ReAssignedWorkItem rw = pd.record[j];
                    sbHtml.Append("<tr>");

                    if (opinionSetting["SMWDAAA331"].ToString().Equals("Y"))
                    {
                        /// 轉派類型.
                        /// 0:直接轉派
                        /// 1:系統代理人轉派
                        /// 2:系統逾時轉派
                        /// 3:管理員代理轉派
                        /// 4:負責人代理轉派
                        /// 5:工作取回
                        /// 6:工作轉派
                        /// 7:管理員工作轉派
                        /// 8:負責人工作轉派
                        /// 9:系統永久代理轉派
                        if (rw.reassignmentType.Equals("0"))
                        {
                            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString28", "直接轉派") + "</td>");
                        }
                        else if (rw.reassignmentType.Equals("1"))
                        {
                            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString29", "系統代理人轉派") + "</td>");
                        }
                        else if (rw.reassignmentType.Equals("2"))
                        {
                            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString30", "系統逾時轉派") + "</td>");
                        }
                        else if (rw.reassignmentType.Equals("3"))
                        {
                            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString31", "管理員代理轉派") + "</td>");
                        }
                        else if (rw.reassignmentType.Equals("4"))
                        {
                            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString32", "負責人代理轉派") + "</td>");
                        }
                        else if (rw.reassignmentType.Equals("5"))
                        {
                            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString33", "工作取回") + "</td>");
                        }
                        else if (rw.reassignmentType.Equals("6"))
                        {
                            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString34", "工作轉派") + "</td>");
                        }
                        else if (rw.reassignmentType.Equals("7"))
                        {
                            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString35", "系統工作轉派") + "</td>");
                        }
                        else if (rw.reassignmentType.Equals("8"))
                        {
                            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString36", "負責人工作轉派") + "</td>");
                        }
                        //start 2009/04/30 hjlin
                        else if (rw.reassignmentType.Equals("9"))
                        {
                            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString37", "系統永久代理轉派") + "</td>");
                        }
                        else
                        {
                            sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>&nbsp;</td>");
                        }
                        //end 2009/04/30 hjlin
                    }
                    if (opinionSetting["SMWDAAA332"].ToString().Equals("Y"))
                    {
                        sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(rw.reassignFromUserId + "_" + rw.reassignFromUserName) + "</td>");
                        flowPerformers += rw.reassignFromUserId + ";";
                    }
                    if (opinionSetting["SMWDAAA333"].ToString().Equals("Y"))
                    {
                        sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(rw.reassignToUserId + "_" + rw.reassignToUserName) + "</td>");
                        flowPerformers += rw.reassignToUserId + ";";
                    }
                    if (opinionSetting["SMWDAAA334"].ToString().Equals("Y"))
                    {
                        sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(rw.comment) + "</td>");
                    }
                    if (opinionSetting["SMWDAAA335"].ToString().Equals("Y"))
                    {
                        sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(rw.reassignedTime) + "</td>");
                    }
                    sbHtml.Append("</tr>");
                }

                sbHtml.Append("</table>");

                sbHtml.Append("</td>");
                sbHtml.Append("</tr>");
                sbHtml.Append("</tbody>");
            }
        }
        if (!string.IsNullOrEmpty(Convert.ToString(Page.Request.QueryString["ParentPageUID"])))
        {
            string SubstituteID = Convert.ToString(getSession(Convert.ToString(Page.Request.QueryString["ParentPageUID"]), "flowPerformers"));
            if (!string.IsNullOrEmpty(SubstituteID))
            {
                flowPerformers += SubstituteID;
            }
        }
        if (!string.IsNullOrEmpty(Convert.ToString(Page.Request.QueryString["mailflowPerformers"])))
        {
            string SubstituteID = Convert.ToString(Page.Request.QueryString["mailflowPerformers"]);
            if (!string.IsNullOrEmpty(SubstituteID) && SubstituteID.IndexOf((string)Session["UserID"]) > -1)
            {
                flowPerformers += SubstituteID;
            }
        }
        setSession("WorkflowOpinion", Opinion);
        setSession("flowPerformers", flowPerformers.Substring(0, flowPerformers.Length - 1));
        sbHtml.Append("</table>");
        sbHtml.Append("</span>");

        return sbHtml.ToString();
    }
    private string fixNbspS(string ori)
    {
        ori = fixNullS(ori);
        if (ori.Equals(""))
        {
            return "&nbsp;";
        }
        else
        {
            return ori;
        }
    }
    private string fixNullS(string ori)
    {
        if (ori == null)
        {
            return "";
        }
        else
        {
            return ori;
        }
    }
    protected string getPersonalImage(AbstractEngine engine, string performerID)
    {
        string sql = "";
        sql += "Select IDENTITYID,FILEEXT ";
        sql += "from FILEITEM ";
        sql += "inner join Users ";
        sql += "on Users.OID=JOBID ";
        sql += "where Users.id='" + performerID + "' ";
        DataSet ds = engine.getDataSet(sql, "tmp");
        DataTable dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            string fileName = dt.Rows[0]["IDENTITYID"].ToString() + "." + dt.Rows[0]["FILEEXT"].ToString();
            return Page.ResolveClientUrl("~/Personal/" + dt.Rows[0]["IDENTITYID"].ToString()) + "//" + fileName;
        }
        return "";
    }

    /// <summary>
    /// 顯示其他簽核狀態(整合EasyFlow)
    /// </summary>
    /// <param name="state">state</param>
    /// <returns>簽核狀態說明</returns>
    protected virtual string ShowOtherState(string state)
    {
        string ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString51", "未定義") + "(" + state + ")";
        try
        {
            switch (state)
            {
                case "1":
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString38", "未傳送");
                    break;
                case "2":
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString39", "已傳送");
                    break;
                case "3":
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString40", "已讀取");
                    break;
                case "4":
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString41", "已通知");
                    break;
                case "5":
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString42", "ByPpass");
                    break;
                case "6":
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString43", "已會辦");
                    break;
                case "7":
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString44", "已簽核");
                    break;
                case "8":
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString45", "已撤簽");
                    break;
                case "9":
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString46", "已執行");
                    break;
                case "10":
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString47", "已退件");
                    break;
                case "11":
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString48", "已抽單");
                    break;
                case "12":
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString49", "他人已簽核");
                    break;
                case "13":
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString50", "他人已簽核");
                    break;
                default:
                    ret = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "OpinionString51", "未定義") + "(" + state + ")";
                    break;
            }
        }
        catch
        { }
        return ret;

    }
    //客製程式碼
    private string getclickRow(int nextstep)
    {
        string ret = "-1";
        int Row = 0;
        try
        {
            if (nextstep < 0 && !string.IsNullOrEmpty(Convert.ToString(Session["clickRow"])))
            {
                //上一筆
                Row = Convert.ToInt32(Session["clickRow"]);
                Row += nextstep;
                if (Row < 0)
                {
                    ret = "a";
                }
                else
                {
                    ret = Convert.ToString(Row);
                    Session["clickRow"] = Convert.ToInt32(ret);

                }
            }
            else if (nextstep > 0 && !string.IsNullOrEmpty(Convert.ToString(Session["clickRow"])))
            {
                //下一筆
                Row = Convert.ToInt32(Session["clickRow"]);
                Row += nextstep;
                ret = Convert.ToString(Row);
                Session["clickRow"] = Convert.ToInt32(ret);
            }
            else
            {
                Row = Convert.ToInt32(Session["clickRow"]);
                Row += nextstep;
                ret = Convert.ToString(Row);
                Session["clickRow"] = Convert.ToInt32(ret);
            }

        }
        catch (Exception eee)
        {
            ret = "-1";
            //MessageBox(eee.Message);
        }

        /*
        if (Session["clickRow"]  == 10 || Session["clickRow"]  == dos.count)
        {
            ret = 1
        }
        if(dos.count > pageSize && Session["clickRow"]  == 1 )
        {
            ret = 10
        } 
        else
        {
            ret = dos.count
        }
        */


        return ret;
    }
    //使用者自訂按鈕一 往後定義為 上一筆 功能
    protected override void userDefineProcedure1()
    {
        executeScript("PreSign();");
    }
    //使用者自訂按鈕一 往後定義為 下一筆 功能
    protected override void userDefineProcedure2()
    {
        executeScript("NextSign();");
    }

    /// <summary>
    /// 退回重辦程序
    /// </summary>
    protected override void rejectProcedure()
    {
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            //代理轉派動作
            reassignmentSubstitute(engine);

            //送單程序

            string flowOID = (string)getSession("FlowGUID"); //流程實例序號
            string WorkItemOID = (string)getSession("WorkItemOID");//工作項目識別號


            string ACTID = fetchActivityIDFromWorkItemOID(engine, WorkItemOID, (string)getSession("PDID"), (string)Session["UserID"]);

            //簽核程序
            engine = factory.getEngine(engineType, connectString);

            string backActID = (string)Session["tempBackActID"];
            string backOpinion = (string)Session["tempBackOpinion"];
            string backType = (string)Session["tempBackType"];


            reexecuteActivity(engine, (string)Session["UserID"], flowOID, WorkItemOID, ACTID, backActID, backOpinion, backType);

            engine.close();

            //儲存成功
            //Response.Write("alert('" + RejectSuccessMsg + "');");
            MessageBox(RejectSuccessMsg);


            string needCloseRefreshClick = Convert.ToString(getSession("closeRefreshClick")); //送簽後動作
            if (needCloseRefreshClick.Equals("N"))
            {
                //closeRefreshClick();
            }
            else
            {
                //closeRefresh();
                closeRefreshClick();
            }

            refreshInbox();
        }
        catch (Exception ue)
        {
            try
            {
                engine.close();
            }
            catch { };
            processErrorMessage(errorLevel, ue);
        }
    }

    /// <summary>
    /// 執行代理轉派動作
    /// </summary>
    public void reassignmentSubstitute(com.dsc.kernal.factory.AbstractEngine engine)
    {
        //檢查是否為被代理之流程
        if (isSubstituteSign)
        {
            //進行轉派動作
            SysParam sp = new SysParam(engine);
            string flowType = sp.getParam("FlowAdapter");
            string con1 = sp.getParam("NaNaWebService");
            string con2 = sp.getParam("DotJWebService");
            string account = sp.getParam("FlowAccount");
            string password = sp.getParam("FlowPassword");

            FlowFactory ff = new FlowFactory();
            AbstractFlowAdapter adp = ff.getAdapter(flowType);
            adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
            adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

            string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
            fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

            adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);


            //Mantis0020030 
            string getOrgPerformer = "";
            getOrgPerformer += "Select Users.id from WorkAssignment ";
            getOrgPerformer += "join Users on assigneeOID =Users.OID ";
            getOrgPerformer += "where WorkAssignment.OID='" + com.dsc.kernal.utility.Utility.filter((string)getSession("workAssignmentOID")) + "' ";

            Object objResult = engine.executeScalar(getOrgPerformer);
            if (objResult != null)
            {
                acceptWorkItem(engine, (string)objResult, (string)getSession("WorkItemOID"));
            }
            adp.managementReassignWorkItem((string)Session["UserGUID"], (string)getSession("WorkItemOID"), "系統自動轉派至代理人");
            adp.logout();
        }
    }

    private void acceptWorkItem(AbstractEngine engine, string pUserID, string pWorkItemOID)
    {
        SysParam sp = new SysParam(engine);
        string flowType = sp.getParam("FlowAdapter");
        string con1 = sp.getParam("NaNaWebService");
        string con2 = sp.getParam("DotJWebService");
        string account = sp.getParam("FlowAccount");
        string password = sp.getParam("FlowPassword");

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
        adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

        adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

        adp.acceptWorkItem(pUserID, pWorkItemOID);

        adp.logout();
    }

    // SMP客製
    //====================================================================================================
    /// <summary>
    /// 刷新收件資料匣
    /// </summary>
    protected void refreshInbox()
    {
        string js = "";
        js += "var outlookWindow = window.parent.getPanelWindowObject('0') ;";
        js += "try {";
        //if (this.GetType() == typeof(SmpErpFormPage))
        //{
        //    js += "     outlookWindow.refreshErpInbox();";
        //}
        //else
        //{
            js += "     outlookWindow.refreshInbox();";
        //}
        js += "}";
        //js += "    catch(e) {alert(e.message);};";  //20170504 跨廠簽核刷新件數, 當異常時不顯示exception
		js += "    catch(e) {};";
        js += "";
        Response.Write(js);
    }
}
