<%@ WebService Language="C#" Class="SmpErpInvMmtService" %>

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
public class SmpErpInvMmtService  : System.Web.Services.WebService {

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    //public string createInvMmtFlow(string flowId, string headerId, string userId, string subject, string password)
    public string createInvMmtFlow(string headerId, string userId, string subject, string password)
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

            string agentSchema = "WebServerProject.form.SPERP005.SmpInvMmtFormAgent";
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
            string creatorGUID = "";
            string originator = "";
            string requestor = "";
            string requestorGUID = "";
            string orgId = "";
			string orgName = "";
			string trxType = "";
            string strERPPortalDB = "";
            DataObject objHead = null;
            string headGUID = null;
			decimal lineQty = 0;
			decimal sumQty = 0; 
			string sobName = "";
			System.IO.StreamWriter sw = null;
           
            try
            {
                sw = new System.IO.StreamWriter(@"d:\temp\SPERP005.log", true, System.Text.Encoding.Default);
                agent.loadSchema(agentSchema);
                engine = factory.getEngine(acs.engineType, acs.connectString);

                //取得人員資訊
                values = getUserGUID(engine, userId);
                if (values[0].Equals(""))
                {
                    errMsg += userId + " 送簽人員工號不存在系統，請選擇正確的人員或洽系統管理員確認!";
                }
                else 
                {
                    creatorGUID = values[0];  
                }
                
                //取得申請者與部門
                values = getDeptInfo(engine, userId);
                orgUnitId = values[3];
                if (orgUnitId.Equals(""))
                {
                    errMsg += userId + " 送簽人員工號找不到人員部門資訊，請選擇正確的人員或洽系統管理員確認!";
                }
                
                //表單資料
                newSubject = "費用領退料申請單 [" + subject + "]";
                createdDate = DateTimeUtility.getSystemTime2(null);
                creator = userId;
                //originator = userId;
                strERPPortalDB = Convert.ToString(engine.executeScalar("select SMVHAAA004 from SMVHAAA where SMVHAAA002='ERPPortalDB'"));
                engineErpPortal = factory.getEngine(EngineConstants.ORACLE, strERPPortalDB);
                //單頭
                sql = " SELECT organization_code, organization_name, invmmt_number, nvl(ecp_type,8) ecp_type, Transaction_type_name, version, im_header_id, " + 
				      " requestor_name, requestor_number ,plan_date, requestor_dept, note, APPS.smp_get_set_of_book_name('OU',ORG_ID) as sob_name "+
                      " FROM smp_invmmt_headers_ecp_vp where im_header_id=" + headerId ;
                
				//sw.WriteLine("header sql => " + sql);
				//sw.WriteLine( DateTime.Now + " - header sql => " + sql);
				
                ds = engineErpPortal.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //單頭資料
                    agent.engine = engine;
                    agent.query("1=2");
                    objHead = agent.defaultData.create();
                    headGUID = IDProcessor.getID("");
					trxType = ds.Tables[0].Rows[0]["ecp_type"].ToString();            // ecp trx type id
                    orgId = ds.Tables[0].Rows[0]["organization_code"].ToString();     // 判斷流程的orgID
                    orgName = ds.Tables[0].Rows[0]["organization_name"].ToString();
                    requestor = ds.Tables[0].Rows[0]["requestor_number"].ToString();
					sobName  = ds.Tables[0].Rows[0]["sob_name"].ToString();
                    values = getUserGUID(engine, requestor);
                    
                    if (!values[0].Equals(""))
                    {
                        requestorGUID = values[0];
                    }
                    else
                    {
                        errMsg += requestor + " 需求者人員工號不存在系統，請選擇正確的人員或洽系統管理員確認!";
                    }
                    objHead.setData("IS_LOCK", "N");
                    objHead.setData("IS_DISPLAY", "Y");
                    objHead.setData("DATA_STATUS", "Y");
                    objHead.setData("GUID", headGUID);
                    objHead.setData("SheetNo", sheetNo);
                    objHead.setData("Subject", newSubject);
                    objHead.setData("OriginatorGUID", requestorGUID);
                    objHead.setData("Checkby1GUID", "");
					objHead.setData("Checkby2GUID", "");
					objHead.setData("RdMemberGUID", "");
					objHead.setData("MeMemberGUID", "");
					objHead.setData("PmMemberGUID", "");
					objHead.setData("SaleMemberGUID", "");
					objHead.setData("SaleManagerGUID", "");
					objHead.setData("McMemberGUID", "");
					objHead.setData("QaMemberGUID", "");
                    objHead.setData("OrgId", orgId);
                    objHead.setData("OrgName", orgName);
					objHead.setData("CompanyCode", sobName);
                    objHead.setData("EcpTrxTypeId", trxType);
                    objHead.setData("InvMmtNum", ds.Tables[0].Rows[0]["invmmt_number"].ToString());
                    //objHead.setData("HeaderId", ds.Tables[0].Rows[0]["im_header_id"].ToString());
                    objHead.setData("HeaderId", headerId);
                    objHead.setData("TrxTypeName", ds.Tables[0].Rows[0]["Transaction_type_name"].ToString());
                    objHead.setData("Version", ds.Tables[0].Rows[0]["version"].ToString());
                    objHead.setData("RequestorName", ds.Tables[0].Rows[0]["requestor_name"].ToString());
                    objHead.setData("RequestorNum", ds.Tables[0].Rows[0]["requestor_number"].ToString());
                    objHead.setData("RequestorDept", ds.Tables[0].Rows[0]["requestor_dept"].ToString());
                    objHead.setData("Comments", ds.Tables[0].Rows[0]["note"].ToString());
                    objHead.setData("LineQuantity", "");
                    objHead.setData("SumQuantity", "");
                    objHead.setData("CreatorGUID", creatorGUID);
                }
                else
                {
                    errMsg += headerId + " 無法找到單頭資料!";
                }
				
                //單身
				sql = " select im_header_id, transaction_Type_name, item_code, item_desc, item_rev, subinventory_code, " +
				      " transfer_subinventory, locator, transfer_locator, transaction_uom, reason_desc, transaction_reference, " +
					  " project_code, quantity, sub_onhand_qty " +
					  " from smp_invmmt_lines_ecp_vp where im_header_id=" + headerId;

				//sw.WriteLine("line sql => " + sql); 
				//sw.WriteLine( DateTime.Now + " - line sql => " + sql); 
					 
                ds = engineErpPortal.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataObject objLine = null;
                    DataObjectSet dos = new DataObjectSet();
                    dos.setAssemblyName("WebServerProject");
                    dos.setChildClassString("WebServerProject.form.SPERP005.SmpInvMmtDetail");
                    dos.setTableName("SmpInvMmtDetail");
                    dos.isNameLess = true;
                    dos.loadFileSchema();
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        objLine = dos.create();
                        objLine.setData("IS_LOCK", "N");
                        objLine.setData("IS_DISPLAY", "Y");
                        objLine.setData("DATA_STATUS", "Y");
                        objLine.setData("GUID", IDProcessor.getID(""));
                        objLine.setData("HeaderGUID", headGUID);
                        objLine.setData("ItemCode", Convert.ToString(ds.Tables[0].Rows[j]["item_code"]));
                        objLine.setData("ItemDesc", Convert.ToString(ds.Tables[0].Rows[j]["item_desc"]));
                        objLine.setData("ItemRev", Convert.ToString(ds.Tables[0].Rows[j]["item_rev"]));
                        objLine.setData("Subinventory", Convert.ToString(ds.Tables[0].Rows[j]["subinventory_code"]));
                        objLine.setData("TransferSubinventory", Convert.ToString(ds.Tables[0].Rows[j]["transfer_subinventory"]));
                        objLine.setData("Locator", Convert.ToString(ds.Tables[0].Rows[j]["locator"]));
                        objLine.setData("TransferLocator", Convert.ToString(ds.Tables[0].Rows[j]["transfer_locator"]));
                        objLine.setData("TransactionUom", Convert.ToString(ds.Tables[0].Rows[j]["transaction_uom"]));
                        objLine.setData("ReasonDesc", Convert.ToString(ds.Tables[0].Rows[j]["reason_desc"]));
                        objLine.setData("TransactionReference", Convert.ToString(ds.Tables[0].Rows[j]["transaction_reference"]));
                        objLine.setData("ProjectCode", Convert.ToString(ds.Tables[0].Rows[j]["project_code"]));
                        objLine.setData("Quantity", Convert.ToString(ds.Tables[0].Rows[j]["quantity"]));
						objLine.setData("SubOnhandQty", Convert.ToString(ds.Tables[0].Rows[j]["sub_onhand_qty"]));
                        dos.add(objLine);
						lineQty = lineQty + 1; 
						sumQty = sumQty + Convert.ToDecimal(ds.Tables[0].Rows[j]["quantity"]);
                    }
                    if (!objHead.setChild("SmpInvMmtDetail", dos))
                    {
                        errMsg += "加入單身時發生錯誤!";
                    }
					objHead.setData("LineQuantity", Convert.ToString(lineQty));
                    objHead.setData("SumQuantity", Convert.ToString(sumQty));
                }
                else
                {
                    errMsg += headerId + " 無法找到單身資料!";
                }
				//sw.WriteLine( "Insert SmpInvMmt time : " + DateTime.Now ); 

                if (errMsg.Equals(""))
                {
                    string dataXml = objHead.saveXML();
                    string flowVarXml = ""; //流程參數
                    string webUrl = null;
                    string serviceUrl = null;
                    WSDLClient ws = null;

                    flowVarXml += "<SPERP005>";
                    flowVarXml += "<SPERP005>";
                    flowVarXml += "<creator dataType=\"java.lang.String\">" + creator + "</creator>";
                    flowVarXml += "<originator dataType=\"java.lang.String\">" + requestor + "</originator>";
                    flowVarXml += "<checkby1 dataType=\"java.lang.String\"></checkby1>";
					flowVarXml += "<checkby2 dataType=\"java.lang.String\"></checkby2>";
					flowVarXml += "<rdMember dataType=\"java.lang.String\"></rdMember>";
					flowVarXml += "<meMember dataType=\"java.lang.String\"></meMember>";
					flowVarXml += "<pmMember dataType=\"java.lang.String\"></pmMember>";
					flowVarXml += "<saleMember dataType=\"java.lang.String\"></saleMember>";
					flowVarXml += "<saleManager dataType=\"java.lang.String\"></saleManager>";
					flowVarXml += "<mcMember dataType=\"java.lang.String\"></mcMember>";
					flowVarXml += "<qaMember dataType=\"java.lang.String\"></qaMember>";
                    flowVarXml += "<orgName dataType=\"java.lang.String\">" + orgId + "</orgName>";
					flowVarXml += "<trxType dataType=\"java.lang.String\">" + trxType + "</trxType>";
					flowVarXml += "<notify1 DataType=\"java.lang.String\"></notify1>";
                    flowVarXml += "</SPERP005>";
                    flowVarXml += "</SPERP005>";
					
					//sw.WriteLine("flow => " + flowVarXml); 
					//sw.WriteLine( "get XML Time : " + DateTime.Now ); 

                    //建立標準Service
                    webUrl = GlobalProperty.getProperty("simplo", "EcpWebUrl");
                    serviceUrl = webUrl + "/WebService/DSCFlowService.asmx";
					sw.WriteLine( "建立標準Service before WSDLClient : " + DateTime.Now ); 
					
                    ws = new WSDLClient(serviceUrl);
                    ws.dllPath = Utility.G_GetTempPath();
					sw.WriteLine( "建立標準Service before build : " + DateTime.Now ); 
					
                    //ws.build(false);
					ws.build(false);
					
					sw.WriteLine( "建立標準Service after build : " + DateTime.Now ); 

                    ret = ws.callSync("createFlow", //呼叫方法
                                        "SMPFORM", //ApplicationID
                                        "SPERP", //ModuleID
                                        "SPERP005", //ProcessPageID
                                        "", //sheetNo
                                        "<FORMID>SPERP005</FORMID>", //sheetnoparam
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
                                        "SPERP005", //firstParam
                                        "", //addSignXML
                                        "",  //localeString
                                        true
                                    );
					sw.WriteLine( "建立標準Service after ret : " + DateTime.Now ); 
					
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
							
                            if (flag == "Y")
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
                                sql = "UPDATE SmpInvMmtForm SET SheetNo = '" + wfnum + "', Subject='" + newSubject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
                                engine.executeSQL(sql);
								//sw.WriteLine( "UPDATE SmpInvMmtForm Time : " + DateTime.Now ); 
								
                                //更新系統表單主旨
                                sql = "UPDATE SMWYAAA SET SMWYAAA006='" + subject + "' WHERE SMWYAAA005='" + wfoid + "'";
                                engine.executeSQL(sql);
								//sw.WriteLine( "UPDATE SMWYAAA Time : " + DateTime.Now ); 
								
                                //更新GP主旨
                                sql = "update NaNa.dbo.ProcessInstance set subject = '" + subject + "' where serialNumber = '" + wfoid + "'";
                                engine.executeSQL(sql);
								//sw.WriteLine( "UPDATE NaNa ProcessInstance Time : " + DateTime.Now ); 
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