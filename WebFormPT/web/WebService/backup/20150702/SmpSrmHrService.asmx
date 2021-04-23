<%@ WebService Language="C#" Class="SmpSrmHrService" %>

using System;
using System.Collections.Generic;
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
public class SmpSrmHrService  : System.Web.Services.WebService {
    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    
    [WebMethod]
    public string createSupplierIntoPlantFlow(string docId, string userId, string subject, string htmlContent, string isType, string password)
    {
        string result = "";
        string servicePwd = GlobalProperty.getProperty("simplo", "ServicePwd");
		
		System.IO.StreamWriter sw = null;
		sw = new System.IO.StreamWriter(@"D:\temp\spsrm002.log", true);
		
		sw.WriteLine("docId=> " + docId);
		sw.WriteLine("subject=> " + subject);
		sw.WriteLine("htmlContent=> " + htmlContent);
		sw.WriteLine("isType=> " + isType);
		sw.WriteLine("password=> " + password);
		
		
        if (password.Equals(servicePwd))
        {
            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();
			
            IOFactory factory = new IOFactory();
            AbstractEngine engine = null;
			AbstractEngine engineErpPortal = null;
            string agentSchema = "WebServerProject.form.SPSRM002.SmpSupplierIntoPlantAgent";
            NLAgent agent = new NLAgent();
            DataSet ds = null;
			DataSet dsErp = null;
            DataObject obj = null;
			
            string managerId = "";
            string originatorGUID = "";
            string orgUnitId = "";
			string erpPortalSql = "";
			string srmNumber = "";
			string strCreator = userId;
			string strReqUser = userId;
            string sql = "";
            object ret = null;
            string dataXml = "";
            string flowVarXml = "";
            string webUrl = null;
            string serviceUrl = null;
            WSDLClient ws = null;
            string newSubject = null;
			string strERPPortalDB = "";
            try
            {
                agent.loadSchema(agentSchema);
                engine = factory.getEngine(acs.engineType, acs.connectString);
				
				strERPPortalDB = Convert.ToString(engine.executeScalar("select SMVHAAA004 from SMVHAAA where SMVHAAA002='ERPPortalDB'"));
				engineErpPortal = factory.getEngine(EngineConstants.ORACLE, strERPPortalDB);

				erpPortalSql = " select su2.EMP_NUMBER ";
                erpPortalSql += " From smp.smp_request_doc_header_t ht, sys_users su1, sys_users su2 ";
                erpPortalSql += " where header_id=" + docId + " and su1.USER_ID=ht.CREATED_BY and su2.USER_ID=ht.REQUESTER_ID ";
				sw.WriteLine("erpPortalSql SQL=> " + erpPortalSql);
				
				ds = engineErpPortal.getDataSet(erpPortalSql, "TEMP");
				
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strReqUser = ds.Tables[0].Rows[0][0].ToString();
                }
				sw.WriteLine("erpPortalSql datatset ok!!! ");
				

                //取得申請者與部門
                sql = "select u.OID userOID, u.userName, o.OID orgUnitOID, o.id orgUnitId, o.organizationUnitName orgUnitName " +
                      "from Functions f, OrganizationUnit o, Users u " +
                      "where u.id='" + strReqUser + "' and occupantOID=u.OID and f.organizationUnitOID=o.OID  and f.isMain='1' ";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    originatorGUID = ds.Tables[0].Rows[0][0].ToString();
                    orgUnitId = ds.Tables[0].Rows[0][3].ToString();
                }

                //取得主管
                sql = "select u.OID, u.id, u.userName from Functions f, Users u where f.occupantOID = '" + Utility.filter(originatorGUID) + "' and f.specifiedManagerOID = u.OID";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    managerId = ds.Tables[0].Rows[0][1].ToString();
                }

                //表單資料
                agent.engine = engine;
                agent.query("1=2");
                obj = agent.defaultData.create();
                obj.setData("GUID", IDProcessor.getID(""));
                obj.setData("IS_LOCK", "N");
                obj.setData("IS_DISPLAY", "Y");
                obj.setData("DATA_STATUS", "Y");
                obj.setData("SourceId", docId);
                obj.setData("Subject", subject);
                obj.setData("OriginatorGUID", originatorGUID);
				obj.setData("DeptGUID", orgUnitId);
				obj.setData("SrmNumber", srmNumber);
                obj.setData("HtmlContent", htmlContent);
                obj.setData("IsType", isType);
                dataXml = obj.saveXML();

                //流程參數
                flowVarXml = "";
                flowVarXml += "<SPSRM002>";
                flowVarXml += "<SPSRM002>";
                flowVarXml += "<creator DataType=\"java.lang.String\">" + strCreator + "</creator>";
                flowVarXml += "<originator DataType=\"java.lang.String\">" + strReqUser + "</originator>";
                flowVarXml += "<manager DataType=\"java.lang.String\">" + managerId + "</manager>";
                flowVarXml += "<safetyStaff DataType=\"java.lang.Integer\">SPSRM002-Safety</safetyStaff>";
                flowVarXml += "<checkby DataType=\"java.lang.String\"></checkby>";
                flowVarXml += "<countersign1 DataType=\"java.lang.String\"></countersign1>";
                flowVarXml += "<countersign2 DataType=\"java.lang.String\"></countersign2>";
                flowVarXml += "<countersign3 DataType=\"java.lang.String\"></countersign3>";
                flowVarXml += "</SPSRM002>";
                flowVarXml += "</SPSRM002>";

                //建立標準Service
                webUrl = GlobalProperty.getProperty("simplo", "EcpWebUrl");
                serviceUrl = webUrl + "/WebService/DSCFlowService.asmx";
                ws = new WSDLClient(serviceUrl);
                ws.dllPath = Utility.G_GetTempPath();
                ws.build(false);
                newSubject = "承攬商進廠審核單 [" + subject + "]";
                ret = ws.callSync("createFlow", //呼叫方法
                                    "SMPFORM", //ApplicationID
                                    "SPSRM", //ModuleID
                                    "SPSRM002", //ProcessPageID
                                    "", //sheetNo
                                    "<FORMID>SPSRM002</FORMID>", //sheetnoparam
                                    newSubject, //subject
                                    userId, //"userID">使用者代號
                                    userId, //fillerID">填表人代號
                                    orgUnitId, //fillerOrgID">填表人單位代號
                                    strReqUser, //ownerID">關係人代號
                                    orgUnitId, //ownerOrgID">關係人單位代號
                                    orgUnitId, //submitOrgID">發起單位
                                    "1", //"importance">重要性
                                    agentSchema, //AgentSchema
                                    dataXml, //dataXML
                                    flowVarXml, //flowParameter
                                    "SPSRM002", //firstParam
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
                        //XDocument xd = XDocument.Parse(result);
                        //var res = xd.Element("ReturnValue");
                        //string flag = res.Element("Result").Value;
                        if (flag.Equals("Y"))
                        {
                            string wfnum = node["SheetNo"].InnerText;
                            string wfoid = node["FlowOID"].InnerText;
                            //string wfnum = res.Element("SheetNo").Value;
                            //string wfoid = res.Element("FlowOID").Value;
                            //更新表單單號
                            subject =subject + "(" + wfnum + ")";
                            sql = "UPDATE SmpSupplierIntoPlant SET SheetNo = '" + wfnum + "', Subject='" + newSubject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
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
				if (engineErpPortal != null)
                {
                    engineErpPortal.close();
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
}