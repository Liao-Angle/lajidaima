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

using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class SmpProgram_Form_SPKM002_Form : SmpKmFormPage
{

    /// <summary>
    /// 初始化參數。
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPKM002";
        AgentSchema = "WebServerProject.form.SPKM002.SmpDocChangeFormAgent";
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

        string actName = Convert.ToString(getSession("ACTName"));
        sb = new System.Text.StringBuilder();
        sb.Append("<script language=javascript>");
        if (actName.Equals("申請人確認"))
        {
            sb.Append(" function signCheck(){");
            sb.Append("     var external = document.getElementById('S_DSCTabDoc_External').value;");
            sb.Append("     var isIncludeExternal = document.getElementById('S_DSCTabDoc_IsIncludeExternal').value;");
            sb.Append("     if(external == \"Y\" && isIncludeExternal != \"Y\") {");
            sb.Append("         if(confirm(\"此文件類別為包含外來文件，但上傳附件未包含外來文件，請確認要簽核送出?\")){");
            sb.Append("             return true;");
            sb.Append("         } else {");
            sb.Append("             return false;");
            sb.Append("         }");
            sb.Append("     } else {");
            sb.Append("         return true;");
            sb.Append("     }");
            sb.Append(" }");

            

            

            //SignButton.BeforeClick = "signCheck";
            
        }
        sb.Append(" function clickDocTypeReader(){");
        sb.Append("     var url = 'SPKM005/ViewDocTypeReader.aspx?DocTypeGUID=" + objects.getData("DocTypeGUID") + "';");
        sb.Append("     parent.window.openWindowGeneral('檢視文件類別預設讀者', url,'','','',true,true);");
        sb.Append(" }");
        sb.Append("</script>");

        if (!cm.IsStartupScriptRegistered(ctype, "signCheckScript"))
        {
            cm.RegisterStartupScript(ctype, "signCheckScript", sb.ToString());
        }

        GbDcoTypeReader.BeforeClick = "clickDocTypeReader";
        
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
        if (isAddNew)
        {
            OriginatorGUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
            CheckBy1GUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
            CheckBy2GUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
        }

        //審核人一
        CheckBy1GUID.clientEngineType = engineType;
        CheckBy1GUID.connectDBString = connectString;

        //審核人二
        CheckBy2GUID.clientEngineType = engineType;
        CheckBy2GUID.connectDBString = connectString;

        //文件編號
        ChangeDocGUID.clientEngineType = engineType;
        ChangeDocGUID.connectDBString = connectString;

        DocGUID.clientEngineType = engineType;
        DocGUID.connectDBString = connectString;
        DocGUID.ReadOnly = true;
        DocGUID.HiddenButton = true;

        string revGUID = objects.getData("RevGUID");
        //版本
        if (isAddNew)
        {
            ids = new string[,]{
                {"",""}
            };
        }
        else
        {
            string docGUID = objects.getData("DocGUID");
            if (!Convert.ToString(docGUID).Equals(""))
            {
                sql = "select GUID, RevNumber, Released from SmpRev where DocGUID='" + docGUID + "'";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables.Count > 0)
                {
                    count = ds.Tables[0].Rows.Count;
                    ids = new string[1 + count, 2];
                    ids[0, 0] = "";
                    ids[0, 1] = "";
                    for (int i = 0; i < count; i++)
                    {
                        string value = ds.Tables[0].Rows[i][0].ToString();
                        string name = ds.Tables[0].Rows[i][1].ToString();
                        ids[1 + i, 0] = value;
                        ids[1 + i, 1] = name;

                        if (value.Equals(revGUID))
                        {
                            string released = ds.Tables[0].Rows[i][2].ToString();
                            Released.ValueText = released;
                        }
                    }
                    RevGUID.setListItem(ids);
                }
                RevGUID.ReadOnly = true;
            }
        }
        RevGUID.setListItem(ids);
        RevGUID.ReadOnly = true;

        //是否已生效
        Released.Display = false;

        //狀態
        ids = new string[,]{
                {"",""},
                {"Create","新增中"},
                {"Closed","已結案"},
                {"Review","變更中"},
                {"Cancel","作廢中"},
                {"Cancelled","已作廢"},
                {"Rject","退件"}
            };
        Status.setListItem(ids);
        Status.ReadOnly = true;

        //主分類
        MajorTypeGUID.clientEngineType = engineType;
        MajorTypeGUID.connectDBString = connectString;

        //子分類
        SubTypeGUID.clientEngineType = engineType;
        SubTypeGUID.connectDBString = connectString;

        //文件類別
        DocTypeGUID.clientEngineType = engineType;
        DocTypeGUID.connectDBString = connectString;

        //外來文件
        ids = new string[,]{
                {"N","否"},
                {"Y","是"},
            };
        External.setListItem(ids);
        External.ReadOnly = true;

        //廠區
        //Site.ValueText = "SMP";
        ids = new string[,]{               
                {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm002_form_aspx.language.ini", "message", "smp", "新普科技")},
                {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm002_form_aspx.language.ini", "message", "tp", "中普科技")},
				{"STCS",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm002_form_aspx.language.ini", "message", "stcs", "常熟新世")},
                {"SCQ",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm002_form_aspx.language.ini", "message", "scq", "重慶新普")}
            };
        Site.setListItem(ids);
        string[] siteValues = base.getUserInfoById(engine, (string)Session["UserId"]); //傳工號
        Site.ValueText = siteValues[5];
        Site.ReadOnly = true;

        //文件性質
        sql = "select l.GUID,l.Value from SmpListName h, SmpListValue l where h.GUID='DocProperty' and h.GUID=l.ListNameGUID";
        ds = engine.getDataSet(sql, "TEMP");
        ids = new string[1, 2];
        ids[0, 0] = "";
        ids[0, 1] = "";
        if (ds.Tables.Count > 0)
        {
            count = ds.Tables[0].Rows.Count;
            ids = new string[1 + count, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            for (int i = 0; i < count; i++)
            {
                ids[1 + i, 0] = ds.Tables[0].Rows[i][0].ToString(); ;
                ids[1 + i, 1] = ds.Tables[0].Rows[i][1].ToString(); ;
            }
        }
        DocPropertyGUID.setListItem(ids);

        //機密等級
        ids = new string[,]{
                {"",""},
                {"0","一般"},
                {"1","機密"},
                {"2","極機密"}
            };
        ConfidentialLevel.setListItem(ids);

        //作者
        AuthorGUID.clientEngineType = engineType;
        AuthorGUID.connectDBString = connectString;
        AuthorGUID.ReadOnly = true;

        //建立者
        CreatorGUID.clientEngineType = engineType;
        CreatorGUID.connectDBString = connectString;
        CreatorGUID.ReadOnly = true;

        //製定部門
        AuthorOrgUnitGUID.clientEngineType = engineType;
        AuthorOrgUnitGUID.connectDBString = connectString;
        AuthorOrgUnitGUID.ReadOnly = true;

        //生效日
        EffectiveDate.ReadOnly = true;
        ExpiryDate.Display = false;
        LblExpiryDate.Display = false;

        //歸屬群組
        DocBelongGroupGUID.clientEngineType = engineType;
        DocBelongGroupGUID.connectDBString = connectString;

        //歸屬群組清單
        DataObjectSet groupSet = null;
        if (isAddNew)
        {
            groupSet = new DataObjectSet();
            groupSet.isNameLess = true;
            groupSet.setAssemblyName("WebServerProject");
            groupSet.setChildClassString("WebServerProject.form.SPKM002.SmpDocBelongGroup");
            groupSet.setTableName("SmpDocBelongGroup");
            groupSet.loadFileSchema();
            objects.setChild("SmpDocBelongGroup", groupSet);
        }
        else
        {
            groupSet = objects.getChild("SmpDocBelongGroup");
        }
        DataListDocBelongGroup.dataSource = groupSet;
        DataListDocBelongGroup.HiddenField = new string[] { "GUID", "DocGUID", "RevGUID", "BelongGroupType", "BelongGroupGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        DataListDocBelongGroup.reSortCondition("建立時間", DataObjectConstants.ASC);
        DataListDocBelongGroup.updateTable();

        //上傳檔案元件初始化
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string fileAdapter = sp.getParam("FileAdapter");
        FileUploadAtta.FileAdapter = fileAdapter;
        IOFactory factory = new IOFactory();
        AbstractEngine fileEngine = factory.getEngine(engineType, connectString);
        FileUploadAtta.engine = fileEngine;
        FileUploadAtta.tempFolder = Server.MapPath("~/tempFolder");
        FileUploadAtta.maxLength = 10485760*3;
        FileUploadAtta.readFile("");
        FileUploadAtta.Display = true;
        AttachmentUpload.Display = false;

        //附件清單
        DataObjectSet attachmentSet = null;
        if (isAddNew)
        {
            attachmentSet = new DataObjectSet();
            attachmentSet.isNameLess = true;
            attachmentSet.setAssemblyName("WebServerProject");
            attachmentSet.setChildClassString("WebServerProject.form.SPKM002.SmpAttachment");
            attachmentSet.setTableName("SmpAttachment");
            attachmentSet.loadFileSchema();
            objects.setChild("SmpAttachment", attachmentSet);
        }
        else
        {
            attachmentSet = objects.getChild("SmpAttachment");
        }
        DataListAttachment.dataSource = attachmentSet;
        DataListAttachment.HiddenField = new string[] { "GUID", "FormGUID", "SheetNo", "DocGUID", "RevGUID", "FileItemGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY", "Processed" };
        
        //DataListAttachment.FormTitle = "附件檔案";
        //DataListAttachment.IsMaintain = false;
        //DataListAttachment.IsGeneralUse = false;
        //DataListAttachment.IsPanelWindow = true;
        DataListAttachment.WidthMode = 1;
        DataListAttachment.NoAdd = true;
        //DataListAttachment.InputForm = "DownloadFile.aspx";
        DataListAttachment.reSortCondition("上傳時間", DataObjectConstants.ASC);
        DataListAttachment.updateTable();

        AttaFileName.ReadOnly = true;
        ids = new string[,]{
                {"",""},
                {"N","否"},
                {"Y","是"},
            };
        AttaExternal.setListItem(ids);
        IsIncludeExternal.Display = false;

        //文件範本
        string kmSampleURL = sp.getParam("eKMSampleURL"); //系統參數
        SampleHyperLink.NavigateUrl = kmSampleURL;

        //參考文件
        ids = new string[,]{
                {"",""},
                {"KM","KM"},
            };
        Source.setListItem(ids);
        if (Source.ValueText.Equals(""))
        {
            Source.ValueText = "KM";
        }

        ReferenceGUID.clientEngineType = engineType;
        ReferenceGUID.connectDBString = connectString;

        //對象
        ids = new string[,]{
                {"",""},
                {"1","人員"},
                {"2","群組"}
                //{"9","部門"},
                //{"21","群組"}
            };
        ReaderBelongGroupType.setListItem(ids);
        if (ReaderBelongGroupType.ValueText.Equals(""))
        {
            ReaderBelongGroupType.ValueText = "2";
        }

        //參考文件清單
        DataObjectSet referenceSet = null;
        if (isAddNew)
        {
            referenceSet = new DataObjectSet();
            referenceSet.isNameLess = true;
            referenceSet.setAssemblyName("WebServerProject");
            referenceSet.setChildClassString("WebServerProject.form.SPKM002.SmpReference");
            referenceSet.setTableName("SmpReference");
            referenceSet.loadFileSchema();
            objects.setChild("SmpReference", referenceSet);
        }
        else
        {
            referenceSet = objects.getChild("SmpReference");
        }
        DataListReference.dataSource = referenceSet;
        DataListReference.HiddenField = new string[] { "GUID", "DocGUID", "RevGUID", "Reference", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        DataListReference.reSortCondition("建立時間", DataObjectConstants.ASC);
        DataListReference.updateTable();

        //讀者名稱
        ReaderBelongGroupGUID.clientEngineType = engineType;
        ReaderBelongGroupGUID.connectDBString = connectString;

        //讀者清單
        DataObjectSet readerSet = null;
        if (isAddNew)
        {
            readerSet = new DataObjectSet();
            readerSet.isNameLess = true;
            readerSet.setAssemblyName("WebServerProject");
            readerSet.setChildClassString("WebServerProject.form.SPKM002.SmpReader");
            readerSet.setTableName("SmpReader");
            readerSet.loadFileSchema();
            objects.setChild("SmpReader", readerSet);
        }
        else
        {
            readerSet = objects.getChild("SmpReader");
        }
        DataListReader.dataSource = readerSet;
        DataListReader.HiddenField = new string[] { "GUID", "DocGUID", "RevGUID", "BelongGroupType", "BelongGroupGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS", "EffectiveDate", "ExpiryDate" };
        DataListReader.reSortCondition("建立時間", DataObjectConstants.ASC);
        DataListReader.updateTable();

        //事件記錄
        DataObjectSet historySet = null;
        if (isAddNew)
        {
            historySet = new DataObjectSet();
            historySet.isNameLess = true;
            historySet.setAssemblyName("WebServerProject");
            historySet.setChildClassString("WebServerProject.form.SPKM002.SmpHistory");
            historySet.setTableName("SmpHistory");
            historySet.loadFileSchema();
            objects.setChild("SmpHistory", historySet);
        }
        else
        {
            historySet = objects.getChild("SmpHistory");
        }
        DataListHistory.dataSource = historySet;
        DataListHistory.HiddenField = new string[] { "GUID", "DocGUID", "RevGUID", "FormGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        DataListHistory.reSortCondition("建立時間", DataObjectConstants.DESC);
        DataListHistory.updateTable();
        DataListHistory.ReadOnly = true;

        //新增
        if (isAddNew)
        {
            DSCTabDoc.Display = false;
            TabIndexCard.Enabled = false;
            TabBelongGroup.Enabled = false;
            TabAttachment.Enabled = false;
            TabReference.Enabled = false;
            TabReader.Enabled = false;
            TabHistory.Enabled = false;
        }
        else
        {
            DSCTabDoc.Display = true;
            TabIndexCard.Enabled = true;
            TabBelongGroup.Enabled = true;
            TabAttachment.Enabled = true;
            TabReference.Enabled = true;
            TabReader.Enabled = true;
            TabHistory.Enabled = true;
        }

        if (isAddNew)
        {
            if (ChangeDocGUID.ValueText.Equals(""))
            {
                string docGUID = Request.QueryString["DocGUID"];
                if (docGUID != null && !docGUID.Equals(""))
                {
                    ChangeDocGUID.GuidValueText = docGUID;
                    ChangeDocGUID.doGUIDValidate();
                }
            }
        }

        if (Released.ValueText.Equals("Y"))
        {
            //DataListAttachment.InputForm = "";
            //GlassButtonViewDoc.ReadOnly = false;
        }

        //LblEffectiveDate.Display = false;
        //EffectiveDate.Display = false;
        LBLStatus.Display = false;
        Status.Display = false;

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
            TabAttachment.Enabled = false;
            //附件檔案加入文件檔案清單
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            DataObjectSet setFile = attachFile.dataSource;
            if (setFile.getAvailableDataObjectCount() > 0)
            {
                DataObjectSet detailSet = DataListAttachment.dataSource;
                for (int j = 0; j < setFile.getAvailableDataObjectCount(); j++)
                {
                    DataObject fileDataObject = setFile.getAvailableDataObject(j); ;
                    DataObject obj = detailSet.create();
                    
                    obj.setData("GUID", IDProcessor.getID(""));
                    obj.setData("SampleGUID", "TEMP");
                    obj.setData("FileItemGUID", fileDataObject.getData("GUID"));
                    fileDataObject.getData("FILENAME");
                    obj.setData("FILEEXT", fileDataObject.getData("FILEEXT"));
                    obj.setData("AttachmentTypeId", "O");
                    obj.setData("DESCRIPTION", fileDataObject.getData("DESCRIPTION"));
                    obj.setData("UPLOADUSER", si.fillerName);
                    obj.setData("UPLOADTIME", fileDataObject.getData("UPLOADTIME"));
                    obj.setData("SheetNo", (string)getSession(PageUniqueID, "SheetNo"));
                    obj.setData("IS_LOCK", "N");
                    obj.setData("IS_DISPLAY", "Y");
                    obj.setData("DATA_STATUS", "Y");
                    detailSet.add(obj);
                }
                DataListAttachment.dataSource = detailSet;
                DataListAttachment.updateTable();
            }
        }

        string released = Released.ValueText;
        bool isCanDownload = true;
        if (released.Equals("Y"))
        {
            isCanDownload = false;
        }
        string UIStatus = (string)getSession("UIStatus");
        if (isCanDownload)
        {
            if (UIStatus.Equals(ProcessModify))
            {
                if (actName.Equals("部門主管"))
                {
                    isCanDownload = false;
                }
            }
            if (UIStatus.Equals(FormReadOnly))
            {
                isCanDownload = false;
            }
        }
        if (isCanDownload)
        {
            DataObjectSet dos = DataListAttachment.dataSource;
            for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
            {
                DataObject obj = dos.getDataObject(i);
                string fileName = obj.getData("FILENAME");
                string fileExt = obj.getData("FILEEXT");
                string sheetNo = obj.getData("SheetNo");
                string fileItemGUID = obj.getData("FileItemGUID");
                string href = "DownloadFile.aspx";
                href += "?FILENAME=" + System.Web.HttpUtility.UrlPathEncode(fileName);
                href += "&FILEEXT=" + fileExt;
                href += "&SheetNo=" + sheetNo;
                href += "&FileItemGUID=" + fileItemGUID;
                string fileNameUrl = "{[a href=\"" + href + "\"]}" + fileName + "{[/a]}";
                
                obj.setData("FILENAME", fileNameUrl);
                DataListAttachment.setColumnStyle("FILENAME", 130, DSCWebControl.GridColumnStyle.LEFT);
            }
            DataListAttachment.updateTable();
        }

        //主旨
        Subject.ValueText = objects.getData("Subject"); 
        //顯示單號
        base.showData(engine, objects);
        //申請人
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID");
        OriginatorGUID.doGUIDValidate();
        //審核人一
        CheckBy1GUID.GuidValueText = objects.getData("CheckBy1GUID");
        CheckBy1GUID.doGUIDValidate();
        //審核人二
        CheckBy2GUID.GuidValueText = objects.getData("CheckBy2GUID");
        CheckBy2GUID.doGUIDValidate();
        //文件編號
        ChangeDocGUID.GuidValueText = objects.getData("ChangeDocGUID");
        ChangeDocGUID.doGUIDValidate();
        //變更原因
        ChangeReason.ValueText = objects.getData("ChangeReason");
        //文件編號
        DocGUID.GuidValueText = objects.getData("DocGUID");
        DocGUID.doGUIDValidate();
        //版本
        RevGUID.ValueText = objects.getData("RevGUID");
        //是否已生效
        //Released.ValueText = objects.getData("Released");
        //狀態
        Status.ValueText = objects.getData("Status");
        //文件名稱
        Name.ValueText = objects.getData("Name");
        //主分類
        MajorTypeGUID.GuidValueText = objects.getData("MajorTypeGUID");
        MajorTypeGUID.doGUIDValidate();
        //子分類
        SubTypeGUID.GuidValueText = objects.getData("SubTypeGUID");
        SubTypeGUID.doGUIDValidate();
        //是否為外部文件
        External.ValueText = objects.getData("External");
        //文件類別
        DocTypeGUID.GuidValueText = objects.getData("DocTypeGUID");
        DocTypeGUID.doGUIDValidate();
        //廠區
        Site.ValueText = objects.getData("Site");
        //文件性質
        DocPropertyGUID.ValueText = objects.getData("DocPropertyGUID");
        //機密等級
        ConfidentialLevel.ValueText = objects.getData("ConfidentialLevel");
        //作者
        AuthorGUID.GuidValueText = objects.getData("AuthorGUID");
        AuthorGUID.doGUIDValidate();
        //建立者
        CreatorGUID.GuidValueText = objects.getData("D_INSERTUSER");
        CreatorGUID.doGUIDValidate();
        //製定部門
        AuthorOrgUnitGUID.GuidValueText = objects.getData("AuthorOrgUnitGUID");
        AuthorOrgUnitGUID.doGUIDValidate();
        //文件摘要
        Abstract.ValueText = objects.getData("Abstract");
        //關鍵字
        KeyWords.ValueText = objects.getData("KeyWords");
        //生效日
        EffectiveDate.ValueText = objects.getData("EffectiveDate");
        //失效日
        ExpiryDate.ValueText = objects.getData("ExpiryDate");

        if (!actName.Equals("") && !actName.Equals("填表人"))
        {
            //表單發起後不允許修改
            Subject.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            ChangeDocGUID.ReadOnly = true;
            ChangeReason.ReadOnly = true;
            CheckBy1GUID.ReadOnly = true;
            CheckBy2GUID.ReadOnly = true;
        }

        if (actName.Equals("申請人確認"))
        {
            //DataListAttachment.InputForm = "";
            if (ExpiryDate.ValueText.Equals(""))
            {
                //目前時間加兩年
                string expiryDate = ExpiryDate.ValueText;
                string now = DateTimeUtility.getSystemTime2(null);
                expiryDate = (Convert.ToInt16(now.Substring(0, 4)) + 2) + now.Substring(4);
                ExpiryDate.ValueText = expiryDate;
            }

            //External.ReadOnly = false;
            string sql = "select External from SmpDocType where GUID='" + DocTypeGUID.GuidValueText + "'";
            string external = (string)engine.executeScalar(sql);
            if (external.Equals("N"))
                External.ReadOnly = true;
            else
                External.ReadOnly = false;
        }
        else
        {
            DocGUID.ReadOnly = true;
            Name.ReadOnly = true;
            MajorTypeGUID.ReadOnly = true;
            SubTypeGUID.ReadOnly = true;
            DocTypeGUID.ReadOnly = true;
            Site.ReadOnly = true;
            DocPropertyGUID.ReadOnly = true;
            ConfidentialLevel.ReadOnly = true;
            Abstract.ReadOnly = true;
            KeyWords.ReadOnly = true;
            ExpiryDate.ReadOnly = true;

            //歸屬群組不能修改
            DocBelongGroupGUID.ReadOnly = true;
            DataListDocBelongGroup.ReadOnly = true;
            DataListDocBelongGroup.IsHideSelectAllButton = true;
            DataListDocBelongGroup.IsShowCheckBox = false;

            //附件清單不能修改
            ButtonUpload.ReadOnly = true;
            DataListAttachment.ReadOnly = true;
            DataListAttachment.IsHideSelectAllButton = true;
            DataListAttachment.IsShowCheckBox = false;

            //參考文件不能修改
            Source.ReadOnly = true;
            ReferenceGUID.ReadOnly = true;
            DataListReference.ReadOnly = true;
            DataListReference.IsHideSelectAllButton = true;
            DataListReference.IsShowCheckBox = false;

            ReaderBelongGroupType.ReadOnly = true;
            ReaderBelongGroupGUID.ReadOnly = true;
            DataListReader.ReadOnly = true;
            DataListReader.IsHideSelectAllButton = true;
            DataListReader.IsShowCheckBox = false;
        }
    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        AbstractEngine engine1 = null;
        NLAgent agent1 = null;
        DataObjectSet set = null;
        DataObject obj = null;

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
                objects.setData("CheckBy1GUID", CheckBy1GUID.GuidValueText);
                objects.setData("CheckBy2GUID", CheckBy2GUID.GuidValueText);
                objects.setData("ChangeDocGUID", ChangeDocGUID.GuidValueText);
                objects.setData("ChangeReason", ChangeReason.ValueText);
            }

            
            if (!actName.Equals("直屬主管"))
            {
                setSession("IsAddSign", "AFTER"); //beforeSign 加簽
            }

            engine1 = factory.getEngine(EngineConstants.SQL, connectString);

            //儲存文件屬性
            if (!isAddNew)
            {
                string docGUID = objects.getData("DocGUID");
                agent1 = new NLAgent();
                agent1.loadSchema("WebServerProject.form.SPKM002.SmpDocumentAgent");
                agent1.engine = engine1;
                agent1.query("GUID='" + docGUID + "'");
                set = agent1.defaultData;
                obj = set.getAvailableDataObject(0);
                //obj.setData("GUID", docGUID);
                obj.setData("DocNumber", DocGUID.ValueText);
                obj.setData("AuthorGUID", AuthorGUID.GuidValueText);
                obj.setData("AuthorOrgUnitGUID", AuthorOrgUnitGUID.GuidValueText);
                obj.setData("Site", Site.ValueText);
                agent1.defaultData = set;
                if (!agent1.update())
                {
                    throw new Exception(engine1.errorString);
                }

                //儲存索引卡
                string indexCardGUID = objects.getData("IndexCardGUID");
                agent1 = new NLAgent();
                agent1.loadSchema("WebServerProject.form.SPKM002.SmpIndexCardAgent");
                agent1.engine = engine1;
                agent1.query("GUID='" + indexCardGUID + "'");
                set = agent1.defaultData;
                obj = set.getAvailableDataObject(0);
                obj.setData("MajorTypeGUID", MajorTypeGUID.GuidValueText);
                obj.setData("SubTypeGUID", SubTypeGUID.GuidValueText);
                obj.setData("DocTypeGUID", DocTypeGUID.GuidValueText);
                obj.setData("DocPropertyGUID", DocPropertyGUID.ValueText);
                obj.setData("ConfidentialLevel", ConfidentialLevel.ValueText);
                obj.setData("Name", Name.ValueText);
                obj.setData("Abstract", Abstract.ValueText);
                obj.setData("KeyWords", KeyWords.ValueText);
                obj.setData("ExpiryDate", ExpiryDate.ValueText);
                obj.setData("External", External.ValueText);
                if (actName.Equals("文管中心"))
                {
                    obj.setData("Status", "Closed");
                    obj.setData("EffectiveDate", DateTimeUtility.getSystemTime2(null).Substring(0, 10));

                    //儲存歷史記錄
                    string histogyGUID = IDProcessor.getID("");
                    string userGUID = (string)Session["UserGUID"];
                    string formGUID = objects.getData("GUID");
                    string revGUID = objects.getData("RevGUID");
                    string action = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_form_spkm.language.ini", "message", "DocChangeComplete", "文件變更");
                    string description = objects.getData("Subject");
                    string originatorGUID = objects.getData("OriginatorGUID");
                    string originatorName = OriginatorGUID.ReadOnlyValueText;
                    string sheetNo = (string)getSession(PageUniqueID, "SheetNo");
                    string revNumber = RevGUID.ReadOnlyText;
                    string now = DateTimeUtility.getSystemTime2(null);

                    DataObjectSet historySet = DataListHistory.dataSource;
                    DataObject history = historySet.create();
                    history.setData("GUID", histogyGUID);
                    history.setData("DocGUID", docGUID);
                    history.setData("Action", action);
                    history.setData("userName", originatorName);
                    history.setData("InsertTime", now);
                    history.setData("Description", description);
                    history.setData("RevGUID", revGUID);
                    history.setData("FormGUID", formGUID);
                    history.setData("SheetNo", sheetNo);
                    history.setData("RevNumber", revNumber);
                    history.setData("IS_LOCK", "Y");
                    history.setData("IS_DISPLAY", "N");
                    history.setData("DATA_STATUS", "Y");
                    history.setData("D_INSERTUSER", originatorGUID);
                    history.setData("D_INSERTTIME", now);
                    history.setData("D_MODIFYUSER", "");
                    history.setData("D_MODIFYTIME", "");
                    historySet.add(history);
                }

                agent1.defaultData = set;
                if (!agent1.update())
                {
                    throw new Exception(engine1.errorString);
                }
            }

            //儲存歸屬群群
            for (int i = 0; i < DataListDocBelongGroup.dataSource.getAvailableDataObjectCount(); i++)
            {
                DataListDocBelongGroup.dataSource.getAvailableDataObject(i).setData("DocGUID", objects.getData("DocGUID"));
                DataListDocBelongGroup.dataSource.getAvailableDataObject(i).setData("RevGUID", objects.getData("RevGUID"));
            }

            //儲存附件
            for (int i = 0; i < DataListAttachment.dataSource.getAvailableDataObjectCount(); i++)
            {
                DataObject dataObject = DataListAttachment.dataSource.getDataObject(i);
                DataListAttachment.dataSource.getAvailableDataObject(i).setData("DocGUID", objects.getData("DocGUID"));
                DataListAttachment.dataSource.getAvailableDataObject(i).setData("RevGUID", objects.getData("RevGUID"));
                string fileName = dataObject.getData("FILENAME");
                int idxs = fileName.IndexOf("]}");
                int idxe = fileName.LastIndexOf("{[");
                if (idxs > 0 && idxe > 0)
                {
                    fileName = fileName.Substring(idxs + 2, idxe - (idxs + 2));
                }
                DataListAttachment.dataSource.getAvailableDataObject(i).setData("FILENAME", fileName); //將filename換回, 避免url過長, 造成saveDB報錯! 
            }
            FileUploadAtta.setJobID(objects.getData(getObjectGUIDField()));
            FileUploadAtta.confirmSave("EKM", objects.getData("SheetNo"));
            FileUploadAtta.saveFile();
            FileUploadAtta.engine.close();

            //儲存參考文件
            for (int i = 0; i < DataListReference.dataSource.getAvailableDataObjectCount(); i++)
            {
                DataListReference.dataSource.getAvailableDataObject(i).setData("DocGUID", objects.getData("DocGUID"));
                DataListReference.dataSource.getAvailableDataObject(i).setData("RevGUID", objects.getData("RevGUID"));
            }

            //儲存讀者
            for (int i = 0; i < DataListReader.dataSource.getAvailableDataObjectCount(); i++)
            {
                DataListReader.dataSource.getAvailableDataObject(i).setData("DocGUID", objects.getData("DocGUID"));
                DataListReader.dataSource.getAvailableDataObject(i).setData("RevGUID", objects.getData("RevGUID"));
            }
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
        bool isAddNew = base.isNew();
        bool result = true;
        string errMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));
        if (isAddNew)
        {
            if (Subject.ValueText.Equals("") && !ChangeDocGUID.GuidValueText.Equals(""))
            {
                string sql = "select i.Name from SmpRev r, SmpIndexCard i where r.DocGUID='" + ChangeDocGUID.GuidValueText+ "' and r.IndexCardGUID=i.GUID and r.Released='Y' and r.LatestFlag='Y'";
                string name = (string)engine.executeScalar(sql);
                Subject.ValueText = name + "(" + ChangeDocGUID.ValueText + ") 文件變更";
            }
        }

        if (actName.Equals("申請人確認"))
        {
            if (Name.ValueText.Equals(""))
            {
                errMsg += "[" + LblName.Text + "]欄位必需有值!\n";
            }
            if (MajorTypeGUID.ValueText.Equals(""))
            {
                errMsg += "[" + LblMajorTypeGUID.Text + "]欄位必需有值!\n";
            }
            if (SubTypeGUID.ValueText.Equals(""))
            {
                errMsg += "[" + LblSubTypeGUID.Text + "]欄位必需有值!\n";
            }
            if (DocTypeGUID.ValueText.Equals(""))
            {
                errMsg += "[" + LblDocTypeGUID.Text + "]欄位必需有值!\n";
            }
            if (DocPropertyGUID.ValueText.Equals(""))
            {
                errMsg += "[" + LblDocPropertyGUID.Text + "]欄位必需有值!\n";
            }
            if (ConfidentialLevel.ValueText.Equals(""))
            {
                errMsg += "[" + LblConfidentialLevel.Text + "]欄位必需有值!\n";
            }
            if (Abstract.ValueText.Equals(""))
            {
                errMsg += "[" + LblAbstract.Text + "]欄位必需有值!\n";
            }
            if (KeyWords.ValueText.Equals(""))
            {
                errMsg += "[" + LblKeyWords.Text + "]欄位必需有值!\n";
            }
            if (ExpiryDate.ValueText.Equals(""))
            {
                errMsg += "[" + LblExpiryDate.Text + "]欄位必需有值!\n";
            }
            else
            {
                //不可大於目前時間加兩年                
                string now = DateTimeUtility.getSystemTime2(null);
                string expiryDate = (Convert.ToInt16(now.Substring(0, 4)) + 2) + now.Substring(4, 6);
                if (ExpiryDate.ValueText.CompareTo(expiryDate) > 0)
                {
                    errMsg += "[" + LblExpiryDate.Text + "]不可大於 " + expiryDate + "\n";
                }

            }

            if (DataListDocBelongGroup.dataSource.getAvailableDataObjectCount() == 0)
            {
                errMsg += "[歸屬群組] 筆數不得為0!\n";
            }

            DataObjectSet setAtta = DataListAttachment.dataSource;
            int attaCount = setAtta.getAvailableDataObjectCount();
            if (attaCount == 0)
            {
                errMsg += "[附件檔案筆數不得為0!\n";
            } else {
                string external = External.ValueText;
                if (external.Equals("Y"))
                {
                    bool isGotAttaExternal = false;
                    for (int i = 0; i < attaCount; i++)
                    {
                        DataObject data = setAtta.getAvailableDataObject(i);
                        if (data.getData("External").Equals("Y"))
                        {
                            isGotAttaExternal = true;
                            break;
                        }
                    }
                    if (isGotAttaExternal == false)
                    {
                        errMsg += "此文件設定為包含外來文件, 附件清單必須包含外來文件!\n";
                    }
                }
            }

            if (!AttachmentUpload.ValueText.Equals("Y"))
            {
                errMsg += "沒有上傳新檔案, 請確認!";
            }
            else
            {

            }

            
        }
        if (!errMsg.Equals(""))
        {
            pushErrorMessage(errMsg);
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
        si.ownerID = OriginatorGUID.ValueText;
        si.ownerName = OriginatorGUID.ReadOnlyValueText;
        depData = getDeptInfo(engine, OriginatorGUID.GuidValueText);
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
        string confidentialLevel = ConfidentialLevel.ValueText;
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

        string[][] results = getDocTypeAdmUserGUID(engine, DocTypeGUID.GuidValueText);
        if (results != null && results.Length > 0)
        {
            for (int i = 0; i < results.Length; i++)
                typeAdm += results[i][1] + ";";
        }

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

        xml += "<SPKM002>";
        xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
        xml += "<checkBy DataType=\"java.lang.String\">" + checkBy + "</checkBy>";
        xml += "<typeAdm DataType=\"java.lang.String\">" + typeAdm + "</typeAdm>";
        xml += "<manager DataType=\"java.lang.String\">" + manager + "</manager>";
        xml += "<confidentialLevel DataType=\"java.lang.String\">" + confidentialLevel + "</confidentialLevel>";
        xml += "<notifier DataType=\"java.lang.String\">" + notifier + "</notifier>";
        xml += "</SPKM002>";

        //表單代號
        param["SPKM002"] = xml;
        return "SPKM002";
    }

    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        //產生變更版本記錄
        string sql = "";
        string docGUID = ChangeDocGUID.GuidValueText;
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
        //NLAgent agent1 = null;
        //DataObjectSet dos = null;
        //DataObject obj = null;

        try
        {
            //新增索引卡
            sql = "insert into SmpIndexCard (GUID, Status, MajorTypeGUID, SubTypeGUID, DocTypeGUID, DocPropertyGUID, ConfidentialLevel, DocGUID, Name, Abstract, KeyWords, EffectiveDate, ExpiryDate, External, "
                + "IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) "
                + "select '" + indexCardGUID + "' GUID, 'Review' Status, i.MajorTypeGUID, i.SubTypeGUID, i.DocTypeGUID, i.DocPropertyGUID, i.ConfidentialLevel, i.DocGUID, i.Name, i.Abstract, i.KeyWords, '' EffectiveDate, '' ExpiryDate, i.External, "
                + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + userGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME "
                + "from SmpIndexCard i, SmpRev r where r.DocGUID='" + docGUID + "' and r.Released='Y' and r.LatestFlag='Y' and i.GUID=r.IndexCardGUID";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }

            //sql = "select * from SmpIndexCard i, SmpRev r where i.GUID=r.IndexCardGUID and r.Released='Y' and r.LatestFlag='Y'";
            //DataSet ds = engine.getDataSet(sql, "TEMP");

            //agent1 = new NLAgent();
            //agent1.loadSchema("WebServerProject.form.SPKM002.SmpIndexCardAgent");
            //agent1.engine = engine1;
            //agent1.query("(1=2)");

            //dos = agent1.defaultData;
            //obj = dos.create();
            //obj.setData("GUID", indexCardGUID);
            //obj.setData("Status", "Review");
            //obj.setData("MajorTypeGUID", ds.Tables[0].Rows[0]["MajorTypeGUID"].ToString());
            //obj.setData("SubTypeGUID", ds.Tables[0].Rows[0]["SubTypeGUID"].ToString());
            //obj.setData("DocTypeGUID", ds.Tables[0].Rows[0]["DocTypeGUID"].ToString());
            //obj.setData("DocPropertyGUID", ds.Tables[0].Rows[0]["DocPropertyGUID"].ToString());
            //obj.setData("ConfidentialLevel", ds.Tables[0].Rows[0]["ConfidentialLevel"].ToString());
            //obj.setData("IS_LOCK", "N");
            //obj.setData("IS_DISPLAY", "Y");
            //obj.setData("DATA_STATUS", "Y");
            //if (!dos.add(obj))
            //{
            //    throw new Exception(dos.errorString);
            //}

            //if (!agent1.update())
            //{
            //    throw new Exception(engine.errorString);
            //}

            //新增版本
            sql = "select RevNumber from SmpRev where DocGUID='" + docGUID + "' and Released='Y' and LatestFlag='Y'";
            revNumber = Convert.ToString(Convert.ToInt16((string)engine.executeScalar(sql)) + 1);

            sql = "insert into SmpRev (GUID, RevNumber, DocGUID, FormGUID, IndexCardGUID, Released, LatestFlag, ReleaseDate, "
                + "IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME, SheetNo) "
                + "select '" + revGUID + "' GUID, '" + revNumber + "' RevNumber, '" + docGUID + "' DocGUID, '" + formGUID + "' FormGUID, '" + indexCardGUID + "' IndexCardGUID, 'N' Released, 'N' LatestFlag, '' ReleaseDate, "
                + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + userGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME, '" + sheetNo + "' SheetNo from SmpRev where DocGUID='" + docGUID + "' and LatestFlag='Y'";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }
            
            //agent1 = new NLAgent();
            //agent1.loadSchema("WebServerProject.form.SPKM002.SmpRevAgent");
            //agent1.engine = engine1;
            //agent1.query("(1=2)");

            //dos = agent1.defaultData;
            //obj = dos.create();
            //obj.setData("GUID", revGUID);
            //obj.setData("RevNumber", revNumber);
            //obj.setData("DocGUID", docGUID);
            //obj.setData("FormGUID", formGUID);
            //obj.setData("IndexCardGUID", indexCardGUID);
            //obj.setData("Released", "N");
            //obj.setData("LatestFlag", "N");
            //obj.setData("ReleaseDate", "");
            //obj.setData("IS_LOCK", "N");
            //obj.setData("IS_DISPLAY", "Y");
            //obj.setData("DATA_STATUS", "Y");
            //if (!dos.add(obj))
            //{
            //    throw new Exception(dos.errorString);
            //}

            //if (!agent1.update())
            //{
            //    throw new Exception(engine.errorString);
            //}

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

            //新增附件
            sql = "insert into SmpAttachment (GUID, DocGUID, RevGUID, FileItemGUID, AttachmentType, "
                + "IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) "
                + "select newid(), a.DocGUID, '" + revGUID + "' RevGUID, a.FileItemGUID, a.AttachmentType, "
                + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + userGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME "
                + "from SmpAttachment a, SmpRev r where r.DocGUID='" + docGUID + "' and a.RevGUID=r.GUID and r.Released='Y' and r.LatestFlag='Y' and a.AttachmentType='Original'";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }

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
        string xml = "";
        string actName = Convert.ToString(getSession("ACTName"));
        if (actName.Equals("文管中心"))
        {
            DataObjectSet attachmentSet = DataListAttachment.dataSource;
            string result = printOutPdf(engine, attachmentSet);
            DataListAttachment.dataSource = attachmentSet;
            DataListAttachment.updateTable();
            if (!result.Equals(""))
            {
                throw new Exception(result);
            }

            //string docTypeGUID = DocTypeGUID.GuidValueText;
            //string revGUID = RevGUID.ValueText;
            //string sql = 
            //        //歸屬群組
            //        "select BelongGroupType, BelongGroupGUID from SmpDocBelongGroup where RevGUID='" + revGUID + "' "
            //        //讀者
            //        + "union select BelongGroupType, BelongGroupGUID from SmpReader where RevGUID='" + revGUID + "' "
            //        //管理人員
            //        + "union select distinct '1' BelongGroupType, c.OID BelongGroupGUID FROM ("
            //        + "    select b.OID, b.id, b.userName  from dbo.SmpMajorTypeAdm a, dbo.Users b, dbo.SmpDocType d "
            //        + "    where d.GUID = '" + docTypeGUID + "' and b.OID = a.MajorTypeAdmUserGUID and a.MajorTypeGUID = d.MajorTypeGUID "
            //        + "    union "
            //        + "    select b.OID, b.id, b.userName from dbo.SmpSubTypeAdm a, dbo.Users b, dbo.SmpDocType d "
            //        + "    where d.GUID = '" + docTypeGUID + "' and b.OID = a.SubTypeAdmUserGUID AND a.SubTypeGUID = d.SubTypeGUID) c ";
            //DataSet ds = engine.getDataSet(sql, "TEMP");
            //int rows = ds.Tables[0].Rows.Count;
            //if (rows > 0)
            //{
            //    xml += "<list>";
            //}
            //for (int i = 0; i < rows; i++)
            //{
            //    string participantType = "";
            //    string performType = "NOTICE";
            //    string belongGroupType = ds.Tables[0].Rows[i][0].ToString();
            //    string belongGroupGUID = ds.Tables[0].Rows[i][1].ToString();
            //    string stateValueName = "通知";

            //    if (belongGroupType.Equals("1"))
            //    {
            //        participantType = "HUMAN";
            //        sql = "select '人員: ' + userName from Users where OID='" + belongGroupGUID + "'";
            //        stateValueName = (string)engine.executeScalar(sql);
            //    }
            //    else if (belongGroupType.Equals("21"))
            //    {
            //        participantType = "GROUP";
            //        sql = "select KindName + ': ' + Name from SmpBelongGroupV where OID='" + belongGroupGUID + "'";
            //        stateValueName = (string)engine.executeScalar(sql);
            //    }
            //    else if (belongGroupType.Equals("9"))
            //    {
            //        participantType = "ORGANIZATION_UNIT";
            //        sql = "select KindName + ': ' + Name from SmpBelongGroupV where OID='" + belongGroupGUID + "'";
            //        stateValueName = (string)engine.executeScalar(sql);
            //    }
                

            //    xml += "    <com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
            //    xml += "        <performers>";
            //    xml += "            <com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
            //    xml += "                <OID>" + belongGroupGUID + "</OID>";
            //    xml += "                <participantType><value>" + participantType + "</value></participantType>";
            //    xml += "            </com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
            //    xml += "        </performers>";
            //    xml += "        <multiUserMode><value>FIREST_GET_FIRST_WIN</value></multiUserMode>";
            //    xml += "        <name>" + stateValueName + "</name>";
            //    xml += "        <performType><value>" + performType + "</value></performType>";
            //    xml += "    </com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
            //}
            //if (rows > 0)
            //{
            //    xml += "</list>";
            //    return xml;
            //}
        }
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
            AbstractEngine engine = null;
            try
            {
                DataObject currentObject = (DataObject)getSession("currentObject");
                string indexCardGUID = currentObject.getData("IndexCardGUID");
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);
                string sql = "update SmpIndexCard set Status='Reject' where GUID='" + indexCardGUID + "'";
                engine.executeSQL(sql);

                base.terminateThisProcess();
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
            //string now = DateTimeUtility.getSystemTime2(null);
            //currentObject.setData("Status", "Closed");
            //currentObject.setData("EffectiveDate", now.Substring(0, 10));
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
            string docGUID = currentObject.getData("DocGUID");
            string revGUID = currentObject.getData("RevGUID");
            string indexCardGUID = currentObject.getData("IndexCardGUID");
            string subject = currentObject.getData("Subject");
            string expiryDate = currentObject.getData("ExpiryDate");
            string originatorGUID = currentObject.getData("OriginatorGUID");

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
            sql = "update SmpIndexCard set Status='Closed', EffectiveDate='" + now.Substring(0,10) + "', ExpiryDate='" + expiryDate + "' , D_MODIFYUSER='" + userGUID + "', D_MODIFYTIME='" + now + "' where DocGUID='" + docGUID + "' and GUID='" + indexCardGUID + "'";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }
            //新增文件歷史記錄
            //string action = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_form_spkm.language.ini", "message", "DocChangeComplete", "文件變更");;
            //string description = subject;
            //sql = "insert into SmpHistory (GUID, DocGUID, Action, Description, RevGUID, FormGUID, IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) "
            //    + "select '" + histogyGUID + "' GUID, '" + docGUID + "' DocGUID, '" + action + "' Action, '" + description + "' Description, '" + revGUID + "' RevGUID, '" + formGUID + "' FormGUID, "
            //    + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + originatorGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME from SmpDocChangeForm where GUID='" + formGUID + "'";
            //if (!engine.executeSQL(sql))
            //{
            //    throw new Exception(engine.errorString);
            //}
        }
        base.afterApprove(engine, currentObject, result);
    }

    /// <summary>
    /// 變更申請人重新確認權限
    /// </summary>
    /// <param name="values"></param>
    protected void OriginatorGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        string strErrMsg = "";
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            string docGUID = ChangeDocGUID.GuidValueText;
            if (!docGUID.Equals(""))
            {
                strErrMsg += docReviewCheck(docGUID);
                if (!strErrMsg.Equals(""))
                {
                    ChangeDocGUID.ValueText = "";
                    ChangeDocGUID.GuidValueText = "";
                    ChangeDocGUID.ReadOnlyValueText = "";
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
    /// 選擇變更文件才能檢視文件
    /// </summary>
    /// <param name="values"></param>
    protected void ChangeDocGUID_SingleOpenWindow(string[,] values)
    {
        string strErrMsg = "";
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            string docGUID = ChangeDocGUID.GuidValueText;
            if (!docGUID.Equals(""))
            {
                strErrMsg += docReviewCheck(docGUID);
                if (!strErrMsg.Equals(""))
                {
                    ChangeDocGUID.ValueText = "";
                    ChangeDocGUID.GuidValueText = "";
                    ChangeDocGUID.ReadOnlyValueText = "";
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
    /// 文件進版檢查
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
            string connectString = (string)Session["connectString"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, connectString);

            sql = "select i.Status from SmpRev r, SmpIndexCard i, SmpDocument d "
                + "where d.GUID='" + docGUID + "' and i.Status='Cancelled' "
                + "and r.Released='Y' and r.LatestFlag='Y' "
                + "and r.IndexCardGUID = i.GUID and r.DocGUID = d.GUID";
            ds = engine.getDataSet(sql, "TEMP");
            if (!engine.errorString.Equals(""))
            {
                strErrMsg += engine.errorString;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                isCancelled = true;
                strErrMsg += "此文件已失效!\n";
            }

            if (!isCancelled)
            {
                //文件一次只能有一張變更單
                sql = "select a.SMWYAAA002, u.userName from SmpRev r, SMWYAAA a, Users u "
                    + "where DocGUID='" + docGUID + "' "
                    + "and r.FormGUID=a.SMWYAAA019 and r.Released = 'N' and r.LatestFlag='N' and a.SMWYAAA020 ='I' "
                    + "and a.D_INSERTUSER = u.OID";
                ds = engine.getDataSet(sql, "TEMP");
                if (!engine.errorString.Equals(""))
                {
                    strErrMsg += engine.errorString;
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sheetNo = ds.Tables[0].Rows[0][0].ToString();
                    string creator = ds.Tables[0].Rows[0][1].ToString();
                    strErrMsg += "此文件異動中, 單號: " + sheetNo + " 建立者: " + creator + "!\n";
                }
                if (!engine.errorString.Equals(""))
                {
                    strErrMsg += engine.errorString;
                }

                //進版權限檢查
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
                    if (!engine.errorString.Equals(""))
                    {
                        strErrMsg += engine.errorString;
                    }
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        authorGUID = ds.Tables[0].Rows[0][0].ToString();
                        docTypeGUID = ds.Tables[0].Rows[0][1].ToString();
                        revGUID = ds.Tables[0].Rows[0][2].ToString();
                    }
                    //是否為作者
                    if (authorGUID.Equals(originatorGUID))
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
                    strErrMsg += "此文件的管理者/歸屬群組/作者，才擁有文件變更權限!\n";
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

    /// <summary>
    /// 填單過濾已失效文件
    /// </summary>
    protected void ChangeDocGUID_BeforeClickButton()
    {
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            ChangeDocGUID.whereClause = "(Status != 'Cancelled')";
        }
    }

    /// <summary>
    /// 上傳檔案後更新附件清單
    /// </summary>
    /// <param name="currentObject"></param>
    /// <param name="isNew"></param>
    protected void Atta_AddOutline(DSCWebControl.FileItem currentObject, bool isNew)
    {
        try
        {
            setSession("currentFile", currentObject);
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            DataObjectSet detailSet = DataListAttachment.dataSource;
            DataObject obj = detailSet.create();
            obj.setData("GUID", IDProcessor.getID(""));
            obj.setData("FormGUID", "FormTEMP");
            obj.setData("SheetNo", (string)getSession(PageUniqueID, "SheetNo"));
            obj.setData("DocGUID", DocGUID.GuidValueText);
            obj.setData("RevGUID", RevGUID.ValueText);
            obj.setData("FileItemGUID", currentObject.GUID);
            obj.setData("FILENAME", currentObject.FILENAME);
            obj.setData("FILEEXT", currentObject.FILEEXT);
            obj.setData("AttachmentType", "Original");
            obj.setData("DESCRIPTION", currentObject.DESCRIPTION);
            obj.setData("External", "N"); //是否為外來文件
            obj.setData("UPLOADUSER", si.fillerName);
            obj.setData("UPLOADTIME", currentObject.UPLOADTIME);
            obj.setData("IS_LOCK", "N");
            obj.setData("IS_DISPLAY", "Y");
            obj.setData("DATA_STATUS", "Y");
            detailSet.add(obj);
            DataListAttachment.dataSource = detailSet;
            DataListAttachment.updateTable();

            AttachmentUpload.ValueText = "Y";
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
    }

    /// <summary>
    /// 刪除附件清單則同步移除檔案清單
    /// </summary>
    /// <returns></returns>
    protected bool Atta_BeforeDeleteData()
    {
        try
        {
            DataObject[] atta = DataListAttachment.getSelectedItem();
            DataObjectSet setAtta = DataListAttachment.dataSource;
            DataObjectSet setFile = FileUploadAtta.uploadedList;

            for (int i = 0; i < atta.Length; i++)
            {
                DataObject attaDataObject = atta[i];
                if (attaDataObject != null)
                {
                    string attaFileGUID = attaDataObject.getData("FileItemGUID");
                    for (int j = 0; j < setFile.getAvailableDataObjectCount(); j++)
                    {
                        DataObject fileDataObject = setFile.getAvailableDataObject(j);
                        string fileGUID = fileDataObject.getData("GUID");
                        if (fileGUID.Equals(attaFileGUID))
                        {
                            setFile.delete(fileDataObject);
                            break;
                        }
                    }
                }
            }

            FileUploadAtta.uploadedList = setFile;

            SetIsIncludeExternal();

            return true;
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
    }

    /// <summary>
    /// 點選上傳檔案開啟上傳檔案元件對話窗
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Upload_Click(object sender, EventArgs e)
    {
        Session["isKM"] = "Y"; //為了判斷是KM表單，FileUpload裡會判斷檔案上傳格式
        FileUploadAtta.openFileUploadDialog();
    }

    /// <summary>
    /// 點選附件清單開啟視窗前記錄此筆資料
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isAddNew"></param>
    /// <returns></returns>
    protected bool Atta_BeforeOpenWindows(DataObject objects, bool isAddNew)
    {
        return setDownload(objects);
    }

    /// <summary>
    /// 檔案下載檢查, 設定下載檔案session
    /// </summary>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected bool setDownload(DataObject objects)
    {
        string released = Released.ValueText;

        bool isCanDownload = true;
        if (released.Equals("Y"))
        {
            MessageBox("表單已結案不允許下載檔案，請點選[檢視文件]檢視文件內容!");
            //GlassButtonViewDoc.ReadOnly = false;
            //DataListAttachment.InputForm = "";
            isCanDownload = false;
        }

        string UIStatus = (string)getSession("UIStatus");
        if (isCanDownload && UIStatus.Equals(FormReadOnly))
        {
            string[,] opinion = (string[,])getSession("WorkflowOpinion");
            string actName = opinion[opinion.GetLength(0) - 1, 0];
            if (actName.Equals("部門主管"))
            {
                MessageBox("部門主管尚未簽核, 不允許下載檔案");
                isCanDownload = false;
            }
        }

        if (isCanDownload)
        {
            setSession((string)Session["UserID"], "DownFile", objects);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 表單結案顯示檢示文件按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GlassButtonViewDoc_Load(object sender, EventArgs e)
    {
        if (Released.ValueText.Equals("Y"))
        {
            GlassButtonViewDoc.ReadOnly = false;
        }
    }

    /// <summary>
    /// 歸屬群組資料顯示
    /// </summary>
    /// <param name="objects"></param>
    protected void DataListDocBelongGroup_ShowRowData(DataObject objects)
    {
        DocBelongGroupGUID.valueIndex = 2;
        DocBelongGroupGUID.GuidValueText = objects.getData("BelongGroupGUID");
        DocBelongGroupGUID.doGUIDValidate();
    }

    /// <summary>
    /// 歸屬群組資料新增
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    /// <returns></returns>
    protected bool DataListDocBelongGroup_SaveRowData(DataObject objects, bool isNew)
    {
        if (DocBelongGroupGUID.ValueText.Equals(""))
        {
            MessageBox("請先選擇歸屬群組!");
            return false;
        }

        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("DocGUID", DocGUID.GuidValueText);
            objects.setData("RevGUID", RevGUID.ValueText);
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }

        objects.setData("BelongGroupGUID", DocBelongGroupGUID.GuidValueText);
        objects.setData("id", DocBelongGroupGUID.ValueText);
        objects.setData("Name", DocBelongGroupGUID.ReadOnlyValueText);

        DocBelongGroupGUID.valueIndex = 3;
        DocBelongGroupGUID.doGUIDValidate();
        objects.setData("KindName", DocBelongGroupGUID.ReadOnlyValueText);
        DocBelongGroupGUID.valueIndex = 4;
        DocBelongGroupGUID.doGUIDValidate();
        objects.setData("BelongGroupType", DocBelongGroupGUID.ReadOnlyValueText);
        DocBelongGroupGUID.valueIndex = 2;
        DocBelongGroupGUID.GuidValueText = objects.getData("BelongGroupGUID");
        DocBelongGroupGUID.doGUIDValidate();

        string[] keys = objects.keyField;
        objects.keyField = new string[] { "RevGUID", "BelongGroupGUID" };
        DataObjectSet groupSet = DataListDocBelongGroup.dataSource;
        if (!groupSet.checkData(objects))
        {
            MessageBox("資料重覆!");
            objects.keyField = keys;
            return false;
        }
        return true;
    }

    /// <summary>
    /// 參考文件資料顯示
    /// </summary>
    /// <param name="objects"></param>
    protected void DataListReference_ShowRowData(DataObject objects)
    {
        Source.ValueText = objects.getData("Source");
        ReferenceGUID.GuidValueText = objects.getData("Reference");
        if (!ReferenceGUID.GuidValueText.Equals(""))
        {
            ReferenceGUID.doGUIDValidate();
        }
    }

    /// <summary>
    /// 參考文件資料儲存
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    /// <returns></returns>
    protected bool DataListReference_SaveRowData(DataObject objects, bool isNew)
    {
        string strErrMsg = "";
        try
        {
            if (Source.ValueText.Equals(""))
            {
                strErrMsg += LblSource.Text + ": 必需選擇!\n";
            }

            if (ReferenceGUID.GuidValueText.Equals(""))
            {
                strErrMsg += LblReferenceGUID.Text + ": 必需選擇!\n";
            }

            if (!strErrMsg.Equals(""))
            {
                MessageBox(strErrMsg);
                return false;
            }
        }
        catch
        {
            throw new Exception(strErrMsg);
        }

        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("DocGUID", DocGUID.GuidValueText);
            objects.setData("RevGUID", RevGUID.ValueText);
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("Source", Source.ValueText);
        objects.setData("Reference", ReferenceGUID.GuidValueText);
        objects.setData("DocNumber", ReferenceGUID.ValueText);
        objects.setData("Description", ReferenceGUID.ReadOnlyValueText);

        string[] keys = objects.keyField;
        objects.keyField = new string[] { "RevGUID", "Reference" };
        DataObjectSet referenceSet = DataListReference.dataSource;
        if (!referenceSet.checkData(objects))
        {
            MessageBox("資料重覆!");
            objects.keyField = keys;
            return false;
        }

        return true;
    }

    /// <summary>
    /// 過濾已失效文件
    /// </summary>
    protected void ReferenceGUID_BeforeClickButton()
    {
        ReferenceGUID.whereClause = "(Status != 'Cancelled')";
    }

    /// <summary>
    /// 對象變更載入不同的開窗
    /// </summary>
    /// <param name="value"></param>
    protected void ReaderBelongGroupType_SelectChanged(string value)
    {
        if (ReaderBelongGroupType.ValueText.Equals("1"))
        {
            ReaderBelongGroupGUID.tableName = "Users";
            ReaderBelongGroupGUID.serialNum = "003";
            ReaderBelongGroupGUID.idIndex = 2;
            ReaderBelongGroupGUID.valueIndex = 3;
        }
        else
        {
            ReaderBelongGroupGUID.tableName = "SPKM001";
            ReaderBelongGroupGUID.serialNum = "001";
            ReaderBelongGroupGUID.idIndex = 1;
            ReaderBelongGroupGUID.valueIndex = 2;
        }

        ReaderBelongGroupGUID.ValueText = "";
        ReaderBelongGroupGUID.GuidValueText = "";
        ReaderBelongGroupGUID.ReadOnlyValueText = "";
    }

    /// <summary>
    /// 讀者資料顯示
    /// </summary>
    /// <param name="objects"></param>
    protected void DataListReader_ShowRowData(DataObject objects)
    {
        if (objects.getData("BelongGroupType").Equals("1"))
        {
            ReaderBelongGroupType.ValueText = "1";
            ReaderBelongGroupGUID.tableName = "Users";
            ReaderBelongGroupGUID.serialNum = "003";
            ReaderBelongGroupGUID.idIndex = 2;
            ReaderBelongGroupGUID.valueIndex = 3;
        }
        else
        {
            ReaderBelongGroupType.ValueText = "2";
            ReaderBelongGroupGUID.tableName = "SPKM001";
            ReaderBelongGroupGUID.serialNum = "001";
            ReaderBelongGroupGUID.idIndex = 1;
            ReaderBelongGroupGUID.valueIndex = 2;
        }
        ReaderBelongGroupGUID.GuidValueText = objects.getData("BelongGroupGUID");
        ReaderBelongGroupGUID.doGUIDValidate();
    }

    /// <summary>
    /// 讀者資料新增
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    /// <returns></returns>
    protected bool DataListReader_SaveRowData(DataObject objects, bool isNew)
    {
        string strErrMsg = "";

        if (ReaderBelongGroupType.ValueText.Equals(""))
        {
            strErrMsg += "請先選擇對象!\n";
        }
        if (ReaderBelongGroupGUID.ValueText.Equals(""))
        {
            strErrMsg += "請先選擇讀書名稱!\n";
        }

        string expiryDate = objects.getData("ExpiryDate");
        if (!expiryDate.Equals(""))
        {
            strErrMsg += "調閱流程新增資料, 不允許異動!";
        }

        //文件類別預設讀者不需要新增
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, connectString);
        string docTypeGUID = DocTypeGUID.GuidValueText;
        string belongGroupGUID = ReaderBelongGroupGUID.GuidValueText;
        string userGUID = (string)Session["UserGUID"];
        bool isReader = base.isDefaultReader(engine, docTypeGUID, belongGroupGUID);
        if (isReader)
        {
            strErrMsg += "文件類別預設讀者不需要再加入讀單清單!";
        }
        engine.close();

        if (!strErrMsg.Equals(""))
        {
            MessageBox(strErrMsg);
            return false;
        }

        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("DocGUID", DocGUID.GuidValueText);
            objects.setData("RevGUID", RevGUID.ValueText);
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }

        objects.setData("BelongGroupGUID", ReaderBelongGroupGUID.GuidValueText);
        objects.setData("id", ReaderBelongGroupGUID.ValueText);
        objects.setData("Name", ReaderBelongGroupGUID.ReadOnlyValueText);

        if (ReaderBelongGroupType.ValueText.Equals("1"))
        {
            objects.setData("KindName", "人員");
            objects.setData("BelongGroupType", "1");

            ReaderBelongGroupGUID.valueIndex = 3;
            ReaderBelongGroupGUID.GuidValueText = objects.getData("BelongGroupGUID");
            ReaderBelongGroupGUID.doGUIDValidate();
        }
        else
        {
            ReaderBelongGroupGUID.valueIndex = 3;
            ReaderBelongGroupGUID.doGUIDValidate();
            objects.setData("KindName", ReaderBelongGroupGUID.ReadOnlyValueText);

            ReaderBelongGroupGUID.valueIndex = 4;
            ReaderBelongGroupGUID.doGUIDValidate();
            objects.setData("BelongGroupType", ReaderBelongGroupGUID.ReadOnlyValueText);

            ReaderBelongGroupGUID.valueIndex = 2;
            ReaderBelongGroupGUID.GuidValueText = objects.getData("BelongGroupGUID");
            ReaderBelongGroupGUID.doGUIDValidate();
        }

        string[] keys = objects.keyField;
        objects.keyField = new string[] { "RevGUID", "BelongGroupGUID" };
        DataObjectSet readerSet = DataListReader.dataSource;
        if (!readerSet.checkData(objects))
        {
            MessageBox("資料重覆!");
            objects.keyField = keys;
            return false;
        }
        
        return true;
    }

    /// <summary>
    /// 讀者資料執行刪除前事件
    /// </summary>
    protected bool DataListReader_BeforeDeleteData()
    {
        DataObject[] rows = DataListReader.getSelectedItem();
        for (int i = 0; i < rows.Length; i++)
        {
            DataObject row = rows[i];
            string expiryDate = row.getData("ExpiryDate");
            if (!expiryDate.Equals(""))
            {
                MessageBox("調閱流程新增資料, 不允許異動!");
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 檢視文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GlassButtonViewDoc_Click(object sender, EventArgs e)
    {
        setSession((string)Session["UserID"], "ViewDocGUID", ChangeDocGUID.GuidValueText);
    }

    /// <summary>
    /// 主分類開窗只能選擇生效
    /// </summary>
    /// <param name="values"></param>
    protected void MajorTypeGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        MajorTypeGUID.whereClause = "(Enable='Y')";
    }

    /// <summary>
    /// 記錄子分類變更前資料
    /// </summary>
    protected void SubTypeGUID_BeforeClickButton()
    {
        setSession(PageUniqueID, "BeforeClickSubTypeGUID", SubTypeGUID.GuidValueText); 
    }

    /// <summary>
    /// 子分類變更重新取得歸屬群組
    /// </summary>
    /// <param name="values"></param>
    protected void SubTypeGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        string majorTypeGUID = MajorTypeGUID.GuidValueText;
        SubTypeGUID.whereClause = "(MajorTypeGUID='" + majorTypeGUID + "' and Enable='Y')";

        string beforeClickSubTypeGUID = (string)getSession(PageUniqueID, "BeforeClickSubTypeGUID");
        if (!beforeClickSubTypeGUID.Equals(SubTypeGUID.GuidValueText))
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(EngineConstants.SQL, connectString);

            //清空歸屬群組                              
            DataObjectSet setDocBelongGroup = DataListDocBelongGroup.dataSource;
            int count = setDocBelongGroup.getDataObjectCount();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    DataObject docBelongGroup = setDocBelongGroup.getDataObject(i);
                    setDocBelongGroup.delete(docBelongGroup);
                }
                DataListDocBelongGroup.updateTable();
            }

            //取得預設的歸屬群組, [0]: Kind, [1]: KindName, [2]: groupOID, [3]: id, [4]: groupName               
            string[][] results = getSubTypeBelongGroupGUID(engine, SubTypeGUID.GuidValueText);
            if (results != null && results.Length > 0)
            {
                DataObjectSet belongGroupSet = DataListDocBelongGroup.dataSource;
                for (int i = 0; i < results.Length; i++)
                {
                    DataObject obj = null;
                    obj = belongGroupSet.create();
                    obj.setData("GUID", IDProcessor.getID(""));
                    obj.setData("DocGUID", "TEMP");
                    obj.setData("RevGUID", "TEMP");
                    obj.setData("BelongGroupType", results[i][0]);
                    obj.setData("KindName", results[i][1]);
                    obj.setData("BelongGroupGUID", results[i][2]);
                    obj.setData("id", results[i][3]);
                    obj.setData("Name", results[i][4]);
                    obj.setData("IS_LOCK", "N");
                    obj.setData("IS_DISPLAY", "Y");
                    obj.setData("DATA_STATUS", "Y");
                    belongGroupSet.add(obj);
                }
                DataListDocBelongGroup.dataSource = belongGroupSet;
                DataListDocBelongGroup.updateTable();
            }

            engine.close();
        }
    }

    /// <summary>
    /// 文件類別開窗只能選擇生效
    /// </summary>
    /// <param name="values"></param>
    protected void DocTypeGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        string majorTypeGUID = MajorTypeGUID.GuidValueText;
        string subTypeGUID = SubTypeGUID.GuidValueText;
        DocTypeGUID.whereClause = "(MajorTypeGUID='" + majorTypeGUID + "' and SubTypeGUID='" + subTypeGUID + "' and Enable='Y')";

        //重新取得此文件類別是否屬於外部文件類別
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, connectString);
        string sql = "select External from SmpDocType where GUID='" + DocTypeGUID.GuidValueText + "'";
        string external = (string)engine.executeScalar(sql);
        External.ValueText = external;
        if (external.Equals("N"))
            External.ReadOnly = true;
        else
            External.ReadOnly = false;
        engine.close();
    }
    
    /// <summary>
    /// 儲存附件列資料, External文件檢查
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    /// <returns></returns>
    protected bool Atta_SaveRowData(DataObject objects, bool isNew)
    {
        string external = External.ValueText;
        string attaExternal = AttaExternal.ValueText;
        if (attaExternal.Equals("Y"))
        {
            if (!external.Equals("Y"))
            {
                MessageBox("文件類別必需設定為包含外來文件，才可上傳外來文件!");
                return false;
            }
        }
        objects.setData("External", attaExternal);

        SetIsIncludeExternal();

        return true;
    }

    /// <summary>
    /// 顯示附件列資料, 檔名處理
    /// </summary>
    /// <param name="objects"></param>
    protected void Atta_ShowRowData(DataObject objects)
    {
        string fileName = objects.getData("FILENAME");
        int idxs = fileName.IndexOf("]}");
        int idxe = fileName.LastIndexOf("{[");
        if (idxs > 0 && idxe > 0)
        {
            fileName = fileName.Substring(idxs+2, idxe-(idxs+2));
        }
        AttaFileName.ValueText = fileName;
        AttaExternal.ValueText = objects.getData("External");

        setDownload(objects);

    }

    /// <summary>
    /// 設定附件是否有包含外來文件
    /// </summary>
    protected void SetIsIncludeExternal()
    {
        string isIncludeExternal = IsIncludeExternal.ValueText;
        if (isIncludeExternal.Equals("Y"))
        {
            IsIncludeExternal.ValueText = "";
            DataObjectSet dos = DataListAttachment.dataSource;
            for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
            {
                DataObject data = dos.getAvailableDataObject(i);
                if (data.getData("External").Equals("Y"))
                {
                    IsIncludeExternal.ValueText = "Y";
                    break;
                }
            }
        }
    }
}