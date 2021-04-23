using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;

using System.IO;
using System.Xml;
using WebServerProject.auth;
using NPOI.HSSF.UserModel;

public partial class SmpProgram_System_Form_SPTS001_Form : SmpAdFormPage  //SmpBasicFormPage
{

    protected override void init()
    {	
		ProcessPageID = "SPTS001"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPTS001.SmpTSInHouseFormAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPTS";			
    }


    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
		SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
		string userId = (string)Session["UserID"];
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string[,] ids = null;      
        bool isAddNew = base.isNew();
		SheetNo.ReadOnly = true;
		WrittenTest.Display = false;
		Implement.Display = false;
		Satisfaction.Display = false;
		EndDate.Display = false;

		try{		
			//公司別		
			string[,] idsCompany = null;
			idsCompany = new string[,]{
				{"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spts001_form_aspx.language.ini", "message", "smp", "新普科技")},
				{"TE",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_sptsd001_form_aspx.language.ini", "message", "te", "嘉普科技")},
				{"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_sptsd001_form_aspx.language.ini", "message", "tp", "中普科技")}
			};
			CompanyCode.setListItem(idsCompany);
			string[] values = base.getUserInfoById(engine,(string)Session["UserId"]);
			CompanyCode.ValueText = values[5];
			//CompanyCode.ReadOnly = true;
			
			//申請部門
			ApplyDeptGUID.clientEngineType = engineType;
			ApplyDeptGUID.connectDBString = connectString;
			if (ApplyDeptGUID.ValueText.Equals(""))
			{
				ApplyDeptGUID.ValueText = si.submitOrgID;
				ApplyDeptGUID.doValidate();
			}
			
	        //申請人員
			ApplicantGUID.clientEngineType = engineType;
			ApplicantGUID.connectDBString = connectString;            
			if (ApplicantGUID.ValueText.Equals(""))
			{
				ApplicantGUID.ValueText = si.fillerID; //預設帶出登入者
				ApplicantGUID.doValidate(); //帶出人員開窗元件中的人員名稱
			}
			
			//審核人員1.2
			CheckBy1GUID.clientEngineType = engineType;
			CheckBy1GUID.connectDBString = connectString;
			CheckBy2GUID.clientEngineType = engineType;
			CheckBy2GUID.connectDBString = connectString;
			
			//課程代號
            SchDetailGUID.clientEngineType = engineType;
            SchDetailGUID.connectDBString = connectString; 
			
	        //課程來源
            ids = new string[,]{
                    {"",""},
                    {"1",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spts001_form_aspx.language.ini", "message", "1", "年度計劃")},                
                    {"2",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spts001_form_aspx.language.ini", "message", "2", "新增申請")}                                                
                };
            SchSource.setListItem(ids);
            SchSource.ReadOnly =true;

            //課程來源
            ids = new string[,] {
                {"",""},
                {"I",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spts001_form_aspx.language.ini", "message", "I", "內訓")},
                {"O",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spts001_form_aspx.language.ini", "message", "O", "外訓")}
            };
            InOut.setListItem(ids);
            InOut.ReadOnly = true;

            //課程類別
            ids = new string[,]{ 
                {"",""},
                {"1",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spts001_form_aspx.language.ini", "message", "1", "新人訓練")},
                {"2",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spts001_form_aspx.language.ini", "message", "2", "專業職能")},
                {"3",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spts001_form_aspx.language.ini", "message", "3", "管理職能")},
                {"4",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spts001_form_aspx.language.ini", "message", "4", "品質管理")},
                {"5",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spts001_form_aspx.language.ini", "message", "5", "安全衛生")} 
            };
            SubjectType.setListItem(ids);
            SubjectType.ReadOnly = true;

            //教授方式
            ids = new string[,]{ 
                    {"1",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spts001_form_aspx.language.ini", "message", "1", "面授")},
                 };
            Way.setListItem(ids);
            Way.ValueText = "1";

            //講師
            //LecturerGUID.ReadOnly = true;
            LecturerGUID.clientEngineType = engineType;
            LecturerGUID.connectDBString = connectString; 

            //開課單位
            DeptGUID.clientEngineType = engineType;
            DeptGUID.connectDBString = connectString;
			
			//學員清單查詢
			CheckDept.clientEngineType = engineType;
			CheckDept.connectDBString = connectString;
			CheckUser.clientEngineType = engineType;
			CheckUser.connectDBString = connectString;
			
			//自動帶出年度
			if (isAddNew)
			{
				CourseYear.ValueText = DateTime.Now.ToString("yyyy");//取年
				//StartTime.ValueText = "08:00";
				//EndTime.ValueText = "17:00";
				//Hours.ValueText = "8";
				//UserQty.ValueText = "0";
			}
		}
        catch (Exception e)
        {
            writeLog(e);
        }

        //學員        
        DataObjectSet traineeSet = null;
        if (isAddNew)
        {
            traineeSet = new DataObjectSet();
            traineeSet.isNameLess = true;
            traineeSet.setAssemblyName("WebServerProject");
            traineeSet.setChildClassString("WebServerProject.form.SPTS001.SmpTSInHouseTrainee");
            traineeSet.setTableName("SmpTSInHouseTrainee");
            traineeSet.loadFileSchema();
            objects.setChild("SmpTSInHouseTrainee", traineeSet);
        }
        else
        {
            traineeSet = objects.getChild("SmpTSInHouseTrainee");
        }
		
        DataListTrainee.dataSource = traineeSet;
        DataListTrainee.HiddenField = new string[] { "GUID", "InHouseFormGUID", "EmployeeGUID","DeptGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        DataListTrainee.reSortCondition("工號", DataObjectConstants.ASC);
        DataListTrainee.updateTable();  
		DataListTrainee.NoAdd = true;
		DataListTrainee.NoModify = true;		
		if (!isAddNew)
        {
			DataListTrainee.isShowAll = true;
		}
		
        //教材 
        ids = new string[,]{ 
                {"KM","KM"}   
                    };					
        Source.setListItem(ids);   
              
        MaterialGUID.clientEngineType = engineType;
        MaterialGUID.connectDBString = connectString;		
		DataObjectSet materialSet = null;
		if (isAddNew)
        {
            materialSet = new DataObjectSet();
            materialSet.isNameLess = true;
            materialSet.setAssemblyName("WebServerProject");
            materialSet.setChildClassString("WebServerProject.form.SPTS001.SmpTSInHouseMaterial");
            materialSet.setTableName("SmpTSInHouseMaterial");
            materialSet.loadFileSchema();
            objects.setChild("SmpTSInHouseMaterial", materialSet);            
        }	
        else
        {
            materialSet = objects.getChild("SmpTSInHouseMaterial");
        }
        
        DataListMaterial.dataSource = materialSet;
        DataListMaterial.HiddenField = new string[] { "GUID", "InHouseFormGUID", "MaterialGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        DataListMaterial.updateTable();
		if (isAddNew)
        {
			DataListMaterial.NoAdd = false;
			DataListMaterial.NoModify = false;
		}else{
			DataListMaterial.NoAdd = true;
			DataListMaterial.NoModify = true;
			DataListMaterial.isShowAll = true;
		}		
				
		if (isAddNew)
        {
            PrintButton.Display = false;
        }else{
			PrintButton.Enabled = true;
		}
		
        //改變工具列順序
        base.initUI(engine, objects);
    }

    protected override void showData(AbstractEngine engine, DataObject objects)
    {
		base.showData(engine, objects);
        string actName = Convert.ToString(getSession("ACTName"));
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);

        //表單欄位
        bool isAddNew = base.isNew();
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號, 主旨含單號
        base.showData(engine, objects);
		SheetNo.ValueText = objects.getData("SheetNo"); //傳入序號,
		
		//公司别
		CompanyCode.ValueText = objects.getData("CompanyCode");
		CompanyCode.ReadOnly = true;

        //申請單位
        ApplyDeptGUID.ValueText = objects.getData("ApplyDeptGUID");
        ApplyDeptGUID.doGUIDValidate();
        
		//申請人員
        ApplicantGUID.GuidValueText = objects.getData("ApplicantGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        ApplicantGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     
		
		//審核人員1
        string checkBy1GUID = objects.getData("CheckBy1GUID");
        if (!checkBy1GUID.Equals(""))
        {
            CheckBy1GUID.GuidValueText = checkBy1GUID; //將值放入人員開窗元件中, 資料庫存放GUID
            CheckBy1GUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
		//審核人員2
        string checkBy2GUID = objects.getData("CheckBy2GUID");
        if (!checkBy2GUID.Equals(""))
        {
            CheckBy2GUID.GuidValueText = checkBy2GUID; //將值放入人員開窗元件中, 資料庫存放GUID
            CheckBy2GUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }

		CourseYear.ValueText = objects.getData("CourseYear");
						
		//課程名稱
        SchDetailGUID.clientEngineType = (string)Session["engineType"];
        SchDetailGUID.connectDBString = (string)Session["connectString"]; 
		SchDetailGUID.GuidValueText = objects.getData("SchDetailGUID");      
        SchDetailGUID.doGUIDValidate();

		//講師
        LecturerGUID.clientEngineType = (string)Session["engineType"];
        LecturerGUID.connectDBString = (string)Session["connectString"]; 
		string lecturerGUID = objects.getData("LecturerGUID");
        if (!lecturerGUID.Equals(""))
        {
            LecturerGUID.GuidValueText = lecturerGUID; //將值放入人員開窗元件中, 資料庫存放GUID
            LecturerGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
		//LecturerGUID.GuidValueText = objects.getData("LecturerGUID");      
        //LecturerGUID.doGUIDValidate();
		writeLog("LecturerGUID: "+objects.getData("LecturerGUID"));
		writeLog("SubjectType: "+objects.getData("SubjectType"));
		
                
        //開課單位
        DeptGUID.clientEngineType = (string)Session["engineType"];
        DeptGUID.connectDBString = (string)Session["connectString"];
		DeptGUID.GuidValueText = objects.getData("DeptGUID");      
        DeptGUID.doGUIDValidate();
		
		StartDate.ValueText = objects.getData("StartDate");
		EndDate.ValueText = objects.getData("EndDate");
		StartTime.ValueText = objects.getData("StartTime");
		EndTime.ValueText = objects.getData("EndTime");
		Hours.ValueText = objects.getData("Hours");
		Remark.ValueText = objects.getData("Remark");
		SchSource.ValueText = objects.getData("SchSource");
		InOut.ValueText = objects.getData("InOut");
		SubjectType.ValueText = objects.getData("SubjectType");
		Way.ValueText = objects.getData("Way");
		Place.ValueText = objects.getData("Place");
		UserQty.ValueText = objects.getData("UserQty");
		
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
		WrittenTest.Display = false;
		Implement.Display = false;
		Satisfaction.Display = false;
		
		//學員        
        DataObjectSet traineeSet = null;
        if (isAddNew)
        {
            traineeSet = new DataObjectSet();
            traineeSet.isNameLess = true;
            traineeSet.setAssemblyName("WebServerProject");
            traineeSet.setChildClassString("WebServerProject.form.SPTS001.SmpTSInHouseTrainee");
            traineeSet.setTableName("SmpTSInHouseTrainee");
            traineeSet.loadFileSchema();
            objects.setChild("SmpTSInHouseTrainee", traineeSet);
        }
        else
        {
            traineeSet = objects.getChild("SmpTSInHouseTrainee");
        }
        DataListTrainee.dataSource = traineeSet;
        DataListTrainee.HiddenField = new string[] { "GUID", "InHouseFormGUID", "EmployeeGUID","DeptGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        DataListTrainee.reSortCondition("工號", DataObjectConstants.ASC);
        DataListTrainee.updateTable();        
		
		//km教材        
        DataObjectSet materialSet = null;
        if (isAddNew)
        {
            materialSet = new DataObjectSet();
            materialSet.isNameLess = true;
            materialSet.setAssemblyName("WebServerProject");
            materialSet.setChildClassString("WebServerProject.form.SPTS001.SmpTSInHouseMaterial");
            materialSet.setTableName("SmpTSInHouseMaterial");
            materialSet.loadFileSchema();
            objects.setChild("SmpTSInHouseMaterial", materialSet);
        }
        else
        {
            materialSet = objects.getChild("SmpTSInHouseMaterial");
            for (int i = 0; i < materialSet.getAvailableDataObjectCount(); i++)
            {
                DataObject obj = materialSet.getAvailableDataObject(i);
                string docUrl = getKmUrl(obj);
                obj.setData("MaterialURL", docUrl);
            }
        }
        DataListMaterial.dataSource = materialSet;
        DataListMaterial.HiddenField = new string[] { "GUID", "InHouseFormGUID", "MaterialGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        DataListMaterial.reSortCondition("文件編號", DataObjectConstants.ASC);
        DataListMaterial.setColumnStyle("MaterialURL", 80, DSCWebControl.GridColumnStyle.LEFT);
        DataListMaterial.updateTable();	
		
		if (isAddNew)
        {
			WrittenTest.Display = false;
			Implement.Display = false;
			Satisfaction.Display = false;
			SheetNo.ReadOnly = true;
		}else{
			Place.ReadOnly = true;
			UserQty.ReadOnly = true;
			CheckUser.ReadOnly = true;
			CheckDept.ReadOnly = true;
			SheetNo.ReadOnly = true;
			Subject.ReadOnly = true;
			ApplyDeptGUID.ReadOnly = true;
			ApplicantGUID.ReadOnly = true;
			CheckBy1GUID.ReadOnly = true;
			CheckBy2GUID.ReadOnly = true;
			CourseYear.ReadOnly = true;
			LecturerGUID.ReadOnly = true;
			SchSource.ReadOnly = true;
			SchDetailGUID.ReadOnly = true;
			DeptGUID.ReadOnly = true;
			StartDate.ReadOnly = true;
			EndDate.ReadOnly = true;
			StartTime.ReadOnly = true;
			EndTime.ReadOnly = true;
			Way.ReadOnly = true;
			Hours.ReadOnly = true;
			CbWrittenTest.ReadOnly = true;
			CbImplement.ReadOnly = true;
			CbSatisfaction.ReadOnly = true;
			Remark.ReadOnly = true;
			Place.ReadOnly = true;
			SearchButton.Enabled = false;
			ClearButton.Enabled = false;
			DataListMaterial.NoAdd = true;
			DataListMaterial.NoModify = true;
			DataListMaterial.NoDelete = true;
			DataListTrainee.NoAdd = true;
			DataListTrainee.NoModify = true;
			DataListTrainee.NoDelete = true;
		}
		
		if (actName.Equals("教育訓練管理單位")){
			Hours.ReadOnly = false;
			DataListMaterial.NoAdd = false;
			DataListMaterial.NoModify = false;
			DataListMaterial.NoDelete = false;
			DataListTrainee.NoAdd = false;
			DataListTrainee.NoModify = false;
			DataListTrainee.NoDelete = false;
			Way.ReadOnly = false;
			Hours.ReadOnly = false;
			CbWrittenTest.ReadOnly = false;
			CbImplement.ReadOnly = false;
			CbSatisfaction.ReadOnly = false;
			Remark.ReadOnly = false;
			Place.ReadOnly = false;
			SearchButton.Enabled = false;
			ClearButton.Enabled = false;
			StartDate.ReadOnly = false;
			StartTime.ReadOnly = false;
			EndTime.ReadOnly = false;
			CheckUser.ReadOnly = false;
			CheckDept.ReadOnly = false;
			SearchButton.Enabled = true;
			ClearButton.Enabled = true;
		}

    }
        
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
		try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");            
            bool isAddNew = base.isNew(); //base 父類別

            if (isAddNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("Subject", Subject.ValueText);
				objects.setData("CompanyCode", CompanyCode.ValueText);
                objects.setData("ApplicantGUID", ApplicantGUID.GuidValueText);
				objects.setData("CheckBy1GUID", CheckBy1GUID.GuidValueText);
				objects.setData("CheckBy2GUID", CheckBy2GUID.GuidValueText);
                objects.setData("ApplyDeptGUID", ApplyDeptGUID.GuidValueText);
				objects.setData("CourseYear", CourseYear.ValueText);
				objects.setData("SchDetailGUID", SchDetailGUID.GuidValueText);
				objects.setData("SubjectNo", SchDetailGUID.ValueText);
				objects.setData("SubjectName", SchDetailGUID.ReadOnlyValueText);
				objects.setData("LecturerGUID", LecturerGUID.GuidValueText);
				objects.setData("Lecturer", LecturerGUID.ValueText);
				objects.setData("DeptGUID", DeptGUID.GuidValueText);
				objects.setData("DeptId", DeptGUID.ValueText);
				objects.setData("DeptName", DeptGUID.ReadOnlyValueText);
				
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("DATA_STATUS", "Y");
                //主旨含單號
                base.saveData(engine, objects);
            }
			objects.setData("StartDate", StartDate.ValueText);
			objects.setData("EndDate", EndDate.ValueText);
			objects.setData("StartTime", StartTime.ValueText);
			objects.setData("EndTime", EndTime.ValueText);
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
			objects.setData("WrittenTest", WrittenTest.ValueText);
			objects.setData("Implement", Implement.ValueText);
			objects.setData("Satisfaction", Satisfaction.ValueText);

			objects.setData("SchSource", SchSource.ValueText);
			objects.setData("InOut", InOut.ValueText);
			objects.setData("SubjectType", SubjectType.ValueText);
			objects.setData("Way", Way.ValueText);
			objects.setData("Hours", Hours.ValueText);
			objects.setData("Place", Place.ValueText);
			objects.setData("UserQty", UserQty.ValueText);
			objects.setData("Remark", Remark.ValueText);
            setSession("IsSetFlow", "Y");		
			objects.setData("Hours", Hours.ValueText);			
			
			//學員
			DataObjectSet traineeSet = DataListTrainee.dataSource;
			for (int i = 0; i < traineeSet.getAvailableDataObjectCount(); i++)
			{
				traineeSet.getAvailableDataObject(i).setData("InHouseFormGUID", objects.getData("GUID"));
			}

			//教材
			DataObjectSet materialSet = DataListMaterial.dataSource;
			for (int i = 0; i < materialSet.getAvailableDataObjectCount(); i++)
			{
				materialSet.getAvailableDataObject(i).setData("InHouseFormGUID", objects.getData("GUID"));
			}
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        
    }

    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
		bool result = true;
        string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));
        string schDetailGUID = objects.getData("SchDetailGUID");
		string inOut = objects.getData("InOut");
		string lecturer = objects.getData("LecturerGUID");
		string sDate = objects.getData("StartDate");
		string eDate = objects.getData("EndDate");
		string subjectType = objects.getData("SubjectType");
		int cnt = 0;
		bool isAddNew = base.isNew(); //base 父類別

        if (isAddNew)
        {		
			//審核人一二不可為申請人
			if (ApplicantGUID.ValueText.Equals(CheckBy1GUID.ValueText) || ApplicantGUID.ValueText.Equals(CheckBy2GUID.ValueText))
			{
				strErrMsg += "審核人不可為申請人!\n";
			}

			//審核人不可請假人的直屬主管
			//開課單位部門主管
			string orgUnitManagerId = "";
			string[] orgUnitInfo = base.getOrgUnitInfo(engine, DeptGUID.GuidValueText);
			if (!orgUnitInfo[3].Equals(""))
			{
				 orgUnitManagerId = orgUnitInfo[3];
			}		
			if (orgUnitManagerId.Equals(CheckBy1GUID.ValueText))
			{
				strErrMsg += "審核人1不需填寫開課部門主管.系統會自動判斷!\n";
			}
			if (orgUnitManagerId.Equals(CheckBy2GUID.ValueText))
			{
				strErrMsg += "審核人2不需填寫開課部門主管.系統會自動判斷!\n";
			}
		}
		
		//資料是否重覆,計劃代號不可以重覆
        string sql = "select count(*) cnt from SmpTSCourseForm where CourseYear = '" + CourseYear.ValueText + "' " +
              "and CompanyCode ='" + CompanyCode.ValueText + "' " +
              "and SchDetailGUID = '" + SchDetailGUID.GuidValueText + "' AND Status <> 'Cancelled' ";
        cnt = (int)engine.executeScalar(sql);
        if (cnt > 0)
        {
            strErrMsg += "課程[" + SchDetailGUID.ReadOnlyValueText + "]記錄已存在，不可重覆!\\n";
        }
		
		if (!CbWrittenTest.Checked && !CbImplement.Checked && !CbSatisfaction.Checked)
		{
			strErrMsg += "請選擇內訓評核方式!\\n";
		}

        //講師
		
        //if (objects.getData("LecturerGUID").Equals(""))
		if (LecturerGUID.ValueText.Equals(""))
        {
            strErrMsg += "請輸入講師資料!\\n";
        }else{
			string subjectype = "";			
			string conditions = "";
		
			switch (subjectType)
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
			sql = " select count(*) cnt from SmpTSLecturerListV where  CompanyCode = '" + CompanyCode.ValueText 
			    + "' and StartDate<='" + StartDate.ValueText + "'  and (EndDate>='" + StartDate.ValueText 
				+ "' or EndDate is null) and Lecturer='" + LecturerGUID.ValueText + "' ";
			cnt = (int)engine.executeScalar(sql);
			if (cnt < 1)
			{
				strErrMsg += " 講師[" + LecturerGUID.ValueText + "]的有效時間不在上課日期內，請與HR確認講師資料!\\n";
			}
		}			
        
		decimal hours = 0;
        bool isDecimal = false;
        //上課時數不可為零
        if (!Hours.ValueText.Equals(""))
        {
            isDecimal = decimal.TryParse(Hours.ValueText, out hours);
            if (!isDecimal)
            {
                strErrMsg += Hours.ValueText + "\n";
            }
        }
        if (hours <= 0)
        {
            strErrMsg += "上課時數必需大於零!\n";
        }
		      
		//檢查 學員
        DataObjectSet chkTrainee = DataListTrainee.dataSource;
        if (chkTrainee.getAvailableDataObjectCount().Equals(0))
        {
            strErrMsg += "請維護學員資料!\n";
        }
		
		//檢查 課程教材 , 外部講師不限制須輸入課程教材
		sql = " select count(*) cnt from SmpTSLecturerListV where  CompanyCode = '" + CompanyCode.ValueText 
			+ "' and Lecturer='" + LecturerGUID.ValueText + "' and LecturerSource='內部' ";
		cnt = (int)engine.executeScalar(sql);
		if (cnt > 0)
		{
			DataObjectSet chkMaterial = DataListMaterial.dataSource;
			if (chkMaterial.getAvailableDataObjectCount().Equals(0))
			{
				strErrMsg += "請維護課程教材資料!\n";
			}
		}
		
		
		//設定主旨
		if (strErrMsg.Equals(""))
        {
			//if (Subject.ValueText.Equals(""))
			//{           
			string subject = "【" +CourseYear.ValueText+ "年度 "+ CompanyCode.ValueText +" 內訓申請 申請部門:"+ ApplyDeptGUID.ReadOnlyValueText +" - 申請人員：" + ApplicantGUID.ReadOnlyValueText + "】";
			if (Subject.ValueText.Equals(""))
			{
					Subject.ValueText = subject;
			}
			//}
		}

        if (!strErrMsg.Equals(""))
        {
            pushErrorMessage(strErrMsg);
            result = false;
        }
        
        return result;        
    }

    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
		string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"]; //填表人, 登入者
        si.fillerName = (string)Session["UserName"]; //填表人姓名
        si.fillerOrgID = depData[0]; 
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"]; //表單關系人
        //si.ownerID = OriginatorGUID.ValueText;
        si.ownerName = (string)Session["UserName"];
        //si.ownerName = OriginatorGUID.ReadOnlyValueText;
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0]; //發起單位代號
        si.objectGUID = objects.getData("GUID");

        //MessageBox("initSubmitInfo");
        return si;
    }

    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
		//writeLog("getSubmitInfo start");
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);
        //string[] depDataRealtionship = getDeptInfo(engine, (string)OriginatorGUID.GuidValueText);

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

    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
		Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID); //自動編號設定作業
        return hs;
    }

    //設定表單參數
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
		string xml = "";
		string result = "";
		string sql = "";
		int cnt = 0;
		string actName = Convert.ToString(getSession("ACTName"));
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            
            //填表人
            string creatorId = si.fillerID;
            
            //申請人員
			//簽核流程目前是簽到申請人的部門主管，建議應改由開課單位的部門主管簽核 (因為很多單位是由助理跨部門key單)
            string applicantId = ApplicantGUID.ValueText;
            
            //申請人員的主管
            string applicantGUID = ApplicantGUID.GuidValueText;
            string[] values = base.getUserManagerInfo(engine, applicantGUID);
            string managerGUID = values[0];
            
            values = base.getUserInfo(engine, managerGUID);
            string managerId = values[0];
			
			//開課單位部門主管
			string orgUnitManagerId = "";
			string[] orgUnitInfo = base.getOrgUnitInfo(engine, DeptGUID.GuidValueText);
            if (!orgUnitInfo[3].Equals(""))
            {
                orgUnitManagerId = orgUnitInfo[3];
            }
			//當填單人=部門主管時, 則簽該人員直屬主管
			if (orgUnitManagerId.Equals(creatorId))
            {
                orgUnitManagerId = managerId;
            }
            
			//通知學員及主管
			string notifier = "";
			//主管
			string managers = "";
			DataObjectSet set = currentObject.getChild("SmpTSInHouseTrainee");
			for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
			{
				string traineeGUID = set.getAvailableDataObject(i).getData("EmployeeGUID");  //學員GUID				
				string[] traineeId = base.getUserInfo(engine, traineeGUID);
				string[] traineeMgrGUID = null;
				string traineeMgrId = "";
				if (!traineeId[0].Equals(applicantId))
				{
					if (notifier.IndexOf(traineeId[0]) == -1)
					{
						notifier += traineeId[0] + ";";

						traineeMgrGUID = base.getUserManagerInfo(engine, traineeGUID);
						traineeMgrId = traineeMgrGUID[1];
						if (notifier.IndexOf(traineeMgrId) == -1)
						{
							notifier += traineeMgrId + ";";
						}
					}
				}
			}
			
			//檢查 講師, 若是內部講師則一併通知
			sql = " select count(*) cnt from SmpTSLecturerListV where  CompanyCode = '" + CompanyCode.ValueText + "' and Lecturer='" + LecturerGUID.ValueText + "' and LecturerSource='內部' ";
			cnt = (int)engine.executeScalar(sql);
			if (cnt > 0)
			{
				string lecturerGUID = "";
				sql = "select InLecturerGUID from SmpTSLecturer where GUID= '" + LecturerGUID.GuidValueText + "'";
				DataSet ds = engine.getDataSet(sql, "TEMP");
				if (ds.Tables[0].Rows.Count > 0)
				{
					lecturerGUID = ds.Tables[0].Rows[0][0].ToString();
				}
				values = base.getUserInfo(engine, lecturerGUID);
				string lecturerID = values[0];
				notifier += lecturerID + ";";
			}			
			string checkby1 = CheckBy1GUID.ValueText;
			string checkby2 = CheckBy2GUID.ValueText;
			
			//寫入開課紀錄
			try{
				//string actName = Convert.ToString(getSession("ACTName"));			
				if (actName.Equals("教育訓練管理單位"))
				{
					result = insertCourseForm(engine, currentObject);
					if (!result.Equals(""))
					{
						throw new Exception(result);
					}
				}
			}
			catch
			{
				throw new Exception(engine.errorString);
			}
            
			xml += "<SPTS001>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
            xml += "<originator DataType=\"java.lang.String\">" + applicantId + "</originator>";
            xml += "<checkby1 DataType=\"java.lang.String\">" + checkby1 + "</checkby1>";
			xml += "<checkby2 DataType=\"java.lang.String\">" + checkby2 + "</checkby2>";
            xml += "<manager DataType=\"java.lang.String\">" + orgUnitManagerId + "</manager>";
            xml += "<notifier DataType=\"java.lang.String\">" + notifier + "</notifier>";
            xml += "</SPTS001>";
            writeLog("xml: " + xml);	
			
			
			
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        
        //表單代號
		if (result.Equals(""))
		{
			param["SPTS001"] = xml;
			return "SPTS001";
		}else{
			return "發生錯誤! 請連絡MIS人員處理";
		}
    }

    protected void ApplicantGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);               
                
    }

    protected override string beforeSetFlow(AbstractEngine engine, string setFlowXml)
    {
        return setFlowXml;
    }

    /// <summary>
    /// 簽核前程序
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
        return addSignXml;
    }

    /// <summary>
    /// 簽核後程序
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
        string signProcess = Convert.ToString(Session["signProcess"]);
        if (signProcess.Equals("N")) //不同意
        {
            //updateSourceStatus("Terminate");
        }
		
        base.afterSign(engine, currentObject, result);
    }

    /// <summary>
    /// 重辦程序
    /// </summary>    
    protected override void rejectProcedure()
    {
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回關卡 ID        
        //先退回
        base.rejectProcedure();
		if (backActID.ToUpper().Equals("CREATOR") ) //流程之中, 申請人關卡的 ID 值        
        {
            //終止流程
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            base.terminateThisProcess(si.ownerID);
        }
    }


    protected override void reGetProcedure()
    {
        //call oracle function set object workflow status to 'ReGet'
        base.reGetProcedure();
    }

    /// <summary>
    /// 撤銷程序
    /// </summary>
    protected override void withDrawProcedure()
    {
        //call oracle function set object workflow status to 'WithDraw'        
        base.withDrawProcedure();
    }
	
	/// <summary>
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {   
		writeLog("afterApprove start" + result);
		/*
		if (result.Equals("Y"))
        {	
			try{
				string actName = Convert.ToString(getSession("ACTName"));			
				if (actName.Equals("教育訓練管理單位"))
				{
					result = insertCourseForm(engine, currentObject);
					if (!result.Equals(""))
					{
						throw new Exception(result);
					}
				}
			}
			catch
			{
				throw new Exception(engine.errorString);
			}
        }
		*/
		/*
        if (result.Equals("Y"))
        {	
			try{
				string subject = currentObject.getData("Subject");
				string userGUID = (string)Session["UserGUID"];
				string formGUID = currentObject.getData("GUID");
				string histogyGUID = IDProcessor.getID("");
				string companyCode = currentObject.getData("CompanyCode");
				string schDetailGUID = currentObject.getData("SchDetailGUID");
				string courseYear = currentObject.getData("CourseYear");
				string lecturerGUID = currentObject.getData("LecturerGUID");
				string deptGUID = currentObject.getData("DeptGUID");
				string startDate = currentObject.getData("StartDate");
				string endDate = currentObject.getData("EndDate");
				string startTime = currentObject.getData("StartTime");
				string endTime = currentObject.getData("EndTime");
				string hours = currentObject.getData("Hours");
				string place = currentObject.getData("Place");
				string way = currentObject.getData("Way");
				string writtenTest = currentObject.getData("WrittenTest");
				string implement = currentObject.getData("Implement");
				string satisfaction = currentObject.getData("Satisfaction");
				string sheetNo = currentObject.getData("SheetNo");
				string remark = currentObject.getData("Remark");
				string now = DateTimeUtility.getSystemTime2(null);
				string sql = null;
				string effectiveDate = now.Substring(0, 10);
				string authorGUID = currentObject.getData("ApplicantGUID");
				
				writeLog(" sheetNo : " + sheetNo);

				//新增開課紀錄單頭 SmpTSCourseForm
				sql = "insert into SmpTSCourseForm (GUID, CompanyCode, SchDetailGUID, CourseYear, LecturerGUID, DeptGUID, StartDate, EndDate, StartTime, EndTime, Hours, ";
				sql+= " Place, Way, Status, WrittenTest, Implement, Satisfaction, ReocrdSource, RecordNo, Remark, IS_DISPLAY, IS_LOCK, ";
				sql+= " DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME )  ";
				sql+= " select '" + histogyGUID + "' GUID, '" + companyCode + "' CompanyCode , '"+schDetailGUID+"' SchDetailGUID, '" + courseYear + "' CourseYear, '" + lecturerGUID + "' LecturerGUID, ";
				sql+= "'" + deptGUID + "' DeptGUID, '" + startDate+ "','" + endDate +"','" + startTime +"','" + endTime +"','" + hours +"','" + place +"','" + way + "', ";
				sql+= "'InProcess' as Status, '" + writtenTest+ "','" + implement +"','" + satisfaction +"','2' as ReocrdSource ,'" + sheetNo +"' as RecordNo,'" + remark +"', ";
				sql+= "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + authorGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME ";
				if (!engine.executeSQL(sql))
				{
					throw new Exception(engine.errorString);
				}
				
				writeLog(" 新增開課紀錄單頭 SmpTSCourseForm complete! SQL " + sql);
				
				//開課記錄單身-學員 SmpTSCourseTrainee
				sql = "insert into SmpTSCourseTrainee (GUID, CourseFormGUID,EmployeeGUID,DeptGUID,ApplyWay, IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, ";
				sql+= "D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) ";
				sql+= "select  lower(newid()) GUID, '" + histogyGUID + "' CourseFormGUID, EmployeeGUID, DeptGUID, ApplyWay, 'Y' IS_DISPLAY, 'N' IS_LOCK, "; 
				sql+= "'Y' DATA_STATUS, '" + authorGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME from  SmpTSInHouseTrainee ";
				sql+= " where InHouseFormGUID = (select GUID from SmpTSInHouseForm where SheetNo='"+ sheetNo +"') ";
				if (!engine.executeSQL(sql))
				{
					throw new Exception(engine.errorString);
				}
				
				writeLog(" 開課記錄單身-學員 SmpTSCourseTrainee complete! SQL " + sql);
				
				//開課記錄單身-教材 SmpTSCourseMaterial
				sql = "insert into SmpTSCourseMaterial (GUID, CourseFormGUID,Source,MaterialGUID,MaterialRevGUID, IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, ";
				sql+= "D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) ";
				sql+= "select lower(newid()) GUID, '" + histogyGUID + "' CourseFormGUID, Source, MaterialGUID,  'Y' IS_DISPLAY, 'N' IS_LOCK, "; 
				sql+= "'Y' DATA_STATUS, '" + authorGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME from SmpTSInHouseMaterial ";
				sql+= " where InHouseFormGUID = (select GUID from SmpTSInHouseForm where SheetNo='"+ sheetNo +"') ";
				if (!engine.executeSQL(sql))
				{
					throw new Exception(engine.errorString);
				}
				
				writeLog(" 開課記錄單身-教材 SmpTSCourseMaterial complete! SQL " + sql);
			}
			catch
			{
				throw new Exception(engine.errorString);
			}
        }
		*/
        base.afterApprove(engine, currentObject, result);
    }


    protected void ApplicantGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            ApplicantGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
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
    
	/// <summary>
    /// 開啟課程前檢查
    /// </summary>
    protected void SchDetailGUID_BeforeClickButton()
    {
		if (CourseYear.ValueText.Equals(""))
		{
			MessageBox("請先維護開課年度");			
		}

        //非取消課程
        SchDetailGUID.whereClause = "(Cancel = 'N' and CompanyCode = '" + CompanyCode.ValueText + 
                "' and SchYear = '" + CourseYear.ValueText + "' and InOut='I' )";
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
            //TTQS.ValueText = results[0][7];
            //SchNo.ValueText = results[0][8];
        }
        
        //評核方式
        if (!InOut.ValueText.Equals(""))
        {
            Boolean ifIn = InOut.ValueText.Equals("I") ? true : false;
            
            //內訓
            CbWrittenTest.ReadOnly = !ifIn;
            CbImplement.ReadOnly = !ifIn;
            CbSatisfaction.ReadOnly = !ifIn;
        }     

		
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

	 /// <summary>
    /// 講師開窗條件
    /// </summary>   
    protected void LecturerGUID_BeforeClickButton()
    {        
        string subjectype = "";
        string sdate = StartDate.ValueText;
		string edate = EndDate.ValueText;
        string conditions = "";
		
		if (StartDate.ValueText.Equals("")){
			//MessageBox("請先維護上課日期!");		
			Page.Response.Write("alert('請先維護上課日期');");  
		}		
		else
		{		
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

			LecturerGUID.whereClause =  " ( CompanyCode = '" + CompanyCode.ValueText + "' "+
										" and '" + StartDate.ValueText + "' between StartDate and EndDate " +  
										//" and (StartDate = '' or StartDate <= '" + sdate + "' )" +  
										//" and (EndDate = '' or EndDate >= '" + edate + "') "+
										subjectype + " )";
			
			//if (sdate.Equals(""))
			//{
			//	LecturerGUID.whereClause =  " ( CompanyCode = '" + CompanyCode.ValueText + "' and getdate() between StartDate and EndDate " + subjectype + " )";
			//}
		}
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
	
	
	protected void StartTime_DateTimeClick(string values)
    {
        string strErrMsg = "";
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
           
        }
        //重新確認請假日期是否跨天
        //IsIncludeDateEve.ValueText = getIsIncludeDateEve();

        //重算時數
		string hours = getHours();
		Hours.ValueText = Convert.ToString(hours);
    }
	

    /// <summary>
    /// 截止日期變更重算時數
    /// </summary>
    /// <param name="values"></param>
    protected void EndTime_DateTimeClick(string values)
    {
        string strErrMsg = "";
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            
        }
        //重新確認請假日期是否跨天
        //IsIncludeDateEve.ValueText = getIsIncludeDateEve();
        
        //重算時數
        string hours = getHours();
        Hours.ValueText = Convert.ToString(hours);
    }
	
	//計算時數
	private string getHours()
    {
        string hours = Hours.ValueText;
		string hours_min = "";
		string tempValue = "";
		int tempLen = 0;
		int startTime = Convert.ToInt32(StartTime.ValueText.Replace(":", ""));
        int endTime = Convert.ToInt32(EndTime.ValueText.Replace(":", ""));
		tempValue = Convert.ToString((int)(Convert.ToDouble(endTime - startTime)) );
		tempLen = tempValue.Length;
		//int sH = Convert.ToInt32(StartTime.ValueText.Replace(":", "").Substring(0,2));
		//int eH = Convert.ToInt32(EndTime.ValueText.Replace(":", "").Substring(0,2));
		//int sM = Convert.ToInt32(StartTime.ValueText.Replace(":", "").Substring(2,2));
		//int eM = Convert.ToInt32(EndTime.ValueText.Replace(":", "").Substring(2,2));
				
		hours = tempValue.Substring(0, tempLen-2);
		//hours_min =  Convert.ToString((int)(Convert.ToDouble(tempValue.Substring(tempLen-2, tempLen)) / 60) ); //tempValue.Substring(tempLen-2, tempLen);  //Convert.ToString((int)(Convert.ToDouble(eM - sM) / 100 * 60) );
		
        //return hours+"."+hours_min;
		return hours;
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
                strErrMsg += lblSource.Text + ": 必需選擇!\\n";
            }

            if (MaterialGUID.GuidValueText.Equals(""))
            {
                strErrMsg += lblMaterialGUID.Text + ": 必需選擇!\\n";
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
            objects.setData("InHouseFormGUID", "TEMP");
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
    /// 學員資料顯示
    /// </summary>
    /// <param name="objects"></param>
    protected void DataListTrainee_ShowRowData(DataObject objects)
    {
        
    }

    /// <summary>
    /// 教材資料儲存
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    /// <returns></returns>
    protected bool DataListTrainee_SaveRowData(DataObject objects, bool isNew)        
    {
        string strErrMsg = "";
        try
        {
            if (CheckDept.ValueText.Equals("") && CheckUser.ValueText.Equals(""))
            {
                strErrMsg += "學員 必需選擇!\\n";
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
            objects.setData("InHouseFormGUID", "TEMP");
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
        //objects.setData("EmployeeGUID", EmployeeGUID.ValueText);
        //objects.setData("DeptGUID", DeptGUID.GuidValueText);
        //objects.setData("ApplyWay", ApplyWay.ValueText);
        return true;
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
	
	protected void SearchButton_Click(object sender, EventArgs e)
    {
		int count = 0;
        decimal totalAmount = 0;
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            string inHouseUser = CheckUser.ValueText;
            string inHouseDept = CheckDept.ValueText;
			string companyName = CompanyCode.ValueText;
            string whereCondition = "";
			
			if ((inHouseUser.Equals("")) && (inHouseDept.Equals(""))) 
			{
				MessageBox("請先選取使用者或部門");
			}
			else
			{			
				if (!inHouseUser.Equals(""))
				{
					whereCondition += " and empNumber = '" + inHouseUser + "' ";
				}
				if (!inHouseDept.Equals(""))
				{
					whereCondition += " and deptId = '" + inHouseDept + "' ";
				}
				
				DataObject doo = null;
				DataObjectSet dos = DataListTrainee.dataSource;

				string sql = " select empGUID, deptOID, empNumber, empName, deptId, deptName, '1' wayName from EmployeeInfo ";
					   sql +=" where orgId='" + companyName + "' and (empLeaveDate='' or empLeaveDate is null) ";
					   sql +=whereCondition;
				
				DataSet allBillingList = engine.getDataSet(sql, "TEMP");
				count = allBillingList.Tables[0].Rows.Count;

				for (int i = 0; i < count; i++)
				{
					doo = dos.create();

					doo.setData("GUID", IDProcessor.getID(""));
					doo.setData("InHouseFormGUID", "TEMP");
					doo.setData("EmployeeGUID", allBillingList.Tables[0].Rows[i]["empGUID"].ToString());
					doo.setData("DeptGUID", allBillingList.Tables[0].Rows[i]["deptOID"].ToString());
					doo.setData("id", allBillingList.Tables[0].Rows[i]["empNumber"].ToString());
					doo.setData("userName", allBillingList.Tables[0].Rows[i]["empName"].ToString());
					doo.setData("deptId", allBillingList.Tables[0].Rows[i]["deptId"].ToString());
					doo.setData("deptName", allBillingList.Tables[0].Rows[i]["deptName"].ToString());
					doo.setData("ApplyWay", allBillingList.Tables[0].Rows[i]["wayName"].ToString());
					doo.setData("IS_LOCK", "N");
					doo.setData("IS_DISPLAY", "Y");
					doo.setData("DATA_STATUS", "Y");

					//dos.add(doo);    
					bool strReturn = true; //檢查是否重覆資料
					//資料檢查
					//strReturn = tripDetailRepeatCheck(doo);
					//sw.WriteLine("回傳資料 : " + strReturn);
					if (strReturn)
					{
						dos.add(doo);
						DataListTrainee.NoAdd = true;
						DataListTrainee.dataSource = dos;
						DataListTrainee.HiddenField = new string[] { "GUID", "InHouseFormGUID", "EmployeeGUID", "DeptGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
						DataListTrainee.updateTable();
						calculateTrainee();
					}
				}
							
				engine.close();
			}

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
    }
	
	protected void ClearButton_Click(object sender, EventArgs e)
    {
		AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
			
			CheckUser.ValueText = "";
			CheckUser.GuidValueText = "";
			CheckUser.ReadOnlyValueText = "";
            CheckDept.ValueText = "";
			CheckDept.GuidValueText = "";
			CheckDept.ReadOnlyValueText = "";
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
    }	

	protected void CheckUser_BeforeClickButton()
    {
        CheckUser.whereClause = "(leaveDate='' OR leaveDate IS NULL)";        
    }
	
	protected void DataListTrainee_AddOutline(DataObject objects, bool isNew)
    {		
        calculateTrainee();		
    }

    protected void DataListTrainee_DeleteData()
    {
        calculateTrainee();
    }
	
	//取得學員人數
	private void calculateTrainee()
    {
        decimal total = 0;
        try
        {
            total = DataListTrainee.dataSource.getAvailableDataObjectCount() ;
        }
        catch (Exception e)
        {
            writeLog(e);
        }        
        UserQty.ValueText = total.ToString();
    }
	
	/// <summary>
    /// 新增開課記錄
    /// </summary>
	/// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected string insertCourseForm(AbstractEngine engine, DataObject currentObject)
    {
		string result = "";		
        try{
				string subject = currentObject.getData("Subject");
				string userGUID = (string)Session["UserGUID"];
				string formGUID = currentObject.getData("GUID");
				string histogyGUID = IDProcessor.getID("");
				string companyCode = currentObject.getData("CompanyCode");
				string schDetailGUID = currentObject.getData("SchDetailGUID");
				string courseYear = currentObject.getData("CourseYear");
				string lecturerGUID = currentObject.getData("LecturerGUID");
				string deptGUID = currentObject.getData("DeptGUID");
				string startDate = currentObject.getData("StartDate");
				string endDate = currentObject.getData("EndDate");
				string startTime = currentObject.getData("StartTime");
				string endTime = currentObject.getData("EndTime");
				string hours = currentObject.getData("Hours");
				string place = currentObject.getData("Place");
				string way = currentObject.getData("Way");
				string writtenTest = currentObject.getData("WrittenTest");
				string implement = currentObject.getData("Implement");
				string satisfaction = currentObject.getData("Satisfaction");
				string sheetNo = currentObject.getData("SheetNo");
				string remark = currentObject.getData("Remark");
				string now = DateTimeUtility.getSystemTime2(null);
				string sql = null;
				string effectiveDate = now.Substring(0, 10);
				string authorGUID = currentObject.getData("ApplicantGUID");
				
				//writeLog("insertCourseForm sheetNo : " + sheetNo);
				//sql+= " select '" + histogyGUID + "' GUID, '" + companyCode + "' CompanyCode , '"+schDetailGUID+"' SchDetailGUID, '" + courseYear + "' CourseYear, ";
                //sql+= " (select InLecturerGUID from SmpTSLecturer where GUID='" + lecturerGUID + "') as LecturerGUID, ";
				//sql+= "'" + deptGUID + "' DeptGUID, '" + startDate+ "','" + endDate +"','" + startTime +"','" + endTime +"','" + hours +"','" + place +"','" + way + "', ";
				//sql+= "'InProcess' as Status, '" + writtenTest+ "','" + implement +"','" + satisfaction +"','2' as ReocrdSource ,'" + sheetNo +"' as RecordNo,'" + remark +"', ";
				//sql+= "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + authorGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME ";


				//新增開課紀錄單頭 SmpTSCourseForm
				sql = "insert into SmpTSCourseForm (GUID, CompanyCode, SchDetailGUID, CourseYear, LecturerGUID, DeptGUID, StartDate, EndDate, StartTime, EndTime, Hours, ";
				sql+= " Place, Way, Status, WrittenTest, Implement, Satisfaction, RecordSource, RecordNo, Remark, IS_DISPLAY, IS_LOCK, ";
				sql+= " DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME )  ";
				sql+= " select '" + histogyGUID + "' GUID, '" + companyCode + "' CompanyCode , '"+schDetailGUID+"' SchDetailGUID, '" + courseYear + "' CourseYear, '" + lecturerGUID + "' LecturerGUID, ";
				sql+= "'" + deptGUID + "' DeptGUID, '" + startDate+ "','" + endDate +"','" + startTime +"','" + endTime +"','" + hours +"','" + place +"','" + way + "', ";
				sql+= "'InProcess' as Status, '" + writtenTest+ "','" + implement +"','" + satisfaction +"','2' as ReocrdSource ,'" + sheetNo +"' as RecordNo,'" + remark +"', ";
				sql+= "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + authorGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME ";
				writeLog(" 新增開課紀錄單頭 SmpTSCourseForm complete! SQL " + sql);
				
				if (!engine.executeSQL(sql))
				{
					result += engine.errorString;
					throw new Exception(result);
				}
				
				
				
				//開課記錄單身-學員 SmpTSCourseTrainee
				sql = "insert into SmpTSCourseTrainee (GUID, CourseFormGUID,EmployeeGUID,DeptGUID,ApplyWay, IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, ";
				sql+= "D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) ";
				sql+= "select  lower(newid()) GUID, '" + histogyGUID + "' CourseFormGUID, EmployeeGUID, DeptGUID, ApplyWay, 'Y' IS_DISPLAY, 'N' IS_LOCK, "; 
				sql+= "'Y' DATA_STATUS, '" + authorGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME from  SmpTSInHouseTrainee ";
				sql+= " where InHouseFormGUID = (select GUID from SmpTSInHouseForm where SheetNo='"+ sheetNo +"') ";
				writeLog(" 開課記錄單身-學員 SmpTSCourseTrainee complete! SQL " + sql);
				if (!engine.executeSQL(sql))
				{
					result += engine.errorString;
					throw new Exception(result);
				}
				
				
				
				//開課記錄單身-教材 SmpTSCourseMaterial
				sql = "insert into SmpTSCourseMaterial (GUID, CourseFormGUID,Source,MaterialGUID,MaterialRevGUID, IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, ";
				sql+= "D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) ";
				sql+= "select lower(newid()) GUID, '" + histogyGUID + "' CourseFormGUID, Source, MaterialGUID,'' MaterialRevGUID , 'Y' IS_DISPLAY, 'N' IS_LOCK, "; 
				sql+= "'Y' DATA_STATUS, '" + authorGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME from SmpTSInHouseMaterial ";
				sql+= " where InHouseFormGUID = (select GUID from SmpTSInHouseForm where SheetNo='"+ sheetNo +"') ";
				writeLog(" 開課記錄單身-教材 SmpTSCourseMaterial complete! SQL " + sql);
				if (!engine.executeSQL(sql))
				{
					throw new Exception(engine.errorString);
				}			
				
			}
			catch
			{
				result += engine.errorString;
				throw new Exception(result);				
			}
				
        return result;
    }
	
	private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPTS001.log", true, System.Text.Encoding.Default);
            sw.WriteLine(line);
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (sw != null)
            {
                sw.Close();
            }
        }
    }

	//列印單據
	protected void PrintButton_OnClick(object sender, EventArgs e)
    {
		//MessageBox("SheetNo : " + SheetNo.ValueText);
		Session["SPTS001_SheetNo"] = SheetNo.ValueText;
        string url = "PrintPage.aspx";
	    base.showOpenWindow(url, "列印教育訓練簽到表格", "", "600", "", "", "", "1", "1", "", "", "", "", "750", "", true);
    }	
		

}