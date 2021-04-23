<%@ WebService Language="C#" Class="SmpErpPrService" %>

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
public class SmpErpPrService  : System.Web.Services.WebService {

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    /// <summary>
    /// 建立庶務性請購單
    /// </summary>
    /// <param name="headerId"></param>
    /// <param name="userId"></param>
    /// <param name="subject"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    [WebMethod]
    public string createPrFlow(string headerId, string userId, string subject, string password)
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
            AbstractEngine engineErpPortal = null;
            //OracleConnection conn = null;
            DataSet ds = null;
            
            string agentSchema = "WebServerProject.form.SPERP003.SPPOAAgent";
            NLAgent agent = new NLAgent();

            string originatorGUID = "";
            string orgUnitId = "";
            string ownerId = "";
            string ownerOrgId = "";
            string sql = "";
            object ret = null;
            string[] values = null;
            
            //表單欄位
            string sheetNo = "";
            string newSubject = "";
            string createdDate = "";
            string creator = "";
            string originator = "";
            string employeeNo = "";
            string prNumber = "";
            string isHaveAtta = "";
            string currency = "";
            string totalAmount = "";
            string description = "";
            string status = "";
            string reqHeaderId = "";
            string requestors = "";
            string checkby = "";
            string managers = "";
            string ccb = "";
            string notifiers = "SPERP003-NOTI-PUR";
            string strERPPortalDB = "";
            DataObject objH = null;
            string headGUID = null;
            string itCategory = "1";
            string setOfBookName = "";
            //System.IO.StreamWriter sw = null;
            try
            {
                //sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
                agent.loadSchema(agentSchema);
                engine = factory.getEngine(acs.engineType, acs.connectString);

                //取得申請者與部門
                values = getDeptInfo(engine, userId);
                originatorGUID = values[0];
                orgUnitId = values[3];
                ownerOrgId = orgUnitId;
                
                //取得主管
                //values = getUserManagerInfo(engine, originatorGUID);
                //managerId = values[0];

                //表單資料
                newSubject = "庶務性請購單 [" + subject + "]";
                createdDate = DateTimeUtility.getSystemTime2(null);
                creator = userId;
                originator = userId;
                employeeNo = userId;
                strERPPortalDB = Convert.ToString(engine.executeScalar("select SMVHAAA004 from SMVHAAA where SMVHAAA002='ERPPortalDB'"));
                //conn = new OracleConnection(strERPPortalDB);
                engineErpPortal = factory.getEngine(EngineConstants.ORACLE, strERPPortalDB);
                //單頭
                sql = "SELECT A.EMPLOYEE_NUMBER, A.SEGMENT1 PR_NUMBER, smp_get_has_attach('REQ_HEADERS', B.REQUISITION_HEADER_ID) IS_HAVE_ATTA, A.CURRENCY_CODE, A.TOTAL_AMOUNT, A.HEADER_DESCRIPTION, A.STATUS, A.HEADER_ID, B.REQUISITION_HEADER_ID, smp_get_set_of_book_name('OU', A.ORG_ID) SOB_NAME ";
                sql += "FROM SMP_PO_REQUISITIONS_HEADER_V A, APPS.PO_REQUISITION_HEADERS_ALL B ";
                sql += "WHERE HEADER_ID = '" + headerId + "' AND A.SEGMENT1=B.SEGMENT1";
                ds = engineErpPortal.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    employeeNo = ds.Tables[0].Rows[0]["EMPLOYEE_NUMBER"].ToString();
                    prNumber = ds.Tables[0].Rows[0]["PR_NUMBER"].ToString();
                    isHaveAtta = ds.Tables[0].Rows[0]["IS_HAVE_ATTA"].ToString();
                    currency = ds.Tables[0].Rows[0]["CURRENCY_CODE"].ToString();
                    totalAmount = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    description = ds.Tables[0].Rows[0]["HEADER_DESCRIPTION"].ToString();
                    status = ds.Tables[0].Rows[0]["STATUS"].ToString();
                    headerId = ds.Tables[0].Rows[0]["HEADER_ID"].ToString();
                    reqHeaderId = ds.Tables[0].Rows[0]["REQUISITION_HEADER_ID"].ToString();
                    setOfBookName = ds.Tables[0].Rows[0]["SOB_NAME"].ToString();
                }
                else
                {
                    errMsg += headerId + "無法找到單頭資料!";
                }

                //單頭資料
                agent.engine = engine;
                agent.query("1=2");
                objH = agent.defaultData.create();
                headGUID = IDProcessor.getID("");
                objH.setData("GUID", headGUID);
                objH.setData("IS_LOCK", "N");
                objH.setData("IS_DISPLAY", "Y");
                objH.setData("DATA_STATUS", "Y");
                objH.setData("SheetNo", sheetNo);
                objH.setData("Subject", newSubject);
                objH.setData("SPPOA003", createdDate);
                objH.setData("SPPOA004", employeeNo);
                objH.setData("SPPOA005", orgUnitId);
                objH.setData("SPPOA006", prNumber);
                objH.setData("SPPOA007", isHaveAtta);
                objH.setData("SPPOA008", currency);
                objH.setData("SPPOA009", totalAmount);
                objH.setData("SPPOA010", description);
                objH.setData("SPPOA011", status);
                objH.setData("SPPOA016", headerId);
                objH.setData("SPPOA017", reqHeaderId);
                objH.setData("SetOfBookName", setOfBookName);
                
                //單身
                sql = "SELECT A.LINE_TYPE_NAME,A.ITEM_NO,A.CATEGORY_NAME,A.CATEGORY_ID,A.ITEM_DESCRIPTION,A.UNIT_OF_MEASURE,";
                sql += "A.QUANTITY,TO_CHAR(A.NEED_BY_DATE, 'YYYY/MM/DD') NEED_BY_DATE,A.LINE_ATTRIBUTE1,A.REQUESTOR_EMPLOYEE_NUM,";
                sql += "A.DEST_ORGANIZATION_NAME,A.DELIVER_TO_LOCATION_CODE,A.CHARGE_ACCOUNT_SEGMENT2,";
                sql += "A.CHARGE_ACCOUNT_SEGMENT4,A.SUGGESTED_VENDOR_NAME,A.TAX_NAME,NOTE_TO_BUYER,A.LINE_ID,";
                sql += "A.HEADER_ID, B.REQUISITION_LINE_ID, E.EMPLOYEE_NUMBER BUYER_EMPLOYEE_NUMBER, ";
                sql += "DECODE(C.CATEGORY_ID, NULL, '1', C.IT_CATEGORY_ID) IT_CATEGORY ";
                sql += "FROM SMP_PO_REQUISITIONS_LINE_V A join APPS.PO_REQUISITION_LINES_ALL B on A.LINE_ID = TO_NUMBER(B.ATTRIBUTE14) ";
                sql += "left join SMP_LOV_EMPLOYEE_V E on A.BUYER_ID = E.PERSON_ID ";
                sql += "left join SMP_PO_REQ_IT_CATEGORY_V C on C.CATEGORY_ID=A.CATEGORY_ID ";
                sql += "WHERE A.HEADER_ID = '" + headerId + "' AND B.REQUISITION_HEADER_ID = '" + reqHeaderId + "' ";
                ds = engineErpPortal.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataObject objL = null;
                    DataObjectSet dos = new DataObjectSet();
                    dos.setAssemblyName("WebServerProject");
                    dos.setChildClassString("WebServerProject.form.SPERP003.SPPOB");
                    dos.setTableName("SPPOB");
                    dos.isNameLess = true;
                    dos.loadFileSchema();
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        string requestor = null;

                        objL = dos.create();
                        objL.setData("GUID", IDProcessor.getID(""));
                        objL.setData("IS_LOCK", "N");
                        objL.setData("IS_DISPLAY", "Y");
                        objL.setData("DATA_STATUS", "Y");
                        objL.setData("HeadGUID", headGUID);
                        objL.setData("SPPOB001", "");
                        objL.setData("SPPOB002", "");
                        objL.setData("SPPOB003", "");
                        objL.setData("SPPOB004", Convert.ToString(ds.Tables[0].Rows[j]["LINE_TYPE_NAME"]));
                        objL.setData("SPPOB005", Convert.ToString(ds.Tables[0].Rows[j]["ITEM_NO"]));
                        objL.setData("SPPOB006", Convert.ToString(ds.Tables[0].Rows[j]["CATEGORY_NAME"]));
                        objL.setData("SPPOB007", Convert.ToString(ds.Tables[0].Rows[j]["ITEM_DESCRIPTION"]));
                        objL.setData("SPPOB008", Convert.ToString(ds.Tables[0].Rows[j]["UNIT_OF_MEASURE"]));
                        objL.setData("SPPOB009", Convert.ToString(ds.Tables[0].Rows[j]["QUANTITY"]));
                        objL.setData("SPPOB010", Convert.ToString(ds.Tables[0].Rows[j]["NEED_BY_DATE"]));
                        objL.setData("SPPOB011", Convert.ToString(ds.Tables[0].Rows[j]["LINE_ATTRIBUTE1"]));
                        requestor = Convert.ToString(ds.Tables[0].Rows[j]["REQUESTOR_EMPLOYEE_NUM"]);
                        objL.setData("SPPOB012", requestor);
                        objL.setData("SPPOB013", Convert.ToString(ds.Tables[0].Rows[j]["DEST_ORGANIZATION_NAME"]));
                        objL.setData("SPPOB014", Convert.ToString(ds.Tables[0].Rows[j]["DELIVER_TO_LOCATION_CODE"]));
                        objL.setData("SPPOB015", Convert.ToString(ds.Tables[0].Rows[j]["CHARGE_ACCOUNT_SEGMENT2"]));
                        objL.setData("SPPOB016", Convert.ToString(ds.Tables[0].Rows[j]["CHARGE_ACCOUNT_SEGMENT4"]));
                        objL.setData("SPPOB017", Convert.ToString(ds.Tables[0].Rows[j]["SUGGESTED_VENDOR_NAME"]));
                        objL.setData("SPPOB018", Convert.ToString(ds.Tables[0].Rows[j]["TAX_NAME"]));
                        objL.setData("SPPOB019", Convert.ToString(ds.Tables[0].Rows[j]["NOTE_TO_BUYER"]));
                        objL.setData("SPPOB020", Convert.ToString(ds.Tables[0].Rows[j]["LINE_ID"]));
                        objL.setData("SPPOB021", Convert.ToString(ds.Tables[0].Rows[j]["HEADER_ID"]));
                        objL.setData("SPPOB022", Convert.ToString(ds.Tables[0].Rows[j]["REQUISITION_LINE_ID"]));
                        objL.setData("SPPOB023", Convert.ToString(ds.Tables[0].Rows[j]["BUYER_EMPLOYEE_NUMBER"]));
                        dos.add(objL);

                        if (requestors.IndexOf(requestor) == -1)
                        {
                            requestors += requestor + ";";
                        }

                        values = getDeptInfo(engine, requestor);
                        string requestorGUID = values[0];
                        if (j == 0)
                        {
                            ownerId = requestor;
                            ownerOrgId = values[3];
                        }

                        values = getUserManagerInfo(engine, requestorGUID);
                        if (managers.Equals(""))
                        {
                            managers = values[1];
                        }

                        itCategory = Convert.ToString(ds.Tables[0].Rows[j]["IT_CATEGORY"]);
                        objH.setData("SPPOA018", itCategory);
                    }
                    if (!objH.setChild("SPPOB", dos))
                    {
                        errMsg += "加入單身時發生錯誤!";
                    }
                }
                else 
                {
                    errMsg += headerId + "無法找到單身資料!";
                }

                if (errMsg.Equals(""))
                {
                    string dataXml = objH.saveXML();
                    string flowVarXml = ""; //流程參數
                    string webUrl = null;
                    string serviceUrl = null;
                    WSDLClient ws = null;

                    sql = "select CheckValue2,CheckValue3 from SmpFlowInspect where FormId='SPERP003' and CheckField1='DeptId' and CheckValue1='" + orgUnitId + "' and Status='Y'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string byPassOriginator = ds.Tables[0].Rows[0][0].ToString();
                        string byPassRequestor = ds.Tables[0].Rows[0][1].ToString();
                        if (byPassOriginator.Equals("Y"))
                        {
                            originator = "";
                        }
                        if (byPassRequestor.Equals("Y"))
                        {
                            requestors = "";
                        }
                    }
                    
                    flowVarXml += "<SPERP003>";
                    flowVarXml += "<SPERP003>";
                    flowVarXml += "<creator dataType=\"java.lang.String\">" + creator + "</creator>";
                    flowVarXml += "<originator dataType=\"java.lang.String\">" + originator + "</originator>";
                    flowVarXml += "<requestors dataType=\"java.lang.String\">" + requestors + "</requestors>";
                    flowVarXml += "<checkby dataType=\"java.lang.String\">" + checkby + "</checkby>";
                    flowVarXml += "<managers dataType=\"java.lang.String\">" + managers + "</managers>";
                    flowVarXml += "<ccb dataType=\"java.lang.String\">" + ccb + "</ccb>";
                    flowVarXml += "<notifiers dataType=\"java.lang.String\">" + notifiers + "</notifiers>";
                    flowVarXml += "</SPERP003>";
                    flowVarXml += "</SPERP003>";

                    //建立標準Service
                    webUrl = GlobalProperty.getProperty("simplo", "EcpWebUrl");
                    serviceUrl = webUrl + "/WebService/DSCFlowService.asmx";
                    ws = new WSDLClient(serviceUrl);
                    ws.dllPath = Utility.G_GetTempPath();
                    ws.build(false);

                    ret = ws.callSync("createFlow", //呼叫方法
                                        "SMPFORM", //ApplicationID
                                        "SPERP", //ModuleID
                                        "SPERP003", //ProcessPageID
                                        "", //sheetNo
                                        "<FORMID>SPERP003</FORMID>", //sheetnoparam
                                        newSubject, //subject
                                        userId, //"userID">使用者代號
                                        userId, //fillerID">填表人代號
                                        orgUnitId, //fillerOrgID">填表人單位代號
                                        userId, //ownerID">關係人代號
                                        orgUnitId, //ownerOrgID">關係人單位代號
                                        orgUnitId, //submitOrgID">發起單位
                                        "1", //"importance">重要性
                                        agentSchema, //AgentSchema
                                        dataXml, //dataXML
                                        flowVarXml, //flowParameter
                                        "SPERP003", //firstParam
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
                                //update erpportal
                                string wfnum = node["SheetNo"].InnerText;
                                string wfoid = node["FlowOID"].InnerText;
                                //string wfnum = res.Element("SheetNo").Value;
                                //string wfoid = res.Element("FlowOID").Value;
                                string wfstatus = "Review";
                                string wfurl = webUrl + "?runMethod=showReadOnlyForm&processSerialNumber=" + wfoid + "&CloseTitle=1&CloseToolBar=1&CloseSetting=1";
                                sql = "UPDATE SMP_PO_REQUISITIONS_HEADER_T SET WORKFLOW_STATUS = '" + wfstatus + "',WORKFLOW_NUMBER = '" + wfnum + "', WORKFLOW_OID='" + wfoid + "', WORKFLOW_URL='" + wfurl + "' WHERE HEADER_ID = " + headerId;
                                engineErpPortal.executeSQL(sql);

                                //更新表單單號
                                subject = newSubject + "(" + wfnum + ")";
                                sql = "UPDATE SPPOA SET SheetNo = '" + wfnum + "', Subject='" + newSubject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
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
                if (engineErpPortal != null)
                {
                    engineErpPortal.close();
                }
                //if (sw != null)
                //{
                //    sw.Close();
                //}
            }
        }
        else
        {
            result = "<ReturnValue><Result>N</Result><Message>parameter [password] value is not correct.</Message></ReturnValue>";
        }
        return result;
    }

    /// <summary>
    /// 建立原物料請購單
    /// </summary>
    /// <param name="password"></param>
    /// <param name="notificationId"></param>
    /// <param name="typeCode"></param>
    /// <param name="creatorId"></param>
    /// <param name="reviewerId"></param>
    /// <param name="subject"></param>
    /// <param name="htmlContent"></param>
    /// <param name="htmlContentExt"></param>
    /// <returns></returns>
    [WebMethod]
    public string createMtlPrFlow(string password, string keyId, string typeCode, string creatorId, string reviewerId, string subject, string htmlContent, string htmlContentExt)
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
            string agentSchema = "WebServerProject.form.SPERP006.SmpRequisitionFormAgent";
            string orgUnitId = "";
            string ownerId = "";
            string ownerOrgId = "";
            string sql = "";
            object ret = null;
            List<string> listReviewer = new List<string>();
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

                    flowVarXml += "<SPERP006>";
                    flowVarXml += "<SPERP006>";
                    flowVarXml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
                    flowVarXml += "<originator DataType=\"java.lang.String\"></originator>";
                    flowVarXml += "<reviewer DataType=\"java.lang.String\">" + reviewer + "</reviewer>";
                    flowVarXml += "<reviewer2 DataType=\"java.lang.String\">" + reviewer2 + "</reviewer2>";
                    flowVarXml += "<reviewer3 DataType=\"java.lang.String\">" + reviewer3 + "</reviewer3>";
                    flowVarXml += "<reviewer4 DataType=\"java.lang.String\">" + reviewer4 + "</reviewer4>";
                    flowVarXml += "<manager DataType=\"java.lang.String\"></manager>";
                    flowVarXml += "<Approver DataType=\"java.lang.String\">" + approverId + "</Approver>";
                    flowVarXml += "<notifier DataType=\"java.lang.String\"></notifier>";
                    flowVarXml += "</SPERP006>";
                    flowVarXml += "</SPERP006>";

                    //建立標準Service
                    webUrl = GlobalProperty.getProperty("simplo", "EcpWebUrl");
                    serviceUrl = webUrl + "/WebService/DSCFlowService.asmx";
                    ws = new WSDLClient(serviceUrl);
                    ws.dllPath = Utility.G_GetTempPath();
                    ws.build(false);

                    ret = ws.callSync("createFlow", //呼叫方法
                                        "SMPFORM", //ApplicationID
                                        "SPERP", //ModuleID
                                        "SPERP006", //ProcessPageID
                                        "", //sheetNo
                                        "<FORMID>SPERP006</FORMID>", //sheetnoparam
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
                                        "SPERP006", //firstParam
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
                                sql = "UPDATE SmpRequisitionForm SET SheetNo = '" + wfnum + "', Subject='" + subject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
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