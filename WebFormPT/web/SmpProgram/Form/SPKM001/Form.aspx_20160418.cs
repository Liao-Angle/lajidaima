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


public partial class SmpProgram_Form_SPKM001_Form : SmpKmFormPage
{

    /// <summary>
    /// 初始化參數。
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPKM001";
        AgentSchema = "WebServerProject.form.SPKM001.SmpDocReleaseFormAgent";
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
        //MessageBox("initUI");
        bool isAddNew = base.isNew();
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string sql = null;
        string[,] ids = null;
        DataSet ds = null;
        int count = 0;

        //檢視文件script
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
            cm.RegisterStartupScript(ctype, "clickViewDocScript", sb.ToString()); //將後端的內容render成script,動態註冊一個JavaScript
        }
        GlassButtonViewDoc.AfterClick = "clickViewDoc";
        GlassButtonViewDoc.ReadOnly = true;

        //查看預設讀者
        sb.Append("<script language=javascript>");
        sb.Append(" function clickDocTypeReader(){");
        sb.Append("     parent.window.openWindowGeneral('檢視文件類別預設讀者','" + Page.ResolveUrl("../SPKM005/ViewDocTypeReader.aspx") + "','','','',true,true);");
        sb.Append(" }");
        sb.Append("</script>");

        if (!cm.IsStartupScriptRegistered(ctype, "signCheckScript"))
        {
            cm.RegisterStartupScript(ctype, "signCheckScript", sb.ToString());
        }

        GbDcoTypeReader.AfterClick = "clickDocTypeReader";
        GbDcoTypeReader.ReadOnly = false;

        //審核人一
        CheckBy1GUID.clientEngineType = engineType;
        CheckBy1GUID.connectDBString = connectString;

        //審核人二
        CheckBy2GUID.clientEngineType = engineType;
        CheckBy2GUID.connectDBString = connectString;

        //文件
        DocGUID.clientEngineType = engineType;
        DocGUID.connectDBString = connectString;
        DocGUID.ReadOnly = true;

        //版本
        ids = new string[,]{
                {"",""},
                {"1","1"}
        };
        RevGUID.setListItem(ids);
        RevGUID.ValueText = "1";
        //RevGUID.ReadOnly = true;
        Released.Display = false;

        //狀態
        //ids = new string[,]{   
        //        {"",""},
        //        {"Create","新增中"},
        //        {"Closed","已結案"}
        //        //{"Review","變更中"},
        //        //{"Cancel","作廢中"},
        //        //{"Cancelled","已作廢"}
        //    };
        //Status.setListItem(ids);
        //if (isAddNew)
        //{
        //    Status.ValueText = "Create";           
        //}
        //Status.ReadOnly = true;
        //LBLStatus.Visible = false;
        //Status.Visible = false;

        //公司別
        //Site.ValueText = "SMP";        
        ids = new string[,]{               
                {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm001_form_aspx.language.ini", "message", "smp", "新普科技")},
                {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm001_form_aspx.language.ini", "message", "tp", "中普科技")},
                {"STCS",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm001_form_aspx.language.ini", "message", "tp", "常熟新世")},
                {"SCQ",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spkm001_form_aspx.language.ini", "message", "tp", "重慶新普")}
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
                ids[1 + i, 0] = ds.Tables[0].Rows[i][0].ToString();
                ids[1 + i, 1] = ds.Tables[0].Rows[i][1].ToString();
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
        ConfidentialLevel.ValueText = "0";

        //作者(同Originator)
        AuthorGUID.clientEngineType = engineType;
        AuthorGUID.connectDBString = connectString;
        if (AuthorGUID.ValueText.Equals(""))
        {
            AuthorGUID.ValueText = si.fillerID; //預設帶出登入者
            AuthorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }

        //建立者(填表人)
        CreatorGUID.clientEngineType = engineType;
        CreatorGUID.connectDBString = connectString;


        if (CreatorGUID.ValueText.Equals(""))
        {
            CreatorGUID.ValueText = si.fillerID; //預設帶出登入者
            CreatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }
        CreatorGUID.ReadOnly = true;

        //製定部門
        AuthorOrgUnitGUID.clientEngineType = engineType;
        AuthorOrgUnitGUID.connectDBString = connectString;
        if (AuthorOrgUnitGUID.ValueText.Equals(""))
        {
            AuthorOrgUnitGUID.ValueText = si.fillerOrgID; //預設帶出登入者部門
            AuthorOrgUnitGUID.doValidate(); //帶出人員開窗元件中的部門名稱
        }
        AuthorOrgUnitGUID.ReadOnly = true;

        //主分類
        MajorTypeGUID.clientEngineType = engineType;
        MajorTypeGUID.connectDBString = connectString;
        if (MajorTypeGUID.ValueText.Equals("") && !AuthorOrgUnitGUID.GuidValueText.Equals(""))
        {

            sql = "select TOP 1 Name from SmpMajorType where DeptGUID ='" + AuthorOrgUnitGUID.GuidValueText + "' ";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                MajorTypeGUID.ValueText = ds.Tables[0].Rows[0][0].ToString();
                MajorTypeGUID.doValidate();
            }
        }

        //子分類
        SubTypeGUID.clientEngineType = engineType;
        SubTypeGUID.connectDBString = connectString;

        //文件類別
        DocTypeGUID.clientEngineType = engineType;
        DocTypeGUID.connectDBString = connectString;

        //是否可包含外部文件
        ids = new string[,]{
                {"N","否"},
                {"Y","是"},
            };
        External.setListItem(ids);
        //External.ReadOnly = true;


        //生效日
        EffectiveDate.ReadOnly = true;
        //EffectiveDate.Visible = false;
        //LblEffectiveDate.Visible = false;

        //失效日
        //if (ExpiryDate.ValueText.Equals(""))
        //{
        //    //目前時間加兩年
        //    string expiryDate = ExpiryDate.ValueText;
        //    string now = DateTimeUtility.getSystemTime2(null);
        //    expiryDate = (Convert.ToInt16(now.Substring(0, 4)) + 2) + now.Substring(4);
        //    ExpiryDate.ValueText = expiryDate;
        //}
        //LblExpiryDate.Visible = false;
        //ExpiryDate.Visible = false;

        //歸屬群組
        DocBelongGroupGUID.clientEngineType = engineType;
        DocBelongGroupGUID.connectDBString = connectString;
        DataObjectSet groupSet = null;
        if (isAddNew)
        {
            groupSet = new DataObjectSet();
            groupSet.isNameLess = true;
            groupSet.setAssemblyName("WebServerProject");
            groupSet.setChildClassString("WebServerProject.form.SPKM001.SmpDocBelongGroup");
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
        string fileAdapter = sp.getParam("FileAdapter"); //系統參數
        FileUploadAtta.FileAdapter = fileAdapter;
        IOFactory factory = new IOFactory();
        AbstractEngine fileEngine = factory.getEngine(engineType, connectString);
        FileUploadAtta.engine = fileEngine;
        FileUploadAtta.tempFolder = Server.MapPath("~/tempFolder");
        FileUploadAtta.maxLength = 10485760 * 3;
        FileUploadAtta.readFile("");
        FileUploadAtta.Display = true;

        //文件範本
        string kmSampleURL = sp.getParam("eKMSampleURL"); //系統參數
        SampleHyperLink.NavigateUrl = kmSampleURL;

        //附件清單        
        DataObjectSet attachmentSet = null;
        if (isAddNew)
        {
            attachmentSet = new DataObjectSet();
            attachmentSet.isNameLess = true;
            attachmentSet.setAssemblyName("WebServerProject");
            attachmentSet.setChildClassString("WebServerProject.form.SPKM001.SmpAttachment");
            attachmentSet.setTableName("SmpAttachment");
            attachmentSet.loadFileSchema();
            objects.setChild("SmpAttachment", attachmentSet);
        }
        else
        {
            attachmentSet = objects.getChild("SmpAttachment");
        }
        DataListAttachment.dataSource = attachmentSet;
        DataListAttachment.HiddenField = new string[] { "GUID", "FormGUID", "SheetNo", "DocGUID", "RevGUID", "FileItemGUID", "Processed", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        // DataListAttachment.FormTitle = "附件檔案";
        DataListAttachment.WidthMode = 1;
        //DataListAttachment.IsMaintain = false;
        //DataListAttachment.IsGeneralUse = false;
        //DataListAttachment.IsPanelWindow = true;
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

        //對象
        ids = new string[,]{
                {"",""},
                {"1","人員"},
                {"2","群組"}
                //{"9","部門"},
                //{"21","群組"}
            };
        ReaderBelongGroupType.setListItem(ids);
        ReaderBelongGroupType.ValueText = "2";

        //參考文件清單
        ids = new string[,]{
                {"",""},
                {"KM","KM"},
            };
        Source.setListItem(ids);
        Source.ValueText = "KM";

        ReferenceGUID.clientEngineType = engineType;
        ReferenceGUID.connectDBString = connectString;
        DataObjectSet referenceSet = null;
        if (isAddNew)
        {
            referenceSet = new DataObjectSet();
            referenceSet.isNameLess = true;
            referenceSet.setAssemblyName("WebServerProject");
            referenceSet.setChildClassString("WebServerProject.form.SPKM001.SmpReference");
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
            readerSet.setChildClassString("WebServerProject.form.SPKM001.SmpReader");
            readerSet.setTableName("SmpReader");
            readerSet.loadFileSchema();
            objects.setChild("SmpReader", readerSet);
        }
        else
        {
            readerSet = objects.getChild("SmpReader");
        }
        DataListReader.dataSource = readerSet;
        DataListReader.HiddenField = new string[] { "GUID", "DocGUID", "RevGUID", "BelongGroupType", "BelongGroupGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS", "FromAccess" };
        DataListReader.reSortCondition("建立時間", DataObjectConstants.ASC);
        DataListReader.updateTable();

        //事件記錄
        DataObjectSet historySet = null;
        if (isAddNew)
        {
            historySet = new DataObjectSet();
            historySet.isNameLess = true;
            historySet.setAssemblyName("WebServerProject");
            historySet.setChildClassString("WebServerProject.form.SPKM001.SmpHistory");
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
        // DataListHistory.ReadOnly = true;
        //歷史記錄不能修改
        DataListHistory.IsShowCheckBox = true;
        DataListHistory.IsHideSelectAllButton = true;


        //版本是否生效        
        string revGUID = objects.getData("RevGUID");
        if (!Convert.ToString(revGUID).Equals(""))
        {
            sql = "select Released from SmpRev where GUID='" + revGUID + "'";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Released.ValueText = ds.Tables[0].Rows[0][0].ToString();
            }
        }

        //改變工具列順序
        base.initUI(engine, objects);

        //改變Tab高度
        if (isAddNew)
            DSCTabDoc.Height = 800;
        else
            DSCTabDoc.Height = 347;
    }

    /// <summary>
    /// 將資料由資料物件填入畫面元件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        //MessageBox("showData");      
        bool isAddNew = base.isNew();
        string actName = Convert.ToString(getSession("ACTName"));
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號
        base.showData(engine, objects);
        //申請人
        //OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID");
        //OriginatorGUID.doGUIDValidate();
        //審核人一
        CheckBy1GUID.GuidValueText = objects.getData("CheckBy1GUID");
        CheckBy1GUID.doGUIDValidate();
        //審核人二
        CheckBy2GUID.GuidValueText = objects.getData("CheckBy2GUID");
        CheckBy2GUID.doGUIDValidate();
        //文件編號
        DocGUID.GuidValueText = objects.getData("DocGUID");
        DocGUID.doGUIDValidate();
        //版本
        RevGUID.ValueText = objects.getData("RevGUID");
        //狀態 
        //Status.ValueText = objects.getData("Status");
        //文件名稱
        Name.ValueText = objects.getData("Name");
        //主分類
        MajorTypeGUID.GuidValueText = objects.getData("MajorTypeGUID");
        MajorTypeGUID.doGUIDValidate();
        //子分類
        SubTypeGUID.GuidValueText = objects.getData("SubTypeGUID");
        SubTypeGUID.doGUIDValidate();
        //文件類別
        DocTypeGUID.GuidValueText = objects.getData("DocTypeGUID");
        DocTypeGUID.doGUIDValidate();
        //是否包含外部文件
        External.ValueText = objects.getData("External");
        //公司別
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

        //MessageBox(CreatorGUID.GuidValueText);

        /*if (objects.getData("D_INSERTUSER").Equals(""))
            CreatorGUID.ValueText = Convert.ToString(getSession("USERID"));
        else
            CreatorGUID.GuidValueText = objects.getData("D_INSERTUSER");
         */
        CreatorGUID.doGUIDValidate();
        //製定部門
        AuthorOrgUnitGUID.GuidValueText = objects.getData("AuthorOrgUnitGUID");
        AuthorOrgUnitGUID.doGUIDValidate();
        //文件摘要
        Abstract.ValueText = objects.getData("Abstract");
        //關鍵字
        KeyWords.ValueText = objects.getData("KeyWords");
        //生效日
        if (Released.ValueText.Equals("Y"))
        {
            EffectiveDate.ValueText = objects.getData("EffectiveDate");
        }
        //失效日
        //ExpiryDate.ValueText = objects.getData("ExpiryDate");

        DataObjectSet groupSet = null;
        DataObjectSet referenceSet = null;
        DataObjectSet readerSet = null;
        DataObjectSet attachmentSet = null;

        groupSet = objects.getChild("SmpDocBelongGroup");
        referenceSet = objects.getChild("SmpReference");
        readerSet = objects.getChild("SmpReader");
        attachmentSet = objects.getChild("SmpAttachment");

        DataListDocBelongGroup.dataSource = groupSet;
        DataListDocBelongGroup.updateTable();

        DataListReference.dataSource = referenceSet;
        DataListReference.updateTable();

        DataListReader.dataSource = readerSet;
        DataListReader.updateTable();

        DataListAttachment.dataSource = attachmentSet;
        DataListAttachment.updateTable();

        if (Request.QueryString["IsCopyForm"] != null) //複製表單 
        {
            //MessageBox("CopyForm");
            Subject.ValueText = "";
            EffectiveDate.ValueText = "";
            Released.ValueText = "N";
            DocGUID.GuidValueText = "";
            DocGUID.doGUIDValidate();
            //附件不複製            
            int count = attachmentSet.getDataObjectCount();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    DataObject attachment = attachmentSet.getDataObject(i);
                    attachmentSet.delete(attachment);
                }
                DataListAttachment.updateTable();
            }

            //history不複製
            DataObjectSet historySet = new DataObjectSet();
            historySet.isNameLess = true;
            historySet.setAssemblyName("WebServerProject");
            historySet.setChildClassString("WebServerProject.form.SPKM001.SmpHistory");
            historySet.setTableName("SmpHistory");
            historySet.loadFileSchema();
            objects.setChild("SmpHistory", historySet);
        }

        //if (isAddNew)
        //{            
        //    TabAttachment.Enabled = true;
        //    //附件檔案加入文件檔案清單

        //    DataObjectSet setFile = attachFile.dataSource;
        //    if (setFile.getAvailableDataObjectCount() > 0)
        //    {
        //        DataObjectSet detailSet = DataListAttachment.dataSource;
        //        for (int j = 0; j < setFile.getAvailableDataObjectCount(); j++)
        //        {                    
        //            DataObject fileDataObject = setFile.getAvailableDataObject(j); ;
        //            DataObject obj = detailSet.create();
        //            obj.setData("GUID", IDProcessor.getID(""));
        //            obj.setData("DocGUID", "TEMP");
        //            obj.setData("RevGUID", "TEMP");
        //            obj.setData("FileItemGUID", fileDataObject.getData("GUID"));
        //            obj.setData("FILENAME", fileDataObject.getData("FILENAME"));
        //            obj.setData("FILEEXT", fileDataObject.getData("FILEEXT"));
        //            obj.setData("AttachmentTypeId", "O");
        //            obj.setData("DESCRIPTION", fileDataObject.getData("DESCRIPTION"));
        //            obj.setData("UPLOADUSER", si.fillerName);
        //            obj.setData("UPLOADTIME", fileDataObject.getData("UPLOADTIME"));
        //            obj.setData("SheetNo", (string)getSession(PageUniqueID, "SheetNo"));
        //            obj.setData("IS_LOCK", "N");
        //            obj.setData("IS_DISPLAY", "Y");
        //            obj.setData("DATA_STATUS", "Y");
        //            detailSet.add(obj);  //欄位於DataObjectSecema->nonUpdateField 並不會回寫
        //        }
        //        DataListAttachment.dataSource = detailSet;
        //        DataListAttachment.updateTable();
        //        MessageBox("att count :" + DataListAttachment.dataSource.getAvailableDataObjectCount() );
        //    }
        //}

        //if (!actName.Equals("") && !actName.Equals("填表人") && !actName.Equals("部門主管確認"))
        //{
        //    //讀者不能修改
        //    ReaderBelongGroupType.ReadOnly = true;
        //    ReaderBelongGroupGUID.ReadOnly = true;
        //    DataListReader.ReadOnly = true;
        //}

        //if ((!actName.Equals("") && !actName.Equals("填表人")) || released.Equals("Y"))
        if (!isAddNew)
        {
            //表單發起後不允許修改
            Subject.ReadOnly = true;
            //OriginatorGUID.ReadOnly = true;            
            CheckBy1GUID.ReadOnly = true;
            CheckBy2GUID.ReadOnly = true;
            DocGUID.ReadOnly = true;
            RevGUID.ReadOnly = true;
            Name.ReadOnly = true;
            MajorTypeGUID.ReadOnly = true;
            SubTypeGUID.ReadOnly = true;
            DocTypeGUID.ReadOnly = true;
            Site.ReadOnly = true;
            DocPropertyGUID.ReadOnly = true;
            ConfidentialLevel.ReadOnly = true;
            AuthorGUID.ReadOnly = true;
            Abstract.ReadOnly = true;
            KeyWords.ReadOnly = true;
            //ExpiryDate.ReadOnly = true;

            //附件清單不能修改
            DataListAttachment.IsShowCheckBox = false;
            DataListAttachment.IsHideSelectAllButton = true;
            DataListAttachment.NoDelete = true;
            DataListAttachment.NoAdd = true;
            DataListAttachment.NoModify = true;
            DataListAttachment.ReadOnly = true;
            ButtonUpload.ReadOnly = true;

            //歸屬群組不能修改
            DataListDocBelongGroup.IsShowCheckBox = false;
            DataListDocBelongGroup.IsHideSelectAllButton = true;
            DocBelongGroupGUID.ReadOnly = true;
            DataListDocBelongGroup.ReadOnly = true;

            //參考文件不能修改
            DataListReference.IsShowCheckBox = false;
            DataListReference.IsHideSelectAllButton = true;
            Source.ReadOnly = true;
            ReferenceGUID.ReadOnly = true;
            DataListReference.ReadOnly = true;

            //讀者不能修改
            DataListReader.IsShowCheckBox = false;
            DataListReader.IsHideSelectAllButton = true;
            ReaderBelongGroupType.ReadOnly = true;
            ReaderBelongGroupGUID.ReadOnly = true;
            DataListReader.ReadOnly = true;
        }

        string released = Released.ValueText;
        bool isCanDownload = true;
        if (released.Equals("Y"))
        {
            isCanDownload = false;
        }

        if (isCanDownload)
        {

            DataObjectSet dos = DataListAttachment.dataSource;
            //DataObjectSet docNewSet = new DataObjectSet();
            for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
            {
                string href = "DownloadFile.aspx";
                DataObject obj = dos.getDataObject(i);
                string fileName = obj.getData("FILENAME");
                string fileExt = obj.getData("FILEEXT");
                string sheetNo = obj.getData("SheetNo");
                string fileItemGUID = obj.getData("FileItemGUID");
                href += "?FILENAME=" + System.Web.HttpUtility.UrlPathEncode(fileName);
                href += "&FILEEXT=" + fileExt;
                href += "&SheetNo=" + sheetNo;
                href += "&FileItemGUID=" + fileItemGUID;
                string fileNameUrl = "{[a href=\"" + href + "\"]}" + fileName + "{[/a]}";

                obj.setData("FILENAME", fileNameUrl);
                //System.IO.StreamWriter sw = null;
                //sw = new System.IO.StreamWriter(@"d:\temp\SPKM001.log", true);
                //sw.WriteLine("FileURL:" + fileNameUrl);
                //sw.Close();
                //MessageBox("url:" + fileNameUrl);             
            }
            // DataListAttachment.dataSource = dos;
            DataListAttachment.setColumnStyle("FILENAME", 230, DSCWebControl.GridColumnStyle.LEFT);
            DataListAttachment.updateTable();
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
        //string sql = "";
        //NLAgent agent1 = null;
        //DataObjectSet set = null;
        //DataObject obj = null;

        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            bool isAddNew = base.isNew();
            string actName = (String)getSession("ACTName");
            string connectString = (string)Session["connectString"];
            IOFactory factory = new IOFactory();
            //MessageBox("savedatea");

            if (isAddNew)
            {
                //MessageBox("savedatea-is add new");
                string docGUID = IDProcessor.getID("");
                string revGUID = IDProcessor.getID("");
                objects.setData("GUID", com.dsc.kernal.utility.IDProcessor.getID(""));

                if (Subject.ValueText.Equals("") || Subject.ValueText.Equals("-文件新增"))
                {
                    Subject.ValueText = Name.ValueText + "-文件新增";
                }
                objects.setData("Subject", Subject.ValueText);
                objects.setData("OriginatorGUID", AuthorGUID.GuidValueText);
                objects.setData("CheckBy1GUID", CheckBy1GUID.GuidValueText);
                objects.setData("CheckBy2GUID", CheckBy2GUID.GuidValueText);
                objects.setData("RevGUID", revGUID);
                objects.setData("DocGUID", docGUID);
                //產生單號並儲存至資料物件,會存xml
                base.saveData(engine, objects);

                //歸屬群組
                for (int i = 0; i < DataListDocBelongGroup.dataSource.getAvailableDataObjectCount(); i++)
                {
                    DataListDocBelongGroup.dataSource.getAvailableDataObject(i).setData("RevGUID", objects.getData("RevGUID"));
                    DataListDocBelongGroup.dataSource.getAvailableDataObject(i).setData("DocGUID", objects.getData("DocGUID"));
                }

                //儲存附件
                for (int i = 0; i < DataListAttachment.dataSource.getAvailableDataObjectCount(); i++)
                {
                    DataListAttachment.dataSource.getAvailableDataObject(i).setData("DocGUID", objects.getData("DocGUID"));
                    DataListAttachment.dataSource.getAvailableDataObject(i).setData("RevGUID", objects.getData("RevGUID"));
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
            else
            { //把url檔名換掉                
                DataObjectSet dos = DataListAttachment.dataSource;
                for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
                {
                    DataObject obj = dos.getDataObject(i);
                    //此欄位為non-update field ，不dataObject.getData("FILENAME")
                    //將filename換回, 避免url過長, 造成saveDB報錯! 
                    string fileName = obj.getData("FILENAME");
                    int idxs = fileName.IndexOf("]}");
                    int idxe = fileName.LastIndexOf("{[");
                    if (idxs > 0 && idxe > 0)
                    {
                        fileName = fileName.Substring(idxs + 2, idxe - (idxs + 2));
                    }
                    obj.setData("FILENAME", fileName);
                }
                DataListAttachment.updateTable();
            }


            if (!actName.Equals("直屬主管"))
            {
                setSession("IsAddSign", "AFTER"); //beforeSign 加簽，若Session無值，不會執行beforesign()
            }

            if (actName.Equals("文管中心"))
            {
                objects.setData("EffectiveDate", DateTimeUtility.getSystemTime2(null).Substring(0, 10));
                //儲存歷史記錄
                string histogyGUID = IDProcessor.getID("");
                string userGUID = (string)Session["UserGUID"];
                string formGUID = objects.getData("GUID");
                string revGUID = objects.getData("RevGUID");
                string action = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_form_spkm.language.ini", "message", "DocChangeComplete", "文件新增");
                string description = objects.getData("Subject");
                string originatorGUID = objects.getData("OriginatorGUID");
                string originatorName = CreatorGUID.ValueText;
                string sheetNo = (string)getSession(PageUniqueID, "SheetNo");
                string revNumber = RevGUID.ReadOnlyText;
                string now = DateTimeUtility.getSystemTime2(null);

                DataObjectSet historySet = DataListHistory.dataSource;
                DataObject history = historySet.create();
                history.setData("GUID", histogyGUID);
                history.setData("DocGUID", objects.getData("DocGUID"));
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
                objects.setChild("SmpHistory", historySet);
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


    protected override string saveDraftProcedure()
    {
        com.dsc.kernal.databean.DataObject currentObject = (com.dsc.kernal.databean.DataObject)getSession("currentObject");

        string now = DateTimeUtility.getSystemTime2(null).Substring(0, 10);
        string expiryDate = (Convert.ToInt16(now.Substring(0, 4)) + 2) + now.Substring(4);

        currentObject.setData("RevNumber", "1");
        currentObject.setData("Status", "Create");
        currentObject.setData("MajorTypeGUID", MajorTypeGUID.GuidValueText);
        currentObject.setData("SubTypeGUID", SubTypeGUID.GuidValueText);
        currentObject.setData("DocTypeGUID", DocTypeGUID.GuidValueText);
        currentObject.setData("DocPropertyGUID", DocPropertyGUID.ValueText);
        currentObject.setData("ConfidentialLevel", ConfidentialLevel.ValueText);
        currentObject.setData("Name", Name.ValueText);
        currentObject.setData("AuthorGUID", AuthorGUID.GuidValueText);
        currentObject.setData("AuthorOrgUnitGUID", AuthorOrgUnitGUID.GuidValueText);
        currentObject.setData("Abstract", Abstract.ValueText);
        currentObject.setData("Site", Site.ValueText);
        currentObject.setData("KeyWords", KeyWords.ValueText);
        currentObject.setData("ExpiryDate", expiryDate);
        currentObject.setData("External", External.ValueText);
        setSession("currentObject", currentObject);
        return base.saveDraftProcedure();
    }

    /// <summary>
    /// 草稿讀取後設定. 通常在此方法中將有時效性以及識別欄位等內容替換, 以免覆蓋原本資料
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="currentObject">目前畫面資料物件(已由草稿內容取代)</param>
    //protected override void afterReadDraft(AbstractEngine engine, DataObject currentObject)
    //{
    //    try
    //    {
    //        /*
    //        string docGUID = currentObject.getData("DocGUID");
    //        string revGUID = currentObject.getData("RevGUID");
    //        MessageBox("docguid:" + docGUID);
    //        if (docGUID != null && !docGUID.Equals(""))
    //        {
    //            string sql ="";
    //            DataSet ds=null;
    //            sql = "select RevNumber, FormGUID, IndexCardGUID, " 
    //            + "'Create' Status, i.MajorTypeGUID, i.SubTypeGUID, i.DocTypeGUID, "
    //            + "i.DocPropertyGUID, i.ConfidentialLevel, i.Name, AuthorGUID,AuthorOrgUnitGUID, "
    //            + "i.Abstract, Site, i.KeyWords,i.ExpiryDate,i.External "
    //            + "from SmpDocument d "
    //            + "left join SmpRev r on d.GUID=r.DocGUID "
    //            + "left join SmpIndexCard i on i.GUID =r.IndexCardGUID "                
    //            + "where d.GUID='" + docGUID + "' and r.GUID ='" + revGUID + "' ";

    //            ds = engine.getDataSet(sql, "TEMP");
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {                            
    //                currentObject.setData("RevNumber", ds.Tables[0].Rows[0][0].ToString());
    //                currentObject.setData("Status", ds.Tables[0].Rows[0][3].ToString());                    
    //                currentObject.setData("MajorTypeGUID", ds.Tables[0].Rows[0][4].ToString());
    //                currentObject.setData("SubTypeGUID", ds.Tables[0].Rows[0][5].ToString());
    //                currentObject.setData("DocTypeGUID", ds.Tables[0].Rows[0][6].ToString());
    //                currentObject.setData("DocPropertyGUID", ds.Tables[0].Rows[0][7].ToString());
    //                currentObject.setData("ConfidentialLevel", ds.Tables[0].Rows[0][8].ToString());
    //                currentObject.setData("Name", ds.Tables[0].Rows[0][9].ToString());
    //                currentObject.setData("AuthorGUID", ds.Tables[0].Rows[0][10].ToString());
    //                currentObject.setData("AuthorOrgUnitGUID", ds.Tables[0].Rows[0][11].ToString());
    //                currentObject.setData("Abstract", ds.Tables[0].Rows[0][12].ToString());
    //                currentObject.setData("Site", ds.Tables[0].Rows[0][13].ToString());
    //                currentObject.setData("KeyWords", ds.Tables[0].Rows[0][14].ToString());
    //                currentObject.setData("ExpiryDate", ds.Tables[0].Rows[0][15].ToString());
    //                currentObject.setData("External", ds.Tables[0].Rows[0][16].ToString());                     
    //            }
                 
    //        }*/
    //    }
    //    catch (Exception e)
    //    {
    //        base.writeLog(e);
    //        throw new Exception(e.StackTrace);
    //    }
    //}

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

        if (isAddNew)
        {
            if (Name.ValueText.Equals(""))
            {
                errMsg += "[" + LblName.Text + "]欄位必需有值!\n";
            }

            //if (Status.ValueText.Equals(""))
            //{
            //    errMsg += "[" + LBLStatus.Text + "]欄位必需有值!\n";
            //}

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

            //if (ExpiryDate.ValueText.Equals(""))
            //{
            //    errMsg += "[" + LblExpiryDate.Text + "]欄位必需有值!\n";
            //}
            //else
            //{
            //    //不可大於目前時間加兩年                
            //    string now = DateTimeUtility.getSystemTime2(null);
            //    string expiryDate = (Convert.ToInt16(now.Substring(0, 4)) + 2) + now.Substring(4, 6);
            //    if (ExpiryDate.ValueText.CompareTo(expiryDate) > 0)
            //    {
            //        errMsg += "[" + LblExpiryDate.Text + "]不可大於 " + expiryDate + "\n";
            //    }

            //}

            if (DataListDocBelongGroup.dataSource.getAvailableDataObjectCount() == 0)
            {
                errMsg += "[" + DSCTabDoc.TabPages[1].Title + "] 筆數不得為0!\n";
            }

            DataObjectSet setAtta = DataListAttachment.dataSource;
            int attaCount = setAtta.getAvailableDataObjectCount();
            if (attaCount == 0)
            {
                errMsg += "[" + DSCTabDoc.TabPages[2].Title + "] 筆數不得為0!\n";
            }
            else
            {
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
                        errMsg += "此文件設定為包含外部文件, 附件清單必須包含外部文件!\n";
                    }
                }
            }

            ////檢查檔案類型
            //DataObjectSet attachmentSet = DataListAttachment.dataSource;
            //checkMsg = checkKMFileType(attachmentSet);
            //if (!checkMsg.Equals(""))
            //{
            //    errMsg += checkMsg;
            //}           
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

        //si.ownerID = AuthorGUID.ValueText; //表單關系人      
        // si.ownerName = AuthorGUID.ReadOnlyValueText; 

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
        string[][] valuesAdm = null;
        string[] valuesManager = null;
        string checkBy = "";
        string manager = "";
        string confidentialLevel = "";
        string notifier = "";

        if (ConfidentialLevel.ValueText.Equals("0"))
        {
            confidentialLevel = "N";
        }
        else
        {
            confidentialLevel = "Y";
        }

        //審核人
        if (!CheckBy1GUID.ValueText.Equals(""))
        {
            checkBy += CheckBy1GUID.ValueText;
        }

        if (!CheckBy2GUID.ValueText.Equals(""))
        {

            if (!CheckBy1GUID.ValueText.Equals(""))
            {
                checkBy += ";" + CheckBy2GUID.ValueText;
            }
            else
            {
                checkBy += CheckBy2GUID.ValueText;
            }

        }

        //取得管理者(主類別+子類別)
        valuesAdm = getDocTypeAdmUserGUID(engine, DocTypeGUID.GuidValueText);
        if (valuesAdm != null && valuesAdm.Length > 0)
        {
            for (int i = 0; i < valuesAdm.Length; i++)
                if (!notifier.Contains(valuesAdm[i][1]))
                    notifier += valuesAdm[i][1] + ";";
        }

        if (!notifier.Contains(AuthorGUID.ValueText))
            notifier += AuthorGUID.ValueText + ";";

        if (!notifier.Contains(CreatorGUID.ReadOnlyValueText))
        {
            notifier += CreatorGUID.ReadOnlyValueText;
        }

        valuesManager = base.getUserManagerInfo(engine, AuthorGUID.GuidValueText); //作者主管
        manager = valuesManager[1];


        xml += "<SPKM001>";
        xml += "<checkBy DataType=\"java.lang.String\">" + checkBy + "</checkBy>";
        xml += "<manager DataType=\"java.lang.String\">" + manager + "</manager>";
        xml += "<typeAdm DataType=\"java.lang.String\">" + manager + "</typeAdm>";
        xml += "<confidentialLevel DataType=\"java.lang.String\">" + confidentialLevel + "</confidentialLevel>";
        xml += "<notifier DataType=\"java.lang.String\">" + notifier + "</notifier>";
        xml += "</SPKM001>";

        //表單代號
        param["SPKM001"] = xml;
        return "SPKM001";
    }

    /// <summary>
    /// 發起流程後呼叫此方法, 只會執行一次，第一次送單
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            string sql = "";
            string sql1 = "";


            //初始值                         
            string userGUID = (string)Session["UserGUID"];
            string indexCardGUID = IDProcessor.getID("");
            string now = DateTimeUtility.getSystemTime2(null);
            string expiryDate = (Convert.ToInt16(now.Substring(0, 4)) + 2) + now.Substring(4,6);
            string sheetNo = (string)getSession(PageUniqueID, "SheetNo");
            string docGUID = currentObject.getData("DocGUID");
            string revGUID = currentObject.getData("RevGUID");
            string formGUID = currentObject.getData("GUID");

            //document
            sql = "insert into SmpDocument( GUID, AuthorGUID, AuthorOrgUnitGUID, Site,"
                    + "IS_LOCK, IS_DISPLAY, DATA_STATUS, D_INSERTUSER, D_INSERTTIME ,D_MODIFYUSER, D_MODIFYTIME) SELECT '"
                    + docGUID + "' ,'" + AuthorGUID.GuidValueText + "','" + AuthorOrgUnitGUID.GuidValueText + "','" + Site.ValueText + "'," 
                    + "'N','Y','Y','" + userGUID + "','" + now + "','','' ";
            if (!engine.executeSQL(sql))
            {                
                throw new Exception(engine.errorString);
            }
                       
            //indexCard
            //sql = "insert into SmpIndexCard (GUID, Status, MajorTypeGUID, SubTypeGUID, DocTypeGUID, DocPropertyGUID, ConfidentialLevel, DocGUID, Name, Abstract, KeyWords, EffectiveDate, ExpiryDate, "
            //    + "IS_LOCK, IS_DISPLAY, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME,External) " ;
            //sql1 = "SELECT '" +  indexCardGUID + "' ,'Create','" + MajorTypeGUID.GuidValueText + "','" + SubTypeGUID.GuidValueText + "','"
            //    + DocTypeGUID.GuidValueText + "','" + DocPropertyGUID.ValueText + "','" + ConfidentialLevel.ValueText + "','" + docGUID + "',N'"
            //    + Name.ValueText + "',N'" + Abstract.ValueText + "',N'" + KeyWords.ValueText + "','','"
            //    + ExpiryDate.ValueText + "','N','Y','Y','" + userGUID + "','" + now + "','','','" + External.ValueText + "' ";
                       
            //if (!engine.executeSQL(sql+sql1))
            //{               
            //    throw new Exception(engine.errorString );
            //}
            

            //Rev
            sql = "insert into SmpRev (GUID, RevNumber, DocGUID, FormGUID, IndexCardGUID, Released, LatestFlag,"
                + "IS_LOCK, IS_DISPLAY, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME,SheetNo) SELECT '"
                + revGUID + "' ,'1','" + docGUID + "','" + formGUID + "','"
                + indexCardGUID + "','N','N','N','Y','Y','" + userGUID + "','" + now + "','','','" + sheetNo + "' ";

            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }

            ////GET engine         
            //engine1 = factory.getEngine(EngineConstants.SQL, connectString);

            ////Document               
            //agent1 = new NLAgent();
            //agent1.loadSchema("WebServerProject.form.SPKM001.SmpDocumentAgent");
            //agent1.engine = engine1;
            ////agent1.engine = engine;
            //agent1.query("1=2");
            //set = agent1.defaultData;
            //obj = set.create();
            //obj.setData("GUID", docGUID);
            //obj.setData("AuthorGUID", AuthorGUID.GuidValueText);
            //obj.setData("AuthorOrgUnitGUID", AuthorOrgUnitGUID.GuidValueText);
            //obj.setData("Site", Site.ValueText);
            //obj.setData("IS_DISPLAY", "Y");
            //obj.setData("IS_LOCK", "N");
            //obj.setData("DATA_STATUS", "Y");
            //obj.setData("D_INSERTUSER", objects.getData("D_INSERTUSER"));
            //obj.setData("D_INSERTTIME", objects.getData("D_INSERTTIME"));
            //obj.setData("D_MODIFYUSER", objects.getData("D_MODIFYUSER"));
            //obj.setData("D_MODIFYTIME", objects.getData("D_MODIFYTIME"));
            //set.add(obj);
            //agent1.defaultData = set;
            //if (!agent1.update())
            //{
            //    throw new Exception(engine1.errorString);
            //}

            //Index Card            
            AbstractEngine engine1 = factory.getEngine(engineType, connectString);
            NLAgent agent1 = new NLAgent();
            agent1.loadSchema("WebServerProject.form.SPKM001.SmpIndexCardAgent");
            agent1.engine = engine1;
            agent1.query("1=2");  

            DataObjectSet set = null;
            DataObject obj = null;
            set = agent1.defaultData;
            obj = set.create();
            obj.setData("GUID", indexCardGUID);
            obj.setData("Status", "Create");
            obj.setData("MajorTypeGUID", MajorTypeGUID.GuidValueText);
            obj.setData("SubTypeGUID", SubTypeGUID.GuidValueText);
            obj.setData("DocTypeGUID", DocTypeGUID.GuidValueText);
            obj.setData("DocPropertyGUID", DocPropertyGUID.ValueText);
            obj.setData("ConfidentialLevel", ConfidentialLevel.ValueText);                    
            obj.setData("DocGUID", docGUID);
            obj.setData("Name", Name.ValueText);
            obj.setData("Abstract", Abstract.ValueText);
            obj.setData("KeyWords", KeyWords.ValueText);                       
            obj.setData("EffectiveDate", EffectiveDate.ValueText);
            obj.setData("ExpiryDate", expiryDate);            
            obj.setData("External", External.ValueText);
            obj.setData("IS_DISPLAY", "Y");
            obj.setData("IS_LOCK", "N");
            obj.setData("DATA_STATUS", "Y");   
            set.add(obj);
            agent1.defaultData = set;
            if (!agent1.update())
            {
                throw new Exception(engine1.errorString);
            }

            ////Rev                  
            //agent1 = new NLAgent();
            //agent1.loadSchema("WebServerProject.form.SPKM001.SmpRevAgent");
            //agent1.engine = engine1;
            //agent1.query("1=2");
            //set = null;
            //obj = null;
            //set = agent1.defaultData;
            //obj = set.create();
            //obj.setData("GUID", revGUID);
            //obj.setData("RevNumber", RevGUID.ValueText);
            //obj.setData("DocGUID", docGUID);
            //obj.setData("FormGUID", objects.getData("GUID"));
            //obj.setData("indexCardGUID", indexCardGUID);
            //obj.setData("Released", "N");
            //obj.setData("LatestFlag", "N");
            //obj.setData("IS_DISPLAY", "Y");
            //obj.setData("IS_LOCK", "N");
            //obj.setData("DATA_STATUS", "Y");
            //obj.setData("D_INSERTUSER", objects.getData("D_INSERTUSER"));
            //obj.setData("D_INSERTTIME", objects.getData("D_INSERTTIME"));
            //obj.setData("D_MODIFYUSER", objects.getData("D_MODIFYUSER"));
            //obj.setData("D_MODIFYTIME", objects.getData("D_MODIFYTIME"));
            //set.add(obj);
            //agent1.defaultData = set;
            //if (!agent1.update())
            //{
            //    throw new Exception(engine1.errorString);
            //}
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
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

        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"d:\temp\SPKM001.log", true);
        //sw.WriteLine(actName);
        string actName = Convert.ToString(getSession("ACTName"));
        if (actName.Equals("文管中心"))
        {
            //產生發佈檔
            DataObjectSet attachmentSet = DataListAttachment.dataSource;
            string result = printOutPdf(engine, attachmentSet);

            DataListAttachment.dataSource = attachmentSet;
            DataListAttachment.updateTable();
            if (!result.Equals(""))
            {
                throw new Exception(result);
            }

            //通知相關人員
            //string revGUID = "";
            //string docTypeGUID = "";
            //string xml = "";
            //string sql = "";
            //sql = "select r.GUID RevGUID, i.DocTypeGUID  from SmpDocReleaseForm d " +
            //      "join SmpRev r on d.GUID = r.FormGUID " +
            //      "join SmpIndexCard i on r.IndexCardGUID = i.GUID " +
            //      "where d.SheetNo = '" + (string)getSession(PageUniqueID, "SheetNo") + "'";
            //DataSet ds = engine.getDataSet(sql, "TEMP");
            //if (ds.Tables[0].Rows.Count > 0)  
            //{
            //    revGUID = ds.Tables[0].Rows[0][0].ToString();
            //    docTypeGUID = ds.Tables[0].Rows[0][1].ToString();
            //}

            /*不展開群組與部門
            sql =
                //歸屬群組
                    "select BelongGroupType, BelongGroupGUID from SmpDocBelongGroup where RevGUID='" + revGUID + "' "
                //讀者
                    + "union select BelongGroupType, BelongGroupGUID from SmpReader where RevGUID='" + revGUID + "' "
                //管理人員
                    + "union select distinct '1' BelongGroupType, c.OID BelongGroupGUID FROM ("
                    + "    select b.OID, b.id, b.userName  from dbo.SmpMajorTypeAdm a, dbo.Users b, dbo.SmpDocType d "
                    + "    where d.GUID = '" + docTypeGUID + "' and b.OID = a.MajorTypeAdmUserGUID and a.MajorTypeGUID = d.MajorTypeGUID "
                    + "    union "
                    + "    select b.OID, b.id, b.userName from dbo.SmpSubTypeAdm a, dbo.Users b, dbo.SmpDocType d "
                    + "    where d.GUID = '" + docTypeGUID + "' and b.OID = a.SubTypeAdmUserGUID AND a.SubTypeGUID = d.SubTypeGUID) c ";           

            ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            if (rows > 0)
            {
                xml += "<list>";
            }

            for (int i = 0; i < rows; i++)
            {
                string participantType = "";
                string performType = "NOTICE";
                string belongGroupType = ds.Tables[0].Rows[i][0].ToString();
                string belongGroupGUID = ds.Tables[0].Rows[i][1].ToString();
                string stateValueName = "通知"; 

                if (belongGroupType.Equals("1"))
                {
                    participantType = "HUMAN";
                    sql = "select '人員: ' + userName from Users where OID='" + belongGroupGUID + "'";
                    stateValueName = (string)engine.executeScalar(sql);
                }
                else if (belongGroupType.Equals("21"))
                {
                    participantType = "GROUP";
                    sql = "select KindName + ': ' + Name from SmpBelongGroupV where OID='" + belongGroupGUID + "'";
                    stateValueName = (string)engine.executeScalar(sql);
                }
                else if (belongGroupType.Equals("9"))
                {
                    participantType = "ORGANIZATION_UNIT";
                    sql = "select KindName + ': ' + Name from SmpBelongGroupV where OID='" + belongGroupGUID + "'";
                    stateValueName = (string)engine.executeScalar(sql);
                }

                xml += "    <com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                xml += "        <performers>";
                xml += "            <com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                xml += "                <OID>" + belongGroupGUID + "</OID>";
                xml += "                <participantType><value>" + participantType + "</value></participantType>";
                xml += "            </com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                xml += "        </performers>";
                xml += "        <multiUserMode><value>FIREST_GET_FIRST_WIN</value></multiUserMode>";
                xml += "        <name>" + stateValueName + "</name>";
                xml += "        <performType><value>" + performType + "</value></performType>";
                xml += "    </com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
            }
            */

            //歸屬群組-部門 
            /*
            sql = "select U.empGUID userOID,U.empName userName from SmpDocBelongGroup B " +
            "join EmployeeInfoAllDept U on  B.BelongGroupGUID = U.organizationGUID " +
            "where B.BelongGroupType ='9' and B.RevGUID='" + revGUID + "' " +
            "and U.empLeaveDate is null " +
            "union " +  //歸屬群組-群組
            "select U.UserOID userOID,U2.userName from SmpDocBelongGroup B " +
            "join Group_User U on  B.BelongGroupGUID = U.GroupOID " +
            "join Groups U1 on U1.OID = U.GroupOID " +
            "join Users U2 on U2.OID = U.UserOID " +
            "where B.BelongGroupType ='21' and B.RevGUID='" + revGUID + "' and U2.leaveDate is null and U1.id <> 'SMPKMALL' " +
            "union " + //union SMPALLUser
            "select U.OID userOID,U.userName from  Users U " +
            "where U.id = 'SMP-All User' and " +
            "'SMPKMALL' = ( select  U1.id  from SmpDocBelongGroup B " +
            "join Groups U1 on U1.OID = B.BelongGroupGUID " +
            "where B.BelongGroupType ='21' and B.RevGUID='" + revGUID + "' and U1.id = 'SMPKMALL') " +
            "union " +  //讀者           
            "select U.OID userOID,U.userName from SmpReader R " +
            "join Users U on  R.BelongGroupGUID = U.OID " +
            "where R.BelongGroupType ='1' and R.RevGUID='" + revGUID + "' and U.leaveDate is NULL " +
            "union " +
            "select U.empGUID userOID,U.empName userName from SmpReader R " +
            "join EmployeeInfoAllDept U on  R.BelongGroupGUID = U.organizationGUID " +
            "where R.BelongGroupType ='9' and R.RevGUID='" + revGUID + "' " +
            "and U.empLeaveDate is null " +
            "union " +
            "select U.UserOID userOID,U2.userName from SmpReader R " +
            "join Group_User U on  R.BelongGroupGUID = U.GroupOID " +
            "join Groups U1 on U1.OID = U.GroupOID " +
            "join Users U2 on U2.OID = U.UserOID " +
            "where R.BelongGroupType ='21' and R.RevGUID='" + revGUID + "' and U2.leaveDate is null and U1.id <> 'SMPKMALL' " +
            "union " +  //union SMPKMALL
            "select U.OID UserOID,U.userName from  Users U " +
            "where U.id = 'SMP-All User' and " +
            "'SMPKMALL' = (select  U1.id  from SmpReader R " +
            "join Groups U1 on U1.OID = R.BelongGroupGUID " +
            "where R.BelongGroupType ='21' and R.RevGUID='" + revGUID + "' and U1.id = 'SMPKMALL') ";

            sw.WriteLine(sql);
            sw.Close();
               
            ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            if (rows > 0)
            {
                xml += "<list>";
            }

            string participantType = "HUMAN";
            string performType = "NOTICE";
            for (int i = 0; i < rows; i++)
            {
                string belongGroupGUID = ds.Tables[0].Rows[i][0].ToString();
                string stateValueName = ds.Tables[0].Rows[i][1].ToString();
                xml += "    <com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                xml += "        <performers>";
                xml += "            <com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                xml += "                <OID>" + belongGroupGUID + "</OID>";
                xml += "                <participantType><value>" + participantType + "</value></participantType>";
                xml += "            </com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                xml += "        </performers>";
                xml += "        <multiUserMode><value>FIREST_GET_FIRST_WIN</value></multiUserMode>";
                xml += "        <name>" + stateValueName + "</name>";
                xml += "        <performType><value>" + performType + "</value></performType>";
                xml += "    </com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";

            }
            if (rows > 0)
            {
                xml += "</list>";
                return xml;
            }*/
        }


        //if (sw != null)
        //    sw.Close();

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
                AbstractEngine engine1 = null;
                string sql = "";
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                engine1 = factory.getEngine(engineType, connectString);
                sql = "update SmpIndexCard set Status ='Reject'" +
                     "where GUID = (SELECT IndexCardGUID from SmpRev where SheetNo ='" + (string)getSession(PageUniqueID, "SheetNo") + "') ";

                if (!engine1.executeSQL(sql))
                {
                    throw new Exception(engine1.errorString);
                }
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
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {       
        
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"d:\temp\SPKM001.log", true);
        //sw.WriteLine("ENTRY AFTERAPPROVE");


        if (result.Equals("Y"))
        {
            //sw.WriteLine("RESULT IS Y");

            string subject = currentObject.getData("Subject");
            string userGUID = (string)Session["UserGUID"];
            string formGUID = currentObject.getData("GUID");
            string docGUID = currentObject.getData("DocGUID");
            string revGUID = currentObject.getData("RevGUID");
            string indexCardGUID = currentObject.getData("IndexCardGUID");
            string histogyGUID = IDProcessor.getID("");
            string now = DateTimeUtility.getSystemTime2(null);
            string sql = null;
            //string expiryDate = ExpiryDate.ValueText;
            string docNo = getCustomSheetNo(engine, "SMPKMDOCInternalSeqNo");
            string effectiveDate = now.Substring(0, 10);
            string authorGUID = currentObject.getData("AuthorGUID");
            //currentObject.setData("Status", "Closed");
            //currentObject.setData("EffectiveDate", effectiveDate);

            //更新文件編號   
            sql = "update SmpDocument set DocNumber='" + docNo + "' where GUID = '" + docGUID + "' ";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }

            //更新此版本為生效最後一版                            
            sql = "update SmpRev set Released = 'Y', LatestFlag='Y', ReleaseDate='" + effectiveDate + "', D_MODIFYUSER='" + userGUID + "', D_MODIFYTIME='"
                + now + "' where DocGUID='" + docGUID + "' and GUID='" + revGUID + "'";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }

            //更新索引卡內容狀態為結案
            sql = "update SmpIndexCard set Status='Closed', EffectiveDate='" + effectiveDate + "' ,D_MODIFYUSER='" + userGUID + "', D_MODIFYTIME='" + now + "' where DocGUID='" + docGUID
                + "' and GUID='" + indexCardGUID + "'";
            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }

            //新增事件記錄
            //sql = "insert into SmpHistory (GUID, DocGUID, Action, Description, RevGUID, FormGUID, IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, "
            //    + "D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) "
            //    + "select '" + histogyGUID + "' GUID, '" + docGUID + "' DocGUID, '文件新增' Action, '" + subject + "' Description, '" + revGUID + "' RevGUID, '"
            //    + formGUID + "' FormGUID, 'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + authorGUID + "' D_INSERTUSER, '" + now 
            //    + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME from SmpDocReleaseForm where GUID='" + formGUID + "'";
            //if (!engine.executeSQL(sql))
            //{
            //    throw new Exception(engine.errorString);
            //}
        }
        //else
        //{
        //    sw.WriteLine("RESULT IS "+result);
        //}
        //sw.Close();
        base.afterApprove(engine, currentObject, result);
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
            obj.setData("DocGUID", "DocGUID");
            obj.setData("RevGUID", "RevGUID");
            obj.setData("FileItemGUID", currentObject.GUID);
            obj.setData("FILENAME", currentObject.FILENAME);
            obj.setData("FILEEXT", currentObject.FILEEXT);
            obj.setData("AttachmentType", "Original");
            obj.setData("DESCRIPTION", currentObject.DESCRIPTION);
            obj.setData("External", "N"); //是否為外部文件
            obj.setData("UPLOADUSER", si.fillerName);
            obj.setData("UPLOADTIME", currentObject.UPLOADTIME);
            obj.setData("IS_LOCK", "N");
            obj.setData("IS_DISPLAY", "Y");
            obj.setData("DATA_STATUS", "Y");
            detailSet.add(obj);
            DataListAttachment.dataSource = detailSet;
            DataListAttachment.updateTable();
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
    /// 歸屬群組資料顯示
    /// </summary>
    /// <param name="objects"></param>
    protected void DataListDocBelongGroup_ShowRowData(DataObject objects)
    {
        DocBelongGroupGUID.valueIndex = 2;
        DocBelongGroupGUID.GuidValueText = objects.getData("BelongGroupGUID");
        DocBelongGroupGUID.doGUIDValidate();
    }

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
            objects.setData("DocGUID", "TEMP");
            objects.setData("RevGUID", "TEMP");
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
                strErrMsg += LblDocGUID.Text + ": 必需選擇!\n";
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
            objects.setData("DocGUID", "DocTEMP");
            objects.setData("RevGUID", "RevTEMP");
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
        if (DocTypeGUID.ValueText.Equals(""))
        {
            strErrMsg += "請先選擇文件類別!\n";
        }
        if (ReaderBelongGroupType.ValueText.Equals(""))
        {
            strErrMsg += "請先選擇對象!\n";
        }
        if (ReaderBelongGroupGUID.ValueText.Equals(""))
        {
            strErrMsg += "請先選擇讀者名稱!\n";
        }

        //文件類別預設讀者不需要新增
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, connectString);
        string docTypeGUID = DocTypeGUID.GuidValueText;
        string belongGroupGUID = ReaderBelongGroupGUID.GuidValueText;        
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
            objects.setData("DocGUID", "DocTEMP");
            objects.setData("RevGUID", "RevTEMP");
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
            objects.setData("FromAccess", "N");
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
    /// 主分類開窗只能選擇生效
    /// </summary>   
    protected void MajorTypeGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            MajorTypeGUID.whereClause = "(Enable='Y')";
        }
    }

    /// <summary>
    /// 子分類開窗只能選擇生效
    /// </summary>   
    protected void SubTypeGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            string majorTypeGUID = MajorTypeGUID.GuidValueText;
            SubTypeGUID.whereClause = "(MajorTypeGUID='" + majorTypeGUID + "' and Enable='Y')";
        }
    }

    /// <summary>
    /// 文件類別開窗只能選擇生效
    /// </summary>   
    protected void DocTypeGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            string majorTypeGUID = MajorTypeGUID.GuidValueText;
            string subTypeGUID = SubTypeGUID.GuidValueText;
            DocTypeGUID.whereClause = "(MajorTypeGUID='" + majorTypeGUID + "' and SubTypeGUID='" + subTypeGUID + "' and Enable='Y')";
        }

    }

    /// <summary>
    /// 子分類變更重新取得歸屬群組
    /// </summary>
    /// <param name="values"></param>
    protected void SubTypeGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        try
        {
            if (base.isNew())
            {
                DocTypeGUID.doGUIDValidate();
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
            }

        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
    }


    /// <summary>
    /// 傳入文件類別取得預設讀者, [0]: Kind, [1]: KindName, [2]: groupOID, [3]: id, [4]: groupName
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="GUID"></param>
    /// <returns>string[]</returns>
    protected string[][] getDocTypeReaderGUID(AbstractEngine engine, string GUID)
    {
        string sql = "select c.Kind, c.KindName, c.OID, c.id, c.Name" +
                     "  from SmpDocTypeReader r, " +
                     "       SmpReaderV c" +
                     " where r.DocTypeGUID = '" + Utility.filter(GUID) + "'" +
                     "   and c.OID = r.BelongGroupGUID ";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;
        string[][] result = new string[rows][];
        for (int i = 0; i < rows; i++)
        {
            result[i] = new string[5];
            result[i][0] = ds.Tables[0].Rows[i][0].ToString();
            result[i][1] = ds.Tables[0].Rows[i][1].ToString();
            result[i][2] = ds.Tables[0].Rows[i][2].ToString();
            result[i][3] = ds.Tables[0].Rows[i][3].ToString();
            result[i][4] = ds.Tables[0].Rows[i][4].ToString();
        }
        return result;
    }



    /// <summary>
    /// 文件類別變更重新取得外部文件屬性及預設讀者
    /// </summary>
    /// <param name="values"></param>
    protected void DocTypeGUID_SingleOpenWindowButtonClick1(string[,] values)
    {
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(EngineConstants.SQL, connectString);

            //重新取得此文件類別是否包含外部文件
            if (!DocTypeGUID.GuidValueText.Equals(""))
            {
                string sql = "select External from SmpDocType where GUID='" + DocTypeGUID.GuidValueText + "'";
                string external = (string)engine.executeScalar(sql);
                External.ValueText = external;
                if (external.Equals("N"))
                    External.ReadOnly = true;
                else
                    External.ReadOnly = false;

                //預設讀者
                if (base.isNew())
                {
                    //清空預設讀者                           
                    DataObjectSet setReader = DataListReader.dataSource;
                    int count = setReader.getDataObjectCount();
                    if (count > 0)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            DataObject reader = setReader.getDataObject(i);
                            setReader.delete(reader);
                        }
                        DataListReader.updateTable();
                    }


                    //取得預設讀者, [0]: Kind, [1]: KindName, [2]: GUID, [3]: id, [4]: Name   
                    //2015/06/15 改為不再取得預設讀者
                    //string[][] results = getDocTypeReaderGUID(engine, DocTypeGUID.GuidValueText);
                    //if (results != null && results.Length > 0)
                    //{
                    //    DataObjectSet readerSet = DataListReader.dataSource;
                    //    for (int i = 0; i < results.Length; i++)
                    //    {
                    //        DataObject obj = null;
                    //        obj = readerSet.create();
                    //        obj.setData("GUID", IDProcessor.getID(""));
                    //        obj.setData("DocGUID", "TEMP");
                    //        obj.setData("RevGUID", "TEMP");
                    //        obj.setData("BelongGroupType", results[i][0]);
                    //        obj.setData("KindName", results[i][1]);
                    //        obj.setData("BelongGroupGUID", results[i][2]);
                    //        obj.setData("id", results[i][3]);
                    //        obj.setData("Name", results[i][4]);
                    //        obj.setData("IS_LOCK", "N");
                    //        obj.setData("IS_DISPLAY", "Y");
                    //        obj.setData("DATA_STATUS", "Y");
                    //        obj.setData("FromAccess", "N");
                    //        readerSet.add(obj);
                    //    }
                    //    DataListReader.dataSource = readerSet;
                    //    DataListReader.updateTable();
                    //}
                }
                //開啟預設讀者
                GbDcoTypeReader.ReadOnly = false;
            }
            engine.close();

        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }

    }

    /// <summary>
    /// 變更作者
    /// </summary>
    /// <param name="values"></param>
    protected void AuthorGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, connectString);

        //更新部門
        string[] deptInfoValues = base.getDeptInfo(engine, AuthorGUID.GuidValueText);
        if (!deptInfoValues[0].Equals(""))
        {
            AuthorOrgUnitGUID.clientEngineType = engineType;
            AuthorOrgUnitGUID.connectDBString = connectString;
            AuthorOrgUnitGUID.ValueText = deptInfoValues[0];
            AuthorOrgUnitGUID.doValidate();
        }

        //更新公司別                      
        string[] siteValues = base.getUserInfoById(engine, AuthorGUID.ValueText); //傳工號
        Site.ValueText = siteValues[5];
    }

    /// <summary>
    /// 判斷 KM 原始檔附件是否符合
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="attachmentSet"></param>
    /// <returns>string</returns>
    //protected string checkKMFileType(DataObjectSet attachmentSet)
    //{
    //    string errMsg = "";
    //    string[] kmFileTypes = new string[] { "xls", "doc", "ppt", "pdf" };

    //    try
    //    {
    //        for (int i = 0; i < attachmentSet.getAvailableDataObjectCount(); i++)
    //        {
    //            DataObject objAttachment = attachmentSet.getAvailableDataObject(i);
    //            string fileType = objAttachment.getData("FILEEXT");
    //            string attType = objAttachment.getData("AttachmentType");
    //            if (attType.Equals("Original"))
    //            {
    //                Boolean isKmType = false;
    //                for (int j = 0; j < kmFileTypes.Length; j++)
    //                {
    //                    if (fileType.Equals(kmFileTypes[j]))
    //                    {
    //                        isKmType = true;
    //                        break;
    //                    }
    //                }

    //                if (!isKmType)
    //                {
    //                    errMsg = "[僅可上傳下列檔案類型的附件:";
    //                    for (int j = 0; j < kmFileTypes.Length; j++)
    //                    {
    //                        if (j == 0)
    //                            errMsg += "." + kmFileTypes[j];
    //                        else
    //                            errMsg += "/." + kmFileTypes[j];
    //                    }
    //                    errMsg += "]";
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        base.writeLog(e);
    //    }

    //    return errMsg;
    //}

    /// <summary>
    /// 過濾已失效文件
    /// </summary>
    protected void ReferenceGUID_BeforeClickButton()
    {
        ReferenceGUID.whereClause = "(Status != 'Cancelled')";
    }

    /// <summary>
    /// 檢示文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GlassButtonViewDoc_Click(object sender, EventArgs e)
    {
        setSession((string)Session["UserID"], "ViewDocGUID", DocGUID.GuidValueText);
    }

    /// <summary>
    /// 表單結案顯示檢示文件按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GlassButtonViewDoc_Load(object sender, EventArgs e)
    {
        if (!DocGUID.ValueText.Equals(""))
        {
            GlassButtonViewDoc.ReadOnly = false;
        }
    }

    /// <summary>
    /// 新增時，作者，不可選離職人員
    /// </summary>
    /// <param name="values"></param>
    protected void AuthorGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            AuthorGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }

    /// <summary>
    /// 新增時，審核人一，不可選離職人員
    /// </summary>
    /// <param name="values"></param>
    protected void CheckBy1GUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            CheckBy1GUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }

    /// <summary>
    /// 新增時，審核人二，不可選離職人員
    /// </summary>
    /// <param name="values"></param>
    protected void CheckBy2GUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            CheckBy2GUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }

    /// <summary>
    /// 檔案下載前記錄檔案
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    /// <returns></returns>
    protected bool setDownload(DataObject objects)
    {
        string released = Released.ValueText;
        bool isCanDownload = true;
        if (released.Equals("Y"))
        {
            MessageBox("表單已結案不允許下載檔案，請點選[文件屬性]->[檢視文件]檢視文件內容!");
            //GlassButtonViewDoc.ReadOnly = false;
            //DataListAttachment.InputForm = "";
            isCanDownload = false;
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
    /// 儲存附件列資料, External文件檢查
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    /// <returns></returns>
    protected bool DataListAttachment_SaveRowData(DataObject objects, bool isNew)
    {
        string external = External.ValueText;
        string attaExternal = AttaExternal.ValueText;
        if (attaExternal.Equals("Y"))
        {
            if (!external.Equals("Y"))
            {
                MessageBox("文件類別必需設定為包含外部文件，才可上傳外部文件!");
                return false;
            }
        }
        objects.setData("External", attaExternal);
        return true;
    }

    /// <summary>
    /// 顯示附件列資料, 檔名處理
    /// </summary>
    /// <param name="objects"></param>
    protected void DataListAttachment_ShowRowData(DataObject objects)
    {
        string fileName = objects.getData("FILENAME");
        int idxs = fileName.IndexOf("]}");
        int idxe = fileName.LastIndexOf("{[");
        if (idxs > 0 && idxe > 0)
        {
            fileName = fileName.Substring(idxs + 2, idxe - (idxs + 2));
        }
        AttaFileName.ValueText = fileName;
        AttaExternal.ValueText = objects.getData("External");

        setDownload(objects);

    }

    //查看預設讀者按鈕
    protected void GbDcoTypeReader_Load(object sender, EventArgs e)
    {
        if (DocGUID.ValueText.Equals(""))
        {
            GbDcoTypeReader.ReadOnly = true;
        }
        else
        {
            GbDcoTypeReader.ReadOnly = false;
        }

    }

    /// <summary>
    /// 設讀者
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbDcoTypeReader_Click(object sender, EventArgs e)
    {
        setSession((string)Session["UserID"], "DocTypeGUID", DocTypeGUID.GuidValueText);        
    }
   
}