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
using System.Xml;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;
using com.dsc.flow.server;
using com.dsc.kernal.mail;
using smp.pms.utility;
using WebServerProject;

public partial class SmpProgram_System_Form_SPERP012_Form : SmpErpFormPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPERP012"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPERP012.SmpSoaOmFormAgent";
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
		
		//主管
        ManagerGUID.clientEngineType = engineType;
        ManagerGUID.connectDBString = connectString;
		
		//業務
        Reviewer2GUID.clientEngineType = engineType;
        Reviewer2GUID.connectDBString = connectString;
		
		
		//業務主管
		Reviewer3GUID.clientEngineType = engineType;
        Reviewer3GUID.connectDBString = connectString;
		
		//通知MFG
		Reviewer4GUID.clientEngineType = engineType;
        Reviewer4GUID.connectDBString = connectString;
		
		//通知QA
		Reviewer5GUID.clientEngineType = engineType;
        Reviewer5GUID.connectDBString = connectString;
		
        
        KeyId.Display = false;
        SheetNo.Display = false;
        OriginatorId.Display = false;
        ReviewerId.Display = false;
        ApproverId.Display = false;
        IsResolved.Display = false;
		
        //測試資料
        bool isAddNew = base.isNew();
        /*if (isAddNew)
        {
            Random rnd = new Random();
            int i = rnd.Next();
            KeyId.ValueText = Convert.ToString(i);
            objects.setData("Subject", "主旨 - 測試 - " + i);
            objects.setData("TypeCode", "AP");
            OriginatorId.ValueText = "3787";
            ReviewerId.ValueText = "<list><com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO><performers><com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO><ID>4019</ID><participantType><value>HUMAN</value></participantType></com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO></performers><multiUserMode><value>FIREST_GET_FIRST_WIN</value></multiUserMode><name>審核人</name><performType><value>NORMAL</value></performType></com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO></list>";
            objects.setData("HtmlContent", "HTML Conent is here !");
			objects.setData("HtmlContentExt", "HtmlContentExt Conent is here !");
        }*/
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
        ReviewerId.ValueText = objects.getData("ReviewerId");
        ApproverId.ValueText = objects.getData("ApproverId");
        IsResolved.ValueText = objects.getData("IsResolved");
        //內容
		//主管
		string[] userData1 = GetUserGUID(engine, objects.getData("Reviewer1GUID"));
		string ManagersGUID = userData1[0];
        if (!ManagerGUID.Equals(""))
        {
            ManagerGUID.GuidValueText = ManagersGUID; //將值放入人員開窗元件中, 資料庫存放GUID
            ManagerGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
		//業務
		string[] userData2 = GetUserGUID(engine, objects.getData("Reviewer2GUID"));
        string reviewer2GUID = userData2[0];
        if (!reviewer2GUID.Equals(""))
        {
            Reviewer2GUID.GuidValueText = reviewer2GUID; //將值放入人員開窗元件中, 資料庫存放GUID
            Reviewer2GUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
		//業務主管
		string[] userData3 = GetUserGUID(engine, objects.getData("Reviewer3GUID"));
        string reviewer3GUID = userData3[0];
        if (!reviewer3GUID.Equals(""))
        {
            Reviewer3GUID.GuidValueText = reviewer3GUID; //將值放入人員開窗元件中, 資料庫存放GUID
            Reviewer3GUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
		//通知MFG
		string[] userData4 = GetUserGUID(engine, objects.getData("Reviewer4GUID"));
        string reviewer4GUID = userData4[0];
        if (!reviewer4GUID.Equals(""))
        {
            Reviewer4GUID.GuidValueText = reviewer4GUID; //將值放入人員開窗元件中, 資料庫存放GUID
            Reviewer4GUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
		//通知QA
		string[] userData5 = GetUserGUID(engine, objects.getData("Reviewer5GUID"));
        string reviewer5GUID = userData5[0];
        if (!reviewer5GUID.Equals(""))
        {
            Reviewer5GUID.GuidValueText = reviewer5GUID; //將值放入人員開窗元件中, 資料庫存放GUID
            Reviewer5GUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }

		
		
        string htmlContent = objects.getData("HtmlContent");
        if (!htmlContent.Equals(""))
        {
            HtmlContentCode.InnerHtml = htmlContent;
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
        if (isAddNew)
        {
            objects.setData("KeyId", "Soa");
			objects.setData("TypeCode", "Soa");
            objects.setData("OriginatorId", "5164");
            objects.setData("IsResolved", "");
            objects.setData("GUID", IDProcessor.getID(""));
			objects.setData("HtmlContent", "HTML Conent is here !");
			objects.setData("HtmlContentExt", "HtmlContentExt Conent is here !");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
            base.saveData(engine, objects);
        }
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
        //int keyId = Convert.ToInt32(KeyId.ValueText);
        //strErrMsg = base.checkErpData(engine, keyId);
		//base.checkJobQueueService();
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
        string xml = "";
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string creatorId = si.fillerID;
        string originatorId = OriginatorId.ValueText;
        string reviewerId = ReviewerId.ValueText;
        //string[] reviewerIds = reviewerId.Split(';');
		string[] reviewerIds = null;
        string approverId = ApproverId.ValueText;
        if (originatorId.Equals(creatorId))
        {
            originatorId = "";
        }
		
		/*
		List<string> listReviewer = new List<string>();
        List<string> listNotifier = new List<string>();
        XMLProcessor xp = new XMLProcessor(reviewerId, 1);
        XmlNodeList xnl = xp.selectAllNodes("list/com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO");
        foreach (XmlNode performer in xnl)
        {
            string id = performer.SelectSingleNode("performers/com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO/ID").InnerText;
            string participantType = performer.SelectSingleNode("performers/com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO/participantType/value").InnerText;
            string performType = performer.SelectSingleNode("performType/value").InnerText;
            if (participantType.Equals("HUMAN") && performType.Equals("NORMAL"))
            {
                listReviewer.Add(id);
            }
            else if (participantType.Equals("HUMAN") && performType.Equals("NOTICE"))
            {
                listNotifier.Add(id);
            }
        }

        reviewerIds = listReviewer.ToArray();
        string reviewer = "";
        string reviewer2 = "";
        string reviewer3 = "";
        string reviewer4 = "";
		string reviewer5 = "";
		
		
        for (int i = 0; i < reviewerIds.Length; i++)
        {
            if (i < reviewerIds.Length - 1)
            {
                if (i == 0)
                {
                    reviewer = reviewerIds[i];
                }
                else if (i == 1)
                {
                    reviewer2 = reviewerIds[i];
                }
                else if (i == 2)
                {
                    reviewer4 = reviewerIds[i];
                }
                else if (i == 3)
                {
                    reviewer5 = reviewerIds[i];
                }
            }
        }

		//writeLog("reviewer : " + reviewer);
		//writeLog("reviewer2 : " + reviewer2);
		//writeLog("reviewer3 : " + reviewer3);
		//writeLog("approverId : " + approverId);

        string notifier = "";
        for(int i=0; i<listNotifier.ToArray().Length; i++) 
        {
            notifier += listNotifier[i] + ";";
        }
		*/
        xml += "<SPERP012>";
        xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
        xml += "<originator DataType=\"java.lang.String\">" + creatorId + "</originator>";
        xml += "<reviewer DataType=\"java.lang.String\">" + currentObject.getData("Reviewer1GUID") + "</reviewer>";
		xml += "<reviewer2 DataType=\"java.lang.String\">"+ currentObject.getData("Reviewer2GUID") +"</reviewer2>";
		xml += "<reviewer3 DataType=\"java.lang.String\">"+ currentObject.getData("Reviewer3GUID") +"</reviewer3>";
		xml += "<reviewer4 DataType=\"java.lang.String\">"+ currentObject.getData("Reviewer4GUID") +"</reviewer4>";
		xml += "<manager DataType=\"java.lang.String\">" + currentObject.getData("Reviewer1GUID")+ "</manager>";
		xml += "<decMember DataType=\"java.lang.String\">" + currentObject.getData("decMember") + "</decMember>";
		xml += "<reviewer5 DataType=\"java.lang.String\">" + currentObject.getData("Reviewer5GUID") + "</reviewer5>";
		xml += "<reviewer6 DataType=\"java.lang.String\">" + currentObject.getData("Reviewer6GUID") + "</reviewer6>";
		xml += "<reviewer7 DataType=\"java.lang.String\">" + currentObject.getData("Reviewer7GUID") + "</reviewer7>";
		xml += "<reviewer8 DataType=\"java.lang.String\">" + currentObject.getData("Reviewer8GUID") + "</reviewer8>";
		xml += "<decLevel DataType=\"java.lang.String\">" + currentObject.getData("decLevel") + "</decLevel>";
		xml += "<OrgCode DataType=\"java.lang.String\">" + currentObject.getData("OrgCode") + "</OrgCode>";
        xml += "</SPERP012>";
        //writeLog("xml : " + xml);
        //表單代號
        param["SPERP012"] = xml;
        return "SPERP012";
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
    /// 簽核前程序
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
        string xml = "";
       
        xml = addSignXml;
        return xml;
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
        string actName = Convert.ToString(getSession("ACTName"));
		string level = currentObject.getData("decLevel");
		string userId = (string)Session["UserID"];
		string userMgr = "";
		string[] values = null;
		values = getUserGUID(engine, userId);
        values = getUserManagerInfo(engine, values[0]);
		userMgr = values[1];
		writeLog("簽核站點 : " + actName + ", 簽核結果 : " + signProcess + " 此表單簽核層級 : " + level + " userId : "  + userId + " userMgr : " + userMgr);
		
		
        if (result.Equals("同意") || signProcess.Equals("Y"))
        {
			if(userMgr.Equals("1356")){
				
				SysParam sp = new SysParam(engine);
				string mailclass = sp.getParam("MailClass");
				string smtpServer = sp.getParam("SMTP_Server");
				string systemMail = sp.getParam("SystemMail");
				string emailHeader = sp.getParam("EmailHeader");
				MailFactory fac = new MailFactory();
				AbstractMailUtility au = fac.getMailUtility(mailclass.Split(new char[] { '.' })[0], mailclass);
				
				string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
				string OU = currentObject.getData("OrgCode");
				
				values = GetUserGUID(engine,currentObject.getData("OriginatorId"));
				string mailAddress = values[5];
				string userName = values[3];
				string content = "";
				string serialNumber = (string)getSession("FlowGUID");
				string href = "http://192.168.2.226/ECP?runMethod=showReadOnlyForm&processSerialNumber=" + serialNumber;
				string MainSubject = currentObject.getData("Subject");
				string erpNumber = MainSubject.Substring(MainSubject.LastIndexOf("-")+1,13);
				string subject = "列印通知:銷貨異常說明書 OU:" + OU + ",Issue Number:" + erpNumber ;
				content += subject + "<br />";
				content += "OU : " + OU + "<br />";
				content += "ERP單號 : " + erpNumber + "<br />";
				content += "EasyFlow單號 : " + sheetNo + "<br />";
				content += "EasyFlow表單資訊 : <a href='" + href + "'>表單連結</a><br />";
				content += "請列印紙本提供董事長簽核<br />";
				
				writeLog("sheetNo : " + sheetNo + ", subject : " + subject + " mailAddress : " + mailAddress + " userName : "  + userName + " content : " + content + " href " + href);
				
				au.sendMailHTML("", smtpServer, mailAddress, "", systemMail, subject, content);
			
			}
			
			
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
            AbstractEngine engine = null;
            try
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);
                int keyId = Convert.ToInt32(KeyId.ValueText);
                string serialNumber = (string)getSession("FlowGUID");
                base.terminateThisProcess();
                string note = base.getWorkflowOpinion(engine, serialNumber);
				update_om(engine, keyId, "REJECT", note);
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
    /// 結案程序
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
		string returnResult = "";
        try
        {
            if (result.Equals("Y"))
            {
                string serialNumber = (string)getSession("FlowGUID");
                string note = base.getWorkflowOpinion(engine, serialNumber);
                returnResult = update_om(engine, Convert.ToInt32(currentObject.getData("KeyId")), "APPROVE", note);
				writeLog("KeyID : " + Convert.ToInt32(currentObject.getData("KeyId")));
				writeLog("note : " + note);
				writeLog("returnResult : " + returnResult);
            }
        }
        catch (Exception e)
        {
            writeLog("afterApprove catch exception : " + e);
            throw new Exception(e.Message);
        }

        base.afterApprove(engine, currentObject, result);
    }
	
	private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPERP012.log", true, System.Text.Encoding.Default);
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
	
	private string[] GetUserGUID(AbstractEngine engine, string userId)
    {
		string sql = "select empGUID,empNumber, empName, empEName, titleName, empEmail,  orgId from EmployeeInfo where empNumber='" + Utility.filter(userId) + "' ";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[9];
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();
            result[1] = ds.Tables[0].Rows[0][1].ToString();
            result[2] = ds.Tables[0].Rows[0][2].ToString();
            result[3] = ds.Tables[0].Rows[0][3].ToString();
            result[4] = ds.Tables[0].Rows[0][4].ToString();
            result[5] = ds.Tables[0].Rows[0][5].ToString();
        }
        else
        {
            result[0] = "";
            result[1] = "";
            result[2] = "";
            result[3] = "";
            result[4] = "";
            result[5] = "";
        }
        return result;
	}
	private string[] getUserManagerInfo(AbstractEngine engine, string userGUID)
    {
        string[] result = new string[3];
        string sql = "select top 1 u.OID, u.id, u.userName from Functions f, Users u where f.occupantOID = '" + Utility.filter(userGUID) + "' and f.specifiedManagerOID = u.OID";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();
            result[1] = ds.Tables[0].Rows[0][1].ToString();
            result[2] = ds.Tables[0].Rows[0][2].ToString();
        }
        else
        {
            result[0] = "";
            result[1] = "";
            result[2] = "";
        }
        return result;
    }
	
	
}