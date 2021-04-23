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
using System.IO;
using System.Xml;
using WebServerProject.auth;
using NPOI.HSSF.UserModel;

public partial class SmpProgram_maintain_SPTS005_Input : BaseWebUI.DataListSaveForm
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>   
    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.Form.Attributes.Add("enctype", "multipart/form-data");
        
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);               
                string[,] ids = null;                              

               //登錄來源
                ids = new string[,]{                   
                    {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "1", "管理單位新增")},
                    {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "2", "流程申請")}                
                    };
                RecordSource.setListItem(ids);
                RecordSource.ValueText = "1";
                RecordSource.ReadOnly = true;

                //登錄單號
                RecordNo.ReadOnly = true;

                //計劃代號
                SchNo.ReadOnly = true;

                //狀態
                ids = new string[,]{
                    //{"InProcess",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "InProcess", "進行中")},
                    //{"Closed",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "Closed", "已結案")},
                    //{"Cancelled",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "Cancelled", "已作廢")}
                    {"InProcess","InProcess"},
                    {"Closed","Closed"},
                    {"Cancelled","Cancelled"}
                    };
                Status.setListItem(ids);
                Status.ValueText = "InProcess";
                Status.ReadOnly = true;
               
                //公司別
                string userGUID = (string)Session["UserGUID"];
                SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
                ids = tsmp.getCompanyCodeName(engine, userGUID);
                CompanyCode.setListItem(ids);

                //課程代號
                SchDetailGUID.clientEngineType = (string)Session["engineType"];
                SchDetailGUID.connectDBString = (string)Session["connectString"];                

                //課程來源
                ids = new string[,]{
                        {"",""},
                        {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "1", "年度計劃")},                
                        {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "2", "新增申請")}                                                
                    };
                SchSource.setListItem(ids);
                SchSource.ReadOnly =true;

                //課程來源
                ids = new string[,] {
                    {"",""},
                    {"I",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "I", "內訓")},
                    {"O",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "O", "外訓")}
                };
                InOut.setListItem(ids);
                InOut.ReadOnly = true;

                //課程類別
                ids = new string[,]{ 
                    {"",""},
                    {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "1", "新人訓練")},
                    {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "2", "專業職能")},
                    {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "3", "管理職能")},
                    {"4",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "4", "品質管理")},
                    {"5",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "5", "安全衛生")} 
                };
                SubjectType.setListItem(ids);
                SubjectType.ReadOnly = true;

                //教授方式
                ids = new string[,]{ 
                        {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", "1", "面授")},
                    };
                Way.setListItem(ids);
                Way.ValueText = "1";

                //講師
                //LecturerGUID.ReadOnly = true;
                LecturerGUID.clientEngineType = (string)Session["engineType"];
                LecturerGUID.connectDBString = (string)Session["connectString"]; 
                

                //開課單位
                DeptGUID.clientEngineType = (string)Session["engineType"];
                DeptGUID.connectDBString = (string)Session["connectString"];
                DeptGUID.ReadOnly = true;

                //承辦人員
                //UndertakerGUID.clientEngineType = (string)Session["engineType"];
                //UndertakerGUID.connectDBString = (string)Session["connectString"];   

                //TTQS
                ids = new string[,]{  
                        {"",""},
                        {"Y","Y"},
                        {"N","N"}       
                    };
                TTQS.setListItem(ids);
                TTQS.ReadOnly = true;

                //評核方式
                CbWrittenTest.ReadOnly = true;
                CbImplement.ReadOnly = true;
                CbSatisfaction.ReadOnly = true;
                CbInOther.ReadOnly = true;
                InOtherDesc.ReadOnly = true;
                CbPresentation.ReadOnly = true;
                CbCertificate.ReadOnly = true;
                CbReport.ReadOnly = true;
                CbOJT.ReadOnly = true;
                CbOutOther.ReadOnly = true;
                OutOtherDesc.ReadOnly = true;

                //file Adapter
                WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
                string fileAdapter = sp.getParam("FileAdapter"); //系統參數
                AbstractEngine fileEngine = factory.getEngine(engineType, connectString);

                //匯入檔案元件初始化
                FileUploadTrainee.FileAdapter = fileAdapter;
                FileUploadTrainee.engine = fileEngine;
                FileUploadTrainee.tempFolder = Server.MapPath("~/tempFolder");
                FileUploadTrainee.maxLength = 10485760 * 3;
                FileUploadTrainee.readFile("");
                FileUploadTrainee.Display = true;

                //上傳檔案元件初始化                                               
                FileUploadAtta.FileAdapter = fileAdapter;                               
                FileUploadAtta.engine = fileEngine;
                FileUploadAtta.tempFolder = Server.MapPath("~/tempFolder");
                FileUploadAtta.maxLength = 10485760 * 3;
                FileUploadAtta.readFile("");
                FileUploadAtta.Display = true;

                //教材 
                ids = new string[,]{ 
                        {"KM","KM"}   
                    };
                Source.setListItem(ids);                 
                MaterialGUID.clientEngineType = engineType;
                MaterialGUID.connectDBString = connectString;

            } //if !IsProcessEvent
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        //head
        RecordSource.ValueText = objects.getData("RecordSource");
        RecordNo.ValueText = objects.getData("RecordNo");        
        CloseDate.ValueText = objects.getData("CloseDate");
        CompanyCode.ValueText = objects.getData("CompanyCode");
        CompanyCode.ReadOnly = !isNew;
        CourseYear.ValueText = objects.getData("CourseYear");
        CourseYear.ReadOnly = !isNew;
		//課程代號
		SchDetailGUID.GuidValueText = objects.getData("SchDetailGUID");      
        SchDetailGUID.doGUIDValidate();
        SchDetailGUID.ReadOnly = !isNew;  //課程一但存過不允許修改
        BriefIntro.ValueText = objects.getData("BriefIntro");  
        Way.ValueText = objects.getData("Way");
		
		//承辦人員
        //UndertakerGUID.GuidValueText = objects.getData("UndertakerGUID");      
        //UndertakerGUID.doGUIDValidate();
        StartDate.ValueText = objects.getData("StartDate");
        /*20150706 HR取消欄位
        EndDate.ValueText = objects.getData("EndDate");
        StartTime.ValueText = objects.getData("StartTime");
        EndTime.ValueText = objects.getData("EndTime");
        Place.ValueText = objects.getData("Place"); 
        */
        Hours.ValueText = objects.getData("Hours");
        
		//講師
		LecturerGUID.GuidValueText = objects.getData("LecturerGUID");      
        LecturerGUID.doGUIDValidate();
		//內訓評核方式
		//筆試
        string value = objects.getData("WrittenTest");
        WrittenTest.ValueText = value;
        if (value.Equals("Y"))
        {
            CbWrittenTest.Checked = true;
        }
		//實作
		value = objects.getData("Implement");
        Implement.ValueText = value;
        if(value.Equals("Y")) 
        {
            CbImplement.Checked = true;
        }
		//課程滿意度調查
		value = objects.getData("Satisfaction");
        Satisfaction.ValueText = value;
        if(value.Equals("Y")) 
        {
            CbSatisfaction.Checked = true;
        }
        //內訓其他
        value = objects.getData("InOther");
        InOther.ValueText = value;
        if (value.Equals("Y"))
        {
            CbInOther.Checked = true;
        }
        //其他說明
        InOtherDesc.ValueText = objects.getData("InOtherDesc"); 
		
		//外訓
		//發表開課
		value = objects.getData("Presentation");
        Presentation.ValueText = value;
        if(value.Equals("Y")) 
        {
            CbPresentation.Checked = true;
        }
		//證書
		value = objects.getData("Certificate");
        Certificate.ValueText = value;
        if(value.Equals("Y")) 
        {
            CbCertificate.Checked = true;
        }
		//受訓心得報告
		value = objects.getData("Report");
        Report.ValueText = value;
        if(value.Equals("Y")) 
        {
            CbReport.Checked = true;
        }
		//OJT
		value = objects.getData("OJT");
        OJT.ValueText = value;
        if(value.Equals("Y")) 
        {
            CbOJT.Checked = true;
        }
		//外訓其他
		value = objects.getData("OutOther");
        OutOther.ValueText = value;
        if(value.Equals("Y")) 
        {
            CbOutOther.Checked = true;
        }		
		//其他說明
		OutOtherDesc.ValueText = objects.getData("OutOtherDesc"); 
		Remark.ValueText = objects.getData("Remark");

        //課程狀態
        Status.ValueText = objects.getData("Status");
        Boolean ifCanModify = true;
        if (Status.ValueText.Equals("Closed"))
            ifCanModify = false;
        setReadOnly(ifCanModify);   

        //學員        
        DataObjectSet traineeSet = null;
        if (isNew)
        {
            traineeSet = new DataObjectSet();
            traineeSet.isNameLess = true;
            traineeSet.setAssemblyName("WebServerProject");
            traineeSet.setChildClassString("WebServerProject.maintain.SPTS005.SmpTSCourseTrainee");
            traineeSet.setTableName("SmpTSCourseTrainee");
            traineeSet.loadFileSchema();
            objects.setChild("SmpTSCourseTrainee", traineeSet);
        }
        else
        {
            traineeSet = objects.getChild("SmpTSCourseTrainee");
        }
        DataListTrainee.dataSource = traineeSet;
        DataListTrainee.InputForm = "Detail.aspx";
        DataListTrainee.DialogWidth = 570;
        DataListTrainee.HiddenField = new string[] { "GUID", "CourseFormGUID", "EmployeeGUID","DeptGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        DataListTrainee.reSortCondition("工號", DataObjectConstants.ASC);
        DataListTrainee.updateTable();        

        //km教材        
        DataObjectSet materialSet = null;
        if (isNew)
        {
            materialSet = new DataObjectSet();
            materialSet.isNameLess = true;
            materialSet.setAssemblyName("WebServerProject");
            materialSet.setChildClassString("WebServerProject.maintain.SPTS005.SmpTSCourseMaterial");
            materialSet.setTableName("SmpTSCourseMaterial");
            materialSet.loadFileSchema();
            objects.setChild("SmpTSCourseMaterial", materialSet);
        }
        else
        {
            materialSet = objects.getChild("SmpTSCourseMaterial");
            for (int i = 0; i < materialSet.getAvailableDataObjectCount(); i++)
            {
                DataObject obj = materialSet.getAvailableDataObject(i);
                string docUrl = getKmUrl(obj);
                obj.setData("MaterialURL", docUrl);
            }
        }
        DataListMaterial.dataSource = materialSet;
        DataListMaterial.HiddenField = new string[] { "GUID", "CourseFormGUID", "MaterialGUID", "DocNumber", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        DataListMaterial.reSortCondition("文件編號", DataObjectConstants.ASC);
        DataListMaterial.setColumnStyle("MaterialURL", 80, DSCWebControl.GridColumnStyle.LEFT);
        DataListMaterial.updateTable();


        //附件
        DataObjectSet attachmentSet = null;
        if (isNew)
        {
            attachmentSet = new DataObjectSet();
            attachmentSet.isNameLess = true;
            attachmentSet.setAssemblyName("WebServerProject");
            attachmentSet.setChildClassString("WebServerProject.maintain.SPTS005.SmpTSCourseAttach");
            attachmentSet.setTableName("SmpTSCourseAttach");
            attachmentSet.loadFileSchema();
            objects.setChild("SmpTSCourseAttach", attachmentSet);
        }
        else
        {
            attachmentSet = objects.getChild("SmpTSCourseAttach");
            for (int i = 0; i < attachmentSet.getAvailableDataObjectCount(); i++)
            {
                DataObject obj = attachmentSet.getAvailableDataObject(i);
                string fileNameUrl = getFileUrl(obj);
                obj.setData("FILEURL", fileNameUrl);
            }
            DataListAttachment.setColumnStyle("FILEURL", 150, DSCWebControl.GridColumnStyle.LEFT);            
        }
        DataListAttachment.dataSource = attachmentSet;
        DataListAttachment.HiddenField = new string[] { "GUID", "CourseFormGUID", "FileItemGUID","FILENAME", "AttachmentType", "RecordNo","IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        DataListAttachment.WidthMode = 1;
        DataListAttachment.NoAdd = true;
        DataListAttachment.reSortCondition("上傳時間", DataObjectConstants.ASC);
        DataListAttachment.updateTable();
        AttaFileName.ReadOnly = true;
                
    }

    /// <summary>
    /// 改變附件Grid裡的URL
    /// </summary>
    /// <param name="objects"></param>
    protected string getFileUrl(DataObject obj)
    {
        string href = "http://" + Request.Url.Authority + Page.ResolveUrl("../SPTS005/DownloadFile.aspx");        
        string fileName = obj.getData("FILENAME");
        string fileExt = obj.getData("FILEEXT");
        string recordNo = obj.getData("RecordNo");
        string fileItemGUID = obj.getData("FileItemGUID");
        href += "?FILENAME=" + System.Web.HttpUtility.UrlPathEncode(fileName);
        href += "&FILEEXT=" + fileExt;
        href += "&RecordNo=" + recordNo;
        href += "&FileItemGUID=" + fileItemGUID;
        string fileNameUrl = "{[a href=\"" + href + "\"]}" + fileName + "{[/a]}";
        return fileNameUrl;
    }



    /// <summary>
    /// 改變教材Grid裡的URL
    /// </summary>
    /// <param name="objects"></param>
    protected string getKmUrl(DataObject obj)
    {
        string source = obj.getData("Source");
        string reference = obj.getData("MaterialGUID");
        string docNum = obj.getData("DocNumber");
        string href = "../../Form/SPKM005/Reference.aspx";
        href += "?Source=" + source;
        href += "&Reference=" + reference;
        //string docUrl = "{[a href=\"" + href + "\"]}" + docNum + "{[/a]}";
        string docUrl = "{[a href=\"" + href + "\" " + " target = \"文件查詢\"]}" + docNum + "{[/a]}";
        return docUrl;
    }

    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        AbstractEngine engine = null;
         
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        engine = factory.getEngine(engineType, connectString);

        bool isNew = (bool)getSession("isNew");
        objects.setData("Status", Status.ValueText);
        if (isNew)
        {
            objects.setData("RecordSource", RecordSource.ValueText);
            string seqNo = "";
            if  (!CourseYear.ValueText.Equals("") &&  !StartDate.ValueText.Equals(""))
               seqNo= getCustomSheetNo(engine, "SMPTSCourseSeqNo", CompanyCode.ValueText, CourseYear.ValueText, StartDate.ValueText.Substring(5, 2));
            //if (seqNo.Equals(""))
            //{
            //    seqNo = "1";
            //}
            //Page.Response.Write("alert('seqNO:" + seqNo + "');");
            objects.setData("RecordNo", seqNo);
            string companyCode = CompanyCode.ValueText;
            string companyName = "";

            if (companyCode.Equals("SMP"))
            {
                companyName = "新普科技";
            }
            else if (companyCode.Equals("TP"))
            {
                companyName = "中普科技";
            }
            objects.setData("CompanyCode", companyCode);
            objects.setData("CompanyName", companyName);
            objects.setData("CourseYear", CourseYear.ValueText);
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        //else
        //{
        //    //把file url檔名換掉                
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

        objects.setData("SchDetailGUID", SchDetailGUID.GuidValueText);
        objects.setData("SchNo", SchNo.ValueText);
        objects.setData("BriefIntro", BriefIntro.ValueText);
        objects.setData("SubjectNo", SchDetailGUID.ValueText);
        objects.setData("SubjectName", SchDetailGUID.ReadOnlyValueText);
        objects.setData("InOut", InOut.ValueText);
        objects.setData("SubjectType", SubjectType.ValueText);
        objects.setData("TTQS", TTQS.ValueText);
        objects.setData("SchSource", SchSource.ValueText);
        objects.setData("Way", Way.ValueText);
        objects.setData("DeptGUID", DeptGUID.GuidValueText);
        objects.setData("DeptId", DeptGUID.ValueText);
        objects.setData("DeptName", DeptGUID.ReadOnlyValueText);
        objects.setData("CloseDate", CloseDate.ValueText);
        //objects.setData("UndertakerGUID", UndertakerGUID.GuidValueText);
        //objects.setData("UndertakerName", UndertakerGUID.ReadOnlyValueText);
        objects.setData("StartDate", StartDate.ValueText);
        
        /*
        if (EndDate.ValueText.Equals(""))
        {
            EndDate.ValueText = StartDate.ValueText;
        }
        objects.setData("EndDate", EndDate.ValueText);
        objects.setData("Place", Place.ValueText);
        objects.setData("StartTime", StartTime.ValueText);
        objects.setData("EndTime", EndTime.ValueText);
        */

        objects.setData("Hours", Hours.ValueText);
        objects.setData("LecturerGUID", LecturerGUID.GuidValueText);
        objects.setData("Lecturer", LecturerGUID.ValueText);
        objects.setData("LecturerUnit", LecturerGUID.ReadOnlyValueText);
        //內訓評核方式
        //筆試
        if (CbWrittenTest.Checked)
        {
            WrittenTest.ValueText = "Y";
        }
        else
        {
            WrittenTest.ValueText = "N";
        }

        //實作	
        if (CbImplement.Checked)
        {
            Implement.ValueText = "Y";
        }
        else
        {
            Implement.ValueText = "N";
        }

        //課程滿意度調查		
        if (CbSatisfaction.Checked)
        {
            Satisfaction.ValueText = "Y";
        }
        else
        {
            Satisfaction.ValueText = "N";
        }

        //內訓其他		
        if (CbInOther.Checked)
        {
            InOther.ValueText = "Y";
        }
        else
        {
            InOther.ValueText = "N";
        }

        //外訓
        //發表開課		
        if (CbPresentation.Checked)
        {
            Presentation.ValueText = "Y";
        }
        else
        {
            Presentation.ValueText = "N";
        }

        //證書		
        if (CbCertificate.Checked)
        {
            Certificate.ValueText = "Y";
        }
        else
        {
            Certificate.ValueText = "N";
        }

        //受訓心得報告		
        if (CbReport.Checked)
        {
            Report.ValueText = "Y";
        }
        else
        {
            Report.ValueText = "N";
        }

        //OJT		
        if (CbOJT.Checked)
        {
            OJT.ValueText = "Y";
        }
        else
        {
            OJT.ValueText = "N";
        }

        //外訓其他		
        if (CbOutOther.Checked)
        {
            OutOther.ValueText = "Y";
        }
        else
        {
            OutOther.ValueText = "N";
        }

        objects.setData("WrittenTest", WrittenTest.ValueText);
        objects.setData("Implement", Implement.ValueText);
        objects.setData("Satisfaction", Satisfaction.ValueText);
        objects.setData("InOther", InOther.ValueText);
        objects.setData("InOtherDesc", InOtherDesc.ValueText);
        objects.setData("Presentation", Presentation.ValueText);
        objects.setData("Certificate", Certificate.ValueText);
        objects.setData("Report", Report.ValueText);
        objects.setData("OJT", OJT.ValueText);
        objects.setData("OutOther", OutOther.ValueText);
        objects.setData("OutOtherDesc", OutOtherDesc.ValueText);
        objects.setData("Remark", Remark.ValueText);

        //學員
        DataObjectSet traineeSet = DataListTrainee.dataSource;
        for (int i = 0; i < traineeSet.getAvailableDataObjectCount(); i++)
        {
            traineeSet.getAvailableDataObject(i).setData("CourseFormGUID", objects.getData("GUID"));
        }

        //教材
        DataObjectSet materialSet = DataListMaterial.dataSource;
        for (int i = 0; i < materialSet.getAvailableDataObjectCount(); i++)
        {
            materialSet.getAvailableDataObject(i).setData("CourseFormGUID", objects.getData("GUID"));
        }

        //附件
        DataObjectSet attachmentSet = DataListAttachment.dataSource;
        for (int i = 0; i < attachmentSet.getAvailableDataObjectCount(); i++)
        {
            attachmentSet.getAvailableDataObject(i).setData("CourseFormGUID", objects.getData("GUID"));
            attachmentSet.getAvailableDataObject(i).setData("Processed", "Y");
        }

        //檢查欄位資料
        string errMsg = checkFieldData(objects, engine);        
        if (!errMsg.Equals(""))
        {
            errMsg = errMsg.Replace("\\n", "; ");
            throw new Exception(errMsg);               
        }
        else
        {
            FileUploadAtta.setJobID(objects.getData("GUID"));
            FileUploadAtta.confirmSave("ETS", objects.getData("RecordNo"));
            FileUploadAtta.saveFile();
            FileUploadAtta.engine.close();
        }

        //回寫計劃代號
        try
        {
            string sql ="";
            if (Status.ValueText.Equals("Cancelled"))
            {
                sql = "update SmpTSSchDetail set Closed ='N' where SchNo ='" + SchNo.ValueText + "' and Closed <>'N' ";
            }
            else
            {
                sql = "update SmpTSSchDetail set Closed ='Y' where SchNo ='" + SchNo.ValueText + "' and Closed <>'Y' ";
            }
            
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
            engine.close();
        }
    }
	
	/// <summary>
    /// 檢查欄位資料
    /// </summary>
    /// <param name="objects"></param>
	public string checkFieldData(com.dsc.kernal.databean.DataObject objects, AbstractEngine engine)
    {
        string errMsg = "";
        string guid = objects.getData("GUID");
        string schDetailGUID = objects.getData("SchDetailGUID");
		string inOut = objects.getData("InOut");       
		string inOtherDesc = objects.getData("InOtherDesc");
        string outOtherDesc = objects.getData("OutOtherDesc");
        string startDate = objects.getData("StartDate");
        //string endDate = objects.getData("EndDate");
        string closeDate = objects.getData("CloseDate");
        string courseYear = objects.getData("CourseYear") ;       
        string lecturerGUID = objects.getData("LecturerGUID");

        if (!startDate.Equals(""))
        {
            if (startDate.Substring(0, 4).CompareTo(courseYear) > 0)
                errMsg += "上課日期不可大於開課年度!\\n";
        }

        /*
        if (!endDate.Equals("") && endDate.Substring(0, 4).CompareTo(courseYear) > 0)
        {            
                errMsg += "上課日期(迄)不可大於開課年度!\\n";
        }
        */
              
		if (inOut.Equals("I"))
        {
            
            //if (DataListMaterial.dataSource.getAvailableDataObjectCount() == 0)
            //{
            //    errMsg += "內訓課程，請連結KM文件!\n";
            //}

            if (!CbWrittenTest.Checked && !CbImplement.Checked && !CbSatisfaction.Checked &&!CbInOther.Checked)
			{
				errMsg += "請選擇內訓評核方式!\\n";
			}

            if (CbInOther.Checked && inOtherDesc.Equals(""))
            {
                errMsg += "請輸入內訓評核方式「其他」說明欄!\\n";
            }
					
        }
		else if (inOut.Equals("O"))
		{
			if(!CbPresentation.Checked && !CbCertificate.Checked && !CbReport.Checked && !CbOJT.Checked && !CbOutOther.Checked)
			{
				errMsg += "請選擇外訓評核方式!\\n";
			}
			
			if (CbOutOther.Checked && outOtherDesc.Equals(""))
			{
				errMsg += "請輸入外訓評核方式「其他」說明欄!\\n";
			}
		}

        //資料是否重覆,計劃代號不可以重覆
        string sql = "select count(*) cnt from SmpTSCourseForm where CourseYear = '" + CourseYear.ValueText + "' " +
              "and CompanyCode ='" + CompanyCode.ValueText + "' " +
              "and GUID != '" + guid + "' " +
              "and SchDetailGUID = '" + schDetailGUID + "' AND Status <> 'Cancelled' ";
              //"and ((StartDate <= '" + startDate + "' and EndDate >= '" + startDate + "') " +
              //  "or (StartDate <= '" + endDate + "' and EndDate >= '" + endDate + "') )";
        int cnt = (int)engine.executeScalar(sql);
        if (cnt > 0)
        {
            errMsg += "課程[" + SchDetailGUID.ReadOnlyValueText + "]記錄已存在，不可重覆!\\n";
        }

        if (!closeDate.Equals(""))
        {
            if (closeDate.Substring(0, 4).CompareTo(courseYear) > 0)
                errMsg += "結案日期不可大於開課年度!\\n";
            //學員
            if (DataListTrainee.dataSource.getAvailableDataObjectCount() == 0)
            {
                errMsg += "請輸入學員資料!\\n";
            }
            //講師
            if (lecturerGUID.Equals(""))
            {
                errMsg += "請輸入講師資料!\\n";
            }
        }

        if (!errMsg.Equals(""))
        {
            CloseDate.ValueText = "";
            Status.ValueText = "InProcess";
            setReadOnly(true);
        }

        return errMsg;
	}


    protected override void afterDelete(DataObject objects)
    {
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            string sql = "update SmpTSSchDetail set Closed ='N' where SchNo ='" + SchNo.ValueText + "' and Closed <>'N' ";            
            
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
            engine.close();
        }
    }


    /// <summary>
    /// saveDB
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveDB(DataObject objects)
    {
        if (objects.getData("RecordNo").Equals(""))
            throw new Exception("無法取得登錄單號，請確認！");
        else
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.SPTS005.SmpTSCourseFormAgent");
            agent.engine = engine;
            agent.query("1=2");

            bool result = agent.defaultData.add(objects);
            if (!result)
            {
                engine.close();
                throw new Exception(agent.defaultData.errorString);
            }
            else
            {
                result = agent.update();
                engine.close();
                if (!result)
                {
                    throw new Exception(engine.errorString);
                }
            }
        }        
    }

    /// <summary>
    /// 取得編號
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    protected string getCustomSheetNo(AbstractEngine engine, string code, string companycode, string year, string month)
    {
        string sheetNo = "";
        try
        {                    
            object codeId = engine.executeScalar("select SMVIAAA002 from SMVIAAA where SMVIAAA002='" + code + "'");
            WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
            ac.engine = engine;
            Hashtable hs = new Hashtable();
            hs.Add("CompanyCode", companycode);
            hs.Add("CourseYear", year);
            hs.Add("CourseMonth", month);
            sheetNo = ac.getAutoCode(Convert.ToString(codeId), hs).ToString();
            
        }
        catch (Exception e)
        {
            base.writeLog(e);
            base.writeLog(new Exception("sheetno:" + sheetNo));
        }
        return sheetNo;
    }
		
    /// <summary>
    /// 教材資料顯示
    /// </summary>
    /// <param name="objects"></param>
    protected void DataListMaterial_ShowRowData(DataObject objects)
    {
        Source.ValueText = objects.getData("Source");
        MaterialGUID.GuidValueText = objects.getData("MaterialGUID");
        if (!MaterialGUID.GuidValueText.Equals(""))
        {
            MaterialGUID.doGUIDValidate();
        }
    }

    /// <summary>
    /// 教材資料儲存
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    /// <returns></returns>
    protected bool DataListMaterial_SaveRowData(DataObject objects, bool isNew)        
    {
        string strErrMsg = "";
        try
        {
            if (Source.ValueText.Equals(""))
            {
                strErrMsg += LblSource.Text + ": 必需選擇!\\n";
            }

            if (MaterialGUID.GuidValueText.Equals(""))
            {
                strErrMsg += LblMaterialGUID.Text + ": 必需選擇!\\n";
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
            objects.setData("CourseFormGUID", "TEMP");
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("Source", Source.ValueText);
        objects.setData("MaterialGUID", MaterialGUID.GuidValueText);
        objects.setData("DocNumber", MaterialGUID.ValueText);
        objects.setData("DocName", MaterialGUID.ReadOnlyValueText);

        string docUrl = getKmUrl(objects);
        objects.setData("MaterialURL", docUrl);

        return true;
    }

    /// <summary>
    /// 過濾已失效文件
    /// </summary>
    /// 
    protected void MaterialGUID_BeforeClickButton()
    {
        MaterialGUID.whereClause = "(Status != 'Cancelled')";
    }

    /// <summary>
    /// 開啟教材文件前設定
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isAddNew"></param>
    /// <returns></returns>
    protected bool Material_BeforeOpenWindow(DataObject objects, bool isAddNew)
    {
        setSession((string)Session["UserID"], "Reference", objects);
        return true;
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
    protected void AttaUpload_Click(object sender, EventArgs e)
    {
        FileUploadAtta.openFileUploadDialog();
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
            DataObjectSet detailSet = DataListAttachment.dataSource;
            DataObject obj = detailSet.create();
            obj.setData("GUID", IDProcessor.getID(""));
            obj.setData("CourseFormGUID", "CourseFormTEMP");
            obj.setData("RecordNo", (string)getSession(PageUniqueID, "RecordNo"));
            obj.setData("FileItemGUID", currentObject.GUID); 
           
            //GETFILE URL  
            string href = "http://" + Request.Url.Authority + Page.ResolveUrl("../SPTS005/DownloadFile.aspx");
            string fileName = currentObject.FILENAME;
            string fileExt =  currentObject.FILEEXT;
            string recordNo = (string)getSession(PageUniqueID, "RecordNo");
            string fileItemGUID = currentObject.GUID;
            href += "?FILENAME=" + System.Web.HttpUtility.UrlPathEncode(fileName);
            href += "&FILEEXT=" + fileExt;
            href += "&RecordNo=" + recordNo;
            href += "&FileItemGUID=" + fileItemGUID;
            string fileNameUrl = "{[a href=\"" + href + "\"]}" + fileName + "{[/a]}";
            
            obj.setData("FILEURL", fileNameUrl);
            obj.setData("FILENAME", currentObject.FILENAME);
            obj.setData("FILEEXT", currentObject.FILEEXT);
            obj.setData("AttachmentType", "n/a");//保留欄位，預設
            obj.setData("Processed", "N"); //保留欄位，預設
            obj.setData("DESCRIPTION", currentObject.DESCRIPTION);

            obj.setData("UPLOADUSER", (string)Session["UserId"]);
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
    /// 檔案名稱
    /// </summary>
    /// <param name="DataObject"></param>   
    protected void DataListAttachment_ShowRowData(DataObject objects)
    {
        string fileName = objects.getData("FILENAME");
        //int idxs = fileName.IndexOf("]}");
        //int idxe = fileName.LastIndexOf("{[");
        //if (idxs > 0 && idxe > 0)
        //{
        //    fileName = fileName.Substring(idxs + 2, idxe - (idxs + 2));
        //}
        AttaFileName.ValueText = fileName;
        setSession((string)Session["UserID"], "DownFile", objects);
    }

    /// <summary>
    /// 開啟課程前檢查
    /// </summary>
    protected void SchDetailGUID_BeforeClickButton()
    {
        //非取消課程
        SchDetailGUID.whereClause = "(Cancel = 'N' and CompanyCode = '" + CompanyCode.ValueText + 
                "' and SchYear = '" + CourseYear.ValueText + "' )";
    }
    
	/// <summary>
    /// 課程開窗回傳預設值    
    /// </summary>   
    /// <param name="engine"></param>
    /// <param name="GUID"></param>
    /// <returns>string[]</returns>
    protected string[][] getCourseInfo(AbstractEngine engine, string GUID)
    {
        string sql = "select SchSource,SubjectName,DeptGUID,deptId,deptName,InOut,SubjectType,TTQS,SchNo " +
                     "from SmpTSSchFormListV " +
                     "where GUID = '" + Utility.filter(GUID) + "' ";

        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;
        string[][] result = new string[rows][];
        for (int i = 0; i < rows; i++)
        {
            result[i] = new string[9];
            result[i][0] = ds.Tables[0].Rows[i][0].ToString();
            result[i][1] = ds.Tables[0].Rows[i][1].ToString();
            result[i][2] = ds.Tables[0].Rows[i][2].ToString();
            result[i][3] = ds.Tables[0].Rows[i][3].ToString();
            result[i][4] = ds.Tables[0].Rows[i][4].ToString();
            result[i][5] = ds.Tables[0].Rows[i][5].ToString();
            result[i][6] = ds.Tables[0].Rows[i][6].ToString();
            result[i][7] = ds.Tables[0].Rows[i][7].ToString();
            result[i][8] = ds.Tables[0].Rows[i][8].ToString();
        }
        return result;
    }

    //取得課預設值
    protected void SchDetailGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, connectString);

        string[][] results = getCourseInfo(engine, SchDetailGUID.GuidValueText);
        if (results != null && results.Length > 0)
        {
            SchSource.ValueText = results[0][0];
            DeptGUID.GuidValueText = results[0][2];
            DeptGUID.doGUIDValidate();
            InOut.ValueText = results[0][5];
            SubjectType.ValueText = results[0][6];
            TTQS.ValueText = results[0][7];
            SchNo.ValueText = results[0][8];
        }
        
        //評核方式
        if (!InOut.ValueText.Equals(""))
        {
            Boolean ifIn = InOut.ValueText.Equals("I") ? true : false;
            
            //內訓
            CbWrittenTest.ReadOnly = !ifIn;
            CbImplement.ReadOnly = !ifIn;
            CbSatisfaction.ReadOnly = !ifIn;
            CbInOther.ReadOnly = !ifIn;
            InOtherDesc.ReadOnly = !ifIn;
            //外訓
            CbPresentation.ReadOnly = ifIn;
            CbCertificate.ReadOnly = ifIn;
            CbReport.ReadOnly = ifIn;
            CbOJT.ReadOnly = ifIn;
            CbOutOther.ReadOnly = ifIn;
            OutOtherDesc.ReadOnly = ifIn;

            if (ifIn)
            {
                CbPresentation.Checked = false;
                CbCertificate.Checked = false;
                CbReport.Checked = false;
                CbOJT.Checked = false;
                CbOutOther.Checked = false;
                Presentation.ValueText = "N";
                Certificate.ValueText = "N";
                Report.ValueText = "N";
                OJT.ValueText = "N";
                OutOther.ValueText = "N";
                OutOtherDesc.ValueText = "";
            }
            else
            {
                CbWrittenTest.Checked  = false;
                CbImplement.Checked  = false;
                CbSatisfaction.Checked  = false;
                CbInOther.Checked  = false;
                WrittenTest.ValueText = "N";
                Implement.ValueText = "N";
                Satisfaction.ValueText = "N";
                InOther.ValueText = "N";
                InOtherDesc.ValueText = "";     
            }            
        }       
    }


    //內訓其他
    protected void CbInOther_Click(object sender, EventArgs e)
    {
        InOtherDesc.ReadOnly = !(CbInOther.Checked);
        if (!CbInOther.Checked)
            InOtherDesc.ValueText = "";
    }

    //外訓其他
    protected void CbOutOther_Click(object sender, EventArgs e)
    {
        OutOtherDesc.ReadOnly = !(CbOutOther.Checked);
        if (!CbOutOther.Checked)
            OutOtherDesc.ValueText ="";         
    }

    
    /// <summary>
    /// Script open window, 開窗下載檔案
    /// </summary>
    /// <param name="url"></param>
    private void downloadFile(string url)
    {
        string mstr = "<script language=javascript>";
        mstr += "function SPTS005_Msg(){";
        mstr += "window.open('" + url + "');";
        mstr += "}";
        mstr += "window.setTimeout('SPTS005_Msg()', 100);";
        mstr += "</script>";

        ClientScriptManager cm = Page.ClientScript;
        Type ctype = Page.GetType();
        cm.RegisterStartupScript(ctype, this.GetType().Name + "_" + ClientID, mstr);

    }    

    /// <summary>
    /// 開啟上傳學員檔案
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TraineeImport_Click(object sender, EventArgs e)
    {
        FileUploadTrainee.openFileUploadDialog();
    }


    /// <summary>
    /// 上傳學員檔案後執行匯入動作
    /// </summary>
    /// <param name="currentObject"></param>
    /// <param name="isNew"></param>
    protected void Trainee_AddOutline(DSCWebControl.FileItem currentObject, bool isNew)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;
        //System.IO.StreamWriter sw1 = null;
        string fileMsg = "";
        string fName = "";
        try
        {            
            //sw1 = new System.IO.StreamWriter(@"d:\temp\SPTS005.log", true);    
            TraineeFileName.ValueText = currentObject.FILENAME + "." + currentObject.FILEEXT;
           
            //檔案上傳入TempFolder                          
            string tempPath = Server.MapPath("~/tempFolder") + "\\";
            fName = tempPath + currentObject.FILEPATH ;
                      
            //取得匯入資料                    
            SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
            ArrayList aryData = tsmp.getExcelData(fName);
            
            //取得欄位資料加入datalist
            DataObjectSet dos = DataListTrainee.dataSource;
            
            //記錄原有的id,後續判斷是否重覆
            string traineeOriginal = "";
            for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
            {
                DataObject obj = dos.getDataObject(i);
                traineeOriginal += obj.getData("id") + ",";
            }

            factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
            string sqlUser = "select OID from Users where id = ";
            string sqlDept = "select OID from SmpOrgUnitAll where deptId = ";
            int counts=0;            
            for (int i = 0; i < aryData.Count; i++)
            {
                DataObject objects = dos.create();
                string[] data = (string[])aryData[i];
                if (data[0].Equals("ADD"))
                {
                    string employeeGuid = (string)engine.executeScalar(sqlUser + "'" + data[1] + "'");
                    string deptGuid = (string)engine.executeScalar(sqlDept + "'" + data[3] + "'");

                    if (employeeGuid != null && deptGuid != null && traineeOriginal.IndexOf(data[1]) ==-1)
                    {
                        objects.setData("GUID", IDProcessor.getID(""));
                        objects.setData("CourseFormGUID", "temp");
                        objects.setData("IS_LOCK", "N");
                        objects.setData("IS_DISPLAY", "Y");
                        objects.setData("DATA_STATUS", "Y");
                        objects.setData("EmployeeGUID", employeeGuid);
                        objects.setData("id", data[1]);
                        objects.setData("userName", data[2]);
                        objects.setData("DeptGUID", deptGuid);
                        objects.setData("deptId", data[3]);
                        objects.setData("deptName", data[4]);
                        objects.setData("ApplyWay", data[5]);
                        objects.setData("Attendance", data[6]);
                        objects.setData("GetCertificate", data[7]);
                        objects.setData("CertificateNo", data[8]);
                        objects.setData("Fee", data[9]);
                        objects.setData("Sign", data[10]);
                        objects.setData("Expire", data[11]);
                        objects.setData("Pass", data[12]);                        
                        dos.add(objects);
                        counts++;
                    }
                    else
                    {
                        fileMsg += (i+1) + ",";
                    }
                }
            }
            DataListTrainee.dataSource = dos;
            DataListTrainee.updateTable();
            
            string returnMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts005_import_aspx.language.ini",
                "message", "returnMsg", "Excel讀取完成，匯入筆數:");
            returnMsg += counts;
            if (!fileMsg.Equals("")){
                string tmpMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts005_import_aspx.language.ini",
                "message", "tmpMsg", "以下列數資料不完整或重覆無法匯入:");
                returnMsg += ";" + tmpMsg + fileMsg; 
            }
            Page.Response.Write("alert('" + returnMsg +"');");          
        }
        catch (Exception e)
        {
            //sw1.WriteLine("fileMsg :" + fileMsg);
            writeLog(e);
            string eMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts005_import_aspx.language.ini",
                        "message", "alertMsg", "處理檔案發生錯誤. 可能您上傳的檔案格式不和, 或是系統處理錯誤. 錯誤訊息為: ") + e.Message;
            Page.Response.Write("alert('"+ eMsg+"');");  
        }
        finally
        {
            if (engine != null)
                engine.close();
            //if (sw1 != null)
            //    sw1.Close();
            //刪除上傳檔案
            System.IO.File.Delete(fName);
    
        }
    }

     
    /// <summary>
    /// 講師開窗條件
    /// </summary>   
    protected void LecturerGUID_BeforeClickButton()
    {        
        string subjectype = "";
        string sdate = StartDate.ValueText;
        string conditions = "";
        string inout= InOut.ValueText;
       
        switch (SubjectType.ValueText)
        {
            case "1":
                subjectype = "and Orientation ='Y' ";
                break;
            case "2":
                subjectype = "and Professional ='Y' ";
                break;
            case "3":
                subjectype = "and Management ='Y' ";
                break;
            case "4":
                subjectype = "and Quality ='Y' ";
                break;
            case "5":
                subjectype = "and EHS ='Y' ";
                break;
            default:
                subjectype = "";
                break;
        }

        
        if (inout.Equals(""))
        {
            inout = "N";
        }

        if (inout.Equals("I")) //內訓可選內外部講師
        {
            conditions = "("; 
        }
        else 
        {
            conditions = "(LecturerSource = '外部' and "; 
        }

        LecturerGUID.whereClause = conditions + " CompanyCode = '" + CompanyCode.ValueText + "' "+
        " and (StartDate = '' or StartDate <= '" + sdate + "' )" +  
        " and (EndDate = '' or EndDate >= '" + sdate + "') "+
        subjectype + " )";
    }

    
    //學員開窗前
    protected bool DataListTrainee_BeforeOpenWindow(DataObject objects, bool isAddNew)
    {
        DataObjectSet set = DataListTrainee.dataSource;
        ArrayList traineeInfo = new ArrayList();
        
        for(int i=0; i< set.getAvailableDataObjectCount(); i++)
        {
            DataObject obj = set.getAvailableDataObject(i);
            traineeInfo.Add(obj.getData("GUID") + "," +  obj.getData("id"));
        }
        setSession((string)Session["UserID"], "TraineeInfo", traineeInfo);
        
        return true;
    }

   
    //結案日期改變
    protected void CloseDate_DateTimeClick(string values)
    {
        Boolean ifCanModify = true;
        if (values == null || values.Equals(""))
            Status.ValueText = "InProcess";
        else
        {             
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
            
            string sql = "SELECT LecturerSource  FROM SmpTSLecturer where GUID= '" + LecturerGUID.GuidValueText + "'";
            string source = (string)engine.executeScalar(sql);              
            engine.close();

            if (InOut.ValueText.Equals("I") && source.Equals("I") && (DataListMaterial.dataSource.getAvailableDataObjectCount() == 0))
            {     
                MessageBox("無法結案! 內訓課程且為內訓講師，請連結KM文件!");
                CloseDate.ValueText = "";
                Status.ValueText = "InProcess";
                ifCanModify = true;                 
            }
            else if (DataListTrainee.dataSource.getAvailableDataObjectCount() == 0)
            {
                MessageBox("無法結案! 請輸入學員資料!");
                CloseDate.ValueText = "";
                Status.ValueText = "InProcess";
                ifCanModify = true;   
            }
            else
            {
                Status.ValueText = "Closed";
                ifCanModify = false;
            }
           
        }

        setReadOnly(ifCanModify);      
    }

    //上課時數為數字
    protected void Hours_TextChanged(object sender, EventArgs e)
    {
        double n;
        if (!double.TryParse(Hours.ValueText, out n)) 
        {
            MessageBox("上課時數需為數字格式");
            Hours.ValueText = "";
            Hours.Focus();
        }
    }

    //field read only
    protected void setReadOnly(Boolean ifCanModify)
    {
        bool isNew = (bool)getSession("isNew");
        if (!isNew)
        {   
            BriefIntro.ReadOnly =!ifCanModify;
            Way.ReadOnly = !ifCanModify;
            StartDate.ReadOnly = !ifCanModify;                        
            /*
            EndDate.ReadOnly = !ifCanModify;
            StartTime.ReadOnly = !ifCanModify;
            EndTime.ReadOnly = !ifCanModify;
            Place.ReadOnly = !ifCanModify;
            */
            Hours.ReadOnly = !ifCanModify;
            LecturerGUID.ReadOnly = !ifCanModify;
           // UndertakerGUID.ReadOnly = !ifCanModify;
            Remark.ReadOnly = !ifCanModify;
            DataListTrainee.ReadOnly = !ifCanModify;
            DataListMaterial.ReadOnly = !ifCanModify;
            DataListAttachment.ReadOnly = !ifCanModify;
            ButtonImport.Enabled = ifCanModify;
            ButtonUpload.Enabled = ifCanModify;

            Boolean ifIn = InOut.ValueText.Equals("I") ? true : false;            
            if (ifIn)
            {
                //內訓
                CbWrittenTest.ReadOnly = !ifCanModify;
                CbImplement.ReadOnly = !ifCanModify;
                CbSatisfaction.ReadOnly = !ifCanModify;
                CbInOther.ReadOnly = !ifCanModify;
                if (ifCanModify)
                    InOtherDesc.ReadOnly = !CbInOther.Checked;
                else
                    InOtherDesc.ReadOnly = !ifCanModify;
            }
            else
            {
                //外訓
                CbPresentation.ReadOnly = !ifCanModify;
                CbCertificate.ReadOnly = !ifCanModify;
                CbReport.ReadOnly = !ifCanModify;
                CbOJT.ReadOnly = !ifCanModify;
                CbOutOther.ReadOnly = !ifCanModify;
                if (ifCanModify)
                    OutOtherDesc.ReadOnly = !CbOutOther.Checked;
                else
                    OutOtherDesc.ReadOnly = !ifCanModify;
            }
            
        }
        DeleteButton.ReadOnly = Status.ValueText.Equals("Closed") ? true : false;
        DeleteButton.Display = Status.ValueText.Equals("Closed") ? false : true;
    }

 
}