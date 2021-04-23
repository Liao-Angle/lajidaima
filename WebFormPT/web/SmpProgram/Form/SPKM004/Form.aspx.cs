using System;
using System.Data;
using System.Configuration;
using System.Collections;
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

using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;
using com.dsc.kernal.utility;
using com.dsc.kernal.document;


public partial class SmpProgram_Form_SPKM004_Form : SmpKmFormPage
{

    /// <summary>
    /// 初始化參數。
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPKM004";
        AgentSchema = "WebServerProject.form.SPKM004.SmpDocAccessFormAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPKM";
    }

    /// <summary>
    /// 初始化畫面元件。初始化資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script language=javascript>");
        sb.Append(" function clickViewDoc(){");
        sb.Append("     parent.window.openWindowGeneral('檢視文件','" + Page.ResolveUrl("../SPKM005/ViewDoc.aspx") + "','','','',true,true);");
        sb.Append(" }");
        sb.Append("</script>");
        Type ctype = this.GetType();
        ClientScriptManager cm = Page.ClientScript;
        if (!cm.IsStartupScriptRegistered(ctype, "clickViewDocScript"))
        {
            cm.RegisterStartupScript(ctype, "clickViewDocScript", sb.ToString());
        }
        GlassButtonViewDoc.AfterClick = "clickViewDoc";
        GlassButtonViewDoc.ReadOnly = true;


        bool isAddNew = base.isNew();
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string sql = null;
        string[,] ids = null;
        DataSet ds = null;
        int count = 0;

        //申請人
        OriginatorGUID.clientEngineType = engineType;
        OriginatorGUID.connectDBString = connectString;
        if (OriginatorGUID.ValueText.Equals(""))
        {
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }
		
		//公司別
		string[,] idsCompany = null;
        idsCompany = new string[,]{
            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm003_form_aspx.language.ini", "message", "smp", "新普科技")},
            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm003_form_aspx.language.ini", "message", "tp", "中普科技")},
			{"STCS",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm003_form_aspx.language.ini", "message", "stcs", "新世電子")}
        };
        CompanyCode.setListItem(idsCompany);
		string[] values = base.getUserInfoById(engine,(string)Session["UserID"]);
        CompanyCode.ValueText = values[5];
        CompanyCode.ReadOnly = true;

        ConfidentialLevel.ReadOnly = true;

        //機密等級
        ids = new string[,]{
                {"",""},
                {"0","一般"},
                {"1","機密"},
                {"2","極機密"}
            };
        ConfidentialLevel.setListItem(ids);

        //調閱文件
        DocGUID.clientEngineType = engineType;
        DocGUID.connectDBString = connectString;
		
        DataObjectSet detailSet = null;
		if (isAddNew)
        {
            detailSet = new DataObjectSet();
            detailSet.isNameLess = true;
            detailSet.setAssemblyName("WebServerProject");
            detailSet.setChildClassString("WebServerProject.form.SPKM004.SmpDocAccessDetail");
            detailSet.setTableName("SmpDocAccessDetail");
            detailSet.loadFileSchema();
            objects.setChild("SmpDocAccessDetail", detailSet);
            
        }
        else
        {
            detailSet = objects.getChild("SmpDocAccessDetail");
        }
        
        DocAccessList.dataSource = detailSet;
        DocAccessList.HiddenField = new string[] { "GUID", "DocAccessGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY", "DocConfidentialLevel", "DocGUID", "DocTypeGUID", "RevGUID","AuthorGUID", "SubGUID" };
        DocAccessList.updateTable();
        DocAccessList.NoAdd = true;
        DocAccessList.NoModify = true;
        
        //由文件查詢按鈕帶入DocGUID
        if (isAddNew)
        {
            if (DocGUID.ValueText.Equals(""))
            {
                string docGUID = Request.QueryString["DocGUID"];
                if (docGUID != null && !docGUID.Equals(""))
                {
                    DocGUID.GuidValueText = docGUID;
                    DocGUID.doGUIDValidate();
                }
            }
        }

        //改變工具列順序
        base.initUI(engine, objects);
    }

    /// <summary>
    /// 將資料由資料物件填入畫面元件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        bool isAddNew = base.isNew();
        string actName = Convert.ToString(getSession("ACTName"));

        if (isAddNew)
        {
            OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID");
            OriginatorGUID.doGUIDValidate();            
        }

        //主旨
        Subject.ValueText = objects.getData("Subject"); 
        //顯示單號
        base.showData(engine, objects);
        //申請人
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID");
        OriginatorGUID.doGUIDValidate();
		
		//公司別
		string[,] idsCompany = null;
        idsCompany = new string[,]{
            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm003_form_aspx.language.ini", "message", "smp", "新普科技")},
            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm003_form_aspx.language.ini", "message", "tp", "中普科技")},
			{"STCS",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm003_form_aspx.language.ini", "message", "stcs", "新世電子")}
        };
        CompanyCode.setListItem(idsCompany);
		string[] values = base.getUserInfo(engine, OriginatorGUID.GuidValueText);
        CompanyCode.ValueText = values[2];
        CompanyCode.ReadOnly = true;

        //調閱截止日
        EndDate.ValueText = objects.getData("EndDate");

        //調閱原因
        AccessReason.ValueText = objects.getData("AccessReason");

        //最高機密等級
        ConfidentialLevel.ValueText = objects.getData("ConfidentialLevel");

        //調閱文件
        //DocGUID.clientEngineType = engineType;
        //DocGUID.connectDBString = connectString;
               
        if (!actName.Equals("") && !actName.Equals("填表人"))
        {
            //表單發起後不允許修改
            Subject.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            ConfidentialLevel.ReadOnly = true;
            AccessReason.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            EndDate.ReadOnly = true;
            SearchButton.ReadOnly = true;
            DocGUID.ReadOnly = true;
            DocAccessList.NoDelete = true;
            DocAccessList.NoAllDelete = true;
        }


        DataObjectSet docAccessSet = null;
        docAccessSet = objects.getChild("SmpDocAccessDetail");
        DocAccessList.dataSource = docAccessSet;
        DocAccessList.updateTable();

        for (int i = 0; i < DocAccessList.dataSource.getAvailableDataObjectCount(); i++)
        {
            DocAccessList.dataSource.getAvailableDataObject(i).setData("SmpDocAccessDetail", objects.getData("GUID"));
        }
        
    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            bool isAddNew = base.isNew();
            string actName = (String)getSession("ACTName");
            string connectString = (string)Session["connectString"];
            IOFactory factory = new IOFactory();

            if (isAddNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("Subject", Subject.ValueText);
                objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
                //產生單號並儲存至資料物件
                base.saveData(engine, objects);
                objects.setData("EndDate", EndDate.ValueText);
                objects.setData("AccessReason", AccessReason.ValueText);
            }

            for (int i = 0; i < DocAccessList.dataSource.getAvailableDataObjectCount(); i++)
            {
                DocAccessList.dataSource.getAvailableDataObject(i).setData("DocAccessGUID", objects.getData("GUID"));
            }

            if (isAddNew)
            {
                DataObjectSet chkType = DocAccessList.dataSource;
                decimal tmpLevel = 0;
                decimal mlLevel = 0;
                for (int i = 0; i < chkType.getAvailableDataObjectCount(); i++)
                {
                    tmpLevel = decimal.Parse(chkType.getAvailableDataObject(i).getData("DocConfidentialLevel"));
                    if (tmpLevel > mlLevel)
                    {
                        mlLevel = tmpLevel;
                    }                    
                }
                //取得最高機密等級
                objects.setData("ConfidentialLevel", mlLevel.ToString());
            }

        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            //if (engine1 != null)
            //{
            //    engine1.close();
            //}
        }

    }

    /// <summary>
    /// 畫面資料稽核。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
		string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));
        string accessReason = AccessReason.ValueText;
        string originator = OriginatorGUID.ValueText;
		bool isAddNew = base.isNew();
        if (isAddNew)
        {
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = "("+DocGUID.ValueText+") 文件調閱";
            }
            if (originator.Equals(""))
            {
                strErrMsg += "請維護申請者\n";
            }
            if (accessReason.Equals(""))
            {
                strErrMsg += "請維護調閱原因\n";
            }
            if (EndDate.ValueText.Equals(""))
            {
                strErrMsg += "請維護調閱截止日\n";
            }
            //檢查 明細清單 資料
            DataObjectSet chkType = DocAccessList.dataSource;
            if (chkType.getAvailableDataObjectCount().Equals(0))
            {
                strErrMsg += "請輸入調閱文件資訊!\n";
            }
		}
        if (!strErrMsg.Equals(""))
        {
            pushErrorMessage(strErrMsg);
            result = false;
        }

        return result;
    }

    /// <summary>
    /// 初始化SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"]; //填表人
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"]; //表單關系人
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 設定SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = OriginatorGUID.ValueText; //表單關系人
        si.ownerName = OriginatorGUID.ReadOnlyValueText;
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 設定自動編碼格式所需變數值。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="autoCodeID"></param>
    /// <returns></returns>
    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
        Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID);
        return hs;
    }

    /// <summary>
    /// 設定流程變數。目前主要是用來傳遞流程所需要的變數值。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        string originatorId = OriginatorGUID.ValueText;
        string checkBy = "";
        string typeAdm = "";
        string manager = "";
        //機密等級, 
        string confidentialLevel = ConfidentialLevel.ValueText;
        string formGUID = currentObject.getData("GUID");
        string notifier = "";
        string[] values = null;
        string sql = "";
        DataSet ds = null;
        string authorOrgUnitGUID = "";
        string authorManager = "";


        if (!typeAdm.Equals(""))
        {
            notifier += typeAdm + ";";
        }
        notifier += OriginatorGUID.ValueText;

        values = base.getUserManagerInfo(engine, OriginatorGUID.GuidValueText);
        manager = values[1];

        if (confidentialLevel.Equals("0"))
        {
            confidentialLevel = "N";
        }
        else
        {
            confidentialLevel = "Y";
        }

        if (typeAdm.Equals(""))
        {
            typeAdm = manager;
        }

        if (!formGUID.Equals(""))
        {
            sql = "select distinct c.AuthorOrgUnitGUID from SmpDocAccessForm f, SmpDocAccessDetail d, SmpDocument c where f.GUID=d.DocAccessGUID and  d.DocGUID = c.GUID and f.GUID='" + formGUID + "'";

            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                authorOrgUnitGUID = ds.Tables[0].Rows[0][0].ToString();
            }
            values = base.getOrgUnitInfo(engine, authorOrgUnitGUID);
			//values = base.getUserManagerInfo(engine, authorOrgUnitGUID);
            authorManager = values[3];
        }

        xml += "<SPKM004>";
        xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
        xml += "<checkBy DataType=\"java.lang.String\">" + checkBy + "</checkBy>";
        xml += "<typeAdm DataType=\"java.lang.String\">" + typeAdm + "</typeAdm>";
        xml += "<manager DataType=\"java.lang.String\">" + manager + "</manager>";
        xml += "<confidentialLevel DataType=\"java.lang.String\">" + confidentialLevel + "</confidentialLevel>";
        xml += "<notifier DataType=\"java.lang.String\">" + notifier + "</notifier>";
        xml += "<makeManager DataType=\"java.lang.String\">" + authorManager + "</makeManager>";
        xml += "</SPKM004>";
        
        //表單代號
        param["SPKM004"] = xml;
        return "SPKM004";
    }

    
    
    /// <summary>
    /// 若有加簽，送簽核前呼叫。
    /// 加簽時系統會設定Session("IsAddSign")，所以必需在saveData時執行 setSession("IsAddSign", "AFTER");
    /// AFTER 代表往後簽核
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {        
        return base.beforeSign(engine, isAfter, addSignXml);
    }

    /// <summary>
    /// 重辦程序
    /// </summary>
    protected override void rejectProcedure()
    {
        //退回填表人終止流程
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回後關卡ID
        if (backActID.ToUpper().Equals("CREATOR"))
        {
            try
            {
                base.terminateThisProcess();
            }
            catch (Exception e)
            {
                base.writeLog(e);
                throw new Exception(e.StackTrace);
            }
        }
        else
        {
            base.rejectProcedure();
        }
    }

    /// <summary>
    /// 按下送簽按鈕後呼叫此方法。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
        string actName = Convert.ToString(getSession("ACTName"));
        if (actName.Equals("文管中心"))
        {
            
        }
        base.afterSign(engine, currentObject, result);
    }

    /// <summary>
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {       
        if (result.Equals("Y"))
        {
            string userGUID = (string)Session["UserGUID"];
            string formGUID = currentObject.getData("GUID");
            string expiryDate = currentObject.getData("EndDate");
            string now = DateTimeUtility.getSystemTime2(null);
            string subject = currentObject.getData("Subject");
            string histogyGUID = "";
            
            string sql = null;
            DataSet ds = null;
            int count = 0;
            string revGUID = "";
            string docGUID = "";
            string originatorGUID = "";
            string endDate = "";

            //寫入讀者SmpReader
            sql = "select  sr.GUID RevGUID, sd.GUID DocGUID, f.OriginatorGUID, f.EndDate From SmpDocAccessForm f , SmpDocAccessDetail d, SmpDocument sd, SmpRev sr where f.GUID = d.DocAccessGUID and f.GUID='" + formGUID + "' and d.DocGUID = sd.GUID and sd.GUID = sr.DocGUID   and sr.LatestFlag='Y' ";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables.Count > 0)
            {
                count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    revGUID = ds.Tables[0].Rows[i][0].ToString();
                    docGUID = ds.Tables[0].Rows[i][1].ToString();
                    originatorGUID = ds.Tables[0].Rows[i][2].ToString();
                    endDate = ds.Tables[0].Rows[i][3].ToString();
                    histogyGUID = IDProcessor.getID(""); //寫入SmpReader GUID
                    //SmpReader
                    sql = "insert into SmpReader (GUID, RevGUID, DocGUID, BelongGroupType, BelongGroupGUID,EffectiveDate, ExpiryDate, IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME, FromAccess) "
                        + "values ('" + histogyGUID + "','" + revGUID + "','" + docGUID + "',1,'" + originatorGUID + "','" + now.Substring(0, 10) + "' , '" + endDate + "','Y','N','Y','" + userGUID + "','" + now + "' , '' , '' , 'Y')";

                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }

                    //新增文件歷史記錄
                    string action = "文件調閱";
                    string description = subject + " 文件調閱";
                    sql = "insert into SmpHistory (GUID, DocGUID, Action, Description, RevGUID, FormGUID, IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) "
                        + "select '" + histogyGUID + "' GUID, '" + docGUID + "' DocGUID, '" + action + "' Action, '" + description + "' Description, '" + revGUID + "' RevGUID, '" + formGUID + "' FormGUID, "
                        + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + originatorGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME from SmpDocAccessForm where GUID='" + formGUID + "'";

                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }

                }
            }            
        }
        base.afterApprove(engine, currentObject, result);
    }

    /// <summary>
    /// 文件調閱檢查
    /// 系統調閱者是否無讀取權限, 若己有權限則提出警示
    /// </summary>
    /// <returns></returns>
    private string docAccessCheck(string docGUID)
    {
        AbstractEngine engine = null;
        DataSet ds = null;
        string sql = null;
        string strErrMsg = "";
        try
        {
            string connectString = (string)Session["connectString"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, connectString);

            ////沒有 管理者/歸屬群組/讀者權限 才可以點選新增按鈕
            //string userGUID = (string)Session["UserGUID"];
            string originatorGUID = OriginatorGUID.GuidValueText;
            string authorGUID = "";
            string docTypeGUID = "";
            string revGUID = "";
            sql = "select d.AuthorGUID, i.DocTypeGUID, r.GUID from SmpDocument d, SmpRev r, SmpIndexCard i where d.GUID='" + docGUID + "' and d.GUID=r.DocGUID and i.GUID=r.IndexCardGUID and r.Released='Y' and r.LatestFlag='Y'";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                authorGUID = ds.Tables[0].Rows[0][0].ToString();
                docTypeGUID = ds.Tables[0].Rows[0][1].ToString();
                revGUID = ds.Tables[0].Rows[0][2].ToString();
            }

            bool isCanView = false;
            bool isTypeAdmin = base.isTypeAdmin(engine, docTypeGUID, originatorGUID);
            bool isBelongGroup = base.isBelongGroup(engine, revGUID, originatorGUID);
            bool isReader = base.isReader(engine, revGUID, originatorGUID);
            bool isAuthor = false;
            if (authorGUID.Equals((string)Session["UserGUID"]))
            {
                isAuthor = true;
            }
            if (isAuthor)
            {
                strErrMsg += "此文件的管理者無需調閱文件!";
            }

            if (isTypeAdmin || isBelongGroup || isAuthor || isReader)
            {
                isCanView = true;
            }
            if (isCanView)
            {
                strErrMsg += "此文件的管理者/歸屬群組/作者/讀者無需調閱文件!";
            }
                        
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
        }
        return strErrMsg;
    }
	
	protected void ViewDocButton_Click(object sender, EventArgs e)
    {
		//return strErrMsg;
    }

	protected void addButton_Click(object sender, EventArgs e)
    {
		//return strErrMsg;
    }

    protected bool DocAccessList_SaveRowData(com.dsc.kernal.databean.DataObject objects, bool isNew)
    {
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("DocAccessGUID", "TEMP");
            objects.setData("DocGUID", DocGUID.GuidValueText);
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }

        string[] keys = objects.keyField;
        objects.keyField = new string[] { "DocGUID" };
        DataObjectSet docSet = DocAccessList.dataSource;
        if (!docSet.checkData(objects))
        {
            MessageBox("資料重覆!");
            objects.keyField = keys;
            return false;
        }
        return true;
    }

    protected void DocAccessList_ShowRowData(com.dsc.kernal.databean.DataObject objects)
    {
        DocGUID.GuidValueText = objects.getData("DocGUID");
        DocGUID.doGUIDValidate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    protected void DocGUID_SingleOpenWindow(string[,] values)
    {
        string strErrMsg = "";
        bool isAddNew = base.isNew();
        if (isAddNew)
        {            
            string docGUID = DocGUID.GuidValueText;
            if (!docGUID.Equals(""))
            {
                strErrMsg += docAccessCheck(docGUID);
                if (!strErrMsg.Equals(""))
                {
                    DocGUID.ValueText = "";
                    DocGUID.GuidValueText = "";
                    DocGUID.ReadOnlyValueText = "";
                    GlassButtonViewDoc.ReadOnly = true;
                    MessageBox(strErrMsg);
                }
                else
                {
                    GlassButtonViewDoc.ReadOnly = false; 
                }
            }
            else
            {
                GlassButtonViewDoc.ReadOnly = true;
            }
        }
        if (!DocGUID.ValueText.Equals("")) {
            GlassButtonViewDoc.ReadOnly = false;
        }
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        int count = 0;        
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            string whereCondition = "";

            string docGUID = DocGUID.GuidValueText;
            bool strReturn = true;  //檢查是否同一子類別
            bool strReturn1 = true; //檢查是否重覆資料

            if (!docGUID.Equals(""))
            {
                whereCondition += " and sdc.GUID = '" + docGUID + "' ";
            }
            else
            {
                whereCondition += " and 1=2 ";
            }

            DataObject doo = null;
            DataObjectSet dos = DocAccessList.dataSource;
            string subGUID = "";

            string sql = " select sdc.GUID, sm.Name MajorName, ss.Name SubName, sd.Name TypeName , sdc.DocNumber, sdc.DocNumber DocName, u.userName UserName, sr.RevNumber, si.ConfidentialLevel, si.Name IndexDocName, sl.Name DocPropertyName ";
            sql += " ,sd.GUID DocTypeGUID, sr.GUID RevGUID, sdc.AuthorGUID, ss.GUID SubGUID ";
            sql += " from SmpDocument  sdc ";
            sql += " join Users u  on  sdc.AuthorGUID = u.OID ";
            sql += " join  SmpRev sr on   sdc.GUID = sr.DocGUID   and sr.LatestFlag='Y' ";
            sql += " join SmpIndexCard si on si.GUID = sr.IndexCardGUID ";
            sql += " left join SmpDocType  sd on sd.GUID = si.DocTypeGUID ";
            sql += " left join SmpSubType ss on  ss.GUID = sd.SubTypeGUID ";
            sql += " left join SmpMajorType sm  on sm.GUID =ss.MajorTypeGUID  ";
            sql += " left join SmpListName sl on sl.GUID =  si.DocPropertyGUID ";
            sql += " where  sr.LatestFlag='Y' ";
            sql += whereCondition;

            DataSet allDocAccessList = engine.getDataSet(sql, "TEMP");
            count = allDocAccessList.Tables[0].Rows.Count;

            for (int i = 0; i < count; i++)
            {
                doo = dos.create();

                doo.setData("GUID", IDProcessor.getID(""));
                doo.setData("DocAccessGUID", "TEMP");
                doo.setData("DocGUID", allDocAccessList.Tables[0].Rows[i]["GUID"].ToString());                
                doo.setData("IS_DISPLAY", "Y");
                doo.setData("IS_LOCK", "N");
                doo.setData("DATA_STATUS", "Y");
                doo.setData("DocNumber", allDocAccessList.Tables[0].Rows[i]["DocNumber"].ToString());
                doo.setData("MajorName", allDocAccessList.Tables[0].Rows[i]["MajorName"].ToString());
                doo.setData("SubName", allDocAccessList.Tables[0].Rows[i]["SubName"].ToString());
                doo.setData("TypeName", allDocAccessList.Tables[0].Rows[i]["TypeName"].ToString());
                doo.setData("DocPropertyName", allDocAccessList.Tables[0].Rows[i]["DocPropertyName"].ToString());
                doo.setData("DocName", allDocAccessList.Tables[0].Rows[i]["IndexDocName"].ToString());
                doo.setData("AuthorName", allDocAccessList.Tables[0].Rows[i]["UserName"].ToString());
                doo.setData("RevNumber", allDocAccessList.Tables[0].Rows[i]["RevNumber"].ToString());
                doo.setData("DocConfidentialLevel", allDocAccessList.Tables[0].Rows[i]["ConfidentialLevel"].ToString());
                doo.setData("DocTypeGUID", allDocAccessList.Tables[0].Rows[i]["DocTypeGUID"].ToString());
                doo.setData("RevGUID", allDocAccessList.Tables[0].Rows[i]["RevGUID"].ToString());
                doo.setData("AuthorGUID", allDocAccessList.Tables[0].Rows[i]["AuthorGUID"].ToString()); 
                subGUID = allDocAccessList.Tables[0].Rows[i]["SubGUID"].ToString();
                doo.setData("SubGUID", subGUID);                

                //資料檢查
                strReturn = docAccessDetailCheck(doo);
                //sw.WriteLine("回傳資料 : " + strReturn);
                strReturn1 = docAccessDetailRepeatCheck(doo);
                //sw.WriteLine("回傳是否重覆資料 : " + strReturn);

                if (strReturn && strReturn1)
                {
                    dos.add(doo);
                    DocAccessList.NoAdd = true;
                    DocAccessList.NoModify = true;
                    DocAccessList.dataSource = dos;
                    DocAccessList.HiddenField = new string[] { "GUID", "DocAccessGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY", "DocConfidentialLevel", "DocGUID", "DocTypeGUID", "RevGUID", "AuthorGUID", "SubGUID" };
                    DocAccessList.updateTable();
                }

            }
            engine.close();

        }
        catch (Exception ze)
        {
            try
            {
                engine.close();
            }
            catch { };
            MessageBox(ze.Message);
            writeLog(ze);
        }
        finally
        {
            //if (sw != null) sw.Close();
        }
    }

    protected void GlassButtonViewDoc_Click(object sender, EventArgs e)
    {
        setSession((string)Session["UserID"], "ViewDocGUID", DocGUID.GuidValueText);
    }

    protected bool docAccessDetailCheck(com.dsc.kernal.databean.DataObject objects)
    {        
        string[] keys = objects.keyField;
        objects.keyField = new string[] { "MajorName", "SubName" };
        DataObjectSet subNameSet = DocAccessList.dataSource;
        if (subNameSet.getDataObjectCount() > 0)
        {
            if (!subNameSet.checkData(objects))
            {
                //objects.keyField = keys;
                //objects.keyField = new string[] { "GUID" };
                //return true;
            }
            else {
                MessageBox("子分類必須為同一類別!");
                objects.keyField = keys;
                return false;
            }
        }
        keys = objects.keyField;
        objects.keyField = new string[] { "GUID" };
        return true;
    }

    protected bool docAccessDetailRepeatCheck(com.dsc.kernal.databean.DataObject objects)
    {
        string[] keys = objects.keyField;
        objects.keyField = new string[] { "DocGUID" };
        
        DataObjectSet accessSet = DocAccessList.dataSource;
        if (!accessSet.checkData(objects))
        {
            MessageBox("調閱文件資料重覆!");
            objects.keyField = keys;
            return false;
        }
        keys = objects.keyField;
        objects.keyField = new string[] { "GUID" };
        return true;
    }


    protected void DocGUID_BeforeClickButton()
    {        
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            DocGUID.whereClause = "(Status != 'Cancelled')";
        }
    }
    protected void OriginatorGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            OriginatorGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
}