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

public partial class SmpProgram_System_Form_SPERP009_Form : SmpErpPoFormPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPERP009"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPERP009.SmpApInvBatchFormAgent";
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

        KeyId.Display = false;
        SheetNo.Display = false;
        OriginatorId.Display = false;
        ReviewerId.Display = false;
        ApproverId.Display = false;
        IsResolved.Display = false;

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
            ReviewerId.ValueText = "<list><com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO><performers><com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO><ID>4019</ID><participantType><value>HUMAN</value></participantType></com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO></performers><multiUserMode><value>FIREST_GET_FIRST_WIN</value></multiUserMode><name>審核人</name><performType><value>NORMAL</value></performType></com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO></list>";
            ApproverId.ValueText = "3992";
            objects.setData("HtmlContent", "HTML Conent is here !");
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
        ReviewerId.ValueText = objects.getData("ReviewerId");
        ApproverId.ValueText = objects.getData("ApproverId");
        IsResolved.ValueText = objects.getData("IsResolved");
        //內容
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
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("KeyId", KeyId.ValueText);
            objects.setData("OriginatorId", OriginatorId.ValueText);
            objects.setData("ReviewerId", ReviewerId.ValueText);
            objects.setData("ApproverId", ApproverId.ValueText);
            objects.setData("IsResolved", IsResolved.ValueText);
            objects.setData("GUID", IDProcessor.getID(""));
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
        int keyId = Convert.ToInt32(KeyId.ValueText);
        //strErrMsg = base.checkErpData(engine, keyId);
		base.checkJobQueueService();
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
		writeLog(" approverId : "  +approverId)
        if (originatorId.Equals(creatorId))
        {
            originatorId = "";
        }
		
		
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
		
		//writeLog("setFlowVariables");
		
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
                    reviewer3 = reviewerIds[i];
                }
                else if (i == 3)
                {
                    reviewer4 = reviewerIds[i];
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

        xml += "<SPERP009>";
        xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
        xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
        xml += "<reviewer DataType=\"java.lang.String\">" + reviewer + "</reviewer>";
		xml += "<reviewer2 DataType=\"java.lang.String\">" + reviewer2 + "</reviewer2>";
		xml += "<reviewer3 DataType=\"java.lang.String\">" + reviewer3 + "</reviewer3>";
		xml += "<reviewer4 DataType=\"java.lang.String\">" + reviewer4 + "</reviewer4>";
		xml += "<manager DataType=\"java.lang.String\">" + "3992" + "</manager>";
		xml += "<approver DataType=\"java.lang.String\">" + approverId + "</approver>";        		
        xml += "</SPERP009>";
        
        //表單代號
        param["SPERP009"] = xml;
        return "SPERP009";
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
        //string actName = Convert.ToString(getSession("ACTName").ToString());
		/*
        if (!IsResolved.ValueText.Equals("Y"))
        {
            string reviewerId = ReviewerId.ValueText;
            bool gotReviewer = false;
            XMLProcessor xp = new XMLProcessor(reviewerId, 1);
            XmlNodeList xnl = xp.selectAllNodes("list/com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO");
            List<Dictionary<string, string>> performers = new List<Dictionary<string, string>>();
            foreach (XmlNode performer in xnl)
            {
                string id = performer.SelectSingleNode("performers/com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO/ID").InnerText;
                string participantType = performer.SelectSingleNode("performers/com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO/participantType/value").InnerText;
                string multiUserMode = performer.SelectSingleNode("multiUserMode/value").InnerText;
                string name = performer.SelectSingleNode("name").InnerText;
                string performType = performer.SelectSingleNode("performType/value").InnerText;

                if (gotReviewer == false)
                {
                    if (participantType.Equals("HUMAN") && performType.Equals("NORMAL"))
                    {
                        //第1個審核人已由WebService傳入, 不再進行加簽
                        gotReviewer = true;
                        continue;
                    }
                }
                Dictionary<string, string> performerData = new Dictionary<string, string>();
                performerData.Add("id", id);
                performerData.Add("participantType", participantType);
                performerData.Add("multiUserMode", multiUserMode);
                performerData.Add("name", name);
                performerData.Add("performType", performType);
                performers.Add(performerData);
            }

            int length = performers.ToArray().Length;
            if (length > 0)
            {
                bool gotDicision = false;
                for (int i = 1; i <= length; i++)
                {
                    Dictionary<string, string> performerData = performers[length - i];
                    string id = performerData["id"];
                    string[] values = base.getUserGUID(engine, id);
                    string oid = values[0];
                    string participantType = "HUMAN";
                    string multiUserMode = "FIREST_GET_FIRST_WIN";
                    string performType = "NORMAL";
                    string stateName = "審核人" + i;
                    string performerXml = "";

                    if (!performerData["participantType"].Equals(""))
                    {
                        participantType = performerData["participantType"];
                    }
                    if (!performerData["multiUserMode"].Equals(""))
                    {
                        multiUserMode = performerData["multiUserMode"];
                    }
                    if (!performerData["name"].Equals(""))
                    {
                        stateName = performerData["name"];
                    }
                    if (!performerData["performType"].Equals(""))
                    {
                        performType = performerData["performType"];
                    }

                    if (gotDicision == false)
                    {
                        if (participantType.Equals("HUMAN") && performType.Equals("NORMAL"))
                        {
                            stateName = "核准人";
                            gotDicision = true;
                        }
                    }

                    performerXml += "    <com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                    performerXml += "        <performers>";
                    performerXml += "            <com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                    performerXml += "                <OID>" + oid + "</OID>";
                    performerXml += "                <participantType><value>" + participantType + "</value></participantType>";
                    performerXml += "            </com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                    performerXml += "        </performers>";
                    performerXml += "        <multiUserMode><value>" + multiUserMode + "</value></multiUserMode>";
                    performerXml += "        <name>" + stateName + "</name>";
                    performerXml += "        <performType><value>" + performType + "</value></performType>";
                    performerXml += "    </com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                    xml = performerXml + xml;
                }
                xml = "<list>" + xml + "</list>";
            }
            DataObject currentObject = (DataObject)getSession("currentObject");
            currentObject.setData("IsResolved", "Y");
            base.saveDB(engine, currentObject, null, (string)getSession("UIStatus"));
        }
        else
        {
            xml = addSignXml;
        }
		*/
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
        if (result.Equals("同意") || signProcess.Equals("Y"))
        {
			/*
            bool needCreateJob = false;
			string userId = (string)Session["UserID"];
            string approverId = ApproverId.ValueText;

            //審核人為多人
			if (actName.IndexOf("核准人") != -1 || userId.Equals(approverId) || actName.Equals("系統關卡"))
            //if (actName.IndexOf("核准人") != -1 || userId.Equals(approverId))
            {
                needCreateJob = true;
            }
            //審核人只有一人
            //if (!needCreateJob)
            //{
            //    string reviewerId = currentObject.getData("ReviewerId");
            //    string[] reviewerIds = reviewerId.Split(';');
            //    if (reviewerIds.Length == 1)
            //    {
            //        needCreateJob = true;
            //    }
            //}

            if (needCreateJob)
            {
                string serialNumber = (string)getSession("FlowGUID");
                int keyId = Convert.ToInt32(currentObject.getData("KeyId"));
                base.createJob(engine, serialNumber, keyId);
            }
			*/
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
                responseNotification(engine, keyId, "REJECT", note);
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
        try
        {
            if (result.Equals("Y"))
            {
                //string serialNumber = (string)getSession("FlowGUID");
                //string note = base.getWorkflowOpinion(engine, serialNumber);
                //responseNotification(engine, Convert.ToInt32(currentObject.getData("KeyId")), "APPROVE", note);
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
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
            sw = new System.IO.StreamWriter(serverPath + @"\SPERP009.log", true, System.Text.Encoding.Default);
            //sw.WriteLine(line);
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