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
using WebServerProject;
using com.dsc.flow.data;
using com.dsc.flow.server;
using System.Data.OracleClient;

public partial class Simplo_Form_SPAD002_Form : SmpAdFormPage
{
    protected override void init()
    {
        ProcessPageID = "SPAD002";
        AgentSchema = "WebServerProject.form.SPAD002.SPADBAgent";
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
		string sql = null;
        DataSet ds = null;
        int count = 0;
		//公司別
        ids = new string[,]{
            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad002_form_aspx.language.ini", "message", "smp", "新普科技")},
            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad002_form_aspx.language.ini", "message", "tp", "中普科技")}
        };
        COMPANY.setListItem(ids);
        string orgId = "SMP";
		string userId = (string)Session["UserId"];
        sql = "select orgId from EmployeeInfo where empNumber='" + userId + "'";
        string value = (string)engine.executeScalar(sql);

        if (value != null)
        {
            orgId = value;
        }
        COMPANY.ValueText = orgId;
        COMPANY.ReadOnly = true;

        USERGUID.clientEngineType = engineType;
        USERGUID.connectDBString = connectString;
        DEPTID.clientEngineType = engineType;
        DEPTID.connectDBString = connectString;
        REVIEWER.clientEngineType = engineType;
        REVIEWER.connectDBString = connectString;
				
		string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);
        //DEPTID.ValueText = si.submitOrgID;
		DEPTID.ValueText = depData[0];
        DEPTID.doValidate();

        //USERGUID.ValueText = si.fillerID;
		USERGUID.ValueText = userId;
        USERGUID.doValidate();
		
		writeLog("initUI^si.submitOrgID: ^" + DEPTID.ValueText);
		writeLog("initUI^si.fillerID: ^" + USERGUID.ValueText);
        
        //CREATEDATE.ValueText = DateTimeUtility.getSystemTime2(null).Substring(0, 10);
        //CREATEDATE.ReadOnly = true;
        DEPTID.ReadOnly = true;
        STARTTIME.AllowEmpty = true;
        ENDTIME.AllowEmpty = true;
		ButtonSearchByDate.Enabled = false;
        
        //Radio Group
        REASON1.GroupName = "grp1";
        REASON2.GroupName = "grp1";
        REASON3.GroupName = "grp1";

        base.initUI(engine,objects);      
		//witeLogSMP(Convert.ToString(Session["UserID"]), USERGUID.ValueText, USERGUID.ReadOnlyValueText);		
		if (isNew() == true)
        {
            witeLogSMP(Convert.ToString(Session["UserID"]), USERGUID.ValueText, USERGUID.ReadOnlyValueText);
        }
    }         

    protected override void showData(AbstractEngine engine, DataObject objects)
    {        
        writeLog("showData");
        //取得objects 單號, 若有值放入Session
        base.showData(engine, objects);
		
		//公司別
        COMPANY.ValueText = objects.getData("COMPANY");
        
        //部門
        DEPTID.ValueText = objects.getData("DEPTID");
        DEPTID.doValidate();

        //申請人
        USERGUID.GuidValueText = objects.getData("USERGUID");
        USERGUID.doGUIDValidate();
        USERGUID.ReadOnlyValueText = objects.getData("USERNAME");

        //填表日期, 上班時間, 下班時間
        //CREATEDATE.ValueText = objects.getData("CREATE_DATE");
        STARTTIME.ValueText = objects.getData("START_TIME");
        ENDTIME.ValueText = objects.getData("END_TIME");
        
        //審核人
        REVIEWER.GuidValueText = objects.getData("REVIEWER");
        REVIEWER.doGUIDValidate();
        //REVIEWER.ReadOnlyValueText=????? 不需要

        //更正理由      
        string reason_id = Convert.ToString(objects.getData("REASON"));
        switch (reason_id)
        {
            case "0":
                REASON1.Checked = true;
                break;
            case "1":
                REASON2.Checked = true;
                break;
            case "2":
                REASON3.Checked = true;
                break;
            default:
                break;
        }

        //更正理由說明
        REASONDESC.ValueText = objects.getData("REASON_DESC");
		
		//主旨
        Subject.ValueText = objects.getData("Subject");
		GUID.ValueText = objects.getData("GUID");

        //非填表人不能修改資料        
        string actName = Convert.ToString(getSession("ACTName"));
        if (actName != "填表人" && actName != "")
        {
            DEPTID.ReadOnly = true;
            USERGUID.ReadOnly = true;
            REASON1.ReadOnly = true;
            REASON2.ReadOnly = true;
            REASON3.ReadOnly = true;
            REASONDESC.ReadOnly = true;
            STARTTIME.ReadOnly = true;
            ENDTIME.ReadOnly = true;
            REVIEWER.ReadOnly = true;
			COMPANY.ReadOnly = true;
        }                
    }

    protected override void afterReadDraft(AbstractEngine engine, DataObject currentObject)
    {
        //base.afterReadDraft(engine, currentObject);
        //如果資料由草稿讀出, 申請日期 reset 成今天日期                
        //currentObject.setData("CREATE_DATE", DateTimeUtility.getSystemTime2(null).Substring(0, 10));                    
    }

    protected override void saveData(AbstractEngine engine, DataObject objects)
    {    
		writeLog("showData");
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        bool isAddNew = base.isNew();
        
        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            //取得 Session 單號, 若無值則產生新單號
            base.saveData(engine,objects);                        
            objects.setData("USERGUID", USERGUID.GuidValueText);
            objects.setData("USERID", USERGUID.ValueText);
            objects.setData("USERNAME", USERGUID.ReadOnlyValueText);
            objects.setData("DEPTID", DEPTID.ValueText);
            objects.setData("DEPTNAME", DEPTID.ReadOnlyValueText);
            //objects.setData("CREATE_DATE", CREATEDATE.ValueText);            
            objects.setData("REASON_DESC", REASONDESC.ValueText);
            objects.setData("START_TIME", STARTTIME.ValueText);
            objects.setData("END_TIME", ENDTIME.ValueText);
            objects.setData("COMPANY", COMPANY.ValueText);
            objects.setData("REVIEWER", REVIEWER.GuidValueText);    
			objects.setData("Subject", Subject.ValueText);			
           
			//更正理由      
            string reason_id = "";
			string reason_name = "";
            if (REASON1.Checked){
                reason_id = "0";
				reason_name="未帶卡片";
			}
            if (REASON2.Checked){
                reason_id = "1";
				reason_name="忘記刷卡";
			}
            if (REASON3.Checked){
                reason_id = "2";
				reason_name="其他(請說明)";
			}
            objects.setData("REASON", reason_id);
			objects.setData("REASON_OPTION", reason_name);
        }

        //於人事承辦人員單位呼叫 beforeSign function
        //beforeSign 要啟動必須設定 session 參數 isAddSign="AFTER"        
        string actName = (string)getSession("ACTName");        
        if (actName.Equals("人事承辦人員"))                  
        {
            setSession("isAddSign", "AFTER");
        };       
    }   

    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        string CrLf = "\r\n";
        string errorMessage = "";
        string startTime = STARTTIME.ValueText;
        string endTime = ENDTIME.ValueText;
        string currentTime=DateTimeUtility.getSystemTime2(null).Substring(0, 19);
		string strReason = "";
		string strTime = "";

        if (REASON3.Checked && REASONDESC.ValueText.Equals(""))
        {
            errorMessage = errorMessage + "更正理由為其他時, 必須填寫說明欄!" + CrLf;            
        }

        if (startTime.Equals("") && endTime.Equals(""))
        {
            errorMessage = errorMessage + "上班時間, 下班時間必需填寫一個!" + CrLf;            
        }
        
        if (!startTime.Equals("")) 
            if (Convert.ToDateTime(startTime) > Convert.ToDateTime(currentTime))
                errorMessage = errorMessage + "上班時間不能填寫未來時間!" + CrLf;

        if (!endTime.Equals(""))
            if (Convert.ToDateTime(endTime) > Convert.ToDateTime(currentTime))
                errorMessage = errorMessage + "下班時間不能填寫未來時間!" + CrLf;

        if (startTime.Equals(endTime) && !(startTime.Equals("") && endTime.Equals("")))
            errorMessage = errorMessage + "上下班時間不能相同!!" + CrLf;
		
		if (REASON1.Checked){
			strReason="未帶卡片";
		}
        if (REASON2.Checked){
			strReason="忘記刷卡";
		}
        if (REASON3.Checked){
			strReason="其他:" + REASONDESC.ValueText;
		}		
		
		if(startTime.Equals(""))
			strTime = endTime;
		else if (endTime.Equals(""))	
			strTime = startTime;
		else if ( (!startTime.Equals("")) && (!endTime.Equals("")))
			strTime = startTime + "~" + endTime; 
			
		//設定主旨
        string[] values = base.getUserInfo(engine, USERGUID.GuidValueText);
		string userName = values[1];
        string subject = "【人員：" + userName + " 原因:"+strReason +"  修正時間：" + strTime + " 】";
        Subject.ValueText = subject;// +Subject.ValueText;	

        if (!errorMessage.Equals(""))
        {
            pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("SmpProgram_Form_SPAD002_Form_aspx.language.ini", "message", "QueryError1", errorMessage));
            result = false;
        }        
        return result;
    }


    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

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
        witeLogSMP(Convert.ToString(Session["UserID"]), USERGUID.ValueText, USERGUID.ReadOnlyValueText);
        return si;
    }
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

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
        hs.Add("FORMID", ProcessPageID);
        return hs;
    }        
    

    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {        
        writeLog("setFlowVariables");
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
		
		string[] values = null;
        values = base.getUserInfo(engine, currentObject.getData("REVIEWER"));
        string checkUserId = values[0];
				
        param["SPAD002"] = "<SPAD002>";
        //關係人            
        param["SPAD002"] += "    <Applicant dataType=\"java.lang.String\">" + currentObject.getData("USERID") + "</Applicant>";

        //部門收發
        string[] userData2 = getUserRoles(engine, "部門收發", currentObject.getData("DEPTID"));
        if (!userData2[2].Equals(""))
            param["SPAD002"] += "    <Assistant dataType=\"java.lang.Integer\">" + userData2[2] + "</Assistant>";
        else
        {
            param["SPAD002"] += "    <Assistant dataType=\"java.lang.Integer\">" + "NA" + "</Assistant>";                
        }            

        //審核人
        if (!REVIEWER.ValueText.Equals(""))
            param["SPAD002"] += "    <Reviewer dataType=\"java.lang.String\">" + checkUserId + "</Reviewer>";
        else
            param["SPAD002"] += "    <Reviewer dataType=\"java.lang.String\">NA</Reviewer>";

        //HR Owner, 應可不傳, 流程裡已指定固定群組
        //param["SPAD002"] += "    <HROwner dataType=\"java.lang.String\">4020</HROwner>";
        param["SPAD002"] += "    <HROwner dataType=\"java.lang.String\">SMP-HRADM</HROwner>";
            
        //填表人            
        param["SPAD002"] += "    <Creator dataType=\"java.lang.String\">" + si.fillerID + "</Creator>";

        //關係人(申請人)主管
        string[] userData = getUserManagerInfo(engine, currentObject.getData("USERGUID"));
		values = base.getUserInfo(engine, userData[0]);
        string managerId = values[0];
		
        if (!managerId.Equals(""))
            param["SPAD002"] += "    <Boss dataType=\"java.lang.String\">" + managerId + "</Boss>";
        else
        {
            MessageBox("無申請人主管資料,組織資料設定錯誤!");
            throw new Exception("申請人:" + currentObject.getData("USERID") +", 無申請人主管資料, 組織資料設定錯誤!");
        }            
        param["SPAD002"] += "</SPAD002>";
		
		writeLog("setFlowVariables^" + param["SPAD002"]);	
		
        return "SPAD002";      
    }
           
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
        //base.afterSign(engine, currentObject, result);
		return base.beforeSign(engine, isAfter, addSignXml);
    }   

    
    /// <summary>
    /// 重辦程序
    /// 重辦當關立即 Terminate Process, 不退回第一關!
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
                writeLog(e);
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
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
		//writeLog("afterApprove");
        //取得目前關卡名稱
        string[] values = base.getNowSignActName(engine, currentObject.getData("GUID"));
        string actName = values[1];
        
        if (actName.Equals("人事承辦人員"))
        {
            try
            {                
                //產生刷卡紀錄
				writeLog("afterApprove actName :" + actName);
                if (!updateHR(engine, currentObject))
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

        base.afterApprove(engine, currentObject, result);
    }
    
    /* 舊版本, 退回第一關並終止, 會多發 email
    protected override void rejectProcedure()
    {
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回關卡 ID        
        //先退回
        base.rejectProcedure();
        if (backActID == "Creator") //流程之中, 申請人關卡的 ID 值
        {
            //終止流程
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            base.terminateThisProcess(si.ownerID);
        }
    }
    */

    //User Defined Functions from here
    /// <summary>
    /// prepareSQL: 組出 Update HR 之 SQL
    /// </summary>
    /// <param name="checkTime"></param> 傳入上班或下班時間
    /// <returns></returns> 回傳值為 SQL 字串
    private string prepareSQL(string checkTime)
    {
		
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string strCompany = COMPANY.ValueText; //公司別
        string strCreator = (string)Session["UserID"]; //建立者 (應為簽核者較好)
	    string strUsrGroup = strCompany; //User Group
        //string strCreateDate = CREATEDATE.ValueText.Replace("/", ""); //Create Date
        string strCreateDate = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", ""); //用現在的日期
	    string strFlag = "1"; //Flag
        string strMC001 = USERGUID.ValueText; //員工代號
        string strMC005 = "N";	//產生明細碼
        string strMC900 = "03"; //機台別
        //MessageBox("SHEETNO: " + SHEETNO.ValueText.Substring(7, 8));
        string sheetNo = Convert.ToString(getSession(PageUniqueID, "SheetNo"));
        if (sheetNo.Length < 15)
        {
            throw new Exception("表單單號錯誤(長度不足15碼)!");
        }
        string strMC901 = sheetNo.Substring(7,8); //簽核單號 , 只能填 10 碼(string)getSession("FlowID")        
        string strMC002 =checkTime.Substring(0,10).Replace("/",""); //刷卡日期
        string strMC003 = checkTime.Substring(11, 5).Replace(":", ""); //刷卡時間
        string strMC006=strMC002; //歸屬日期
        string strSQL = "";        
        strSQL = "insert into AMSMC (COMPANY,CREATOR,USR_GROUP,CREATE_DATE,FLAG,MC001,MC002,MC003,MC005,MC006,MC900,MC901) values (";
	    strSQL = strSQL + "'" + strCompany + "'," ;
	    strSQL = strSQL + "'" + strCreator + "'," ;
	    strSQL = strSQL + "'" + strUsrGroup + "'," ;
	    strSQL = strSQL + "'" + strCreateDate + "',"; 
	    strSQL = strSQL + "'" + strFlag + "'," ;
	    strSQL = strSQL + "'" + strMC001 + "'," ;
	    strSQL = strSQL + "'" + strMC002 + "'," ;
	    strSQL = strSQL + "'" + strMC003 + "'," ;
	    strSQL = strSQL + "'" + strMC005 + "'," ;
	    strSQL = strSQL + "'" + strMC006 + "'," ;
	    strSQL = strSQL + "'" + strMC900 + "'," ;
	    strSQL = strSQL + "'" + strMC901 + "')";
        return strSQL;
    }
/*
    /// <summary>
    /// 更新 HR 資料庫
    /// </summary>
    /// <returns></returns>
    private Boolean updateHR()
    {
        //prepare update sql string
        //string strSQL = prepareSQL(STARTTIME.ValueText);
        //MessageBox(strSQL);

        //connect HR system
        AbstractEngine engine = null;
        AbstractEngine hrengine = null;
        string sql = "";
        string startTime = STARTTIME.ValueText;
        string endTime = ENDTIME.ValueText;	
		string strUserId = USERGUID.ValueText;
		string strMC002s = "";
		string strMC003s = "";
		string strMC002e = "";
		string strMC003e = "";
		
		if (!startTime.Equals(""))
		{
	        strMC002s =startTime.Substring(0,10).Replace("/",""); //刷卡日期
	        strMC003s = startTime.Substring(11, 5).Replace(":", ""); //刷卡時間
		}
		if (!endTime.Equals(""))
		{
	        strMC002e = endTime.Substring(0,10).Replace("/",""); //刷卡日期
	        strMC003e = endTime.Substring(11, 5).Replace(":", ""); //刷卡時間
        }
		
        try
        {
            //取得 HR DB Connection String
            IOFactory factory = new IOFactory();
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];            
            engine = factory.getEngine(engineType, connectString);

            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
			
            string companyId = COMPANY.ValueText;
            string hrconStr = sp.getParam(companyId + "WorkFlowERPDB");		
			
            //string hrconStr = sp.getParam("WorkFlowERPDB");//由變數取得 HRDB connection string, 如同下列字串            
            //string hrconStr = "USER=EASYFLOW;PWD=XXXXX;SERVER=192.168.2.17;DATABASE=SMP_TEST";                               
            if (hrconStr.Equals(""))
            {
                throw new Exception("找不到HR系統之參數設定:" + hrconStr);
            }

            //建立 HR SQL Server 資料庫連線
            hrengine = factory.getEngine(EngineConstants.SQL, hrconStr);
			
			//檢查出勤資料是否重覆, 若重覆則不再insert
			if (!startTime.Equals(""))
			{
	            sql = "select * from AMSMC where MC001='"+strUserId+"' and MC002='"+strMC002s+"' and MC003='"+strMC003s+"' ";
	            DataSet ds = hrengine.getDataSet(sql, "TEMP");
	            //int rows = ds.Tables[0].Rows.Count;
	            //ds = hrengine.getDataSet(sql, "TEMP");
	            
	            if (ds.Tables[0].Rows.Count > 0) {
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
	                    throw new Exception(hrengine.errorString);
	                }                
	            }
			}
			
			
			//檢查出勤資料是否重覆, 若重覆則不再insert
			if (!endTime.Equals(""))
			{
	            sql = "select * from AMSMC where MC001='"+strUserId+"' and MC002='"+strMC002e+"' and MC003='"+strMC003e+"' ";
	            DataSet ds = hrengine.getDataSet(sql, "TEMP");
	            //rows = ds.Tables[0].Rows.Count;
	            if (ds.Tables[0].Rows.Count > 0)
	            {
	                //資料已存在, 不再insert出差資料
	            }
	            else
	            {
	            //處理下班時間
	            //if (!endTime.Equals(""))
	            //{                
	                sql = "";
	                sql = prepareSQL(endTime);
	                if (!hrengine.executeSQL(sql))
	                {
	                    throw new Exception(hrengine.errorString);
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
            
            showMessage(te.Message);
            return false;
        }

        //關閉引擎
        engine.close();
        return true;
    }
*/

    /// <summary>
    /// 更新 HR 資料庫, 傳入 AbstractEngine engine, DataObject currentObject
    /// </summary>
    /// <returns></returns>
    private Boolean updateHR(AbstractEngine engine, DataObject currentObject)
    {
		writeLog("updateHR Start" );
        //connect HR system
        //AbstractEngine engine = null;
        AbstractEngine hrengine = null;
        string sql = "";
        //string startTime = STARTTIME.ValueText;
        //string endTime = ENDTIME.ValueText;
        //string strUserId = USERGUID.ValueText;

        string startTime = currentObject.getData("START_TIME");
        string endTime = currentObject.getData("END_TIME");
        string strUserId = currentObject.getData("USERGUID"); //工號
        string strMC002s = "";
        string strMC003s = "";
        string strMC002e = "";
        string strMC003e = "";
		string strMC900  = "";
		
		writeLog("updateHR Values : " + startTime + " - "+ endTime + " - "+ strUserId );

        if (!startTime.Equals(""))
        {
            strMC002s = startTime.Substring(0, 10).Replace("/", ""); //刷卡日期
            strMC003s = startTime.Substring(11, 5).Replace(":", ""); //刷卡時間
			strMC900  = "02";
        }
        if (!endTime.Equals(""))
        {
            strMC002e = endTime.Substring(0, 10).Replace("/", ""); //刷卡日期
            strMC003e = endTime.Substring(11, 5).Replace(":", ""); //刷卡時間
			strMC900  = "03";
        }

        try
        {
            //取得 HR DB Connection String
            IOFactory factory = new IOFactory();
            //string connectString = (string)Session["connectString"];
            //string engineType = (string)Session["engineType"];
            //engine = factory.getEngine(engineType, connectString);

            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);

            string companyId = currentObject.getData("COMPANY");
            string hrconStr = sp.getParam(companyId + "WorkFlowERPDB");

            //string hrconStr = sp.getParam("WorkFlowERPDB");//由變數取得 HRDB connection string, 如同下列字串            
            //string hrconStr = "USER=EASYFLOW;PWD=XXXXX;SERVER=192.168.2.17;DATABASE=SMP_TEST";                               
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
                //int rows = ds.Tables[0].Rows.Count;
                //ds = hrengine.getDataSet(sql, "TEMP");

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
                    if (!hrengine.executeSQL(sql))
                    {
                        throw new Exception(hrengine.errorString);
                    }
                }
            }


            //檢查出勤資料是否重覆, 若重覆則不再insert
            if (!endTime.Equals(""))
            {
                sql = "select * from AMSMC where MC001='" + strUserId + "' and MC002='" + strMC002e + "' and MC003='" + strMC003e + "' ";
                DataSet ds = hrengine.getDataSet(sql, "TEMP");
                //rows = ds.Tables[0].Rows.Count;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //資料已存在, 不再insert出差資料
                }
                else
                {
                    //處理下班時間
                    //if (!endTime.Equals(""))
                    //{                
                    sql = "";
                    sql = prepareSQL(endTime, currentObject, engine, strMC900);
                    if (!hrengine.executeSQL(sql))
                    {
                        throw new Exception(hrengine.errorString);
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

            showMessage(te.Message);
            return false;
        }

        //關閉引擎
        engine.close();
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
		writeLog("prepareSQL currentObject COMPANY:" +  currentObject.getData("COMPANY"));
		
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string strCompany = currentObject.getData("COMPANY"); //COMPANY.ValueText; //公司別
        //string strCreator = currentObject.getData("");  //(string)Session["UserID"]; //建立者 (應為簽核者較好)
		string[] values = base.getUserInfo(engine, currentObject.getData("USERGUID"));
        string strCreator = values[0];
        string strUsrGroup = strCompany; //User Group
        //string strCreateDate = CREATEDATE.ValueText.Replace("/", ""); //Create Date
        string strCreateDate = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", ""); //用現在的日期
        string strFlag = "1"; //Flag
        values = base.getUserInfo(engine, currentObject.getData("USERGUID"));
		string strMC001 = values[0]; //currentObject.getData("USERGUID"); //USERGUID.ValueText; //員工代號		
        string strMC005 = "N";	//產生明細碼
        //string strMC900 = "03"; //機台別
        //MessageBox("SHEETNO: " + SHEETNO.ValueText.Substring(7, 8));
        string sheetNo = currentObject.getData("SheetNo"); // Convert.ToString(getSession(PageUniqueID, "SheetNo"));
        if (sheetNo.Length < 15)
        {
            throw new Exception("表單單號錯誤(長度不足15碼)!");
        }
        string strMC901 = sheetNo.Substring(7, 8); //簽核單號 , 只能填 10 碼(string)getSession("FlowID")        
        string strMC002 = checkTime.Substring(0, 10).Replace("/", ""); //刷卡日期
        string strMC003 = checkTime.Substring(11, 5).Replace(":", ""); //刷卡時間
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
		writeLog( " insert AMSMC SQL : " + strSQL);
        return strSQL;
    }

    private void showMessage(string msg)
    {
        Response.Write("<script language=javascript>");
        Response.Write("alert('" + msg.Replace(System.Environment.NewLine, "\\n") + "');");
        Response.Write("</script>");
    }
   
    /// <summary>
    /// 使用者開窗異動後,自動更新部門欄位
    /// </summary>
    /// <param name="values"></param>
    protected void  USERGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        //MessageBox(values[0, 0]);
        //MessageBox(values[0, 1]);
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            AbstractEngine engine = null;
            IOFactory factory = new IOFactory();            
            string userGUID = values[0, 0];			
            string[] depData;
			string orgId = "SMP";
            try
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];				
                engine = factory.getEngine(engineType, connectString);				
                depData = getDeptInfo(engine, userGUID);
                DEPTID.ValueText = depData[0];
                DEPTID.doValidate();
				
				//公司別
				string[] userValues = base.getUserInfoById(engine, USERGUID.ValueText);
	            string value = (string)engine.executeScalar("select orgId from EmployeeInfo where empNumber='" + userValues[0] + "'");
				if (value != null)
	            {
	                orgId = value;
	            }
	            COMPANY.ValueText = orgId;	
            }
            catch (Exception ue)
            {
                try
                {
                    engine.close();
                }
                catch { };

                MessageBox(ue.Message);
            }            
            engine.close();
			
			//當填單人不=申請人, 無法看到出勤明細
			if (USERGUID.ValueText.Equals((string)Session["UserId"]))
            {
                ButtonSearch.Enabled = true;
				ButtonSearchByDate.Enabled = true;
            }
            else
            {
                ButtonSearch.Enabled = false;
				ButtonSearchByDate.Enabled = false;
            }
        }
    }
	
	/// <summary>
    /// 請30天出勤明細查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSearch_OnClick(object sender, EventArgs e)
    {
		string strCompany = COMPANY.ValueText;
        Session["SPAD002_OriginatorId"] = USERGUID.ValueText;
		Session["SPAD002_CompanyId"] = strCompany;
		Session["SPAD002_StartDate"] = "";
        Session["SPAD002_EndDate"] = "";
        string url = "AttendSummary.aspx";
        base.showOpenWindow(url, "查詢近30天出勤明細", "", "500", "", "", "", "1", "1", "", "", "", "", "300", "", true);
    }
	
	/// <summary>
    /// 上下班日期出勤明細
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSearchByDate_OnClick(object sender, EventArgs e)
    {
        string startTime = STARTTIME.ValueText;
        string endTime = ENDTIME.ValueText;
        string strUserId = USERGUID.ValueText;
		string strCompany = COMPANY.ValueText;
		string startDate = "";
		string endDate = "";

		if (!startTime.Equals("") ){
			startDate = startTime.Substring(0, 10).Replace("/", ""); //刷卡日期
		}
		if (!endTime.Equals("") ){
			endDate = endTime.Substring(0, 10).Replace("/", ""); //刷卡日期
		}
		
        Session["SPAD002_OriginatorId"] = strUserId;
		Session["SPAD002_CompanyId"] = strCompany;
        Session["SPAD002_StartDate"] = startDate;
        Session["SPAD002_EndDate"] = endDate;
        string url = "AttendSummary.aspx";
        base.showOpenWindow(url, "上下班日期出勤明細", "", "300", "", "", "", "1", "1", "", "", "", "", "300", "", true);
    }
	
	protected void ClearStartDateButton_Click(object sender, EventArgs e)
    {
        STARTTIME.ValueText = "";
    }

    protected void ClearEndDateButton_Click(object sender, EventArgs e)
    {
        ENDTIME.ValueText = "";
    }
	protected void StartDate_DateTimeClick(string values)
    {
        if (!STARTTIME.ValueText.Equals("") || !ENDTIME.ValueText.Equals(""))
        {
            ButtonSearchByDate.Enabled = true;
        }        
    }
	protected void EndDate_DateTimeClick(string values)
    {
        if (!STARTTIME.ValueText.Equals("") || !ENDTIME.ValueText.Equals(""))
        {
            ButtonSearchByDate.Enabled = true;
        }        
    }
    protected void USERGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            USERGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
    protected void REVIEWER_BeforeClickButton()
    {
        if (base.isNew())
        {
            REVIEWER.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
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
            sw = new System.IO.StreamWriter(serverPath + @"\SPAD002.log", true, System.Text.Encoding.Default);
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
			
	private void witeLogSMP(string UserID, string fillerID, string fillerName)
    {
        try
        {
            writeLog(new Exception(ProcessPageID + " " + DateTime.Now.ToString("yyyy/mm/dd HH:mm:ss.ff") + " Session[\"UserID\"]=" + UserID + " fillerID=" + fillerID + " fillerName=" + fillerName), false);
        }
        catch { }
    }
}