<%@ WebService Language="C#" Class="SmpErpReceiptService" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
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
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SmpErpReceiptService  : System.Web.Services.WebService {

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public string createReceiptFlow(string flowId, string headerId, string userId, string subject, string password)
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
            DataSet ds = null;

            string agentSchema = "WebServerProject.form.SPERP004.SmpReceiptFormAgent";
            NLAgent agent = new NLAgent();

            string orgUnitId = "";
            string sql = "";
            object ret = null;
            string[] values = null;

            //表單欄位
            string sheetNo = "";
            string newSubject = "";
            string createdDate = "";
            string creator = "";
            string originator = "";
            string accepter = "";
            string strERPPortalDB = "";
            DataObject objHead = null;
            string headGUID = null;
           
            try
            {
                //sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
                agent.loadSchema(agentSchema);
                engine = factory.getEngine(acs.engineType, acs.connectString);

                //取得人員資訊
                values = getUserGUID(engine, userId);
                if (values[0].Equals(""))
                {
                    errMsg += userId + " 送簽人員工號不存在系統，請選擇正確的人員或洽系統管理員確認!";
                }
                
                //取得申請者與部門
                values = getDeptInfo(engine, userId);
                orgUnitId = values[3];
                if (orgUnitId.Equals(""))
                {
                    errMsg += userId + " 送簽人員工號找不到人員部門資訊，請選擇正確的人員或洽系統管理員確認!";
                }

                if (Convert.ToString(flowId).Equals(""))
                {
                    errMsg += " 送簽流程識別碼必需有值，請洽系統管理員確認!";
                }
                
                //表單資料
                newSubject = "庶務性請購驗收單 [" + subject + "]";
                createdDate = DateTimeUtility.getSystemTime2(null);
                creator = userId;
                //originator = userId;
                strERPPortalDB = Convert.ToString(engine.executeScalar("select SMVHAAA004 from SMVHAAA where SMVHAAA002='ERPPortalDB'"));
                engineErpPortal = factory.getEngine(EngineConstants.ORACLE, strERPPortalDB);
                //單頭
                sql = "SELECT R.RECEIPT_NUM, R.LINE_NUM, R.DUE_DATE, R.VENDOR_NAME, R.SHIPMENT_HEADER_ID, R.RECEIPT_NUM, " +
                    "R.LINE_NUM, R.DUE_DATE, R.VENDOR_NAME, R.VAT_REGISTRATION_NUM, R.PACKING_SLIP, R.SHIPPED_DATE, " +
                    "R.WAYBILL_AIRBILL_NUM, R.RATE, R.PAYMENT_NAME, R.VAT_CODE, R.COMMENTS, R.ORG_ID, R.CURRENCY_CODE, " +
                    "R.CURRENCY_CONVERSION_RATE, R.FLOW_ID, EMPLOYEE_NUMBER, smp_get_set_of_book_name('OU', R.ORG_ID) SOB_NAME " +
                    "FROM SMP_ERP_RCV_HEADERS_V R " + 
                    "WHERE R.SHIPMENT_HEADER_ID=" + headerId + " AND FLOW_ID=" + flowId;
                ds = engineErpPortal.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //單頭資料
                    agent.engine = engine;
                    agent.query("1=2");
                    objHead = agent.defaultData.create();
                    headGUID = IDProcessor.getID("");
                    objHead.setData("IS_LOCK", "N");
                    objHead.setData("IS_DISPLAY", "Y");
                    objHead.setData("DATA_STATUS", "Y");
                    objHead.setData("GUID", headGUID);
                    objHead.setData("SheetNo", sheetNo);
                    objHead.setData("Subject", newSubject);
                    objHead.setData("OriginatorGUID", "");
                    objHead.setData("CheckbyGUID", "");
                    objHead.setData("ReceptNum", ds.Tables[0].Rows[0]["RECEIPT_NUM"].ToString());
                    objHead.setData("VatCode", ds.Tables[0].Rows[0]["VAT_CODE"].ToString());
                    objHead.setData("VatRegistrationNum", ds.Tables[0].Rows[0]["VAT_REGISTRATION_NUM"].ToString());
                    objHead.setData("DueDate", ds.Tables[0].Rows[0]["DUE_DATE"].ToString());
                    objHead.setData("PaymentName", ds.Tables[0].Rows[0]["PAYMENT_NAME"].ToString());
                    objHead.setData("PackingSlip", ds.Tables[0].Rows[0]["PACKING_SLIP"].ToString());
                    objHead.setData("VendorName", ds.Tables[0].Rows[0]["VENDOR_NAME"].ToString());
                    objHead.setData("Rate", ds.Tables[0].Rows[0]["RATE"].ToString());
                    objHead.setData("ShippedDate", ds.Tables[0].Rows[0]["SHIPPED_DATE"].ToString());
                    objHead.setData("Comments", ds.Tables[0].Rows[0]["COMMENTS"].ToString());
                    objHead.setData("WaybillAirbillNum", ds.Tables[0].Rows[0]["WAYBILL_AIRBILL_NUM"].ToString());
                    objHead.setData("CurrencyCode", ds.Tables[0].Rows[0]["CURRENCY_CODE"].ToString());
                    objHead.setData("ShipmentHeaderId", headerId);
                    objHead.setData("FlowId", flowId);
                    objHead.setData("SetOfBookName", ds.Tables[0].Rows[0]["SOB_NAME"].ToString());
                    accepter = ds.Tables[0].Rows[0]["EMPLOYEE_NUMBER"].ToString();
                    objHead.setData("AccepterId", accepter);
                    if (accepter.Equals(""))
                    {
                        accepter = userId;
                    }
                    
                    values = getUserGUID(engine, accepter);
                    if (values[0].Equals(""))
                    {
                        errMsg += accepter + " 簽核人員工號不存在系統，請確認!";
                    }
                }
                else
                {
                    errMsg += headerId + " 無法找到單頭資料!";
                }

                //單身
                sql = "SELECT SHIPMENT_HEADER_ID, RECEIPT_NUM, LINE_NUM, DUE_DATE, ITEM_CODE, " +
                        "ITEM_DESCRIPTION, LONG_DESCRIPTION, SUBINVENTORY, UNIT_OF_MEASURE, PROJECT_CODE, LINE_TYPE, " +
                        "INSPECT_DATE, PURCHASE_NUMBER, RECEIPT_QUANTITY, QA_QUANTITY, RECEIPT_AMT, QA_AMT, " + 
                        "REQ_DISTRIBUTION_ID, ITEM_ID, ORG_ID, CURRENCY_CODE, LINE_TYPE_ID, CURRENCY_CONVERSION_RATE, " + 
                        "INSPECTION_REQUIRED_FLAG, QUANTITY_ORDERED, QUANTITY_RECEIVED, REQUEST_NAME, " + 
                        "BUYER_NAME, PAYMENT_NAME, VAT_CODE, PR_NUM, RCV_TRANSACTION_ID, UNIT_PRICE, VOUCHER_NUM, " + 
                        "ITEM_CATEGORY " +
                        "FROM SMP_ERP_RCV_LINES_V WHERE SHIPMENT_HEADER_ID=" + headerId;
                ds = engineErpPortal.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataObject objLine = null;
                    DataObjectSet dos = new DataObjectSet();
                    dos.setAssemblyName("WebServerProject");
                    dos.setChildClassString("WebServerProject.form.SPERP004.SmpReceiptDetail");
                    dos.setTableName("SmpReceiptDetail");
                    dos.isNameLess = true;
                    dos.loadFileSchema();
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        objLine = dos.create();
                        objLine.setData("IS_LOCK", "N");
                        objLine.setData("IS_DISPLAY", "Y");
                        objLine.setData("DATA_STATUS", "Y");
                        objLine.setData("GUID", IDProcessor.getID(""));
                        objLine.setData("ReceiptFormGUID", headGUID);
                        objLine.setData("LineNum", Convert.ToString(ds.Tables[0].Rows[j]["LINE_NUM"]));
                        objLine.setData("LineType", Convert.ToString(ds.Tables[0].Rows[j]["LINE_TYPE"]));
                        objLine.setData("ItemCode", Convert.ToString(ds.Tables[0].Rows[j]["ITEM_CODE"]));
                        objLine.setData("ItemDescription", Convert.ToString(ds.Tables[0].Rows[j]["ITEM_DESCRIPTION"]));
                        objLine.setData("UnitOfMeasure", Convert.ToString(ds.Tables[0].Rows[j]["UNIT_OF_MEASURE"]));
                        objLine.setData("Subinventory", Convert.ToString(ds.Tables[0].Rows[j]["SUBINVENTORY"]));
                        objLine.setData("PurchaseNumber", Convert.ToString(ds.Tables[0].Rows[j]["PURCHASE_NUMBER"]));
                        objLine.setData("PrNum", Convert.ToString(ds.Tables[0].Rows[j]["PR_NUM"]));
                        objLine.setData("ItemCategory", Convert.ToString(ds.Tables[0].Rows[j]["ITEM_CATEGORY"]));
                        objLine.setData("ProjectCode", Convert.ToString(ds.Tables[0].Rows[j]["PROJECT_CODE"]));
                        objLine.setData("RequestName", Convert.ToString(ds.Tables[0].Rows[j]["REQUEST_NAME"]));
                        objLine.setData("QuantityOrdered", Convert.ToString(ds.Tables[0].Rows[j]["QUANTITY_ORDERED"]));
                        objLine.setData("QuantityReceived", Convert.ToString(ds.Tables[0].Rows[j]["QUANTITY_RECEIVED"]));
                        objLine.setData("ReceiptQuantity", Convert.ToString(ds.Tables[0].Rows[j]["RECEIPT_QUANTITY"]));
                        dos.add(objLine);
                    }
                    if (!objHead.setChild("SmpReceiptDetail", dos))
                    {
                        errMsg += "加入單身時發生錯誤!";
                    }
                }
                else
                {
                    errMsg += headerId + " 無法找到單身資料!";
                }

                if (errMsg.Equals(""))
                {
                    string dataXml = objHead.saveXML();
                    string flowVarXml = ""; //流程參數
                    string webUrl = null;
                    string serviceUrl = null;
                    WSDLClient ws = null;

                    flowVarXml += "<SPERP004>";
                    flowVarXml += "<SPERP004>";
                    flowVarXml += "<originator dataType=\"java.lang.String\">" + originator + "</originator>";
                    flowVarXml += "<accepter dataType=\"java.lang.String\">" + accepter + "</accepter>";
                    flowVarXml += "<manager dataType=\"java.lang.String\"></manager>";
                    flowVarXml += "<accepter1 dataType=\"java.lang.String\"></accepter1>";
                    flowVarXml += "<accepter2 dataType=\"java.lang.String\"></accepter2>";
                    flowVarXml += "<accepter3 dataType=\"java.lang.String\"></accepter3>";
                    flowVarXml += "<accepter4 dataType=\"java.lang.String\"></accepter4>";
                    flowVarXml += "<accepter5 dataType=\"java.lang.String\"></accepter5>";
                    flowVarXml += "</SPERP004>";
                    flowVarXml += "</SPERP004>";

                    //建立標準Service
                    webUrl = GlobalProperty.getProperty("simplo", "EcpWebUrl");
                    serviceUrl = webUrl + "/WebService/DSCFlowService.asmx";
                    ws = new WSDLClient(serviceUrl);
                    ws.dllPath = Utility.G_GetTempPath();
                    ws.build(false);

                    ret = ws.callSync("createFlow", //呼叫方法
                                        "SMPFORM", //ApplicationID
                                        "SPERP", //ModuleID
                                        "SPERP004", //ProcessPageID
                                        "", //sheetNo
                                        "<FORMID>SPERP004</FORMID>", //sheetnoparam
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
                                        "SPERP004", //firstParam
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
                                //string wfstatus = "Review";
                                //string wfurl = webUrl + "?runMethod=showReadOnlyForm&processSerialNumber=" + wfoid + "&CloseTitle=1&CloseToolBar=1&CloseSetting=1";
                                //engineErpPortal.executeSQL(sql);

                                //更新表單單號
                                subject = newSubject + "(" + wfnum + ")";
                                sql = "UPDATE SmpReceiptForm SET SheetNo = '" + wfnum + "', Subject='" + newSubject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
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
                    errMsg = "無法送簽! " + errMsg;
                    result = "<ReturnValue><Result>N</Result><Message>" + errMsg + "</Message><StackTrace></StackTrace></ReturnValue>";
                }
            }
            catch (Exception e)
            {
                result = "<ReturnValue><Result>N</Result><Message>" + errMsg + "," + e.Message + "</Message><StackTrace>" + e.StackTrace + "</StackTrace></ReturnValue>";
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