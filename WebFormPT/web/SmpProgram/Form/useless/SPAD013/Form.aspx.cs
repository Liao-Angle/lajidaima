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


public partial class SmpProgram_System_Form_SPAD013_Form : SmpAdFormPage
{

    protected override void init()
    {	
        ProcessPageID = "SPAD013"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPAD013.SmpStationeryFormAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPAD";		
    }


    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        string[,] ids = null;
        DataSet ds = null;
        int count = 0;

        //主旨不顯示於發起單據畫面
        SheetNo.ReadOnly = true;

        try
        {
            //公司別			
			string[,] idsCompany = null;
	        idsCompany = new string[,]{
	            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad013_form_aspx.language.ini", "message", "smp", "新普科技")},
	            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad013_form_aspx.language.ini", "message", "tp", "中普科技")},
				{"STCS",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad013_form_aspx.language.ini", "message", "stcs", "新世電子")}
	        };
	        Company.setListItem(idsCompany);
			string[] values = base.getUserInfoById(engine,(string)Session["UserID"]);
	        Company.ValueText = values[5];
	        Company.ReadOnly = true;

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

            ReqDesc.ValueText = "";
			HrDesc.ValueText = "";
			TotalAmount.ValueText = "";
			DueDate.ValueText = "";			
            
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
           // if (sw != null) sw.Close();
        }
       
	    bool isAddNew = base.isNew();
		
		StationeryGUID.clientEngineType = engineType;
        StationeryGUID.connectDBString = connectString;		
        DataObjectSet requestSet = null;
        if (isAddNew)
        {
            requestSet = new DataObjectSet();
            requestSet.isNameLess = true;
            requestSet.setAssemblyName("WebServerProject");
            requestSet.setChildClassString("WebServerProject.form.SPAD013.SmpStationeryDetail");
            requestSet.setTableName("SmpStationeryDetail");
            requestSet.loadFileSchema();
            objects.setChild("SmpStationeryDetail", requestSet);
        }
        else
        {
            requestSet = objects.getChild("SmpStationeryDetail");
        }
        RequestList.dataSource = requestSet;
        RequestList.HiddenField = new string[] { "GUID", "HeaderGUID", "StationeryGUID",  "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
		RequestList.reSortCondition("品名", DataObjectConstants.ASC);
        RequestList.updateTable();

        if (isAddNew)
        {
            DueDate.ReadOnly = true;            
            HrDesc.ReadOnly = true;
            Company.ReadOnly = true;
			TotalAmount.ReadOnly = true;
			lbRequestTips.Text = "請先維護 [申請文具之品名規格]  後，點選[新增]按鈕。 ";
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
        SheetNo.ReadOnly = true;

        bool isAddNew = base.isNew();
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號, 主旨含單號
        base.showData(engine, objects);
		SheetNo.ValueText = objects.getData("SheetNo");
		//Company.ValueText = "新普科技";
        Company.ValueText = objects.getData("Company");
		
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
		
        //申請人員資訊
		Company.ValueText = objects.getData("Company");
		OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID");
		ReqDesc.ValueText = objects.getData("ReqDesc");
        HrDesc.ValueText = objects.getData("HrDesc");
		TotalAmount.ValueText = objects.getData("TotalAmount");		
		DueDate.ValueText = objects.getData("DueDate");
		       
	       
        //申請人員資訊        
        DataObjectSet requestSet = null;
        requestSet = objects.getChild("SmpStationeryDetail");
        RequestList.dataSource = requestSet;
        RequestList.updateTable();

        if (!isAddNew)
        {
            Subject.ReadOnly = true;
            SheetNo.ReadOnly = true;
            Company.ReadOnly = true;
            DeptGUID.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            CheckByGUID.ReadOnly = true;
            ReqDesc.ReadOnly = true;
            HrDesc.ReadOnly = true;
            TotalAmount.ReadOnly = true;
            DueDate.ReadOnly = true;
			RequestList.NoAdd = true;                                                                                                                                                     
			RequestList.NoDelete = true;                                                                                                       
			RequestList.NoModify = true;			
        }
        
        for (int i = 0; i < RequestList.dataSource.getAvailableDataObjectCount(); i++)
        {
            RequestList.dataSource.getAvailableDataObject(i).setData("SmpStationeryDetail", objects.getData("GUID"));
        }
        
        if (actName.Equals("HR負責人員")) {            
            HrDesc.ReadOnly = false;
			DueDate.ReadOnly = false;
			AddSignButton.Display = true; //允許加簽,
        }

    }
        
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
		writeLog("saveData^start");
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");            
            bool isAddNew = base.isNew(); //base 父類別

            if (isAddNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("Subject", Subject.ValueText);
                objects.setData("Company", Company.ValueText);
                objects.setData("DeptGUID", DeptGUID.GuidValueText);
				objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
				objects.setData("CheckByGUID", CheckByGUID.GuidValueText);
				objects.setData("TotalAmount", TotalAmount.ValueText);
                objects.setData("ReqDesc", ReqDesc.ValueText);
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("DATA_STATUS", "Y");
                //主旨含單號
                base.saveData(engine, objects);
                //sw.WriteLine("主旨含單號");
            }
			//sw.WriteLine("saveData Line217==> " + DeliveryDate.ValueText);
            objects.setData("DueDate", DueDate.ValueText);
            objects.setData("HrDesc", HrDesc.ValueText);

            for (int i = 0; i < RequestList.dataSource.getAvailableDataObjectCount(); i++)
            {
                RequestList.dataSource.getAvailableDataObject(i).setData("HeaderGUID", objects.getData("GUID"));
            }

            //MessageBox("nonono!");

            //beforeSetFlow
            setSession("IsSetFlow", "Y");
            //sw.Close();
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
           // if (sw != null) sw.Close();
        }
		//writeLog("saveData^end - " & MisOwnerGUID.ValueText);
    }

    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
		bool result = true;
        string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));
        string[] values = base.getUserInfo(engine, DeptGUID.GuidValueText);
		string strTemp = "";
		string[] depData = getDeptInfo(engine, OriginatorGUID.GuidValueText);

        if (actName.Equals(""))
        {
            if (OriginatorGUID.ValueText.Equals("")) { strErrMsg += "申請人不可為空值\n";  }
            if (DeptGUID.ValueText.Equals("")) { strErrMsg += "申請部門不可為空值\n"; }
            if (ReqDesc.ValueText.Equals("")) { strErrMsg += "使用者需求說明不可為空值\n"; }
            
			//檢查 明細清單 資料
            DataObjectSet chkType = RequestList.dataSource;
            if (chkType.getAvailableDataObjectCount().Equals(0))
            {
                strErrMsg += "請輸入文具申請清單!\n";
            }
			
            //設定主旨
			strTemp = ReqDesc.ValueText;
			if (strTemp.Length > 50)
            {
                strTemp = strTemp.Substring(0, 50);
            }

            if (Subject.ValueText.Equals(""))
            {
                //values = base.getUserInfo(engine, RequestList.ValueText);
                string subject = "文具申請單【申請部門：" + depData[1] + "   " + strTemp + " 】";
                if (Subject.ValueText.Equals(""))
                {
                    Subject.ValueText = subject;
                }
            }        
        }
        
        if (actName.Equals("HR負責人員"))
        {
            if (HrDesc.ValueText.Equals("")) { strErrMsg += "處理說明不可為空值\n"; }
			if (DueDate.ValueText.Equals("")) { strErrMsg += "預計購入日期不可為空值\n"; }
        }
        
		writeLog("checkFieldData^end");
        if (!strErrMsg.Equals(""))
        {
            pushErrorMessage(strErrMsg);
            result = false;
        }
        //sw.Close();
		//writeLog("checkFieldData^end");
        return result;        
    }

    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"]; //填表人, 登入者
        si.fillerName = (string)Session["UserName"]; //填表人姓名
        si.fillerOrgID = depData[0]; 
        si.fillerOrgName = depData[1];
		si.ownerID = OriginatorGUID.ValueText;     //表單關系人=出差人員
        si.ownerName = OriginatorGUID.ReadOnlyValueText;
        si.ownerID = (string)Session["UserID"]; //表單關系人
        si.ownerName = (string)Session["UserName"];
        //si.ownerOrgID = depData[0];
        //si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0]; //發起單位代號
        si.objectGUID = objects.getData("GUID");
        return si;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
		//writeLog("getSubmitInfo^start");
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);
        //string[] depDataRealtionship = getDeptInfo(engine, (string)OriginatorGUID.GuidValueText);

        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];
        si.ownerName = (string)Session["UserName"];
		//si.ownerID = OriginatorGUID.ValueText;
        //si.ownerName = OriginatorGUID.ReadOnlyValueText;
		depData = getDeptInfo(engine, OriginatorGUID.GuidValueText);
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");
        //MessageBox("getSubmitInfo");
		//writeLog("getSubmitInfo^end");
		
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
		//writeLog("setFlowVariables^start");
        System.IO.StreamWriter sw = null;
        string xml = "";
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

            //填表人
            string creatorId = si.fillerID;

            //填表人員的主管
            string[] userGuidValues = base.getUserGUID(engine, creatorId);
            string[] values = base.getUserManagerInfo(engine, userGuidValues[0]);
            string managerGUID = values[0];
            
            values = base.getUserInfo(engine, managerGUID);
            string managerId = values[0];
			
			string orgUnitManagerId = "";
            string[] userInfo = base.getUserGUID(engine, creatorId);
            string[] managerInfo = base.getUserManagerInfo(engine, userInfo[0]);
            if (managerInfo[1].Equals(""))
            {
                orgUnitManagerId = managerInfo[1];
            }
            string[] orgUnitInfo = base.getOrgUnitInfo(engine, DeptGUID.GuidValueText);
            if (!orgUnitInfo[3].Equals(""))
            {
                orgUnitManagerId = orgUnitInfo[3];
            }
			//20130911修改, 當填單人=部門主管時, 則簽該人員直屬主管
			if (orgUnitManagerId.Equals(creatorId))
            {
                orgUnitManagerId = managerId;
            }
            //sw.WriteLine("填表人員的主管=" + managerId);

            //審核人員
            string checkByGUID = CheckByGUID.GuidValueText;
            string checkById = "";
            if (!checkByGUID.Equals(""))
            {
                values = base.getUserInfo(engine, checkByGUID);
                checkById = values[0];
            }
            //sw.WriteLine("checkById=" + checkById);
			
			decimal totalAmount = decimal.Parse(TotalAmount.ValueText);
            
            xml += "<SPAD013>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
            xml += "<originator DataType=\"java.lang.String\">" + creatorId + "</originator>";
            xml += "<checkby DataType=\"java.lang.String\">" + checkById + "</checkby>";
            xml += "<manager DataType=\"java.lang.String\">" + orgUnitManagerId + "</manager>";
            xml += "<notify DataType=\"java.lang.String\"></notify>";
			xml += "<totalamount DataType=\"java.lang.Integer\">" + totalAmount + "</totalamount>";
            xml += "</SPAD013>";
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
        param["SPAD013"] = xml;
		
		//writeLog("setFlowVariables^end");
		
        return "SPAD013";
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
	        Company.ValueText = userValues[5];	
        }
        else
        {
            DeptGUID.ValueText = "";
			Company.ValueText = "";	
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
		writeLog("afterSign^start");
        string signProcess = Convert.ToString(Session["signProcess"]);
        if (signProcess.Equals("N")) //不同意
        {
            //updateSourceStatus("Terminate");
        }
        base.afterSign(engine, currentObject, result);
		//writeLog("afterSign^end");
    }

    /// <summary>
    /// 重辦程序
    /// </summary>    
    protected override void rejectProcedure()
    {
		//writeLog("rejectProcedure^start");
        //String backActID = Convert.ToString(Session["tempBackActID"]); //退回關卡 ID        
        //先退回
        //base.rejectProcedure();
        //if (backActID == "creator") //流程之中, 申請人關卡的 ID 值
        //{
       //     //終止流程
        //    SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        //    base.terminateThisProcess(si.ownerID);
        //}
		
		//退回填表人終止流程
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回後關卡ID
        if (backActID.ToUpper().Equals("CREATOR") || backActID.ToUpper().Equals("ACT29"))
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
		writeLog("rejectProcedure^end");
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


    protected void RequestList_ShowRowData(DataObject objects)
    {
        StationeryGUID.GuidValueText = objects.getData("StationeryGUID");
		if (!StationeryGUID.GuidValueText.Equals(""))
        {
            StationeryGUID.doGUIDValidate();
        }
		Quantity.ValueText = objects.getData("Quantity");
        UserDesc.ValueText = objects.getData("UserDesc");
        OtherDesc.ValueText = objects.getData("OtherDesc");
    }

    protected bool RequestList_SaveRowData(DataObject objects, bool isNew)
    {
		if (isNew)
        {
			string[] keys = objects.keyField;
	        objects.keyField = new string[] { "GUID", "StationeryGUID" };
			
			if (StationeryGUID.GuidValueText.Equals(""))
	        {
	            MessageBox(lblStationeryGUID.Text + ": 必需選擇!");
				return false;
	        }
			if (Quantity.ValueText.Equals(""))
	        {
				MessageBox(LBQuantity.Text +  ": 必需選擇!");
				return false;
	        }			
			if (Quantity.ValueText.Equals("0"))
	        {
				MessageBox(LBQuantity.Text +  ": 金額必須大於0!");
				return false;
	        }

			DataObjectSet readerSet = RequestList.dataSource;
	        for (int i = 0; i < readerSet.getAvailableDataObjectCount(); i++)
	        {
	            string requestType = readerSet.getAvailableDataObject(i).getData("ProdDesc");
				
	            if (StationeryGUID.ValueText.Equals(requestType))
	            {					
	                MessageBox(requestType + " 文具申請資料重覆!");
	                return false;
	            }

	        }
		}
		
		if (isNew)
        {
			objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("HeaderGUID", "TEMP");
			objects.setData("StationeryGUID", StationeryGUID.GuidValueText);
	        objects.setData("Quantity", Quantity.ValueText);
			objects.setData("UserDesc", UserDesc.ValueText);
			objects.setData("OtherDesc", OtherDesc.ValueText);
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");            
        }
        objects.setData("StationeryGUID", StationeryGUID.GuidValueText);
        objects.setData("ProdDesc", StationeryGUID.ValueText);
        objects.setData("Unit", StationeryGUID.ReadOnlyValueText);
		objects.setData("Quantity", Quantity.ValueText);
		objects.setData("UserDesc", UserDesc.ValueText);
		objects.setData("OtherDesc", OtherDesc.ValueText);

        return true;
    }
	
    
    protected void OriginatorNumber_SingleFieldButtonClick(object sender, EventArgs e)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

        //更新申請人員主要部門
        string[] userGuidValues = base.getUserGUID(engine, OriginatorGUID.GuidValueText);
        if (userGuidValues[0] != "")
        {
            string[] userDeptValues = base.getDeptInfo(engine, userGuidValues[0]);
            if (userDeptValues[0] != "")
            {
                DeptGUID.ValueText = userDeptValues[0];
                DeptGUID.doValidate();
            }
            else
            {
                DeptGUID.ValueText = "";
            }
        }
    }

    protected void CheckByGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
    }
	
    protected void OriginatorGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            OriginatorGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
    	
	protected bool detailRepeatCheck(com.dsc.kernal.databean.DataObject objects)
    {
        string[] keys = objects.keyField;
        objects.keyField = new string[] { "StationeryGUID" };

        DataObjectSet accessSet = RequestList.dataSource;
        if (!accessSet.checkData(objects))
        {
            MessageBox(" 文具申請品名資料重覆! ");
            objects.keyField = keys;
            return false;
        }
        keys = objects.keyField;
        objects.keyField = new string[] { "GUID" };
        return true;
		
    }	

    protected void CheckByGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            CheckByGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
    
    protected void StationeryGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
    }
	
	/// 過濾已失效文具品名
	protected void StationeryGUID_BeforeClickButton()
    {
		//writeLog("StationeryGUID_BeforeClickButton^start");
        if (base.isNew())
        {
            StationeryGUID.whereClause = "(Active='Y')";
        }
		//writeLog("StationeryGUID_BeforeClickButton^end");
    }
	
	protected void RequestList_AddOutline(DataObject objects, bool isNew)
    {
        calculateTotal();
    }
	
	protected void RequestList_DeleteData()
    {
        calculateTotal();
    }
	
	private void calculateTotal()
    {
		int count = 0;
        decimal ttlAmount = 0;
		AbstractEngine engine = null;
        decimal total = 0;
		string reqPrice = "0";
		string reqQty = "0";
		string strStationery = "";
        try
        {
			string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
			
            for (int i = 0; i < RequestList.dataSource.getAvailableDataObjectCount(); i++)
            {
				strStationery = RequestList.dataSource.getAvailableDataObject(i).getData("StationeryGUID");
				reqQty = RequestList.dataSource.getAvailableDataObject(i).getData("Quantity");
				//MessageBox("strStationery : " + strStationery + "   reqQty : " + reqQty);
				
				DataObject doo = null;
				DataObjectSet dos = RequestList.dataSource;

	            string sql = "    select GUID, ProdDesc, Unit, Price, AttributeName from SmpHrStationeryMt ";
	                   sql += " where GUID = '" + strStationery + "' ";

	            DataSet allBillingList = engine.getDataSet(sql, "TEMP");
	            count = allBillingList.Tables[0].Rows.Count;
				for (int j = 0; j < count; j++)
				{
	                doo = dos.create();
	                reqPrice = allBillingList.Tables[0].Rows[j]["Price"].ToString();
	                if (reqPrice.Equals("")) { reqPrice = "0"; }
					ttlAmount += decimal.Parse(reqPrice) * decimal.Parse(reqQty);
					//MessageBox("ttlAmount : " + ttlAmount);
				}	
				
				total += ttlAmount;
				
            }
			engine.close();
        }
        catch { };

        TotalAmount.ValueText = total.ToString();
    }
	
	
	private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPAD013.log", true, System.Text.Encoding.Default);
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