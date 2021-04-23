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
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;

public partial class SmpProgram_Form_SPIT002_Form : SmpBasicFormPage
{

    /// <summary>
    /// 初始化參數。
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPIT002";
        AgentSchema = "WebServerProject.form.SPIT002.SmpSystemReleaseFormAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPIT";
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
        sb.Append(" function submitCheck(){");
        sb.Append("     var infoDemand = document.getElementById('S_InfoDemandGUID').value;");
        sb.Append("     if(infoDemand == \"\") {");
        sb.Append("         if(confirm(\"[需求申請單單號]沒有輸入，請確認?\")){");
        sb.Append("             return true;");
        sb.Append("         } else {");
        sb.Append("             return false;");
        sb.Append("         }");
        sb.Append("     } else {");
        sb.Append("         return true;");
        sb.Append("     }");
        sb.Append(" }");
        sb.Append(" function clickViewInfoDemand(){");
        sb.Append("     parent.window.openWindowGeneral('檢視資訊需求申請單','" + Page.ResolveUrl("ViewInfoDemand.aspx") + "','','','',true,true);");
        sb.Append(" }");
        sb.Append("</script>");
        Type ctype = this.GetType();
        ClientScriptManager cm = Page.ClientScript;

        if (!cm.IsStartupScriptRegistered(ctype, "submitCheckScript"))
        {
            cm.RegisterStartupScript(ctype, "submitCheckScript", sb.ToString());
        }

        SubmitButton.BeforeClick = "submitCheck";
        GlassButtonViewInfoDemand.AfterClick = "clickViewInfoDemand";
        
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string[,] ids = null;

        //申請人
        OriginatorGUID.clientEngineType = engineType;
        OriginatorGUID.connectDBString = connectString;

        if (OriginatorGUID.ValueText.Equals(""))
        {
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }

        //QA人員
        Qa1GUID.clientEngineType = engineType;
        Qa1GUID.connectDBString = connectString;
        Qa2GUID.clientEngineType = engineType;
        Qa2GUID.connectDBString = connectString;

        //系統名稱
        ids = new string[,]{
                {"",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "ids0", "")},
                {"3",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "dellb2b", "DellB2B")},
                {"2",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "easyflow", "EasyFlow")},
                {"12",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "ekm", "eKM")},
                {"7",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "edms", "eDMS")},
                {"10",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "erpportal", "ERP Portal")},
                {"6",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "gib", "GIB")},
                {"11",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "gpm", "GPM")},
                {"9",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "isp", "iSupplier Portal(ISP)")},
                {"4",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "mes", "MES/SFC")},
                {"0",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "erp", "Oracle ERP")},
                {"5",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "others", "Others")},
                {"8",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "plm", "PLM")},
                {"1",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "workflow", "Workflow ERP")}
            };
        SystemName.setListItem(ids);

        //模組名稱
        ids = new string[,]{
                {"",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "ids0", "")},
                {"3",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "ap", "AP")},
                {"2",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "ar", "AR")},
                {"10",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "bom", "BOM")},
                {"13",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "cost", "COST")},
                {"4",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "fa", "FA")},
                {"15",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "gib", "GIB")},
                {"1",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "gl", "GL")},
                {"5",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "gv", "GV")},
                {"16",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "hr", "HR")},
                {"9",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "mst", "MST")},
                {"6",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "inv", "INV")},
                {"12",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "mrp", "MRP")},
                {"8",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "om", "OM")},
                {"7",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "po", "PO")},
                {"11",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "wip", "WIP")},
                {"16",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "salary", "人事新資")},
            };
        CategoryName.setListItem(ids);
        CategoryName.ReadOnly = true;

        //需求部門
        RequesterOrgUnitGUID.clientEngineType = engineType;
        RequesterOrgUnitGUID.connectDBString = connectString;

        //需求單單號
        InfoDemandGUID.clientEngineType = engineType;
        InfoDemandGUID.connectDBString = connectString;
        
        //是否含包KM文件
        ids = new string[,]{
                {"",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "ids0", "")},
                {"Y",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "Y", "是")},
                {"N",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stit002_form_aspx.language.ini", "message", "N", "否")}
            };
        IncludeKmDoc.setListItem(ids);

        //文件編號
        DocGUID.clientEngineType = engineType;
        DocGUID.connectDBString = connectString;

        //不允許修改欄位
        ActualReleaseDateTime.ReadOnly = true;
        QaDescription.ReadOnly = true;
        VssDescription.ReadOnly = true;

        HyperLinkSd.Visible = false;
        HyperLinkUt.Visible = false;
        HyperLinkQa.Visible = false;

        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            //InfoDemandGUID.whereClause = " UserId='" + (string)Session["UserID"] + "' and SMWYAAA020='I'";
            InfoDemandGUID.whereClause = " (UserId='" + (string)Session["UserID"] + "' or UserId='NA') and (EstimateCompleteDate > getDate()-35 or SMWYAAA020='I')";

            OriginatorGUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
            Qa1GUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
            Qa2GUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
        }

        GlassButtonViewInfoDemand.ReadOnly = true;

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
        //顯示單號
        base.showData(engine, objects);
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //申請人
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        //QA人員
        Qa1GUID.GuidValueText = objects.getData("Qa1GUID");
        Qa1GUID.doGUIDValidate();
        Qa2GUID.GuidValueText = objects.getData("Qa2GUID");
        Qa2GUID.doGUIDValidate();
        //系統名稱
        SystemName.ValueText = objects.getData("SystemName");
        //模組名稱
        CategoryName.ValueText = objects.getData("CategoryName");
        //需求部門
        RequesterOrgUnitGUID.GuidValueText = objects.getData("RequesterOrgUnitGUID");
        RequesterOrgUnitGUID.doGUIDValidate();
        //需求單單號
        InfoDemandGUID.GuidValueText = objects.getData("InfoDemandGUID");
        if (!InfoDemandGUID.GuidValueText.Equals(""))
        {
            InfoDemandGUID.doGUIDValidate();
        }
        //預計上線日
        ExpectReleaseDateTime.ValueText = objects.getData("ExpectReleaseDateTime");
        //實際上線日
        ActualReleaseDateTime.ValueText = objects.getData("ActualReleaseDateTime");
        //程式/報表名稱
        ProgramName.ValueText = objects.getData("ProgramName");
        //是否含包KM文件
        IncludeKmDoc.ValueText = objects.getData("IncludeKmDoc");
        //文件編號
        DocGUID.GuidValueText = objects.getData("DocGUID");
        if (!DocGUID.GuidValueText.Equals(""))
        {
            DocGUID.doGUIDValidate();
        }
        //上線文件
        SdDocFilePath.ValueText = objects.getData("SdDocFilePath");
        UtDocFilePath.ValueText = objects.getData("UtDocFilePath");
        QaDocFilePath.ValueText = objects.getData("QaDocFilePath");
        //說明
        Description.ValueText = objects.getData("Description");
        //QA說明
        QaDescription.ValueText = objects.getData("QaDescription");
        //VSS說明
        VssDescription.ValueText = objects.getData("VssDescription");

        if (!SdDocFilePath.ValueText.Equals(""))
        {
            HyperLinkSd.Visible = true;
            HyperLinkSd.NavigateUrl = SdDocFilePath.ValueText;
        }
        if (!UtDocFilePath.ValueText.Equals(""))
        {
            HyperLinkUt.Visible = true;
            HyperLinkUt.NavigateUrl = UtDocFilePath.ValueText;
        }
        if (!QaDocFilePath.ValueText.Equals(""))
        {
            HyperLinkQa.Visible = true;
            HyperLinkQa.NavigateUrl = QaDocFilePath.ValueText;
        }

        string actName = Convert.ToString(getSession("ACTName"));
        if (actName.Equals("填表人") || actName.Equals(""))
        {
            ActualReleaseDateTime.ReadOnly = true;
            QaDescription.ReadOnly = true;
            VssDescription.ReadOnly = true;
        }
        else
        {
            //表單發起後不允許修改
            Subject.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            Qa1GUID.ReadOnly = true;
            Qa2GUID.ReadOnly = true;
            SystemName.ReadOnly = true;
            CategoryName.ReadOnly = true;
            RequesterOrgUnitGUID.ReadOnly = true;
            InfoDemandGUID.ReadOnly = true;
            ExpectReleaseDateTime.ReadOnly = true;
            ActualReleaseDateTime.ReadOnly = true;
            ProgramName.ReadOnly = true;
            IncludeKmDoc.ReadOnly = true;
            DocGUID.ReadOnly = true;
            SdDocFilePath.ReadOnly = true;
            UtDocFilePath.ReadOnly = true;
            QaDocFilePath.ReadOnly = true;
            Description.ReadOnly = true;
            QaDescription.ReadOnly = true;
            VssDescription.ReadOnly = true;
        }
        
        if (actName.Equals("驗證"))
        {
            AddSignButton.Display = true;
            QaDocFilePath.ReadOnly = false;
            QaDescription.ReadOnly = false;
            QaDocFilePath.ValueText = UtDocFilePath.ValueText;
        }
        else if (actName.Equals("驗收"))
        {
            Description.ReadOnly = false;
            QaDescription.ReadOnly = false;
            
            attachFile.ReadOnly = false;
            attachFile.NoDelete = false;
            attachFile.NoAdd = false;
            IncludeKmDoc.ReadOnly = false;
        }
        else if (actName.Equals("上線"))
        {
            ExpectReleaseDateTime.ReadOnly = false;
            ActualReleaseDateTime.ReadOnly = false;
            VssDescription.ReadOnly = false;
        }

        if (isAddNew)
        {
            //InfoDemandGUID.whereClause = " UserId='" + (string)Session["UserID"] + "' and SMWYAAA020='I'";
            QaDescription.ValueText = "";
            VssDescription.ValueText = "";
            QaDocFilePath.ValueText = "";
        }

        if (!InfoDemandGUID.GuidValueText.Equals(""))
        {
            if (!InfoDemandGUID.GuidValueText.Equals("NA"))
            {
                GlassButtonViewInfoDemand.ReadOnly = false;
            }
        }
    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
            objects.setData("Qa1GUID", Qa1GUID.GuidValueText);
            objects.setData("Qa2GUID", Qa2GUID.GuidValueText);
            //for MCloud
            //objects.setData("OriginatorUserName", OriginatorGUID.ReadOnlyValueText);
            //objects.setData("Qa1UserName", Qa1GUID.ReadOnlyValueText);
            //objects.setData("Qa2UserName", Qa2GUID.ReadOnlyValueText);
            objects.setData("SystemNameValue", SystemName.ReadOnlyText);
            objects.setData("CategoryNameValue", CategoryName.ReadOnlyText);
            //objects.setData("RequesterOrgUnitName", RequesterOrgUnitGUID.ReadOnlyValueText);

            //產生單號並儲存至資料物件
            base.saveData(engine, objects);
        }
        objects.setData("SystemName", SystemName.ValueText);
        objects.setData("CategoryName", CategoryName.ValueText);
        objects.setData("RequesterOrgUnitGUID", RequesterOrgUnitGUID.GuidValueText);
        objects.setData("InfoDemandGUID", InfoDemandGUID.GuidValueText);
        objects.setData("ExpectReleaseDateTime", ExpectReleaseDateTime.ValueText);
        objects.setData("ActualReleaseDateTime", ActualReleaseDateTime.ValueText);
        objects.setData("ProgramName", ProgramName.ValueText);
        objects.setData("IncludeKmDoc", IncludeKmDoc.ValueText);
        objects.setData("DocGUID", DocGUID.GuidValueText);
        objects.setData("SdDocFilePath", SdDocFilePath.ValueText);
        objects.setData("UtDocFilePath", UtDocFilePath.ValueText);
        objects.setData("QaDocFilePath", QaDocFilePath.ValueText);
        objects.setData("Description", Description.ValueText);
        objects.setData("QaDescription", QaDescription.ValueText);
        objects.setData("VssDescription", VssDescription.ValueText);

        string actName = (String)getSession("ACTName");
        if (!actName.Equals("直屬主管"))
        {
            setSession("IsAddSign", "AFTER"); //beforeSign 加簽
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

        if (isNew())
        {
            if (OriginatorGUID.ValueText.Equals(Qa1GUID.ValueText) 
                || OriginatorGUID.ValueText.Equals(Qa2GUID.ValueText))
            {
                strErrMsg += "QA人員不可與申請人員相同!\n";
            }

            if (IncludeKmDoc.ValueText.Equals(""))
            {
                strErrMsg += "請選擇[" + LblIncludeKmDoc.Text + "]!";
            }

        }
        else if (actName.Equals("驗證"))
        {
            if (QaDocFilePath.ValueText.Equals(""))
            {
                strErrMsg += "請輸入["+ LblQaDocFilePath.Text + "]!\n";
            }
            if (QaDescription.ValueText.Equals(""))
            {
                strErrMsg += "請輸入[" + LblQaDescription.Text + "]!\n";
            }
        }
        else if (actName.Equals("驗收"))
        {
        }
        else if (actName.Equals("上線"))
        {
            string value = VssDescription.ValueText;
            if (value.IndexOf("Date:") < 1)
                strErrMsg += "請輸入 VSS Date:\n";
            if (value.IndexOf("User:") < 1)
                strErrMsg += "請輸入 VSS User:\n";
            if (value.IndexOf("Time:") < 1)
                strErrMsg += "請輸入 VSS Time:\n";
            if (value.IndexOf("Version") < 1)
                strErrMsg += "請輸入 VSS Version:\n";
            if (value.IndexOf("Comment:") < 1)
                strErrMsg += "請輸入 VSS Comment:\n";

            if (ActualReleaseDateTime.ValueText.Equals(""))
                strErrMsg += "請輸入[" + LblActualReleaseDateTime.Text + "]!\n";
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
        //si.ownerID = (string)Session["UserID"];
        si.ownerID = OriginatorGUID.ValueText; //表單關系人
        //si.ownerName = (string)Session["UserName"];
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
        string qaUserId = "";
        if (!Qa1GUID.ValueText.Equals(""))
        {
            qaUserId += Qa1GUID.ValueText + ";";
        }
        if (!Qa2GUID.ValueText.Equals(""))
        {
            qaUserId += Qa2GUID.ValueText + ";";
        }

        xml += "<SPIT002>";
        xml += "<qa DataType=\"java.lang.String\">" + qaUserId + "</qa>";
        xml += "</SPIT002>";
        param["SPIT002"] = xml;
        return "SPIT002";
    }

    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
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
    /// 按下送簽按鈕後呼叫此方法。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
        base.afterSign(engine, currentObject, result);
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
            }
        }
        else
        {
            base.rejectProcedure();
        }
    }

    /// <summary>
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        base.afterApprove(engine, currentObject, result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    protected void SystemName_SelectChanged(string value)
    {
        CategoryName.ReadOnly = true;
        CategoryName.ValueText = "";
        if (SystemName.ValueText.Equals("0"))
        {
            CategoryName.ReadOnly = false;
            SdDocFilePath.ValueText = "\\\\smpfile01\\ERPCoding\\Standard\\SD文件\\一般\\";
            UtDocFilePath.ValueText = "\\\\smpfile01\\ERPCoding\\Standard\\客製程式單元測試表\\一般\\";
        }
        else if (SystemName.ValueText.Equals("8"))
        {
            SdDocFilePath.ValueText = "\\\\smpfile01\\plm\\MIS\\Coding\\SD\\";
            UtDocFilePath.ValueText = "\\\\smpfile01\\plm\\MIS\\Coding\\UT\\";
        }
        else if (SystemName.ValueText.Equals("6"))
        {
            SdDocFilePath.ValueText = "\\\\smpfile01\\ERPCODING\\GIB\\SD\\";
            UtDocFilePath.ValueText = "\\\\smpfile01\\ERPCODING\\GIB\\UT\\";
        }
        else if (SystemName.ValueText.Equals("2"))
        {
            SdDocFilePath.ValueText = "\\\\smpfile01\\ECP\\MIS\\SD\\";
            UtDocFilePath.ValueText = "\\\\smpfile01\\ECP\\MIS\\UT\\";
        }
        else if (SystemName.ValueText.Equals("12"))
        {
            SdDocFilePath.ValueText = "\\\\smpfile01\\ekm\\SD\\";
            UtDocFilePath.ValueText = "\\\\smpfile01\\ekm\\UT\\";
        }
        else if (SystemName.ValueText.Equals("9"))
        {
            SdDocFilePath.ValueText = "\\\\smpfile01\\ISP\\Customization\\System Design\\";
            UtDocFilePath.ValueText = "\\\\smpfile01\\ISP\\Customization\\Unit Test\\";
        }
        else if (SystemName.ValueText.Equals("11"))
        {
            SdDocFilePath.ValueText = "\\\\smpfile01\\GPM\\SD\\";
            UtDocFilePath.ValueText = "\\\\smpfile01\\GPM\\UT\\";
        }
        else
        {
            SdDocFilePath.ValueText = "";
            UtDocFilePath.ValueText = "";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GlassButtonViewInfoDemand_BeforeClicks(object sender, EventArgs e)
    {
        setSession((string)Session["UserID"], "InfoDemandGUID", InfoDemandGUID.GuidValueText);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    protected void InfoDemandGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        if (InfoDemandGUID.GuidValueText.Equals("") || InfoDemandGUID.GuidValueText.Equals("NA"))
        {
            GlassButtonViewInfoDemand.ReadOnly = true;
        }
        else
        {
            GlassButtonViewInfoDemand.ReadOnly = false;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    protected void DocGUID_BeforeClickButton()
    {
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            DocGUID.whereClause = "(Status != 'Cancelled')";
        }
    }
}