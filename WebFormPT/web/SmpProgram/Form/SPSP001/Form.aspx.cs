using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
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
//using Microsoft.Practices.ServiceLocation;

using iTextSharp.text;
using iTextSharp.text.pdf;

//using SolrNet;
//using SolrNet.Attributes;
//using SolrNet.Commands;
//using SolrNet.Commands.Parameters;

public partial class SmpProgram_Form_SPSP001_Form : SmpKmFormPage
{

    /// <summary>
    /// 初始化參數。
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPSP001";
        AgentSchema = "WebServerProject.form.SPSP001.SmpSampleFormAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPSP";
    }

    /// <summary>
    /// 初始化畫面元件。初始化資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        //indexFile();
        //deleteIndexingById();
        ConvertToPdf();

        string userId = (string)Session["UserID"];
        //string whereClause = "DocTypeGUID = '36aced83-3cec-42d3-b81e-1bb340709181'";
        //Session[userId+"_SPKM005_Maintain_whereClause"] =  whereClause;
        //Response.Redirect("../SPKM005/Maintain.aspx");
        
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        //申請人
        OriginatorGUID.clientEngineType = engineType;
        OriginatorGUID.connectDBString = connectString;
        if (OriginatorGUID.ValueText.Equals(""))
        {
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }

        bool isAddNew = base.isNew();
        DataObjectSet detailSet = null;
        if (isAddNew)
        {
            detailSet = new DataObjectSet();
            detailSet.isNameLess = true;
            detailSet.setAssemblyName("WebServerProject");
            detailSet.setChildClassString("WebServerProject.form.SPSP001.SmpSampleFile");
            detailSet.setTableName("SmpSampleFile");
            detailSet.loadFileSchema();
            objects.setChild("SmpSampleFile", detailSet);
        }
        else
        {
            detailSet = objects.getChild("SmpSampleFile");
        }
        DataListAttachment.dataSource = detailSet;
        DataListAttachment.HiddenField = new string[] { "GUID", "SampleGUID", "FileItemGUID", "SheetNo", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        DataListAttachment.FormTitle = "文件檔案";
        DataListAttachment.WidthMode = 1;
        DataListAttachment.IsMaintain = false;
        DataListAttachment.IsGeneralUse = false;
        DataListAttachment.IsPanelWindow = true;
        DataListAttachment.NoAdd = true;
        DataListAttachment.InputForm = "DownloadFile.aspx";
        DataListAttachment.updateTable();


        //改變工具列順序
        base.initUI(engine, objects);

        //上傳檔案
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string fileAdapter = sp.getParam("FileAdapter");
        FileUploadAtta.FileAdapter = fileAdapter;
        IOFactory factory = new IOFactory();
        AbstractEngine fileEngine = factory.getEngine(engineType, connectString);
        FileUploadAtta.engine = fileEngine;
        //FileUploadAtta.engine = engine;
        //FileUploadAtta.setJobID((string)Session["UserGUID"]);
        FileUploadAtta.tempFolder = Server.MapPath("~/tempFolder");
        FileUploadAtta.maxLength = 10485760;
        FileUploadAtta.readFile("");
        FileUploadAtta.Display = true;
    }


    /// <summary>
    /// 將資料由資料物件填入畫面元件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        bool isAddNew = base.isNew();
        //附件檔案加入文件檔案清單
        if (isAddNew)
        {
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
                    obj.setData("FILENAME", fileDataObject.getData("FILENAME"));
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
        //顯示單號
        base.showData(engine, objects);
        //申請人
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID

        string actName = Convert.ToString(getSession("ACTName"));
        if (actName == "" || actName.Equals("填表人"))
        {

        }
        else
        {
            //表單發起後不允許修改
            Subject.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;

            //附件清單不能修改
            DataListAttachment.NoDelete = true;
            DataListAttachment.NoAdd = true;
            DataListAttachment.NoModify = true;
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
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);
        }

        string actName = (String)getSession("ACTName");
        if (!actName.Equals("直屬主管"))
        {
            setSession("IsAddSign", "AFTER"); //beforeSign 加簽
        }

        for (int i = 0; i < DataListAttachment.dataSource.getAvailableDataObjectCount(); i++)
        {
            DataListAttachment.dataSource.getAvailableDataObject(i).setData("SampleGUID", objects.getData("GUID"));
        }

        FileUploadAtta.setJobID(objects.getData(getObjectGUIDField()));
        FileUploadAtta.confirmSave("EKM", objects.getData("SheetNo"));
        FileUploadAtta.saveFile();
        FileUploadAtta.engine.close();
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
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        base.afterApprove(engine, currentObject, result);
    }

    protected void Attachment_DataBinding(object sender, EventArgs e)
    {
        //TableAttachment.Controls.Clear();

        //TableRow rowNew = new TableRow();
        //TableAttachment.Controls.Add(rowNew);
        //TableCell cellNew = null;
        //System.Web.UI.WebControls.Label lblNew = null;

        //lblNew = new System.Web.UI.WebControls.Label();
        //lblNew.Text = "檔案名稱";
        //cellNew = new TableCell();
        //cellNew.Controls.Add(lblNew);
        //rowNew.Controls.Add(cellNew);

        //lblNew = new System.Web.UI.WebControls.Label();
        //lblNew.Text = "檔案說明";
        //cellNew = new TableCell();
        //cellNew.Controls.Add(lblNew);
        //rowNew.Controls.Add(cellNew);

        //lblNew = new System.Web.UI.WebControls.Label();
        //lblNew.Text = "檔案類別";
        //cellNew = new TableCell();
        //cellNew.Controls.Add(lblNew);
        //rowNew.Controls.Add(cellNew);

        //int rows = 5;
        //int cols = 3;
        //for (int i = 0; i < rows; i++)
        //{
        //    rowNew = new TableRow();
        //    TableAttachment.Controls.Add(rowNew);
        //    for (int j = 0; j < cols; j++)
        //    {
        //        cellNew = new TableCell();
        //        lblNew = new System.Web.UI.WebControls.Label();
        //        lblNew.Text = "(" + i.ToString() + "," + j.ToString() + ")<br />";

        //        System.Web.UI.WebControls.Image imgNew = new System.Web.UI.WebControls.Image();
        //        cellNew.Controls.Add(lblNew);
        //        cellNew.BorderStyle = BorderStyle.Inset;
        //        cellNew.BorderWidth = Unit.Pixel(1);

        //        rowNew.Controls.Add(cellNew);
        //    }
        //}
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
            obj.setData("SampleGUID", "TEMP");
            obj.setData("FileItemGUID", currentObject.GUID);
            obj.setData("FILENAME", currentObject.FILENAME);
            obj.setData("FILEEXT", currentObject.FILEEXT);
            obj.setData("AttachmentTypeId", "O");
            obj.setData("DESCRIPTION", currentObject.DESCRIPTION);
            obj.setData("UPLOADUSER", si.fillerName);
            obj.setData("UPLOADTIME", currentObject.UPLOADTIME);
            obj.setData("SheetNo", (string)getSession(PageUniqueID, "SheetNo"));
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

    protected void Upload_Click(object sender, EventArgs e)
    {
        //maxLength
        FileUploadAtta.openFileUploadDialog();
        Session["isKM"] = "Y";
    }

    protected void DataListAttachment_ShowRowData(DataObject objects)
    {
        //FileStream fs = new FileStream(Server.MapPath("~/tempFolder/EF_發起請假單_預設.xls"), FileMode.Open);
        //byte[] file = new byte[fs.Length];
        //fs.Read(file, 0, file.Length);
        //fs.Close();
        //Response.Clear();
        //Response.AddHeader("content-disposition", "inline; filename=excel2003.xls");
        //Response.ContentType = "application/ms-excel";
        //Response.BinaryWrite(file);  
    }

    protected bool Atta_BeforeOpenWindows(DataObject objects, bool isAddNew)
    {
        setSession((string)Session["UserID"], "DownFile", objects);
        return true;
    }

    private void ConvertToPdf()
    {
        //轉PDF
        string fileName = Server.MapPath("~/tempFolder") + "\\test.xlsx";
        string pdfFileName = @"d:\temp\test1.pdf";
        XlsConvert(fileName, pdfFileName);
        //KillProcess("POWERPNT");
        fileName = Server.MapPath("~/tempFolder") + "\\test.pptx";
        pdfFileName = @"d:\temp\test2.pdf";
        PptConvert(fileName, pdfFileName);

        fileName = Server.MapPath("~/tempFolder") + "\\test.docx";
        pdfFileName = @"d:\temp\test3.pdf";
        DocConvert(fileName, pdfFileName);

        //加浮水印
        //int frequency = 0;
        //while (true)
        //{
        //    Thread.Sleep(500);
        //    if (System.IO.File.Exists(@"D:\Temp\print_pdf\test.pdf"))
        //    {
        //        AddWatermark(@"D:\Temp\print_pdf\test.pdf", @"D:\Temp\pdf_watermark\test.pdf", @"D:\Temp\pdf_watermark\watermark.gif");
        //        break;
        //    }
        //    else
        //    {
        //        frequency++;
        //    }

        //    if (frequency > 120)
        //    {
        //        break;
        //    }
        //}

        //DataSet ds = new DataSet();
    }

    private void createSolrCmd(AbstractEngine engine) {
        string xmlFile = Server.MapPath("~/tempFolder") + "\\SmpDocIndexCardV.xml";
        System.IO.StreamWriter sw1 = new System.IO.StreamWriter(xmlFile, true, System.Text.Encoding.UTF8);

        string sql = "select * from SmpDocIndexCardV where LatestFlag='Y'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;

        sw1.WriteLine("<add>");
        for (int i = 0; i < rows; i++)
        {
            string id = ds.Tables[0].Rows[i]["GUID"].ToString();
            string docNumber = ds.Tables[0].Rows[i]["DocNumber"].ToString();
            string name = ds.Tables[0].Rows[i]["Name"].ToString();
            string revGUID = ds.Tables[0].Rows[i]["RevGUID"].ToString();
            string revNumber = ds.Tables[0].Rows[i]["RevNumber"].ToString();
            string releaseDate = ds.Tables[0].Rows[i]["ReleaseDate"].ToString();
            string status = ds.Tables[0].Rows[i]["Status"].ToString();
            string majorTypeName = ds.Tables[0].Rows[i]["MajorTypeName"].ToString();
            string subTypeName = ds.Tables[0].Rows[i]["SubTypeName"].ToString();
            string docTypeName = ds.Tables[0].Rows[i]["DocTypeName"].ToString();
            string docPropertyName = ds.Tables[0].Rows[i]["DocPropertyName"].ToString();
            string confidentialLevel = ds.Tables[0].Rows[i]["ConfidentialLevel"].ToString();
            string authorName = ds.Tables[0].Rows[i]["AuthorName"].ToString();
            string authorOrgName = ds.Tables[0].Rows[i]["AuthorOrgUnitName"].ToString();
            string abstractValue = ds.Tables[0].Rows[i]["Abstract"].ToString();
            string keyWords = ds.Tables[0].Rows[i]["KeyWords"].ToString();
            string effectiveDate = ds.Tables[0].Rows[i]["EffectiveDate"].ToString();
            string expiryDate = ds.Tables[0].Rows[i]["ExpiryDate"].ToString();
            string objectGUID = ds.Tables[0].Rows[i]["ObjectGUID"].ToString();
            string sheetNo = ds.Tables[0].Rows[i]["SheetNo"].ToString();
            string subject = ds.Tables[0].Rows[i]["Subject"].ToString();
            string originatorName = ds.Tables[0].Rows[i]["OriginatorName"].ToString();
            string url = "http://192.168.2.226/ECP/SmpProgram/Form/SPKM005/Form.aspx?ParentPanelID=1&DataListID=ListTable&ParentPageUID=&UIType=General&UIStatus=8&ObjectGUID=" + objectGUID + "&IsMaintain=Y&CurPanelID=1";
            url = HttpUtility.HtmlEncode(url);
            sw1.WriteLine("<doc>");
            sw1.WriteLine("<field name=\"id\">" + docNumber + "</field>");
            sw1.WriteLine("<field name=\"doc_number\">" + docNumber + "</field>");
            sw1.WriteLine("<field name=\"name\">" + name + "</field>");
            sw1.WriteLine("<field name=\"rev_number\">" + revNumber + "</field>");
            sw1.WriteLine("<field name=\"release_date\">" + releaseDate + "</field>");
            sw1.WriteLine("<field name=\"status\">" + status + "</field>");
            sw1.WriteLine("<field name=\"major_type_name\">" + majorTypeName + "</field>");
            sw1.WriteLine("<field name=\"sub_type_name\">" + subTypeName + "</field>");
            sw1.WriteLine("<field name=\"doc_type_name\">" + docTypeName + "</field>");
            sw1.WriteLine("<field name=\"doc_property_name\">" + docPropertyName + "</field>");
            sw1.WriteLine("<field name=\"confidential_level\">" + confidentialLevel + "</field>");
            sw1.WriteLine("<field name=\"author_name\">" + authorName + "</field>");
            sw1.WriteLine("<field name=\"author_org_unit_name\">" + authorOrgName + "</field>");
            sw1.WriteLine("<field name=\"abstract\">" + abstractValue + "</field>");
            sw1.WriteLine("<field name=\"keywords\">" + keyWords + "</field>");
            sw1.WriteLine("<field name=\"effective_date\">" + effectiveDate + "</field>");
            sw1.WriteLine("<field name=\"expiry_date\">" + expiryDate + "</field>");
            sw1.WriteLine("<field name=\"sheet_no\">" + sheetNo + "</field>");
            sw1.WriteLine("<field name=\"subject\">" + subject + "</field>");
            sw1.WriteLine("<field name=\"originator\">" + originatorName + "</field>");
            sw1.WriteLine("<field name=\"doc_url\">" + url + "</field>");
            sw1.WriteLine("</doc>");
            sw1.WriteLine("");
        }
        sw1.WriteLine("</add>");
        sw1.Close();

        xmlFile = Server.MapPath("~/tempFolder") + "\\post_publish_cmd.bat";
        System.IO.StreamWriter sw2 = new System.IO.StreamWriter(xmlFile, true, System.Text.Encoding.UTF8);

        sql = "select * from SmpDocPublishFileV";
        ds = engine.getDataSet(sql, "TEMP");
        rows = ds.Tables[0].Rows.Count;

        for (int i = 0; i < rows; i++)
        {
            string id = ds.Tables[0].Rows[i]["UKey"].ToString();
            string docNumber = ds.Tables[0].Rows[i]["DocNumber"].ToString();
            string name = ds.Tables[0].Rows[i]["DocName"].ToString();
            string revGUID = ds.Tables[0].Rows[i]["RevGUID"].ToString();
            string revNumber = ds.Tables[0].Rows[i]["RevNumber"].ToString();
            string releaseDate = ds.Tables[0].Rows[i]["ReleaseDate"].ToString();
            string status = ds.Tables[0].Rows[i]["Status"].ToString();
            string majorTypeName = ds.Tables[0].Rows[i]["MajorTypeName"].ToString();
            string subTypeName = ds.Tables[0].Rows[i]["SubTypeName"].ToString();
            string docTypeName = ds.Tables[0].Rows[i]["DocTypeName"].ToString();
            string docPropertyName = ds.Tables[0].Rows[i]["DocProperty"].ToString();
            string confidentialLevel = ds.Tables[0].Rows[i]["ConfidentialLevel"].ToString();
            string authorName = ds.Tables[0].Rows[i]["AuthorName"].ToString();
            string authorOrgName = ds.Tables[0].Rows[i]["AuthorOrgUnitName"].ToString();
            string abstractValue = ds.Tables[0].Rows[i]["Abstract"].ToString();
            string keyWords = ds.Tables[0].Rows[i]["KeyWords"].ToString();
            string effectiveDate = ds.Tables[0].Rows[i]["EffectiveDate"].ToString();
            string expiryDate = ds.Tables[0].Rows[i]["ExpiryDate"].ToString();
            //string objectGUID = ds.Tables[0].Rows[i]["ObjectGUID"].ToString();
            string sheetNo = ds.Tables[0].Rows[i]["SheetNo"].ToString();
            string subject = ds.Tables[0].Rows[i]["Subject"].ToString();
            string originatorName = ds.Tables[0].Rows[i]["OriginatorName"].ToString();
            string fileName = ds.Tables[0].Rows[i]["FILENAME"].ToString();
            string fileExt = ds.Tables[0].Rows[i]["FILEEXT"].ToString();
            string localFilePath = ds.Tables[0].Rows[i]["LocalFilePath"].ToString();
            string url = ds.Tables[0].Rows[i]["URL"].ToString();
            string dparams = "";
            dparams += "literal.id=" + id;
            dparams += "&literal.doc_number="+docNumber;
            dparams += "&literal.name=" + HttpUtility.UrlEncode(name);
            dparams += "&literal.release_date=" + releaseDate;
            dparams += "&literal.major_type_name=" + HttpUtility.UrlEncode(majorTypeName);
            dparams += "&literal.sub_type_name=" + HttpUtility.UrlEncode(subTypeName);
            dparams += "&literal.doc_type_name=" + HttpUtility.UrlEncode(docTypeName);
            dparams += "&literal.doc_property_name=" + HttpUtility.UrlEncode(docPropertyName);
            dparams += "&literal.author_name=" + HttpUtility.UrlEncode(authorName);
            dparams += "&literal.author_org_unit_name=" + HttpUtility.UrlEncode(authorOrgName);
            dparams += "&literal.abstract=" + HttpUtility.UrlEncode(abstractValue);
            dparams += "&literal.keywords=" + HttpUtility.UrlEncode(keyWords);
            dparams += "&literal.sheet_no=" + sheetNo;
            dparams += "&literal.subject=" + HttpUtility.UrlEncode(subject);
            dparams += "&literal.originator=" + HttpUtility.UrlEncode(originatorName);
            dparams += "&literal.file_name=" + HttpUtility.UrlEncode(fileName);
            dparams += "&literal.file_ext=" + fileExt;
            dparams += "&literal.doc_url=" + HttpUtility.UrlEncode(url);
            string cmd = "java -Durl=http://localhost:8983/solr/update/extract -Dparams=\"" + dparams + "\" -Dtype=application/pdf -jar post.jar " + localFilePath;
            sw2.WriteLine(cmd);
        }

        sw2.Close();
    }

    /*
    private void indexFile()
    {
        new SolrBaseRepository.Instance<Document>().Start();
        var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Document>>();
        //var guid = Guid.NewGuid().ToString();
        using (var fileStream = System.IO.File.OpenRead(Server.MapPath("~/tempFolder/") + "ECP程式設計規範.pdf"))
        {
            var response =
                solr.Extract(
                    new ExtractParameters(fileStream, "DOC-000001")
                    {
                        ExtractFormat = ExtractFormat.Text,
                        ExtractOnly = false,
                        Fields = new[] { 
                            new ExtractField("name", "ECP程式設計規範"), 
                            new ExtractField("doc_number", "DOC-000001"), 
                            new ExtractField("author_name", "馬小九") }
                    });
        }
        solr.Commit();
    }
    */
    /*
    public void deleteIndexingById() 
    {
        new SolrBaseRepository.Instance<Document>().Start();
        var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Document>>();
        Document doc = new Document{
            Id = "DOC-000001"
        };
        solr.Delete(doc);
        solr.Commit();
    }
    */
    /*
    public class Document
    {
        [SolrUniqueKey("id")]
        public string Id { get; set; }

        [SolrField("name")]
        public string Name { get; set; }

        [SolrField("manu_exact")]
        public string Manufacturer { get; set; }

        [SolrField("cat")]
        public ICollection<string> Categories { get; set; }

        [SolrField("features")]
        public ICollection<string> Features { get; set; }

        [SolrField("doc_number")]
        public string DocNumber { get; set; }

        [SolrField("release_date")]
        public string ReleaseDate { get; set; }

        [SolrField("author")]
        public string Author { get; set; }

        [SolrField("author_name")]
        public string AuthorName { get; set; }

        [SolrField("abstract")]
        public string AbstactValue { get; set; }

        [SolrField("keywords")]
        public string Keywords { get; set; }

        [SolrField("doc_url")]
        public string DocUrl { get; set; }

        [SolrField("file_name")]
        public string FileName { get; set; }

        [SolrField("file_ext")]
        public string FileExt { get; set; }

        [SolrField("content")]
        public List<string> content { get; set; }
    }
    */
}