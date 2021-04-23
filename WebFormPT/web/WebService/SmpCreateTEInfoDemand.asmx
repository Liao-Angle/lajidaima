<%@ WebService Language="C#" Class="SmpCreateTEInfoDemand" %>


using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Xml;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.kernal.webservice;
using WebServerProject;


    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    
    // 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
    // [System.Web.Script.Services.ScriptService]
    public class SmpCreateTEInfoDemand : System.Web.Services.WebService 
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";

        }

        [WebMethod]
        public string createSMPinfoDemandForTEFlow(string TESheetNo, string TESheetId, string subject)
        {
            string result = "";
            string errMsg = "";
			DataSet ds = null;
            System.IO.StreamWriter sw = null;
            sw = new System.IO.StreamWriter(@"d:\temp\SPIT005.log", true, System.Text.Encoding.Default);
            sw.WriteLine("---------------------createSMPinfoDemandForTEFlow  No: " +TESheetNo + "-------------");

                string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
                com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
                com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
                acs.getConnectionString();

                IOFactory factory = new IOFactory();
				IOFactory factory2 = new IOFactory();
                AbstractEngine engine = null;          

                NLAgent agent = new NLAgent();
                string agentSchema = "WebServerProject.form.SPIT005.SmpInfoDemandForTEAgent";

                string approverId = "";
                string orgUnitId = "";
                string ownerId = "";
                string ownerOrgId = "";
                string sql = "";

                object ret = null;

                string[] values = null;
                DataObject dataObject = null;
                DataSet ds33 = null;
                DataSet ds44 = null;
                string newSubject = null;
                string sheetNo = "";
                
                try
                {
                    sw.WriteLine("Try Load Schema ");
                    agent.loadSchema(agentSchema);
                    engine = factory.getEngine(acs.engineType, acs.connectString);
                    sw.WriteLine("Load Schema End  engine : " + engine );

					
                    if (errMsg.Equals(""))
                    {
                        string dataXml = null;
                        string flowVarXml = ""; //流程參數
                        string webUrl = null;
                        string serviceUrl = null;
                        string misWindow = "3787";
                        string misOwnerId = "";
                        string headGUID = "";
                        string creatorId = "smpformadm";
                        string sTESheetNo = TESheetNo;
						string sTESheetId = TESheetId;
						headGUID = IDProcessor.getID("");
                        WSDLClient ws = null;
                        //填表人
                        values = getDeptInfo(engine, creatorId);
						
                        sw.WriteLine("creatorId :  " + creatorId);
                        orgUnitId = values[3];

                        ownerId = creatorId;
                        ownerOrgId = orgUnitId;
                        sw.WriteLine("ownerId :  " + ownerId + " ownerOrgId : " + ownerOrgId);
						
						
						
                        newSubject = "嘉普資訊需求申請單 [" + subject + "]";
                        //sql = " select   Company, UserDesc ,OriginatorCName ,OriginatorNumber, Title, OriginatorDeptGUID, OriginatorEName ,Extension, GUID from WebFormPT.dbo.SmpInfoDemand_v where GUID = '"+ Utility.filter(TESheetId) +"' and SheetNo = '" +Utility.filter(TESheetNo)+ "'" ;
						//sql = " select   Company, UserDesc ,OriginatorCName ,OriginatorNumber, Title, OriginatorDeptGUID, OriginatorEName ,Extension from WebFormPT.dbo.SmpInfoDemand where SheetNo = 'SPIT00100008569'" ;
						sw.WriteLine("newSubject :  " + newSubject);
						
                        
						String Company = "";
						String UserDesc = "";
						String OriginatorCName = "";
						String OriginatorNumber = "";
						String Title = "";
						String OriginatorDeptGUID = "";
						String OriginatorEName = "";
						String Extension = "";
						
						sql = " select a.Company, a.UserDesc , a.OriginatorCName , a.OriginatorNumber, " +
						      " a.Title, a.OriginatorDeptGUID, a.OriginatorEName , a.Extension, a.GUID  " +
							  " from SmpInfoDemand_TEMP a where a.GUID = '"+ sTESheetId +"' and a.SheetNo = '" + sTESheetNo + "'" ;
						sw.WriteLine("sTESheetId : " + sTESheetId );
						sw.WriteLine("sTESheetNo : " + sTESheetNo );
						sw.WriteLine("sql : " + sql);
                        ds33 = engine.getDataSet(sql, "TEMP");
						
						
						if (ds33.Tables[0].Rows.Count > 0)
						{
							Company = ds33.Tables[0].Rows[0][0].ToString();
							UserDesc = ds33.Tables[0].Rows[0][1].ToString();	
							OriginatorCName = ds33.Tables[0].Rows[0][2].ToString();
							OriginatorNumber = ds33.Tables[0].Rows[0][3].ToString();
							Title = ds33.Tables[0].Rows[0][4].ToString();
							OriginatorDeptGUID = ds33.Tables[0].Rows[0][5].ToString();
							OriginatorEName = ds33.Tables[0].Rows[0][6].ToString();
							Extension = ds33.Tables[0].Rows[0][7].ToString();
							sw.WriteLine("Company : " + Company + " UserDesc : " + UserDesc + " OriginatorCName : " + OriginatorCName + " OriginatorNumber : " + OriginatorNumber);
						
							//單頭
                        
                            agent.engine = engine;
                            agent.query("1=2");
                            dataObject = agent.defaultData.create();
                            
                            dataObject.setData("GUID", headGUID);
                            dataObject.setData("IS_LOCK", "N");
                            dataObject.setData("IS_DISPLAY", "Y");
                            dataObject.setData("DATA_STATUS", "Y");
                            dataObject.setData("Subject", newSubject);
                            dataObject.setData("SheetNo", sheetNo);
                            dataObject.setData("OriginatorId", ownerId);

                            dataObject.setData("Company", Company);
                            
                            dataObject.setData("OriginatorDeptGUID", OriginatorDeptGUID);
                            dataObject.setData("Title", Title);
                            dataObject.setData("OriginatorNumber", OriginatorNumber);
                            dataObject.setData("OriginatorCName", OriginatorCName);
                            dataObject.setData("OriginatorEName", OriginatorEName);
                            dataObject.setData("Extension", Extension);
                            dataObject.setData("UserDesc", UserDesc);
                            dataObject.setData("TE_SheetNo", TESheetNo);
                            dataObject.setData("TE_GUID", TESheetId);
                        }        
                            sql =  " select GUID, HeaderGUID, RequestType, RequestItem, RequestDesc, " + 
                               " IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, " + 
                               " D_MODIFYUSER, D_MODIFYTIME from SmpInfoDemandDetail_TEMP " + 
                               " where HeaderGUID = '"+TESheetId +"' " ;  
                        
                            //單身
                            sw.WriteLine("Try Insert Form Detail Data " + sql);
                            ds44 = engine.getDataSet(sql, "TEMP");
							sw.WriteLine("Detail No. "  + ds44.Tables[0].Rows.Count  );
							String RequestType = "";
							String RequestItem = "";
							String RequestDesc = "";
                            if (ds44.Tables[0].Rows.Count > 0)
							{
                                 DataObject objLine = null;
                                 DataObjectSet dos = new DataObjectSet();
                                 dos.setAssemblyName("WebServerProject");
                                 dos.setChildClassString("WebServerProject.form.SPIT005.SmpInfoDemandDetailForTE");
                                 dos.setTableName("SmpInfoDemandDetailForTE");
                                 dos.isNameLess = true;
                                 dos.loadFileSchema();
                                 for (int i=0; i<ds44.Tables[0].Rows.Count; i++){
                                     objLine = dos.create();
                                     objLine.setData("IS_LOCK", "N");
                                     objLine.setData("IS_DISPLAY", "Y");
                                     objLine.setData("DATA_STATUS", "Y");
                                     objLine.setData("GUID", IDProcessor.getID(""));
                                     objLine.setData("HeaderGUID", headGUID);
									 RequestType =  Convert.ToString(ds44.Tables[0].Rows[i]["RequestType"]);
									 RequestItem =  Convert.ToString(ds44.Tables[0].Rows[i]["RequestItem"]);
									 RequestDesc =  Convert.ToString(ds44.Tables[0].Rows[i]["RequestDesc"]);
									 
                                     objLine.setData("RequestType", RequestType);
                                     objLine.setData("RequestItem", RequestItem);
                                     objLine.setData("RequestDesc", RequestDesc);
                                     sw.WriteLine("RequestType ::: "  + RequestType  );
                                     sw.WriteLine("RequestItem ::: "  + RequestItem  );
                                     sw.WriteLine("RequestDesc ::: "  + RequestDesc  );
                                     
                                     dos.add(objLine);
                                 }
                                 if (!dataObject.setChild("SmpInfoDemandDetailForTE", dos))
                                {
                                    sw.WriteLine("加入單身時發生錯誤");
                                }
                            }
                        
                        if (RequestType.Equals("1.辦公作業系統需求"))
	                    {
	                        misWindow = "4188";
	                    }
                        if (RequestItem.Equals("鼎新ERP"))
                        {
                            misWindow = "5164";
                        }
                        if (RequestItem.Equals("MES"))
                        {
                            misWindow = "3842";
                        }
				        if (RequestType.Equals("2.應用系統-帳號申請/權限異動") && RequestItem.Equals("SRM(供應商)"))
                        {
                            misWindow = "3625";
                        }				
				        if (RequestType.Equals("5.新人軟硬體需求"))
	                    {
	                        misWindow = "4188;3787";
	                    }

                        sw.WriteLine("misWindow : " + misWindow);
                        

                        dataXml = dataObject.saveXML();

                        sw.WriteLine("Set Flow Variable ");
                        flowVarXml += "<SPIT005>";
                        flowVarXml += "<SPIT005>";
                        flowVarXml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
                        flowVarXml += "<miswindow DataType=\"java.lang.String\">" + misWindow + "</miswindow>";
                        flowVarXml += "<misowner DataType=\"java.lang.String\">" + misOwnerId + "</misowner>";
                        flowVarXml += "</SPIT005>";
                        flowVarXml += "</SPIT005>";

                        //sw.WriteLine("SPIT005 dataXml: " + dataXml);

                        //建立標準Service
                        sw.WriteLine("Create Flow Service ");
                        webUrl = GlobalProperty.getProperty("simplo", "EcpWebUrl");
                        serviceUrl = webUrl + "/WebService/DSCFlowService.asmx";
                        ws = new WSDLClient(serviceUrl);
                        ws.dllPath = Utility.G_GetTempPath();
                        ws.build(false);

                        
                        sw.WriteLine("Create Flow Service call");
                        //sw.WriteLine("newSubject " + newSubject + " creatorId " + creatorId + " orgUnitId " + orgUnitId + " ownerId "+ ownerId + " ownerOrgId " + ownerOrgId);
                       // sw.WriteLine("agentSchema : " + agentSchema + " dataXml : " + dataXml +" flowVarXml : " + flowVarXml);
                        ret = ws.callSync("createFlow", //呼叫方法
                                            "SMPFORM", //ApplicationID
                                            "SPIT", //ModuleID
                                            "SPIT005", //ProcessPageID
                                            "", //sheetNo
                                            "<FORMID>SPIT005</FORMID>", //sheetnoparam
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
                                            "SPIT005", //firstParam
                                            "", //addSignXML
                                            "",  //localeString
                                            true
                                        );
                        //sw.WriteLine("ret :: " + ret);
                        if (ret == null || Convert.ToString(ret).Equals(""))
                        {
                            sw.WriteLine(" createFlow return null ");
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
                                    newSubject = newSubject + "(" + wfnum + ")";
                                    sql = "UPDATE SmpInfoDemandForTE SET SheetNo = '" + wfnum + "', Subject='" + newSubject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
                                    engine.executeSQL(sql);
                                    //更新系統表單主旨
                                    sql = "UPDATE SMWYAAA SET SMWYAAA006='" + newSubject + "' WHERE SMWYAAA005='" + wfoid + "'";
                                    engine.executeSQL(sql);
                                    //更新GP主旨
                                    sql = "update NaNa.dbo.ProcessInstance set subject = '" + newSubject + "' where serialNumber = '" + wfoid + "'";
                                    engine.executeSQL(sql);
									
									//CopyDataToDCCSCANNER(engine,TESheetNo,headGUID,wfnum);
									
									sw.WriteLine(" CopyDataFromDCCSCANNER start ");
									string IDFileName = "";
									string RealFileName = "";
									string FileExt = "";
									string FileDesc = "";
									
									string userId = creatorId;
									sw.WriteLine(" CopyDataFromDCCSCANNER start 1");
									
									string sourcePath = "\\\\\\\\DCC_SCANNER\\\\All User\\\\Eric\\\\" + TESheetNo + "\\\\";
									string targetPath = "D:\\\\ECP\\\\WebFormPT\\\\FileStorage\\\\SPIT005\\\\" + wfnum + "\\\\";
									sql = " select GUID,FILENAME,FILEEXT,DESCRIPTION from WebFormPT.dbo.FILEITEM_TEMP where LEVEL2 = '" + TESheetNo + "'";
									sw.WriteLine(" Get FileItem sql : " + sql);
									string now = DateTimeUtility.getSystemTime2(null);
									DataSet ds2 = null;
									ds2 = engine.getDataSet(sql, "TEMP");
									sw.WriteLine(" CopyDataFromDCCSCANNER start 2");
									sw.WriteLine(" Get FileItem sql Result count : "+ds2.Tables[0].Rows.Count);
									if (ds2.Tables[0].Rows.Count > 0)
									{
										for (int i = 0; i < ds2.Tables[0].Rows.Count; i++) {
											string FileGUID = IDProcessor.getID("");
											sw.WriteLine(" 11111 ");
											IDFileName = ds2.Tables[0].Rows[i]["GUID"].ToString();
											sw.WriteLine(" 22222 ");
											RealFileName = ds2.Tables[0].Rows[i]["FILENAME"].ToString();
											sw.WriteLine(" 33333 ");
											FileExt = ds2.Tables[0].Rows[i]["FILEEXT"].ToString();
											FileDesc = ds2.Tables[0].Rows[i]["DESCRIPTION"].ToString();
											string sourceFile = System.IO.Path.Combine(sourcePath, IDFileName);
											string destFile = System.IO.Path.Combine(targetPath, FileGUID);
											sw.WriteLine(" 44444 ");
											if (!System.IO.Directory.Exists(targetPath))
											{
												System.IO.Directory.CreateDirectory(targetPath);
											}
											sw.WriteLine(" 55555 ");
											System.IO.File.Copy(sourceFile, destFile, true);
											sw.WriteLine(" Copy data End ");
											sql = "Insert into WebFormPT.dbo.FILEITEM values ('" + FileGUID + "','" + headGUID + "','";
											sql += FileGUID + "','" + RealFileName + "','" + FileExt + "','','" + FileDesc + "','SPIT005','";
											sql += wfnum + "','" + userId + "','" + now + "','','','','')";
											sw.WriteLine("  Insert Data Sql : " + sql) ;
											engine.executeSQL(sql);
										}
										//engine.commit();
										sw.WriteLine(" CopyDataFromDCCSCANNER start 3 in");
									}
									sw.WriteLine(" CopyDataFromDCCSCANNER start 3 out");
                                }
                            }
                            catch (Exception e)
                            {
								sw.WriteLine(" CopyDataFromDCCSCANNER end at catch");
                            }
                        }
                    }
                    else
                    {   
                        sw.WriteLine(" errMsg : " + errMsg);
                        result = "<ReturnValue><Result>N</Result><Message>" + errMsg + "</Message><StackTrace></StackTrace></ReturnValue>";
                    }
                }
                catch (Exception e)
                {
                    result = "<ReturnValue><Result>N</Result><Message>" + e.Message + "</Message><StackTrace>" + e.StackTrace + "</StackTrace></ReturnValue>";
                    sw.WriteLine("Fail !! " + e.Message + " why?　" + e.StackTrace);
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
            return result;
        }

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

                DataSet ds11 = engine.getDataSet(sql, "TEMP");

                if (ds11.Tables[0].Rows.Count > 0)
                {
                    result[0] = ds11.Tables[0].Rows[0][0].ToString();
                    result[1] = ds11.Tables[0].Rows[0][1].ToString();
                    result[2] = ds11.Tables[0].Rows[0][2].ToString();
                    result[3] = ds11.Tables[0].Rows[0][3].ToString();
                    result[4] = ds11.Tables[0].Rows[0][4].ToString();
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
                DataSet ds22 = engine.getDataSet(sql, "TEMP");
                string[] result = new string[2];
                if (ds22.Tables[0].Rows.Count > 0)
                {
                    result[0] = ds22.Tables[0].Rows[0][0].ToString();
                    result[1] = ds22.Tables[0].Rows[0][1].ToString();
                }
                else
                {
                    result[0] = "";
                    result[1] = "";
                }

                return result;
            }
			
			
			
			
    
    
    }




