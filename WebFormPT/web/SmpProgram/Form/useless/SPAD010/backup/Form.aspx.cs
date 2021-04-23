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
using com.dsc.kernal.global;
using com.dsc.kernal.databean;
using WebServerProject.system.login;
using com.dsc.kernal.logon;

public partial class SmpProgram_System_Form_SPAD010_Form : SmpAdFormPage
{

    protected override void init()
    {	
        ProcessPageID = "SPAD010"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPAD010.SmpTripBillingAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPAD";		
    }


    protected override void initUI(AbstractEngine engine, DataObject objects)
    {        
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];		

        SheetNo.Display = false;
		RefEtagFee.Display = false;  //Etag參考最大請款金額

        //申請單位
        DeptGUID.clientEngineType = engineType;
        DeptGUID.connectDBString = connectString;
        DeptGUID.ValueText = si.submitOrgID;
        DeptGUID.doValidate();

        CheckDept.clientEngineType = engineType;
        CheckDept.connectDBString = connectString;
        CheckUser.clientEngineType = engineType;
        CheckUser.connectDBString = connectString;
        CheckUser.ValueText = si.fillerID;
        CheckUser.doValidate();

        //申請人員
        OriginatorGUID.clientEngineType = engineType;
        OriginatorGUID.connectDBString = connectString;            
        if (OriginatorGUID.ValueText.Equals(""))
        {
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }
		
		//公司別
		//string userId = (string)Session["UserId"];
		//string[] values = base.getUserInfo(engine, userId);
		//CompanyCode.ValueText = values[2];
		
		//公司別
		string[,] idsCompany = null;
        idsCompany = new string[,]{
            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "smp", "新普科技")},
            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "tp", "中普科技")}
        };
        CompanyCode.setListItem(idsCompany);
		string[] values = base.getUserInfoById(engine,(string)Session["UserId"]);
        CompanyCode.ValueText = values[5];
        CompanyCode.ReadOnly = true;		
		
		string[,] ids = null;
        DataSet ds = null;
        int count = 0;
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
		      		
		//審核人, 暫無使用
        CheckByGUID.clientEngineType = engineType;
        CheckByGUID.connectDBString = connectString;
				
        TotalAmount.ReadOnly = true;
		DeptGUID.ReadOnly = true;

        CheckUser.clientEngineType = engineType;
        CheckUser.connectDBString = connectString;
        CheckDept.clientEngineType = engineType;
        CheckDept.connectDBString = connectString;

        UserGUID.clientEngineType = engineType;
        UserGUID.connectDBString = connectString;
		

        //領款人
        PayeeGUID.clientEngineType = engineType;
        PayeeGUID.connectDBString = connectString;
        PayeeGUID.ReadOnly = true;
        //FinDesc.ReadOnly = true;
        OilFee.ReadOnly = true;
        MileageSum.ReadOnly = true;
        StartTime.ReadOnly = true;
        EndTime.ReadOnly = true;
		TripSite.ReadOnly = true;
		//EtcEnd.ReadOnly = true;
		//EtcStart.ReadOnly = true;

		bool isAddNew = base.isNew();
        DataObjectSet requestSet = null;
        //int count = 0;
        decimal totalAmount = 0;
        DataObject dooInit = null;
        if (isAddNew)
        {
            requestSet = new DataObjectSet();
            requestSet.isNameLess = true;
            requestSet.setAssemblyName("WebServerProject");
            requestSet.setChildClassString("WebServerProject.form.SPAD010.SmpTripBillingDetail");
            requestSet.setTableName("SmpTripBillingDetail");
            requestSet.loadFileSchema();
            objects.setChild("SmpTripBillingDetail", requestSet);
            try
            {
                string billingUser = si.fillerID;
                string trafficFee = "0";
                string eatFee = "0";
                string parkingFee = "0";
                string otherFee = "0";
                string etagFee = "0";
                string oilFee = "0";

                //string sql = " select a.GUID, b.OID, b.id as UserId, b.userName, TripDate, TrafficFee, EatFee, ParkingFee, OtherFee, SheetNo, c.id as DeptId, StartMileage, EndMileage, EtagFee, MileageSum, a.StartTime, a.EndTime, a.OilFee, a.TripSite, setc.Name EtcStart , eect.Name EtcEnd ";
				string sql = " select a.GUID, b.OID, b.id as UserId, b.userName, TripDate, TrafficFee, EatFee, ParkingFee, OtherFee, SheetNo, c.id as DeptId, StartMileage, EndMileage, EtagFee, MileageSum, a.StartTime, a.EndTime, a.OilFee, a.TripSite, a.EtcStart , a.EtcEnd, a.RefEtagFee ";
                sql += " From SmpTripForm a, NaNa.dbo.Users b, NaNa.dbo.OrganizationUnit c  ";
                sql += " where a.OriginatorGUID=b.OID and IsTripFee='Y'  and c.OID = a.DeptGUID    ";
				//sql += " From SmpTripForm a, NaNa.dbo.Users b, NaNa.dbo.OrganizationUnit c  , SmpETCMileage setc, SmpETCMileage eect  ";
				//sql += " where a.OriginatorGUID=b.OID and IsTripFee='Y'  and c.OID = a.DeptGUID   and setc.Serial=a.EtcStart and eect.Serial=a.EtcEnd  ";
                //sql += " and SheetNo in (select SMWYAAA002 from SMWYAAA where SMWYAAA002 like 'SPAD009%'  and SMWYAAA020='Y') ";
				sql += " and a.GUID in (select SMWYAAA019 from SMWYAAA where SMWYAAA002 like 'SPAD009%'  and SMWYAAA020='Y') ";
                sql += " and SheetNo not in (select c.OriTripFormGUID From SmpTripBilling a, SMWYAAA b, SmpTripBillingDetail c where a.GUID = b.SMWYAAA019  and SMWYAAA020 in ('Y','I') and c.HeaderGUID=a.GUID )  ";
                sql += " and b.id = '" + billingUser + "' order by TripDate";

                DataSet allBillingList = engine.getDataSet(sql, "TEMP");
                count = allBillingList.Tables[0].Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    dooInit = requestSet.create();

                    trafficFee = allBillingList.Tables[0].Rows[i]["TrafficFee"].ToString();
                    if (trafficFee.Equals("")) { trafficFee = "0"; }
                    eatFee = allBillingList.Tables[0].Rows[i]["EatFee"].ToString();
                    if (eatFee.Equals("")) { eatFee = "0"; }
                    parkingFee = allBillingList.Tables[0].Rows[i]["ParkingFee"].ToString();
                    if (parkingFee.Equals("")) { parkingFee = "0"; }
                    otherFee = allBillingList.Tables[0].Rows[i]["OtherFee"].ToString();
                    if (otherFee.Equals("")) { otherFee = "0"; }
                    etagFee = allBillingList.Tables[0].Rows[i]["EtagFee"].ToString();
                    if (etagFee.Equals("")) { etagFee = "0"; }
                    oilFee = allBillingList.Tables[0].Rows[i]["OilFee"].ToString();
                    if (oilFee.Equals("")) { oilFee = "0"; }

                    totalAmount += decimal.Parse(trafficFee) + decimal.Parse(eatFee) + decimal.Parse(parkingFee) + decimal.Parse(otherFee) + decimal.Parse(etagFee) + decimal.Parse(oilFee);

                    dooInit.setData("GUID", IDProcessor.getID(""));
                    dooInit.setData("HeaderGUID", "TEMP");
                    dooInit.setData("UserGUID", allBillingList.Tables[0].Rows[i]["OID"].ToString());
                    dooInit.setData("UserId", allBillingList.Tables[0].Rows[i]["UserId"].ToString());
                    dooInit.setData("UserName", allBillingList.Tables[0].Rows[i]["userName"].ToString());
                    dooInit.setData("TripDate", allBillingList.Tables[0].Rows[i]["TripDate"].ToString());
					dooInit.setData("StartTime", allBillingList.Tables[0].Rows[i]["StartTime"].ToString());
					dooInit.setData("EndTime", allBillingList.Tables[0].Rows[i]["EndTime"].ToString());
                    dooInit.setData("StartMileage", allBillingList.Tables[0].Rows[i]["StartMileage"].ToString());
                    dooInit.setData("EndMileage", allBillingList.Tables[0].Rows[i]["EndMileage"].ToString());
                    dooInit.setData("MileageSum", allBillingList.Tables[0].Rows[i]["MileageSum"].ToString());
                    dooInit.setData("OilFee", oilFee);
                    dooInit.setData("TrafficFee", trafficFee);
                    dooInit.setData("EatFee", eatFee);
                    dooInit.setData("ParkingFee", parkingFee);
                    dooInit.setData("EtagFee", etagFee);
					dooInit.setData("EtcStart", allBillingList.Tables[0].Rows[i]["EtcStart"].ToString());
					dooInit.setData("EtcEnd", allBillingList.Tables[0].Rows[i]["EtcEnd"].ToString());
					dooInit.setData("RefEtagFee", allBillingList.Tables[0].Rows[i]["RefEtagFee"].ToString());					
                    dooInit.setData("OtherFee", otherFee);
                    dooInit.setData("OriTripFormGUID", allBillingList.Tables[0].Rows[i]["SheetNo"].ToString());
					dooInit.setData("TripSite", allBillingList.Tables[0].Rows[i]["TripSite"].ToString());

                    dooInit.setData("IS_LOCK", "N");
                    dooInit.setData("IS_DISPLAY", "Y");
                    dooInit.setData("DATA_STATUS", "Y");

                    requestSet.add(dooInit);
                    TotalAmount.ValueText = totalAmount.ToString();
                }
            }
            catch (Exception ze)
            {
                MessageBox(ze.Message);
                writeLog(ze);
            }

        }
        else
        {
            requestSet = objects.getChild("SmpTripBillingDetail");
        }
        BillingDetailList.NoAdd = true;
		BillingDetailList.isShowAll = true;
        BillingDetailList.dataSource = requestSet;        
        BillingDetailList.HiddenField = new string[] { "GUID", "HeaderGUID", "UserGUID", "EtcStart" , "EtcEnd", "RefEtagFee", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        BillingDetailList.addSortCondition("出差日期", DataObjectConstants.ASC);
		BillingDetailList.updateTable();		
		
		UserGUID.ReadOnly = true;
        OriTripFormGUID.ReadOnly = true;
        TripDate.ReadOnly = true;
		
		if (base.isNew())
        {
            PrintButton1.Display = false;
        }else{
			PrintButton1.Enabled = true;
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
        SheetNo.Display = false;
		
        bool isAddNew = base.isNew();
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號, 主旨含單號
        base.showData(engine, objects);
		SheetNo.ValueText = objects.getData("SheetNo"); //傳入序號,
		
        //申請人員
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
		
		//公司別				
		string[,] idsCompany = null;
        idsCompany = new string[,]{
            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "smp", "新普科技")},
            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "tp", "中普科技")}
        };
        CompanyCode.setListItem(idsCompany);
		string[] values = base.getUserInfo(engine, OriginatorGUID.GuidValueText);
        CompanyCode.ValueText = values[2];
        CompanyCode.ReadOnly = true;

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
		
        //起始時間
        StartDate.ValueText = objects.getData("StartDate");
        //截止時間
        EndDate.ValueText = objects.getData("EndDate");        
		//請款總額
		calculateTotal();
		TotalAmount.ValueText = objects.getData("TotalAmount");
        //領款人．財務註記
        //審核人員
        string payeeGUID = objects.getData("PayeeGUID");
        if (!payeeGUID.Equals(""))
        {
            PayeeGUID.GuidValueText = payeeGUID; //將值放入人員開窗元件中, 資料庫存放GUID
            PayeeGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
        //PayeeGUID.ValueText = objects.getData("PayeeGUID");
        FinDesc.ValueText = objects.getData("FinDesc"); 

		DataObjectSet requestSet = null;
        requestSet = objects.getChild("SmpTripBillingDetail");
        BillingDetailList.dataSource = requestSet;
        BillingDetailList.updateTable();
		BillingDetailList.addSortCondition("出差日期", DataObjectConstants.ASC);
        for (int i = 0; i < BillingDetailList.dataSource.getAvailableDataObjectCount(); i++)
        {
            BillingDetailList.dataSource.getAvailableDataObject(i).setData("SmpTripBillingDetail", objects.getData("GUID"));
        }
		
		if (!isAddNew)
        {			
            Subject.ReadOnly = true;
            SheetNo.ReadOnly = true;
            DeptGUID.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            CheckByGUID.ReadOnly = true;
            StartDate.ReadOnly = true;
			EndDate.ReadOnly = true;
            TotalAmount.ReadOnly = true;
            SearchButton.ReadOnly = true;
            TrafficFee.ReadOnly = true;
            EatFee.ReadOnly = true;
            ParkingFee.ReadOnly = true;
            OtherFee.ReadOnly = true;
            EtagFee.ReadOnly = true;
            StartMileage.ReadOnly = true;
            EndMileage.ReadOnly = true;
            MileageSum.ReadOnly = true;
			UserGUID.ReadOnly = true;
            TripDate.ReadOnly = true;
			CheckDept.ReadOnly = true;
            CheckUser.ReadOnly = true;
            OriTripFormGUID.ReadOnly = true;
            BillingDetailList.NoAdd = true;
            BillingDetailList.NoModify = true;
            BillingDetailList.NoDelete = true;
            PayeeGUID.ReadOnly = true;
            FinDesc.ReadOnly = true;
			StartTime.ReadOnly = true;
			EndTime.ReadOnly = true;
			TripSite.ReadOnly = true;
			CheckUser.ValueText = "";
			CheckUser.doValidate();			
        }
        if (isAddNew) {
            PayeeGUID.ReadOnly = true;
            //FinDesc.ReadOnly = true;
        }

        if (actName.Equals("財務人員"))
        {
            PayeeGUID.ReadOnly = false;
            FinDesc.ReadOnly = false;
        }
        
        //sw.Close();
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
                objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
                objects.setData("DeptGUID", DeptGUID.GuidValueText);
                objects.setData("CheckByGUID", CheckByGUID.GuidValueText);
				objects.setData("StartDate", StartDate.ValueText);
                objects.setData("EndDate", EndDate.ValueText);				
				objects.setData("TotalAmount", TotalAmount.ValueText);
            				
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("DATA_STATUS", "Y");
                //主旨含單號
                base.saveData(engine, objects);
            }
			calculateTotal();
            objects.setData("TotalAmount", TotalAmount.ValueText);
            objects.setData("StartDate", StartDate.ValueText);
            objects.setData("EndDate", EndDate.ValueText);
            objects.setData("PayeeGUID", PayeeGUID.GuidValueText);
            objects.setData("FinDesc", FinDesc.ValueText);
				
			objects.setData("OriginatorUserName", OriginatorGUID.ReadOnlyValueText);
			objects.setData("DeptName", DeptGUID.ReadOnlyValueText);
			objects.setData("CheckByName", CheckByGUID.ReadOnlyValueText);
			
            for (int i = 0; i < BillingDetailList.dataSource.getAvailableDataObjectCount(); i++)
            {
                BillingDetailList.dataSource.getAvailableDataObject(i).setData("HeaderGUID", objects.getData("GUID"));
                //MessageBox("SaveBillingDetailList!");
            }
			
            //beforeSetFlow
            setSession("IsSetFlow", "Y");
			
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
           // if (sw != null) sw.Close();
        }
    }

    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));
        string[] values = null;

        if (actName.Equals(""))
        {
			if (TotalAmount.ValueText.Equals("0"))
            {
                pushErrorMessage("申請金額必須大於0");
                result = false;
            }
            
			//檢查 明細清單 資料
            DataObjectSet chkType = BillingDetailList.dataSource;
            if (chkType.getAvailableDataObjectCount().Equals(0))
            {
                strErrMsg += "請輸入需請款的出差資訊!\n";
            }			
            
            //設定主旨
            values = base.getUserInfoById(engine, OriginatorGUID.ValueText);	
            string subject = "國內出差旅費核銷單【請款人員：" + values[1] + "   日期："+StartDate.ValueText +" ~ " + EndDate.ValueText + " 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject + Subject.ValueText;
            }
            
        }
        if (actName.Equals("財務人員"))
        {
            if (PayeeGUID.ValueText.Equals(""))
            {
                pushErrorMessage("請維護領款人");
                result = false;
            }
			if (TotalAmount.ValueText.Equals("0"))
            {
                pushErrorMessage("申請金額必須大於0");
                result = false;
            }
        }
        

        if (!strErrMsg.Equals(""))
        {
            pushErrorMessage(strErrMsg);
            result = false;
        }
        //sw.Close();
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
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);
        //string[] depDataRealtionship = getDeptInfo(engine, (string)OriginatorGUID.GuidValueText);
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
        //MessageBox("getSubmitInfo");
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
        //System.IO.StreamWriter sw = null;
        string xml = "";
		string[] values = null;
        try
        {
            //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\spad006.log", true);
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
	        string creatorId = si.fillerID;
	        string notifierId = ""; //通知人員
			string deptAsstId = ""; 
			string deptId = "";
			string actName = Convert.ToString(getSession("ACTName"));  //關卡名稱
			
			string originatorGUID = currentObject.getData("OriginatorGUID");
			values = base.getUserInfo(engine, originatorGUID);
			string originatorId = values[0];
			string[] deptInfo = base.getDeptInfo(engine, originatorGUID);
			if (!deptInfo[0].Equals(""))
			{
				string[] userFunc = getUserRoles(engine, "部門收發", deptInfo[0]);
				deptAsstId = userFunc[2];
				deptId = deptInfo[0];
			}

			//審核人員
            string checkByGUID = currentObject.getData("CheckByGUID");
            string checkById = "";
            if (!checkByGUID.Equals(""))
            {
                values = base.getUserInfo(engine, checkByGUID);
                checkById = values[0];
            }
			
	        string orgUnitManagerId = "";
	        string[] userInfo = base.getUserGUID(engine, creatorId);
	        string[] managerInfo = base.getUserManagerInfo(engine, userInfo[0]);
	        if (managerInfo[1].Equals(""))
	        {
	            orgUnitManagerId = managerInfo[1];
	        }
			string deptGUID = currentObject.getData("DeptGUID"); 
	        string[] orgUnitInfo = base.getOrgUnitInfo(engine, deptGUID);
	        if (!orgUnitInfo[3].Equals(""))
	        {
	            orgUnitManagerId = orgUnitInfo[3];
	        }
			
			//實際請款人者
			string requestors = "";
			
			//主管
			if (!deptId.Equals("R6100")) 
			{
		        string managers = "";
		        DataObjectSet set = currentObject.getChild("SmpTripBillingDetail");
		        for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
		        {
		            string requestorId = set.getAvailableDataObject(i).getData("UserID");
		            if (!requestorId.Equals(originatorId))
		            {
		                if (requestors.IndexOf(requestorId) == -1)
		                {
		                    requestors += requestorId + ";";
		                }
		            }

		            if (managers.Equals(""))
		            {
		                string[] results = base.getUserGUID(engine, requestorId);
		                string requestorGUID = results[0];
		                results = base.getUserManagerInfo(engine, requestorGUID);
		                string managerId = results[1];
		                managers = managerId;
						orgUnitManagerId = managerId;
		            }
		        }
			}
			
			notifierId = creatorId + (";");			
            //填表人不等於申請人員則通知
            if (!creatorId.Equals(originatorId) )
            {
                notifierId += originatorId + (";");
            }
            //通知部門助理
            if (!deptAsstId.Equals("") && !deptAsstId.Equals(creatorId))
            {
                notifierId += deptAsstId + (";");
            }
			
			if (actName.Equals("通知人員"))
			{
				//通知實際請款人者
	            if (!requestors.Equals(""))
	            {
					//sw.WriteLine("requestors=" + requestors);
	                notifierId += requestors + (";");
	            }
			}
			//sw.WriteLine("notifierId=" + notifierId);
			
			DataSet ds = null;
            string sql = null;
            string isFlowCheck = "P";  //P:個人請款依據金額, 到財務1532 或到出納4846，　S:彙總請款, 由財務1532統一處理
            try
            {
                sql = "select CheckValue2 from SmpFlowInspect where FormId='SPAD010' and CheckField1='DeptId' and CheckValue1='" + deptId + "' and Status='Y' ";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    isFlowCheck = ds.Tables[0].Rows[0][0].ToString();
                    //sw.WriteLine("IN isFlowCheck=" + isFlowCheck);
                }                
            }
            catch (Exception e)
            {
                base.writeLog(e);
                throw new Exception(e.StackTrace);
            }

            string totalAmount = TotalAmount.ValueText;
			decimal ttlAmount = decimal.Parse(TotalAmount.ValueText);
            string finOwner = "";
			if ((ttlAmount >= 3000) || (isFlowCheck.Equals("S")))
            {
				//sw.WriteLine("requestors=" + requestors);
                finOwner = "SMP-FINAPOWNER" ;
            }else{
                finOwner = "SMP-CASHER" ;
			}
			

            //sw.WriteLine("orgUnitManagerId=" + orgUnitManagerId);
            xml += "<SPAD010>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
            xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
            xml += "<orgUnitManager DataType=\"java.lang.String\">" + orgUnitManagerId + "</orgUnitManager>";
            xml += "<note1 DataType=\"java.lang.String\">" + finOwner + "</note1>";
            xml += "<note2 DataType=\"java.lang.String\"></note2>";
            xml += "<checkby DataType=\"java.lang.String\">" + checkById + "</checkby>";
            xml += "<notifier DataType=\"java.lang.String\">" + notifierId + "</notifier>";
            xml += "<totalamount DataType=\"java.lang.String\">" + totalAmount + "</totalamount>";
            xml += "</SPAD010>";
            //sw.WriteLine("xml: " + xml);
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
            //if (sw != null) sw.Close();
        }
        //表單代號
        param["SPAD010"] = xml;
        return "SPAD010";
    }
	
	/// <summary>
    /// 表單送出後
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        try
        {
			string strErrMsg = "";

            if (!strErrMsg.Equals(""))
            {
                pushErrorMessage(strErrMsg);
                //throw new Exception(strErrMsg);
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        base.afterSend(engine, currentObject);
    }	

    protected void OriginatorGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                
        //更新出差人員主要部門
        string[] userDeptValues = base.getDeptInfo(engine, OriginatorGUID.GuidValueText);
        if (userDeptValues[0] != "")
        {            
            DeptGUID.ValueText = userDeptValues[0];
            DeptGUID.doValidate();
			//公司別
			string[] userValues = base.getUserInfoById(engine, OriginatorGUID.ValueText);
	        CompanyCode.ValueText = userValues[5];	 			
        }
        else
        {
            DeptGUID.ValueText = "";
			CompanyCode.ValueText = "";
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
                //widhDrawErpForm();
                //base.rejectProcedure();
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
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        base.afterApprove(engine, currentObject, result);
    }
	
	protected void SearchButton_Click(object sender, EventArgs e)
    {
		//System.IO.StreamWriter sw = null;
		//sw = new System.IO.StreamWriter(@"d:\temp\SPERP005.log", true, System.Text.Encoding.Default);
		
        int count = 0;
        decimal totalAmount = 0;
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            string billingUser = CheckUser.ValueText;
            string billingDept = CheckDept.ValueText;
            string billingSDate = StartDate.ValueText;
            string billingEDate = EndDate.ValueText;
            string trafficFee = "0";
            string eatFee = "0";
            string parkingFee = "0";
            string otherFee = "0";
            string etagFee = "0";
            string oilFee = "0";
            string whereCondition = "";
			
			//sw.WriteLine("billingSDate : " + billingSDate);	
			//sw.WriteLine("billingEDate : " + billingEDate);	

            if (!billingUser.Equals(""))
            {
                whereCondition += " and b.id = '" + billingUser + "' ";
            }
            if (!billingDept.Equals(""))
            {
                whereCondition += " and c.id = '" + billingDept + "' ";
            }
            if (!billingSDate.Equals("") && !billingEDate.Equals(""))
            {
                whereCondition += " and TripDate >= '" + billingSDate + "' and TripDate <= '" + billingEDate + "' ";
            }
			if (billingSDate.Equals("") && !billingEDate.Equals(""))
            {
                whereCondition += " and TripDate <= '" + billingEDate + "' ";
            }
            
            DataObject doo = null;
            DataObjectSet dos = BillingDetailList.dataSource;

            string sql = " select a.GUID, b.OID, b.id as UserId, b.userName, TripDate, TrafficFee, EatFee, ParkingFee, OtherFee, SheetNo, c.id as DeptId, StartMileage, EndMileage, EtagFee, MileageSum, a.StartTime, a.EndTime, a.OilFee, a.TripSite , a.EtcStart , a.EtcEnd , a.RefEtagFee ";
			       sql += " From SmpTripForm a, NaNa.dbo.Users b, NaNa.dbo.OrganizationUnit c ";
			       sql += " where a.OriginatorGUID=b.OID and IsTripFee='Y'  and c.OID = a.DeptGUID ";
			       //sql += " and SheetNo in (select SMWYAAA002 from SMWYAAA where SMWYAAA002 like 'SPAD009%'  and SMWYAAA020='Y') ";
				   sql += " and a.GUID in (select SMWYAAA019 from SMWYAAA where SMWYAAA002 like 'SPAD009%'  and SMWYAAA020='Y') ";
                   sql += " and SheetNo not in (select c.OriTripFormGUID From SmpTripBilling a , SMWYAAA b, SmpTripBillingDetail c where a.SheetNo = b.SMWYAAA002  and SMWYAAA020  in ('Y','I')  and c.HeaderGUID=a.GUID ) ";
                   sql += whereCondition;
			//sw.WriteLine("sql : " + sql);

            DataSet allBillingList = engine.getDataSet(sql, "TEMP");
            count = allBillingList.Tables[0].Rows.Count;

            for (int i = 0; i < count; i++)
            {
                doo = dos.create();

                trafficFee = allBillingList.Tables[0].Rows[i]["TrafficFee"].ToString();
                if (trafficFee.Equals("")) { trafficFee = "0"; }
                eatFee = allBillingList.Tables[0].Rows[i]["EatFee"].ToString();
                if (eatFee.Equals("")) { eatFee = "0"; }
                parkingFee = allBillingList.Tables[0].Rows[i]["ParkingFee"].ToString();
                if (parkingFee.Equals("")) { parkingFee = "0"; }
                otherFee = allBillingList.Tables[0].Rows[i]["OtherFee"].ToString();
                if (otherFee.Equals("")) { otherFee = "0"; }
                etagFee = allBillingList.Tables[0].Rows[i]["EtagFee"].ToString();
                if (etagFee.Equals("")) { etagFee = "0"; }
                oilFee = allBillingList.Tables[0].Rows[i]["OilFee"].ToString();
                if (oilFee.Equals("")) { oilFee = "0"; }

                totalAmount = totalAmount + decimal.Parse(trafficFee) + decimal.Parse(eatFee) + decimal.Parse(parkingFee) + decimal.Parse(otherFee) + decimal.Parse(etagFee) + decimal.Parse(oilFee);
				
				//sw.WriteLine("totalAmount : " + totalAmount);
                              
                doo.setData("GUID", IDProcessor.getID(""));
                doo.setData("HeaderGUID", "TEMP");
                doo.setData("UserGUID", allBillingList.Tables[0].Rows[i]["OID"].ToString());
                doo.setData("UserId", allBillingList.Tables[0].Rows[i]["UserId"].ToString());
                doo.setData("UserName", allBillingList.Tables[0].Rows[i]["userName"].ToString());
                doo.setData("TripDate", allBillingList.Tables[0].Rows[i]["TripDate"].ToString());
				doo.setData("StartTime", allBillingList.Tables[0].Rows[i]["StartTime"].ToString());
				doo.setData("EndTime", allBillingList.Tables[0].Rows[i]["EndTime"].ToString());
                doo.setData("StartMileage", allBillingList.Tables[0].Rows[i]["StartMileage"].ToString());
                doo.setData("EndMileage", allBillingList.Tables[0].Rows[i]["EndMileage"].ToString());
                doo.setData("MileageSum", allBillingList.Tables[0].Rows[i]["MileageSum"].ToString());
                doo.setData("OilFee", oilFee);
                doo.setData("TrafficFee", trafficFee);
                doo.setData("EatFee", eatFee);
                doo.setData("ParkingFee", parkingFee);
                doo.setData("EtagFee", etagFee);
				doo.setData("EtcStart", allBillingList.Tables[0].Rows[i]["EtcStart"].ToString());  
				doo.setData("EtcEnd", allBillingList.Tables[0].Rows[i]["EtcEnd"].ToString());  
				doo.setData("RefEtagFee", allBillingList.Tables[0].Rows[i]["RefEtagFee"].ToString());  
                doo.setData("OtherFee", otherFee);
                doo.setData("OriTripFormGUID", allBillingList.Tables[0].Rows[i]["SheetNo"].ToString());                
				doo.setData("TripSite", allBillingList.Tables[0].Rows[i]["TripSite"].ToString());                
				
                doo.setData("IS_LOCK", "N");
                doo.setData("IS_DISPLAY", "Y");
                doo.setData("DATA_STATUS", "Y");

                //dos.add(doo);    
				bool strReturn = true; //檢查是否重覆資料
                //資料檢查
                strReturn = tripDetailRepeatCheck(doo);
                //sw.WriteLine("回傳資料 : " + strReturn);
                if (strReturn)
                {
                    dos.add(doo);
                    BillingDetailList.NoAdd = true;
                    BillingDetailList.dataSource = dos;
                    BillingDetailList.HiddenField = new string[] { "GUID", "HeaderGUID", "UserGUID", "RefEtagFee", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
                    BillingDetailList.updateTable();
                    //TotalAmount.ValueText = totalAmount.ToString();
					calculateTotal();
                }
            }
            
            //BillingDetailList.NoAdd = true;
            //BillingDetailList.dataSource = dos;
            //BillingDetailList.HiddenField = new string[] { "GUID", "HeaderGUID", "UserGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
            //BillingDetailList.updateTable();
            //TotalAmount.ValueText = totalAmount.ToString();
            
            engine.close();

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
		//if (sw != null) sw.Close();
    }

    protected void BillingDetailList_ShowRowData(com.dsc.kernal.databean.DataObject objects)
    {
        UserGUID.GuidValueText = objects.getData("UserGUID");
        UserGUID.doGUIDValidate();
        TripDate.ValueText = objects.getData("TripDate");
		StartTime.ValueText = objects.getData("StartTime");
		EndTime.ValueText = objects.getData("EndTime");
        StartMileage.ValueText = objects.getData("StartMileage");
        EndMileage.ValueText = objects.getData("EndMileage");
        MileageSum.ValueText = objects.getData("MileageSum");
        OilFee.ValueText = objects.getData("OilFee");
        TrafficFee.ValueText = objects.getData("TrafficFee");
        EatFee.ValueText = objects.getData("EatFee");
        ParkingFee.ValueText = objects.getData("ParkingFee");
        EtagFee.ValueText = objects.getData("EtagFee");
		EtcStart.ValueText = objects.getData("EtcStart");
		EtcEnd.ValueText = objects.getData("EtcEnd");
		RefEtagFee.ValueText = objects.getData("RefEtagFee");
        OtherFee.ValueText = objects.getData("OtherFee");
        OriTripFormGUID.ValueText = objects.getData("OriTripFormGUID");        
		TripSite.ValueText = objects.getData("TripSite");                
    }

    protected bool BillingDetailList_SaveRowData(com.dsc.kernal.databean.DataObject objects, bool isNew)
    {
        if (isNew)
        {
            calculateTotal();
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("HeaderGUID", "TEMP");
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
		
		calculateTotal();
        objects.setData("UserGUID", UserGUID.GuidValueText);
        objects.setData("UserID", UserGUID.ValueText);
        objects.setData("UserName", UserGUID.ReadOnlyValueText);
        objects.setData("TripDate", TripDate.ValueText);
		objects.setData("StartTime", StartTime.ValueText);
		objects.setData("EndTime", EndTime.ValueText);
        objects.setData("StartMileage", StartMileage.ValueText);
        objects.setData("EndMileage", EndMileage.ValueText);
        objects.setData("MileageSum", MileageSum.ValueText);
        objects.setData("OilFee", OilFee.ValueText);
        objects.setData("TrafficFee", TrafficFee.ValueText);
        objects.setData("EatFee", EatFee.ValueText);
        objects.setData("ParkingFee", ParkingFee.ValueText);
        objects.setData("OtherFee", OtherFee.ValueText);
        objects.setData("EtagFee", EtagFee.ValueText);        
		objects.setData("EtcStart", EtcStart.ValueText);        
		objects.setData("EtcEnd", EtcEnd.ValueText);        
		objects.setData("RefEtagFee", RefEtagFee.ValueText);        
        objects.setData("OriTripFormGUID", OriTripFormGUID.ValueText);
        objects.setData("TripSite", TripSite.ValueText);
		
		
		if (isNew)
        {
			string[] keys = objects.keyField;
	        objects.keyField = new string[] { "OriTripFormGUID",  };

	        DataObjectSet accessSet = BillingDetailList.dataSource;
	        if (!accessSet.checkData(objects))
	        {
	            MessageBox(" 國內出差單請款資料重覆! ");
	            objects.keyField = keys;
	            return false;
	        }
		}

        return true;
    }

    protected void BillingDetailList_AddOutline(DataObject objects, bool isNew)
    {
		etcFeeCheck(objects);
        calculateTotal();
		
    }

    protected void BillingDetailList_DeleteData()
    {
        calculateTotal();
    }

    private void calculateTotal()
    {
        decimal total = 0;
        try
        {
            for (int i = 0; i < BillingDetailList.dataSource.getAvailableDataObjectCount(); i++)
            {
                total += decimal.Parse(BillingDetailList.dataSource.getAvailableDataObject(i).getData("OilFee"));
                total += decimal.Parse(BillingDetailList.dataSource.getAvailableDataObject(i).getData("TrafficFee"));
                total += decimal.Parse(BillingDetailList.dataSource.getAvailableDataObject(i).getData("EatFee"));
                total += decimal.Parse(BillingDetailList.dataSource.getAvailableDataObject(i).getData("ParkingFee"));
                total += decimal.Parse(BillingDetailList.dataSource.getAvailableDataObject(i).getData("OtherFee"));
                total += decimal.Parse(BillingDetailList.dataSource.getAvailableDataObject(i).getData("EtagFee"));                
            }
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
            // if (sw != null) sw.Close();
        }
        
        TotalAmount.ValueText = total.ToString();
    }
	
	
	
	protected bool tripDetailRepeatCheck(com.dsc.kernal.databean.DataObject objects)
    {
        string[] keys = objects.keyField;
        objects.keyField = new string[] { "OriTripFormGUID"  };

        DataObjectSet accessSet = BillingDetailList.dataSource;
        if (!accessSet.checkData(objects))
        {
            MessageBox(" 國內出差單請款資料重覆! ");
            objects.keyField = keys;
            return false;
        }
        keys = objects.keyField;
        objects.keyField = new string[] { "GUID" };
        return true;
		
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
    protected void CheckUser_BeforeClickButton()
    {
        CheckUser.whereClause = "(leaveDate='' OR leaveDate IS NULL)";        
    }
    protected void UserGUID_BeforeClickButton()
    {
        UserGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
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

    /// <summary>
    /// 取得里程數
    /// </summary>
    /// <returns></returns>
    private string getMiles()
    {
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"d:\temp\SPAD009.log", true, System.Text.Encoding.Default);
        string miles = MileageSum.ValueText;
        string startMileage = StartMileage.ValueText;
        string endMileage = EndMileage.ValueText;
        string actName = Convert.ToString(getSession("ACTName"));
        if (base.isNew())
        {
            if (!startMileage.Equals("") && (!endMileage.Equals("")))
            {
                int startMileageInt = Convert.ToInt32(startMileage);
                int endMileageInt = Convert.ToInt32(endMileage);

                miles = Convert.ToString(endMileageInt - startMileageInt);
                //sw.WriteLine("miles : " + miles);
            }

        }
        //if (sw != null) { sw.Close(); }
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
            int oilFeeInt = Convert.ToInt32(miles) * 6;

            oilFee = Convert.ToString(oilFeeInt);
        }
        return oilFee;
    }
	
	//列印單據
	protected void PrintButton_OnClick(object sender, EventArgs e)
    {
		//MessageBox("SheetNo : " + SheetNo.ValueText);
		Session["SPAD010_SheetNo"] = SheetNo.ValueText;
        string url = "PrintPage.aspx";
	    base.showOpenWindow(url, "列印國內出差旅費核銷單", "", "600", "", "", "", "1", "1", "", "", "", "", "750", "", true);
    }
	
	protected void EtcMileage_SelectChanged(string value)
    {
		string strEtcStart = EtcStart.ValueText;
		string[,] ids = null;
        DataSet ds = null;
        int count = 0;
		
		if (strEtcStart.Equals(""))        
        {
        //    sw.WriteLine("no data!");
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
        //    sw.WriteLine("no data!");
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
            //EtagFee.ValueText = Convert.ToString(maxMiles);
			//miles = EtagFee.ValueText;
			miles = Convert.ToString(maxMiles);
        }
        return miles;
    }
	
	protected bool etcFeeCheck(com.dsc.kernal.databean.DataObject objects)
    {		
		if (!EtagFee.ValueText.Equals("") && !EtagFee.ValueText.Equals("0"))
        {
            if (EtcStart.ValueText.Equals("") || EtcEnd.ValueText.Equals(""))
            {
				MessageBox("請填寫 交流道起~交流道迄！!\n");
                return false;
            }
			if (!EtcStart.ValueText.Equals("") && !EtcEnd.ValueText.Equals(""))
	        {
				if ((Convert.ToDecimal(EtagFee.ValueText)).CompareTo(Convert.ToDecimal(RefEtagFee.ValueText)) > 0)
				{
					//strErrMsg += "ETC費用已超過您出差之交流道起訖總額, 請再次確認!\n";
				
					MessageBox("ETC費用已超過您出差之交流道起訖總額, 請再次確認!\n");
					return false;
				}				
			}
		}
		return true;
		/*
		decimal total = 0;	
		decimal  decimal.Parse(BillingDetailList.dataSource.getAvailableDataObject(i).getData("OilFee"));
	
        string[] keys = objects.keyField;
        objects.keyField = new string[] { "OriTripFormGUID"  };

        DataObjectSet accessSet = BillingDetailList.dataSource;
        if (!accessSet.checkData(objects))
        {
            MessageBox(" 國內出差單請款資料重覆! ");
            objects.keyField = keys;
            return false;
        }
        keys = objects.keyField;
        objects.keyField = new string[] { "GUID" };
        return true;
		*/
    }
}