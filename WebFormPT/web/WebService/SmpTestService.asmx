<%@ WebService Language="C#" Class="SmpTestService" %>

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
//using System.Data.OracleClient;
//using System.Xml.Linq;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.kernal.webservice;
using Oracle.DataAccess.Client;
using System.Xml;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class SmpTestService  : System.Web.Services.WebService {

    [WebMethod]
    public string getFileList(int id)
    {
        string xml = "";
        StringBuilder sb = new StringBuilder();
        sb.Append("<Attachments>");
	    sb.Append("<EntityName>REQ_HEADERS</EntityName>");
	    sb.Append("<Pk1Value>270234</Pk1Value>");
	    sb.Append("<Modify>N</Modify>");
	    sb.Append("<Title>Requisition Number-10120002955 Attachment</Title>");
	    sb.Append("<Attachment>");
		sb.Append("<Sequence>10<Sequence>");
		sb.Append("<FileName>新增文字文件.txt</FileName>");
		sb.Append("<FileDescription>新增文字文件.txt</FileDescription>");
		sb.Append("<FileExt>txt</FileExt>");
		sb.Append("<FileUrl>http://smpeprtl.simplo.com.tw:8080/GpmAvl/Download?encryptId=0F5698ACFF78CC78</FileUrl>");
		sb.Append("<AttachmentType>零件主要尺寸CPK調查數據</AttachmentType>");
		sb.Append("<AttachmentCategory>Miscellaneous</AttachmentCategory>");
		sb.Append("<UploadDate>2012/01/03 16:30:04</UploadDate>");
	    sb.Append("</Attachment>");
	    sb.Append("<Attachment>");
		sb.Append("<Sequence>20<Sequence>");
		sb.Append("<FileName>COVER PAGE.txt</FileName>");
		sb.Append("<FileDescription>COVER PAGE.txt</FileDescription>");
		sb.Append("<FileExt>txt</FileExt>");
		sb.Append("<FileUrl>http://smpeprtl.simplo.com.tw:8080/GpmAvl/Download?encryptId=ECE8BC8BAF14A103</FileUrl>");
		sb.Append("<AttachmentType>承認書封面</AttachmentType>");
		sb.Append("<AttachmentCategory>Miscellaneous</AttachmentCategory>");
		sb.Append("<UploadDate>2012/01/03 16:30:05</UploadDate>");
	    sb.Append("</Attachment>");
	    sb.Append("<Attachment>");
        sb.Append("<Sequence>30<Sequence>");
		sb.Append("<FileName>新增Microsoft Excel 工作表.xls</FileName>");
		sb.Append("<FileDescription>新增Microsoft Excel 工作表.xls</FileDescription>");
		sb.Append("<FileExt>xls</FileExt>");
		sb.Append("<FileUrl>http://smpeprtl.simplo.com.tw:8080/GpmAvl/Download?encryptId=74A27C588BDF2618</FileUrl>");
		sb.Append("<AttachmentType>環境可靠度測試報告</AttachmentType>");
		sb.Append("<AttachmentCategory>Miscellaneous</AttachmentCategory>");
		sb.Append("<UploadDate>2012/01/03 16:30:05</UploadDate>");
	    sb.Append("</Attachment>");
	    sb.Append("<Attachment>");
		sb.Append("<Sequence>40<Sequence>");
		sb.Append("<FileName>11111.DOC</FileName>");
		sb.Append("<FileDescription>11111.DOC</FileDescription>");
		sb.Append("<FileExt>doc</FileExt>");
		sb.Append("<FileUrl>http://smpeprtl.simplo.com.tw:8080/GpmAvl/Download?encryptId=3ABA33C617517E7F</FileUrl>");
		sb.Append("<AttachmentType>原廠產品規格書</AttachmentType>");
		sb.Append("<AttachmentCategory>Miscellaneous</AttachmentCategory>");
		sb.Append("<UploadDate>2012/01/03 16:30:05</UploadDate>");
	    sb.Append("</Attachment>");
        sb.Append("</Attachments>");
        xml = sb.ToString();
        return xml;
    }

    [WebMethod]
    public string createTestFlow(string typeCode, int frequency)
    {
        string result = "";
        string password = "SmpEcp2013";
        string keyId = frequency + "";
        string creatorId = "4019";
        string reviewerId = "<list>	<com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>		<performers>			<com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>				<ID>3787</ID>				<participantType>					<value>HUMAN</value>				</participantType>			</com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>		</performers>		<multiUserMode>			<value>FIREST_GET_FIRST_WIN</value>		</multiUserMode>		<name>審核人</name>			<performType>				<value>NORMAL</value>			</performType>	</com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>	<com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>		<performers>			<com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>				<ID>4019</ID>				<participantType>					<value>HUMAN</value>				</participantType>			</com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>		</performers>		<multiUserMode>			<value>FIREST_GET_FIRST_WIN</value>		</multiUserMode>		<name>審核人</name>			<performType>				<value>NORMAL</value>			</performType>	</com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>	<com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>		<performers>			<com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>				<ID>3992</ID>				<participantType>					<value>HUMAN</value>				</participantType>			</com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>		</performers>		<multiUserMode>			<value>FIREST_GET_FIRST_WIN</value>		</multiUserMode>		<name>核准人</name>			<performType>				<value>NORMAL</value>			</performType>	</com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO></list>";
        string subject = "subject-" + frequency;
        string htmlContent = "htmlContent";
        string htmlContentExt = "htmlContentExt";
        if (typeCode.Equals("BP"))
        {
            for (int i = 0; i < frequency; i++)
            {
                createMtlBpFlow(password, keyId, typeCode, creatorId, reviewerId, subject, htmlContent, htmlContentExt);
            }
        }
        return result;
    }
    
    
    public string createMtlBpFlow(string password, string keyId, string typeCode, string creatorId, string reviewerId, string subject, string htmlContent, string htmlContentExt)
    {
        string result = "";
        string servicePwd = GlobalProperty.getProperty("simplo", "ServicePwd");
        string errMsg = "";
        if (password.Equals(servicePwd))
        {
            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            IOFactory factory = new IOFactory();
            AbstractEngine engine = null;
            NLAgent agent = new NLAgent();
            string agentSchema = "WebServerProject.form.SPERP008.SmpBlanketPoFormAgent";
            string orgUnitId = "";
            string ownerId = "";
            string ownerOrgId = "";
            string sql = "";
            object ret = null;
            string[] values = null;
            DataObject dataObject = null;
            XMLProcessor xp = null;
            XmlNode node = null;
            
            try
            {
                agent.loadSchema(agentSchema);
                engine = factory.getEngine(acs.engineType, acs.connectString);

                xp = new XMLProcessor(reviewerId, 1);
                XmlNodeList xnl = xp.selectAllNodes("list/com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO");
                List<string> listReviewer = new List<string>();
                foreach (XmlNode performer in xnl)
                {
                    string id = performer.SelectSingleNode("performers/com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO/ID").InnerText;
                    string participantType = performer.SelectSingleNode("performers/com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO/participantType/value").InnerText;
                    string performType = performer.SelectSingleNode("performType/value").InnerText;
                    if (participantType.Equals("HUMAN") && performType.Equals("NORMAL"))
                    {
                        listReviewer.Add(id);
                    }
                }

                string[] reviewerIds = listReviewer.ToArray();
                string reviewer = "";
                string reviewer2 = "";
                string reviewer3 = "";
                string reviewer4 = "";
                string approverId = reviewerIds[reviewerIds.Length - 1].Trim();
                for (int i = 0; i < reviewerIds.Length; i++)
                {
                    values = getDeptInfo(engine, reviewerIds[i]);
                    if (values[0].Equals(""))
                    {
                        errMsg += reviewerIds[i] + " 工號不存在系統，請選擇正確的人員或洽系統管理員確認!";
                    }
                    else
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
                }

                if (errMsg.Equals(""))
                {
                    string dataXml = null;
                    string flowVarXml = ""; //流程參數
                    string webUrl = null;
                    string serviceUrl = null;
                    WSDLClient ws = null;
                    //填表人
                    values = getDeptInfo(engine, creatorId);
                    orgUnitId = values[3];
                    //關系人
                    //ownerId = reviewerIds[0].Trim();
                    //values = getDeptInfo(engine, ownerId);
                    //ownerOrgId = values[3];
                    ownerId = creatorId;
                    ownerOrgId = orgUnitId;

                    

                    //表單資料
                    agent.engine = engine;
                    agent.query("1=2");
                    dataObject = agent.defaultData.create();
                    dataObject.setData("GUID", IDProcessor.getID(""));
                    dataObject.setData("IS_LOCK", "N");
                    dataObject.setData("IS_DISPLAY", "Y");
                    dataObject.setData("DATA_STATUS", "Y");
                    dataObject.setData("KeyId", keyId);
                    dataObject.setData("TypeCode", typeCode);
                    dataObject.setData("Subject", subject);
                    dataObject.setData("SheetNo", "");
                    dataObject.setData("OriginatorId", ownerId);
                    dataObject.setData("ReviewerId", reviewerId);
                    dataObject.setData("ApproverId", approverId);
                    dataObject.setData("HtmlContent", htmlContent);
                    dataObject.setData("HtmlContentExt", htmlContentExt);
                    dataObject.setData("SetOfBookName", "");

                    dataXml = dataObject.saveXML();

                    flowVarXml += "<SPERP008>";
                    flowVarXml += "<SPERP008>";
                    flowVarXml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
                    flowVarXml += "<originator DataType=\"java.lang.String\"></originator>";
                    flowVarXml += "<reviewer DataType=\"java.lang.String\">" + reviewer + "</reviewer>";
                    flowVarXml += "<reviewer2 DataType=\"java.lang.String\">" + reviewer2 + "</reviewer2>";
                    flowVarXml += "<reviewer3 DataType=\"java.lang.String\">" + reviewer3 + "</reviewer3>";
                    flowVarXml += "<reviewer4 DataType=\"java.lang.String\">" + reviewer4 + "</reviewer4>";
                    flowVarXml += "<manager DataType=\"java.lang.String\"></manager>";
                    flowVarXml += "<Approver DataType=\"java.lang.String\">" + approverId + "</Approver>";
                    flowVarXml += "<notifier DataType=\"java.lang.String\"></notifier>";
                    flowVarXml += "</SPERP008>";
                    flowVarXml += "</SPERP008>";

                    //建立標準Service
                    webUrl = GlobalProperty.getProperty("simplo", "EcpWebUrl");
                    serviceUrl = webUrl + "/WebService/DSCFlowService.asmx";
                    ws = new WSDLClient(serviceUrl);
                    ws.dllPath = Utility.G_GetTempPath();
                    ws.build(true);

                    ret = ws.callSync("createFlow", //呼叫方法
                                        "SMPFORM", //ApplicationID
                                        "SPERP", //ModuleID
                                        "SPERP008", //ProcessPageID
                                        "", //sheetNo
                                        "<FORMID>SPERP008</FORMID>", //sheetnoparam
                                        subject, //subject
                                        creatorId, //"userID">使用者代號
                                        creatorId, //fillerID">填表人代號
                                        orgUnitId, //fillerOrgID">填表人單位代號
                                        ownerId, //ownerID">關係人代號
                                        ownerOrgId, //ownerOrgID">關係人單位代號
                                        ownerOrgId, //submitOrgID">發起單位
                                        "1", //"importance">重要性
                                        agentSchema, //AgentSchema
                                        dataXml, //dataXML
                                        flowVarXml, //flowParameter
                                        "SPERP008", //firstParam
                                        "", //addSignXML
                                        "",  //localeString
                                        true
                                    );
                    if (ret == null || Convert.ToString(ret).Equals(""))
                    {
                        result = "<ReturnValue><Result>N</Result><Message>createFlow return null.</Message><StackTrace></StackTrace></ReturnValue>";
                    }
                    else
                    {
                        result = ret.ToString(); //回傳xml, Result, FlowOID, SheetNo
                        try
                        {
                            xp = new XMLProcessor(result, 1);
                            node = xp.selectSingleNode("ReturnValue");
                            string flag = node["Result"].InnerText;
                            if (flag.Equals("Y"))
                            {
                                string wfnum = node["SheetNo"].InnerText;
                                string wfoid = node["FlowOID"].InnerText;
                                //更新表單單號
                                subject = subject + "(" + wfnum + ")";
                                sql = "UPDATE SmpBlanketPoForm SET SheetNo = '" + wfnum + "', Subject='" + subject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
                                engine.executeSQL(sql);
                                //更新系統表單主旨
                                sql = "UPDATE SMWYAAA SET SMWYAAA006='" + subject + "' WHERE SMWYAAA005='" + wfoid + "'";
                                engine.executeSQL(sql);
                                //更新GP主旨
                                sql = "update NaNa.dbo.ProcessInstance set subject = '" + subject + "' where serialNumber = '" + wfoid + "'";
                                engine.executeSQL(sql);
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                else
                {
                    result = "<ReturnValue><Result>N</Result><Message>" + errMsg + "</Message><StackTrace></StackTrace></ReturnValue>";
                }
            }
            catch (Exception e)
            {
                result = "<ReturnValue><Result>N</Result><Message>" + e.Message + "</Message><StackTrace>" + e.StackTrace + "</StackTrace></ReturnValue>";
            }
            finally
            {
                if (engine != null)
                {
                    engine.close();
                }
            }
        }
        else
        {
            result = "<ReturnValue><Result>N</Result><Message>parameter [password] value is not correct.</Message></ReturnValue>";
        }
        return result;
    }
	
    public string createApInvBatchFlow(string password, string keyId, string typeCode, string creatorId, string reviewerId, string subject, string htmlContent, string htmlContentExt)
    {
        string result = "";
        string servicePwd = GlobalProperty.getProperty("simplo", "ServicePwd");
        string errMsg = "";
		if (password.Equals(servicePwd))
        {
            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            IOFactory factory = new IOFactory();
            AbstractEngine engine = null;
            NLAgent agent = new NLAgent();
            string agentSchema = "WebServerProject.form.SPERP009.SmpApInvBatchFormAgent";
            string approverId = "";
            string orgUnitId = "";
            string ownerId = "";
            string ownerOrgId = "";
            string sql = "";
            object ret = null;
			List<string> listReviewer = new List<string>();
            string[] reviewerIds = null;
            string[] values = null;
            DataObject dataObject = null;
            //DataSet ds = null;
			XMLProcessor xp = null;
            XmlNode node = null;
			
			try
            {
                agent.loadSchema(agentSchema);
                engine = factory.getEngine(acs.engineType, acs.connectString);
												
				xp = new XMLProcessor(reviewerId, 1);
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
                }
					
				reviewerIds = listReviewer.ToArray();
                for (int i = 0; i < reviewerIds.Length; i++)
                {
                    values = getUserGUID(engine, reviewerIds[i]);
                    if (values[0].Equals(""))
                    {
                        errMsg += reviewerIds[i] + " 工號不存在系統，請選擇正確的人員或洽系統管理員確認!";
                    }
                }

                if (errMsg.Equals(""))
                {
                    string dataXml = null;
                    string flowVarXml = ""; //流程參數
                    string webUrl = null;
                    string serviceUrl = null;
                    WSDLClient ws = null;
					
                    //填表人
                    values = getDeptInfo(engine, creatorId);
                    orgUnitId = values[3];
                    //關系人
                    //ownerId = reviewerIds[0].Trim();
                    //values = getDeptInfo(engine, ownerId);
                    //ownerOrgId = values[3];
                    ownerId = creatorId;
                    ownerOrgId = orgUnitId;

                    approverId = reviewerIds[reviewerIds.Length - 1].Trim();
                    
                    //表單資料
                    agent.engine = engine;
                    agent.query("1=2");
                    dataObject = agent.defaultData.create();
                    dataObject.setData("GUID", IDProcessor.getID(""));
                    dataObject.setData("IS_LOCK", "N");
                    dataObject.setData("IS_DISPLAY", "Y");
                    dataObject.setData("DATA_STATUS", "Y");
                    dataObject.setData("KeyId", keyId);
                    dataObject.setData("TypeCode", typeCode);
                    dataObject.setData("Subject", subject);
                    dataObject.setData("SheetNo", "");
                    dataObject.setData("OriginatorId", ownerId);
                    dataObject.setData("ReviewerId", reviewerId);
                    dataObject.setData("ApproverId", approverId);
                    dataObject.setData("HtmlContent", htmlContent);
                    dataObject.setData("HtmlContentExt", htmlContentExt);
                    dataObject.setData("SetOfBookName", "");
                    
                    dataXml = dataObject.saveXML();

                    flowVarXml += "<SPERP009>";
                    flowVarXml += "<SPERP009>";
                    flowVarXml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
                    flowVarXml += "<originator DataType=\"java.lang.String\"></originator>";
                    flowVarXml += "<reviewer DataType=\"java.lang.String\">" + reviewerIds[0] + "</reviewer>";
                    flowVarXml += "<manager DataType=\"java.lang.String\"></manager>";
                    flowVarXml += "</SPERP009>";
                    flowVarXml += "</SPERP009>";
					
                    //建立標準Service
                    webUrl = GlobalProperty.getProperty("simplo", "EcpWebUrl");
                    serviceUrl = webUrl + "/WebService/DSCFlowService.asmx";
                    ws = new WSDLClient(serviceUrl);
                    ws.dllPath = Utility.G_GetTempPath();
                    ws.build(true);

                    ret = ws.callSync("createFlow", //呼叫方法
                                        "SMPFORM", //ApplicationID
                                        "SPERP", //ModuleID
                                        "SPERP009", //ProcessPageID
                                        "", //sheetNo
                                        "<FORMID>SPERP009</FORMID>", //sheetnoparam
                                        subject, //subject
                                        creatorId, //"userID">使用者代號
                                        creatorId, //fillerID">填表人代號
                                        orgUnitId, //fillerOrgID">填表人單位代號
                                        ownerId, //ownerID">關係人代號
                                        ownerOrgId, //ownerOrgID">關係人單位代號
                                        ownerOrgId, //submitOrgID">發起單位
                                        "1", //"importance">重要性
                                        agentSchema, //AgentSchema
                                        dataXml, //dataXML
                                        flowVarXml, //flowParameter
                                        "SPERP009", //firstParam
                                        "", //addSignXML
                                        "",  //localeString
                                        true
                                    );
                    if (ret == null || Convert.ToString(ret).Equals(""))
                    {
                        result = "<ReturnValue><Result>N</Result><Message>createFlow return null.</Message><StackTrace></StackTrace></ReturnValue>";
                    }
                    else
                    {
                        result = ret.ToString(); //回傳xml, Result, FlowOID, SheetNo
						try
                        {
							xp = new XMLProcessor(result, 1);
                            node = xp.selectSingleNode("ReturnValue");
                            string flag = node["Result"].InnerText;
                            if (flag.Equals("Y"))
                            {
                                string wfnum = node["SheetNo"].InnerText;
                                string wfoid = node["FlowOID"].InnerText;
                                //更新表單單號
                                subject = subject + "(" + wfnum + ")";
                                sql = "UPDATE SmpApInvBatchForm SET SheetNo = '" + wfnum + "', Subject='" + subject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
                                engine.executeSQL(sql);
                                //更新系統表單主旨
                                sql = "UPDATE SMWYAAA SET SMWYAAA006='" + subject + "' WHERE SMWYAAA005='" + wfoid + "'";
                                engine.executeSQL(sql);
                                //更新GP主旨
                                sql = "update NaNa.dbo.ProcessInstance set subject = '" + subject + "' where serialNumber = '" + wfoid + "'";
                                engine.executeSQL(sql);
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                else
                {
                    result = "<ReturnValue><Result>N</Result><Message>" + errMsg + "</Message><StackTrace></StackTrace></ReturnValue>";
                }
            }
            catch (Exception e)
            {
                result = "<ReturnValue><Result>N</Result><Message>" + e.Message + "</Message><StackTrace>" + e.StackTrace + "</StackTrace></ReturnValue>";
            }
            finally
            {
                if (engine != null)
                {
                    engine.close();
                }
            }
        }
        else
        {
            result = "<ReturnValue><Result>N</Result><Message>parameter [password] value is not correct.</Message></ReturnValue>";
        }
        return result;
    }

    public string createMtlPoFlow(string password, string keyId, string typeCode, string creatorId, string reviewerId, string subject, string htmlContent, string htmlContentExt)
    {
        string result = "";
        string servicePwd = GlobalProperty.getProperty("simplo", "ServicePwd");
        string errMsg = "";
        if (password.Equals(servicePwd))
        {
            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            IOFactory factory = new IOFactory();
            AbstractEngine engine = null;
            NLAgent agent = new NLAgent();
            string agentSchema = "WebServerProject.form.SPERP007.SmpStandardPoFormAgent";
            //string approverId = "";
            string orgUnitId = "";
            string ownerId = "";
            string ownerOrgId = "";
            string sql = "";
            object ret = null;
			List<string> listReviewer = new List<string>();
            //string[] reviewerIds = null;
            string[] values = null;
            DataObject dataObject = null;
            //DataSet ds = null;
			XMLProcessor xp = null;
            XmlNode node = null;

            try
            {
                agent.loadSchema(agentSchema);
                engine = factory.getEngine(acs.engineType, acs.connectString);
				
				values = getUserGUID(engine, creatorId);
                if (values[0].Equals(""))
                {
                    errMsg += creatorId + " 工號不存在系統，請選擇正確的人員或洽系統管理員確認!";
                }
				
				xp = new XMLProcessor(reviewerId, 1);
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
                }
				string[] reviewerIds = listReviewer.ToArray();
                string reviewer = "";
                string reviewer2 = "";
                string reviewer3 = "";
                string reviewer4 = "";
                string approverId = reviewerIds[reviewerIds.Length - 1].Trim();
                for (int i = 0; i < reviewerIds.Length; i++)
                {
                    values = getDeptInfo(engine, reviewerIds[i]);
                    if (values[0].Equals(""))
                    {
                        errMsg += reviewerIds[i] + " 工號不存在系統，請選擇正確的人員或洽系統管理員確認!";
                    }
                    else
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
                }

                if (errMsg.Equals(""))
                {
                    string dataXml = null;
                    string flowVarXml = ""; //流程參數
                    string webUrl = null;
                    string serviceUrl = null;
                    WSDLClient ws = null;
                    //填表人
                    values = getDeptInfo(engine, creatorId);
                    orgUnitId = values[3];
                    //關系人
                    //ownerId = reviewerIds[0].Trim();
                    //values = getDeptInfo(engine, ownerId);
                    //ownerOrgId = values[3];
                    ownerId = creatorId;
                    ownerOrgId = orgUnitId;

                    approverId = reviewerIds[reviewerIds.Length - 1].Trim();

                    //表單資料
                    agent.engine = engine;
                    agent.query("1=2");
                    dataObject = agent.defaultData.create();
                    dataObject.setData("GUID", IDProcessor.getID(""));
                    dataObject.setData("IS_LOCK", "N");
                    dataObject.setData("IS_DISPLAY", "Y");
                    dataObject.setData("DATA_STATUS", "Y");
                    dataObject.setData("KeyId", keyId);
                    dataObject.setData("TypeCode", typeCode);
                    dataObject.setData("Subject", subject);
                    dataObject.setData("SheetNo", "");
                    dataObject.setData("OriginatorId", ownerId);
                    dataObject.setData("ReviewerId", reviewerId);
                    dataObject.setData("ApproverId", approverId);
                    dataObject.setData("HtmlContent", htmlContent);
                    dataObject.setData("HtmlContentExt", htmlContentExt);
                    dataObject.setData("SetOfBookName", "");

                    dataXml = dataObject.saveXML();

                    flowVarXml += "<SPERP007>";
                    flowVarXml += "<SPERP007>";
                    flowVarXml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
                    flowVarXml += "<originator DataType=\"java.lang.String\"></originator>";
					flowVarXml += "<reviewer DataType=\"java.lang.String\">" + reviewer + "</reviewer>";
                    flowVarXml += "<reviewer2 DataType=\"java.lang.String\">" + reviewer2 + "</reviewer2>";
                    flowVarXml += "<reviewer3 DataType=\"java.lang.String\">" + reviewer3 + "</reviewer3>";
                    flowVarXml += "<reviewer4 DataType=\"java.lang.String\">" + reviewer4 + "</reviewer4>";
                    flowVarXml += "<manager DataType=\"java.lang.String\"></manager>";
                    flowVarXml += "<approver DataType=\"java.lang.String\">" + approverId + "</approver>";
                    flowVarXml += "</SPERP007>";
                    flowVarXml += "</SPERP007>";

                    //建立標準Service
                    webUrl = GlobalProperty.getProperty("simplo", "EcpWebUrl");
                    serviceUrl = webUrl + "/WebService/DSCFlowService.asmx";
                    ws = new WSDLClient(serviceUrl);
                    ws.dllPath = Utility.G_GetTempPath();
                    ws.build(true);

                    ret = ws.callSync("createFlow", //呼叫方法
                                        "SMPFORM", //ApplicationID
                                        "SPERP", //ModuleID
                                        "SPERP007", //ProcessPageID
                                        "", //sheetNo
                                        "<FORMID>SPERP007</FORMID>", //sheetnoparam
                                        subject, //subject
                                        creatorId, //"userID">使用者代號
                                        creatorId, //fillerID">填表人代號
                                        orgUnitId, //fillerOrgID">填表人單位代號
                                        ownerId, //ownerID">關係人代號
                                        ownerOrgId, //ownerOrgID">關係人單位代號
                                        ownerOrgId, //submitOrgID">發起單位
                                        "1", //"importance">重要性
                                        agentSchema, //AgentSchema
                                        dataXml, //dataXML
                                        flowVarXml, //flowParameter
                                        "SPERP007", //firstParam
                                        "", //addSignXML
                                        "",  //localeString
                                        true
                                    );
                    if (ret == null || Convert.ToString(ret).Equals(""))
                    {
                        result = "<ReturnValue><Result>N</Result><Message>createFlow return null.</Message><StackTrace></StackTrace></ReturnValue>";
                    }
                    else
                    {
                        result = ret.ToString(); //回傳xml, Result, FlowOID, SheetNo
                        try
                        {
							xp = new XMLProcessor(result, 1);
                            node = xp.selectSingleNode("ReturnValue");
                            //XMLProcessor doc = new XMLProcessor(result, 1);
                            //XmlNode node = doc.selectSingleNode("ReturnValue");
                            string flag = node["Result"].InnerText;
                            if (flag.Equals("Y"))
                            {
                                string wfnum = node["SheetNo"].InnerText;
                                string wfoid = node["FlowOID"].InnerText;
                                //更新表單單號
                                subject = subject + "(" + wfnum + ")";
                                sql = "UPDATE SmpStandardPoForm SET SheetNo = '" + wfnum + "', Subject='" + subject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
                                engine.executeSQL(sql);
                                //更新系統表單主旨
                                sql = "UPDATE SMWYAAA SET SMWYAAA006='" + subject + "' WHERE SMWYAAA005='" + wfoid + "'";
                                engine.executeSQL(sql);
                                //更新GP主旨
                                sql = "update NaNa.dbo.ProcessInstance set subject = '" + subject + "' where serialNumber = '" + wfoid + "'";
                                engine.executeSQL(sql);
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                else
                {
                    result = "<ReturnValue><Result>N</Result><Message>" + errMsg + "</Message><StackTrace></StackTrace></ReturnValue>";
                }
            }
            catch (Exception e)
            {
                result = "<ReturnValue><Result>N</Result><Message>" + e.Message + "</Message><StackTrace>" + e.StackTrace + "</StackTrace></ReturnValue>";
            }
            finally
            {
                if (engine != null)
                {
                    engine.close();
                }
            }
        }
        else
        {
            result = "<ReturnValue><Result>N</Result><Message>parameter [password] value is not correct.</Message></ReturnValue>";
        }
        return result;
    }
	
	

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    private string[] getUserInfo(AbstractEngine engine, string userId)
    {
        string[] result = new string[5];
        string sql = "select u.OID userOID, u.userName, o.OID orgUnitOID, o.id orgUnitId, o.organizationUnitName orgUnitName " +
                          "from Functions f, OrganizationUnit o, Users u " +
                          "where u.id='" + Utility.filter(userId) + "' and occupantOID=u.OID and f.organizationUnitOID=o.OID and f.isMain='1'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();
            result[1] = ds.Tables[0].Rows[0][1].ToString();
            result[2] = ds.Tables[0].Rows[0][2].ToString();
            result[3] = ds.Tables[0].Rows[0][3].ToString();
            result[4] = ds.Tables[0].Rows[0][4].ToString();
        }
        else
        {
            result[0] = "";
            result[1] = "";
            result[2] = "";
            result[3] = "";
            result[4] = "";
        }

        return result;
    }

    /// <summary>
    /// 取得人員部門資料
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    private string[] getDeptInfo(AbstractEngine engine, string userId)
    {
        string[] result = new string[5];
        string sql = "select u.OID userOID, u.userName, o.OID orgUnitOID, o.id orgUnitId, o.organizationUnitName orgUnitName " +
                          "from Functions f, OrganizationUnit o, Users u " +
                          "where u.id='" + Utility.filter(userId) + "' and occupantOID=u.OID and f.organizationUnitOID=o.OID and f.isMain='1'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();
            result[1] = ds.Tables[0].Rows[0][1].ToString();
            result[2] = ds.Tables[0].Rows[0][2].ToString();
            result[3] = ds.Tables[0].Rows[0][3].ToString();
            result[4] = ds.Tables[0].Rows[0][4].ToString();
        }
        else
        {
            result[0] = "";
            result[1] = "";
            result[2] = "";
            result[3] = "";
            result[4] = "";
        }

        return result;
    }

    /// <summary>
    /// 取得人員資訊
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    private string[] getUserGUID(AbstractEngine engine, string userId)
    {
        string sql = "select OID, userName from Users where id='" + Utility.filter(userId) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[2];
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();
            result[1] = ds.Tables[0].Rows[0][1].ToString();
        }
        else
        {
            result[0] = "";
            result[1] = "";
        }

        return result;
    }

}