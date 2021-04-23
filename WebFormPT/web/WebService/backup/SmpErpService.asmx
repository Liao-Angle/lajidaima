<%@ WebService Language="C#" Class="SmpErpService" %>

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
public class SmpErpService  : System.Web.Services.WebService {

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public string createExpenseBillFlow(string poHeaderId, string subject)
    {
        string result = "";
        string errMsg = "";
        if (!poHeaderId.Equals(""))
        {
            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            IOFactory factory = new IOFactory();
            AbstractEngine engine = null;
            AbstractEngine engineErpPortal = null;

            string agentSchema = "WebServerProject.form.SPERP002.SmpExpenseBillAgent";
            NLAgent agent = new NLAgent();

            string managerId = "";
            string creatorGUID = "";
            string originatorGUID = "";
            string requesterGUID = "";
			string checkBy = "";
            string orgUnitId = "";
			string creatorUnitId = "";
            string sql = "";
            object ret = null;
            System.IO.StreamWriter sw = null;

            try
            {
                //sw = new System.IO.StreamWriter(@"D:\ECP\WebFormPT\web\LogFolder\WebService1.log", true);
                
                agent.loadSchema(agentSchema);
                engine = factory.getEngine(acs.engineType, acs.connectString);
                
                //取得ERP Portal PO Header / PO Line Data
                string erpPortalSql = "";
                string newSubject = "";
                string poNumber = "";
                string currencyCode = "";
                string taxName = "";
                string poDate = "";
                string paymentTerms = "";
                string locationCode = "";
                string comments = "";
                string vendorCode = "";
                string vendorName = "";
                string address = "";
                string buyer = "";
                decimal isBillAmount = 0;
                decimal amount = 0;
                decimal rate = 1;
                decimal taxRate = 0;
                string requester = "";
                string poRev = "";
                string strERPPortalDB = Convert.ToString(engine.executeScalar("select SMVHAAA004 from SMVHAAA where SMVHAAA002='ERPPortalDB'"));
                string lineQty = "";
                string lineUnitPrice = "";
                string createUser = "";
				string poCategory = "";
				string companyCode = "";
                DataSet ds = null;
                
                engineErpPortal = factory.getEngine(EngineConstants.ORACLE, strERPPortalDB);

                erpPortalSql = " select a.po_number, a.currency_code, nvl(tax_name,'None') tax_name, to_char(a.po_date,'yyyy/mm/dd') po_date, payment_terms, location_code, comments, ";
                erpPortalSql += " vendor_code, vendor_name, address, buyer_employee, po_header_id, nvl(rate,1)rate, nvl(tax_rate,'1')tax_rate, revision_num, EMP_NUMBER, book_name ";
                erpPortalSql += " from smp_po_exp_header_v A, SMP_PO_HEADERS_RNF_T B, SYS_USERS C ";
                erpPortalSql += " where po_header_id = " + poHeaderId + " AND A.PO_NUMBER = B.PO_NUMBER AND B.PORTAL_USER_ID = C.USER_ID ";

                DataSet dsErp = engineErpPortal.getDataSet(erpPortalSql, "TEMP");
				
				//sw.WriteLine("erpPortalSql Count=> " + erpPortalSql);

                if (dsErp.Tables[0].Rows.Count > 0)
                {
                    poNumber = dsErp.Tables[0].Rows[0][0].ToString();
                    currencyCode = dsErp.Tables[0].Rows[0][1].ToString();	
                    taxName = dsErp.Tables[0].Rows[0][2].ToString();
                    poDate = dsErp.Tables[0].Rows[0]["po_date"].ToString();
                    paymentTerms = dsErp.Tables[0].Rows[0][4].ToString();
                    locationCode = dsErp.Tables[0].Rows[0][5].ToString();
                    comments = dsErp.Tables[0].Rows[0][6].ToString();
                    vendorCode = dsErp.Tables[0].Rows[0][7].ToString();
                    vendorName = dsErp.Tables[0].Rows[0][8].ToString();
                    address = dsErp.Tables[0].Rows[0][9].ToString();
                    buyer = dsErp.Tables[0].Rows[0][10].ToString();
                    rate = Convert.ToDecimal(dsErp.Tables[0].Rows[0]["rate"].ToString());
                    taxRate = Convert.ToDecimal(dsErp.Tables[0].Rows[0]["tax_rate"].ToString());
                    poRev = dsErp.Tables[0].Rows[0]["revision_num"].ToString();
                    createUser = dsErp.Tables[0].Rows[0]["EMP_NUMBER"].ToString();
					companyCode = dsErp.Tables[0].Rows[0]["book_name"].ToString();
                }
                else
                {
                    errMsg += poHeaderId + "無法找到單頭資料!";
                }

                //抓出採購人員
                if (buyer.Equals("4239"))
                {
	                sql = "select u.OID userOID, u.userName, o.OID orgUnitOID, o.id orgUnitId, o.organizationUnitName orgUnitName " +
	                      "from Functions f, OrganizationUnit o, Users u " +
	                      "where u.id='4239' and occupantOID=u.OID and f.organizationUnitOID=o.OID and  o.id='NSS1100'";				
				}
				else
				{
	                sql = "select u.OID userOID, u.userName, o.OID orgUnitOID, o.id orgUnitId, o.organizationUnitName orgUnitName " +
	                      "from Functions f, OrganizationUnit o, Users u " +
	                      "where u.id='" + buyer + "' and occupantOID=u.OID and f.organizationUnitOID=o.OID and f.isMain='1'";
				}
                ds = engine.getDataSet(sql, "TEMP");
				//sw.WriteLine("抓出採購人員=> " + sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    originatorGUID = ds.Tables[0].Rows[0][0].ToString();
                    orgUnitId = ds.Tables[0].Rows[0][3].ToString();
                }

                //抓出建立人員
                sql = "select u.OID userOID, u.userName, o.OID orgUnitOID, o.id orgUnitId, o.organizationUnitName orgUnitName " +
                      "from Functions f, OrganizationUnit o, Users u " +
                      "where u.id='" + createUser + "' and occupantOID=u.OID and f.organizationUnitOID=o.OID and f.isMain='1'";
					  
				//sw.WriteLine("sql=> " + sql);
				
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    creatorGUID = ds.Tables[0].Rows[0][0].ToString();
					creatorUnitId =  ds.Tables[0].Rows[0][3].ToString();
                }
                
                agent.engine = engine;
                agent.query("1=2");
                DataObject objH = agent.defaultData.create();
                string headGUID = IDProcessor.getID("");
                objH.setData("GUID", headGUID);
                objH.setData("IS_LOCK", "N");
                objH.setData("IS_DISPLAY", "Y");
                objH.setData("DATA_STATUS", "Y");
                objH.setData("SourceId", poHeaderId);
                objH.setData("Subject", subject);
                objH.setData("OriginatorGUID", creatorGUID);
                objH.setData("PoNumber", poNumber);
                objH.setData("PoVersion", poRev);
                objH.setData("Currency", currencyCode);
                objH.setData("Rate", Convert.ToString(rate));
                objH.setData("TaxCode", taxName);
                objH.setData("PoCreateDate", poDate);
                objH.setData("PurMemberGUID", originatorGUID);
                objH.setData("PaymentTerm", paymentTerms);
                objH.setData("SupplierNum", vendorCode);
                objH.setData("Organization", locationCode);
                objH.setData("Remark", comments);
                objH.setData("SupplierName", vendorName);
				objH.setData("CompanyCode", companyCode);				

				newSubject = "費用請款單 [" + poNumber + " " + vendorName + " " + comments  + "] " ; 
                                
                //sw.WriteLine(newSubject);

                if (!buyer.Equals(""))
                {
                    originatorGUID = "";
                    orgUnitId = "";
                    //抓出採購人員, 走採購人員流程
                    sql = "select u.OID userOID, u.userName, o.OID orgUnitOID, o.id orgUnitId, o.organizationUnitName orgUnitName " +
                          "from Functions f, OrganizationUnit o, Users u " +
                          "where u.id='" + Utility.filter(buyer) + "' and occupantOID=u.OID and f.organizationUnitOID=o.OID and isMain='1' ";
						  
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        originatorGUID = ds.Tables[0].Rows[0][0].ToString();
                        orgUnitId = ds.Tables[0].Rows[0][3].ToString();
                    }
                                        
                    sql = "select u.OID, u.id, u.userName from Functions f, Users u where f.occupantOID = '" + Utility.filter(originatorGUID) + "' and f.specifiedManagerOID = u.OID and isMain='1'  ";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        managerId = ds.Tables[0].Rows[0][1].ToString();
                    }
                    else {
                        errMsg += "無法取得Manager!";
                    }
                }
                else
                {
                    errMsg += "無法取得Buyer!";
                }

                //sw.WriteLine("managerId=> " + managerId);
                
                
                //單身
                sql = " select LINE_NUM, LINE_TYPE, REQUEST_NAME, ITEM_DESCRIPTION, CATEGORY_CONCAT_SEGS, NVL(QUANTITY_ORDERED,0)QUANTITY, NVL(AMOUNT,0) AMOUNT, NVL(UNIT_PRICE,0)UNIT_PRICE, REQUESTER, TAX_RATE , TAX_NAME ";
                sql += " from smp_po_exp_lines_v where po_header_id =" + poHeaderId;

                //sw.WriteLine("sql=> " + sql);
                               
                dsErp = engineErpPortal.getDataSet(sql, "TEMP");
                //sw.WriteLine("筆數=> " + dsErp.Tables[0].Rows.Count);
                if (dsErp.Tables[0].Rows.Count > 0)
                {
                    DataObject objL = null;
                    DataObjectSet dos = new DataObjectSet();
                    dos.setAssemblyName("WebServerProject");
                    dos.setChildClassString("WebServerProject.form.SPERP002.SmpExpenseBillDetail");
                    dos.setTableName("SmpExpenseBillDetail");
                    dos.isNameLess = true;
                    dos.loadFileSchema();

                    for (int j = 0; j < dsErp.Tables[0].Rows.Count; j++)
                    {
                        amount = Convert.ToDecimal(dsErp.Tables[0].Rows[j]["AMOUNT"]);
                        isBillAmount = isBillAmount + amount;
                        requester = Convert.ToString(dsErp.Tables[0].Rows[j]["REQUESTER"]);
						
						taxRate = Convert.ToDecimal(dsErp.Tables[0].Rows[j]["TAX_RATE"].ToString());
						taxName = Convert.ToString(dsErp.Tables[0].Rows[j]["TAX_NAME"]);
                        
                        lineQty = Convert.ToString(dsErp.Tables[0].Rows[j]["QUANTITY"]);
                        lineUnitPrice = Convert.ToString(dsErp.Tables[0].Rows[j]["UNIT_PRICE"]);
						poCategory = Convert.ToString(dsErp.Tables[0].Rows[j]["CATEGORY_CONCAT_SEGS"]);
                        if (lineQty.Equals("0"))
                        {
                            lineQty = "";
                        }
                        if (lineUnitPrice.Equals("0"))
                        {
                            lineUnitPrice = "";
                        }
                        //sw.WriteLine("lineQty => " + lineQty);
                        //sw.WriteLine("lineUnitPrice => " + lineQty);
                        
                        objL = dos.create();
                        objL.setData("GUID", IDProcessor.getID(""));
                        objL.setData("IS_LOCK", "N");
                        objL.setData("IS_DISPLAY", "Y");
                        objL.setData("DATA_STATUS", "Y");
                        objL.setData("PoNumberGUID", headGUID);
                        objL.setData("PoType", Convert.ToString(dsErp.Tables[0].Rows[j]["LINE_TYPE"]));
                        objL.setData("PoCategory", poCategory);
                        objL.setData("OriginatorGUID", Convert.ToString(dsErp.Tables[0].Rows[j]["REQUEST_NAME"]));
                        objL.setData("ItemSpec", Convert.ToString(dsErp.Tables[0].Rows[j]["ITEM_DESCRIPTION"]));
                        objL.setData("Remark", "");
                        objL.setData("Quantity", lineQty);
                        objL.setData("PriceUnit", lineUnitPrice);
                        objL.setData("Amount", Convert.ToString(amount));
                        objL.setData("DueDate", "");
                        objL.setData("SourceId", poHeaderId);
                        dos.add(objL);
                        
						if ((poCategory.Substring(0, 4).Equals("F1進口")) || (poCategory.Substring(0, 4).Equals("F2出貨")) || (poCategory.Substring(0, 4).Equals("F14其")) || (poCategory.Substring(0, 4).Equals("F3快遞"))) {
							if ((buyer.Equals("2676")) || (buyer.Equals("3583")) || (buyer.Equals("4098")) || (buyer.Equals("4878")) || (buyer.Equals("4868")))
                            {
								checkBy = "SMPEPR002-S1800-CHK1";    
							}
                        }
						//sw.WriteLine("poCategory.Substring(0, 2) => " + poCategory.Substring(0, 2));
						//sw.WriteLine("checkBy => " + checkBy);
						
                //        //sw.WriteLine("OriginatorGUID=> " + Convert.ToString(dsErp.Tables[0].Rows[j]["REQUEST_NAME"]));
                        //sw.WriteLine("<HeaderXML> " + objH.saveXML());
                    }
                    if (!objH.setChild("SmpExpenseBillDetail", dos))
                    {
                        errMsg += "加入單身時發生錯誤!";
                    }
                    
                   
                }
                else
                {
                    errMsg += poHeaderId + "無法找到單身資料!";
                }

                decimal enterNTAmount = isBillAmount;
                decimal enterTax = enterNTAmount * (taxRate / 100);
                decimal enterAmount = enterNTAmount + enterTax;
                decimal funcNTAmount = enterNTAmount * rate;
                decimal funcTax = enterTax * rate;
                decimal funcAmount = funcNTAmount + funcTax;
                
                objH.setData("EnterNonTaxAmount", Convert.ToString(enterNTAmount));
                objH.setData("EnterTaxAmount", Convert.ToString(enterTax));
                objH.setData("EnterAmount", Convert.ToString(enterAmount));
                objH.setData("FunctionNoTaxAmount", Convert.ToString(funcNTAmount));
                objH.setData("FunctionTaxAmount", Convert.ToString(funcTax));
                objH.setData("FunctionAmount", Convert.ToString(funcAmount));
				objH.setData("CheckBy", Convert.ToString(checkBy));
				objH.setData("TaxCode", taxName);
                				
				//sw.WriteLine("<checkBy> " + checkBy);
				//判斷申請人是否再次簽核 
				string flag1 = "";				
	            if (orgUnitId.Equals("GSC2200") || orgUnitId.Equals("GSA1000") || orgUnitId.Equals("GSC1100") 
				   || orgUnitId.Equals("NSR2000") || orgUnitId.Equals("NSR2100")  || orgUnitId.Equals("NSR2200") 
				   || orgUnitId.Equals("NSR2300") || orgUnitId.Equals("NSS1800"))
	            {
	                flag1 = "N";
	            }				
				
				//判斷本位幣是台幣或其他幣別, 影響核決權限簽核
				string flag2 = "TWD";

                
                if (errMsg.Equals(""))
                {
                    string dataXml = objH.saveXML();

                    //流程參數
                    string flowVarXml = "";
                    isBillAmount = funcAmount;

                    flowVarXml += "<SPERP002>";
                    flowVarXml += "<SPERP002>";
                    flowVarXml += "<creator DataType=\"java.lang.String\">" + createUser + "</creator>";
                    flowVarXml += "<originator DataType=\"java.lang.String\">" + buyer + "</originator>";
                    flowVarXml += "<checkby DataType=\"java.lang.String\">" + checkBy + "</checkby>";
					//flowVarXml += "<checkby DataType=\"java.lang.String\">SMPEPR002-S1800-CHK1</checkby>";
					flowVarXml += "<manager DataType=\"java.lang.String\">" + managerId + "</manager>";
                    flowVarXml += "<finStaff DataType=\"java.lang.String\"></finStaff>";
                    flowVarXml += "<billAmount DataType=\"java.lang.Integer\">" + isBillAmount + "</billAmount>";
					flowVarXml += "<checkby1 DataType=\"java.lang.String\"></checkby1>";
					flowVarXml += "<checkby2 DataType=\"java.lang.String\"></checkby2>";
					flowVarXml += "<flag1 DataType=\"java.lang.String\">" + flag1 + "</flag1>";
					flowVarXml += "<flag2 DataType=\"java.lang.String\">" + flag2 + "</flag2>";
                    flowVarXml += "</SPERP002>";
                    flowVarXml += "</SPERP002>";

                    //sw.WriteLine("<flowVarXml> " + flowVarXml);

                    //建立標準Service
                    string webUrl = GlobalProperty.getProperty("simplo", "EcpWebUrl");
                    string serviceUrl = webUrl + "/WebService/DSCFlowService.asmx";
                    WSDLClient ws = new WSDLClient(serviceUrl);
                    ws.dllPath = Utility.G_GetTempPath();
                    ws.build(true);

                    ret = ws.callSync("createFlow", //呼叫方法
                                        "SMPFORM", //ApplicationID
                                        "SPERP", //ModuleID
                                        "SPERP002", //ProcessPageID
                                        "", //sheetNo
                                        "<FORMID>SPERP002</FORMID>", //sheetnoparam
										newSubject, //subject newSubject 
                                        createUser, //"userID">使用者代號
                                        createUser, //fillerID">填表人代號
                                        creatorUnitId, //fillerOrgID">填表人單位代號
                                        createUser, //ownerID">關係人代號
                                        creatorUnitId, //ownerOrgID">關係人單位代號
                                        creatorUnitId, //submitOrgID">發起單位
                                        "1", //"importance">重要性
                                        agentSchema, //AgentSchema
                                        dataXml, //dataXML
                                        flowVarXml, //flowParameter
                                        "SPERP002", //firstParam
                                        "", //addSignXML
                                        "",  //localeString
                                        true
                                    );

					

                    //sw.WriteLine("<ret> " + ret);

                    if (ret == null || Convert.ToString(ret).Equals(""))
                    {
                        result = "<ReturnValue><Result>N</Result><Message>createFlow return null.</Message><StackTrace></StackTrace></ReturnValue>";
                    }
                    else
                    {				
                        result = ret.ToString(); //回傳xml, Result, FlowOID, SheetNo
						
						XMLProcessor doc = new XMLProcessor(result, 1);
                        XmlNode node = doc.selectSingleNode("ReturnValue");
                        string flag = node["Result"].InnerText;
                        //XDocument xd = XDocument.Parse(result);
                        //var res = xd.Element("ReturnValue");
                        //string flag = res.Element("Result").Value;
                        if (flag == "Y")
                        {
                            //update erpportal
							string wfnum = node["SheetNo"].InnerText;
                            string wfoid = node["FlowOID"].InnerText;
                            //string wfnum = res.Element("SheetNo").Value;
                            //string wfoid = res.Element("FlowOID").Value;
                            string wfstatus = "Review";
                            string wfurl = webUrl + "?runMethod=showReadOnlyForm&processSerialNumber=" + wfoid + "&CloseTitle=1&CloseToolBar=1&CloseSetting=1";
                            //sql = "UPDATE SMP_PO_HEADERS_RNF_T SET WORKFLOW_STATUS = '" + wfstatus + "',WORKFLOW_NUMBER = '" + wfnum + "', WORKFLOW_OID='" + wfoid + "', WORKFLOW_URL='" + wfurl + "' WHERE PO_NUMBER = " + poNumber;
                            //engineErpPortal.executeSQL(sql);
                            System.Data.OracleClient.OracleConnection conn = null;
                            try
                            {
                                conn = new System.Data.OracleClient.OracleConnection(strERPPortalDB);
                                System.Data.OracleClient.OracleCommand objCmd = new System.Data.OracleClient.OracleCommand();
                                objCmd.Connection = conn;
                                objCmd.CommandText = "SMP_IMPORT_PO.UPDATE_WF_STATUS";
                                objCmd.CommandType = CommandType.StoredProcedure;
                                objCmd.Parameters.Add("P_PO_NUMBER", System.Data.OracleClient.OracleType.VarChar).Value = poNumber;
                                objCmd.Parameters.Add("P_WF_STATUS", System.Data.OracleClient.OracleType.VarChar).Value = wfstatus;
                                objCmd.Parameters.Add("P_WF_OID", System.Data.OracleClient.OracleType.VarChar).Value = wfoid;
                                objCmd.Parameters.Add("P_WF_NUMBER", System.Data.OracleClient.OracleType.VarChar).Value = wfnum;
                                objCmd.Parameters.Add("P_WF_URL", System.Data.OracleClient.OracleType.VarChar).Value = wfurl;
                                objCmd.Parameters.Add("RETURN_VALUE", System.Data.OracleClient.OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
                                conn.Open();
                                objCmd.ExecuteNonQuery();
                                //result = Convert.ToString(objCmd.Parameters["RETURN_VALUE"].Value);
                            }
                            catch (Exception e)
                            {
                                throw new Exception(e.StackTrace);
                            }
                            finally
                            {
                                if (conn != null)
                                {
                                    conn.Close();
                                }
                            }
                            

                            //更新表單單號
                            subject = newSubject + "(" + wfnum + ")";
                            sql = "UPDATE SmpExpenseBill SET SheetNo = '" + wfnum + "', Subject='" + newSubject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
                            engine.executeSQL(sql);
                            //更新系統表單主旨
                            sql = "UPDATE SMWYAAA SET SMWYAAA006='" + subject + "' WHERE SMWYAAA005='" + wfoid + "'";
                            engine.executeSQL(sql);
							//更新GP主旨
                            sql = "update NaNa.dbo.ProcessInstance set subject = '" + subject + "' where serialNumber = '" + wfoid + "'";
                            engine.executeSQL(sql);
                        }

                    }

            
                }
                else
                {
                    result = "<ReturnValue><Result>N</Result><Message>" + errMsg + "</Message><StackTrace></StackTrace></ReturnValue>";
                }

                //sw.Close();    
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
            result = "<string xmlns=\"http://tempuri.org/\"><ReturnValue><Result>N</Result><Message>parameter [password] value is not correct.</Message></ReturnValue></string>";
        }
        return result;
    }

    /// <summary>
    /// 建立原物料核價單
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
            string approverId = "";
            string orgUnitId = "";
            string ownerId = "";
            string ownerOrgId = "";
            string sql = "";
            object ret = null;
            string[] reviewerIds = null;
            string[] values = null;
            DataObject dataObject = null;
            DataSet ds = null;

            try
            {
                agent.loadSchema(agentSchema);
                engine = factory.getEngine(acs.engineType, acs.connectString);

                reviewerIds = reviewerId.Split(';');
                for (int i = 0; i < reviewerIds.Length; i++)
                {
                    values = getDeptInfo(engine, reviewerIds[i]);
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

                    flowVarXml += "<SPERP008>";
                    flowVarXml += "<SPERP008>";
                    flowVarXml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
                    flowVarXml += "<originator DataType=\"java.lang.String\"></originator>";
                    flowVarXml += "<reviewer DataType=\"java.lang.String\">" + reviewerIds[0] + "</reviewer>";
                    flowVarXml += "<manager DataType=\"java.lang.String\"></manager>";
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
                            XMLProcessor doc = new XMLProcessor(result, 1);
                            XmlNode node = doc.selectSingleNode("ReturnValue");
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
}