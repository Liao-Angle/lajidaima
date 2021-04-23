using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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

public partial class SmpProgram_Form_SPKM005_Form : SmpKmFormPage
{

    /// <summary>
    /// 初始化參數。
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPKM005";
        AgentSchema = "WebServerProject.form.SPKM005.SMWYAAAAgent";
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
        string showBackListButton = Request.QueryString["ShowBackList"];
        if (showBackListButton != null && showBackListButton.Equals("N"))
        {
            BackListButton.Display = false;
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script language=javascript>");
        
        sb.Append(" function clickChange(){");
        sb.Append("     window.location='../SPKM002/Form.aspx?UIStatus=0&UIType=Process&CurPanelID=1&DocGUID=" + objects.getData("DocGUID") + "';");
        sb.Append(" }");
        
        sb.Append(" function clickAccess(){");
        sb.Append("     window.location='../SPKM004/Form.aspx?UIStatus=0&UIType=Process&CurPanelID=1&DocGUID=" + objects.getData("DocGUID") + "';");
        sb.Append(" }");
        
        sb.Append(" function clickVoid(){");
        sb.Append("     window.location='../SPKM003/Form.aspx?UIStatus=0&UIType=Process&CurPanelID=1&DocGUID=" + objects.getData("DocGUID") + "';");
        sb.Append(" }");
        
        sb.Append(" function submitRevHistory(){");
        sb.Append("    window.parent.setZIndex('" + (string)getSession("ParentPanelID") + "');"); //這行可以把指定的panelID帶到最前端
        sb.Append("    wobj=window.parent.getPanelWindowObject('" + (string)getSession("ParentPanelID") + "');"); //這行可以取得該panelID的PanelWindow中代表內容的window HTML物件
        sb.Append("    wobj.refreshDataList('" + (string)getSession("DataListID") + "');"); //這行可以呼叫該視窗的refreshDataList方法, 此方法為WebFormBasePage提供
        sb.Append(" }");

        sb.Append(" function clickViewReference(url){");
        sb.Append("     parent.window.openWindowGeneral('檢視關聯文件', url,'','','',true,true);");
        sb.Append(" }");

        sb.Append("</script>");
        Type ctype = this.GetType();
        ClientScriptManager cm = Page.ClientScript;
        if (!cm.IsStartupScriptRegistered(ctype, "clickSubmitScript"))
        {
            cm.RegisterStartupScript(ctype, "clickSubmitScript", sb.ToString());
        }
        GlassButtonChange.BeforeClick = "clickChange";
        GlassButtonAccess.BeforeClick = "clickAccess";
        GlassButtonVoid.BeforeClick = "clickVoid";

        bool isAddNew = base.isNew();
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string userGUID = (string)Session["UserGUID"];
        bool isAdmin = base.isTypeAdmin(engine, objects.getData("DocTypeGUID"), userGUID);
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string sql = null;
        string[,] ids = null;
        DataSet ds = null;
        int count = 0;

        //儲存按鈕
        SaveButton.Display = isAdmin;

        //文件號碼
        DocGUID.clientEngineType = engineType;
        DocGUID.connectDBString = connectString;
        DocGUID.ReadOnly = true;
        DocGUID.HiddenButton = true;

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
                sql = "select r.GUID, r. RevNumber, r.Released, r.SheetNo from SmpRev r left join SMWYAAA a on a.SMWYAAA019=r.FormGUID where r.DocGUID='"+docGUID+"' and isnull(a.SMWYAAA020, 'Y') != 'N' order by r.RevNumber desc, r.ReleaseDate desc";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables.Count > 0)
                {
                    count = ds.Tables[0].Rows.Count;
                    ids = new string[count, 2];
                    for (int i = 0; i < count; i++)
                    {
                        string guid = ds.Tables[0].Rows[i][0].ToString();
                        string revNumber = ds.Tables[0].Rows[i][1].ToString();
                        string released = ds.Tables[0].Rows[i][2].ToString();
                        string sheetNo = ds.Tables[0].Rows[i][3].ToString();

                        if (released.Equals("Y"))
                        {
                            revNumber = revNumber + " " + sheetNo;
                        }
                        else
                        {
                            revNumber = "(" + revNumber + ") " + sheetNo;
                            SaveButton.ReadOnly = true;
                            SaveButton.Text = "儲存 (變更中不能異動)";
                            //SaveButton.BorderColor = System.Drawing.Color.Red;
                            isAdmin = false;
                        }

                        ids[i, 0] = guid;
                        ids[i, 1] = revNumber;
                    }
                    RevGUID.setListItem(ids);
                    sql = "select GUID from SmpRev where DocGUID='" + objects.getData("DocGUID") + "' and LatestFlag='Y' and Released='Y'";
                    string revGUID = (string)engine.executeScalar(sql);
                    RevGUID.ValueText = revGUID;
                }
            }
        }
        RevGUID.setListItem(ids);

        //狀態
        ids = new string[,]{
                //{"",""},
                {"Create","新增中"},
                {"Closed","已結案"},
                {"Review","變更中"},
                {"Cancel","作廢中"},
                {"Cancelled","已作廢"},
                {"Rject","退件"}
            };
        Status.setListItem(ids);
        Status.ReadOnly = true;

        //文件名稱
        Name.ReadOnly = !isAdmin;

        //主分類
        MajorTypeGUID.clientEngineType = engineType;
        MajorTypeGUID.connectDBString = connectString;
        MajorTypeGUID.ReadOnly = true;

        //子分類
        SubTypeGUID.clientEngineType = engineType;
        SubTypeGUID.connectDBString = connectString;
        SubTypeGUID.ReadOnly = true;

        //文件類別
        DocTypeGUID.clientEngineType = engineType;
        DocTypeGUID.connectDBString = connectString;
        DocTypeGUID.ReadOnly = true;

        //外來文件
        ids = new string[,]{
                {"N","否"},
                {"Y","是"},
            };
        External.setListItem(ids);
        External.ReadOnly = true;

        //廠區
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
        DocPropertyGUID.ReadOnly = !isAdmin;

        //機密等級
        ids = new string[,]{
                //{"",""},
                {"0","一般"},
                {"1","機密"},
                {"2","極機密"}
            };
        ConfidentialLevel.setListItem(ids);
        ConfidentialLevel.ReadOnly = !isAdmin;

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

        //文件摘要
        Abstract.ReadOnly = !isAdmin;

        //關鍵字
        KeyWords.ReadOnly = !isAdmin;

        //生效日
        EffectiveDate.ReadOnly = true;

        //失效日
        ExpiryDate.ReadOnly = !isAdmin;

        //歸屬群組
        DocBelongGroupGUID.clientEngineType = engineType;
        DocBelongGroupGUID.connectDBString = connectString;
        LblDocBelongGroup.Display = false;
        DocBelongGroupGUID.Display = false;

        //歸屬群組清單
        DataObjectSet groupSet = null;
        if (isAddNew)
        {
            groupSet = new DataObjectSet();
            groupSet.isNameLess = true;
            groupSet.setAssemblyName("WebServerProject");
            groupSet.setChildClassString("WebServerProject.form.SPKM005.SmpDocBelongGroup");
            groupSet.setTableName("SmpDocBelongGroup");
            groupSet.loadFileSchema();
            objects.setChild("SmpDocBelongGroup", groupSet);
        }
        else
        {
            groupSet = objects.getChild("SmpDocBelongGroup");
        }
        DataListDocBelongGroup.dataSource = groupSet;
        DataListDocBelongGroup.HiddenField = new string[] { "GUID", "DocGUID", "RevGUID", "BelongGroupType", "BelongGroupGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS", "D_INSERTUSER", "D_INSERTTIME", "D_MODIFYUSER", "D_MODIFYTIME" };
        DataListDocBelongGroup.reSortCondition("建立時間", DataObjectConstants.ASC);
        DataListDocBelongGroup.updateTable();
        DataListDocBelongGroup.NoDelete = true;
        DataListDocBelongGroup.NoAdd = true;
        DataListDocBelongGroup.NoModify = true;
        DataListDocBelongGroup.IsShowCheckBox = false;

        //上傳檔案元件初始化
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string fileAdapter = sp.getParam("FileAdapter");
        FileUploadAtta.FileAdapter = fileAdapter;
        IOFactory factory = new IOFactory();
        AbstractEngine fileEngine = factory.getEngine(engineType, connectString);
        FileUploadAtta.engine = fileEngine;
        FileUploadAtta.tempFolder = Server.MapPath("~/tempFolder");
        FileUploadAtta.readFile("");
        FileUploadAtta.Display = true;
        ButtonUpload.Display = false;

        //附件清單
        DataObjectSet attachmentSet = null;
        if (isAddNew)
        {
            attachmentSet = new DataObjectSet();
            attachmentSet.isNameLess = true;
            attachmentSet.setAssemblyName("WebServerProject");
            attachmentSet.setChildClassString("WebServerProject.form.SPKM005.SmpAttachment");
            attachmentSet.setTableName("SmpAttachment");
            attachmentSet.loadFileSchema();
            objects.setChild("SmpAttachment", attachmentSet);
        }
        else
        {
            attachmentSet = objects.getChild("SmpAttachment");
        }
        DataListAttachment.dataSource = attachmentSet;
        DataListAttachment.HiddenField = new string[] { "GUID", "FormGUID", "SheetNo", "DocGUID", "RevGUID", "FileItemGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY", "Processed", "LatestFlag", "D_INSERTUSER", "D_INSERTTIME", "D_MODIFYUSER", "D_MODIFYTIME" };
        DataListAttachment.FormTitle = "文件檔案";
        DataListAttachment.WidthMode = 1;
        DataListAttachment.IsMaintain = false;
        DataListAttachment.IsGeneralUse = false;
        DataListAttachment.IsPanelWindow = false;
        DataListAttachment.DialogWidth = 1;
        DataListAttachment.DialogHeight = 1;
        DataListAttachment.NoAdd = true;
        DataListAttachment.InputForm = "DownloadFile.aspx";
        DataListAttachment.reSortCondition("上傳時間", DataObjectConstants.ASC);
        DataListAttachment.updateTable();
        DataListAttachment.ReadOnly = true;

        //關聯文件
        ids = new string[,]{
                {"",""},
                {"KM","KM"},
            };
        Source.setListItem(ids);

        ReferenceGUID.clientEngineType = engineType;
        ReferenceGUID.connectDBString = connectString;
        LblSource.Display = isAdmin;
        Source.Display = isAdmin;
        LblReferenceGUID.Display = isAdmin;
        ReferenceGUID.Display = isAdmin;

        //參考文件清單
        DataObjectSet referenceSet = null;
        if (isAddNew)
        {
            referenceSet = new DataObjectSet();
            referenceSet.isNameLess = true;
            referenceSet.setAssemblyName("WebServerProject");
            referenceSet.setChildClassString("WebServerProject.form.SPKM005.SmpReference");
            referenceSet.setTableName("SmpReference");
            referenceSet.loadFileSchema();
            objects.setChild("SmpReference", referenceSet);
        }
        else
        {
            referenceSet = objects.getChild("SmpReference");
        }
        DataListReference.dataSource = referenceSet;
        DataListReference.HiddenField = new string[] { "GUID", "DocGUID", "RevGUID", "Reference", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY", "ReferenceGUID", "DATA_STATUS", "D_INSERTUSER", "D_INSERTTIME", "D_MODIFYUSER", "D_MODIFYTIME" };
        //DataListReference.FormTitle = "參考文件";
        DataListReference.WidthMode = 1;
        //DataListReference.IsMaintain = false;
        //DataListReference.IsGeneralUse = false;
        //DataListReference.IsPanelWindow = true;
        //DataListReference.InputForm = "Reference.aspx";
        DataListReference.reSortCondition("建立時間", DataObjectConstants.ASC);
        DataListReference.updateTable();
        DataListReference.NoAdd = !isAdmin;
        DataListReference.NoDelete = !isAdmin;
        DataListReference.NoModify = !isAdmin;
        DataListReference.IsShowCheckBox = !isAdmin;

        //對象
        ids = new string[,]{
                {"",""},
                {"1","人員"},
                {"2","群組"}
                //{"9","部門"},
                //{"21","群組"}
            };
        ReaderBelongGroupType.setListItem(ids);
        LblReaderBelongGroupType.Display = isAdmin;
        ReaderBelongGroupType.Display = isAdmin;
        LblBelongGroup.Display = isAdmin;
        ReaderBelongGroupGUID.Display = isAdmin;

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
            readerSet.setChildClassString("WebServerProject.form.SPKM005.SmpReader");
            readerSet.setTableName("SmpReader");
            readerSet.loadFileSchema();
            objects.setChild("SmpReader", readerSet);
        }
        else
        {
            readerSet = objects.getChild("SmpReader");
        }
        DataListReader.dataSource = readerSet;
        DataListReader.HiddenField = new string[] { "GUID", "DocGUID", "RevGUID", "BelongGroupType", "BelongGroupGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS", "D_INSERTUSER", "D_INSERTTIME", "D_MODIFYUSER", "D_MODIFYTIME", "EffectiveDate", "ExpiryDate" };
        DataListReader.reSortCondition("建立時間", DataObjectConstants.ASC);
        DataListReader.updateTable();
        DataListReader.NoAdd = !isAdmin;
        DataListReader.NoDelete = !isAdmin;
        DataListReader.NoModify = !isAdmin;
        DataListReader.IsShowCheckBox = isAdmin;

        //調閱記錄清單
        DataObjectSet accessSet = null;
        if (isAddNew)
        {
            accessSet = new DataObjectSet();
            accessSet.isNameLess = true;
            accessSet.setAssemblyName("WebServerProject");
            accessSet.setChildClassString("WebServerProject.form.SPKM005.SmpAccess");
            accessSet.setTableName("SmpAccess");
            accessSet.loadFileSchema();
            objects.setChild("SmpAccess", accessSet);
        }
        else
        {
            accessSet = objects.getChild("SmpAccess");
        }
        DataListAccess.dataSource = accessSet;
        DataListAccess.HiddenField = new string[] { "GUID", "DocGUID", "RevGUID", "BelongGroupType", "BelongGroupGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS", "D_INSERTUSER", "D_INSERTTIME", "D_MODIFYUSER", "D_MODIFYTIME" };
        DataListAccess.reSortCondition("建立時間", DataObjectConstants.ASC);
        DataListAccess.updateTable();
        DataListAccess.ReadOnly = true;
        DataListAccess.IsHideSelectAllButton = true;

        //事件記錄
        DataObjectSet historySet = null;
        if (isAddNew)
        {
            historySet = new DataObjectSet();
            historySet.isNameLess = true;
            historySet.setAssemblyName("WebServerProject");
            historySet.setChildClassString("WebServerProject.form.SPKM005.SmpHistory");
            historySet.setTableName("SmpHistory");
            historySet.loadFileSchema();
            objects.setChild("SmpHistory", historySet);
        }
        else
        {
            historySet = objects.getChild("SmpHistory");
        }
        DataListHistory.dataSource = historySet;
        DataListHistory.HiddenField = new string[] { "GUID", "DocGUID", "RevGUID", "FormGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS", "D_INSERTUSER", "D_INSERTTIME", "D_MODIFYUSER", "D_MODIFYTIME" };
        DataListHistory.reSortCondition("建立時間", DataObjectConstants.DESC);
        DataListHistory.updateTable();
        DataListHistory.ReadOnly = true;
        DataListHistory.IsHideSelectAllButton = true;

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
        }

        //文件編號
        DocGUID.GuidValueText = objects.getData("DocGUID");
        DocGUID.doGUIDValidate();
        //版本
        RevGUID.ValueText = objects.getData("RevGUID");
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
        //文件類別
        DocTypeGUID.GuidValueText = objects.getData("DocTypeGUID");
        DocTypeGUID.doGUIDValidate();
        //外來文件
        External.ValueText = objects.getData("External");
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

        NLAgent agent1 = null;
        DataObjectSet dataSet = null;
        //重新設定關聯文件資料(從objects取出之資料Reader GUID會一直變, 懷疑是GUID更換為RevGUID的影響)
        agent1 = new NLAgent();
        agent1.loadSchema("WebServerProject.form.SPKM005.SmpReferenceAgent");
        agent1.engine = engine;
        agent1.query("(DocGUID='" + DocGUID.GuidValueText + "')");
        dataSet = agent1.defaultData;
        for (int i = 0; i < dataSet.getAvailableDataObjectCount(); i++)
        {
            DataObject obj = dataSet.getDataObject(i);
            string source = obj.getData("Source");
            string reference = obj.getData("Reference");
            string docNum = obj.getData("DocNumber");
            string href = "Reference.aspx";
            href += "?Source=" + source;
            href += "&Reference=" + reference;
            string docUrl = "{[a href=\"javascript:clickViewReference('" + href + "');\"]}" + docNum + "{[/a]}";
            obj.setData("DocNumber", docUrl);
            DataListReference.setColumnStyle("DocNumber", 120, DSCWebControl.GridColumnStyle.LEFT);
        }
        DataListReference.dataSource = dataSet;
        DataListReference.updateTable();

        //重新設定讀者資料
        agent1 = new NLAgent();
        agent1.loadSchema("WebServerProject.form.SPKM005.SmpReaderAgent");
        agent1.engine = engine;
        agent1.query("(DocGUID='" + DocGUID.GuidValueText + "')");
        dataSet = agent1.defaultData;
        DataListReader.dataSource = dataSet;
        DataListReader.updateTable();
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
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            bool isAddNew = base.isNew();
            string actName = (String)getSession("ACTName");
            string connectString = (string)Session["connectString"];
            IOFactory factory = new IOFactory();

            engine1 = factory.getEngine(EngineConstants.SQL, connectString);

            //儲存索引卡
            string indexCardGUID = objects.getData("IndexCardGUID");
            agent1 = new NLAgent();
            agent1.loadSchema("WebServerProject.form.SPKM005.SmpIndexCardAgent");
            agent1.engine = engine1;
            agent1.query("GUID='" + indexCardGUID + "'");
            set = agent1.defaultData;
            DataObject obj = set.getAvailableDataObject(0);
            //obj.setData("MajorTypeGUID", MajorTypeGUID.GuidValueText);
            //obj.setData("SubTypeGUID", SubTypeGUID.GuidValueText);
            //obj.setData("DocTypeGUID", DocTypeGUID.GuidValueText);
            obj.setData("DocPropertyGUID", DocPropertyGUID.ValueText);
            obj.setData("ConfidentialLevel", ConfidentialLevel.ValueText);
            obj.setData("Name", Name.ValueText);
            obj.setData("Abstract", Abstract.ValueText);
            obj.setData("KeyWords", KeyWords.ValueText);
            obj.setData("ExpiryDate", ExpiryDate.ValueText);
            //obj.setData("External", External.ValueText);
            agent1.defaultData = set;
            if (!agent1.update())
            {
                throw new Exception(engine1.errorString);
            }

            //儲存關聯文件
            for (int i = 0; i < DataListReference.dataSource.getAvailableDataObjectCount(); i++)
            {
                DataListReference.dataSource.getAvailableDataObject(i).setData("DocGUID", objects.getData("DocGUID"));
                DataListReference.dataSource.getAvailableDataObject(i).setData("RevGUID", objects.getData("RevGUID"));
            }
            agent1 = new NLAgent();
            agent1.loadSchema("WebServerProject.form.SPKM005.SmpReferenceAgent");
            agent1.engine = engine1;
            agent1.query("(1=2)");
            agent1.defaultData = DataListReference.dataSource;
            if (!agent1.update())
            {
                throw new Exception(engine1.errorString);
            }

            //儲存讀者
            for (int i = 0; i < DataListReader.dataSource.getAvailableDataObjectCount(); i++)
            {
                DataObject dataObject = DataListReader.dataSource.getAvailableDataObject(i);
                dataObject.setData("DocGUID", objects.getData("DocGUID"));
                dataObject.setData("RevGUID", objects.getData("RevGUID"));
            }
            agent1 = new NLAgent();
            agent1.loadSchema("WebServerProject.form.SPKM005.SmpReaderAgent");
            agent1.engine = engine1;
            agent1.query("1=2");
            agent1.defaultData = DataListReader.dataSource;
            if (!agent1.update())
            {
                throw new Exception(engine1.errorString);
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

    protected override void saveProcedure()
    {
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            engine.startTransaction(IsolationLevel.ReadCommitted);

            com.dsc.kernal.databean.DataObject currentObject = (com.dsc.kernal.databean.DataObject)getSession("currentObject");

            bool ans = checkFieldData(engine, currentObject);

            if (!ans)
            {
                throw new Exception(showErrorMessage());
            }
            saveData(engine, currentObject);
            MessageBox("儲存成功!");
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
            }
            catch { };
            //if (ue.Message.Equals("畫面欄位填寫錯誤"))
            if (ue.Message.Equals(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError14", "畫面欄位填寫錯誤")))
            {
                processErrorMessage(0, ue);
            }
            else
            {
                processErrorMessage(errorLevel, ue);
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
        bool result = true;
        string errMsg = "";
        if (Name.ValueText.Equals(""))
        {
            errMsg += "[" + LblName.Text + "]欄位必需有值!\n";
        }
        //if (MajorTypeGUID.ValueText.Equals(""))
        //{
        //    errMsg += "[" + LblMajorTypeGUID.Text + "]欄位必需有值!\n";
        //}
        //if (SubTypeGUID.ValueText.Equals(""))
        //{
        //    errMsg += "[" + LblSubTypeGUID.Text + "]欄位必需有值!\n";
        //}
        //if (DocTypeGUID.ValueText.Equals(""))
        //{
        //    errMsg += "[" + LblDocTypeGUID.Text + "]欄位必需有值!\n";
        //}
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

        //if (DataListDocBelongGroup.dataSource.getAvailableDataObjectCount() == 0)
        //{
        //    errMsg += "[歸屬群組] 筆數不得為0!\n";
        //}

        //DataObjectSet setAtta = DataListAttachment.dataSource;
        //int attaCount = setAtta.getAvailableDataObjectCount();
        //if (attaCount == 0)
        //{
        //    errMsg += "[附件檔案筆數不得為0!\n";
        //}
        //else
        //{
        //    string external = External.ValueText;
        //    if (external.Equals("Y"))
        //    {
        //        bool isGotAttaExternal = false;
        //        for (int i = 0; i < attaCount; i++)
        //        {
        //            DataObject data = setAtta.getAvailableDataObject(i);
        //            if (data.getData("External").Equals("Y"))
        //            {
        //                isGotAttaExternal = true;
        //                break;
        //            }
        //        }
        //        if (isGotAttaExternal == false)
        //        {
        //            errMsg += "此文件設定為包含外來文件, 附件清單必須包含外來文件!\n";
        //        }
        //    }
        //}

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
        si.ownerID = (string)Session["UserID"];
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
        return "";
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
        string actName = Convert.ToString(getSession("ACTName"));
        if (actName.Equals("文管中心"))
        {
            DataObjectSet attachmentSet = DataListAttachment.dataSource;
            string result = printOutPdf(engine, attachmentSet);
            if (!result.Equals(""))
            {
                throw new Exception(result);
            }
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
        FileUploadAtta.openFileUploadDialog();
    }

    /// <summary>
    /// 點選附件清單導向PDF Viewer
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isAddNew"></param>
    /// <returns></returns>
    protected bool Atta_BeforeOpenWindows(DataObject objects, bool isAddNew)
    {
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            //此文件的管理者/歸屬群組/讀者才可以點選附件清單
            bool isCanView = false;
            string userGUID = (string)Session["UserGUID"];
            string attachmentType = objects.getData("AttachmentType");
            string latestFlag = objects.getData("LatestFlag");
            string docTypeGUID = DocTypeGUID.GuidValueText;
            string revGUID = RevGUID.ValueText;
            string status = Status.ValueText;
            string docGUID = DocGUID.GuidValueText;
            bool isTypeAdmin = base.isTypeAdmin(engine, docTypeGUID, userGUID);
            bool isBelongGroup = base.isBelongGroup(engine, revGUID, userGUID);
            bool isReader = base.isReader(engine, revGUID, userGUID);
            bool isAccesser = base.isAccesser(engine, docGUID, userGUID);
            bool isAuthor = false;
            if (AuthorGUID.GuidValueText.Equals((string)Session["UserGUID"]))
            {
                isAuthor = true;
            }

            if (attachmentType.Equals("Publish"))
            {
                if (isTypeAdmin || isBelongGroup || isAuthor)
                {
                    isCanView = true;
                }
                else
                {
                    if (latestFlag.Equals("Y"))
                    {
                        if (isReader || isAccesser)
                        {
                            isCanView = true;
                        }
                        else
                        {
                            MessageBox("此文件的管理者/歸屬群組/作者/讀者/調閱人員才可以閱讀檔案!");
                        }
                    }
                    else
                    {
                        MessageBox("只有管理者/歸屬群組/作者才可以閱讀舊版本附件!");
                    }
                }

                if (status.Equals("Cancelled") && isCanView)
                {
                    if (isTypeAdmin == false)
                    {
                        MessageBox("只有管理者才可以閱讀已作廢版本附件!");
                        isCanView = false;
                    }
                }
            }
            else
            {
                MessageBox("只有發佈檔才能閱讀!");
            }
            
            if (isCanView)
            {
                //setSession((string)Session["UserID"], "DownFile", objects);
                string fileName = objects.getData("FILENAME");
                string fileExt = objects.getData("FILEEXT");
                if (!fileExt.Equals(""))
                {
                    fileName = fileName + "." + fileExt;
                }
                //string external = External.ValueText;
                string external = objects.getData("External");
                string level1 = "EKM";
                string level2 = objects.getData("SheetNo");
                string fileGUID = objects.getData("FileItemGUID");
                string destpath = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "filepath");
                string sql = "SELECT LEVEL1, LEVEL2 FROM FILEITEM where GUID='" + fileGUID + "'";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    level1 = ds.Tables[0].Rows[0][0].ToString();
                    level2 = ds.Tables[0].Rows[0][1].ToString();
                    destpath += level1 + "\\" + level2 + "\\";
                    destpath += fileGUID;
                    destpath = destpath.Replace("\\", "\\\\");
                }

                Session["PDFViewerFile"] = destpath;
                Session["External"] = external;
                //DataListAttachment.InputForm = "../../Maintain/PDFViewer/ViewDocument.aspx";

                DataListAttachment.InputForm = "ViewPublish.aspx";

                //新增文件歷史記錄
                string histogyGUID = IDProcessor.getID("");
                string formGUID = objects.getData("FormGUID");
                string description = "檔案名稱: " + fileName;
                string now = DateTimeUtility.getSystemTime2(null);
                sql = "insert into SmpHistory (GUID, DocGUID, Action, Description, RevGUID, FormGUID, IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) "
                    + "select '" + histogyGUID + "' GUID, '" + docGUID + "' DocGUID, '檢視檔案' Action, '" + description + "' Description, '" + revGUID + "' RevGUID, '" + formGUID + "' FormGUID, "
                    + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + userGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME from SmpRev where GUID='" + revGUID + "'";
                if (!engine.executeSQL(sql))
                {
                    MessageBox(engine.errorString);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
        }
        return true;
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

        DataObjectSet readerSet = DataListReader.dataSource;
        if (readerSet.getAvailableDataObjectCount() > 0)
        {
            string[] keys = objects.keyField;
            objects.keyField = new string[] { "RevGUID", "BelongGroupGUID" };

            if (!readerSet.checkData(objects))
            {
                MessageBox("資料重覆!");
                objects.keyField = keys;
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 過濾主分類
    /// </summary>
    protected void MajorTypeGUID_BeforeClickButton()
    {
        MajorTypeGUID.whereClause = "(Enable='Y')";
    }

    /// <summary>
    /// 過濾子分類
    /// </summary>
    protected void SubTypeGUID_BeforeClickButton()
    {
        string majorTypeGUID = MajorTypeGUID.GuidValueText;
        SubTypeGUID.whereClause = "(MajorTypeGUID='" + majorTypeGUID + "' and Enable='Y')";
    }

    /// <summary>
    /// 過濾文件類別
    /// </summary>
    protected void DocTypeGUID_BeforeClickButton()
    {
        string majorTypeGUID = MajorTypeGUID.GuidValueText;
        string subTypeGUID = SubTypeGUID.GuidValueText;
        DocTypeGUID.whereClause = "(MajorTypeGUID='" + majorTypeGUID + "' and SubTypeGUID='" + subTypeGUID + "' and Enable='Y')";
    }

    /// <summary>
    /// 開啟參考文件前設定
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isAddNew"></param>
    /// <returns></returns>
    protected bool Reference_BeforeOpenWindow(DataObject objects, bool isAddNew)
    {
        setSession((string)Session["UserID"], "Reference", objects);
        return true;
    }

    /// <summary>
    /// 讀取版本變更重新取得資料顯示
    /// </summary>
    /// <param name="value"></param>
    protected void RevGUID_SelectChanged(string value)
    {
        string revGUID = value;
        AbstractEngine engine = null;

        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            //string sql = "select a.SMWYAAA001 from SMWYAAA a, SmpRev r where r.GUID='" + revGUID + "' and  a.SMWYAAA019=r.FormGUID";
            //string objectGUID = (string)engine.executeScalar(sql);
            string objectGUID = revGUID;
            if (objectGUID != null && !objectGUID.Equals(""))
            {
                DataObject currentObject = readDB(engine, objectGUID, (string)getSession("UIStatus"));
                showData(engine, currentObject);

                DataObjectSet groupSet = currentObject.getChild("SmpDocBelongGroup");
                DataListDocBelongGroup.dataSource = groupSet;
                DataListDocBelongGroup.updateTable();

                DataObjectSet attachmentSet = currentObject.getChild("SmpAttachment");
                DataListAttachment.dataSource = attachmentSet;
                DataListAttachment.updateTable();

                DataObjectSet referenceSet = currentObject.getChild("SmpReference");
                DataListReference.dataSource = referenceSet;
                DataListReference.updateTable();

                DataObjectSet readerSet = currentObject.getChild("SmpReader");
                DataListReader.dataSource = readerSet;
                DataListReader.updateTable();
            }
            else
            {
                MessageBox("此版本找不到表單資料!");
            }

            engine.close();
        }
        catch (Exception te)
        {
            processErrorMessage(errorLevel, te);
        }
    }
}