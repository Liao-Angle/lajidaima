<%@ WebService Language="C#" Class="SmpErpOmService" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
//using System.Data.OracleClient;
//using System.Xml.Linq;
using System.Xml;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.kernal.webservice;
using Oracle.DataAccess.Client;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class SmpErpOmService  : System.Web.Services.WebService {

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    

    /// <summary>
    /// 建立OM報價單
    /// </summary>
    /// <param name="password"></param>
    /// <param name="notificationId"></param>
    /// <param name="typeCode"></param>
    /// <param name="creatorId"></param>
    /// <param name="reviewer1Id"></param>
	/// <param name="reviewer2Id"></param>
    /// <param name="subject"></param>
    /// <param name="htmlContent"></param>
    /// <param name="htmlContentExt"></param>
    /// <returns></returns>
    [WebMethod]
    public string createOmRfqFlow(string password, string keyId, string typeCode, string creatorId, string reviewer1Id, string reviewer2Id, string subject, string htmlContent, string htmlContentExt)
    {
        string result = "";
        string servicePwd = GlobalProperty.getProperty("simplo", "ServicePwd");
        string errMsg = "";
		System.IO.StreamWriter sw = null;
		sw = new System.IO.StreamWriter(@"d:\temp\SPERP010.log", true, System.Text.Encoding.Default);
		sw.WriteLine("createOmRfqFlow ");
		
		if (password.Equals(servicePwd))
        {
            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            IOFactory factory = new IOFactory();
            AbstractEngine engine = null;
            NLAgent agent = new NLAgent();
            string agentSchema = "WebServerProject.form.SPERP010.SmpRfqOmFormAgent";
            string approverId = "";
            string orgUnitId = "";
            string ownerId = "";
            string ownerOrgId = "";
            string sql = "";
            object ret = null;
			string originatorGUID = "";
            //string reviewer1Id = null;
			//string reviewer2Id = null;
            string[] values = null;
            DataObject dataObject = null;
            DataSet ds = null;
			string newSubject = null;

            try
            {
                agent.loadSchema(agentSchema);
                engine = factory.getEngine(acs.engineType, acs.connectString);
				
				values = getUserGUID(engine, creatorId);
				if (values[0].Equals(""))
				{
					errMsg += creatorId + " 工號不存在系統，請選擇正確的人員或洽系統管理員確認!";
				}
				
				
				//if (!reviewer1Id.Equals(""))
				//{
				//	values = getUserGUID(engine, reviewer1Id);
				//	if (values[0].Equals(""))
				//	{
				//		errMsg += reviewer1Id + " 工號不存在系統，請選擇正確的人員或洽系統管理員確認!";
				//	}
				//}
				
				//if (!reviewer2Id.Equals(""))
				//{
				//	values = getUserGUID(engine, reviewer2Id);
				//	if (values[0].Equals(""))
				//	{
				//		errMsg += reviewer2Id + " 工號不存在系統，請選擇正確的人員或洽系統管理員確認!";
				//	}
				//}
				
				//取得申請者GUID, 取得主管
				values = getDeptInfo(engine, creatorId);
				originatorGUID = values[0];                
				values = getUserManagerInfo(engine, originatorGUID);
				if (values[0].Equals(""))
                {
                    errMsg += " 建立者主管不存在系統或建立者資料不正確，請選擇正確的人員或洽系統管理員確認!";
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

                    //取得申請者GUID
					values = getDeptInfo(engine, creatorId);
					originatorGUID = values[0];                
					//取得主管
					values = getUserManagerInfo(engine, originatorGUID);
					approverId = values[1];
					
					//sw.WriteLine("approverId: " + approverId);
                    
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
                    //dataObject.setData("Reviewer1GUID", reviewer1Id);
					//dataObject.setData("Reviewer2GUID", reviewer2Id);
                    dataObject.setData("ApproverId", approverId);
                    dataObject.setData("HtmlContent", htmlContent);
                    dataObject.setData("HtmlContentExt", htmlContentExt);
                    dataObject.setData("SetOfBookName", "");
					dataObject.setData("IsResolved", reviewer1Id);   //是否需顯示審核人
                    
                    dataXml = dataObject.saveXML();

                    flowVarXml += "<SPERP010>";
                    flowVarXml += "<SPERP010>";
                    flowVarXml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
                    flowVarXml += "<originator DataType=\"java.lang.String\">" + creatorId + "</originator>";
                    flowVarXml += "<reviewer1 DataType=\"java.lang.String\"></reviewer1>";
					flowVarXml += "<reviewer2 DataType=\"java.lang.String\"></reviewer2>";
                    flowVarXml += "<manager DataType=\"java.lang.String\">" + approverId + "</manager>";
					flowVarXml += "<preperson1 DataType=\"java.lang.String\"></preperson1>";
					flowVarXml += "<prenote1 DataType=\"java.lang.String\">" + reviewer1Id + "</prenote1>";
                    flowVarXml += "</SPERP010>";
                    flowVarXml += "</SPERP010>";				
					
					sw.WriteLine("SPERP010 flowVarXml: " + flowVarXml);

                    //建立標準Service
                    webUrl = GlobalProperty.getProperty("simplo", "EcpWebUrl");
                    serviceUrl = webUrl + "/WebService/DSCFlowService.asmx";
                    ws = new WSDLClient(serviceUrl);
                    ws.dllPath = Utility.G_GetTempPath();
                    ws.build(false);
					
					newSubject = "OM報價單 [" + subject + "]";

                    ret = ws.callSync("createFlow", //呼叫方法
                                        "SMPFORM", //ApplicationID
                                        "SPERP", //ModuleID
                                        "SPERP010", //ProcessPageID
                                        "", //sheetNo
                                        "<FORMID>SPERP010</FORMID>", //sheetnoparam
                                        newSubject, //subject
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
                                        "SPERP010", //firstParam
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
                            XMLProcessor doc = new XMLProcessor(result, 1);
                            XmlNode node = doc.selectSingleNode("ReturnValue");
                            string flag = node["Result"].InnerText;
                            if (flag.Equals("Y"))
                            {
                                string wfnum = node["SheetNo"].InnerText;
                                string wfoid = node["FlowOID"].InnerText;
                                //更新表單單號
                                subject = subject + "(" + wfnum + ")";
                                sql = "UPDATE SmpRfqOmForm SET SheetNo = '" + wfnum + "', Subject='" + subject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
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
				if (sw != null)
                {
                    sw.Close();
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
    /// 取得人員主管資料
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userGUID"></param>
    /// <returns></returns>
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