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

public partial class Program_SCQ_Form_SCQKM01_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "SCQKM01";
        AgentSchema = "WebServerProject.form.SCQKM01.SCQKM01Agent";
        ApplicationID = "SYSTEM";
        ModuleID = "EASYFLOW";
    }

    /// <summary>
    /// 初始化畫面元件。初始化資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        bool isAddNew = base.isNew();
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];


        string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where EmpNo ='" + si.fillerID + "'";

        DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];
        ///--------------------------帶出使用者信息
        EmpNo.clientEngineType = engineType;
        EmpNo.connectDBString = connectString;
        EmpNo.DoEventWhenNoKeyIn = false;
        EmpNo.ValueText = si.fillerID;
        EmpNo.doValidate();
        UpdateData(si.fillerID);
        EmpNo.ReadOnly = true;
        //id.ReadOnly = true;
        bm.ReadOnly = true;

        string[,] a = new string[3, 2] { { "請選擇", "請選擇" }, { "NPI", "NPI" }, { "MP", "MP" } };
        jd.setListItem(a);
        string[,] b = new string[5, 2] { { "請選擇", "請選擇" }, { "機構", "機構" }, { "電性", "電性" }, { "漏液", "漏液" }, { "Other", "Other" } };
        lb.setListItem(b);
        string[,] c = new string[4, 2] { { "請選擇", "請選擇" }, { "廠內", "廠內" }, { "ODM", "ODM" }, { "Field", "Field" } };
        qy.setListItem(c);
        string[,] d = new string[3, 2] { { "請選擇", "請選擇" }, { "Open", "Open" }, { "Close", "Close" }};
        zt.setListItem(d);
        //文件
        DocGUID.clientEngineType = engineType;
        DocGUID.connectDBString = connectString;
        DocGUID.ReadOnly = true;
        bm.ValueText = partNouser.ValueText;


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

        //附件清單        
        DataObjectSet attachmentSet = null;
        if (isAddNew)
        {
            attachmentSet = new DataObjectSet();
            attachmentSet.isNameLess = true;
            attachmentSet.setAssemblyName("WebServerProject");
            attachmentSet.setChildClassString("WebServerProject.form.SCQKM01.SmpAttachment");
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

        //改變工具列順序
        base.initUI(engine, objects);
    }
    private void UpdateData(string id)
    {
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);

            Hashtable h1 = base.getHRUsers(engine, id);
            partNouser.ValueText = h1["PartNo"].ToString();
            partNouser.ReadOnly = true;
            //  Mobile.ValueText = h1["Mobile"].ToString();
            Hashtable h2 = base.getADUserData(engine, id);
            mobileuser.ValueText = h2["telephonenumber"].ToString();
        }
        catch { }
    }

    /// <summary>
    /// 將資料由資料物件填入畫面元件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        //顯示單號
        bool isAddNew = base.isNew();
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        //主旨
        Subject.ValueText = objects.getData("Subject");
        mobileuser.ValueText = objects.getData("mobileuser");
        partNouser.ValueText = objects.getData("partNouser");
        kh.ValueText = objects.getData("kh");
        jz.ValueText = objects.getData("jz");
        bt.ValueText = objects.getData("bt");
        jd.ValueText = objects.getData("jd");
        lb.ValueText = objects.getData("lb");
        qy.ValueText = objects.getData("qy");
        zt.ValueText = objects.getData("zt");
        DocGUID.GuidValueText = objects.getData("DocGUID");
        DocGUID.doGUIDValidate();
        bm.ValueText = objects.getData("bm");

        DataObjectSet attachmentSet = null;

        attachmentSet = objects.getChild("SmpAttachment");
        DataListAttachment.dataSource = attachmentSet;
        DataListAttachment.updateTable();

        //bool isCanDownload = true;
        //if (isCanDownload)
        //{
        //    DataObjectSet dos = DataListAttachment.dataSource;
        //    int a = dos.getAvailableDataObjectCount();
        //    MessageBox(Convert.ToString(a));

        //    for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        //    {
        //        string href = "DownloadFile.aspx";
        //        DataObject obj = dos.getDataObject(i);
        //        string fileName = obj.getData("FILENAME");
        //        string fileExt = obj.getData("FILEEXT");
        //        string sheetNo = obj.getData("SheetNo");
        //        string fileItemGUID = obj.getData("FileItemGUID");
        //        href += "?FILENAME=" + System.Web.HttpUtility.UrlPathEncode(fileName);
        //        href += "&FILEEXT=" + fileExt;
        //        href += "&SheetNo=" + sheetNo;
        //        href += "&FileItemGUID=" + fileItemGUID;
        //        string fileNameUrl = "{[a href=\"" + href + "\"]}" + fileName + "{[/a]}";

        //        obj.setData("FILENAME", fileNameUrl);
        //        //System.IO.StreamWriter sw = null;
        //        //sw = new System.IO.StreamWriter(@"d:\temp\SPKM001.log", true);
        //        //sw.WriteLine("FileURL:" + fileNameUrl);
        //        //sw.Close();
        //        //MessageBox("url:" + fileNameUrl);             
        //    }
        //    // DataListAttachment.dataSource = dos;
        //    DataListAttachment.setColumnStyle("FILENAME", 230, DSCWebControl.GridColumnStyle.LEFT);
        //    DataListAttachment.updateTable();
        //}
        



        base.showData(engine, objects);

        //顯示發起資料

    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        AbstractEngine engine1 = null;
        try
        {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        bool isAddNew = base.isNew();
        string connectString = (string)Session["connectString"];
        IOFactory factory = new IOFactory();
        if (isAddNew)
        {
            string docGUID = IDProcessor.getID("");
            //顯示要Save的資料
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("SheetNo", objects.getData("SheetNo"));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("mobileuser", mobileuser.ValueText);
            objects.setData("partNouser", partNouser.ValueText);
            objects.setData("Uname", si.fillerID);
            objects.setData("CName", si.fillerName);
            objects.setData("kh", kh.ValueText);
            objects.setData("jz", jz.ValueText);
            objects.setData("bt", bt.ValueText);
            objects.setData("jd", jd.ValueText);
            objects.setData("lb", lb.ValueText);
            objects.setData("qy", qy.ValueText);
            objects.setData("zt", zt.ValueText);
            objects.setData("DocGUID", docGUID);
            objects.setData("bm", bm.ValueText);


            
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);
            //儲存附件
            for (int i = 0; i < DataListAttachment.dataSource.getAvailableDataObjectCount(); i++)
            {
                DataListAttachment.dataSource.getAvailableDataObject(i).setData("DocGUID", objects.getData("DocGUID"));
                //DataListAttachment.dataSource.getAvailableDataObject(i).setData("RevGUID", objects.getData("RevGUID"));
            }
            FileUploadAtta.setJobID(objects.getData(getObjectGUIDField()));
            FileUploadAtta.confirmSave("EKM", objects.getData("SheetNo"));
            FileUploadAtta.saveFile();
            FileUploadAtta.engine.close();

        }
        //else
        //{ //把url檔名換掉                
        //    DataObjectSet dos = DataListAttachment.dataSource;
        //    for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        //    {
        //        DataObject obj = dos.getDataObject(i);
        //        //此欄位為non-update field ，不dataObject.getData("FILENAME")
        //        //將filename換回, 避免url過長, 造成saveDB報錯! 
        //        string fileName = obj.getData("FILENAME");
        //        int idxs = fileName.IndexOf("]}");
        //        int idxe = fileName.LastIndexOf("{[");
        //        if (idxs > 0 && idxe > 0)
        //        {
        //            fileName = fileName.Substring(idxs + 2, idxe - (idxs + 2));
        //        }
        //        obj.setData("FILENAME", fileName);
        //    }
        //    DataListAttachment.updateTable();
        //}
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
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【客訴文件申請單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
                // objects.setData("Subject", subject);
            }

        }
        return result;
    }

    /// <summary>
    /// 初始化SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];//填表人
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];//表單關系人
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
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];//填表人
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];//表單關系人
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
    /// <param name="engine">WebFormPT</param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string xml = "";
        xml += "<SCQKM01>";
        xml += "</EM0111>";
        param["SCQKM01"] = xml;
        return "SCQKM01";
    }


    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine">WebFormPT</param>
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
            //string revGUID = currentObject.getData("RevGUID");
            string formGUID = currentObject.getData("GUID");

            //document
            sql = "insert into SmpDocument( GUID, AuthorGUID, AuthorOrgUnitGUID, Site,"
                    + "IS_LOCK, IS_DISPLAY, DATA_STATUS, D_INSERTUSER, D_INSERTTIME ,D_MODIFYUSER, D_MODIFYTIME) SELECT '"
                    + docGUID + "' ,'" + EmpNo.ValueText + "','" + bm.ValueText + "','" + kh.ValueText + "'," 
                    + "'N','Y','Y','" + userGUID + "','" + now + "','','' ";
            if (!engine.executeSQL(sql))
            {                
                throw new Exception(engine.errorString);
            }
                       
           
            

            //Rev
            sql = "insert into SmpRev (GUID, RevNumber, DocGUID, FormGUID, IndexCardGUID, Released, LatestFlag,"
                + "IS_LOCK, IS_DISPLAY, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME,SheetNo) SELECT '"
                + docGUID + "' ,'1','" + docGUID + "','" + formGUID + "','"
                + indexCardGUID + "','N','N','N','Y','Y','" + userGUID + "','" + now + "','','','" + sheetNo + "' ";

            if (!engine.executeSQL(sql))
            {
                throw new Exception(engine.errorString);
            }

                    
            AbstractEngine engine1 = factory.getEngine(engineType, connectString);
            NLAgent agent1 = new NLAgent();
            agent1.loadSchema("WebServerProject.form.SPKM001.SmpIndexCardAgent");
            agent1.engine = engine1;
            agent1.query("1=2");  

            

           
            
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
    /// <param name="engine">WebFormPT</param>
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
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        string subject = currentObject.getData("Subject");
        writeLog("Subject:" + subject);
        writeLog("AfterApprove Result:" + result);


        if (result.Equals("Y"))
        {
            //sw.WriteLine("RESULT IS Y");

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

            ////更新此版本為生效最後一版                            
            //sql = "update SmpRev set Released = 'Y', LatestFlag='Y', ReleaseDate='" + effectiveDate + "', D_MODIFYUSER='" + userGUID + "', D_MODIFYTIME='"
            //    + now + "' where DocGUID='" + docGUID + "' and GUID='" + revGUID + "'";
            //if (!engine.executeSQL(sql))
            //{
            //    throw new Exception(engine.errorString);
            //}

            ////更新索引卡內容狀態為結案
            //sql = "update SmpIndexCard set Status='Closed', EffectiveDate='" + effectiveDate + "' ,D_MODIFYUSER='" + userGUID + "', D_MODIFYTIME='" + now + "' where DocGUID='" + docGUID
            //    + "' and GUID='" + indexCardGUID + "'";
            //if (!engine.executeSQL(sql))
            //{
            //    throw new Exception(engine.errorString);
            //}

          
        }
        
        base.afterApprove(engine, currentObject, result);
    }
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
    protected void Upload_Click(object sender, EventArgs e)
    {
        Session["isKM"] = "Y"; //為了判斷是KM表單，FileUpload裡會判斷檔案上傳格式
        FileUploadAtta.openFileUploadDialog();
    }
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
    /// 儲存附件列資料, External文件檢查
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    /// <returns></returns>
    protected bool DataListAttachment_SaveRowData(DataObject objects, bool isNew)
    {
        
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

        //setDownload(objects);

    }
    private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string line = DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPKM001.log", true, System.Text.Encoding.Default);
            sw.WriteLine(line);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            if (sw != null)
            {
                sw.Close();
            }
        }
    }
}
