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


public partial class SmpProgram_System_Form_SPAD012_Form : SmpBasicFormPage
{

    protected override void init()
    {	
        ProcessPageID = "SPAD012"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPAD012.SmpFacMaintenanceFormAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPAD";		
    }


    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
		//writeLog("initUI^start");
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        string[,] ids = null;
        DataSet ds = null;
        int count = 0;

        //主旨不顯示於發起單據畫面
        SheetNo.Display = false;
        Subject.Display = false;
		CheckByGUID.Display = false;
		OriginatorGUID.Display = false;
		//FacOwnerGUID.Display = false;

        try
        {
		
			//公司別
			string[,] idsCompany = null;
	        idsCompany = new string[,]{
	            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad012_form_aspx.language.ini", "message", "smp", "新普科技")},
	            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad012_form_aspx.language.ini", "message", "tp", "中普科技")}
	        };
	        Company.setListItem(idsCompany);
			string[] values = base.getUserInfoById(engine,(string)Session["UserId"]);
	        Company.ValueText = values[5];
	        Company.ReadOnly = true;

            //申請部門
            OriginatorDeptGUID.clientEngineType = engineType;
            OriginatorDeptGUID.connectDBString = connectString;
            OriginatorDeptGUID.ValueText = si.submitOrgID;
            OriginatorDeptGUID.doValidate();

            //申請人工號. 中文名. 英文名. 職稱
            string[] userValues = base.getUserInfoById(engine, si.fillerID);
			string[] userGuidValue = base.getUserGUID(engine, si.fillerID);
            if (userValues[0] != "")
            {
                OriginatorNumber.ValueText = userValues[0];
                OriginatorCName.ValueText = userValues[1];
                OriginatorEName.ValueText = userValues[2];
                Title.ValueText = userValues[3];
				OriginatorGUID.ValueText = userGuidValue[0];
				Company.ValueText = userValues[5];
            }
            else
            {
                OriginatorNumber.ValueText = "";
                OriginatorCName.ValueText = "";
                OriginatorEName.ValueText = "";
                Title.ValueText = "";
				OriginatorGUID.ValueText = "";
				Company.ValueText = "";
            }

            FacDesc.ValueText = "";
			
			//FAC User				
			ds = engine.getDataSet("select '','' from EmployeeInfo  union select u.OID, u.userName from Groups g, Group_User gu, Users u where g.OID=gu.GroupOID and g.id='SPAD012-FAC' and u.OID=gu.UserOID  ", "TEMP");
            count = ds.Tables[0].Rows.Count;
            ids = new string[0 + count, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";

            for (int i = 1; i < count; i++)
            {
                ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
                ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
            }
            FacOwnerGUID.setListItem(ids);
            
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
           // if (sw != null) sw.Close();
        }

        //審核人
        //CheckByGUID.clientEngineType = engineType;
        //CheckByGUID.connectDBString = connectString;
        		
		//sw.Close();\
        bool isAddNew = base.isNew();

        if (isAddNew)
        {
            FacDesc.ReadOnly = true;
            FacOwnerGUID.ReadOnly = true;
            EstimateCompleteDate.ReadOnly = true;
			ProcessingHours.ReadOnly = true;
            Company.ReadOnly = true;
        }
		
		//sw.Close();

        //改變工具列順序
        base.initUI(engine, objects);
		
		//writeLog("initUI^end");
    }

    protected override void showData(AbstractEngine engine, DataObject objects)
    {	
		//writeLog("showData^start");
		
        base.showData(engine, objects);
        string actName = Convert.ToString(getSession("ACTName"));

        //sw.WriteLine("showData actName : " + actName);
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);

        //表單欄位
        SheetNo.Display = false;
        Subject.Display = false;
		CheckByGUID.Display = false;
		OriginatorGUID.Display = false;
		//FacOwnerGUID.Display = false;		
		
        bool isAddNew = base.isNew();
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號, 主旨含單號
        base.showData(engine, objects);

        //申請人員資訊
        Company.ValueText = objects.getData("Company");
        OriginatorNumber.ValueText = objects.getData("OriginatorNumber");
        OriginatorCName.ValueText = objects.getData("OriginatorCName");
        OriginatorEName.ValueText = objects.getData("OriginatorEName");
		OriginatorGUID.ValueText = objects.getData("OriginatorGUID");
        Title.ValueText = objects.getData("Title");
        Extension.ValueText = objects.getData("Extension");
        UserDesc.ValueText = objects.getData("UserDesc");
        FacDesc.ValueText = objects.getData("FacDesc");
		EstimateCompleteDate.ValueText = objects.getData("EstimateCompleteDate");		
		FacOwnerGUID.ValueText = objects.getData("FacOwnerGUID");
		ProcessingHours.ValueText = objects.getData("ProcessingHours");
		//writeLog(objects.getData("FacOwnerGUID"));

        //申請單位
        OriginatorDeptGUID.GuidValueText = objects.getData("OriginatorDeptGUID");
        OriginatorDeptGUID.doGUIDValidate();
        
        Title.ValueText = objects.getData("Title");
               
        //審核人員
        string checkByGUID = objects.getData("CheckByGUID");
        //if (!checkByGUID.Equals(""))
        //{
        //    CheckByGUID.GuidValueText = checkByGUID; //將值放入人員開窗元件中, 資料庫存放GUID
        //    CheckByGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        //}

        if (!isAddNew)
        {
            Subject.ReadOnly = true;
            SheetNo.ReadOnly = true;
            Company.ReadOnly = true;
            OriginatorDeptGUID.ReadOnly = true;
            OriginatorNumber.ReadOnly = true;
            OriginatorCName.ReadOnly = true;
            OriginatorEName.ReadOnly = true;
            Title.ReadOnly = true;
            Extension.ReadOnly = true;
            CheckByGUID.ReadOnly = true;
            UserDesc.ReadOnly = true;
            FacDesc.ReadOnly = true;
            FacOwnerGUID.ReadOnly = true;
            EstimateCompleteDate.ReadOnly = true;
			ProcessingHours.ReadOnly = true;
        }
        
        if (actName.Equals("廠務窗口")) {
			if (EstimateCompleteDate.ValueText.Equals(""))
			{
				//預計完成日自動加3天
				DateTime dt = DateTime.Now.AddDays(3) ;
				EstimateCompleteDate.ValueText = dt.ToShortDateString().ToString();
				EstimateCompleteDate.ValueText = DateTimeUtility.getSystemTime2(null).Substring(0, 10);
			}
			if (FacOwnerGUID.ValueText.Equals(""))
			{	
				//FAC User				
				SubmitInfo si = (SubmitInfo)getSession("SubmitInfo"); //填表人的一些基本資訊
				string facUser = (string)Session["UserID"]; //預設帶出fac owner 				
				string[,] ids = null;
				DataSet ds = null;
				int count = 0;
				ds = engine.getDataSet("select u.OID, u.userName from Groups g, Group_User gu, Users u where g.OID=gu.GroupOID and g.id='SPAD012-FAC' and u.OID=gu.UserOID and u.id='"+facUser+"'   union all select u.OID, u.userName from Groups g, Group_User gu, Users u where g.OID=gu.GroupOID and g.id='SPAD012-FAC' and u.OID=gu.UserOID and u.id<>'"+facUser+"' ", "TEMP");
	            count = ds.Tables[0].Rows.Count;
	            ids = new string[0 + count, 2];
	            ids[0, 0] = "";
	            ids[0, 1] = "";

	            for (int i = 0; i < count; i++)
	            {
	                ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
	                ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
	            }
	            FacOwnerGUID.setListItem(ids);
			}
            FacDesc.ReadOnly = false;
            AddSignButton.Display = true; //允許加簽, 
			EstimateCompleteDate.ReadOnly = false;
			FacDesc.ReadOnly = false;
			ProcessingHours.ReadOnly = false;
			FacOwnerGUID.ReadOnly = false;
        }

        //if (actName.Equals("廠務主管")) {
        //    FacOwnerGUID.ReadOnly = false;
        //}

        //if (actName.Equals("廠務承辦人員")) {            
        //    FacDesc.ReadOnly = false;
		//	ProcessingHours.ReadOnly = false;
		//	AddSignButton.Display = true; //允許加簽,
        //}

        //writeLog("showData^End");
    }
        
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
		//writeLog("saveData^start");
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");            
            bool isAddNew = base.isNew(); //base 父類別

            if (isAddNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("Subject", Subject.ValueText);
                objects.setData("Company", Company.ValueText);
                objects.setData("OriginatorDeptGUID", OriginatorDeptGUID.GuidValueText);
                objects.setData("Title", Title.ValueText);
                objects.setData("OriginatorNumber", OriginatorNumber.ValueText);
                objects.setData("OriginatorCName", OriginatorCName.ValueText);
                objects.setData("OriginatorEName", OriginatorEName.ValueText);
				objects.setData("OriginatorGUID", OriginatorGUID.ValueText);
                objects.setData("Extension", Extension.ValueText);
				objects.setData("CheckByGUID", CheckByGUID.ValueText);
                objects.setData("UserDesc", UserDesc.ValueText);
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("DATA_STATUS", "Y");
                //主旨含單號
                base.saveData(engine, objects);
                //sw.WriteLine("主旨含單號");
            }
			//sw.WriteLine("saveData Line217==> " + DeliveryDate.ValueText);
            objects.setData("EstimateCompleteDate", EstimateCompleteDate.ValueText);
            objects.setData("FacDesc", FacDesc.ValueText);
			objects.setData("FacOwnerGUID", FacOwnerGUID.ValueText);
			objects.setData("ProcessingHours", ProcessingHours.ValueText);
			//sw.WriteLine("EstimateCompleteDate==> " + EstimateCompleteDate.ValueText);
			//sw.WriteLine("FacDesc==> " + FacDesc.ValueText);

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
		//writeLog("saveData^end - " & FacOwnerGUID.ValueText);
    }

    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
		//writeLog("checkFieldData^start");
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\spad006.log", true);
        //sw.WriteLine("checkFieldData ");
        
        bool result = true;
        string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));
        string[] values = base.getUserInfo(engine, OriginatorDeptGUID.GuidValueText);
		string strTemp = "";

        //sw.WriteLine("actName=>> " + actName);        

        if (actName.Equals(""))
        {
            if (OriginatorNumber.ValueText.Equals("")) { strErrMsg += "工號不可為空值\n";  }
            if (OriginatorCName.ValueText.Equals("")) { strErrMsg += "申請人(中)不可為空值\n"; }
            if (OriginatorEName.ValueText.Equals("")) { strErrMsg += "申請人(英)不可為空值\n"; }
            if (OriginatorDeptGUID.ValueText.Equals("")) { strErrMsg += "部門不可為空值\n"; }
            if (Title.ValueText.Equals("")) { strErrMsg += "職稱不可為空值\n"; }
            if (Extension.ValueText.Equals("")) { strErrMsg += "分機不可為空值\n"; }
            if (UserDesc.ValueText.Equals("")) { strErrMsg += "使用者需求說明不可為空值\n"; }
            
            //設定主旨
			strTemp = UserDesc.ValueText;
			if (strTemp.Length > 20)
            {
                strTemp = strTemp.Substring(0, 20);
            }

            if (Subject.ValueText.Equals(""))
            {
                //values = base.getUserInfo(engine, RequestList.ValueText);
                string subject = OriginatorCName.ValueText + " ，需求說明：" + strTemp ;
                if (Subject.ValueText.Equals(""))
                {
                    Subject.ValueText = subject;
                }
            }
        
        }
        if (actName.Equals("廠務窗口"))
        {
            if (EstimateCompleteDate.ValueText.Equals("")) { strErrMsg += "預計完成日不可為空值\n"; }
			if (FacDesc.ValueText.Equals("")) { strErrMsg += "廠務處理說明不可為空值\n"; }
			if (ProcessingHours.ValueText.Equals("")) { strErrMsg += "處理時間不可為空值\n"; }
			if (ProcessingHours.ValueText.Equals("0"))
            {
                 strErrMsg += "處理時間必須為數值\n"; 
            }
        }
        //if (actName.Equals("廠務主管"))
        //{
        //    if (FacOwnerGUID.ValueText.Equals("")) { strErrMsg += "請指廠務承辦人員\n"; }
        //}

        //if (actName.Equals("廠務承辦人員"))
        //{
        //    if (FacDesc.ValueText.Equals("")) { strErrMsg += "廠務處理說明不可為空值\n"; }
		//	if (ProcessingHours.ValueText.Equals("")) { strErrMsg += "處理時間不可為空值\n"; }
        //}
        
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
		//writeLog("initSubmitInfo^start");
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
		//writeLog("initSubmitInfo^end");
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
		//depData = getDeptInfo(engine, OriginatorGUID.GuidValueText);
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
		writeLog("setFlowVariables^start");
        System.IO.StreamWriter sw = null;
        string xml = "";
        try
        {
            //sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\SPAD012.log", true);
            //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true);
            //sw.WriteLine("setFlowVariables");
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

            //填表人
            string creatorId = OriginatorNumber.ValueText;
			
			//writeLog("creatorId >> " + creatorId);

            //填表人員的主管
            string[] userGuidValues = base.getUserGUID(engine, creatorId);
            string[] values = base.getUserManagerInfo(engine, userGuidValues[0]);
            string managerGUID = values[0];
			values = base.getUserInfo(engine, managerGUID);
            string managerId = values[0];
			
						
            //審核人員
            string checkByGUID = "";
            string checkById = "";
            if (!checkByGUID.Equals(""))
            {
                values = base.getUserInfo(engine, checkByGUID);
                checkById = values[0];
            }
            //sw.WriteLine("checkById=" + checkById);

            //廠務處理人員		
			string facOwnerGUID = FacOwnerGUID.ValueText;
			//sw.WriteLine("facOwnerGUID=" + facOwnerGUID);
			//writeLog("facOwnerGUID >> " + facOwnerGUID);
            string facOwnerId = "";	
            if (!facOwnerGUID.Equals(""))
            {
                values = base.getUserInfo(engine, facOwnerGUID);
                facOwnerId = values[0];
            }
			//sw.WriteLine("facOwnerId=" + facOwnerId);
			writeLog("facOwnerId >> " + facOwnerId);
						
            xml += "<SPAD012>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
            xml += "<originator DataType=\"java.lang.String\">" + creatorId + "</originator>";
            xml += "<checkby1 DataType=\"java.lang.String\">" + checkById + "</checkby1>";
            xml += "<manager DataType=\"java.lang.String\">" + managerId + "</manager>";
            xml += "<facwindow DataType=\"java.lang.String\">SPAD012-FAC</facwindow>";
            xml += "<facmgr DataType=\"java.lang.String\">SPAD012-FACMGR</facmgr>";
            xml += "<facowner DataType=\"java.lang.String\">" + facOwnerId + "</facowner>";
            xml += "<notify1 DataType=\"java.lang.String\"></notify1>";
            xml += "</SPAD012>";
            writeLog("xmL >> " + xml);
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
            if (sw != null) sw.Close();
        }
        //表單代號
        param["SPAD012"] = xml;
		
		writeLog("setFlowVariables^end");
		
        return "SPAD012";
    }

    protected void OriginatorGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
            
        //更新申請人資料
        string[] userValues = base.getUserInfoById(engine, OriginatorNumber.ValueText);
		string[] userGuidValue = base.getUserGUID(engine, OriginatorNumber.ValueText);
        if (userValues[0] != "")
        {
            //OriginatorNumber.ValueText = userValues[0];
            OriginatorCName.ValueText = userValues[1];
            OriginatorEName.ValueText = userValues[2];
            Title.ValueText = userValues[3];
			OriginatorGUID.ValueText = userGuidValue[0];
			Company.ValueText = userValues[5];
        }
        else
        {
            //OriginatorNumber.ValueText = "";
            OriginatorCName.ValueText = "";
            OriginatorEName.ValueText = "";
            Title.ValueText = "";
			OriginatorGUID.ValueText = "";
			Company.ValueText = "";
        }

        //更新出差人員主要部門
        string[] userGuidValues = base.getUserGUID(engine, OriginatorNumber.ValueText);
        if (userGuidValues[0] != "")
        {
            string[] userDeptValues = base.getDeptInfo(engine, userGuidValues[0]);
            if (userDeptValues[0] != "")
            {
                OriginatorDeptGUID.ValueText = userDeptValues[0];
                OriginatorDeptGUID.doValidate();
            }
            else
            {
                OriginatorDeptGUID.ValueText = "";
            }
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
		//writeLog("afterSign^start");
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
		//writeLog("rejectProcedure^end");
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

    protected void OriginatorNumber_SingleFieldButtonClick(object sender, EventArgs e)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

        //更新基本資料
        string[] userValues = base.getUserInfoById(engine, OriginatorNumber.ValueText);
		string[] userGuidValue = base.getUserInfo(engine, OriginatorNumber.ValueText);
        if (userValues[0] != "")
        {
            OriginatorNumber.ValueText = userValues[0];  //申請人工號
            OriginatorCName.ValueText = userValues[1];   //中文名
            OriginatorEName.ValueText = userValues[2];   //英文名
            Title.ValueText = userValues[3];              // 職稱
			OriginatorGUID.ValueText = userGuidValue[0];  //GUID
			Company.ValueText = userValues[5];
        }

        //更新申請人員主要部門
        string[] userGuidValues = base.getUserGUID(engine, OriginatorNumber.ValueText);
        if (userGuidValues[0] != "")
        {
            string[] userDeptValues = base.getDeptInfo(engine, userGuidValues[0]);
            if (userDeptValues[0] != "")
            {
                OriginatorDeptGUID.ValueText = userDeptValues[0];
                OriginatorDeptGUID.doValidate();
            }
            else
            {
                OriginatorDeptGUID.ValueText = "";
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
	
	
	protected void FacUser_SelectChanged(string value)
    {
        string strFacOwner = FacOwnerGUID.ValueText;
		//IsIncludeDateEve.ValueText = getIsIncludeDateEve();
		string[,] ids = null;
        //string sql = null;
        DataSet ds = null;
        int count = 0;
		
		if (strFacOwner.Equals(""))     
        {
        //    sw.WriteLine("no data!");
            ids[0, 0] = "";
            ids[0, 1] = "";
            FacOwnerGUID.setListItem(ids);
        }
    }

    protected void CheckByGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            //CheckByGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
	
	private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPAD012.log", true, System.Text.Encoding.Default);
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