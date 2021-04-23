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



public partial class SmpProgram_System_Form_SPAD010_Form : BasicFormPage
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
		
        //申請單位
        DeptGUID.clientEngineType = engineType;
        DeptGUID.connectDBString = connectString;
        DeptGUID.ValueText = si.submitOrgID;
        DeptGUID.doValidate();

        CheckDept.clientEngineType = engineType;
        CheckDept.connectDBString = connectString;
        CheckDept.ValueText = si.submitOrgID;
        CheckDept.doValidate();

        //申請人員
        OriginatorGUID.clientEngineType = engineType;
        OriginatorGUID.connectDBString = connectString;            
        if (OriginatorGUID.ValueText.Equals(""))
        {
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }
        		
		//審核人, 暫無使用
        CheckByGUID.clientEngineType = engineType;
        CheckByGUID.connectDBString = connectString;
				
		StartDate.ValueText = DateTimeUtility.getSystemTime2(null).Substring(0, 10);
		EndDate.ValueText = DateTimeUtility.getSystemTime2(null).Substring(0, 10);
		
        TotalAmount.ReadOnly = true;
		DeptGUID.ReadOnly = true;

        CheckUser.clientEngineType = engineType;
        CheckUser.connectDBString = connectString;
        CheckDept.clientEngineType = engineType;
        CheckDept.connectDBString = connectString;
		//sw.Close();
        UserGUID.clientEngineType = engineType;
        UserGUID.connectDBString = connectString; 

		bool isAddNew = base.isNew();
        DataObjectSet requestSet = null;
        if (isAddNew)
        {
            requestSet = new DataObjectSet();
            requestSet.isNameLess = true;
            requestSet.setAssemblyName("WebServerProject");
            requestSet.setChildClassString("WebServerProject.form.SPAD010.SmpTripBillingDetail");
            requestSet.setTableName("SmpTripBillingDetail");
            requestSet.loadFileSchema();
            objects.setChild("SmpTripBillingDetail", requestSet);

        }
        else
        {
            requestSet = objects.getChild("SmpTripBillingDetail");
        }
        BillingDetailList.dataSource = requestSet;
        BillingDetailList.HiddenField = new string[] { "GUID", "HeaderGUID", "UserGUID", "DeptID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        BillingDetailList.updateTable();

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
		
        //起始時間
        StartDate.ValueText = objects.getData("StartDate");
        //截止時間
        EndDate.ValueText = objects.getData("EndDate");        
		//請款總額
		TotalAmount.ValueText = objects.getData("TotalAmount"); 
		
		DataObjectSet requestSet = null;
        requestSet = objects.getChild("SmpTripBillingDetail");
        BillingDetailList.dataSource = requestSet;
        BillingDetailList.updateTable();
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
            objects.setData("TotalAmount", TotalAmount.ValueText);
            objects.setData("StartDate", StartDate.ValueText);
            objects.setData("EndDate", EndDate.ValueText);

            for (int i = 0; i < BillingDetailList.dataSource.getAvailableDataObjectCount(); i++)
            {
                BillingDetailList.dataSource.getAvailableDataObject(i).setData("HeaderGUID", objects.getData("GUID"));
                MessageBox("SaveBillingDetailList!");
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
        string[] values = base.getUserInfo(engine, OriginatorGUID.GuidValueText);

        if (actName.Equals(""))
        {
            if (isNecessary(TotalAmount))
            {
                if (TotalAmount.ValueText.Equals("0"))
                {
                    pushErrorMessage("申請金額必須大於0");
                    result = false;
                }
            }

			//檢查 明細清單 資料
            DataObjectSet chkType = BillingDetailList.dataSource;
            if (chkType.getAvailableDataObjectCount().Equals(0))
            {
                strErrMsg += "請輸入需請款的出差資訊!\n";
            }			
            
            //設定主旨
            if (Subject.ValueText.Equals(""))
            {
                values = base.getUserInfo(engine, OriginatorGUID.GuidValueText);
                string subject = "國內出差旅費核銷單【請款人員：" + values[1] + "日期："+StartDate.ValueText +" ~ " + EndDate.ValueText + " 】";
                if (Subject.ValueText.Equals(""))
                {
                    Subject.ValueText = subject;
                }
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
        //si.ownerID = (string)Session["UserID"]; //表單關系人
        si.ownerID = OriginatorGUID.ValueText;
        //si.ownerName = (string)Session["UserName"];
        si.ownerName = OriginatorGUID.ReadOnlyValueText;
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
        si.ownerID = OriginatorGUID.ValueText;
        si.ownerName = OriginatorGUID.ReadOnlyValueText;
		depData = getDeptInfo(engine, OriginatorGUID.GuidValueText);
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
        try
        {
            //sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
            //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\spad006.log", true);
            //sw.WriteLine("setFlowVariables");			
	        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
	        string creatorId = si.fillerID;
	        string notifierId = ""; //通知人員
			string deptAsstId = ""; 
	        string[] userFunc = getUserRoles(engine, "部門收發", DeptGUID.ValueText);
	        deptAsstId = userFunc[2]; //部門助理	     

			string originatorId = OriginatorGUID.ValueText;
			
			//審核人員
            string checkByGUID = CheckByGUID.GuidValueText;
            string checkById = "";
            if (!checkByGUID.Equals(""))
            {
                string[] values = base.getUserInfo(engine, checkByGUID);
                checkById = values[0];
            }
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

            string totalAmount = TotalAmount.ValueText;

            //sw.WriteLine("managerId=" + managerId);
            xml += "<SPAD010>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
            xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
            xml += "<orgUnitManager DataType=\"java.lang.String\">" + orgUnitManagerId + "</orgUnitManager>";
            xml += "<note1 DataType=\"java.lang.String\"></note1>";
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
        }
        else
        {
            DeptGUID.ValueText = "";
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
		string xml = "";
        string actName = Convert.ToString(getSession("ACTName").ToString());
		
		if (actName.Equals("差勤負責人"))
        {
            //確認ERP單據
            //string result = approveErpForm();
            //if (!result.Equals(""))
            //{
            //    throw new Exception(result);
            //}
        }
	
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

            string billingUser = CheckUser.ValueText;
            string billingDept = CheckDept.ValueText;
            string billingSDate = StartDate.ValueText;
            string billingEDate = EndDate.ValueText;
            string trafficFee = "0";
            string eatFee = "0";
            string parkingFee = "0";
            string otherFee = "0";
            string whereCondition = "";

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

            DataObjectFactory dof = new DataObjectFactory();
            dof.init();
            dof.assemblyName = "WebServerProject";
            dof.childClassString = "WebServerProject.form.SPAD010.SmpTripBillingDetail";
            dof.tableName = "SmpTripBillingDetail";

            dof.addFieldDefinition("UserGUID", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "UserGUID", "UserGUID"), "");
            dof.addFieldDefinition("UserID", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "UserID", "工號"), "");
            dof.addFieldDefinition("userName", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "userName", "姓名"), "");
            dof.addFieldDefinition("TripDate", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "TripDate", "出差日期"), "");
            dof.addFieldDefinition("TrafficFee", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "TrafficFee", "車資"), "");
            dof.addFieldDefinition("EatFee", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "EatFee", "繕雜費"), "");
            dof.addFieldDefinition("ParkingFee", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "ParkingFee", "停車費"), "");
            dof.addFieldDefinition("OtherFee", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "OtherFee", "其他"), "");
            dof.addFieldDefinition("OriTripFormGUID", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "OriTripFormGUID", "出差單據"), "");
            //dof.addFieldDefinition("DeptId", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "DeptId", "部門"), "");

            dof.addFieldDefinition("D_INSERTUSER", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "D_INSERTUSER", "建立者"), "");
            dof.addFieldDefinition("D_INSERTTIME", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "D_INSERTTIME", "建立時間"), "");
            dof.addFieldDefinition("D_MODIFYUSER", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "D_MODIFYUSER", "更新者"), "");
            dof.addFieldDefinition("D_MODIFYTIME", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad010_form_aspx.language.ini", "message", "D_MODIFYTIME", "更新時間"), "");
            dof.addIdentityField("OriTripFormGUID");
            dof.addKeyField("OriTripFormGUID");
            dof.allowAllFieldEmpty();
            string xml = dof.generalXML();

            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.dataObjectSchema = xml;
			
			string sql =  " select b.OID, b.id as UserId, b.userName, TripDate, TrafficFee, EatFee, ParkingFee, OtherFee, SheetNo, c.id as DeptId ";
			       sql += " From SmpTripForm a, NaNa.dbo.Users b, NaNa.dbo.OrganizationUnit c ";
			       sql += " where a.OriginatorGUID=b.OID and IsTripFee='Y'  and c.OID = a.DeptGUID ";
			       sql += " and SheetNo in (select SMWYAAA002 from SMWYAAA where SMWYAAA002 like 'SPAD009%'  and SMWYAAA020='Y') ";
                   sql += " and SheetNo not in (select c.OriTripFormGUID From SmpTripBilling a , SMWYAAA b, SmpTripBillingDetail c where a.SheetNo = b.SMWYAAA002  and SMWYAAA020!='Y' and SMWYAAA020!='I' and c.HeaderGUID=a.GUID ) ";
                   sql += whereCondition;

            DataSet allBillingList = engine.getDataSet(sql, "TEMP");
            count = allBillingList.Tables[0].Rows.Count;

            for (int i = 0; i < count; i++)
            {                
                trafficFee = allBillingList.Tables[0].Rows[0][4].ToString();
                if (trafficFee.Equals("")) { trafficFee = "0"; }
                eatFee = allBillingList.Tables[0].Rows[0][5].ToString();
                if (eatFee.Equals("")) { eatFee = "0"; }
                parkingFee = allBillingList.Tables[0].Rows[0][6].ToString();
                if (parkingFee.Equals("")) { parkingFee = "0"; }
                otherFee = allBillingList.Tables[0].Rows[0][7].ToString();
                if (otherFee.Equals("")) { otherFee = "0"; }

                totalAmount += totalAmount + decimal.Parse(trafficFee) + decimal.Parse(eatFee) + decimal.Parse(parkingFee) + decimal.Parse(otherFee); 

                DataObject doo = dos.create();
                //doo.setData("UserId", groups[i]);
                doo.setData("UserGUID", allBillingList.Tables[0].Rows[0][0].ToString());
                doo.setData("UserId", allBillingList.Tables[0].Rows[0][1].ToString());
                doo.setData("userName", allBillingList.Tables[0].Rows[0][2].ToString());
                doo.setData("TripDate", allBillingList.Tables[0].Rows[0][3].ToString());
                doo.setData("TrafficFee", trafficFee);
                doo.setData("EatFee", eatFee);
                doo.setData("ParkingFee", parkingFee);
                doo.setData("OtherFee", otherFee);
                doo.setData("OriTripFormGUID", allBillingList.Tables[0].Rows[0][8].ToString());
                //doo.setData("DeptId", allBillingList.Tables[0].Rows[0][9].ToString());
                dos.add(doo);
            }
            BillingDetailList.NoAdd = true;
            //RequestList.NoModify = true;
            //RequestList.NoDelete = true;
            BillingDetailList.dataSource = dos;
            BillingDetailList.updateTable();
            //allBillingList.Dispose();

            TotalAmount.ValueText = totalAmount.ToString();
            
            //programList.Dispose();
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
    }

    protected void BillingDetailList_ShowRowData(com.dsc.kernal.databean.DataObject objects)
    {
        UserGUID.GuidValueText = objects.getData("UserGUID");
        UserGUID.doGUIDValidate();
        TripDate.ValueText = objects.getData("TripDate");
        TrafficFee.ValueText = objects.getData("TrafficFee");
        EatFee.ValueText = objects.getData("EatFee");
        ParkingFee.ValueText = objects.getData("ParkingFee");
        OtherFee.ValueText = objects.getData("OtherFee");
        OriTripFormGUID.ValueText = objects.getData("OriTripFormGUID");
    }

    protected bool BillingDetailList_SaveRowData(com.dsc.kernal.databean.DataObject objects, bool isNew)
    {
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\spad006.log", true);
        //sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
        //sw.WriteLine("BillingDetailList_SaveRowData");	
          MessageBox("BillingDetailList_SaveRowData");     
        if (isNew)
        {
            //calculateTotal();
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("HeaderGUID", "TEMP");
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }

        //sw.WriteLine("UserGUID : " + UserGUID.GuidValueText);
        //sw.WriteLine("UserID : " + UserGUID.ValueText);
        //sw.WriteLine("UserName : " + UserGUID.ReadOnlyValueText);
        //sw.WriteLine("TripDate : " + TripDate.ValueText);
        //sw.WriteLine("TrafficFee : " + TrafficFee.ValueText);
        //sw.WriteLine("EatFee : " + EatFee.ValueText);
        //sw.WriteLine("ParkingFee : " + ParkingFee.ValueText);
        //sw.WriteLine("OtherFee : " + OtherFee.ValueText);
        //sw.WriteLine("OriTripFormGUID : " + OriTripFormGUID.ValueText);	

        objects.setData("UserGUID", UserGUID.GuidValueText);
        objects.setData("UserID", UserGUID.ValueText);
        objects.setData("UserName", UserGUID.ReadOnlyValueText);
        objects.setData("TripDate", TripDate.ValueText);
        objects.setData("TrafficFee", TrafficFee.ValueText);
        objects.setData("EatFee", EatFee.ValueText);
        objects.setData("ParkingFee", ParkingFee.ValueText);
        objects.setData("OtherFee", OtherFee.ValueText);
        objects.setData("OriTripFormGUID", OriTripFormGUID.ValueText);
        //sw.WriteLine("BillingDetailList_SaveRowData");	
        //sw.Close();
        return true;
    }

    protected void BillingDetailList_AddOutline(DataObject objects, bool isNew)
    {
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
                total += decimal.Parse(BillingDetailList.dataSource.getAvailableDataObject(i).getData("TrafficFee"));
                total += decimal.Parse(BillingDetailList.dataSource.getAvailableDataObject(i).getData("EatFee"));
                total += decimal.Parse(BillingDetailList.dataSource.getAvailableDataObject(i).getData("ParkingFee"));
                total += decimal.Parse(BillingDetailList.dataSource.getAvailableDataObject(i).getData("OtherFee"));
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
}