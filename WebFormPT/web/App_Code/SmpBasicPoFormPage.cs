using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

using WebServerProject;
using com.dsc.kernal.factory;
using com.dsc.kernal.databean;
using com.dsc.kernal.agent;
using com.dsc.kernal.utility;
using com.dsc.flow.data;
using com.dsc.flow.server;
using WebServerProject;
using WebServerProject.auth;
using WebServerProject.flow.SMWP;


/// <summary>
/// BasicFormPage 的摘要描述
/// </summary>
public class SmpBasicPoFormPage : BaseWebUI.GeneralWebForm
{
    #region 繼承類別必須設定變數
    public string attachFileConfirmSaveLevel1 = "";
    #endregion
    #region 私有變數
    private ArrayList errMsg = new ArrayList();
    #endregion

    protected void setSignMode(string simMode)
    {
        if (!simMode.Equals(""))
        {
            string tempUIStatus = fixNull(Request.QueryString["UIStatus"]);
            Session["SIMMODE"] = simMode;
            setSession("SIMMODE", Session["SIMMODE"]);
            setProcessForm(tempUIStatus);
            initToolBar();
        }
    }

    private void setProcessForm(string UIStatus)
    {
        //特別處理AgreeButton & DisagreeButton
        AgreeButton.Display = false;
        DisagreeButton.Display = false;
        if ((UIStatus.Equals(ProcessNew)) || (UIStatus.Equals(ProcessModify)))
        {
            string simmode = (String)Session["SIMMODE"];
            if (simmode.Equals("2"))
            {
                SignButton.Display = false;
                AgreeButton.Display = true;
                //DisagreeButton.Display = true;
                //移除確認
                AgreeButton.ConfirmText = "";
                AgreeButton.Text = "同意";
            }
        }
    }

    private void initToolBar()
    {
        string simMode = (string)Session["SIMMODE"];

        //初始化工具列
        if (Request.QueryString["CertificateMode"] != null)
        {
        }
        else
        {
            com.dsc.kernal.utility.BrowserProcessor.BrowserType resultType = com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page);
            switch (resultType)
            {
                default:
                    #region IE
                    FloatingToolBar2.Text = "</div>";
                    #endregion
                    break;
                case com.dsc.kernal.utility.BrowserProcessor.BrowserType.FireFox:
                    #region FireFox
                    FloatingToolBar2.Text = "</div><br/><br/>";
                    #endregion
                    break;
                case com.dsc.kernal.utility.BrowserProcessor.BrowserType.Chrome:
                    #region Chrome
                    FloatingToolBar2.Text = "</div><br/><br/>";
                    #endregion
                    break;
                case com.dsc.kernal.utility.BrowserProcessor.BrowserType.Safari:
                    #region Safari
                    FloatingToolBar2.Text = "</div><br/><br/>";
                    #endregion
                    break;
            }

        }
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        if (Request.QueryString["CertificateMode"] != null)
        {
            FloatingSignBar.Text = "<div id='ATB' style=\"display:none;text-alignment:bottom;position:absolute;top:25px;left:0px;height:25px;z-index:9999;width:100%;\" class='SimSignPanel'><table border=0 cellspacing=0 cellpadding=0><tr><td>";
        }
        else
        {
            if (!simMode.Equals("0"))
            {
                FloatingSignBar.Text = "<div id='ATB' style=\"text-alignment:bottom;position:absolute;top:25px;left:0px;height:25px;z-index:9999;width:100%;\" class='SimSignPanel'><table border=0 cellspacing=0 cellpadding=0><tr><td>";

            }
            else
            {
                FloatingSignBar.Text = "<div id='ATB' style=\"text-alignment:bottom;position:absolute;top:25px;left:0px;height:0px;display:none;z-index:9999;width:100%;\" class='SimSignPanel'><table border=0 cellspacing=0 cellpadding=0><tr><td>";
            }
        }

        SignLabel.Width = new System.Web.UI.WebControls.Unit("80px");
        SignLabel.Text = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "SignPanel1", "簽核意見");


        FloatingSignSep1.Text = "</td><td>";

        SignResultField.Width = new System.Web.UI.WebControls.Unit("150px");
        if (simMode.Equals("2"))
        {
            SignResultField.Display = false;
        }
        FloatingSignSep2.Text = "</td><td valign=bottom>";

        SignOpinionField.Width = new System.Web.UI.WebControls.Unit("250px");
        //SignOpinionField.MultiLine = true;
        //SignOpinionField.Height = new System.Web.UI.WebControls.Unit("25px");

        FloatingSignSep3.Text = "</td><td>";

        SignPhraseButton.Text = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "SignPanel2", "片語");
        SignPhraseButton.Width = new System.Web.UI.WebControls.Unit("35px");

        SignPhraseOpenWin.clientEngineType = engineType;
        SignPhraseOpenWin.connectDBString = connectString;


        FloatingSignBar2.Text = "</td></tr></table></div>";

        DraftOpenWin.clientEngineType = engineType;
        DraftOpenWin.connectDBString = connectString;
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
            
			string PPUID = fixNull(Request.QueryString["ParentPageUID"]);
            //string PPUID = Utility.CheckCrossSiteScripting(Request.QueryString["ParentPageUID"].ToString());
            DataObjectSet pdos = (DataObjectSet)Session[PPUID + "_CURLIST"];
            if (pdos != null)
            {
                int total = pdos.getDataObjectCount();
                int current = total - pdos.getAvailableDataObjectCount() + 1;
                string processCount = current + "/" + total;
                header += "<tr>";
                header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead colspan=8 align=right>" + processCount + "</td>";
                header += "</tr>";
            }

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
            /*
            header += "<tr>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwUserId + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA008"].ToString() + "</td>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwUserName + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA009"].ToString() + "</td>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwOrgId + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA010"].ToString() + "</td>";
            header += "<td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadHead>" + hwOrgName + "</td><td valign=top style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=FormHeadDetail>" + oss.Tables[0].Rows[0]["SMWYAAA011"].ToString() + "</td>";
            header += "</tr>";
            */
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        string tempUIStatus = fixNull(Request.QueryString["UIStatus"]);
        if (tempUIStatus.Equals("5")) //處理中
        { 
            this.Controls.Remove(RejectButton);
            this.Controls.AddAt(7, RejectButton);
            RejectButton.Display = true;
        }
        AddSignButton.ImageUrl = "~/Images/GeneralAddSign.gif";
    }

    /// <summary>
    /// 簽核處理程序
    /// </summary>
    protected override void signProcedure()
    {
        //Jack
        //writeLog(new Exception("signProcedure_Star/" + com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null) + "/" + (string)Session["UserID"]));
        //Timestep(DateTimeUtility.getSystemTime2(null), (string)Session["UserID"], this.PageUniqueID, (string)getSession("FlowGUID"), "S", "signProcedure");
        checkPoint("SignProcedure Start", modeSignProc);
        AbstractEngine engine = null;
        string flowOID = (string)getSession("FlowGUID"); //流程實例序號
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            setSession("progressEngine", engine);
            engine.startTransaction(IsolationLevel.ReadCommitted);

            com.dsc.kernal.global.GlobalCache.setValue(flowOID, engine);

            com.dsc.kernal.databean.DataObject currentObject = (com.dsc.kernal.databean.DataObject)getSession("currentObject");
            com.dsc.kernal.databean.DataObject oriObject = (com.dsc.kernal.databean.DataObject)getSession("oriObject");

            string UIStatus = (string)getSession("UIStatus");

            //start: 模擬EasyFlow 05/26 edward
            string simMode = (string)getSession("SIMMODE");
            string strSignOpinionField = SignOpinionField.ValueText;
            strSignOpinionField = filterLowASCII(strSignOpinionField);
            if (simMode.Equals("1"))
            {
                string tempSignType = (string)getSession("tempSignType");
                if (tempSignType.Equals("0"))
                {
                    if (SignResultField.ValueText.Split(new char[] { ';' })[0].Equals("Y"))
                    {
                        Session["signProcess"] = "Y";
                    }
                    else
                    {
                        Session["signProcess"] = "N";
                    }
                }
                else
                {
                    Session["signProcess"] = "Y";
                }

                Session["tempSignResult"] = SignResultField.ReadOnlyText;

                Session["tempSignOpinion"] = strSignOpinionField;

                bool signCheckResult = (bool)Session["signCheckResult"];
                if (!signCheckResult)
                {
                    return;
                }
            }
            else if (simMode.Equals("2"))
            {
                Session["tempSignOpinion"] = strSignOpinionField;
            }
            //end: 模擬EasyFlow 05/26 edward

            string signProcessResult = (string)Session["signProcess"];

            //送單程序

            string afterSendMode = (string)getSession("AfterSignProcess"); //送簽後動作
            string flowid = (string)getSession("PDID"); //流程定義代號
            string flowname = ""; //流程定義名稱
            string SMWBAAA001 = "";

            if (signProcessResult.Equals("Y"))
            {
                errMsg = new ArrayList();
                //bool ans = checkFieldData(engine, currentObject);

                //if (!ans)
                //{
                //    throw new Exception(showErrorMessage());
                //}

                saveData(engine, currentObject);
                checkPoint("Save Data Done", modeSignProc);
                //Jack
                //writeLog(new Exception("signProcedure_saveData_End/" + com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null) + "/" + (string)Session["UserID"]));
                //Timestep(DateTimeUtility.getSystemTime2(null), (string)Session["UserID"], this.PageUniqueID, (string)getSession("FlowGUID"), "E", "signProcedure_saveData");

                if (UIStatus.Equals(ProcessNew))
                {
                    bool isProcessNewRepeat = (bool)getSession("ProcessNewRepeat");
                    if (!isProcessNewRepeat)
                    {
                        currentObject.INSERTUSER = (string)Session["UserGUID"];
                        currentObject.INSERTTIME = com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null);
                        currentObject.setData("IS_LOCK", "A");
                    }
                }
                else if ((UIStatus.Equals(ProcessModify)) || (UIStatus.Equals(ProcessDelete)))
                {
                    //這裡直接修改內容, 不用改狀態
                }
                string returnValue = currentObject.checkData();

                if (!returnValue.Equals(""))
                {
                    throw new Exception(returnValue);
                }

                saveDB(engine, currentObject, oriObject, UIStatus);
                checkPoint("Save DB Done", modeSignProc);
                //Jack
                //writeLog(new Exception("signProcedure_saveDB_End/" + com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null) + "/" + (string)Session["UserID"]));
                //Timestep(DateTimeUtility.getSystemTime2(null), (string)Session["UserID"], this.PageUniqueID, (string)getSession("FlowGUID"), "E", "signProcedure_saveDB");



                //這裡要檢查此關卡是否設定所有參考流程結束
                string checkWaitForReference = (string)getSession("WaitForReference");
                if (checkWaitForReference.Equals("Y"))
                {
                    //檢查是否有參考流程
                    string sch = "select FORMRELATION.GUID from FORMRELATION left outer join DATARELATION on CURGUID=CURRENTGUID where ORIGUID='" + currentObject.getData(getObjectGUIDField()) + "' and RELATIONTYPE='1' and isnull(DATA_STATUS,'N')<>'Y'";
                    DataSet che = engine.getDataSet(sch, "TEMP");
                    if (che.Tables[0].Rows.Count > 0)
                    {
                        //throw new Exception("所發起的參考流程尚未全部簽核完畢");
                        throw new Exception(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError15", "所發起的參考流程尚未全部簽核完畢"));
                    }
                }

                //處理附件
                //要注意如果找不到對應的資料物件, 那就在FORMRELATION中找
                string ssf = "select SMWYAAA002 from SMWYAAA where SMWYAAA005='" + (string)getSession("FlowGUID") + "'";
                DataSet atf = engine.getDataSet(ssf, "TEMP");

                attachFile.engine = engine;
                attachFile.setJobID(currentObject.getData(getObjectGUIDField()));
                //若atf有資料, 代表為一般流程, 若無資料, 代表為參考流程, 無單號, 直接以流程實力序號
                if (atf.Tables[0].Rows.Count > 0)
                {
                    if (attachFileConfirmSaveLevel1.Length > 0)
                    {
                        attachFile.confirmSave(attachFileConfirmSaveLevel1, atf.Tables[0].Rows[0][0].ToString());
                    }
                    else
                    {
                        attachFile.confirmSave(ProcessPageID, atf.Tables[0].Rows[0][0].ToString());
                    }
                }
                else
                {
                    if (attachFileConfirmSaveLevel1.Length > 0)
                    {
                        attachFile.confirmSave(attachFileConfirmSaveLevel1, (string)getSession("FlowGUID"));
                    }
                    else
                    {
                        attachFile.confirmSave(ProcessPageID, (string)getSession("FlowGUID"));
                    }
                }
                attachFile.saveFile();

                string sql = "";
                DataSet ds = null;

                //處理DATARELATION
                if (UIStatus.Equals(ProcessNew))
                {
                    bool isProcessNewRepeat = (bool)getSession("ProcessNewRepeat");
                    if (!isProcessNewRepeat)
                    {
                        sql = "select SMWAAAA001 from SMWAAAA where SMWAAAA002='" + ProcessPageID + "'";
                        ds = engine.getDataSet(sql, "TEMP");
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            //throw new Exception("找不到此作業畫面設定檔");
                            throw new Exception(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError13", "找不到此作業畫面設定檔"));
                        }

                        string processGUID = ds.Tables[0].Rows[0][0].ToString();

                        //新增
                        sql = "insert into DATARELATION(GUID, LASTGUID, CURRENTGUID, PROCESSGUID, FLOWGUID, FLOWID, AGENTSCHEMA, DATA_STATUS, UPDATEUSER, UPDATETIME) values(";
                        sql += "'" + IDProcessor.getID("") + "',";
                        sql += "'',";
                        sql += "'" + currentObject.getData("GUID") + "',";
                        sql += "'" + processGUID + "',";
                        sql += "'" + flowOID + "',";
                        sql += "'" + flowid + "',";
                        sql += "'" + AgentSchema + "',";
                        sql += "'A',";
                        sql += "'" + (string)Session["UserGUID"] + "',";
                        sql += "'" + DateTimeUtility.getSystemTime2(null) + "')";
                        if (!engine.executeSQL(sql))
                        {
                            throw new Exception(engine.errorString);
                        }

                        //寫到FORMRELATION
                        sql = "select SMWYAAA019, SMWAAAA003, SMWAAAA002 from SMWYAAA inner join SMWAAAA on SMWYAAA018=SMWAAAA001 where SMWYAAA005='" + (string)getSession("FlowGUID") + "'";
                        DataSet gst = engine.getDataSet(sql, "TEMP");

                        sql = "select SMWAAAA003 from SMWAAAA where SMWAAAA002='" + ProcessPageID + "'";
                        DataSet curset = engine.getDataSet(sql, "TEMP");

                        //若gst有資料, 代表為正常流程的ProcessNew, 否則為發起參考流程後的ProcessNew
                        if (gst.Tables[0].Rows.Count > 0)
                        {
                            sql = "insert into FORMRELATION(GUID, ORIGUID, CURGUID, FLOWGUID, RELATIONTYPE, ORIFORMNAME, CURFORMNAME, CREATETIME, ORIPAGETYPE, CURPAGETYPE, FLOWID) values(";
                            sql += "'" + IDProcessor.getID("") + "',";
                            sql += "'" + gst.Tables[0].Rows[0][0].ToString() + "',";
                            sql += "'" + currentObject.getData(getObjectGUIDField()) + "',";
                            sql += "'" + (string)getSession("FlowGUID") + "',";
                            sql += "'0',";
                            sql += "'" + gst.Tables[0].Rows[0][1].ToString() + "',";
                            sql += "'" + curset.Tables[0].Rows[0][0].ToString() + "',";
                            sql += "'" + DateTimeUtility.getSystemTime2(null) + "',";
                            sql += "'" + gst.Tables[0].Rows[0][2].ToString() + "',";
                            sql += "'" + ProcessPageID + "',";
                            sql += "'" + (string)getSession("PDID") + "')";
                            if (!engine.executeSQL(sql))
                            {
                                throw new Exception(engine.errorString);
                            }
                        }
                        else
                        {
                            sql = "select * from FORMRELATION where FLOWGUID='" + (string)getSession("FlowGUID") + "' and CURGUID=''";
                            DataSet dsr = engine.getDataSet(sql, "TEMP");
                            if (dsr.Tables[0].Rows.Count > 0)
                            {
                                dsr.Tables[0].Rows[0]["CURGUID"] = currentObject.getData(getObjectGUIDField());
                                dsr.Tables[0].Rows[0]["CURFORMNAME"] = curset.Tables[0].Rows[0][0].ToString();
                                dsr.Tables[0].Rows[0]["CURPAGETYPE"] = ProcessPageID;
                                if (!engine.updateDataSet(dsr))
                                {
                                    throw new Exception(engine.errorString);
                                }
                            }
                            else
                            {
                                //throw new Exception("並未找到發起參考流程的資料");
                                throw new Exception(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError16", "並未找到發起參考流程的資料"));
                            }
                        }
                    }
                }

                //代理轉派動作
                reassignmentSubstitute(engine);
                checkPoint("reassignmentSubstitute", modeSignProc);
                string addSignXml = "";
                string IsAddSign = (string)getSession("IsAddSign");
                if (IsAddSign.Equals("AFTER"))
                {
                    addSignXml = getAddSignXml();

                    //簽核前呼叫: 可以在此新增或修改加簽的資料等等
                    addSignXml = beforeSign(engine, true, addSignXml);

                    //加簽
                    if (addSignXml.Length > 0)
                    {
                        addCustomActivity(engine, (string)getSession("WorkItemOID"), true, addSignXml, "", "");
                    }
                }

                //取得流程定義代號, 要注意ProcessNew
                sql = "select SMWBAAA001, SMWYAAA001 from SMWBAAA inner join SMWYAAA on SMWBAAA003=SMWYAAA003 where SMWYAAA005='" + (string)getSession("FlowGUID") + "'";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    //有可能是發起參考流程, 此時會有PDID
                    sql = "select SMWBAAA001 from SMWBAAA where SMWBAAA003='" + (string)getSession("PDID") + "'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        //throw new Exception("找不到此作業畫面所要發起的流程定義");
                        throw new Exception(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError12", "找不到此作業畫面所要發起的流程定義"));
                    }
                }
                string isSignQueue = (string)getSession("IsSignQueue");
                if (isSignQueue.Equals("N"))
                {
                    //取得流程預設參數
                    Hashtable param = getProcessParameter(engine, ds.Tables[0].Rows[0][0].ToString());

                    //取得流程參數
                    string firstParam = setFlowVariables(engine, param, currentObject);

                    //取得表單參數--直接使用流程參數方式設定表單變數
                    //string formXML = setFormVariables(engine, currentObject);

                    //發起流程
                    if (!firstParam.Equals(""))
                    {
                        IDictionaryEnumerator ie = param.GetEnumerator();
                        while (ie.MoveNext())
                        {
                            string pID = (string)ie.Key;
                            string pValue = (string)ie.Value;
                            if (!pID.ToUpper().Equals("PROCESSSERIALNUMBER")
                                && !pID.ToUpper().Equals("PROCESSTERMINATE")
                                && !pID.ToUpper().Equals("PROCESSWITHDRAW")
                                && !pID.ToUpper().Equals("PROCESSCOMPLETE")
                                && !pID.ToUpper().Equals("PROCESSSUCCESS")
                                )
                            {
                                assignRelevantDataBySerialNo(engine, flowOID, pID, pValue);
                                checkPoint("assignRelevantDataBySerialNo " + pID, modeSignProc);
                            }
                        }
                    }
                }
                else
                {
                    SMWBAAA001 = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            else
            {
                //代理轉派動作
                reassignmentSubstitute(engine);
                checkPoint("reassignmentSubstitute", modeSignProc);
            }
            /*--直接使用流程參數方式設定表單變數
            if (!formXML.Equals(""))
            {
                XMLProcessor xmlp = new XMLProcessor(formXML, 1);
                string formID = xmlp.doc.ChildNodes[0].Name;
                assignRelevantDataBySerialNo(engine, flowOID, formID, formXML);
            }
            */
            //engine.close();

            //簽核程序
            //engine = factory.getEngine(engineType, connectString);

            //國昌20091217: 先將目前使用者儲存到GlobalCache中，這樣在afterApprove時才可以取得使用者代號
            com.dsc.kernal.global.GlobalCache.setValue(flowOID + "USERID", base.systemInfo.UserID);

            string signProcess = (string)Session["signProcess"];
            string signResult = (string)Session["tempSignResult"];
            string signOpinion = (string)Session["tempSignOpinion"];
            checkPoint("Call EFGP ", modeSignProc);
            try
            {
                string isSignQueue = (string)getSession("IsSignQueue");
                if (signProcess.Equals("Y"))
                {
                    if (isSignQueue.Equals("Y"))
                    {
                        //MessageBox("SignQueue");
                        engine.executeSQL("insert into QUEUEWAITING (KEYS) values ('" + (string)getSession("WorkItemOID") + "')");
                        try
                        {
                            //取得流程預設參數
                            Hashtable param = getProcessParameter(engine, SMWBAAA001);

                            //取得流程參數
                            string firstParam = setFlowVariables(engine, param, currentObject);

                            //取得表單參數--直接使用流程參數方式設定表單變數
                            //string formXML = setFormVariables(engine, currentObject);

                            //發起流程
                            string hash = "";
                            if (!firstParam.Equals(""))
                            {
                                IDictionaryEnumerator ie = param.GetEnumerator();
                                while (ie.MoveNext())
                                {
                                    string pID = (string)ie.Key;
                                    string pValue = (string)ie.Value;
                                    if (!pID.Equals("processSerialNumber"))
                                    {
                                        //assignRelevantDataBySerialNo(engine, flowOID, pID, pValue);
                                        //checkPoint("assignRelevantDataBySerialNo", modeSignProc);
                                        hash += pID + "^^^^" + pValue + "$$$$";
                                    }
                                }
                                if (hash.Length > 0) hash = hash.Substring(0, hash.Length - 4);
                            }

                            string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
                            string pam = (string)Session["engineType"] + "&&&&" + (string)Session["connectString"] + "&&&&" + (string)getSession("WorkItemOID") + "&&&&" + signResult + "&&&&" + signOpinion + "&&&&" + (string)Session["UserID"] + "&&&&" + Session["FlowProcessCount"].ToString() + "&&&&" + Session["FlowProcessWaiting"].ToString() + "&&&&" + debugPage.ToString() + "&&&&" + Server.MapPath("~/LogFolder/" + fname + "_flowdata.log") + "&&&&" + flowOID + "&&&&" + hash + "&&&&" + firstParam;
                            WebServerProject.SysParam sps = new SysParam(engine);
                            string ip = sps.getParam("QueueIP");
                            string port = sps.getParam("QueuePort");

                            com.dsc.kernal.batch.JobClient jc = new com.dsc.kernal.batch.JobClient();
                            jc.IP = ip;
                            jc.port = int.Parse(port);
                            jc.createJob("GPQueueClass.CompleteWorkItem", pam);
                        }
                        catch (Exception zze)
                        {
                            engine.executeSQL("delete from QUEUEWAITING where KEYS='" + (string)getSession("WorkItemOID") + "'");
                            throw (zze);
                        }
                    }
                    else
                    {
                        completeWorkItem(engine, (string)getSession("WorkItemOID"), signResult, signOpinion);
                    }
                }
                else
                {
                    if (isSignQueue.Equals("Y"))
                    {
                        //MessageBox("SignQueue");
                        engine.executeSQL("insert into QUEUEWAITING (KEYS) values ('" + flowOID + "')");

                        try
                        {
                            string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
                            string pam = (string)Session["engineType"] + "&&&&" + (string)Session["connectString"] + "&&&&" + flowOID + "&&&&" + signResult + "&&&&" + signOpinion + "&&&&" + (string)Session["UserID"] + "&&&&" + Session["FlowProcessCount"].ToString() + "&&&&" + Session["FlowProcessWaiting"].ToString() + "&&&&" + debugPage.ToString() + "&&&&" + Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");
                            WebServerProject.SysParam sps = new SysParam(engine);
                            string ip = sps.getParam("QueueIP");
                            string port = sps.getParam("QueuePort");

                            com.dsc.kernal.batch.JobClient jc = new com.dsc.kernal.batch.JobClient();
                            jc.IP = ip;
                            jc.port = int.Parse(port);
                            jc.createJob("GPQueueClass.TerminateProcess", pam);
                        }
                        catch (Exception zze)
                        {
                            engine.executeSQL("delete from QUEUEWAITING where KEYS='" + flowOID + "'");
                            throw (zze);
                        }
                    }
                    else
                    {
                        terminateProcess(engine, flowOID, signResult, signOpinion);
                    }
                }
                //Jack
                //writeLog(new Exception("signProcedure_C/T_End/" + com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null) + "/" + (string)Session["UserID"]));
                //Timestep(DateTimeUtility.getSystemTime2(null), (string)Session["UserID"], this.PageUniqueID, (string)getSession("FlowGUID"), "E", "signProcedure_C/T");
                checkPoint("Call EFGP Done ", modeSignProc);
            }
            catch (Exception gpe)
            {
                processErrorMessage(errorLevel, gpe, true);
                throw new Exception("GPError");
            }

            //國昌20091217: 最後一關時，確認afterApprove後才能執行afterSign，所以先不要呼叫afterSign，移至afterApprove前呼叫。

            string sqsl = "select SMWYAAA020 from SMWYAAA where SMWYAAA005='" + flowOID + "'";
            DataSet dsaf = engine.getDataSet(sqsl, "TEMP");
            if (dsaf.Tables[0].Rows.Count > 0) //國昌20100402 mantis 0016688
            {
                if (dsaf.Tables[0].Rows[0][0].ToString().Equals("I"))
                {
                    //簽核後呼叫
                    afterSign(engine, currentObject, signResult);
                    checkPoint("afterSign Done ", modeSignProc);
                }
            }
            //Jack
            //writeLog(new Exception("signProcedure_afterSign_End/" + com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null) + "/" + (string)Session["UserID"]));
            //Timestep(DateTimeUtility.getSystemTime2(null), (string)Session["UserID"], this.PageUniqueID, (string)getSession("FlowGUID"), "E", "signProcedure_afterSign");

            engine.commit();
            engine.close();
            com.dsc.kernal.global.GlobalCache.setValue(flowOID, null);
            checkPoint("Engine Commit and Close ", modeSignProc);


            //儲存成功
            //Response.Write("alert('" + SignSuccessMsg + "');");

            string isShowFlowChart = (string)getSession("IsShowFlowChart");
            bool isShowFlow = false;
            if (isShowFlowChart.Equals("Y"))
            {
                isShowFlow = true;
            }
            else
            {
                isShowFlow = false;
            }


            //20130403 取消以系統參數-showSignSuccessMsg 開關方式控管簽核後的訊息
            //改以判斷簽核成功訊息是否有值，來決定是否顯示。
            //該參數已被引用，因此仍保留。

            //參數化決定是否顯示SignSuccessMsg Jack
            bool showSignSuccessMsg = true;
            SysParam sp = new SysParam(engine);
            try
            {
                if (Convert.ToString(sp.getParam("showSignSuccessMsg")).ToUpper().Equals("FALSE"))
                {
                    showSignSuccessMsg = false;
                }
            }
            catch { }

            if (afterSendMode.Equals("1"))
            {
                //要顯示流程圖
                string imageURL = "";

                engine = factory.getEngine(engineType, connectString);
                if (isShowFlow)
                {

                    string filename = IDProcessor.getID("") + ".jpg";
                    string localFilePath = Server.MapPath("~/tempFolder/" + filename);

                    fetchFlowDiagram(engine, (string)getSession("FlowGUID"), localFilePath);

                    imageURL = Server.UrlEncode(Request.ApplicationPath + "/tempFolder/" + filename);


                }
                else
                {
                    imageURL = "";
                }

                DataRow opSetting = (DataRow)getSession("SignOpinionSetting");
                string opinionHTML = "";
                if (opSetting["SMWDAAA017"].ToString().Equals("Y"))
                {
                    opinionHTML = getSignOpinion(Page.Server, debugPage, engine, (string)getSession("FlowGUID"), SignOpinion.SHOW_ALL, (DataRow)getSession("SignOpinionSetting"));
                }
                engine.close();
                setSession("OpinionHTML", opinionHTML);

                string urls = Page.ResolveUrl("~/Program/DSCGPFlowService/Public/ViewOpinion.aspx?ImageURL=" + imageURL + "&processSerialNumber=" + flowOID + "&opinionType=SHOW_ALL&SourceURL=" + Request.ServerVariables["PATH_INFO"] + "&PGID=" + this.PageUniqueID);
                base.showPanelWindow(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError17", "流程圖"), urls, 0, 0, "", true, true);
                //base.showOpenWindow(urls, "流程圖", "", "", "", "", "", "1", "1", "", "", "", "", "", "", true);
                //base.showOpenWindow(urls, com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError17", "流程圖"), "", "", "", "", "", "1", "1", "", "", "", "", "", "", true);
                //Response.Write("window.location.href='" + urls + "';");

            }
            else if (afterSendMode.Equals("0"))
            {
                if (showSignSuccessMsg)
                {
                    if (SignSuccessMsg.Length > 0)
                    {
                        MessageBox(SignSuccessMsg);
                    }
                }
            }

            string ParentPanelID = (string)getSession("ParentPanelID");
            if (ParentPanelID.Equals(""))
            {
                closeSilence();
                //Edit by Jack 20110420 Mantis-0018996 
                if (afterSendMode.Equals("2"))
                {
                    if (showSignSuccessMsg)
                    {
                        if (SignSuccessMsg.Length > 0)
                        {
                            MessageBox(SignSuccessMsg);
                        }
                    }
                }
            }
            else
            {
                //cl_chang
                string workItemOID = (string)getSession("WorkItemOID");
                string PPUID = Utility.CheckCrossSiteScripting(Request.QueryString["ParentPageUID"].ToString());
                DataObjectSet pdos = (DataObjectSet)Session[PPUID + "_CURLIST"];
                //delete CURLIST ObjectGUID
                for (int i = 0; i < pdos.getAvailableDataObjectCount(); i++)
                {
                    DataObject dataObject = pdos.getAvailableDataObject(i);
                    string guid = dataObject.getData("GUID");
                    if (guid.Equals(workItemOID))
                    {
                        dataObject.delete();
                        Session[PPUID + "_CURLIST"] = pdos;
                        break;
                    }
                }
                //載入下一張表單
                DataObject firstDataObject = pdos.getAvailableDataObject(0);
                if (firstDataObject != null)
                {
                    redirectToObject(firstDataObject, engine);
                }
                else
                {
                    closeRefreshClick();
                    refreshInbox();
                }
                
                //--------------------

                /*
                if (afterSendMode.Equals("2"))
                {
                    if (showSignSuccessMsg)
                    {
                        if (SignSuccessMsg.Length > 0)
                        {
                            MessageBox(SignSuccessMsg);
                        }
                    }
                    //自動帶下一張簽核表單
                    closeRefreshClick();
                    checkPoint("closeRefreshClick", modeSignProc);

                }
                else
                {
                    closeRefresh();
                }
                */
            }
            checkPoint("END", modeSignProc);
        }
        catch (Exception ue)
        {
            try
            {
                engine.rollback();
            }
            catch { };
            try
            {
                engine.close();
                com.dsc.kernal.global.GlobalCache.setValue(flowOID, null);
            }
            catch { };
            if (!ue.Message.Equals("GPError"))
            {
                processErrorMessage(0, ue);
            }
        }
        //Jack
        //writeLog(new Exception("signProcedure_End/" + com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null) + "/" + (string)Session["UserID"]));
        //Timestep(DateTimeUtility.getSystemTime2(null), (string)Session["UserID"], this.PageUniqueID, (string)getSession("FlowGUID"), "E", "signProcedure");

    }

    /// <summary>
    /// //低序位ASCII 字元將造成xPath剖析錯誤；將此類字元都替代為空白符
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private string filterLowASCII(string input)
    {
        for (int i = 0; i < 32; i++)
        {
            if (i != 10 && i != 13)
            {
                input = input.Replace((char)i, (char)32);
            }
        }
        return input;
    }

    /// <summary>
    /// 將加簽的資料串成xml
    /// </summary>
    /// <returns>加簽的xml</returns>
    private string getAddSignXml()
    {
        DataObjectSet dos = (DataObjectSet)getSession("AddSignGroup");
        if (dos.getAvailableDataObjectCount() > 0)
        {
            string xml = "<list>";
            //xml += "<list>";

            for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
            {
                SMWPAAA aa = (SMWPAAA)dos.getAvailableDataObject(i);
                xml += "<com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                xml += "<performers>";
                string[] tag = aa.SMWPAAA004.Split(new char[] { '#' });
                for (int j = 0; j < tag.Length; j++)
                {
                    string[] ztag = tag[j].Split(new char[] { ';' });
                    xml += "<com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                    xml += "<OID>" + ztag[1] + "</OID>";
                    xml += "<participantType><value>" + ztag[0] + "</value></participantType>";
                    xml += "</com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                }
                xml += "</performers>";

                xml += "<multiUserMode><value>" + aa.SMWPAAA005 + "</value></multiUserMode>";
                xml += "<name>" + aa.SMWPAAA003 + "</name>";
                xml += "<performType><value>" + aa.SMWPAAA006 + "</value></performType>";

                xml += "</com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
            }

            //xml += "</list>";
            xml += "</list>";

            return xml;
        }
        else
        {
            return "";
        }
    }

    protected void redirectToObject(DataObject dataObject, AbstractEngine engine)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string pdid = (string)getSession("PDID");
        IOFactory factory = new IOFactory();
        engine = factory.getEngine(engineType, connectString);

        bool needRedirect = true;
        string urls = Request.ServerVariables["PATH_INFO"];
        string parentPanelID = Page.Request.QueryString["ParentPanelID"];
        string dataListID = (string)getSession("DataListID");
        string pageUID = Page.Request.QueryString["ParentPageUID"];
        string sheetNo = dataObject.getData("SHEETNO");
        string objectGUID = "";
        string actName = dataObject.getData("WORKITEMNAME");
        string flowOID = "";
        string flowid = "";
        string workItemOID = dataObject.getData("GUID");
        string workAssignmentOID = dataObject.getData("WORKASSIGNMENT");
        string assignmentType = dataObject.getData("ASSIGNMENTTYPE");
        string curPanelID = Request.QueryString["CurPanelID"];
        string sql = "select SMWYAAA003, SMWYAAA005, SMWYAAA019 from SMWYAAA where SMWYAAA002='" + sheetNo + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        flowid = ds.Tables[0].Rows[0][0].ToString();
        flowOID = ds.Tables[0].Rows[0][1].ToString();
        objectGUID = ds.Tables[0].Rows[0][2].ToString();

        if (!pdid.Equals(flowid))
        {
            needRedirect = false;
            closeRefreshClick();
            refreshInbox();
        }
        
        if (needRedirect)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("?");
            sb.Append("ParentPanelID=" + parentPanelID);
            sb.Append("&DataListID=" + dataListID);
            sb.Append("&ParentPageUID=" + pageUID);
            sb.Append("&UIType=Process");
            sb.Append("&ObjectGUID=" + objectGUID);
            sb.Append("&HistoryGUID=");
            sb.Append("&FlowGUID=" + flowOID);
            sb.Append("&ACTID=");
            sb.Append("&PDID=" + flowid);
            sb.Append("&PDVer=");
            sb.Append("&ACTName=" + actName);
            sb.Append("&UIStatus=5");
            sb.Append("&WorkItemOID=" + workItemOID);
            sb.Append("&TargetWorkItemOID=");
            sb.Append("&workAssignmentOID=" + workAssignmentOID);
            sb.Append("&assignmentType=" + assignmentType);
            sb.Append("&reassignmentType=&manualReassignType=1&IsAllowWithDraw=true&IsMaintain=N");
            sb.Append("&CurPanelID=" + curPanelID);
            urls = sb.ToString();
            //urls = com.dsc.kernal.utility.Utility.URLParameterEncode(urls);
            Response.Write("window.location.href='" + urls + "';");
        }
    }

    /// <summary>
    /// 終止目前表單流程
    /// </summary>
	protected void terminateThisProcess()
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
				
			string signResult = "不同意";
			string backOpinion = (string)Session["tempBackOpinion"];
            //string signOpinion = (string)Session["tempSignOpinion"];
            string flowOID = (string)getSession("FlowGUID");
            base.terminateProcess(engine, flowOID, signResult, backOpinion);
			
			MessageBox("退件成功");
			closeRefreshClick();
        }
        catch (Exception e)
        {
            MessageBox(e.Message);
        }
        finally
        {
            if (engine != null) engine.close();
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

            //儲存成功
            //Response.Write("alert('" + RejectSuccessMsg + "');");
            MessageBox(RejectSuccessMsg);

            string userGUID = (string)Session["UserGUID"];
            string afterRejectProcess = (string)engine.executeScalar("select AfterRejectProcess from SmpUserConf where UserGUID='" + userGUID + "'");
            if (afterRejectProcess != null && !afterRejectProcess.Equals(""))
            {
                if (afterRejectProcess.Equals("3"))
                {
                    closeRefresh();
                }
                else
                {
                    closeRefreshClick();
                }
            }
            else
            {
                string needCloseRefreshClick = Convert.ToString(getSession("closeRefreshClick")); //送簽後動作
                if (needCloseRefreshClick.Equals("N"))
                {
                    //closeRefreshClick();
                }
                else
                {
                    closeRefreshClick();
                }
            }

            engine.close();
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
                    //string url = getPersonalImage(engine, pid);
                    //if (url.Length > 0 && (fixNbspS(Page.Server.HtmlEncode(pd.executiveResult)) == "同意" || fixNbspS(Page.Server.HtmlEncode(pd.executiveResult)) == "退回重辦"))
                    //{
                    //    string image = "<br/><img width=\"150\" height=\"40\" src='" + url + "' /> ";//For Personal Image
                    //    sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(pd.performerName)) + image + "</td>");
                    //}
                    //else
                    //{
                        sbHtml.Append("<td style=\"border-style:solid;border-width:1px;border-top-style:none;border-left-style:none\" class=OpinionDetail>" + fixNbspS(Page.Server.HtmlEncode(pd.performerName)) + "</td>");
                    //}
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

    /// <summary>
    /// 刷新收件資料匣
    /// </summary>
    protected void refreshInbox()
    {
        string js = "";
        js += "var outlookWindow = window.parent.getPanelWindowObject('0') ;";
        js += "try {";
        js += "     outlookWindow.refreshErpInbox();";
        js += "}";
        js += "    catch(e) {alert(e.message);};";
        js += "";
        Response.Write(js);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userGUID"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 取得群組人員, [][]: [id][userName]
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="groupName"></param>
    /// <returns>string[][]</returns>
    protected string[][] getGroupdUser(AbstractEngine engine, string groupId)
    {
        string sql = "select u.id, userName from Groups g, Users u, Group_User gu where g.id='" + Utility.filter(groupId) + "' and gu.GroupOID = g.OID and gu.UserOID = u.OID";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;
        string[][] result = new string[rows][];
        for (int i = 0; i < rows; i++)
        {
            result[i] = new string[2];
            result[i][0] = ds.Tables[0].Rows[i][0].ToString();
            result[i][1] = ds.Tables[0].Rows[i][1].ToString();
        }

        return result;
    }

    /// <summary>
    /// 取得使用者OID, [0]: OID, [1]: userName
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userId"></param>
    /// <returns>string[]</returns>
    protected string[] getUserGUID(AbstractEngine engine, string userId)
    {
        string sql = "select OID, userName from Users where id='" + Utility.filter(userId) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[2];
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();
            result[1] = ds.Tables[0].Rows[0][1].ToString();
        }
        else
        {
            result[0] = "";
            result[1] = "";
        }

        return result;
    }
}
