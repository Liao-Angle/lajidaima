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
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;

public partial class SmpProgram_System_Form_SPAD009_Form : SmpAdFormPage
{
    protected override void init()
    {	
        ProcessPageID = "SPAD009"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPAD009.SmpTripFormAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPAD";		
    }

    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
		SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        //不顯示於發起單據畫面
        TempSerialNo.Display = false;
		Hours.Display = false;
		ClassCode.Display = false;
		CategoryCode.Display = false;
		IsIncludeHoliday.Display = false;
		IsHrForm.Display = false;
        PayeeFlag.Display = false;  //是否已請款
		SheetNo.ReadOnly = true;
		RefEtagFee.Display = false;  //Etag參考最大請款金額
		
		string[,] ids = null;
        DataSet ds = null;
        int count = 0;
		string sql = null;
            		
		//公司別		
		string[,] idsCompany = null;
        idsCompany = new string[,]{
            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad009_form_aspx.language.ini", "message", "smp", "新普科技")},
            {"TE",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad009_form_aspx.language.ini", "message", "te", "嘉普科技")},
            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad009_form_aspx.language.ini", "message", "tp", "中普科技")}
        };
        CompanyCode.setListItem(idsCompany);
		string[] values = base.getUserInfoById(engine,(string)Session["UserId"]);
        CompanyCode.ValueText = values[5];
        CompanyCode.ReadOnly = true;	
		
		// 20161007 Eva增加 TE
		if (values[5].Equals("TE"))
		{
			LblOtherDesc.Text = "";
			//LblOtherDesc.Text = " ** 嘉普人員至105/12/31止國內出差費請款(發票、收據等)請使用中普統一編號:54542191 ** ";
		}else{
			LblOtherDesc.Text = "";
		}	
        			
        //申請單位
        DeptGUID.clientEngineType = engineType;
        DeptGUID.connectDBString = connectString;
        DeptGUID.ValueText = si.submitOrgID;
        DeptGUID.doValidate();
		
        //申請人員
        OriginatorGUID.clientEngineType = engineType;
        OriginatorGUID.connectDBString = connectString;            
        if (OriginatorGUID.ValueText.Equals(""))
        {
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }
        		
		//審核人
        CheckByGUID.clientEngineType = engineType;
        CheckByGUID.connectDBString = connectString;

		//預設假別為出差假
		CategoryCode.ValueText = "LI01";  //出差假    
		
		//班別 
		ClassCode.ValueText = "SD4";  //常日班(間接)        
		
		//請假含假日, 預設為N
        IsIncludeHoliday.ValueText = "N";

		//是否產生假單
		IsHrForm.Checked = true;  //皆要產生假單		
		IsTripFee.Checked = true; //是否要申報費用
        		
		//時數
        Hours.ValueText = "0";

		TripDate.ValueText = DateTimeUtility.getSystemTime2(null).Substring(0, 10);
		
		//ETC Start
        ds = engine.getDataSet("select 0, '' from SmpETCMileage  union select distinct Serial, Name  From SmpETCMileage order by 1 ", "TEMP");
        count = ds.Tables[0].Rows.Count;
        ids = new string[0 + count, 2];
        ids[0, 0] = "";
        ids[0, 1] = "";

        for (int i = 1; i < count; i++)
        {                
            ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
            ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
        }
        EtcStart.setListItem(ids);
		
		//etc end
		ds = engine.getDataSet("select 0, '' from SmpETCMileage  union select distinct Serial, Name  From SmpETCMileage order by 1 ", "TEMP");
        count = ds.Tables[0].Rows.Count;
        ids = new string[0 + count, 2];
        ids[0, 0] = "";
        ids[0, 1] = "";

        for (int i = 1; i < count; i++)
        {                
            ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
            ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
        }
		EtcEnd.setListItem(ids);
		
		TrafficFee.ReadOnly = true;
        EatFee.ReadOnly = true;
		ParkingFee.ReadOnly = true;
		OtherFee.ReadOnly = true;
		DeptGUID.ReadOnly = true;
		CompanyCode.ReadOnly = true;
		MeetingMinute.ReadOnly = true;  //ORIGINATOR申請人才可維護此欄位, 並開放附件上傳
        EtagFee.ReadOnly = true;
        StartMileage.ReadOnly = true;
        EndMileage.ReadOnly = true;
        MileageSum.ReadOnly = true;
        OilFee.ReadOnly = true;
		EtcEnd.ReadOnly = true;
		EtcStart.ReadOnly = true;
		
		if (base.isNew())
        {
            PrintButton.Display = false;
        }else{
			PrintButton.Enabled = true;
		}
		
		//eTag帳戶查詢連結
		WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string eTagURL = sp.getParam("eTagURL"); //系統參數
        ETCHyperLink.NavigateUrl = eTagURL;
		
		//隱藏欄位
        TempSerialNo.Display = false;

        //改變工具列順序
        base.initUI(engine, objects);
    }

    protected override void showData(AbstractEngine engine, DataObject objects)
    {	
		//writeLog("showData" );
		base.showData(engine, objects);
        string actName = Convert.ToString(getSession("ACTName"));
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);

        //表單欄位
        TempSerialNo.Display = false;
		Hours.Display = false;
		CategoryCode.Display = false;
		ClassCode.Display = false;
		IsIncludeHoliday.Display = false;	
		IsHrForm.Display = false;
        PayeeFlag.Display = false;
        OilFee.ReadOnly = true;
		RefEtagFee.Display = false;  //Etag參考最大請款金額		
		
        bool isAddNew = base.isNew();
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號, 主旨含單號
        base.showData(engine, objects);
		SheetNo.ValueText = objects.getData("SheetNo");
		
		//公司别
		CompanyCode.ValueText = objects.getData("CompanyCode");

        //申請人員
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID

        //申請單位
        DeptGUID.ValueText = objects.getData("DeptGUID");
        DeptGUID.doGUIDValidate();

        //審核人員
        string checkByGUID = objects.getData("CheckByGUID");
        if (!checkByGUID.Equals(""))
        {
            CheckByGUID.GuidValueText = checkByGUID; //將值放入人員開窗元件中, 資料庫存放GUID
            CheckByGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
		
        //班別
        ClassCode.ValueText = objects.getData("ClassCode");
        //假別
        CategoryCode.ValueText = objects.getData("CategoryCode");
        //請假含假日
        IsIncludeHoliday.ValueText = objects.getData("IsIncludeHoliday");
        //出差日期
        TripDate.ValueText = objects.getData("TripDate");
        //起始時間
        StartTime.ValueText = objects.getData("StartTime");
        //截止時間
        EndTime.ValueText = objects.getData("EndTime");
        //時數
        Hours.ValueText = objects.getData("Hours");
		//回傳序號
        TempSerialNo.ValueText = objects.getData("TempSerialNo");
		
		if (objects.getData("IsHrForm").Equals("Y")) { IsHrForm.Checked = true; } else { IsHrForm.Checked = false; }
        if (objects.getData("IsTripFee").Equals("Y")){ IsTripFee.Checked = true; } else { IsTripFee.Checked = false; }
		
		//出差地點
        TripSite.ValueText = objects.getData("TripSite");
		//擬辦事項
		Notes.ValueText = objects.getData("Notes");
		//會議記錄
		MeetingMinute.ValueText = objects.getData("MeetingMinute");
		//申請費用
		TrafficFee.ValueText = objects.getData("TrafficFee");
        EatFee.ValueText = objects.getData("EatFee");
		ParkingFee.ValueText = objects.getData("ParkingFee");
		OtherFee.ValueText = objects.getData("OtherFee");
        EtagFee.ValueText = objects.getData("EtagFee");
		
		EtcStart.ValueText = objects.getData("EtcStart");
		EtcEnd.ValueText = objects.getData("EtcEnd");
		RefEtagFee.ValueText = objects.getData("RefEtagFee");
		
        StartMileage.ValueText = objects.getData("StartMileage");
        EndMileage.ValueText = objects.getData("EndMileage");
        MileageSum.ValueText = objects.getData("MileageSum");
        OilFee.ValueText = objects.getData("OilFee");
		RefEtagFee.ValueText = objects.getData("RefEtagFee");
        
		//產生假單所需資訊
		Hours.ValueText = objects.getData("Hours");
		TempSerialNo.ValueText = objects.getData("TempSerialNo");
		ClassCode.ValueText = objects.getData("ClassCode");
		IsIncludeHoliday.ValueText = objects.getData("IsIncludeHoliday");
		
		if (!isAddNew)
        {
            Subject.ReadOnly = true;
            SheetNo.ReadOnly = true;
            DeptGUID.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            CheckByGUID.ReadOnly = true;
            CompanyCode.ReadOnly = true;
            Hours.ReadOnly = true;
            TempSerialNo.ReadOnly = true;
            ClassCode.ReadOnly = true;
            IsIncludeHoliday.ReadOnly = true;
            IsHrForm.ReadOnly = true;
            IsTripFee.ReadOnly = true;
            TripSite.ReadOnly = true;
            Notes.ReadOnly = true;
            TrafficFee.ReadOnly = true;
            EatFee.ReadOnly = true;
			ParkingFee.ReadOnly = true;
            OtherFee.ReadOnly = true;
			TripDate.ReadOnly = true;
			StartTime.ReadOnly = true;
			EndTime.ReadOnly = true;
			MeetingMinute.ReadOnly = true;
            EtagFee.ReadOnly = true;
            StartMileage.ReadOnly = true;
            EndMileage.ReadOnly = true;
            MileageSum.ReadOnly = true;
            OilFee.ReadOnly = true; 
			EtcEnd.ReadOnly = true;
			EtcStart.ReadOnly = true;
        }
        
        if (actName.Equals("申請人"))
        {
			TrafficFee.ValueText = "0";
            EatFee.ValueText = "0";
			ParkingFee.ValueText = "0";
            OtherFee.ValueText = "0";
            EtagFee.ValueText = "0";
            MileageSum.ValueText = "0";
            OilFee.ValueText = "0";
			StartMileage.ValueText = "0";
            EndMileage.ValueText = "0";
			TrafficFee.ReadOnly = false;
            EatFee.ReadOnly = false;
			ParkingFee.ReadOnly = false;
            OtherFee.ReadOnly = false;	
			MeetingMinute.ReadOnly = false;
            EtagFee.ReadOnly = false;
            StartMileage.ReadOnly = false;
            EndMileage.ReadOnly = false;
            EndTime.ReadOnly = false;
			EtcStart.ReadOnly = false;
            EtcEnd.ReadOnly = false;
        }

		if (actName.Equals("直屬主管"))
        {	
		AddSignButton.Display = true; //允許加簽,
        }
		
		if (!IsTripFee.Checked)
		{
			TrafficFee.ReadOnly = true;
            EatFee.ReadOnly = true;
			ParkingFee.ReadOnly = true;
            OtherFee.ReadOnly = true;
            EtagFee.ReadOnly = true;
            StartMileage.ReadOnly = true;
            EndMileage.ReadOnly = true;
            MileageSum.ReadOnly = true;
            OilFee.ReadOnly = true;
			EtcStart.ReadOnly = true;
            EtcEnd.ReadOnly = true;			
		}        
    }
        
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
		//writeLog("saveData" );
		try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");            
            bool isAddNew = base.isNew(); //base 父類別

            if (isAddNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("Subject", Subject.ValueText);
				objects.setData("CompanyCode", CompanyCode.ValueText); //公司別
                objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
                objects.setData("DeptGUID", DeptGUID.GuidValueText);
                objects.setData("CheckByGUID", CheckByGUID.GuidValueText);
				objects.setData("TripDate", TripDate.ValueText);
				objects.setData("StartTime", StartTime.ValueText);
                objects.setData("EndTime", EndTime.ValueText);
				objects.setData("IsHrForm", "Y");
                if (IsTripFee.Checked) { objects.setData("IsTripFee", "Y"); } else { objects.setData("IsTripFee", "N"); }
                objects.setData("TripSite", TripSite.ValueText);
				objects.setData("Notes", Notes.ValueText);
				
				objects.setData("ClassCode", ClassCode.ValueText);
				objects.setData("IsIncludeHoliday", IsIncludeHoliday.ValueText);
				objects.setData("Hours", Hours.ValueText);
				objects.setData("TempSerialNo", TempSerialNo.ValueText);
				objects.setData("CategoryCode", CategoryCode.ValueText);

                objects.setData("TrafficFee", "0");
                objects.setData("EatFee", "0");
                objects.setData("ParkingFee", "0");
                objects.setData("OtherFee", "0");
                objects.setData("EtagFee", "0");
                objects.setData("OilFee", "0");
                objects.setData("PayeeFlag", "N");
				
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("DATA_STATUS", "Y");
                //主旨含單號
                base.saveData(engine, objects);
            }
            objects.setData("OilFee", OilFee.ValueText);
            objects.setData("TrafficFee", TrafficFee.ValueText);
            objects.setData("EatFee", EatFee.ValueText);
            objects.setData("ParkingFee", ParkingFee.ValueText);
            objects.setData("OtherFee", OtherFee.ValueText);
			objects.setData("CategoryCode", CategoryCode.ValueText);
			objects.setData("TempSerialNo", TempSerialNo.ValueText);
			objects.setData("Hours", Hours.ValueText);
			objects.setData("ClassCode", ClassCode.ValueText);
			objects.setData("MeetingMinute", MeetingMinute.ValueText);
            objects.setData("StartMileage", StartMileage.ValueText);
            objects.setData("EndMileage", EndMileage.ValueText);
            objects.setData("MileageSum", MileageSum.ValueText);
            objects.setData("EtagFee", EtagFee.ValueText);
            objects.setData("EndTime", EndTime.ValueText);            
			objects.setData("EtcStart", EtcStart.ValueText); 
			objects.setData("EtcEnd", EtcEnd.ValueText); 			
            
            //beforeSetFlow
            setSession("IsSetFlow", "Y");
			
		    // 加簽
	        string actName = Convert.ToString(getSession("ACTName").ToString());
	        if (actName.Equals("直屬主管") || actName.Equals("差勤負責人"))
	        {
	            setSession("IsAddSign", "AFTER");
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
        string[] values = base.getUserInfo(engine, OriginatorGUID.GuidValueText);
		string startTime = StartTime.ValueText;
		string endTime = EndTime.ValueText;
		string tripDate = TripDate.ValueText;
		string userGuid = OriginatorGUID.GuidValueText;

        if (actName.Equals(""))
        {
			if (StartTime.ValueText.Equals("") || EndTime.ValueText.Equals(""))
            {
                strErrMsg += "請填寫完整出差時間\n";
            }

            if (TripSite.ValueText.Equals(""))
            {
                strErrMsg += "出差地點不可為空值\n";
            }
            if (Notes.ValueText.Equals(""))
            {
                strErrMsg += "擬辦事項不可為空值\n";
            }
            if (CheckByGUID.ValueText.Equals(OriginatorGUID.ValueText)) 
            {
                strErrMsg += "審核人不可為出差人員\n";
            }
			
			string strStartDateTime = tripDate + " " + startTime + ":00";
			string strEndDateTime = tripDate + " " + endTime + ":00";
			string strHrStartDateTime = tripDate + " " + "07:00" + ":00";
			string strHrEndDateTime = tripDate + " " + "23:59" + ":00";
			DateTime startDateTime = Convert.ToDateTime(strStartDateTime);
			DateTime endDateTime = Convert.ToDateTime(strEndDateTime);
			DateTime hrStartDateTime = Convert.ToDateTime(strHrStartDateTime);
			DateTime hrEndDateTime = Convert.ToDateTime(strHrEndDateTime);
			
			DateTime sDate = Convert.ToDateTime(strStartDateTime);
			DateTime eDate = DateTime.Now.AddDays(-6);
			TimeSpan ts = sDate - eDate;
			double days = ts.TotalDays;
			
			if ((startDateTime.CompareTo(endDateTime) > 0) )
			{
				strErrMsg += "出差時間-起 不可大於 出差時間-訖 !\n";
			}
			
			if (days < 0)
			{
				strErrMsg += "出差單應於最遲應於出差日後5日內(含假日)完成補單，逾時不得申請。\n";
			} 
			
			if ((hrStartDateTime.CompareTo(startDateTime) > 0) || (endDateTime.CompareTo(hrEndDateTime) > 0) )
			{
				strErrMsg += "出差起迄時間以出差當日07:00至23:59為限!\n";
			}
			
            strErrMsg += tripDateTimeCheck(tripDate, userGuid, startTime, endTime);  
			
            //設定主旨
            values = base.getUserInfo(engine, OriginatorGUID.GuidValueText);
            string subject = "【出差人員：" + values[1] + "日期："+TripDate.ValueText +"  時間：" + StartTime.ValueText + " ~ " + EndTime.ValueText + "  地點：" + TripSite.ValueText + " 】";
            Subject.ValueText = subject;// +Subject.ValueText;
                    
        }
        if (actName.Equals("申請人"))
        {
            if (TrafficFee.ValueText.Equals("") && EatFee.ValueText.Equals("") && ParkingFee.ValueText.Equals("") && OtherFee.ValueText.Equals("") && OilFee.ValueText.Equals(""))
            {
                strErrMsg += "請至少維護一項出差費用 ( 車資、繕雜費、停車費、其他費用、油資)!\n";
            }
            if (DeptGUID.ValueText.Equals("S1500") || DeptGUID.ValueText.Equals("R6100") || DeptGUID.ValueText.Equals("S1000") 
			    || DeptGUID.ValueText.Equals("NSS1100") || DeptGUID.ValueText.Equals("NSR6100") )
			{
				if (MeetingMinute.ValueText.Equals(""))
	            {
	                strErrMsg += "請詳填 會議紀錄(Meeting Minute)！!\n";
	            }
			}
			
            if (!OilFee.ValueText.Equals(""))
            {
                if (StartMileage.ValueText.Equals("") || EndMileage.ValueText.Equals(""))
                {
                    strErrMsg += "請填寫 去回公里數！!\n";
                }
            }
			if (!EtagFee.ValueText.Equals("") && !EtagFee.ValueText.Equals("0"))
            {
                if (EtcStart.ValueText.Equals("") || EtcEnd.ValueText.Equals(""))
				
                {
                    strErrMsg += "請填寫 交流道起~交流道迄！!\n";
                }
				
				if (!EtcStart.ValueText.Equals("") && !EtcEnd.ValueText.Equals(""))
                {
					if ((Convert.ToDecimal(EtagFee.ValueText)).CompareTo(Convert.ToDecimal(RefEtagFee.ValueText)) > 0)
					{
						strErrMsg += "ETC費用已超過您出差之交流道起訖總額, 請再次確認!\n";
					}				
				}
            }			
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
        //si.ownerID = OriginatorGUID.ValueText;     //表單關系人=出差人員
        //si.ownerName = OriginatorGUID.ReadOnlyValueText;
		si.ownerID = (string)Session["UserID"];
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0]; //發起單位代號
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
		string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);
        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        //si.ownerID = OriginatorGUID.ValueText;
        //si.ownerName = OriginatorGUID.ReadOnlyValueText;
		//depData = getDeptInfo(engine, OriginatorGUID.GuidValueText);
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
		//writeLog("setFlowVariables" );
        string xml = "";
		string[] values = null;
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
	        string creatorId = si.fillerID;
	        string notifierId = ""; //通知人員
            string deptId = "";  //部門代碼
			string deptAsstId = ""; 
			
			string[] deptInfo = base.getDeptInfo(engine, currentObject.getData("OriginatorGUID"));
			if (!deptInfo[0].Equals(""))
			{
				string[] userFunc = getUserRoles(engine, "部門收發", deptInfo[0]);
				deptAsstId = userFunc[2];
				deptId = deptInfo[0];
			}

            //出差人員, 申請人
			string originatorGUID = currentObject.getData("OriginatorGUID");
			values = base.getUserInfo(engine, originatorGUID);
			string originatorId = values[0];
			int titleId = Convert.ToInt32(values[3]);
            
			//審核人員
            string checkByGUID = currentObject.getData("CheckByGUID");
            string checkById = "";
            if (!checkByGUID.Equals(""))
            {
                values = base.getUserInfo(engine, checkByGUID);
                checkById = values[0];
            }

	        string orgUnitManagerId = "";
			string userManagerId = "";
	        string[] userInfo = base.getUserGUID(engine, originatorId);
	        //直屬主管	
			values = base.getUserManagerInfo(engine, originatorGUID);
            string managerGUID = values[0];
            
            values = base.getUserInfo(engine, managerGUID);
            string managerId = values[0];
			
			userManagerId = managerId;
			
			//部門主管
			string deptGUID = currentObject.getData("DeptGUID");
	        string[] orgUnitInfo = base.getOrgUnitInfo(engine, deptGUID);
	        if (!orgUnitInfo[3].Equals(""))
	        {
	            orgUnitManagerId = orgUnitInfo[3];
	        }
						
            //填表人不等於請假人員則通知
            if (!creatorId.Equals(originatorId))
            {
                notifierId += originatorId + (";");
            }
            
            //通知部門助理
            if (!deptAsstId.Equals(""))
            {
                notifierId += deptAsstId + (";");
            }
			
			//簽核完畢後通知申請人及部門助理
			string actName = Convert.ToString(getSession("ACTName").ToString());
			//sw.WriteLine("actName : " + actName);
			if (actName.Equals("申請人"))
            {
                originatorId += (";") + notifierId;// + (";") + originatorId;				
            }
						
			//check flag
			string checkFlag = "N";
			if (currentObject.getData("IsTripFee").Equals("Y"))
			{
				checkFlag = "Y";
			}
			
			decimal isBillAmount = 0;
			decimal trafficFee = 0;
			decimal eatFee = 0;
			decimal parkingFee = 0;
			decimal otherFee = 0;
			
			if (!TrafficFee.ValueText.Equals("") || !EatFee.ValueText.Equals("") || !ParkingFee.ValueText.Equals("") || !OtherFee.ValueText.Equals(""))
			{
				trafficFee = decimal.Parse(TrafficFee.ValueText);
				eatFee = decimal.Parse(EatFee.ValueText);
				parkingFee = decimal.Parse(ParkingFee.ValueText);
				otherFee = decimal.Parse(OtherFee.ValueText);
			}
			
			isBillAmount = trafficFee + eatFee + parkingFee + otherFee;

            //當請款金額與會議記錄為空值時，不需給直屬主管再次簽核
            if (isBillAmount.Equals(0) && MeetingMinute.ValueText.Equals(""))
            {
                checkFlag = "N";
            }
            else 
            {
                checkFlag = "Y";
            }
            
            string checkFlag2 = "N";
            if (currentObject.getData("DeptGUID").Equals("R6100") && (isBillAmount > 3000))
            {
                checkFlag2 = "Y";
            }

            DataSet ds = null;
            string sql = null;
            string isFlowCheck = "P";  //P:個人請款，　S:彙總請款
            try
            {
                sql = "select distinct CheckValue2 from SmpFlowInspect where FormId='SPAD009' and CheckField1='DeptId' and CheckValue1='" + deptId + "' and Status='Y' ";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    isFlowCheck = ds.Tables[0].Rows[0][0].ToString();
                }
				
				if (userManagerId.Equals("1356") ) 
				{
					isFlowCheck = "P";
				}
                
            }
            catch (Exception e)
            {
                base.writeLog(e);
                throw new Exception(e.StackTrace);
            }
                        
            xml += "<SPAD009>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
            xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
            xml += "<manager DataType=\"java.lang.String\">" + userManagerId + "</manager>";
            xml += "<checkFlag DataType=\"java.lang.String\">" + checkFlag + "</checkFlag>";
            xml += "<hrowner DataType=\"java.lang.String\">SMP-HRADM</hrowner>";
            xml += "<checkby1 DataType=\"java.lang.String\">" + checkById + "</checkby1>";
            xml += "<notify1 DataType=\"java.lang.String\">" + notifierId + "</notify1>";
			xml += "<billAmount DataType=\"java.lang.Integer\">" + isBillAmount + "</billAmount>";
			xml += "<deptManager DataType=\"java.lang.String\">" + orgUnitManagerId + "</deptManager>";
            xml += "<checkFlag2 DataType=\"java.lang.String\">" + checkFlag2 + "</checkFlag2>";
            xml += "<checkFlag3 DataType=\"java.lang.String\">" + isFlowCheck + "</checkFlag3>";
            xml += "<titleNo DataType=\"java.lang.Integer\">" + titleId + "</titleNo>";
            xml += "</SPAD009>";
            writeLog("XML : " + xml);
        }
        catch (Exception e)
        {
            writeLog(e);
        }
       
        //表單代號
        param["SPAD009"] = xml;
        return "SPAD009";
    }
	

    protected void OriginatorGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                
        //更新出差人員主要部門
        string[] userDeptValues = base.getDeptInfo(engine, OriginatorGUID.GuidValueText);
		string orgId = "SMP";
        if (userDeptValues[0] != "")
        {            
            DeptGUID.ValueText = userDeptValues[0];
            DeptGUID.doValidate();
			
			//公司別
			string[] userValues = base.getUserInfoById(engine, OriginatorGUID.ValueText);
	        CompanyCode.ValueText = userValues[5];
			// 20161007 Eva增加 TE
			if (userValues[5].Equals("TE"))
			{
				//LblOtherDesc.Text = " **嘉普人員至105/12/31止國內出差費請款(發票、收據)請使用中普統編:54542191** ";
				LblOtherDesc.Text = "";
			}else{
				LblOtherDesc.Text = "";
			}
        }
        else
        {
            DeptGUID.ValueText = "";
			CompanyCode.ValueText = "";
			LblOtherDesc.Text = "";
        }        
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
		//writeLog("beforeSign" );
		/*
        string xml = "";
        string actName = Convert.ToString(getSession("ACTName").ToString());
		
		if (actName.Equals("直屬主管"))
        {
			try
            {                
                //產生刷卡紀錄
                if (!updateHR())
                {
                    MessageBox("產生出差資料異常!");
                    throw new Exception("HR 整合失敗");
                }
				
			}
            catch (Exception e)
            {
                base.writeLog(e);
            }
        }
		*/
        return addSignXml;
    }

    /// <summary>
    /// 簽核後程序, 群簽會執行
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
		string signProcess = Convert.ToString(Session["signProcess"]);
		//writeLog("afterSign signProcess : " + signProcess );
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
    /// 結案程序
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {	
        //取得目前關卡名稱
		string[] values = base.getNowSignActName(engine, currentObject.getData("GUID"));
        string actName = values[1];
		if (actName.Equals("直屬主管"))
        {
			try
            {                
                //產生刷卡紀錄
                if (!updateHR(engine, currentObject))
                {
                    //MessageBox("產生出差資料異常!");
                    //throw new Exception("HR 整合失敗");
                }
				
			}
            catch (Exception e)
            {
                base.writeLog(e);
            }
        }
		
        base.afterApprove(engine, currentObject, result);
    }
	
	
	/// <summary>
    /// 請假日期變更重算時數
    /// </summary>
    /// <param name="values"></param>
	
    protected void TripDate_DateTimeClick(string values)
    {
        string strErrMsg = "";
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            string tripDate = TripDate.ValueText;
            string userGuid = OriginatorGUID.GuidValueText;
            //檢查是否有相同天的出差單
            strErrMsg += tripDateCheck(tripDate, userGuid);            
            if (!strErrMsg.Equals(""))
            {
                MessageBox(strErrMsg);
            }
        }
    }
	
	
    protected void StartTime_DateTimeClick(string values)
    {
        string strErrMsg = "";
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            string tripDate = TripDate.ValueText;
            string userGuid = OriginatorGUID.GuidValueText;
            //檢查是否有相同天的出差單
            strErrMsg += tripDateCheck(tripDate, userGuid);
            if (!strErrMsg.Equals(""))
            {
                MessageBox("startTime : "+strErrMsg);
            }
        }

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
            string tripDate = TripDate.ValueText;
            string userGuid = OriginatorGUID.GuidValueText;
            //檢查是否有相同天的出差單
            strErrMsg += tripDateCheck(tripDate, userGuid);
            if (!strErrMsg.Equals(""))
            {
                MessageBox("endTime : "+strErrMsg);
            }
        }

    }
	

	//User Defined Functions from here
    /// <summary>
    /// prepareSQL: 組出 Update HR 之 SQL
    /// </summary>
    /// <param name="checkTime"></param> 傳入上班或下班時間
    /// <returns></returns> 回傳值為 SQL 字串
    private string prepareSQL(string checkTime)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string strCompany = CompanyCode.ValueText; //公司別
        string strCreator = "5152"; //建立者
        string strUsrGroup = strCompany; //User Group        
        string strCreateDate = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", ""); //用現在的日期
        string strFlag = "1"; //Flag
        string strMC001 = OriginatorGUID.ValueText; //員工代號
        string strMC005 = "N";	//產生明細碼
        string strMC900 = "03"; //機台別        
        string sheetNo = Convert.ToString(getSession(PageUniqueID, "SheetNo"));
        if (sheetNo.Length < 15)
        {
            throw new Exception("表單單號錯誤(長度不足15碼)!");
        }
        string strMC901 = "出"+sheetNo.Substring(7, 8); //簽核單號 , 只能填 10 碼(string)getSession("FlowID")        
        string strMC002 = TripDate.ValueText.Substring(0, 10).Replace("/", ""); //刷卡日期
        string strMC003 = checkTime; //產生刷卡時間
        string strMC006 = strMC002; //歸屬日期
        string strSQL = "";
        strSQL = "insert into AMSMC (COMPANY,CREATOR,USR_GROUP,CREATE_DATE,FLAG,MC001,MC002,MC003,MC005,MC006,MC900,MC901) values (";
        strSQL = strSQL + "'" + strCompany + "',";
        strSQL = strSQL + "'" + strCreator + "',";
        strSQL = strSQL + "'" + strUsrGroup + "',";
        strSQL = strSQL + "'" + strCreateDate + "',";
        strSQL = strSQL + "'" + strFlag + "',";
        strSQL = strSQL + "'" + strMC001 + "',";
        strSQL = strSQL + "'" + strMC002 + "',";
        strSQL = strSQL + "'" + strMC003 + "',";
        strSQL = strSQL + "'" + strMC005 + "',";
        strSQL = strSQL + "'" + strMC006 + "',";
        strSQL = strSQL + "'" + strMC900 + "',";
        strSQL = strSQL + "'" + strMC901 + "')";
        return strSQL;
    }

    /// <summary>
    /// 更新 HR 資料庫
    /// </summary>
    /// <returns></returns>
    private Boolean updateHR()
    {
        //connect HR system
        AbstractEngine engine = null;
        AbstractEngine hrengine = null;
        string sql = "";		
        string startTime = StartTime.ValueText.Replace(":", "");
        string endTime = EndTime.ValueText.Replace(":", "");
        string strUserId = OriginatorGUID.ValueText;
        string strMC002s = "";
        string strMC003s = "";
        string strMC002e = "";
        string strMC003e = "";

        if (!startTime.Equals(""))
        {
            strMC002e = TripDate.ValueText.Substring(0, 10).Replace("/", ""); //刷卡日期
            strMC003e = startTime; //出差起刷卡時間
        }
        if (!endTime.Equals(""))
        {
            strMC002e = TripDate.ValueText.Substring(0, 10).Replace("/", ""); //刷卡日期
            strMC003e = endTime; //出差訖刷卡時間
        }

        try
        {
            //取得 HR DB Connection String
            IOFactory factory = new IOFactory();
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            engine = factory.getEngine(engineType, connectString);

            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
			
			string companyId = CompanyCode.ValueText;
            string hrconStr = sp.getParam(companyId + "WorkFlowERPDB");
			
            if (hrconStr.Equals(""))
            {
                throw new Exception("找不到HR系統之參數設定:" + hrconStr);
            }

            //建立 HR SQL Server 資料庫連線
            hrengine = factory.getEngine(EngineConstants.SQL, hrconStr);

            //檢查出勤資料是否重覆, 若重覆則不再insert
            if (!startTime.Equals(""))
            {
                sql = "select * from AMSMC where MC001='" + strUserId + "' and MC002='" + strMC002s + "' and MC003='" + strMC003s + "' ";
                DataSet ds = hrengine.getDataSet(sql, "TEMP");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //MessageBox("DSC count : " + ds.Tables[0].Rows.Count);
                    //資料已存在, 不再insert出差資料
                }
                else
                //處理上班時間
                //if (!startTime.Equals(""))
                {
                    sql = "";
                    sql = prepareSQL(startTime);
                    if (!hrengine.executeSQL(sql))
                    {
                        //throw new Exception(hrengine.errorString);
                    }
                }
            }

            if (!endTime.Equals(""))
            {
                sql = "select * from AMSMC where MC001='" + strUserId + "' and MC002='" + strMC002e + "' and MC003='" + strMC003e + "' ";
                DataSet ds = hrengine.getDataSet(sql, "TEMP");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //資料已存在, 不再insert出差資料
                }
                else
                {
                    //處理下班時間
                    sql = prepareSQL(endTime);                    
                    if (!hrengine.executeSQL(sql))
                    {
                        //throw new Exception(hrengine.errorString);
                    }
                }
            }
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };

            return false;
        }

        //關閉引擎
        //engine.close();
        return true;
    }

    /// <summary>
    /// 更新 HR 資料庫
    /// </summary>
    /// <returns></returns>
    private Boolean updateHR(AbstractEngine engine, DataObject currentObject)
    {
		//connect HR system
        AbstractEngine hrengine = null;
        string sql = "";
        string startTime = currentObject.getData("StartTime").Replace(":", "");
        string endTime = currentObject.getData("EndTime").Replace(":", "");
        string strUserId = currentObject.getData("OriginatorGUID");
        string strMC002s = "";
        string strMC003s = "";
        string strMC002e = "";
        string strMC003e = "";
		string strMC900 = "";
		DataSet ds = null;

        if (!startTime.Equals(""))
        {
            strMC002s = currentObject.getData("TripDate").Substring(0, 10).Replace("/", ""); //刷卡日期
            strMC003s = startTime; //出差起刷卡時間
			strMC900  = "02";
        }
        if (!endTime.Equals(""))
        {
            strMC002e = currentObject.getData("TripDate").Substring(0, 10).Replace("/", ""); //刷卡日期
            strMC003e = endTime; //出差訖刷卡時間
			strMC900  = "03";
        }

        try
        {
            //取得 HR DB Connection String
            IOFactory factory = new IOFactory();
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            //engine = factory.getEngine(engineType, connectString);

            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string companyId = CompanyCode.ValueText;
            string hrconStr = sp.getParam(companyId + "WorkFlowERPDB");

            if (hrconStr.Equals(""))
            {
                throw new Exception("找不到HR系統之參數設定:" + hrconStr);
            }

            //建立 HR SQL Server 資料庫連線
            hrengine = factory.getEngine(EngineConstants.SQL, hrconStr);

            //檢查出勤資料是否重覆, 若重覆則不再insert
            if (!startTime.Equals(""))
            {
                sql = "select * from AMSMC where MC001='" + strUserId + "' and MC002='" + strMC002s + "' and MC003='" + strMC003s + "' ";
                ds = hrengine.getDataSet(sql, "TEMP");
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //MessageBox("DSC count : " + ds.Tables[0].Rows.Count);
                    //資料已存在, 不再insert出差資料
                }
                else
                //處理上班時間
                //if (!startTime.Equals(""))
                {
                    sql = "";
                    sql = prepareSQL(startTime, currentObject, engine, strMC900);
					writeLog("updateHR - insert 上班時間 SQL:" + sql);
                    if (!hrengine.executeSQL(sql))
                    {
                        //throw new Exception(hrengine.errorString);
                    }
                }
            }

            if (!endTime.Equals(""))
            {
                sql = "select * from AMSMC where MC001='" + strUserId + "' and MC002='" + strMC002e + "' and MC003='" + strMC003e + "' ";
                ds = hrengine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //資料已存在, 不再insert出差資料
                }
                else
                {
                    //處理下班時間
                    sql = "";
                    sql = prepareSQL(endTime, currentObject, engine, strMC900);
					writeLog("updateHR - insert 下班時間 SQL:" + sql);
                    if (!hrengine.executeSQL(sql))
                    {
                        //throw new Exception(hrengine.errorString);
                    }
                }
            }
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
				hrengine.close();
            }
            catch { };

            return false;
        }
		hrengine.close();
        return true;
    }

    //User Defined Functions from here
    /// <summary>
    /// prepareSQL: 組出 Update HR 之 SQL
    /// </summary>
    /// <param name="checkTime"></param> 傳入上班或下班時間
    /// <returns></returns> 回傳值為 SQL 字串
    private string prepareSQL(string checkTime, DataObject currentObject, AbstractEngine engine, string strMC900)
    {		
		SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string strCompany = currentObject.getData("CompanyCode");
        string strCreator = "2854"; //建立者
        string strUsrGroup = strCompany; //User Group
        string strCreateDate = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", ""); //用現在的日期
        string strFlag = "1"; //Flag
        string[] values = base.getUserInfo(engine, currentObject.getData("OriginatorGUID"));
        string strMC001 = values[0];
		//writeLog("prepareSQL strMC001 員工代號:" +  strMC001);
        string strMC005 = "N";	//產生明細碼
        string sheetNo = currentObject.getData("SheetNo"); //Convert.ToString(getSession(PageUniqueID, ""));
        if (sheetNo.Length < 15)
        {
            throw new Exception("表單單號錯誤(長度不足15碼)!");
        }
        string strMC901 = "出" + sheetNo.Substring(7, 8); //簽核單號 , 只能填 10 碼(string)getSession("FlowID")        
        string strMC002 = currentObject.getData("TripDate").Substring(0, 10).Replace("/", ""); //刷卡日期
        string strMC003 = checkTime; //產生刷卡時間
        string strMC006 = strMC002; //歸屬日期
        string strSQL = "";
        strSQL = "insert into AMSMC (COMPANY,CREATOR,USR_GROUP,CREATE_DATE,FLAG,MC001,MC002,MC003,MC005,MC006,MC900,MC901) values (";
        strSQL = strSQL + "'" + strCompany + "',";
        strSQL = strSQL + "'" + strCreator + "',";
        strSQL = strSQL + "'" + strUsrGroup + "',";
        strSQL = strSQL + "'" + strCreateDate + "',";
        strSQL = strSQL + "'" + strFlag + "',";
        strSQL = strSQL + "'" + strMC001 + "',";
        strSQL = strSQL + "'" + strMC002 + "',";
        strSQL = strSQL + "'" + strMC003 + "',";
        strSQL = strSQL + "'" + strMC005 + "',";
        strSQL = strSQL + "'" + strMC006 + "',";
        strSQL = strSQL + "'" + strMC900 + "',";
        strSQL = strSQL + "'" + strMC901 + "')";
		
		writeLog( " GET prepareSQL SQL : " + strSQL);
        return strSQL;
    }

    protected void CheckByGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
    }

    /// <summary>
    /// 出差日期檢查
    /// </summary>
    /// <returns></returns>
    private string tripDateCheck(string tripDate, string userGuid)
    {
        AbstractEngine engine = null;
        DataSet ds = null;
        string sql = null;
        string strErrMsg = "";
        try
        {
            bool isSameTripDate = true;
            string connectString = (string)Session["connectString"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, connectString);

            sql = "select  SheetNo,Subject From SmpTripForm, SMWYAAA  "
                + "where SMWYAAA002=SheetNo and SMWYAAA004='國內出差單流程' and OriginatorGUID='" + userGuid + "' "
                + " and TripDate ='" + tripDate + "'  and SMWYAAA020 in ('I','Y')  order by 1 desc ";

            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                isSameTripDate = false;
                string sheetNo = ds.Tables[0].Rows[0][0].ToString();
                string subject = ds.Tables[0].Rows[0][1].ToString();
                strErrMsg += "您單號:" + sheetNo + " 已有相同日期出差單存在, 主旨:" + subject;
            }
            if (!engine.errorString.Equals(""))
            {
                strErrMsg += engine.errorString;
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
    /// 出差日期時間檢查
    /// </summary>
    /// <returns></returns>
    private string tripDateTimeCheck(string tripDate, string userGuid, string startTime, string endTime)
    {
        AbstractEngine engine = null;
        DataSet ds = null;
        string sql = null;
        string strErrMsg = "";
        try
        {
            bool isSameTripDate = true;
            string connectString = (string)Session["connectString"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, connectString);

            sql = "select  SheetNo,Subject, StartTime, EndTime, TripDate From  SmpTripForm stf, SMWYAAA  ya  "
                + "where ya.SMWYAAA019 = stf.GUID and SMWYAAA004='國內出差單流程' and OriginatorGUID='" + userGuid + "' "
                + " and TripDate ='" + tripDate + "'  and SMWYAAA020 in ('I','Y') and StartTime='" + startTime + "' and EndTime='"+ endTime +"' ";

            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                isSameTripDate = false;
                string sheetNo = ds.Tables[0].Rows[0][0].ToString();
                string subject = ds.Tables[0].Rows[0][1].ToString();
                strErrMsg += "您單號:" + sheetNo + " 已有相同日期時間出差單存在, 主旨:" + subject;
            }
            if (!engine.errorString.Equals(""))
            {
                strErrMsg += engine.errorString;
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

    protected void OriginatorGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            OriginatorGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
    protected void CheckByGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            CheckByGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }

    /// <summary>
    /// 取得里程數
    /// </summary>
    /// <returns></returns>
    private string getMiles()
    {
        System.IO.StreamWriter sw = null;
        
        string miles = MileageSum.ValueText;
        string startMileage = StartMileage.ValueText;
        string endMileage = EndMileage.ValueText;
        string actName = Convert.ToString(getSession("ACTName"));
        if (actName.Equals("申請人"))
        {
            if (!startMileage.Equals("") && (!endMileage.Equals("")))
            {
                int startMileageInt = Convert.ToInt32(startMileage);
                int endMileageInt = Convert.ToInt32(endMileage);

                miles = Convert.ToString(endMileageInt - startMileageInt);
            }
            
        }
        if (sw != null) { sw.Close(); }
        return miles;
    }

    /// <summary>
    /// 取得油資
    /// </summary>
    /// <returns></returns>
    private string getOilFee()
    {
        string miles = MileageSum.ValueText;
        string oilFee = "";
        if (!miles.Equals(""))
        {
            //取得請假時數
            int oilFeeInt = Convert.ToInt32(miles) * 6 ;

            oilFee = Convert.ToString(oilFeeInt);
        }
        return oilFee;
    }
    
    protected void StartMileage_TextChanged(object sender, EventArgs e)
    {
        MileageSum.ValueText = getMiles();
        OilFee.ValueText = getOilFee();
    }
    protected void EndMileage_TextChanged(object sender, EventArgs e)
    {
        MileageSum.ValueText = getMiles();
        OilFee.ValueText = getOilFee();
    }
	//列印單據
	protected void PrintButton_OnClick(object sender, EventArgs e)
    {
		Session["SPAD009_SheetNo"] = SheetNo.ValueText;
        string url = "PrintPage.aspx";
	    base.showOpenWindow(url, "列印國內出差單", "", "600", "", "", "", "1", "1", "", "", "", "", "750", "", true);
    }	
	
	protected void EtcMileage_SelectChanged(string value)
    {
		string strEtcStart = EtcStart.ValueText;
		string[,] ids = null;
        DataSet ds = null;
        int count = 0;
		
		if (strEtcStart.Equals(""))        
        {
            ids[0, 0] = "";
            ids[0, 1] = "";
            EtcStart.setListItem(ids);
        }	
		RefEtagFee.ValueText = getETCFee();
    }
	
	protected void EtcMileageEnd_SelectChanged(string value)
    {
        string strEtcEnd = EtcEnd.ValueText;
		string[,] ids = null;
        DataSet ds = null;
        int count = 0;
		
		if (strEtcEnd.Equals(""))        
        {
            ids[0, 0] = "";
            ids[0, 1] = "";
            EtcEnd.setListItem(ids);
        }
		RefEtagFee.ValueText = getETCFee();
    }
	
	 /// <summary>
    /// 取得eTag費用
    /// </summary>
    /// <returns></returns>
    private string getETCFee()
    {
		AbstractEngine engine = null;
		string sql = null;
        DataSet ds = null;
        string strEtcStart = EtcStart.ValueText;		
        string strEtcEnd = EtcEnd.ValueText;
		string strStartMileage = "";
		string strEndMileage = "";
		string value = "";
		string miles = "";
		
		IOFactory factory = new IOFactory();
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];            
        engine = factory.getEngine(engineType, connectString);
		
		sql = " select Mileage from SmpETCMileage where Serial='" + strEtcStart + "'";
        value = (string)engine.executeScalar(sql);
        if (value != null)
        {
           strStartMileage = value;
        }
		
		sql = " select Mileage from SmpETCMileage where Serial='" + strEtcEnd + "'";
        value = (string)engine.executeScalar(sql);
        if (value != null)
        {
           strEndMileage = value;
        }
		
		if (!strStartMileage.Equals("") && (!strEndMileage.Equals("")))
        {
            int startMileageInt = Convert.ToInt32(strStartMileage);
            int endMileageInt = Convert.ToInt32(strEndMileage);
			double maxMiles =  Math.Ceiling(( Math.Abs(endMileageInt - startMileageInt) * 2 ) * 1.2)+10;  
			miles = Convert.ToString(maxMiles);
        }
        return miles;
    }
	
	private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPAD009.log", true, System.Text.Encoding.Default);
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
}