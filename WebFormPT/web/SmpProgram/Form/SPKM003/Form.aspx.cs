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


public partial class SmpProgram_Form_SPKM003_Form : SmpKmFormPage 
{

    /// <summary>
    /// 初始化參數。
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPKM003";
        AgentSchema = "WebServerProject.form.SPKM003.SmpDocVoidFormAgent";
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

        ConfidentialLevel.Display = false;
        DocTypeGUID.Display = false;

        bool isAddNew = base.isNew();
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string sql = null;
        string[,] ids = null;
        DataSet ds = null;
        int count = 0;
		
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

        //申請人
        OriginatorGUID.clientEngineType = engineType;
        OriginatorGUID.connectDBString = connectString;
        if (OriginatorGUID.ValueText.Equals(""))
        {
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }

        //審核人一
        CheckBy1GUID.clientEngineType = engineType;
        CheckBy1GUID.connectDBString = connectString;

        //審核人二
        CheckBy2GUID.clientEngineType = engineType;
        CheckBy2GUID.connectDBString = connectString;

        //作廢文件編號
        VoidDocGUID.clientEngineType = engineType;
        VoidDocGUID.connectDBString = connectString;

        //作廢版本
        if (isAddNew)
        {
            ids = new string[,]{
                {"",""}
            };
        }
        else
        {
            string docGUID = objects.getData("VoidDocGUID");
            if (!Convert.ToString(docGUID).Equals(""))
            {
                sql = "select GUID,RevNumber from SmpRev where DocGUID='" + docGUID + "'";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables.Count > 0)
                {
                    count = ds.Tables[0].Rows.Count;
                    ids = new string[1 + count, 2];
                    ids[0, 0] = "";
                    ids[0, 1] = "";
                    for (int i = 0; i < count; i++)
                    {
                        ids[1 + i, 0] = ds.Tables[0].Rows[i][0].ToString();
                        ids[1 + i, 1] = ds.Tables[0].Rows[i][1].ToString();
                    }
                }
            }
        }

        //由文件查詢按鈕帶入DocGUID
        if (isAddNew)
        {
            if (VoidDocGUID.ValueText.Equals(""))
            {
                string docGUID = Request.QueryString["DocGUID"];
                if (docGUID != null && !docGUID.Equals(""))
                {
                    VoidDocGUID.GuidValueText = docGUID;
                    VoidDocGUID.doGUIDValidate();
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
		//System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"d:\temp\SPERP005.log", true, System.Text.Encoding.Default);
		
        bool isAddNew = base.isNew();
        string actName = Convert.ToString(getSession("ACTName"));
        ConfidentialLevel.Display = false;
        DocTypeGUID.Display = false;

        if (isAddNew)
        {
            OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID");
            OriginatorGUID.doGUIDValidate();
            //審核人一
            CheckBy1GUID.GuidValueText = objects.getData("CheckBy1GUID");
            CheckBy1GUID.doGUIDValidate();
            //審核人二
            CheckBy2GUID.GuidValueText = objects.getData("CheckBy2GUID");
            CheckBy2GUID.doGUIDValidate();
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
		
        //審核人一
        CheckBy1GUID.GuidValueText = objects.getData("CheckBy1GUID");
        CheckBy1GUID.doGUIDValidate();
        
        //審核人二
        CheckBy2GUID.GuidValueText = objects.getData("CheckBy2GUID");
        CheckBy2GUID.doGUIDValidate();
        
        //作廢文件編號
        VoidDocGUID.GuidValueText = objects.getData("VoidDocGUID");
        VoidDocGUID.doGUIDValidate();
		//sw.WriteLine("作廢文件編號 : " +VoidDocGUID.GuidValueText);

        //作廢原因
        VoidReason.ValueText = objects.getData("VoidReason");

        ConfidentialLevel.ValueText = objects.getData("ConfidentialLevel");
        DocTypeGUID.ValueText = objects.getData("DocTypeGUID");

        if (!actName.Equals("") && !actName.Equals("填表人"))
        {
            //表單發起後不允許修改
            Subject.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            CheckBy1GUID.ReadOnly = true;
            CheckBy2GUID.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            VoidDocGUID.ReadOnly = true;
            VoidReason.ReadOnly = true;
            GlassButtonViewDoc.ReadOnly = false;
        }
		
        //sw.WriteLine("xml : " + xml);
        //sw.Close();
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
                objects.setData("CheckBy1", CheckBy1GUID.GuidValueText);
                objects.setData("CheckBy2", CheckBy2GUID.GuidValueText);
                objects.setData("VoidDocGUID", VoidDocGUID.GuidValueText);
                objects.setData("VoidReason", VoidReason.ValueText);
            }

            
            if (!actName.Equals("直屬主管"))
            {
                setSession("IsAddSign", "AFTER"); //beforeSign 加簽
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
		string voidReason = VoidReason.ValueText;
        string docGUID = VoidDocGUID.ValueText;
		bool isAddNew = base.isNew();
        if (isAddNew)
        {
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = "(" + docGUID + ") 文件作廢";
            }
        }
		
		if (actName.Equals("") && actName.Equals("填表人"))
        {
			if (voidReason.Equals(""))
            {
                strErrMsg += "請維護作廢原因\n";
            }
            if (docGUID.Equals(""))
            {
                strErrMsg += "請維護文件號碼\n";
            }
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
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"d:\temp\SPERP005.log", true, System.Text.Encoding.Default);

        string xml = "";
        string originatorId = OriginatorGUID.ValueText;
        string checkBy = "";
        string typeAdm = "";
        string manager = "";
        string[][] valuesAdm = null;
        //機密等級, 
        string confidentialLevel = ConfidentialLevel.ValueText;
        string docTypeGUID = DocTypeGUID.ValueText;

        string notifier = "";
        string[] values = null;

        if (!CheckBy1GUID.ValueText.Equals(""))
        {
            checkBy += CheckBy1GUID.ValueText + ";";
        }
        if (!CheckBy2GUID.ValueText.Equals(""))
        {
            checkBy += CheckBy2GUID.ValueText;
        }

        //取得管理者(主類別+子類別)
        valuesAdm = getDocTypeAdmUserGUID(engine, docTypeGUID);
        if (valuesAdm != null && valuesAdm.Length > 0)
        {
            for (int i = 0; i < valuesAdm.Length; i++)
                typeAdm += valuesAdm[i][1] + ";";
        }
        
        if (!typeAdm.Equals(""))
        {
            notifier += typeAdm ;
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

        xml += "<SPKM003>";
        xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
        xml += "<checkBy DataType=\"java.lang.String\">" + checkBy + "</checkBy>";
        xml += "<typeAdm DataType=\"java.lang.String\">" + typeAdm + "</typeAdm>";
        xml += "<manager DataType=\"java.lang.String\">" + manager + "</manager>";
        xml += "<confidentialLevel DataType=\"java.lang.String\">" + confidentialLevel + "</confidentialLevel>";
        xml += "<notifier DataType=\"java.lang.String\">" + notifier + "</notifier>";
        xml += "</SPKM003>";
        //sw.WriteLine("xml : " + xml);
        //sw.Close();

        //表單代號
        param["SPKM003"] = xml;
        return "SPKM003";
    }

    
    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"d:\temp\SPERP005.log", true, System.Text.Encoding.Default);

        //產生作廢版本記錄
        string sql = "";
        string docGUID = VoidDocGUID.GuidValueText;
        string formGUID = currentObject.getData("GUID");
        string indexCardGUID = IDProcessor.getID("");
        string revGUID = IDProcessor.getID("");
        string revNumber = "?";
        string userGUID = (string)Session["UserGUID"];
        string now = DateTimeUtility.getSystemTime2(null);
        string connectString = (string)Session["connectString"];
        string sheetNo = (string)getSession(PageUniqueID, "SheetNo");
        IOFactory factory = new IOFactory();
        AbstractEngine engine1 = factory.getEngine(EngineConstants.SQL, connectString);

        try
        {
            //新增索引卡
            sql = "insert into SmpIndexCard (GUID, Status, MajorTypeGUID, SubTypeGUID, DocTypeGUID, DocPropertyGUID, ConfidentialLevel, DocGUID, Name, Abstract, KeyWords, EffectiveDate, ExpiryDate, "
                + "IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) "
                + "select '" + indexCardGUID + "' GUID, 'Cancel' Status, i.MajorTypeGUID, i.SubTypeGUID, i.DocTypeGUID, i.DocPropertyGUID, i.ConfidentialLevel, i.DocGUID, i.Name, i.Abstract, i.KeyWords,  i.EffectiveDate, i.ExpiryDate, "
                + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + userGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME "
                + "from SmpIndexCard i, SmpRev r where r.DocGUID='" + docGUID + "' and r.Released='Y' and r.LatestFlag='Y' and i.GUID=r.IndexCardGUID";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }
            //sw.WriteLine("新增索引卡 : " + sql);
            
            //新增版本
            sql = "select RevNumber from SmpRev where DocGUID='" + docGUID + "' and Released='Y' and LatestFlag='Y'";
            revNumber = Convert.ToString(Convert.ToInt16((string)engine.executeScalar(sql)));

            sql = "insert into SmpRev (GUID, RevNumber, DocGUID, FormGUID, IndexCardGUID, Released, LatestFlag, ReleaseDate, "
                + "IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME, SheetNo) "
                + "select '" + revGUID + "' GUID, '" + revNumber + "' RevNumber, '" + docGUID + "' DocGUID, '" + formGUID + "' FormGUID, '" + indexCardGUID + "' IndexCardGUID, 'N' Released, 'N' LatestFlag, '' ReleaseDate, "
                + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + userGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME, "
                + "'" + sheetNo + "' "
                + "from SmpRev where DocGUID='" + docGUID + "' and LatestFlag='Y' ";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }
            //sw.WriteLine("新增版本 : " + sql);
                        
            
            //新增歸屬群組
            sql = "insert into SmpDocBelongGroup (GUID, DocGUID, RevGUID, BelongGroupType, BelongGroupGUID, "
                + "IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) "
                + "select newid(), b.DocGUID, '"+revGUID+"' RevGUID, b.BelongGroupType, b.BelongGroupGUID, "
                + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + userGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME "
                + "from SmpDocBelongGroup b, SmpRev r where r.DocGUID='" + docGUID + "' and b.RevGUID=r.GUID and r.Released='Y' and r.LatestFlag='Y'";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }
            //sw.WriteLine("新增歸屬群組 : " + sql);

            //新增附件
            sql = "insert into SmpAttachment (GUID, DocGUID, RevGUID, FileItemGUID, AttachmentType, "
                + "IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) "
                + "select newid(), a.DocGUID, '" + revGUID + "' RevGUID, a.FileItemGUID, a.AttachmentType, "
                + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + userGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME "
                + "from SmpAttachment a, SmpRev r where r.DocGUID='" + docGUID + "' and a.RevGUID=r.GUID and r.Released='Y' and r.LatestFlag='Y' ";  //and a.AttachmentType='Original'";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }
            //sw.WriteLine("新增附件 : " + sql);


            //新增參考文件
            sql = "insert into SmpReference (GUID, DocGUID, RevGUID, Source, Reference, "
                + "IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) "
                + "select newid(), a.DocGUID, '" + revGUID + "' RevGUID, a.Source, a.Reference, "
                + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + userGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME "
                + "from SmpReference a, SmpRev r where r.DocGUID='" + docGUID + "' and a.RevGUID=r.GUID and r.Released='Y' and r.LatestFlag='Y'";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }
            //sw.WriteLine("新增參考文件 : " + sql);


            //新增讀者
            sql = "insert into SmpReader (GUID, DocGUID, RevGUID, BelongGroupType, BelongGroupGUID, EffectiveDate, ExpiryDate, "
                + "IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) "
                + "select newid(), a.DocGUID, '" + revGUID + "' RevGUID, a.BelongGroupType, a.BelongGroupGUID, a.EffectiveDate, a.ExpiryDate, "
                + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + userGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME "
                + "from SmpReader a, SmpRev r where r.DocGUID='" + docGUID + "' and a.RevGUID=r.GUID and r.Released='Y' and r.LatestFlag='Y'";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }
            //sw.WriteLine("新增讀者 : " + sql);
            
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (engine1 != null)
            {
                engine1.close();
            }
            //if (sw != null)
            //{
            //    sw.Close();
            //}
        }
        base.afterSend(engine, currentObject);
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
            string originatorGUID = currentObject.getData("OriginatorGUID");
            string formGUID = currentObject.getData("GUID");
            string docGUID = currentObject.getData("VoidDocGUID");
            string revGUID = currentObject.getData("RevGUID");
            string indexCardGUID = currentObject.getData("IndexCardGUID");
            string subject = currentObject.getData("Subject");
            string expiryDate = currentObject.getData("ExpiryDate");

            string histogyGUID = IDProcessor.getID("");
            string now = DateTimeUtility.getSystemTime2(null);
            string sql = null;


            if (expiryDate.Equals(""))
            {
                expiryDate = (Convert.ToInt16(now.Substring(0, 4)) + 2) + now.Substring(4);
            }

            //更新此版本為生效最後一版
            sql = "update SmpRev set Released = 'Y', LatestFlag='Y', ReleaseDate='" + now + "', D_MODIFYUSER='" + userGUID + "', D_MODIFYTIME='" + now + "' where DocGUID='" + docGUID + "' and GUID='" + revGUID + "'";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }
            //更新前版為不是最後一版
            sql = "update SmpRev set LatestFlag='N', D_MODIFYUSER='" + userGUID + "', D_MODIFYTIME='" + now + "' where DocGUID='" + docGUID + "' and LatestFlag='Y' and GUID != '" + revGUID + "'";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }
            //更新索引卡內容狀態為結案
            sql = "update SmpIndexCard set Status='Cancelled', EffectiveDate='" + now.Substring(0, 10) + "', ExpiryDate='" + expiryDate + "' , D_MODIFYUSER='" + userGUID + "', D_MODIFYTIME='" + now + "' where DocGUID='" + docGUID + "' and GUID='" + indexCardGUID + "'";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }
            //新增文件歷史記錄
            string action = "文件作廢";
            string description = subject + " 文件作廢";
            sql = "insert into SmpHistory (GUID, DocGUID, Action, Description, RevGUID, FormGUID, IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) "
                + "select '" + histogyGUID + "' GUID, '" + docGUID + "' DocGUID, '" + action + "' Action, '" + description + "' Description, '" + revGUID + "' RevGUID, '" + formGUID + "' FormGUID, "
                + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + originatorGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME from SmpDocVoidForm where GUID='" + formGUID + "'";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }
        }
        base.afterApprove(engine, currentObject, result);
    }




    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    protected void VoidDocGUID_SingleOpenWindow(string[,] values)
    {
        string strErrMsg = "";
        bool isAddNew = base.isNew();
        if (isAddNew)
        {            
            string docGUID = VoidDocGUID.GuidValueText;
            if (!docGUID.Equals(""))
            {
                strErrMsg += docReviewCheck(docGUID);
                if (!strErrMsg.Equals(""))
                {
                    VoidDocGUID.ValueText = "";
                    VoidDocGUID.GuidValueText = "";
                    VoidDocGUID.ReadOnlyValueText = "";
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
    }

    /// <summary>
    /// 文件作廢檢查
    /// </summary>
    /// <returns></returns>
    private string docReviewCheck(string docGUID)
    {
        AbstractEngine engine = null;
        DataSet ds = null;
        string sql = null;
        string strErrMsg = "";
        try
        {
            bool isCanChange = false;
            bool isCancelled = false;
            bool isCreated = false;
            string connectString = (string)Session["connectString"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, connectString);

            sql = "select i.Status from SmpRev r, SmpIndexCard i, SmpDocument d "
                + "where d.GUID='" + docGUID + "' and i.Status='Create' "
                + "and r.Released='Y' and r.LatestFlag='Y' "
                + "and r.IndexCardGUID = i.GUID and r.DocGUID = d.GUID";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                isCreated = true;
                strErrMsg += "此文件新增中, 不可作廢!\n";
            }
            if (!engine.errorString.Equals(""))
            {
                strErrMsg += engine.errorString;
            }


            sql = "select i.Status from SmpRev r, SmpIndexCard i, SmpDocument d "
                + "where d.GUID='" + docGUID + "' and i.Status='Cancelled' "
                + "and r.Released='Y' and r.LatestFlag='Y' "
                + "and r.IndexCardGUID = i.GUID and r.DocGUID = d.GUID";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                isCancelled = true;
                strErrMsg += "此文件已失效!\n";
            }
            if (!engine.errorString.Equals(""))
            {
                strErrMsg += engine.errorString;
            }

            if (!isCancelled && !isCreated)
            {
                //文件一次只能有一張作廢單
                sql = "select a.SMWYAAA002, u.userName from SmpRev r, SMWYAAA a, Users u "
                    + "where DocGUID='" + docGUID + "' "
                    + "and r.FormGUID=a.SMWYAAA019 and r.Released = 'N' and r.LatestFlag='N' and a.SMWYAAA020 ='I' "
                    + "and a.D_INSERTUSER = u.OID";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sheetNo = ds.Tables[0].Rows[0][0].ToString();
                    string creator = ds.Tables[0].Rows[0][1].ToString();
                    strErrMsg += "此文件作廢中, 單號: " + sheetNo + " 建立者: " + creator + "!\n";
                }
                if (!engine.errorString.Equals(""))
                {
                    strErrMsg += engine.errorString;
                }



                //作廢權限檢查
                //作者/管理者/歸屬群組
                if (!isCanChange)
                {
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
                    //是否為作者
                    if (authorGUID.Equals((string)Session["UserGUID"]))
                    {
                        isCanChange = true;
                    }
                    //是否為管理者
                    if (isCanChange == false && base.isTypeAdmin(engine, docTypeGUID, originatorGUID))
                    {
                        isCanChange = true;
                    }
                    //是否為歸屬群組
                    if (isCanChange == false && base.isBelongGroup(engine, revGUID, originatorGUID))
                    {
                        isCanChange = true;
                    }
                }
                if (!isCanChange)
                {
                    strErrMsg += "此文件的管理者/歸屬群組/作者，才擁有文件作廢權限!\n";
                }
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            strErrMsg += e.Message;
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

    protected void GlassButtonViewDoc_Click(object sender, EventArgs e)
    {
        setSession((string)Session["UserID"], "ViewDocGUID", VoidDocGUID.GuidValueText);
    }


    protected void VoidDocGUID_BeforeClickButton()
    {        
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            VoidDocGUID.whereClause = "(Status != 'Cancelled')";
        }
    }
    protected void OriginatorGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            OriginatorGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
    protected void CheckBy1GUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            CheckBy1GUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
    protected void CheckBy2GUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            CheckBy2GUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
}