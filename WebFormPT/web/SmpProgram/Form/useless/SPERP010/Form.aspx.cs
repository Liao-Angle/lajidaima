using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
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
using com.dsc.flow.server;

public partial class SmpProgram_System_Form_SPERP010_Form : SmpErpFormPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPERP010"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPERP010.SmpRfqOmFormAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPERP";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        //改變工具列順序
        base.initUI(engine, objects);
		
		string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        KeyId.Display = false;
        SheetNo.Display = false;
        OriginatorId.Display = false;
        //Reviewer1GUID.Display = false;
		//Reviewer2GUID.Display = false;
        ApproverId.Display = false;
        IsResolved.Display = false;
		IsCheckUser.Display = false;
		
		//審核人1
        Reviewer1GUID.clientEngineType = engineType;
        Reviewer1GUID.connectDBString = connectString;
		
		//審核人2
        Reviewer2GUID.clientEngineType = engineType;
        Reviewer2GUID.connectDBString = connectString;

        //測試資料
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            Random rnd = new Random();
            int i = rnd.Next();
            KeyId.ValueText = Convert.ToString(i);
            objects.setData("Subject", "主旨 - 測試 - " + i);
            objects.setData("TypeCode", "AP");
            OriginatorId.ValueText = "3787";
            //Reviewer1Id.ValueText = "4019";
			//Reviewer2Id.ValueText = "2556";
            ApproverId.ValueText = "4019";
            objects.setData("HtmlContent", "HTML Conent is here !");
			objects.setData("HtmlContentExt", "HTML Conent Ext is here !");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        base.showData(engine, objects);
        KeyId.ValueText = objects.getData("KeyId");
        OriginatorId.ValueText = objects.getData("OriginatorId");
        //Reviewer1GUID.ValueText = objects.getData("Reviewer1GUID");
		//Reviewer2GUID.ValueText = objects.getData("Reviewer2GUID");
        ApproverId.ValueText = objects.getData("ApproverId");
        IsResolved.ValueText = objects.getData("IsResolved");
		IsCheckUser.ValueText = objects.getData("IsCheckUser");
		bool isAddNew = base.isNew();
		string actName = Convert.ToString(getSession("ACTName"));
		
		//審核人員1
        string reviewer1GUID = objects.getData("Reviewer1GUID");
        if (!reviewer1GUID.Equals(""))
        {
            Reviewer1GUID.GuidValueText = reviewer1GUID; //將值放入人員開窗元件中, 資料庫存放GUID
            Reviewer1GUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
		//審核人員1
        string reviewer2GUID = objects.getData("Reviewer2GUID");
        if (!reviewer2GUID.Equals(""))
        {
            Reviewer2GUID.GuidValueText = reviewer2GUID; //將值放入人員開窗元件中, 資料庫存放GUID
            Reviewer2GUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
		
        //內容-單頭
        string htmlContent = objects.getData("HtmlContent");
        if (!htmlContent.Equals(""))
        {
            HtmlContentCode.InnerHtml = htmlContent;
        }
		//內容-單身
        string htmlContentExt = objects.getData("HtmlContentExt");
        if (!htmlContentExt.Equals(""))
        {
            HtmlContentCodeExt.InnerHtml = htmlContentExt;
        }
		
		if (actName.Equals("申請人"))
        {
			Reviewer1GUID.ReadOnly = false;
			Reviewer2GUID.ReadOnly = false;
		}else{
			Reviewer1GUID.ReadOnly = true;
			Reviewer2GUID.ReadOnly = true;
		}
		
		if (!IsResolved.ValueText.Equals("Y"))
		{
			Reviewer1GUID.Display = false;
			Reviewer2GUID.Display = false;
			LblCheckby1.Display = false;
			LblCheckby2.Display = false;
		}
		
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        bool isAddNew = base.isNew();
		string actName = Convert.ToString(getSession("ACTName"));
        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("KeyId", KeyId.ValueText);
            objects.setData("OriginatorId", OriginatorId.ValueText);
            objects.setData("Reviewer1GUID", Reviewer1GUID.GuidValueText);
			objects.setData("Reviewer2GUID", Reviewer2GUID.GuidValueText);
            objects.setData("ApproverId", ApproverId.ValueText);
            objects.setData("IsResolved", IsResolved.ValueText);
			objects.setData("IsCheckUser", IsCheckUser.ValueText);
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
            base.saveData(engine, objects);
        }
		objects.setData("Reviewer1GUID", Reviewer1GUID.GuidValueText);
		objects.setData("Reviewer2GUID", Reviewer2GUID.GuidValueText);		

        //if (ApproverId.ValueText.Equals(""))
        //{
        //    string reviewerId = ReviewerId.ValueText;
        //    string[] reviewerIds = reviewerId.Split(';');
        //    ApproverId.ValueText = reviewerIds[reviewerIds.Length - 1];
        //}

        //beforeSetFlow
        setSession("IsSetFlow", "Y");
        setSession("IsAddSign", "AFTER");
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        string strErrMsg = "";
        //string actName = Convert.ToString(getSession("ACTName"));
        int keyId = Convert.ToInt32(KeyId.ValueText);
        //strErrMsg = base.checkErpData(engine, keyId);

        if (!strErrMsg.Equals(""))
        {
            pushErrorMessage(strErrMsg);
            result = false;
        }

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="autoCodeID"></param>
    /// <returns></returns>
    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
        Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID); //自動編號設定作業
        return hs;
    }

    /// <summary>
    /// 設定表單參數
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
		//System.IO.StreamWriter sw = null;
		//sw = new System.IO.StreamWriter(@"d:\temp\SPERP010.log", true, System.Text.Encoding.Default);
		//sw.WriteLine("setFlowVariables ");		
        string xml = "";
		string sql = "";
		string strdeptId = "";
		DataSet ds = null;
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string creatorId = si.fillerID;
        string originatorId = OriginatorId.ValueText;
        string reviewer1Id = Reviewer1GUID.ValueText;
		string reviewer2Id = Reviewer2GUID.ValueText;
		string costCenterFlag = ""; 
		string strCostCenter = "";
		string actName = Convert.ToString(getSession("ACTName")); //關卡名稱
		//sw.WriteLine("actName :  " + actName);
		//string approverId = ApproverId.ValueText;	
        if (originatorId.Equals(creatorId))
        {
            originatorId = creatorId;
        }
		if (actName.Equals("manager") || actName.Equals("主管"))
		{
			costCenterFlag = IsCheckUser.ValueText;
			//sw.WriteLine("costCenterFlag :  " + costCenterFlag);
			if (costCenterFlag.Equals("Y"))
			{
				string[][] groupUsers = base.getGroupdUser(engine, "SPERP010-CostCenter");
				string userId = groupUsers[0][0];
				strCostCenter = userId;
				//sw.WriteLine("strCostCenter :  " + strCostCenter);
				
			}
			
		}
		if (actName.Equals("CostCenter"))
		{
			reviewer2Id = "T0001"; //處級主管
			sql = "select ou.id  from UserFunctions uf, OrganizationUnit ou where uf.UnitOID=ou.OID and uf.UserID= '" + originatorId + "'";
	        ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                strdeptId = ds.Tables[0].Rows[0][0].ToString();
            }
			if (strdeptId.Substring(0, 2).Equals("BJ"))
            {
                string[][] groupUsers = base.getGroupdUser(engine, "SPERP010-PBOU");
                string userId = groupUsers[0][0];
                reviewer2Id = userId;
            }else{
				string[][] groupUsers = base.getGroupdUser(engine, "SPERP010-LEVOU");
                string userId = groupUsers[0][0];
                reviewer2Id = userId;
			}
			
			//sw.WriteLine("reviewer2Id :  " + reviewer2Id);
		}
		
		//主管
        //填表人員的主管
        string[] userGuidValues = base.getUserGUID(engine, originatorId);
        string[] values = base.getUserManagerInfo(engine, userGuidValues[0]);
        string managerGUID = values[0];            
        values = base.getUserInfo(engine, managerGUID);
        string managerId = values[0];

        xml += "<SPERP010>";
        xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
        xml += "<originator DataType=\"java.lang.String\">" + creatorId + "</originator>";
        xml += "<reviewer1 DataType=\"java.lang.String\">" + reviewer1Id + "</reviewer1>";
		xml += "<reviewer2 DataType=\"java.lang.String\">" + reviewer2Id + "</reviewer2>";		
        xml += "<manager DataType=\"java.lang.String\">" + managerId + "</manager>";
		xml += "<preperson1 DataType=\"java.lang.String\">" + strCostCenter + "</preperson1>";
		xml += "<prenote1 DataType=\"java.lang.String\">" + costCenterFlag + "</prenote1>";
        xml += "</SPERP010>";
         
		//sw.WriteLine("xml : " + xml);	
		//if (sw != null) sw.Close();
		
        //表單代號
        param["SPERP010"] = xml;
        return "SPERP010";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="setFlowXml"></param>
    /// <returns></returns>
    protected override string beforeSetFlow(AbstractEngine engine, string setFlowXml)
    {
        return setFlowXml;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected override void beforeSend(AbstractEngine engine, DataObject currentObject)
    {
        base.beforeSend(engine, currentObject);
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
        string xml = "";
        //string actName = Convert.ToString(getSession("ACTName").ToString());
        //if (!IsResolved.ValueText.Equals("Y"))
        //{   
        //    DataObject currentObject = (DataObject)getSession("currentObject");
        //    currentObject.setData("IsResolved", "Y");
        //    base.saveDB(engine, currentObject, null, (string)getSession("UIStatus"));
        //}
        		
        return base.beforeSign(engine, isAfter, addSignXml);
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
                //SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
                //call oracle function set object workflow status to 'Reject'
                updateSourceStatus("Reject");
                //base.terminateThisProcess(si.fillerID);
                base.terminateThisProcess();
            }
            catch (Exception e)
            {
                base.writeLog(e);
                throw new Exception(e.StackTrace);
            }
        }
        else
        {
            base.rejectProcedure();
        }
    }


    /// <summary>
    /// 取回程序
    /// </summary>
    protected override void reGetProcedure()
    {
        //call oracle function set object workflow status to 'ReGet'
        updateSourceStatus("ReGet");
        base.reGetProcedure();
    }

    /// <summary>
    /// 撤銷程序
    /// </summary>
    protected override void withDrawProcedure()
    {
        //call oracle function set object workflow status to 'WithDraw'
        updateSourceStatus("WithDraw");
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
        try
        {
            //string sourceId = currentObject.getData("FlowId");
            string sourceId = currentObject.getData("KeyId");
			//string sheetNo = currentObject.getData("SheetNo");
            if (result.Equals("Y"))
            {
                updateSourceStatus(engine, sourceId, "Close", currentObject);
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        base.afterApprove(engine, currentObject, result);
    }
	

    /// <summary>
    /// 更新需求單狀態
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    private string updateSourceStatus(string status)
    {
        string result = "";
        DataObject currentObject = (DataObject)getSession("currentObject");
        //string sourceId = currentObject.getData("FlowId");
        string sourceId = currentObject.getData("KeyId");
        if (!sourceId.Equals(""))
        {
            result = updateSourceStatus(sourceId, status);
        }
        return result;
    }
	
    /// <summary>
    /// 更新需求單狀態
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="number"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private string updateSourceStatus(string number, string status)
    {
        string result = "";
        OracleConnection conn = null;
        try
        {
            conn = base.getErpPortalConn();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "smp_update_om_quote_headers";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_header_id", OracleType.Number).Value = Convert.ToInt32(number);
            objCmd.Parameters.Add("p_approved_status", OracleType.VarChar).Value = status;
			objCmd.Parameters.Add("p_action_history", OracleType.VarChar).Value = "";
            objCmd.Parameters.Add("return_value", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;		
            conn.Open();
            objCmd.ExecuteNonQuery();
            result = Convert.ToString(objCmd.Parameters["return_value"].Value);
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            conn.Close();
        }
        return result;
    }
	
    /// <summary>
    /// 更新需求單狀態
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="number"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private string updateSourceStatus(AbstractEngine engine, string number, string status, DataObject currentObject)
    {
		//System.IO.StreamWriter sw = null;
		//sw = new System.IO.StreamWriter(@"d:\temp\SPERP005.log", true, System.Text.Encoding.Default);
		
		//sw.WriteLine("updateSourceStatus ");
		
        string result = "";
		string strOpinion = "";
        OracleConnection conn = null;
        try
        {
			string sheetNo = currentObject.getData("SheetNo");	
			strOpinion = getWorkflowOpinion(engine, sheetNo);
			
			//sw.WriteLine("strOpinion=> " + strOpinion);
			
            conn = base.getErpPortalConn();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "smp_update_om_quote_headers";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_header_id", OracleType.Number).Value = Convert.ToInt32(number);
            objCmd.Parameters.Add("p_approved_status", OracleType.VarChar).Value = status;
			objCmd.Parameters.Add("p_action_history", OracleType.VarChar).Value = strOpinion;
            objCmd.Parameters.Add("return_value", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
            conn.Open();
            objCmd.ExecuteNonQuery();
            result = Convert.ToString(objCmd.Parameters["return_value"].Value);
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            conn.Close();
			//sw.Close();
        }
        return result;
    }
	

	

    /// <summary>
    /// 取得簽核意見
    /// </summary>
    /// <param name="engine"></param>
    /// <returns></returns>
    private string getWorkflowOpinion(AbstractEngine engine, string sheetNo)
    {	
		//System.IO.StreamWriter sw = null;
		//sw = new System.IO.StreamWriter(@"d:\temp\SPERP010.log", true, System.Text.Encoding.Default);

        string strOpinion = "";
		
        try
        {
            string strSheetNo = Convert.ToString(getSession(PageUniqueID, "SheetNo"));
            string sql = "select wi.workItemName,u.userName,wi.createdTime ";
            sql += "from [NaNa].dbo.WorkItem wi, [NaNa].dbo.ProcessInstance pi, [WebFormPT].[dbo].SMWYAAA ya, [NaNa].dbo.Users u ";
            sql += "where SMWYAAA002='" + sheetNo + "' and wi.contextOID=pi.contextOID and pi.serialNumber=ya.SMWYAAA005 and u.OID=wi.performerOID ";
            sql += "  and wi.workItemName not in ('填表人')  ";
			sql += "union ";
			sql += "select   wi.workItemName,u.userName,wi.createdTime from  [NaNa].dbo.WorkItem wi, [NaNa].dbo.ProcessInstance pi, [WebFormPT].[dbo].SMWYAAA ya , [NaNa].dbo.WorkAssignment wa , [NaNa].dbo.Users u ";
			sql += "where SMWYAAA002='" + sheetNo + "'  and  wi.contextOID=pi.contextOID and  pi.serialNumber=ya.SMWYAAA005 and wa.workItemOID=wi.OID  and u.OID=wa.assigneeOID ";
            sql += "order by wi.createdTime asc";
			
			//sw.WriteLine("getWorkflowOpinion sql => " + sql);
			
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            //bool needGotComment = false;
			//sw.WriteLine("rows QTY => " + rows);
            for (int i = 0; i < rows; i++)
            {
                string actName = ds.Tables[0].Rows[i][0].ToString();
                string userName = ds.Tables[0].Rows[i][1].ToString();
                //string comment = ds.Tables[0].Rows[i][2].ToString();
                
                strOpinion += actName + ":" + userName + " ^ ";
                //sw.WriteLine("strOpinion in  => " + strOpinion);
            }
			//sw.WriteLine("strOpinion => " + strOpinion);
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
		finally
        {
            //sw.Close();
        }
		
		
        return strOpinion;
    }	
	
}