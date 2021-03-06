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
using System.Xml;


public partial class SmpProgram_System_Form_SPIT005_Form : SmpBasicFormPage
{

    protected override void init()
    {	
        ProcessPageID = "SPIT005"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPIT005.SmpInfoDemandForTEAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPIT";		
    }


    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
		//writeLog("initUI^start");

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
		string userId = (string)Session["UserID"];
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        string[,] ids = null;
        DataSet ds = null;
        int count = 0;
		string sql = null;
        
        //主旨不顯示於發起單據畫面
        SheetNo.Display = false;
        Subject.Display = false;

        try
        {
			//公司別
            ids = new string[,]{
                {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spit001_form_aspx.language.ini", "message", "smp", "新普科技")},
                {"TE",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spit001_form_aspx.language.ini", "message", "te", "嘉普科技")},
                {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spit001_form_aspx.language.ini", "message", "tp", "中普科技")},
				{"STCS",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spit001_form_aspx.language.ini", "message", "stcs", "新世電子")},
				{"SCQ",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spit001_form_aspx.language.ini", "message", "scq", "新普重慶")}
            };
            Company.setListItem(ids);
            string orgId = "SMP";
            sql = "select orgId from EmployeeInfo where empNumber='" + userId + "'";
            string value = (string)engine.executeScalar(sql);

            if (value != null)
            {
                orgId = value;
            }
            Company.ValueText = orgId;
            Company.ReadOnly = true;

			
            //預期效益類別
            ids = new string[,]{
                {"Human",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spit005_form_aspx.language.ini", "message", "human", "人力")},
				{"Cost",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spit005_form_aspx.language.ini", "message", "cost", "費用")}
            };
            BenefitType.setListItem(ids);
            if (BenefitType.ValueText.Equals(""))
            {
                BenefitType.ValueText = "人力";
				LBBenefitHours.Text = "小時(每月)";
            }else if (BenefitType.ValueText.Equals("Cost")){
				BenefitType.ValueText = "費用";
				LBBenefitHours.Text = "金額(每月)";
			}
			
            //申請部門
            OriginatorDeptGUID.clientEngineType = engineType;
            OriginatorDeptGUID.connectDBString = connectString;
			
			string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);
            OriginatorDeptGUID.ValueText = depData[0]; //si.fillerOrgID;
			OriginatorDeptGUID.GuidValueText = depData[3];
            //OriginatorDeptGUID.ValueText = si.submitOrgID;
            OriginatorDeptGUID.doValidate();
						
            //申請人工號. 中文名. 英文名. 職稱
            string[] userValues = base.getUserInfoById(engine, userId);
            if (userValues[0] != "")
            {
                OriginatorNumber.ValueText = userValues[0];
                OriginatorCName.ValueText = userValues[1];
                OriginatorEName.ValueText = userValues[2];
                Title.ValueText = userValues[3];
            }
            else
            {
                OriginatorNumber.ValueText = "";
                OriginatorCName.ValueText = "";
                OriginatorEName.ValueText = "";
                Title.ValueText = "";
            }

            MisDesc.ValueText = "";
			
			//MIS User
            //ds = engine.getDataSet("select '','' from EmployeeInfo  union select empGUID, empName from EmployeeInfo where deptId='GSC2200' and levelValue > '4' and (empLeaveDate is null or empLeaveDate ='') and empName not in ('ecptest6','SMP-All User')  ", "TEMP");
			ds = engine.getDataSet("select '','' from EmployeeInfo  union select empGUID, empName from EmployeeInfo where deptId in ('GSC2200','C2200') and levelValue > '4' and (empLeaveDate is null or empLeaveDate ='') and empName not in ('ecptest6','SMP-All User')  ", "TEMP");
            count = ds.Tables[0].Rows.Count;
            ids = new string[0 + count, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";

            for (int i = 1; i < count; i++)
            {
                ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
                ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
            }
            MisOwnerGUID.setListItem(ids);


            //需求類別
            ds = engine.getDataSet("select '','' from SmpInfoDemandType  union select distinct RequestType, RequestType From SmpInfoDemandType", "TEMP");
            count = ds.Tables[0].Rows.Count;
            ids = new string[0 + count, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";

            for (int i = 1; i < count; i++)
            {                
                ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
                ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
            }

            RequestType.setListItem(ids);

            //需求項目
            ds = engine.getDataSet("select top 1 '','' from SmpInfoDemandType", "TEMP");
            count = ds.Tables[0].Rows.Count;
            ids = new string[0 + count, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";

            for (int i = 1; i < count; i++)
            {
                ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
                ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
            }
            RequestItem.setListItem(ids);
            
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
       // CheckByGUID.clientEngineType = engineType;
       // CheckByGUID.connectDBString = connectString;

        //會簽人員1, 會簽人員2
        //Countersign1GUID.clientEngineType = engineType;
        //Countersign1GUID.connectDBString = connectString;
        //Countersign2GUID.clientEngineType = engineType;
        //Countersign2GUID.connectDBString = connectString;
        //MisOwnerGUID.clientEngineType = engineType;
        //MisOwnerGUID.connectDBString = connectString;
        		
		//sw.Close();\
        bool isAddNew = base.isNew();
        DataObjectSet requestSet = null;
        if (isAddNew)
        {
            requestSet = new DataObjectSet();
            requestSet.isNameLess = true;
            requestSet.setAssemblyName("WebServerProject");
            requestSet.setChildClassString("WebServerProject.form.SPIT005.SmpInfoDemandDetailForTE");
            requestSet.setTableName("SmpInfoDemandDetailForTE");
            requestSet.loadFileSchema();
            objects.setChild("SmpInfoDemandDetailForTE", requestSet);

        }
        else
        {
            requestSet = objects.getChild("SmpInfoDemandDetailForTE");
        }
        RequestList.dataSource = requestSet;
        //RequestList.HiddenField = new string[] { "GUID", "RequestType", "RequestItem", "RequestDesc", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        RequestList.HiddenField = new string[] { "GUID", "HeaderGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        RequestList.updateTable();

        if (isAddNew)
        {
            Countersign1GUID.ReadOnly = true;
            Countersign2GUID.ReadOnly = true;
            MisDesc.ReadOnly = true;
            MisOwnerGUID.ReadOnly = true;
            EstimateCompleteDate.ReadOnly = true;
            Company.ReadOnly = true;
			lbRequestTips.Text = "請先維護[需求類別]、[需求項目]、[需求說明] 後，點選[新增]按鈕。 ";
			MisDesc.ValueText = "";
			MisOwnerGUID.ValueText = "";
			EstimateCompleteDate.ValueText = "";
			Countersign1GUID.ValueText = "";
			Countersign2GUID.ValueText = "";
        }
		
		//sw.Close();

        //改變工具列順序
        base.initUI(engine, objects);
		
		//writeLog("initUI^end");
    }

    protected override void showData(AbstractEngine engine, DataObject objects)
    {	
		//writeLog("showData^start");
		
        string actName = Convert.ToString(getSession("ACTName"));
        base.showData(engine, objects);
        

        //sw.WriteLine("showData actName : " + actName);

        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);

        //表單欄位
        SheetNo.Display = false;
        Subject.Display = false;
        bool isAddNew = base.isNew();
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號, 主旨含單號
        base.showData(engine, objects);

        //申請人員資訊
        //if (!actName.Equals("審核")) {20170831 更新流程

            Company.ValueText = objects.getData("Company");
            UserDesc.ValueText = objects.getData("UserDesc");
        //}
   
		MisOwnerGUID.ValueText = "ece611bdd36c1004815bdfcbc6e1a37a";
        OriginatorNumber.ValueText = objects.getData("OriginatorNumber");
        OriginatorCName.ValueText = objects.getData("OriginatorCName");
        OriginatorEName.ValueText = objects.getData("OriginatorEName");
        Title.ValueText = objects.getData("Title");
        Extension.ValueText = objects.getData("Extension");
       
        MisDesc.ValueText = objects.getData("MisDesc");
		EstimateCompleteDate.ValueText = objects.getData("EstimateCompleteDate");		
		//MisOwnerGUID.ValueText = objects.getData("MisOwnerGUID");
		
		ExpBenefitDesc.ValueText = objects.getData("ExpBenefitDesc");
		ExpBenefitSaving.ValueText = objects.getData("ExpBenefitSaving");
		BenefitDesc.ValueText = objects.getData("BenefitDesc");
		BenefitType.ValueText = objects.getData("BenefitType");
        TESheetNo.ValueText = objects.getData("TE_SheetNo");
		if (BenefitType.ValueText.Equals("Cost"))
        {
			BenefitDesc.ValueText = "節省費用 [ "+ExpBenefitSaving.ValueText + " ] 金額(每月)";
		}
		else if (BenefitType.ValueText.Equals("Human"))
		{
			BenefitDesc.ValueText = "節省人力 [ "+ExpBenefitSaving.ValueText + " ] 小時(每月)";
		}
		else if (ExpBenefitSaving.ValueText.Equals(""))
		{
			BenefitDesc.ValueText = " ";
		}
		
		//writeLog(objects.getData("MisOwnerGUID"));

        //申請單位
        OriginatorDeptGUID.GuidValueText = objects.getData("OriginatorDeptGUID");
        OriginatorDeptGUID.doGUIDValidate();
        
        Title.ValueText = objects.getData("Title");
      
        DataObjectSet requestSet = null;
        requestSet = objects.getChild("SmpInfoDemandDetailForTE");
        RequestList.dataSource = requestSet;
        RequestList.updateTable();
        
		if (isAddNew)
        {
            Countersign1GUID.ReadOnly = true;
            Countersign2GUID.ReadOnly = true;
            MisDesc.ReadOnly = true;
            MisOwnerGUID.ReadOnly = true;
            EstimateCompleteDate.ReadOnly = true;
            Company.ReadOnly = true;
			lbRequestTips.Text = "請先維護[需求類別]、[需求項目]、[需求說明] 後，點選[新增]按鈕。 ";
			MisDesc.ValueText = "";
			MisOwnerGUID.ValueText = "";
			EstimateCompleteDate.ValueText = "";
			//Countersign1GUID.ValueText = "";
			//Countersign2GUID.ValueText = "";
			ExpBenefitDesc.ValueText = "";
			ExpBenefitSaving.ValueText = "";
			BenefitDesc.ValueText = "";
        }
		
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
            //CheckByGUID.ReadOnly = true;
            UserDesc.ReadOnly = true;
            MisDesc.ReadOnly = true;
            //Countersign1GUID.ReadOnly = true;
            //Countersign2GUID.ReadOnly = true;
            RequestType.ReadOnly = true;
            RequestItem.ReadOnly = true;
            RequestDesc.ReadOnly = true;
            MisOwnerGUID.ReadOnly = true;
            EstimateCompleteDate.ReadOnly = true;
			RequestList.NoAdd = true;                                                                                                                                                     
			RequestList.NoDelete = true;                                                                                                                                                  
			RequestList.NoModify = true;
            TESheetNo.ReadOnly = true;
			ExpBenefitDesc.ReadOnly = true;
			ExpBenefitSaving.ReadOnly = true;
			BenefitDesc.ReadOnly = true;
			BenefitType.ReadOnly = true;
			if (BenefitType.ValueText.Equals(""))
            {
                BenefitType.ValueText = "人力";
				LBBenefitHours.Text = "小時(每月)";
            }else if (BenefitType.ValueText.Equals("Cost")){
				BenefitType.ValueText = "費用";
				LBBenefitHours.Text = "金額(每月)";
			}
        }
        //if (!actName.Equals("審核"))20170831 更新流程
        //{
            for (int i = 0; i < RequestList.dataSource.getAvailableDataObjectCount(); i++)
            {
                RequestList.dataSource.getAvailableDataObject(i).setData("SmpInfoDemandDetailForTE", objects.getData("GUID"));
            }
        //}
        if (actName.Equals("MIS窗口")) {
            SignFlowTE.Display = true;
            //RequestType.ReadOnly = false;
            //RequestItem.ReadOnly = false;
            //RequestDesc.ReadOnly = false;
			if (EstimateCompleteDate.ValueText.Equals(""))
			{
				//預計完成日自動加7天
				DateTime dt = DateTime.Now.AddDays(7) ;
				EstimateCompleteDate.ValueText = dt.ToShortDateString().ToString();
				//EstimateCompleteDate.ValueText = DateTimeUtility.getSystemTime2(null).Substring(0, 10);
			}
            MisDesc.ReadOnly = false;
            AddSignButton.Display = true; //允許加簽, 
			EstimateCompleteDate.ReadOnly = false;
			
			ExpBenefitDesc.ReadOnly = false;
			ExpBenefitSaving.ReadOnly = false;
			BenefitDesc.ReadOnly = false;
			BenefitType.ReadOnly = true;
        }

        if (actName.Equals("MIS主管")) {
            Countersign1GUID.ReadOnly = false;
            Countersign2GUID.ReadOnly = false;
            MisOwnerGUID.ReadOnly = false;
        }

        if (actName.Equals("MIS承辦人員")) {            
            MisDesc.ReadOnly = false;
			AddSignButton.Display = true; //允許加簽,
        }

        //writeLog("showData^End");
    }
        
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
		//writeLog("saveData^start");

       
        try
        {
            //System.IO.StreamWriter sw = null;
            //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true, System.Text.Encoding.Default);
			//sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
            //sw.WriteLine("saveData start!!");
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");            
            bool isAddNew = base.isNew(); //base 父類別
			
			string[] values1 = getDeptGuidByOrg(engine, Company.ValueText, OriginatorDeptGUID.ValueText);
			
			//writeLog("365 OriginatorDeptGUID :" + OriginatorDeptGUID.ValueText + "  - "+ Company.ValueText + " - "+values1[0]);

            if (isAddNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("Subject", Subject.ValueText);
                objects.setData("Company", Company.ValueText);
				string[] values = getDeptGuidByOrg(engine, Company.ValueText, OriginatorDeptGUID.ValueText);
                objects.setData("OriginatorDeptGUID", values[0]);
                //objects.setData("OriginatorDeptGUID", OriginatorDeptGUID.GuidValueText);
                objects.setData("Title", Title.ValueText);
                objects.setData("OriginatorNumber", OriginatorNumber.ValueText);
                objects.setData("OriginatorCName", OriginatorCName.ValueText);
                objects.setData("OriginatorEName", OriginatorEName.ValueText);
                objects.setData("Extension", Extension.ValueText);
				//objects.setData("CheckByGUID", CheckByGUID.GuidValueText);
                objects.setData("UserDesc", UserDesc.ValueText);
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("DATA_STATUS", "Y");
                //主旨含單號
                base.saveData(engine, objects);
                //sw.WriteLine("主旨含單號");
            }
			//sw.WriteLine("saveData Line217==> " + DeliveryDate.ValueText);
            objects.setData("EstimateCompleteDate", EstimateCompleteDate.ValueText);
            objects.setData("MisDesc", MisDesc.ValueText);
            //objects.setData("Countersign1GUID", Countersign1GUID.GuidValueText);
            //objects.setData("Countersign2GUID", Countersign2GUID.GuidValueText);
			objects.setData("MisOwnerGUID", MisOwnerGUID.ValueText);
			//sw.WriteLine("EstimateCompleteDate==> " + EstimateCompleteDate.ValueText);
			//sw.WriteLine("MisDesc==> " + MisDesc.ValueText);
			
			objects.setData("ExpBenefitDesc", ExpBenefitDesc.ValueText);
			objects.setData("ExpBenefitSaving", ExpBenefitSaving.ValueText);
			objects.setData("BenefitDesc", BenefitDesc.ValueText);
			objects.setData("BenefitType", BenefitType.ValueText);
            string actName = Convert.ToString(getSession("ACTName"));
            //if (!actName.Equals("審核"))20170831 更新流程
            //{
                for (int i = 0; i < RequestList.dataSource.getAvailableDataObjectCount(); i++)
                {
                    RequestList.dataSource.getAvailableDataObject(i).setData("HeaderGUID", objects.getData("GUID"));
                }
            //}
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
			if (UserDesc.ValueText.Equals("工作需求")) { strErrMsg += "請具體說明需求原因，如:什麼樣的作業會需要此一功能，勿以”工作需求”含糊帶過\n"; } 
            
			//檢查 明細清單 資料
            DataObjectSet chkType = RequestList.dataSource;
            if (chkType.getAvailableDataObjectCount().Equals(0))
            {
                strErrMsg += "請輸入資訊需求內容清單(需求類別、需求項目、需求說明)!\n";
            }
			
			//預期效益必須為數值
			decimal intBenefitSaving = 0;
            bool isDecimal = false;
            if (!ExpBenefitSaving.ValueText.Equals(""))
            {
                isDecimal = decimal.TryParse(ExpBenefitSaving.ValueText, out intBenefitSaving);
                if (!isDecimal)
                {
                    strErrMsg += "預期效益須為數值" + "\n";
                }
            }
			if (RequestType.ValueText.Equals("3.應用系統-程式報表修改") || RequestType.ValueText.Equals("4.應用系統-系統設定與維護"))
			{
				if (ExpBenefitDesc.ValueText.Equals("") || ExpBenefitSaving.ValueText.Equals(""))
				{
					strErrMsg += "必須填寫預期效益/節省內容";
				}				
			}
			
            //設定主旨
			strTemp = RequestDesc.ValueText;
			if (strTemp.Length > 20)
            {
                strTemp = strTemp.Substring(0, 20);
            }

            if (Subject.ValueText.Equals(""))
            {
                //values = base.getUserInfo(engine, RequestList.ValueText);
                string subject = "【" + OriginatorCName.ValueText + " ，類別：" + RequestType.ValueText + "-"+ RequestItem.ValueText +"，" + strTemp + " 】";
                if (Subject.ValueText.Equals(""))
                {
                    Subject.ValueText = subject;
                }
            }
        
        }
        if (actName.Equals("MIS窗口"))
        {
            if (EstimateCompleteDate.ValueText.Equals("")) { strErrMsg += "預計完成日不可為空值\n"; }
        }
        if (actName.Equals("MIS主管"))
        {
            if (MisOwnerGUID.ValueText.Equals("")) { strErrMsg += "請指MIS承辦人員\n"; }
        }

        if (actName.Equals("MIS承辦人員"))
        {
            if (MisDesc.ValueText.Equals("")) { strErrMsg += "MIS處理說明不可為空值\n"; }
        }
        
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
            //sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
            //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true);
            //sw.WriteLine("setFlowVariables");
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
            string[] orgUnitInfo = base.getOrgUnitInfo(engine, OriginatorDeptGUID.GuidValueText);
			if (!orgUnitInfo[3].Equals(""))
            {
                orgUnitManagerId = orgUnitInfo[3];
            }
			
			//20130911修改, 當填單人=部門主管時, 則簽該人員直屬主管
			/*if (orgUnitManagerId.Equals(creatorId))
            {
                orgUnitManagerId = managerId;
            }*/
            //sw.WriteLine("填表人員的主管=" + managerId);

            //審核人員
            //string checkByGUID = CheckByGUID.GuidValueText;
            //string checkById = "";
           // if (!checkByGUID.Equals(""))
            //{
           //     values = base.getUserInfo(engine, checkByGUID);
           //     checkById = values[0];
           // }
            //sw.WriteLine("checkById=" + checkById);

            //會簽人員１，會簽人員２            
            /*string countersign1GUID = Countersign1GUID.GuidValueText;
            string countersign1Id = "";
            if (!countersign1GUID.Equals(""))
            {
                values = base.getUserInfo(engine, countersign1GUID);
                countersign1Id = values[0];
            }
            string countersign2GUID = Countersign2GUID.GuidValueText;
            string countersign2Id = "";
            if (!countersign2GUID.Equals(""))
            {
                values = base.getUserInfo(engine, countersign2GUID);
                countersign2Id = values[0];
            }
            */
            //MIS處理人員
            //string misOwnerGUID = MisOwnerGUID.GuidValueText;
			string misOwnerGUID = MisOwnerGUID.ValueText;
            string misOwnerId = "";	
            if (!misOwnerGUID.Equals(""))
            {
                values = base.getUserInfo(engine, misOwnerGUID);
                misOwnerId = values[0];
            }

            string misWindow = "";

            misWindow = GetMISWindow(engine, currentObject);
			//UpdateDataFromTEInfoDemand(engine, currentObject);//20170831 更新流程
			//MIS窗口
            /*string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");

            string sql = "select RequestType,RequestItem,RequestDesc from TESTSMPECPAP2K.WebFormPT.dbo.SmpInfoDemandDetail where HeaderGUID = (select TE_GUID from SmpInfoDemandForTE where SheetNo = '" + sheetNo + "')";
            string RequestType1 = "";
            string RequestItem1 = "";
            string RequestDesc1 = "";
            DataSet ds = null;
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                RequestType1 = ds.Tables[0].Rows[0]["RequestType"].ToString();
                RequestItem1 = ds.Tables[0].Rows[0]["RequestItem"].ToString();
                RequestDesc1 = ds.Tables[0].Rows[0]["RequestDesc"].ToString();
            }

			
			DataObjectSet set = currentObject.getChild("SmpInfoDemandDetailForTE");
	        //for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
	        //{
	        //    string requestType = set.getAvailableDataObject(i).getData("RequestType");
            //    string requestItem = set.getAvailableDataObject(i).getData("RequestItem");

            if (RequestType1.Equals("1.辦公作業系統需求"))
	            {
	                misWindow = "4188";
	            }
            if (RequestItem1.Equals("鼎新ERP"))
                {
                    misWindow = "2556";
                }
            if (RequestItem1.Equals("MES"))
                {
                    misWindow = "3842";
                }
            if (RequestType1.Equals("2.應用系統-帳號申請/權限異動") && RequestItem1.Equals("SRM(供應商)"))
                {
                    misWindow = "3625";
                }
            if (RequestType1.Equals("5.新人軟硬體需求"))
	            {
	                misWindow = "4188;3787";
	            }
            */
          /*  try{
                sql = "Update TESTSMPECPAP2K.WebFormPT.dbo.SmpInfoDemand set TEMISWindow = '" + misWindow + "' where SheetNo = (select TE_SheetNo from WebFormPT.dbo.SmpInfoDemandForTE where SheetNo = '" + sheetNo + "')";
                writeLog("Update TEInfoDemandMISWindow SQL : " + sql);
                engine.executeSQL(sql);
                //engine.commit();
            }catch (Exception e)
            {
                writeLog("---------------------------" + e);
            }*/
            //}
			//sw.WriteLine("misWindow=" + misWindow);

            xml += "<SPIT005>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
           // xml += "<originator DataType=\"java.lang.String\">" + creatorId + "</originator>";
           // xml += "<checkby1 DataType=\"java.lang.String\">" + checkById + "</checkby1>";
           // xml += "<checkby2 DataType=\"java.lang.String\"></checkby2>";
          //  xml += "<manager DataType=\"java.lang.String\">" + orgUnitManagerId + "</manager>";
            xml += "<miswindow DataType=\"java.lang.String\">" + misWindow + "</miswindow>";
            xml += "<mismgr DataType=\"java.lang.String\">SMP-MISMGR</mismgr>";
            xml += "<misowner DataType=\"java.lang.String\">" + misOwnerId + "</misowner>";
            //xml += "<countersign1 DataType=\"java.lang.String\">" + countersign1Id + "</countersign1>";
           // xml += "<countersign2 DataType=\"java.lang.String\">" + countersign2Id + "</countersign2>";
            //xml += "<notify1 DataType=\"java.lang.String\"></notify1>";
            xml += "</SPIT005>";
            //sw.WriteLine("xml: " + xml);
			writeLog("xml : " + xml);
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
        param["SPIT005"] = xml;
		
		//writeLog("setFlowVariables^end");
		
        return "SPIT005";
    }

    protected void OriginatorGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                
        //更新申請人資料
        string[] userValues = base.getUserInfoById(engine, OriginatorNumber.ValueText);
        if (userValues[0] != "")
        {
            //OriginatorNumber.ValueText = userValues[0];
            OriginatorCName.ValueText = userValues[1];
            OriginatorEName.ValueText = userValues[2];
            Title.ValueText = userValues[3];
        }
        else
        {
            //OriginatorNumber.ValueText = "";
            OriginatorCName.ValueText = "";
            OriginatorEName.ValueText = "";
            Title.ValueText = "";
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

        return base.beforeSign(engine, isAfter, addSignXml);
    }

    /// <summary>
    /// 簽核後程序
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
		writeLog("In afterSign--------------------------------------- ");
		string signProcess = Convert.ToString(Session["signProcess"]);
		writeLog("signProcess : " + signProcess);
		writeLog("result : " + result);
		string backOpinion = (string)Session["tempBackOpinion"];
		writeLog("backOpinion : " + backOpinion);
		//writeLog("afterSign^start");
		string SignedCommentAll ="";
		string serialNumber ="";
		string UserId ="";
		bool callWS = true;
		string CommentAndCall = getCommentAndCall(engine,currentObject,"");
		writeLog("CommentAndCall : " + CommentAndCall);
		Char delimiter = ';';
        string[] CAC = CommentAndCall.Split(delimiter);
		SignedCommentAll = CAC[0];
		string callBoolean = CAC[1];
		serialNumber = getTESerialNumber(engine,currentObject);
		if(!callBoolean.Equals("True")){
			callWS =false;
		}
		if(callWS){
			UserId = getTEUserId(engine,currentObject,"");
			UpdateTEMisDesc(engine,currentObject);
			if (result.Equals("N")) //不同意
			{
			//	writeLog("Call  WSrejectProcedure  " );
			//	WSrejectProcedure(engine,UserId,serialNumber,"",SignedCommentAll,"");
			}else{
			//	writeLog("Call  CallSignFlowWebService  " );
				CallSignFlowWebService(engine,currentObject,UserId,serialNumber,SignedCommentAll,"Y");
			}
		}
        base.afterSign(engine, currentObject, result);
		//writeLog("afterSign^end");
		writeLog("End Of afterSign--------------------------------------- ");
    }
	
	protected string getCommentAndCall(AbstractEngine engine, DataObject currentObject,string actNames)
	{
		string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
		string actName = Convert.ToString(getSession("ACTName"));
		if(actName.Equals("")){
			actName = actNames;
		}
		string WorkItemCreatedTime = "";
		string WorkItemCompletedTime = "";
		string SignedComment ="";
		string SignedCommentAll ="";
		bool callWS = true;
		string sql = "select createdTime, completedTime, executiveComment from  NaNa.dbo.WorkItem " +
					 " where contextOID=(select contextOID from NaNa.dbo.ProcessInstance " +
					 " where serialNumber=(select SMWYAAA005 from SMWYAAA where  SMWYAAA002='"+sheetNo+"')) " +
				     " and workItemName='"+actName+"' ";
		DataSet ds = engine.getDataSet(sql, "TEMP");
		writeLog("WorkItemCreatedTime Related sql  : " + sql);
		writeLog("ds.Tables[0].Rows.Count  : " + ds.Tables[0].Rows.Count);
		
        if (ds.Tables[0].Rows.Count > 0)
        {
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				WorkItemCreatedTime = ds.Tables[0].Rows[i]["createdTime"].ToString();
				WorkItemCompletedTime = ds.Tables[0].Rows[i]["completedTime"].ToString();
				SignedComment = ds.Tables[0].Rows[i]["executiveComment"].ToString();
				writeLog("WorkItemCreatedTime  : " + WorkItemCreatedTime);
				writeLog("WorkItemCompletedTime  : " + WorkItemCompletedTime);
				SignedComment = SignedComment.Replace("同意##", "");
				SignedComment = SignedComment.Replace("退回重辦##", "");
				writeLog("SignedComment  : " + SignedComment);
				if(WorkItemCompletedTime.Equals("")){
					callWS = false;
				}else{
					SignedCommentAll = SignedCommentAll + SignedComment + ". ";
				}	
			}
        }
		
		return SignedCommentAll + ";" + callWS;
		writeLog("SignedCommentAll  ;  callWS : " + SignedCommentAll + ";" + callWS);
	}
	
	protected string getTESerialNumber(AbstractEngine engine, DataObject currentObject)
	{
		string serialNumber ="";
		AbstractEngine hrengine = null;
		WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
		string hrconStr = sp.getParam("WebFormPTDB");
		IOFactory factory = new IOFactory();
		hrengine = factory.getEngine(EngineConstants.SQL, hrconStr);
		hrengine.startTransaction(IsolationLevel.ReadCommitted);
		writeLog("hrengine : " + hrengine);
		
		string sql = "select SMWYAAA005 from SMWYAAA where  SMWYAAA002='"+currentObject.getData("TE_SheetNo")+"'";
		writeLog("sql  : " + sql);
		DataSet ds = hrengine.getDataSet(sql, "TEMP");
		if (ds.Tables[0].Rows.Count > 0)
		{
			serialNumber = ds.Tables[0].Rows[0]["SMWYAAA005"].ToString();
		}
		writeLog("serialNumber  : " + serialNumber);
		return serialNumber;
	}
	
	protected string getTEUserId(AbstractEngine engine, DataObject currentObject,string actNames)
	{
		string UserId ="";
		string actName = Convert.ToString(getSession("ACTName"));
		if(actName.Equals("")){
			actName = actNames;
		}
		AbstractEngine hrengine = null;
		WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
		string hrconStr = sp.getParam("WebFormPTDB");
		IOFactory factory = new IOFactory();
		hrengine = factory.getEngine(EngineConstants.SQL, hrconStr);
		hrengine.startTransaction(IsolationLevel.ReadCommitted);
		writeLog("hrengine : " + hrengine);
		
		string sql = "select id from Users where OID = (select assigneeOID from WorkAssignment where workItemOID ="
				+ "(select OID from  NaNa.dbo.WorkItem where contextOID= (select contextOID from NaNa.dbo.ProcessInstance where serialNumber="
				+ "(select SMWYAAA005 from SMWYAAA where  SMWYAAA002='"+currentObject.getData("TE_SheetNo")+"')) and completedTime is null and workItemName='"+actName+"' ))";
			writeLog("Get UserID Sql  : " + sql);	
			DataSet ds = hrengine.getDataSet(sql, "TEMP");
			if (ds.Tables[0].Rows.Count > 0)
			{
				UserId = ds.Tables[0].Rows[0]["id"].ToString();
			}		
		writeLog("UserId  : " + UserId);
		return UserId;
	}
	
	
	
	

	protected void CallSignFlowWebService(AbstractEngine engine, DataObject currentObject,string UserId,string serialNumber,string comment,string signResult) {
		
		string result ="";		
		writeLog("SignFlow  : signFlow start ");
		result = signFlow(engine,UserId,serialNumber,signResult,comment);
		writeLog("SignFlow  : signFlow end ");
		writeLog("result  : " + result);
	}
	
    /// <summary>
    /// 重辦程序
    /// </summary>    
    protected override void rejectProcedure()
    {
		//退回填表人終止流程
		string actName = Convert.ToString(getSession("ACTName"));
        string backActID = Convert.ToString(Session["tempBackActID"]); //退回後關卡ID
		string backActIDReal = backActID;
		string backOpinion = (string)Session["tempBackOpinion"];
		if(backActID.IndexOf("-") != -1){
			backActIDReal = backActID.Substring(0,backActID.IndexOf("-"));
		}
		
		
		writeLog("actName : " + actName + " backActID : " + backActIDReal + " backOpinion : " + backOpinion);
		IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
		DataObject currentObject = (DataObject)getSession("currentObject");
		string UserId = getTEUserId(engine,currentObject,"");
		string serialNumber = getTESerialNumber(engine,currentObject);
		if(backActIDReal.ToUpper().Equals("CREATOR")){
			WSrejectProcedure(engine,UserId,serialNumber,"",backOpinion,"");
		}else{
			WSrejectProcedure(engine,UserId,serialNumber,backActIDReal,backOpinion,"");
		}
		
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


    protected void RequestList_ShowRowData(DataObject objects)
    {
        System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true);
        //sw.WriteLine("RequestList_ShowRowData");
        
        RequestType.ValueText = objects.getData("RequestType");
        RequestItem.ValueText = objects.getData("RequestItem");
        RequestDesc.ValueText = objects.getData("RequestDesc");
        //sw.WriteLine("RequestType.ValueText" + RequestType.ValueText);
        //sw.WriteLine("RequestItem.ValueText" + RequestItem.ValueText);
        //sw.Close();
    }

    protected bool RequestList_SaveRowData(DataObject objects, bool isNew)
    {
        System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true);
		//sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
        //sw.WriteLine("RequestList_SaveRowData");
		
		//string tmpReqType = "";
        DataObjectSet chkType = RequestList.dataSource;
        for (int i = 0; i < chkType.getAvailableDataObjectCount(); i++)
        {
            string requestType = chkType.getAvailableDataObject(i).getData("RequestType");
            if (!RequestType.ValueText.Equals(requestType))
            {
                MessageBox("不同需求類別，請分開填單");
                return false;
            }

        }
        
        if (RequestType.ValueText.Equals(""))
        {
            MessageBox("必須填寫需求類別");
            return false;
        }
        if (RequestItem.ValueText.Equals(""))
        {
            MessageBox("必須填寫需求項目");
            return false;
        }		
		if (RequestType.ValueText.Equals("3.應用系統-程式報表修改") || RequestType.ValueText.Equals("4.應用系統-系統設定與維護"))
        {
			if (ExpBenefitDesc.ValueText.Equals("") || ExpBenefitSaving.ValueText.Equals(""))
			{
				MessageBox("必須填寫預期效益/節省內容");
				//return false;
			}
			//預期效益必須為數值
			decimal intBenefitSaving = 0;
            bool isDecimal = false;
            if (!ExpBenefitSaving.ValueText.Equals(""))
            {
                isDecimal = decimal.TryParse(ExpBenefitSaving.ValueText, out intBenefitSaving);
                if (!isDecimal)
                {
					MessageBox("預期效益須為數值");
					//return false;
                }
            }
        }
		
		//if (RequestType.ValueText.Equals("tmpReqType"))
		//{
		//	MessageBox("需求類別只能維護一項");
        //    return false;
		//}
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("HeaderGUID", "TEMP");
			objects.setData("RequestType", RequestType.ValueText);
	        objects.setData("RequestItem", RequestItem.ValueText);
	        objects.setData("RequestDesc", RequestDesc.ValueText);			
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
			//tmpReqType = RequestType.ValueText;
        }

        //sw.WriteLine("Request Type OBJECT>> " + objects.getData("RequestType"));
        //sw.WriteLine("Request Type DIRGETDATA>> " + RequestType.ValueText);		
		//sw.WriteLine("tmpReqType" + tmpReqType);
		
        //sw.Close();
        return true;
    }
    protected void RequestType_SelectChanged(string value)
    {
        System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true);
       // sw.WriteLine("RequestType_SelectChanged value => " + value);

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

        string[,] ids = null;
        string sql = null;
        DataSet ds = null;
        int count = 0;

        string strRequestType = RequestType.ValueText;
       // MessageBox("hh" + strRequestType);
        if (!strRequestType.Equals(""))
        {
            sql = "select distinct RequestItem, RequestItem From SmpInfoDemandType where RequestType='" + value + "'";
            //sw.WriteLine("RequestType_SelectChanged SQL => " + sql);
            ds = engine.getDataSet(sql, "TEMP");
            count = ds.Tables[0].Rows.Count;
            ids = new string[count, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";

            for (int i = 0; i < count; i++)
            {                
                ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
                ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
                //清除說明
                lbRequestTips.Text = "";
            }
            RequestItem.setListItem(ids);            
        }
        else
        {            
            ids[0, 0] = "";
            ids[0, 1] = "";
            RequestItem.setListItem(ids);
        }

        

        if (sw != null) sw.Close();
    }

    protected void RequestItem_SelectChanged(string value)
    {
        System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true);
        //sw.WriteLine("RequestItem_SelectChanged value => " + value);

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

        //string[,] ids = null;
        string sql = null;
        DataSet ds = null;
        int count = 0;

        string strRequestItem = RequestItem.ValueText;
        string strRequestType = RequestType.ValueText;
        // MessageBox("hh" + strRequestType);
        if (!strRequestItem.Equals(""))
        {
            sql = "select distinct Description From SmpInfoDemandType where RequestItem='" + value + "' and RequestType='" + strRequestType + "' ";
            //sw.WriteLine("Tips SQL => " + sql);
            ds = engine.getDataSet(sql, "TEMP");
            count = ds.Tables[0].Rows.Count;

            if (count > 0)
            {
                lbRequestTips.Text = ds.Tables[0].Rows[0][0].ToString();
                sw.WriteLine("lbRequestTips => " + lbRequestTips.Text);
            }
            else
            {
                lbRequestTips.Display = false;
            }
            //RequestItem.setListItem(ids);
        }
        //else
        //{
        //    lbRequestTips.Enabled = false;
        //}

        if (sw != null) sw.Close();
    }

    protected void OriginatorNumber_SingleFieldButtonClick(object sender, EventArgs e)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

        //更新基本資料
        string[] userValues = base.getUserInfoById(engine, OriginatorNumber.ValueText);
        if (userValues[0] != "")
        {
            OriginatorNumber.ValueText = userValues[0];  //申請人工號
            OriginatorCName.ValueText = userValues[1];   //中文名
            OriginatorEName.ValueText = userValues[2];   //英文名
            Title.ValueText = userValues[3];              // 職稱
						
			//公司別
            string value = (string)engine.executeScalar("select orgId from EmployeeInfo where empNumber='" + userValues[0] + "'");
			string orgId = "SMP";

            if (value != null)
            {
                orgId = value;
            }
            Company.ValueText = orgId;	
			
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
	
	protected void misUser_SelectChanged(string value)
    {
        string strMisOwner = MisOwnerGUID.ValueText;
		//IsIncludeDateEve.ValueText = getIsIncludeDateEve();
		string[,] ids = null;
        //string sql = null;
        DataSet ds = null;
        int count = 0;
		
		if (strMisOwner.Equals(""))

        {
        //    sw.WriteLine("no data!");
            ids[0, 0] = "";
            ids[0, 1] = "";
            MisOwnerGUID.setListItem(ids);
        }
    }

    protected void Countersign1GUID_BeforeClickButton()
    {
        Countersign1GUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";        
    }
    protected void Countersign2GUID_BeforeClickButton()
    {
        Countersign2GUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";        
    }
	
	private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPIT005.log", true, System.Text.Encoding.Default);
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

	protected string[] getDeptGuidByOrg(AbstractEngine engine, string companyId, string deptId)
    {
        //string sql = "select u.id, u.organizationUnitName, isMain, u.OID from Functions inner join OrganizationUnit u on organizationUnitOID=u.OID where occupantOID='" + Utility.filter(userGUID) + "' and isMain='1'";
        string sql = "select ou.OID from OrganizationUnit ou, Organization o where ou.organizationOID=o.OID and o.id='" + companyId + "' and ou.id='" + deptId + "' ";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[1];
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();            
        }
        else
        {
            result[0] = "";            
        }
        return result;
    }	
	
	/// <summary>
    /// 效益類別變更
    /// </summary>
    /// <param name="value"></param>
    protected void BenefitType_SelectChanged(string value)
    {
        //提示
        string strMessage = "";
        string strType = BenefitType.ValueText;
		
		if (strType.Equals("Cost"))
        {
            LBBenefitHours.Text = "金額(每月)";			
        }else{
		    LBBenefitHours.Text = "小時(每月)";			
		}
		
    }

    protected void CopyDataToDCCSCANNER(AbstractEngine engine, DataObject currentObject) {
        string IDFileName = "";
        string RealFileName = "";
        string FileExt = "";
        string FileDesc = "";

        
        string userId = (string)Session["UserID"];
        string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
        //engine.startTransaction(IsolationLevel.ReadCommitted);
        //Insert FileItem Data
        //string sourcePath = "\\\\\\\\192.168.2.228\\\\d$\\\\ECP\\\\WebFormPT\\\\FileStorage\\\\SPIT001\\\\" + objects.getData("TE_SheetNo") + "\\\\";
        string sourcePath = "\\\\\\\\DCC_SCANNER\\\\All User\\\\Eric\\\\" + currentObject.getData("TE_SheetNo") + "\\\\";
        string targetPath = "D:\\\\ECP\\\\WebFormPT\\\\FileStorage\\\\SPIT005\\\\" + sheetNo + "\\\\";
        string sql = " select GUID,FILENAME,FILEEXT,DESCRIPTION from TESTSMPECPAP2K.WebFormPT.dbo.FILEITEM where LEVEL2 = '" + currentObject.getData("TE_SheetNo") + "'";
        string now = DateTimeUtility.getSystemTime2(null);
        DataSet ds2 = null;
        ds2 = engine.getDataSet(sql, "TEMP");
        if (ds2.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++) {
                string FileGUID = IDProcessor.getID("");
                IDFileName = ds2.Tables[0].Rows[i]["GUID"].ToString();
                RealFileName = ds2.Tables[0].Rows[i]["FILENAME"].ToString();
                FileExt = ds2.Tables[0].Rows[i]["FILEEXT"].ToString();
                FileDesc = ds2.Tables[0].Rows[i]["DESCRIPTION"].ToString();
                string sourceFile = System.IO.Path.Combine(sourcePath, IDFileName);
                string destFile = System.IO.Path.Combine(targetPath, FileGUID);
                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }
                writeLog("sourceFile : " + sourceFile);
                writeLog("destFile : " + destFile);
                System.IO.File.Copy(sourceFile, destFile, true);

                sql = "Insert into WebFormPT.dbo.FILEITEM values ('" + FileGUID + "','" + currentObject.getData("GUID") + "','";
                sql += FileGUID + "','" + RealFileName + "','" + FileExt + "','','" + FileDesc + "','SPIT005','";
                sql += sheetNo + "','" + userId + "','" + now + "','','','','')";
                writeLog("Insert sql : " + sql);
                engine.executeSQL(sql);
            }
            
        }

        //engine.commit();

        
    }


    protected string GetMISWindow(AbstractEngine engine, DataObject currentObject) {
        string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");

        string sql = "select RequestType,RequestItem,RequestDesc from TESTSMPECPAP2K.WebFormPT.dbo.SmpInfoDemandDetail where HeaderGUID = (select TE_GUID from SmpInfoDemandForTE where SheetNo = '" + sheetNo + "')";
        string RequestType1 = "";
        string RequestItem1 = "";
        string RequestDesc1 = "";
        string misWindow = "3787";
        DataSet ds = null;
        ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            RequestType1 = ds.Tables[0].Rows[0]["RequestType"].ToString();
            RequestItem1 = ds.Tables[0].Rows[0]["RequestItem"].ToString();
            RequestDesc1 = ds.Tables[0].Rows[0]["RequestDesc"].ToString();
        }


        DataObjectSet set = currentObject.getChild("SmpInfoDemandDetailForTE");
        //for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
        //{
        //    string requestType = set.getAvailableDataObject(i).getData("RequestType");
        //    string requestItem = set.getAvailableDataObject(i).getData("RequestItem");

        if (RequestType1.Equals("1.辦公作業系統需求"))
        {
            misWindow = "4188";
        }
        if (RequestItem1.Equals("鼎新ERP"))
        {
            misWindow = "2556";
        }
        if (RequestItem1.Equals("MES"))
        {
            misWindow = "3842";
        }
        if (RequestType1.Equals("2.應用系統-帳號申請/權限異動") && RequestItem1.Equals("SRM(供應商)"))
        {
            misWindow = "3625";
        }
        if (RequestType1.Equals("5.新人軟硬體需求"))
        {
            misWindow = "4188;3787";
        }

        return misWindow;
    
    }
	
	

    protected string signFlow(AbstractEngine engine, string userId, string serialNumber, string signProcess, string executiveComment)
    {
        string result = null;
        try
        {
            WebServerProject.SysParam param = new WebServerProject.SysParam(engine);
            string ws = param.getParam("FlowService228");  //SmpCreateTEInfoDemand       		
            com.dsc.kernal.webservice.WSDLClient service = new com.dsc.kernal.webservice.WSDLClient(ws);
            service.dllPath = com.dsc.kernal.utility.Utility.G_GetTempPath();
            service.build(true);
            result = Convert.ToString(service.callSync("signFlow", userId, serialNumber, signProcess, executiveComment));
					
        }
        catch (Exception e)
        {
			writeLog("WS signFlow error  : " + e);
            throw new Exception(e.StackTrace);
        }
        return result;
    }
	

    protected string WSrejectProcedure(AbstractEngine engine, string userId, string serialNumber, string backActID, string executiveComment, string reexecuteActivityType)
    {
		writeLog("WS rejectProcedure userId  : " + userId);
		writeLog("WS rejectProcedure serialNumber  : " + serialNumber);
		writeLog("WS rejectProcedure executiveComment  : " + executiveComment);
        string result = null;
        try
        {
            WebServerProject.SysParam param = new WebServerProject.SysParam(engine);
            string ws = param.getParam("FlowService228");  //SmpCreateTEInfoDemand       		
            com.dsc.kernal.webservice.WSDLClient service = new com.dsc.kernal.webservice.WSDLClient(ws);
            service.dllPath = com.dsc.kernal.utility.Utility.G_GetTempPath();
            service.build(true);
            result = Convert.ToString(service.callSync("rejectProcedure", userId, serialNumber, backActID, executiveComment, "0"));
			writeLog("WS rejectProcedure result  : " + result);
        }
        catch (Exception e)
        {
			writeLog("WS rejectProcedure error  : " + e);
            throw new Exception(e.StackTrace);
        }
        return result;
    }
	
	protected void UpdateTEMisDesc(AbstractEngine engine, DataObject currentObject)
	{
		AbstractEngine hrengine = null;
		WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
		string hrconStr = sp.getParam("WebFormPTDB");
		IOFactory factory = new IOFactory();
		hrengine = factory.getEngine(EngineConstants.SQL, hrconStr);
		hrengine.startTransaction(IsolationLevel.ReadCommitted);
		writeLog("hrengine : " + hrengine);
		
		string sql = "Update SmpInfoDemand set MisDesc ='"+currentObject.getData("MisDesc")+"' where SheetNo = '"+currentObject.getData("TE_SheetNo")+"'";
		writeLog("sql  : " + sql);
		hrengine.executeSQL(sql);
		hrengine.commit();
		
	}
	
	 protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        writeLog("In afterApprove--------------------------------------- ");
		string[] values = base.getNowSignActName(engine, currentObject.getData("GUID"));
        string actName = values[1];
		string signProcess = Convert.ToString(Session["signProcess"]);
		writeLog("result : " + result);
		string backOpinion = (string)Session["tempBackOpinion"];
		writeLog("backOpinion : " + backOpinion);
		//writeLog("afterSign^start");
		string SignedCommentAll ="";
		string serialNumber ="";
		string UserId ="";
		bool callWS = true;
		string CommentAndCall = getCommentAndCall(engine,currentObject,actName);
		writeLog("CommentAndCall : " + CommentAndCall);
		Char delimiter = ';';
        string[] CAC = CommentAndCall.Split(delimiter);
		SignedCommentAll = CAC[0];
		string callBoolean = CAC[1];
		serialNumber = getTESerialNumber(engine,currentObject);
		if(!callBoolean.Equals("True")){
			callWS =false;
		}
		if(callWS){
			UserId = getTEUserId(engine,currentObject,actName);
			UpdateTEMisDesc(engine,currentObject);
			if (result.Equals("N")) //不同意
			{
			//	writeLog("Call  WSrejectProcedure  " );
			//	WSrejectProcedure(engine,UserId,serialNumber,"",SignedCommentAll,"");
			}else{
			//	writeLog("Call  CallSignFlowWebService  " );
				CallSignFlowWebService(engine,currentObject,UserId,serialNumber,SignedCommentAll,"Y");
			}
		}
		//writeLog("afterSign^end");
		writeLog("End Of afterApprove--------------------------------------- ");
    }
	
	//------------------
	//------------------ HR 出勤修正單 Web Service 測試
	//------------------
	protected void SignFlow_OnClick(object sender, EventArgs e)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
	    string result = null;
        try
        {
			//init ECP Web Service 
            WebServerProject.SysParam param = new WebServerProject.SysParam(engine);
            string ws = param.getParam("SmpHrmWebService");  //Get System Configuration       		
            com.dsc.kernal.webservice.WSDLClient service = new com.dsc.kernal.webservice.WSDLClient(ws);
            service.dllPath = com.dsc.kernal.utility.Utility.G_GetTempPath();
            service.build(true);
			
			//Prepare XML(This one for test)
		    /*
			string xml = @"<Request><Access><Authentication password="""" user=""gp""/></Access><RequestContent><ServiceType>Dcms.HR.Services.IAttendanceParameterService,Dcms.HR.Business.AttendanceParameter</ServiceType><Method>GetEmpOTHours</Method><Parameters><Parameter type=""System.String"">B7A52998-ED73-4187-BA0B-2587632D13B3</Parameter><Parameter type=""System.DateTime"">2018/01/12</Parameter><Parameter type=""System.Boolean"">True</Parameter><Parameter type=""System.Boolean"">False</Parameter><Parameter type=""System.Boolean"">False</Parameter><Parameter type=""System.String""/></Parameters></RequestContent></Request>";
			*/
			
			//Call Function to Get Xml For Web Service
			string xml = GetXmlForAttendance("1168");
			//writeLog("Request xml : " + xml);
			
			//Call HR WebService
            result = Convert.ToString(service.callSync("IntegrationByXmlWithLanguageEx", xml,"0"));
		  	//writeLog("XML result : " + result);	

			//XML result : 
			//<?xml version="1.0" encoding="utf-16"?>
			//<Response>
			//	  <Execution>
			//	  	  <Status Code="0" description="" />
			//	  </Execution>
			//	  <ResponseContent>
			//	  	  <Data>0.0000</Data>
			//	  </ResponseContent>
			//</Response>

			//Get Response XML Data
			
			XMLProcessor xp = null;
            XmlNode node = null;			
			xp = new XMLProcessor(result, 1);
                XmlNodeList xnl = xp.selectAllNodes("/Response/ResponseContent/Data/DataSet/Master/Record");
                foreach (XmlNode performer in xnl)
                {
					//writeLog("XML performer : " + performer);	
                    string data = performer.InnerText;
					writeLog("XML data : " + data);	

                }
			
			
        }
        catch (Exception ex)
        {
		  	writeLog("WS IntegrationByXmlWithLanguageEx error  : " + ex);
            throw new Exception(ex.StackTrace);
        }
		
    }
	
	//取得WebService 出缺勤刷卡記錄時間XML
	 protected string GetXmlForAttendance(string employeeId)
    {

		string hrEmpid = GetHrEmpIDstring(employeeId);
		writeLog("hrEmpid : " + hrEmpid);
		DateTime dt = DateTime.Now ;
		writeLog(" dt : " + dt.ToShortDateString().ToString().Replace("/","-"));
        string result = null;
		string xml = "";
        try
        {
			
			xml += @"<Request>";
			xml += @"<Access>";
			xml += @"<Authentication user=""gp"" password=""""/>";
			xml += @"</Access>";
			xml += @"<RequestContent>";
			xml += @"<ServiceType> Dcms.HR.Services.IAttendanceCollectService,Dcms.HR.Business.AttendanceCollect</ServiceType>";
			xml += @"<Method>GetCollectByEmpId</Method>";
			xml += @"<Parameters>";
			xml += @"<Parameter type=""System.String"">"+hrEmpid+@"</Parameter>";
			xml += @"<Parameter type=""System.DateTime"">2017-10-31</Parameter>";
			xml += @"<Parameter type=""System.DateTime"">2017-10-31</Parameter>";
			xml += @"</Parameters>";
			xml += @"</RequestContent>";
			xml += @"</Request>";
			
			result = xml;
        }
        catch (Exception e)
        {
			writeLog("WS rejectProcedure error  : " + e);
            throw new Exception(e.StackTrace);
        }
        return result;
    }
	
	//取得WebService 新增刷卡記錄XML
	 protected string GetXmlForAddNewAttendance(string employeeId)
    {

		string hrEmpid = GetHrEmpIDstring(employeeId);
		writeLog("hrEmpid : " + hrEmpid);
		DateTime dt = DateTime.Now ;
		writeLog(" dt : " + dt.ToShortDateString().ToString().Replace("/","-"));
        string result = null;
		string xml = "";
        try
        {
			
			xml += @"<Request>";
			xml += @"<Access>";
			xml += @"<Authentication  user=""gp""  password=""""/>";
			xml += @"</Access>";
			xml += @"<RequestContent>";
			xml += @"<ServiceType>Dcms.HR.Services.IAttendanceCollectService,Dcms.HR.Business.AttendanceCollect</ServiceType>";
			xml += @"<Method>SaveForESS</Method>";
			xml += @"<Parameters>";
			xml += @"<Parameter type=""System.String"">"+hrEmpid+@"</Parameter>";
			xml += @"<Parameter type=""System.DateTime"">2017-10-31</Parameter>";
			xml += @"<Parameter type=""System.DateTime"">2017-10-31</Parameter>";
			xml += @"<Parameter type=""System.String"">02:00,,,,,</Parameter>";
			xml += @"<Parameter type=""System.String"">1004</Parameter>";
			xml += @"<Parameter type=""System.String""><![CDATA[ESSF03-201311290001]]></Parameter>";
			xml += @"<Parameter type=""System.String"">"+hrEmpid+@"</Parameter>";
			xml += @"<Parameter type=""System.String"">00:00</Parameter>";
			xml += @"<Parameter type=""System.String"">23:59</Parameter>";
			xml += @"<Parameter type=""System.String"">ESSF03</Parameter>";
			xml += @"<Parameter type=""System.String"">201801300001</Parameter>";
			xml += @"<Parameter type=""System.Int32"">1</Parameter>";
			xml += @"</Parameters>";
			xml += @"</RequestContent>";
			xml += @"</Request>";
			result = xml;
        }
        catch (Exception e)
        {
			writeLog("WS rejectProcedure error  : " + e);
            throw new Exception(e.StackTrace);
        }
        return result;
    }
	
	//取得WebService 刪除刷卡記錄XML
	 protected string GetXmlForDeleteAttendance(string employeeId)
    {

		string hrEmpid = GetHrEmpIDstring(employeeId);
		writeLog("hrEmpid : " + hrEmpid);
		DateTime dt = DateTime.Now ;
		writeLog(" dt : " + dt.ToShortDateString().ToString().Replace("/","-"));
        string result = null;
		string xml = "";
        try
        {
			
			xml += @"<Request>";
			xml += @"<Access>";
			xml += @"<Authentication password="""" user=""gp""/>";
			xml += @"</Access>";
			xml += @"<RequestContent>";
			xml += @"<ServiceType>Dcms.HR.Services.IAttendanceCollectService,Dcms.HR.Business.AttendanceCollect";
			xml += @"</ServiceType>";
			xml += @"<Method>DeleteForESS</Method>";
			xml += @"<Parameters>";
			xml += @"<Parameter type=""System.String"">ESSF03</Parameter>";
			xml += @"<Parameter type=""System.String"">806112e7-973f-4887-b707-f9c59c91aa87</Parameter>";
			xml += @"<Parameter type=""System.String""></Parameter>";
			xml += @"</Parameters>";
			xml += @"</RequestContent>";
			xml += @"</Request>";
			result = xml;
        }
        catch (Exception e)
        {
			writeLog("WS rejectProcedure error  : " + e);
            throw new Exception(e.StackTrace);
        }
        return result;
    }
	
	//取得人員在HRDB中的EmpID
	protected string GetHrEmpIDstring(string employeeId){
		
		writeLog("In GetHrEmpIDstring");
		string HrEmpID = null;
		
		IOFactory factory = new IOFactory();
		AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
		
		AbstractEngine SmpHrmEngine = null;
		WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
		string SmpHrmDBStr = sp.getParam("SmpHrmDB");
		
		SmpHrmEngine = factory.getEngine(EngineConstants.SQL, SmpHrmDBStr);
		SmpHrmEngine.startTransaction(IsolationLevel.ReadCommitted);
		
		writeLog("SmpHrmEngine : " + SmpHrmEngine);
		
		string sql = "SELECT EmployeeId FROM Employee WHERE Code in ('"+ employeeId +"') ";
		writeLog("sql  : " + sql);
		
		DataSet ds = SmpHrmEngine.getDataSet(sql, "TEMP");
		if (ds.Tables[0].Rows.Count > 0)
		{
			HrEmpID = ds.Tables[0].Rows[0]["EmployeeId"].ToString();
		}
		writeLog("HrEmpID  : " + HrEmpID);
		
		return HrEmpID;
	}
	
   
}




