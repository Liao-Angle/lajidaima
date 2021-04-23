<%@ WebService Language="C#" Class="IRSSignService" %>
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

/// <summary>
///IRSSignService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]

public class IRSSignService : System.Web.Services.WebService
{
    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    //資訊立案
    [WebMethod]
    public string createIRSSignFlow(string BillNo)
    {
        string result = "";
        string errMsg = "";
        DataSet ds = null;
        System.IO.StreamWriter sw = null;
        sw = new System.IO.StreamWriter(@"d:\temp\SQIT003.log", true, System.Text.Encoding.Default);
        sw.WriteLine("---------------------createSMPinfoDemandForTEFlow  No: " + BillNo + "-------------");

        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        IOFactory factory2 = new IOFactory();
        AbstractEngine engine = null;

        NLAgent agent = new NLAgent();
        string agentSchema = "WebServerProject.form.SQIT003.SQIT003Agent";

        string orgUnitId = "";
        string ownerId = "";
        string ownerOrgId = "";
        string sql = "";

        object ret = null;

        string[] values = null;
        DataObject dataObject = null;
        DataSet ds33 = null;
        //DataSet ds44 = null;
        string newSubject = null;
        string sheetNo = "";

        try
        {
            sw.WriteLine("Try Load Schema ");
            agent.loadSchema(agentSchema);
            engine = factory.getEngine(acs.engineType, acs.connectString);
            sw.WriteLine("Load Schema End  engine : " + engine);


            if (errMsg.Equals(""))
            {
                string dataXml = null;
                string flowVarXml = ""; //流程參數
                string webUrl = null;
                string serviceUrl = null;
                string headGUID = "";
                string creatorId = "";
                string sTESheetNo = BillNo;
                string sTESheetId = "";
                headGUID = IDProcessor.getID("");
                WSDLClient ws = null;
                
                //傳入單號獲取簽核頁面內容
                sql = "SELECT BillNumber,WBS,WriterName+'/'+WriterOwnerName WtName,WriterDeptName, " +
                      "ApplicationTime1,ExpectedTime1,ApplicantName,WriterOrgShortName, " +
                      "IRTName+'-'+IRNName+'-'+IRSNName IRSItem,FormTheme,FormContents,Applicant,IRSPContents " +
                      "FROM [10.3.11.102].ITPlatForm_DB.dbo.VIEW_Requirements_Form " +
                      "where BillNumber='" + BillNo + "' ";
                ds33 = engine.getDataSet(sql, "TEMP");
                if (ds33.Tables[0].Rows.Count > 0)
                {
                    creatorId = ds33.Tables[0].Rows[0][11].ToString();
                    sTESheetId = ds33.Tables[0].Rows[0][11].ToString();
                }
                
                //填表人
                values = getDeptInfo(engine, creatorId);

                sw.WriteLine("creatorId :  " + creatorId);
                orgUnitId = values[3];

                ownerId = creatorId;
                ownerOrgId = orgUnitId;
                sw.WriteLine("ownerId :  " + ownerId + " ownerOrgId : " + ownerOrgId);



                string sub = ds33.Tables[0].Rows[0][9].ToString();
                string emp = ds33.Tables[0].Rows[0][6].ToString();
                newSubject = "資訊需求立案"+"["+"申請人:"+emp+";"+"主旨:"+sub+"]";

                sw.WriteLine("newSubject :  " + newSubject);


                String RqNO = "";
                String version = "";
                String Creator = "";
                String PartNo = "";
                String RqDate = "";
                String RqPlanDate = "";
                String RqOwner = "";
                String BU = "";
                String RqType = "";
                String Subject1 = "";
                String RqConent = "";

                
                
                sw.WriteLine("sTESheetId : " + sTESheetId);
                sw.WriteLine("sTESheetNo : " + sTESheetNo);
                sw.WriteLine("sql : " + sql);
                


                if (ds33.Tables[0].Rows.Count > 0)
                {
                    RqNO = ds33.Tables[0].Rows[0][0].ToString();
                    version = ds33.Tables[0].Rows[0][1].ToString();
                    Creator = ds33.Tables[0].Rows[0][2].ToString();
                    //string writerString = ds33.Tables[0].Rows[0]["WriterName"].ToString().Contains("/") ? ds33.Tables[0].Rows[0]["WriterName"].ToString().Split('/')[0] : ds33.Tables[0].Rows[0]["WriterName"].ToString();
                    //Creator = string.Format("{0}/{1}", writerString, ds33.Tables[0].Rows[0]["WriterOwnerName"].ToString());
                    PartNo = ds33.Tables[0].Rows[0][3].ToString();
                    RqDate = ds33.Tables[0].Rows[0][4].ToString();
                    RqPlanDate = ds33.Tables[0].Rows[0][5].ToString();
                    RqOwner = ds33.Tables[0].Rows[0][6].ToString();
                    BU = ds33.Tables[0].Rows[0][7].ToString();
                    RqType = ds33.Tables[0].Rows[0][8].ToString();
                    Subject1 = ds33.Tables[0].Rows[0][9].ToString();
                    if (ds33.Tables[0].Rows[0][12].ToString() == "")
                    {
                        RqConent = ds33.Tables[0].Rows[0][10].ToString().Replace("<br/>", "\n").Replace("<tab/>", "\t");
                    }
                    else
                    {
                        RqConent = ds33.Tables[0].Rows[0][10].ToString().Replace("<br/>", "\n").Replace("<tab/>", "\t") + "\n\n" + "權限：" + ds33.Tables[0].Rows[0][12].ToString();  
                    }
                    

                    //sw.WriteLine("EmpNo : " + EmpNo + " Reason : " + Reason + " WorkDate : " + WorkDate + "  " );

                    //單頭
                    
                    agent.engine = engine;
                    agent.query("1=2");
                    dataObject = agent.defaultData.create();

                    dataObject.setData("GUID", headGUID);
                    dataObject.setData("ForwardHR", "N");
                    dataObject.setData("IS_LOCK", "N");
                    dataObject.setData("IS_DISPLAY", "Y");
                    dataObject.setData("DATA_STATUS", "Y");
                    dataObject.setData("Subject", newSubject);
                    dataObject.setData("SheetNo", sheetNo);
                    dataObject.setData("RqNO", RqNO);
                    dataObject.setData("version", version);
                    dataObject.setData("Creator", Creator);
                    dataObject.setData("PartNo", PartNo);
                    dataObject.setData("RqDate", RqDate);
                    dataObject.setData("RqPlanDate", RqPlanDate);
                    dataObject.setData("RqOwner", RqOwner);
                    dataObject.setData("BU", BU);
                    dataObject.setData("RqType", RqType);
                    dataObject.setData("Subject1", Subject1);
                    dataObject.setData("RqConent", RqConent);
                    dataObject.setData("SheetNo", BillNo);
                }



                dataXml = dataObject.saveXML();

                sw.WriteLine("Set Flow Variable ");
                flowVarXml += "<SQIT003>";
                flowVarXml += "<SQIT003>";
                flowVarXml += "<CREATOR DataType=\"java.lang.String\">" + creatorId + "</CREATOR>";
                flowVarXml += "</SQIT003>";
                flowVarXml += "</SQIT003>";

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
                                    "SYSTEM", //ApplicationID
                                    "EASYFLOW", //ModuleID
                                    "SQIT003", //ProcessPageID
                                    "", //sheetNo
                                    "<FORMID>SQIT003</FORMID>", //sheetnoparam
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
                                    "SQIT003", //firstParam
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
                            //newSubject = newSubject + "(" + wfnum + ")";
                            sql = "UPDATE SQIT003A SET SheetNo = '" + wfnum + "', Subject=N'" + newSubject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
                            engine.executeSQL(sql);
                            //更新系統表單主旨
                            sql = "UPDATE SMWYAAA SET SMWYAAA006=N'" + newSubject + "' WHERE SMWYAAA005='" + wfoid + "'";
                            engine.executeSQL(sql);
                            //更新GP主旨
                            sql = "update NaNa.dbo.ProcessInstance set subject = N'" + newSubject + "' where serialNumber = '" + wfoid + "'";
                            engine.executeSQL(sql);
			    
                            //更新附件資料
                            string sourcePath = "\\\\\\\\10.3.11.102\\\\InformationRequirementsSystem\\\\";
                            sw.WriteLine("sourcePath:" +sourcePath);
                            string targetPath = "D:\\\\ECP\\\\WebFormPT\\\\FileStorage\\\\SQIT003\\\\" + wfnum + "\\\\";
                            sw.WriteLine("targetPath:" + targetPath);
                            string now = DateTimeUtility.getSystemTime2(null);
                            sql = " SELECT BillNumber,SUBSTRING(isnull(FileName,''),1,CHARINDEX('.',isnull(FileName,''))-1) FileName1,FileName,SUBSTRING(isnull(FileName,''),CHARINDEX('.',isnull(FileName,''))+1,len(isnull(FileName,''))-charindex('-',isnull(FileName,''))) FileExt FROM [10.3.11.102].ITPlatForm_DB.dbo.IRS_Requirements_Files WHERE BillNumber  = '" + BillNo + "'";
                            sw.WriteLine("sql:" + sql);
                            string userId = values[0];
                            DataSet ds1 = null;
                            ds1 = engine.getDataSet(sql, "TEMP");
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                                {
                                    string FileGUID = IDProcessor.getID("");
                                    sw.WriteLine(" FileGUID :" + FileGUID);
                                    string IDFileName = headGUID;
                                    sw.WriteLine(" IDFileName :" + IDFileName);
                                    string RealFileName = ds1.Tables[0].Rows[i]["FileName1"].ToString();
                                    string SFileName = ds1.Tables[0].Rows[i]["FileName"].ToString();
                                    sw.WriteLine(" RealFileName: " + RealFileName);
                                    string FileExt = ds1.Tables[0].Rows[i]["FILEEXT"].ToString();
                                    string FileDesc = "";
                                    string sourceFile = System.IO.Path.Combine(sourcePath, SFileName);
                                    string destFile = System.IO.Path.Combine(targetPath, FileGUID);
                                    sw.WriteLine(" 44444 " + sourceFile + "/n" + destFile);
                                    if (!System.IO.Directory.Exists(targetPath))
                                    {
                                        System.IO.Directory.CreateDirectory(targetPath);
                                    }
                                    sw.WriteLine(" 55555 ");
                                    System.IO.File.Copy(sourceFile, destFile, true);
                                    sw.WriteLine(" Copy data End ");
                                    sql = "Insert into WebFormPT.dbo.FILEITEM values ('" + FileGUID + "','" + headGUID + "','";
                                    sql += FileGUID + "','" + RealFileName + "','" + FileExt + "','','" + FileDesc + "','SQIT003','";
                                    sql += wfnum + "','" + userId + "','" + now + "','','','','')";
                                    sw.WriteLine("  Insert Data Sql : " + sql);
                                    engine.executeSQL(sql);

                                    
                                } 
                            }
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
     //資訊申請
    [WebMethod]
    public string createIRSSignSencondFlow(string BillNo)
    {
        string result = "";
        string errMsg = "";
        DataSet ds = null;
        System.IO.StreamWriter sw = null;
        sw = new System.IO.StreamWriter(@"d:\temp\SQIT004.log", true, System.Text.Encoding.Default);
        sw.WriteLine("---------------------createIRSSignFlow  No: " + BillNo + "-------------");

        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        IOFactory factory2 = new IOFactory();
        AbstractEngine engine = null;

        NLAgent agent = new NLAgent();
        string agentSchema = "WebServerProject.form.SQIT004.SQIT004Agent";

        string orgUnitId = "";
        string ownerId = "";
        string ownerOrgId = "";
        string sql = "";
        string sql1 = "";

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
            sw.WriteLine("Load Schema End  engine : " + engine);


            if (errMsg.Equals(""))
            {
                string dataXml = null;
                string flowVarXml = ""; //流程參數
                string webUrl = null;
                string serviceUrl = null;
                string headGUID = "";
                string creatorId = "";
                string sTESheetNo = BillNo;
                string sTESheetId = "";
                headGUID = IDProcessor.getID("");
                WSDLClient ws = null;

                //傳入單號獲取簽核頁面內容
                sql = "SELECT BillNumber,WBS,WriterName+'/'+WriterOwnerName WtName,WriterDeptName, " +
                      "ApplicationTime1,ExpectedTime1,ApplicantName,ActualOrgShortName, " +
                      "ActualIRTName+'-'+ActualIRNName+'-'+ActualIRSNName IRSItem,ActualTheme,ActualContents,Applicant, " +
                      "ActualIRSPContents,ActualLevels,DeveloperName,ScheduleEndTime1,SAHours,StaffHours,TotalExpenditure,FormTheme,FormContents " +
                      "FROM [10.3.11.102].ITPlatForm_DB.dbo.VIEW_Requirements_Form " +
                      "where BillNumber='" + BillNo + "' ";
                ds33 = engine.getDataSet(sql, "TEMP");
                if (ds33.Tables[0].Rows.Count > 0)
                {
                    creatorId = ds33.Tables[0].Rows[0][11].ToString();
                    sTESheetId = ds33.Tables[0].Rows[0][11].ToString();
                }

                sql1 = "select case when ISNULL(IsPR,0)=0 then '否' else '是' end IsPR FROM [10.3.11.102].ITPlatForm_DB.dbo.IRS_Requirements_Time WHERE BillNumber = '" + BillNo + "'";

                ds44 = engine.getDataSet(sql1, "TEMP");
                
                //填表人
                values = getDeptInfo(engine, creatorId);

                sw.WriteLine("creatorId :  " + creatorId);
                orgUnitId = values[3];

                ownerId = creatorId;
                ownerOrgId = orgUnitId;
                sw.WriteLine("ownerId :  " + ownerId + " ownerOrgId : " + ownerOrgId);



                string sub = "";
                if (string.IsNullOrEmpty(ds33.Tables[0].Rows[0][9].ToString()))
                {
                     sub = ds33.Tables[0].Rows[0][19].ToString();
                }
                else
                {
                     sub = ds33.Tables[0].Rows[0][9].ToString(); 
                }
                string emp = ds33.Tables[0].Rows[0][6].ToString();
                newSubject = "資訊需求申請" + "[" + "申請人:" + emp + ";" + "主旨:" + sub + "]";

                sw.WriteLine("newSubject :  " + newSubject);


                String RqNO = "";
                String version = "";
                String Creator = "";
                String PartNo = "";
                String RqDate = "";
                String RqPlanDate = "";
                String RqOwner = "";
                String BU = "";
                String xzsb = "";
                String RqType = "";
                String Subject1 = "";
                String RqConent = "";
                String RqLv = "";
                String DevlopOwner = "";
                String RqMISDate = "";
                String PlanHour = "";
                String DevlopHour = "";
                String PlanCost = "";
                string content="";



                sw.WriteLine("sTESheetId : " + sTESheetId);
                sw.WriteLine("sTESheetNo : " + sTESheetNo);
                sw.WriteLine("sql : " + sql);



                if (ds33.Tables[0].Rows.Count > 0)
                {
                    RqNO = ds33.Tables[0].Rows[0][0].ToString();
                    version = ds33.Tables[0].Rows[0][1].ToString();
                    Creator = ds33.Tables[0].Rows[0][2].ToString();
                    PartNo = ds33.Tables[0].Rows[0][3].ToString();
                    RqDate = ds33.Tables[0].Rows[0][4].ToString();
                    RqPlanDate = ds33.Tables[0].Rows[0][5].ToString();
                    RqOwner = ds33.Tables[0].Rows[0][6].ToString();
                    BU = ds33.Tables[0].Rows[0][7].ToString();
                    RqType = ds33.Tables[0].Rows[0][8].ToString();
                    if (string.IsNullOrEmpty(ds33.Tables[0].Rows[0][9].ToString()))
                    {
                        Subject1 = ds33.Tables[0].Rows[0][19].ToString();
                    }
                    else
                    {
                        Subject1 = ds33.Tables[0].Rows[0][9].ToString();
                    }

                    if (ds33.Tables[0].Rows[0][12].ToString() == "")
                    {
                        if (string.IsNullOrEmpty(ds33.Tables[0].Rows[0][10].ToString()))
                        {
                            RqConent = ds33.Tables[0].Rows[0][20].ToString().Replace("<br/>", "\n").Replace("<tab/>", "\t");
                        }
                        else
                        {
                            RqConent = ds33.Tables[0].Rows[0][10].ToString().Replace("<br/>", "\n").Replace("<tab/>", "\t");
                        }
                        
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(ds33.Tables[0].Rows[0][10].ToString()))
                        {
                            RqConent = ds33.Tables[0].Rows[0][20].ToString().Replace("<br/>", "\n").Replace("<tab/>", "\t") + "\n\n" + "權限：" + ds33.Tables[0].Rows[0][12].ToString();
                        }
                        else
                        {
                            RqConent = ds33.Tables[0].Rows[0][10].ToString().Replace("<br/>", "\n").Replace("<tab/>", "\t") + "\n\n" + "權限：" + ds33.Tables[0].Rows[0][12].ToString(); 
                        }
                    }
                    RqLv = ds33.Tables[0].Rows[0][13].ToString();
                    DevlopOwner = ds33.Tables[0].Rows[0][14].ToString();
                    RqMISDate = ds33.Tables[0].Rows[0][15].ToString();
                    PlanHour = ds33.Tables[0].Rows[0][16].ToString();
                    DevlopHour = ds33.Tables[0].Rows[0][17].ToString();
                    PlanCost = ds33.Tables[0].Rows[0][18].ToString();
                    xzsb = ds44.Tables[0].Rows[0]["IsPR"].ToString();

                    //sw.WriteLine("EmpNo : " + EmpNo + " Reason : " + Reason + " WorkDate : " + WorkDate + "  " );

                    //單頭

                    agent.engine = engine;
                    agent.query("1=2");
                    dataObject = agent.defaultData.create();

                    dataObject.setData("GUID", headGUID);
                    dataObject.setData("ForwardHR", "N");
                    dataObject.setData("IS_LOCK", "N");
                    dataObject.setData("IS_DISPLAY", "Y");
                    dataObject.setData("DATA_STATUS", "Y");
                    dataObject.setData("Subject", newSubject);
                    dataObject.setData("SheetNo", sheetNo);
                    dataObject.setData("RqNO", RqNO);
                    dataObject.setData("version", version);
                    dataObject.setData("Creator", Creator);
                    dataObject.setData("PartNo", PartNo);
                    dataObject.setData("RqDate", RqDate);
                    dataObject.setData("RqPlanDate", RqPlanDate);
                    dataObject.setData("RqOwner", RqOwner);
                    dataObject.setData("BU", BU);
                    dataObject.setData("xzsb", xzsb);
                    dataObject.setData("RqType", RqType);
                    dataObject.setData("Subject1", Subject1);
                    dataObject.setData("RqConent", RqConent);
                    dataObject.setData("RqLv", RqLv);
                    dataObject.setData("DevlopOwner", DevlopOwner);
                    dataObject.setData("RqMISDate", RqMISDate);
                    dataObject.setData("PlanHour", PlanHour);
                    dataObject.setData("DevlopHour", DevlopHour);
                    dataObject.setData("PlanCost", PlanCost);
                    dataObject.setData("SheetNo", BillNo);
                }



                dataXml = dataObject.saveXML();

                sw.WriteLine("Set Flow Variable ");
                flowVarXml += "<SQIT004>";
                flowVarXml += "<SQIT004>";
                flowVarXml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
                flowVarXml += "<qhcj DataType=\"java.lang.String\">" + RqLv + "</qhcj>";
                flowVarXml += "</SQIT004>";
                flowVarXml += "</SQIT004>";

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
                                    "SYSTEM", //ApplicationID
                                    "EASYFLOW", //ModuleID
                                    "SQIT004", //ProcessPageID
                                    "", //sheetNo
                                    "<FORMID>SQIT004</FORMID>", //sheetnoparam
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
                                    "SQIT004", //firstParam
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
                            //newSubject = newSubject + "(" + wfnum + ")";
                            sql = "UPDATE SQIT004A SET SheetNo = '" + wfnum + "', Subject=N'" + newSubject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
                            engine.executeSQL(sql);
                            //更新系統表單主旨
                            sql = "UPDATE SMWYAAA SET SMWYAAA006=N'" + newSubject + "' WHERE SMWYAAA005='" + wfoid + "'";
                            engine.executeSQL(sql);
                            //更新GP主旨
                            sql = "update NaNa.dbo.ProcessInstance set subject = N'" + newSubject + "' where serialNumber = '" + wfoid + "'";
                            engine.executeSQL(sql);

                            //更新附件資料
                            string sourcePath = "\\\\\\\\10.3.11.198\\\\InformationRequirementsSystem\\\\";
                            sw.WriteLine("sourcePath:" + sourcePath);
                            string targetPath = "D:\\\\ECP\\\\WebFormPT\\\\FileStorage\\\\SQIT004\\\\" + wfnum + "\\\\";
                            sw.WriteLine("targetPath:" + targetPath);
                            string now = DateTimeUtility.getSystemTime2(null);
                            sql = " SELECT BillNumber,SUBSTRING(isnull(FileName,''),1,CHARINDEX('.',isnull(FileName,''))-1) FileName1,FileName,SUBSTRING(isnull(FileName,''),CHARINDEX('.',isnull(FileName,''))+1,len(isnull(FileName,''))-charindex('-',isnull(FileName,''))) FileExt FROM [10.3.11.102].ITPlatForm_DB.dbo.IRS_Requirements_Files WHERE BillNumber  = '" + BillNo + "'";
                            sw.WriteLine("sql:" + sql);
                            string userId = values[0];
                            DataSet ds1 = null;
                            ds1 = engine.getDataSet(sql, "TEMP");
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                                {
                                    string FileGUID = IDProcessor.getID("");
                                    sw.WriteLine(" FileGUID :" + FileGUID);
                                    string IDFileName = headGUID;
                                    sw.WriteLine(" IDFileName :" + IDFileName);
                                    string RealFileName = ds1.Tables[0].Rows[i]["FileName1"].ToString();
                                    string SFileName = ds1.Tables[0].Rows[i]["FileName"].ToString();
                                    sw.WriteLine(" RealFileName: " + RealFileName);
                                    string FileExt = ds1.Tables[0].Rows[i]["FILEEXT"].ToString();
                                    string FileDesc = "";
                                    string sourceFile = System.IO.Path.Combine(sourcePath, SFileName);
                                    string destFile = System.IO.Path.Combine(targetPath, FileGUID);
                                    sw.WriteLine(" 44444 " + sourceFile + "/n" + destFile);
                                    if (!System.IO.Directory.Exists(targetPath))
                                    {
                                        System.IO.Directory.CreateDirectory(targetPath);
                                    }
                                    sw.WriteLine(" 55555 ");
                                    System.IO.File.Copy(sourceFile, destFile, true);
                                    sw.WriteLine(" Copy data End ");
                                    sql = "Insert into WebFormPT.dbo.FILEITEM values ('" + FileGUID + "','" + headGUID + "','";
                                    sql += FileGUID + "','" + RealFileName + "','" + FileExt + "','','" + FileDesc + "','SQIT004','";
                                    sql += wfnum + "','" + userId + "','" + now + "','','','','')";
                                    sw.WriteLine("  Insert Data Sql : " + sql);
                                    engine.executeSQL(sql);


                                }
                            }
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
    //資訊展延
    [WebMethod]
    public string createIRSSignExtensionFlow(string BillNo,string wbs)
    {
        string result = "";
        string errMsg = "";
        DataSet ds = null;
        System.IO.StreamWriter sw = null;
        sw = new System.IO.StreamWriter(@"d:\temp\SQIT005.log", true, System.Text.Encoding.Default);
        sw.WriteLine("---------------------createIRSSignFlow  No: " + BillNo + "-------------");

        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        IOFactory factory2 = new IOFactory();
        AbstractEngine engine = null;

        NLAgent agent = new NLAgent();
        string agentSchema = "WebServerProject.form.SQIT005.SQIT005Agent";

        string orgUnitId = "";
        string ownerId = "";
        string ownerOrgId = "";
        string sql1 = "";
        string sql2 = "";
        string sql3 = "";

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
            sw.WriteLine("Load Schema End  engine : " + engine);


            if (errMsg.Equals(""))
            {
                string dataXml = null;
                string flowVarXml = ""; //流程參數
                string webUrl = null;
                string serviceUrl = null;
                string headGUID = "";
                string creatorId = "";
                string sTESheetNo = BillNo;
                string sTESheetId = "";
                headGUID = IDProcessor.getID("");
                WSDLClient ws = null;

                //傳入單號獲取簽核頁面內容
                //wbs=1 主單展延
                sql1 = "SELECT BillNumber,WBS,WriterName+'/'+WriterOwnerName WtName,WriterDeptName, " +
                      "ApplicationTime1,ExpectedTime1,ApplicantName,ActualOrgShortName, " +
                      "ActualIRTName+'-'+ActualIRNName+'-'+ActualIRSNName IRSItem,ActualTheme,ActualContents,Applicant, " +
                      "ActualIRSPContents,ActualLevels,DeveloperName,ScheduleEndTime1,SAHours,StaffHours,TotalExpenditure,FormTheme,FormContents " +
                      "FROM [10.3.11.102].ITPlatForm_DB.dbo.VIEW_Requirements_Form " +
                      "where BillNumber='" + BillNo + "' ";
                //wbs<>1
                sql2 = "SELECT BillNumber,WBS,WriterName+'/'+WriterOwnerName WtName,WriterDeptName, " +
                      "ApplicationTime1,ExpectedTime1,ApplicantName,ActualOrgShortName, " +
                      "ActualIRTName+'-'+ActualIRNName+'-'+ActualIRSNName IRSItem,ActualTheme,ActualContents,Applicant, " +
                      "ActualIRSPContents,ActualLevels,DeveloperName,ScheduleEndTime1,SAHours,StaffHours,TotalExpenditure,FormTheme,FormContents " +
                      "FROM [10.3.11.102].ITPlatForm_DB.dbo.VIEW_Requirements_Split " +
                      "where BillNumber='" + BillNo + "' ";
                //展延資料
                sql3 = "SELECT CONVERT(NVARCHAR(10),PreDate,20) PreDate, " +
                      "CONVERT(NVARCHAR(10),NextDate,20) NextDate,Deferrer,Opinion " +
                      "FROM [10.3.11.102].ITPlatForm_DB.dbo.IRS_Requirements_Delay " +
                      "WHERE BillNumber = '" + BillNo + "' AND WBS = '" + wbs + "' AND IRDStatus = 0";
                ds44 = engine.getDataSet(sql3, "TEMP");
                if (ds44.Tables[0].Rows.Count > 0)
                {
                    creatorId = ds44.Tables[0].Rows[0][2].ToString();
                    sTESheetId = ds44.Tables[0].Rows[0][2].ToString();
                }

                //填表人
                values = getDeptInfo(engine, creatorId);

                sw.WriteLine("creatorId :  " + creatorId);
                orgUnitId = values[3];

                ownerId = creatorId;
                ownerOrgId = orgUnitId;
                sw.WriteLine("ownerId :  " + ownerId + " ownerOrgId : " + ownerOrgId);

                if (wbs == "1")
                {
                    ds33 = engine.getDataSet(sql1, "TEMP");
                }
                else
                {
                    ds33 = engine.getDataSet(sql2, "TEMP");
                }

                string sub = "";
                if (string.IsNullOrEmpty(ds33.Tables[0].Rows[0][9].ToString()))
                {
                     sub = ds33.Tables[0].Rows[0][19].ToString();
                }
                else
                {
                     sub = ds33.Tables[0].Rows[0][9].ToString(); 
                }
                string emp = ds33.Tables[0].Rows[0][6].ToString();
                newSubject = "資訊需求展延" + "[" + "申請人:" + emp + ";" + "主旨:" + sub + "]";

                sw.WriteLine("newSubject :  " + newSubject);


                String RqNO = "";
                String version = "";
                String Creator = "";
                String PartNo = "";
                String RqDate = "";
                String RqPlanDate = "";
                String RqOwner = "";
                String BU = "";
                String RqType = "";
                String Subject1 = "";
                String RqConent = "";
                String RqLv = "";
                String DevlopOwner = "";
                String RqMISDate = "";
                String PlanHour = "";
                String DevlopHour = "";
                String PlanCost = "";
                String ExtensionDate1 = "";
                String ExtensionDate2 = "";
                String Wbs = "";
                String ExtensionReason = "";
                string content = "";



                sw.WriteLine("sTESheetId : " + sTESheetId);
                sw.WriteLine("sTESheetNo : " + sTESheetNo);
                sw.WriteLine("sql : " + sql1);
                


                if (ds33.Tables[0].Rows.Count > 0 && ds44.Tables[0].Rows.Count>0)
                {
                    RqNO = ds33.Tables[0].Rows[0][0].ToString();
                    version = ds33.Tables[0].Rows[0][1].ToString();
                    Creator = ds33.Tables[0].Rows[0][2].ToString();
                    PartNo = ds33.Tables[0].Rows[0][3].ToString();
                    RqDate = ds33.Tables[0].Rows[0][4].ToString();
                    RqPlanDate = ds33.Tables[0].Rows[0][5].ToString();
                    RqOwner = ds33.Tables[0].Rows[0][6].ToString();
                    BU = ds33.Tables[0].Rows[0][7].ToString();
                    RqType = ds33.Tables[0].Rows[0][8].ToString();
                    if (string.IsNullOrEmpty(ds33.Tables[0].Rows[0][9].ToString()))
                    {
                        Subject1 = ds33.Tables[0].Rows[0][19].ToString();
                    }
                    else
                    {
                        Subject1 = ds33.Tables[0].Rows[0][9].ToString();
                    }

                    if (ds33.Tables[0].Rows[0][12].ToString() == "")
                    {
                        if (string.IsNullOrEmpty(ds33.Tables[0].Rows[0][10].ToString()))
                        {
                            RqConent = ds33.Tables[0].Rows[0][20].ToString().Replace("<br/>", "\n").Replace("<tab/>", "\t");
                        }
                        else
                        {
                            RqConent = ds33.Tables[0].Rows[0][10].ToString().Replace("<br/>", "\n").Replace("<tab/>", "\t");
                        }
                        
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(ds33.Tables[0].Rows[0][10].ToString()))
                        {
                            RqConent = ds33.Tables[0].Rows[0][20].ToString().Replace("<br/>", "\n").Replace("<tab/>", "\t") + "\n\n" + "權限：" + ds33.Tables[0].Rows[0][12].ToString();
                        }
                        else
                        {
                            RqConent = ds33.Tables[0].Rows[0][10].ToString().Replace("<br/>", "\n").Replace("<tab/>", "\t") + "\n\n" + "權限：" + ds33.Tables[0].Rows[0][12].ToString(); 
                        }
                    }
                    RqLv = ds33.Tables[0].Rows[0][13].ToString();
                    DevlopOwner = ds33.Tables[0].Rows[0][14].ToString();
                    RqMISDate = ds33.Tables[0].Rows[0][15].ToString();
                    PlanHour = ds33.Tables[0].Rows[0][16].ToString();
                    DevlopHour = ds33.Tables[0].Rows[0][17].ToString();
                    PlanCost = ds33.Tables[0].Rows[0][18].ToString();
                    ExtensionDate1 = ds44.Tables[0].Rows[0][0].ToString();
                    ExtensionDate2 = ds44.Tables[0].Rows[0][1].ToString();
                    ExtensionReason = ds44.Tables[0].Rows[0][3].ToString();
                    Wbs = wbs.ToString();
                    
                    //sw.WriteLine("EmpNo : " + EmpNo + " Reason : " + Reason + " WorkDate : " + WorkDate + "  " );

                    //單頭

                    agent.engine = engine;
                    agent.query("1=2");
                    dataObject = agent.defaultData.create();

                    dataObject.setData("GUID", headGUID);
                    dataObject.setData("ForwardHR", "N");
                    dataObject.setData("IS_LOCK", "N");
                    dataObject.setData("IS_DISPLAY", "Y");
                    dataObject.setData("DATA_STATUS", "Y");
                    dataObject.setData("Subject", newSubject);
                    dataObject.setData("SheetNo", sheetNo);
                    dataObject.setData("RqNO", RqNO);
                    dataObject.setData("version", version);
                    dataObject.setData("Creator", Creator);
                    dataObject.setData("PartNo", PartNo);
                    dataObject.setData("RqDate", RqDate);
                    dataObject.setData("RqPlanDate", RqPlanDate);
                    dataObject.setData("RqOwner", RqOwner);
                    dataObject.setData("BU", BU);
                    dataObject.setData("RqType", RqType);
                    dataObject.setData("Subject1", Subject1);
                    dataObject.setData("RqConent", RqConent);
                    dataObject.setData("RqLv", RqLv);
                    dataObject.setData("DevlopOwner", DevlopOwner);
                    dataObject.setData("RqMISDate", RqMISDate);
                    dataObject.setData("PlanHour", PlanHour);
                    dataObject.setData("DevlopHour", DevlopHour);
                    dataObject.setData("PlanCost", PlanCost);
                    dataObject.setData("ExtensionDate1", ExtensionDate1);
                    dataObject.setData("ExtensionDate2", ExtensionDate2);
                    dataObject.setData("wbs", Wbs);
                    dataObject.setData("ExtensionReason", ExtensionReason);
                    dataObject.setData("SheetNo", BillNo);
                }



                dataXml = dataObject.saveXML();

                sw.WriteLine("Set Flow Variable ");
                flowVarXml += "<SQIT005>";
                flowVarXml += "<SQIT005>";
                flowVarXml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
                flowVarXml += "<qhcj DataType=\"java.lang.String\">" + RqLv + "</qhcj>";
                flowVarXml += "</SQIT005>";
                flowVarXml += "</SQIT005>";

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
                                    "SYSTEM", //ApplicationID
                                    "EASYFLOW", //ModuleID
                                    "SQIT005", //ProcessPageID
                                    "", //sheetNo
                                    "<FORMID>SQIT005</FORMID>", //sheetnoparam
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
                                    "SQIT005", //firstParam
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
                            //newSubject = newSubject + "(" + wfnum + ")";
                            sql1 = "UPDATE SQIT005A SET SheetNo = '" + wfnum + "', Subject=N'" + newSubject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
                            engine.executeSQL(sql1);
                            //更新系統表單主旨
                            sql1 = "UPDATE SMWYAAA SET SMWYAAA006=N'" + newSubject + "' WHERE SMWYAAA005='" + wfoid + "'";
                            engine.executeSQL(sql1);
                            //更新GP主旨
                            sql1 = "update NaNa.dbo.ProcessInstance set subject = N'" + newSubject + "' where serialNumber = '" + wfoid + "'";
                            engine.executeSQL(sql1);

                            //更新附件資料
                            string sourcePath = "\\\\\\\\10.3.11.198\\\\InformationRequirementsSystem\\\\";
                            sw.WriteLine("sourcePath:" + sourcePath);
                            string targetPath = "D:\\\\ECP\\\\WebFormPT\\\\FileStorage\\\\SQIT005\\\\" + wfnum + "\\\\";
                            sw.WriteLine("targetPath:" + targetPath);
                            string now = DateTimeUtility.getSystemTime2(null);
                            sql1 = " SELECT BillNumber,SUBSTRING(isnull(FileName,''),1,CHARINDEX('.',isnull(FileName,''))-1) FileName1,FileName,SUBSTRING(isnull(FileName,''),CHARINDEX('.',isnull(FileName,''))+1,len(isnull(FileName,''))-charindex('-',isnull(FileName,''))) FileExt FROM [10.3.11.102].ITPlatForm_DB.dbo.IRS_Requirements_Files WHERE BillNumber  = '" + BillNo + "'";
                            sw.WriteLine("sql:" + sql1);
                            string userId = values[0];
                            DataSet ds1 = null;
                            ds1 = engine.getDataSet(sql1, "TEMP");
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                                {
                                    string FileGUID = IDProcessor.getID("");
                                    sw.WriteLine(" FileGUID :" + FileGUID);
                                    string IDFileName = headGUID;
                                    sw.WriteLine(" IDFileName :" + IDFileName);
                                    string RealFileName = ds1.Tables[0].Rows[i]["FileName1"].ToString();
                                    string SFileName = ds1.Tables[0].Rows[i]["FileName"].ToString();
                                    sw.WriteLine(" RealFileName: " + RealFileName);
                                    string FileExt = ds1.Tables[0].Rows[i]["FILEEXT"].ToString();
                                    string FileDesc = "";
                                    string sourceFile = System.IO.Path.Combine(sourcePath, SFileName);
                                    string destFile = System.IO.Path.Combine(targetPath, FileGUID);
                                    sw.WriteLine(" 44444 " + sourceFile + "/n" + destFile);
                                    if (!System.IO.Directory.Exists(targetPath))
                                    {
                                        System.IO.Directory.CreateDirectory(targetPath);
                                    }
                                    sw.WriteLine(" 55555 ");
                                    System.IO.File.Copy(sourceFile, destFile, true);
                                    sw.WriteLine(" Copy data End ");
                                    sql1 = "Insert into WebFormPT.dbo.FILEITEM values ('" + FileGUID + "','" + headGUID + "','";
                                    sql1 += FileGUID + "','" + RealFileName + "','" + FileExt + "','','" + FileDesc + "','SQIT005','";
                                    sql1 += wfnum + "','" + userId + "','" + now + "','','','','')";
                                    sw.WriteLine("  Insert Data Sql : " + sql1);
                                    engine.executeSQL(sql1);


                                }
                            }
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
    //維修立案

    [WebMethod]
    public string createIMSSignFlow(string BillNo)
    {
        string result = "";
        string errMsg = "";
        DataSet ds = null;
        System.IO.StreamWriter sw = null;
        sw = new System.IO.StreamWriter(@"d:\temp\SQIT006.log", true, System.Text.Encoding.Default);
        sw.WriteLine("---------------------createSMPinfoDemandForTEFlow  No: " + BillNo + "-------------");

        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        IOFactory factory2 = new IOFactory();
        AbstractEngine engine = null;

        NLAgent agent = new NLAgent();
        string agentSchema = "WebServerProject.form.SQIT006.SQIT006Agent";

        string orgUnitId = "";
        string ownerId = "";
        string ownerOrgId = "";
        string sql = "";
        string sql2 = "";

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
            sw.WriteLine("Load Schema End  engine : " + engine);


            if (errMsg.Equals(""))
            {
                string dataXml = null;
                string flowVarXml = ""; //流程參數
                string webUrl = null;
                string serviceUrl = null;
                string headGUID = "";
                string creatorId = "";
                string sTESheetNo = BillNo;
                string sTESheetId = "";
                headGUID = IDProcessor.getID("");
                WSDLClient ws = null;

                //傳入單號獲取簽核頁面內容
                sql = "SELECT BillNumber,Barcode,WriterName+'/'+WriterOwnerName WtName, " +
                      "WriterDeptName,ApplicationTime1,ExpectedTime1,ApplicantName,WriterOrgShortName, " +
                      "IMTName,FormReason,Applicant " +
                      "FROM [10.3.11.102].ITPlatForm_DB.dbo.VIEW_Maintenance_Form  " +
                      "where BillNumber='" + BillNo + "' ";
                ds33 = engine.getDataSet(sql, "TEMP");
                if (ds33.Tables[0].Rows.Count > 0)
                {
                    creatorId = ds33.Tables[0].Rows[0][10].ToString();
                    sTESheetId = ds33.Tables[0].Rows[0][10].ToString();
                }

                //填表人
                values = getDeptInfo(engine, creatorId);

                sw.WriteLine("creatorId :  " + creatorId);
                orgUnitId = values[3];

                ownerId = creatorId;
                ownerOrgId = orgUnitId;
                sw.WriteLine("ownerId :  " + ownerId + " ownerOrgId : " + ownerOrgId);



                string sub = ds33.Tables[0].Rows[0][8].ToString();
                string emp = ds33.Tables[0].Rows[0][6].ToString();
                newSubject = "維修立案" + "[" + "申請人:" + emp + ";" + "主旨:" + sub + "]";

                sw.WriteLine("newSubject :  " + newSubject);


                String RqNO = "";
                String MID = "";
                String Creator = "";
                String PartNo = "";
                String RqDate = "";
                String RqPlanDate = "";
                String RqOwner = "";
                String BU = "";
                String RqType = "";
                String RqConent = "";



                sw.WriteLine("sTESheetId : " + sTESheetId);
                sw.WriteLine("sTESheetNo : " + sTESheetNo);
                sw.WriteLine("sql : " + sql);



                if (ds33.Tables[0].Rows.Count > 0)
                {
                    RqNO = ds33.Tables[0].Rows[0][0].ToString();
                    MID = ds33.Tables[0].Rows[0][1].ToString();
                    Creator = ds33.Tables[0].Rows[0][2].ToString();
                    PartNo = ds33.Tables[0].Rows[0][3].ToString();
                    RqDate = ds33.Tables[0].Rows[0][4].ToString();
                    RqPlanDate = ds33.Tables[0].Rows[0][5].ToString();
                    RqOwner = ds33.Tables[0].Rows[0][6].ToString();
                    BU = ds33.Tables[0].Rows[0][7].ToString();
                    RqType = ds33.Tables[0].Rows[0][8].ToString();
                    RqConent = ds33.Tables[0].Rows[0][9].ToString();


                    //sw.WriteLine("EmpNo : " + EmpNo + " Reason : " + Reason + " WorkDate : " + WorkDate + "  " );

                    //單頭

                    agent.engine = engine;
                    agent.query("1=2");
                    dataObject = agent.defaultData.create();

                    dataObject.setData("GUID", headGUID);
                    dataObject.setData("ForwardHR", "N");
                    dataObject.setData("IS_LOCK", "N");
                    dataObject.setData("IS_DISPLAY", "Y");
                    dataObject.setData("DATA_STATUS", "Y");
                    dataObject.setData("Subject", newSubject);
                    dataObject.setData("SheetNo", sheetNo);
                    dataObject.setData("RqNO", RqNO);
                    dataObject.setData("MID", MID);
                    dataObject.setData("Creator", Creator);
                    dataObject.setData("PartNo", PartNo);
                    dataObject.setData("RqDate", RqDate);
                    dataObject.setData("RqPlanDate", RqPlanDate);
                    dataObject.setData("RqOwner", RqOwner);
                    dataObject.setData("BU", BU);
                    dataObject.setData("RqType", RqType);
                    dataObject.setData("RqConent", RqConent);
                    dataObject.setData("SheetNo", BillNo);
                }
                
                //單身
                sql2 = "SELECT * " +
                      "FROM [10.3.11.102].ITPlatForm_DB.dbo.IMS_Maintenance_Files  " +
                      "where BillNumber='" + BillNo + "'  ";
                ds44 = engine.getDataSet(sql2, "TEMP");
                
                sw.WriteLine("Detail No. " + ds44.Tables[0].Rows.Count);
                String FileName = "";
                String FileExtension = "";
                String Photo64 = "";
                if (ds44.Tables[0].Rows.Count > 0)
                {
                    DataObject objLine = null;
                    DataObjectSet dos = new DataObjectSet();
                    dos.setAssemblyName("WebServerProject");
                    dos.setChildClassString("WebServerProject.form.SQIT006.SQIT006B");
                    dos.setTableName("SQIT006B");
                    dos.isNameLess = true;
                    dos.loadFileSchema();
                    for (int i = 0; i < ds44.Tables[0].Rows.Count; i++)
                    {
                        objLine = dos.create();
                        objLine.setData("GUID", IDProcessor.getID(""));
                        objLine.setData("HeaderGUID", headGUID);
                        FileName = Convert.ToString(ds44.Tables[0].Rows[i]["FileName"]);
                        FileExtension = Convert.ToString(ds44.Tables[0].Rows[i]["FileExtension"]);
                        Photo64 = Convert.ToString(ds44.Tables[0].Rows[i]["Photo64"]);

                        objLine.setData("FileName", FileName);
                        objLine.setData("FileExtension", FileExtension);
                        objLine.setData("Photo64", Photo64);
                        sw.WriteLine("price ::: " + FileName + headGUID);

                        dos.add(objLine);
                    }
                    if (!dataObject.setChild("SQIT006B", dos))
                    {
                        sw.WriteLine("加入單身時發生錯誤");
                    }
                }


                dataXml = dataObject.saveXML();

                sw.WriteLine("Set Flow Variable ");
                flowVarXml += "<SQIT006>";
                flowVarXml += "<SQIT006>";
                flowVarXml += "<CREATOR DataType=\"java.lang.String\">" + creatorId + "</CREATOR>";
                flowVarXml += "</SQIT006>";
                flowVarXml += "</SQIT006>";

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
                                    "SYSTEM", //ApplicationID
                                    "EASYFLOW", //ModuleID
                                    "SQIT006", //ProcessPageID
                                    "", //sheetNo
                                    "<FORMID>SQIT006</FORMID>", //sheetnoparam
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
                                    "SQIT006", //firstParam
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
                            //newSubject = newSubject + "(" + wfnum + ")";
                            sql = "UPDATE SQIT006A SET SheetNo = '" + wfnum + "', Subject=N'" + newSubject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
                            engine.executeSQL(sql);
                            //更新系統表單主旨
                            sql = "UPDATE SMWYAAA SET SMWYAAA006=N'" + newSubject + "' WHERE SMWYAAA005='" + wfoid + "'";
                            engine.executeSQL(sql);
                            //更新GP主旨
                            sql = "update NaNa.dbo.ProcessInstance set subject = N'" + newSubject + "' where serialNumber = '" + wfoid + "'";
                            engine.executeSQL(sql);
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

    //維修設備領用

    [WebMethod]
    public string createIMWXSignFlow(string BillNo)
    {
        string result = "";
        string errMsg = "";
        DataSet ds = null;
        System.IO.StreamWriter sw = null;
        sw = new System.IO.StreamWriter(@"d:\temp\SQIT007.log", true, System.Text.Encoding.Default);
        sw.WriteLine("---------------------createSMPinfoDemandForTEFlow  No: " + BillNo + "-------------");

        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        IOFactory factory2 = new IOFactory();
        AbstractEngine engine = null;

        NLAgent agent = new NLAgent();
        string agentSchema = "WebServerProject.form.SQIT007.SQIT007Agent";

        string orgUnitId = "";
        string ownerId = "";
        string ownerOrgId = "";
        string sql = "";
        string sql2 = "";
        string sql3 = "";

        object ret = null;

        string[] values = null;
        DataObject dataObject = null;
        DataSet ds33 = null;
        DataSet ds44 = null;
        DataSet ds55 = null;
        DataSet ds77 = null;
        string newSubject = null;
        string sheetNo = "";

        try
        {
            sw.WriteLine("Try Load Schema ");
            agent.loadSchema(agentSchema);
            engine = factory.getEngine(acs.engineType, acs.connectString);
            sw.WriteLine("Load Schema End  engine : " + engine);


            if (errMsg.Equals(""))
            {
                string dataXml = null;
                string flowVarXml = ""; //流程參數
                string webUrl = null;
                string serviceUrl = null;
                string headGUID = "";
                string creatorId = "";
                string sTESheetNo = BillNo;
                string sTESheetId = "";
                string sqlaft = null;
                headGUID = IDProcessor.getID("");
                WSDLClient ws = null;

                //傳入單號獲取簽核頁面內容
                sql = "SELECT BillNumber,Barcode,WriterName+'/'+WriterOwnerName WtName, " +
                      "WriterDeptName,ApplicationTime1,ExpectedTime1,ApplicantName,WriterOrgShortName, " +
                      "IMTName,FormReason,Applicant,Instructions,TotalExpenditure,ISNULL(HandlerLastName,HandlerName) wxEmp " +
                      "FROM [10.3.11.102].ITPlatForm_DB.dbo.VIEW_Maintenance_Form  " +
                      "where BillNumber='" + BillNo + "' ";
                sqlaft = "select ArtiFicial from [10.3.11.102].ITPlatForm_DB.dbo.IMS_Maintenance_Repair where BillNumber='" + BillNo + "'";
                ds33 = engine.getDataSet(sql, "TEMP");
                if (ds33.Tables[0].Rows.Count > 0)
                {
                    creatorId = ds33.Tables[0].Rows[0][10].ToString();
                    sTESheetId = ds33.Tables[0].Rows[0][10].ToString();
                }

                ds77 = engine.getDataSet(sqlaft, "TEMP");
                //填表人
                values = getDeptInfo(engine, creatorId);

                sw.WriteLine("creatorId :  " + creatorId);
                orgUnitId = values[3];

                ownerId = creatorId;
                ownerOrgId = orgUnitId;
                sw.WriteLine("ownerId :  " + ownerId + " ownerOrgId : " + ownerOrgId);



                string sub = ds33.Tables[0].Rows[0][8].ToString();
                string emp = ds33.Tables[0].Rows[0][6].ToString();
                newSubject = "維修設備領用" + "[" + "申請人:" + emp + ";" + "主旨:" + sub + "]";

                sw.WriteLine("newSubject :  " + newSubject);


                String RqNO = "";
                String MID = "";
                String Creator = "";
                String PartNo = "";
                String RqDate = "";
                String RqPlanDate = "";
                String RqOwner = "";
                String BU = "";
                String RqType = "";
                String RqConent = "";
                String wxsm = "";
                String charge = "";
				String wxEmp = "";



                sw.WriteLine("sTESheetId : " + sTESheetId);
                sw.WriteLine("sTESheetNo : " + sTESheetNo);
                sw.WriteLine("sql : " + sql);



                if (ds33.Tables[0].Rows.Count > 0)
                {
                    RqNO = ds33.Tables[0].Rows[0][0].ToString();
                    MID = ds33.Tables[0].Rows[0][1].ToString();
                    Creator = ds33.Tables[0].Rows[0][2].ToString();
                    PartNo = ds33.Tables[0].Rows[0][3].ToString();
                    RqDate = ds33.Tables[0].Rows[0][4].ToString();
                    RqPlanDate = ds33.Tables[0].Rows[0][5].ToString();
                    RqOwner = ds33.Tables[0].Rows[0][6].ToString();
                    BU = ds33.Tables[0].Rows[0][7].ToString();
                    RqType = ds33.Tables[0].Rows[0][8].ToString();
                    RqConent = ds33.Tables[0].Rows[0][9].ToString();
                    if (ds77.Tables[0].Rows.Count > 0)
                    {
                        if (ds77.Tables[0].Rows[0]["ArtiFicial"].ToString() == "True")
                        {
                            wxsm = "人为损坏;" + "\n" + ds33.Tables[0].Rows[0][11].ToString();
                        }
                        else
                        {
                            wxsm = ds33.Tables[0].Rows[0][11].ToString();
                        }
                    }
                    else
                    {
                        wxsm = ds33.Tables[0].Rows[0][11].ToString(); 
                    }
                    charge = ds33.Tables[0].Rows[0][12].ToString();
					wxEmp = ds33.Tables[0].Rows[0][13].ToString();


                    //sw.WriteLine("EmpNo : " + ds77.Tables[0].Rows[0]["ArtiFicial"].ToString());

                    //單頭

                    agent.engine = engine;
                    agent.query("1=2");
                    dataObject = agent.defaultData.create();

                    dataObject.setData("GUID", headGUID);
                    dataObject.setData("ForwardHR", "N");
                    dataObject.setData("IS_LOCK", "N");
                    dataObject.setData("IS_DISPLAY", "Y");
                    dataObject.setData("DATA_STATUS", "Y");
                    dataObject.setData("Subject", newSubject);
                    dataObject.setData("SheetNo", sheetNo);
                    dataObject.setData("RqNO", RqNO);
                    dataObject.setData("MID", MID);
                    dataObject.setData("Creator", Creator);
                    dataObject.setData("PartNo", PartNo);
                    dataObject.setData("RqDate", RqDate);
                    dataObject.setData("RqPlanDate", RqPlanDate);
                    dataObject.setData("RqOwner", RqOwner);
                    dataObject.setData("BU", BU);
                    dataObject.setData("RqType", RqType);
                    dataObject.setData("RqConent", RqConent);
                    dataObject.setData("wxsm", wxsm);
                    dataObject.setData("charge", charge);
					dataObject.setData("wxEmp", wxEmp);
                    dataObject.setData("SheetNo", BillNo);
                }
                sql2 = "SELECT * "+
                      "FROM [10.3.11.102].ITPlatForm_DB.dbo.LOG_Maintenance_Computer_Details  " +
                      "where BillNumber='" + BillNo + "'  ORDER BY CreateTime ";
                 string sqltype = "select IsReplace from [10.3.11.102].ITPlatForm_DB.dbo.LOG_Maintenance_Computer where BillNumber ='" + BillNo + "'";
                string sql4="select * from [10.3.11.102].ITPlatForm_DB.dbo.LOG_Maintenance_Computer_Offsite where BillNumber='" + BillNo + "'";
                DataSet ds66 = null;
                ds66=engine.getDataSet(sqltype, "TEMP");
                //單身
                if(Convert.ToString(ds66.Tables[0].Rows[0]["IsReplace"])=="2")
                {
                    ds44 = engine.getDataSet(sql4, "TEMP");
                    String pjlb = "廠外維修";
                    String pinpai = "廠外維修";
                    String xinghao = "廠外維修";
                    String counts = "1";
                    String price = "";
                    String ActionN = "新增";
                    if (ds44.Tables[0].Rows.Count > 0)
                    {
                        DataObject objLine = null;
                        DataObjectSet dos = new DataObjectSet();
                        dos.setAssemblyName("WebServerProject");
                        dos.setChildClassString("WebServerProject.form.SQIT007.SQIT007B");
                        dos.setTableName("SQIT007B");
                        dos.isNameLess = true;
                        dos.loadFileSchema();
                        objLine = dos.create();

                        objLine.setData("GUID", IDProcessor.getID(""));
                        objLine.setData("HeaderGUID", headGUID);

                        price = Convert.ToString(ds44.Tables[0].Rows[0]["Expenditure"]);

                        objLine.setData("ActionN", ActionN);
                        objLine.setData("pjlb", pjlb);
                        objLine.setData("pinpai", pinpai);
                        objLine.setData("xinghao", xinghao);
                        objLine.setData("counts", counts);
                        objLine.setData("price", price);
                        sw.WriteLine("price ::: " + price + headGUID);

                        dos.add(objLine);
                        if (!dataObject.setChild("SQIT007B", dos))
                        {
                            sw.WriteLine("加入單身時發生錯誤");
                        }
                            
                      }

                }
                else
                {
                    ds44 = engine.getDataSet(sql2, "TEMP");
                    String ActionN = "";
                    String pjlb = "";
                    String pinpai = "";
                    String xinghao = "";
                    String counts = "";
                    String price = "";
                    if (ds44.Tables[0].Rows.Count > 0)
                    {
                        DataObject objLine = null;
                        DataObjectSet dos = new DataObjectSet();
                        dos.setAssemblyName("WebServerProject");
                        dos.setChildClassString("WebServerProject.form.SQIT007.SQIT007B");
                        dos.setTableName("SQIT007B");
                        dos.isNameLess = true;
                        dos.loadFileSchema();
                        for (int i = 0; i < ds44.Tables[0].Rows.Count; i++)
                        {
                            objLine = dos.create();
                            //objLine.setData("IS_LOCK", "N");
                            //objLine.setData("IS_DISPLAY", "Y");
                            //objLine.setData("DATA_STATUS", "Y");
                            objLine.setData("GUID", IDProcessor.getID(""));
                            objLine.setData("HeaderGUID", headGUID);
                            ActionN = Convert.ToString(ds44.Tables[0].Rows[i]["Action"]);
                            pjlb = Convert.ToString(ds44.Tables[0].Rows[i]["IMPName"]);
                            pinpai = Convert.ToString(ds44.Tables[0].Rows[i]["IMBName"]);
                            xinghao = Convert.ToString(ds44.Tables[0].Rows[i]["IMPLDes"]);
                            counts = Convert.ToString(ds44.Tables[0].Rows[i]["Qty"]);
                            price = Convert.ToString(ds44.Tables[0].Rows[i]["IMPLPrice"]);

                            objLine.setData("ActionN", ActionN);
                            objLine.setData("pjlb", pjlb);
                            objLine.setData("pinpai", pinpai);
                            objLine.setData("xinghao", xinghao);
                            objLine.setData("counts", counts);
                            objLine.setData("price", price);
                            sw.WriteLine("price ::: " + price + headGUID);

                            dos.add(objLine);
                        }
                        if (!dataObject.setChild("SQIT007B", dos))
                        {
                            sw.WriteLine("加入單身時發生錯誤");
                        }
                    }
                }
                //附件資料
                sql3 = "SELECT * " +
                      "FROM [10.3.11.102].ITPlatForm_DB.dbo.IMS_Maintenance_Files  " +
                      "where BillNumber='" + BillNo + "'  ";
                ds55 = engine.getDataSet(sql3, "TEMP");

                sw.WriteLine("Detail55 " + ds55.Tables[0].Rows.Count);
                String FileName = "";
                String FileExtension = "";
                String Photo64 = "";
                if (ds55.Tables[0].Rows.Count > 0)
                {
                    DataObject objLine = null;
                    DataObjectSet dos = new DataObjectSet();
                    dos.setAssemblyName("WebServerProject");
                    dos.setChildClassString("WebServerProject.form.SQIT007.SQIT007C");
                    dos.setTableName("SQIT007C");
                    dos.isNameLess = true;
                    dos.loadFileSchema();
                    for (int i = 0; i < ds55.Tables[0].Rows.Count; i++)
                    {
                        objLine = dos.create();
                        objLine.setData("GUID", IDProcessor.getID(""));
                        objLine.setData("HeaderGUID", headGUID);
                        FileName = Convert.ToString(ds55.Tables[0].Rows[i]["FileName"]);
                        FileExtension = Convert.ToString(ds55.Tables[0].Rows[i]["FileExtension"]);
                        Photo64 = Convert.ToString(ds55.Tables[0].Rows[i]["Photo64"]);

                        objLine.setData("FileName", FileName);
                        objLine.setData("FileExtension", FileExtension);
                        objLine.setData("Photo64", Photo64);
                        sw.WriteLine("price ::: " + FileName + headGUID);

                        dos.add(objLine);
                    }
                    if (!dataObject.setChild("SQIT007C", dos))
                    {
                        sw.WriteLine("加入附件時發生錯誤");
                    }
                }


                dataXml = dataObject.saveXML();

                sw.WriteLine("Set Flow Variable ");
                flowVarXml += "<SQIT007>";
                flowVarXml += "<SQIT007>";
                flowVarXml += "<CREATOR DataType=\"java.lang.String\">" + creatorId + "</CREATOR>";
                flowVarXml += "</SQIT007>";
                flowVarXml += "</SQIT007>";

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
                                    "SYSTEM", //ApplicationID
                                    "EASYFLOW", //ModuleID
                                    "SQIT007", //ProcessPageID
                                    "", //sheetNo
                                    "<FORMID>SQIT007</FORMID>", //sheetnoparam
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
                                    "SQIT007", //firstParam
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
                            //newSubject = newSubject + "(" + wfnum + ")";
                            sql = "UPDATE SQIT007A SET SheetNo = '" + wfnum + "', Subject=N'" + newSubject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
                            engine.executeSQL(sql);
                            //更新系統表單主旨
                            sql = "UPDATE SMWYAAA SET SMWYAAA006=N'" + newSubject + "' WHERE SMWYAAA005='" + wfoid + "'";
                            engine.executeSQL(sql);
                            //更新GP主旨
                            sql = "update NaNa.dbo.ProcessInstance set subject = N'" + newSubject + "' where serialNumber = '" + wfoid + "'";
                            engine.executeSQL(sql);

                            //更新附件資料
                            //string sourcePath = "\\\\\\\\10.3.11.198\\\\InformationRequirementsSystem\\\\";
                            //sw.WriteLine("sourcePath:" + sourcePath);
                            //string targetPath = "D:\\\\ECP\\\\WebFormPT\\\\FileStorage\\\\SQIT006\\\\" + wfnum + "\\\\";
                            //sw.WriteLine("targetPath:" + targetPath);
                            //string now = DateTimeUtility.getSystemTime2(null);
                            //sql = " SELECT BillNumber,SUBSTRING(isnull(FileName,''),1,CHARINDEX('.',isnull(FileName,''))-1) FileName1,FileName,SUBSTRING(isnull(FileName,''),CHARINDEX('.',isnull(FileName,''))+1,len(isnull(FileName,''))-charindex('-',isnull(FileName,''))) FileExt FROM [10.3.11.102].ITPlatForm_DB.dbo.IRS_Requirements_Files WHERE BillNumber  = '" + BillNo + "'";
                            //sw.WriteLine("sql:" + sql);
                            //string userId = values[0];
                            //DataSet ds1 = null;
                            //ds1 = engine.getDataSet(sql, "TEMP");
                            //if (ds1.Tables[0].Rows.Count > 0)
                            //{
                            //    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                            //    {
                            //        string FileGUID = IDProcessor.getID("");
                            //        sw.WriteLine(" FileGUID :" + FileGUID);
                            //        string IDFileName = headGUID;
                            //        sw.WriteLine(" IDFileName :" + IDFileName);
                            //        string RealFileName = ds1.Tables[0].Rows[i]["FileName1"].ToString();
                            //        string SFileName = ds1.Tables[0].Rows[i]["FileName"].ToString();
                            //        sw.WriteLine(" RealFileName: " + RealFileName);
                            //        string FileExt = ds1.Tables[0].Rows[i]["FILEEXT"].ToString();
                            //        string FileDesc = "";
                            //        string sourceFile = System.IO.Path.Combine(sourcePath, SFileName);
                            //        string destFile = System.IO.Path.Combine(targetPath, FileGUID);
                            //        sw.WriteLine(" 44444 " + sourceFile + "/n" + destFile);
                            //        if (!System.IO.Directory.Exists(targetPath))
                            //        {
                            //            System.IO.Directory.CreateDirectory(targetPath);
                            //        }
                            //        sw.WriteLine(" 55555 ");
                            //        System.IO.File.Copy(sourceFile, destFile, true);
                            //        sw.WriteLine(" Copy data End ");
                            //        sql = "Insert into WebFormPT.dbo.FILEITEM values ('" + FileGUID + "','" + headGUID + "','";
                            //        sql += FileGUID + "','" + RealFileName + "','" + FileExt + "','','" + FileDesc + "','SQIT006','";
                            //        sql += wfnum + "','" + userId + "','" + now + "','','','','')";
                            //        sw.WriteLine("  Insert Data Sql : " + sql);
                            //        engine.executeSQL(sql);


                            //    }
                            //}
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

    //人員外出

    [WebMethod]
    public string createPerOutFlow(string BillNo)
    {
        string result = "";
        string errMsg = "";
        DataSet ds = null;
        System.IO.StreamWriter sw = null;
        sw = new System.IO.StreamWriter(@"d:\temp\EG0108.log", true, System.Text.Encoding.Default);
        sw.WriteLine("---------------------createSMPinfoDemandForTEFlow  No: " + BillNo + "-------------");

        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        IOFactory factory2 = new IOFactory();
        AbstractEngine engine = null;

        NLAgent agent = new NLAgent();
        string agentSchema = "WebServerProject.form.EG0108.EG0108Agent";

        string orgUnitId = "";
        string ownerId = "";
        string ownerOrgId = "";
        string sql = "";
        string sql2 = "";
        string sqlad = "";

        object ret = null;

        string[] values = null;
        DataObject dataObject = null;
        DataSet ds33 = null;
        DataSet ds44 = null;
        DataSet dsad = null;
        string newSubject = null;
        string sheetNo = "";

        try
        {
            sw.WriteLine("Try Load Schema ");
            agent.loadSchema(agentSchema);
            engine = factory.getEngine(acs.engineType, acs.connectString);
            sw.WriteLine("Load Schema End  engine : " + engine);


            if (errMsg.Equals(""))
            {
                string dataXml = null;
                string flowVarXml = ""; //流程參數
                string webUrl = null;
                string serviceUrl = null;
                string headGUID = "";
                string creatorId = "";
                string sTESheetNo = BillNo;
                string sTESheetId = "";
                headGUID = IDProcessor.getID("");
                WSDLClient ws = null;

                //傳入單號獲取簽核頁面內容
                sql = "select BillNumber,ApplicationTime1,WriterName+'/'+WriterOwnerName owners,WriterDeptName,Writer,IsMultiple  " +
                      "from [10.3.11.102].ITPlatForm_DB.dbo.VIEW_Personnelout_Form " +
                      "where BillNumber='" + BillNo + "' ";
                ds33 = engine.getDataSet(sql, "TEMP");
                //單身資料
                sql2 = "SELECT * " +
                      "from [10.3.11.102].ITPlatForm_DB.dbo.VIEW_Personnelout_Details  " +
                      "where BillNumber='" + BillNo + "'  ";
                ds44 = engine.getDataSet(sql2, "TEMP");

                if (ds33.Tables[0].Rows[0]["IsMultiple"].ToString() == "1")
                {
                    if (ds33.Tables[0].Rows.Count > 0)
                    {
                        creatorId = ds33.Tables[0].Rows[0]["Writer"].ToString();
                        sTESheetId = ds33.Tables[0].Rows[0]["Writer"].ToString();
                    }
                    else
                    {
                        creatorId = "";
                        sTESheetId = "";
                    }
                }
                else
                {
                    if (ds44.Tables[0].Rows.Count > 0)
                    {
                        string sqr = ds44.Tables[0].Rows[0]["Applicant"].ToString();
                        sqlad = @"select a.EmpNo,ADName,MyField3 from [10.3.11.96].MIS_DB.dbo.PerEmployeeMD a,[10.3.11.96].MIS_DB.dbo.PerDepartMD b where a.EmpNo='" + sqr + "'and a.PartNo=b.PartNo and b.Status='Y'";
                        dsad = engine.getDataSet(sqlad, "TEMP");
                        string sqradname = dsad.Tables[0].Rows[0]["ADName"].ToString();
                        if (!string.IsNullOrEmpty(sqradname) || sqradname != "")
                        {
                            creatorId = sqr;
                            sTESheetId = sqr;
                        }
                        else
                        {
                            creatorId = dsad.Tables[0].Rows[0]["MyField3"].ToString();
                            sTESheetId = dsad.Tables[0].Rows[0]["MyField3"].ToString();
                        }

                    }
                    else
                    {
                        creatorId = "";
                        sTESheetId = "";
                    }
                }
                

                //填表人
                 values = getDeptInfo(engine, creatorId);

                sw.WriteLine("creatorId :  " + creatorId);
                orgUnitId = values[3];

                ownerId = creatorId;
                ownerOrgId = orgUnitId;
                //sw.WriteLine("ownerId :  " + ownerId + " ownerOrgId : " + ownerOrgId);

                string emp = ds44.Tables[0].Rows[0]["ApplicantName"].ToString();
                newSubject = "人員外出申請" + "[" + "申請人:" + emp+ "]";

                sw.WriteLine("newSubject :  " + newSubject);


                String RqNO = "";
                String RqTime = "";
                String RqEmp = "";
                String PartNo = "";

                if (ds33.Tables[0].Rows.Count > 0)
                {
                    RqNO = ds33.Tables[0].Rows[0]["BillNumber"].ToString();
                    RqTime = ds33.Tables[0].Rows[0]["ApplicationTime1"].ToString();
                    RqEmp = ds33.Tables[0].Rows[0]["owners"].ToString();
                    PartNo = ds33.Tables[0].Rows[0]["WriterDeptName"].ToString();

                    //sw.WriteLine("RqNO : " + RqNO + " RqTime : " + RqTime + " RqEmp : " + RqEmp + "  ");

                    //單頭

                    agent.engine = engine;
                    agent.query("1=2");
                    dataObject = agent.defaultData.create();

                    dataObject.setData("GUID", headGUID);
                    dataObject.setData("ForwardHR", "N");
                    dataObject.setData("IS_LOCK", "N");
                    dataObject.setData("IS_DISPLAY", "Y");
                    dataObject.setData("DATA_STATUS", "Y");
                    dataObject.setData("Subject", newSubject);
                    dataObject.setData("SheetNo", sheetNo);
                    dataObject.setData("RqNO", RqNO);
                    dataObject.setData("RqTime", RqTime);
                    dataObject.setData("RqEmp", RqEmp);
                    dataObject.setData("PartNo", PartNo);
                    dataObject.setData("SheetNo", BillNo);
                }

                //單身
                

                //sw.WriteLine("Detail No. " + ds44.Tables[0].Rows.Count);
                String EmpNo = "";
                String EmpName = "";
                String OutType = "";
                String OutReason = "";
                String OutTime = "";
                String InTime = "";
                String OutPlace = "";
                                        
                    if (ds44.Tables[0].Rows.Count > 0)
                    {
                        DataObject objLine = null;
                        DataObjectSet dos = new DataObjectSet();
                        dos.setAssemblyName("WebServerProject");
                        dos.setChildClassString("WebServerProject.form.EG0108.EG0108B");
                        dos.setTableName("EG0108B");
                        dos.isNameLess = true;
                        dos.loadFileSchema();
                        for (int i = 0; i < ds44.Tables[0].Rows.Count; i++)
                        {
                            objLine = dos.create();
                            objLine.setData("GUID", IDProcessor.getID(""));
                            objLine.setData("HeaderGUID", headGUID);
                            EmpNo = ds44.Tables[0].Rows[i]["Applicant"].ToString();
                            EmpName = ds44.Tables[0].Rows[i]["ApplicantName"].ToString();
                            if(ds33.Tables[0].Rows[0]["IsMultiple"].ToString() == "2")
                            {
                                OutType = "白名單"; 
                                OutTime = ds44.Tables[0].Rows[i]["EffectiveBegintime1"].ToString();
                                InTime = ds44.Tables[0].Rows[i]["EffectiveEndtime1"].ToString();
                            }
                            else
                            {
                               OutType = ds44.Tables[0].Rows[i]["OutType1"].ToString(); 
                               OutTime = ds44.Tables[0].Rows[i]["ScheduleOutTime1"].ToString();
                                if (ds44.Tables[0].Rows[i]["IsReturn"].ToString() == "0")
                                {
                                    InTime = "不返廠";
                                }
                                else
                                {
                                    InTime = ds44.Tables[0].Rows[i]["ScheduleReturnTime1"].ToString();
                                }
                            }
                            
                            OutReason = ds44.Tables[0].Rows[i]["OutReason"].ToString();
                            if (ds44.Tables[0].Rows[i]["Destination"].ToString() == "")
                            {
                                OutPlace = "無";
                            }
                            else
                            {
                                OutPlace = ds44.Tables[0].Rows[i]["Destination"].ToString();
                            }
                            

                            objLine.setData("EmpNo", EmpNo);
                            objLine.setData("EmpName", EmpName);
                            objLine.setData("OutType", OutType);
                            objLine.setData("OutReason", OutReason);
                            objLine.setData("OutTime", OutTime);
                            objLine.setData("InTime", InTime);
                            objLine.setData("OutPlace", OutPlace);
                            //sw.WriteLine("qt ::: " + EmpNo + OutType);

                            dos.add(objLine);
                        }
                        if (!dataObject.setChild("EG0108B", dos))
                        {
                            sw.WriteLine("加入單身時發生錯誤");
                        }
                    }      


                dataXml = dataObject.saveXML();
                //填表人
                string isbb = "0";
                string bmzg = "";
                string isbmd = "";
                string[] Managers = getUserManagerInfoID(engine, creatorId);

                string sqrpd = ds44.Tables[0].Rows[0]["Applicant"].ToString();
                if (sqrpd == "Q1200364" || sqrpd == "Q1606675" || sqrpd == "Q1100101")
                {
                    bmzg = "Q1508126";
                }
                else
                {
                    bmzg = Managers[1];
                }
                
                if (ds33.Tables[0].Rows[0]["IsMultiple"].ToString() == "2")
                {
                    isbmd = "1"; 
                }

                sw.WriteLine("Set Flow Variable ");
                flowVarXml += "<EG0108>";
                flowVarXml += "<EG0108>";
                flowVarXml += "<bmzg DataType=\"java.lang.String\">" + bmzg + "</bmzg>";
                flowVarXml += "<isbb DataType=\"java.lang.String\">" + isbb + "</isbb>";
                flowVarXml += "<isbmd DataType=\"java.lang.String\">" + isbmd + "</isbmd>";
                flowVarXml += "</EG0108>";
                flowVarXml += "</EG0108>";

                //建立標準Service
                sw.WriteLine("Create Flow Service " + bmzg + creatorId);
                webUrl = GlobalProperty.getProperty("simplo", "EcpWebUrl");
                serviceUrl = webUrl + "/WebService/DSCFlowService.asmx";
                ws = new WSDLClient(serviceUrl);
                ws.dllPath = Utility.G_GetTempPath();
                ws.build(false);


                sw.WriteLine("Create Flow Service call");
                //sw.WriteLine("newSubject " + newSubject + " creatorId " + creatorId + " orgUnitId " + orgUnitId + " ownerId "+ ownerId + " ownerOrgId " + ownerOrgId);
                // sw.WriteLine("agentSchema : " + agentSchema + " dataXml : " + dataXml +" flowVarXml : " + flowVarXml);
                ret = ws.callSync("createFlow", //呼叫方法
                                    "SYSTEM", //ApplicationID
                                    "EASYFLOW", //ModuleID
                                    "EG0108", //ProcessPageID
                                    "", //sheetNo
                                    "<FORMID>EG0108</FORMID>", //sheetnoparam
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
                                    "EG0108", //firstParam
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
                            //newSubject = newSubject + "(" + wfnum + ")";
                            sql = "UPDATE EG0108A SET SheetNo = '" + wfnum + "', Subject=N'" + newSubject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
                            engine.executeSQL(sql);
                            //更新系統表單主旨
                            sql = "UPDATE SMWYAAA SET SMWYAAA006=N'" + newSubject + "' WHERE SMWYAAA005='" + wfoid + "'";
                            engine.executeSQL(sql);
                            //更新GP主旨
                            sql = "update NaNa.dbo.ProcessInstance set subject = N'" + newSubject + "' where serialNumber = '" + wfoid + "'";
                            engine.executeSQL(sql);
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

//電腦預報廢

    [WebMethod]
    public string createComputerScrapSignFlow(string BillNo)
    {
        string result = "";
        string errMsg = "";
        DataSet ds = null;
        System.IO.StreamWriter sw = null;
        sw = new System.IO.StreamWriter(@"d:\temp\SQIT008.log", true, System.Text.Encoding.Default);
        sw.WriteLine("---------------------createSMPinfoDemandForTEFlow  No: " + BillNo + "-------------");

        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        IOFactory factory2 = new IOFactory();
        AbstractEngine engine = null;

        NLAgent agent = new NLAgent();
        string agentSchema = "WebServerProject.form.SQIT008.SQIT008Agent";

        string orgUnitId = "";
        string ownerId = "";
        string ownerOrgId = "";
        string sql = "";
        string sql2 = "";
        string sql3 = "";

        object ret = null;

        string[] values = null;
        DataObject dataObject = null;
        DataSet ds33 = null;
        DataSet ds44 = null;
        DataSet ds55 = null;
        string newSubject = null;
        string sheetNo = "";

        try
        {
            sw.WriteLine("Try Load Schema ");
            agent.loadSchema(agentSchema);
            engine = factory.getEngine(acs.engineType, acs.connectString);
            sw.WriteLine("Load Schema End  engine : " + engine);


            if (errMsg.Equals(""))
            {
                string dataXml = null;
                string flowVarXml = ""; //流程參數
                string webUrl = null;
                string serviceUrl = null;
                string headGUID = "";
                string creatorId = "";
                string sTESheetNo = BillNo;
                string sTESheetId = "";
                headGUID = IDProcessor.getID("");
                WSDLClient ws = null;

                //傳入單號獲取簽核頁面內容
                sql = "SELECT BillNumber,Barcode,WriterName+'/'+WriterOwnerName WtName, " +
                      "WriterDeptName,ApplicationTime1,ExpectedTime1,ApplicantName,WriterOrgShortName, " +
                      "IMTName,FormReason,Applicant,Instructions,TotalExpenditure,ISNULL(HandlerLastName,HandlerName) wxEmp " +
                      "FROM [10.3.11.102].ITPlatForm_DB.dbo.VIEW_Maintenance_Form  " +
                      "where BillNumber='" + BillNo + "' ";
                ds33 = engine.getDataSet(sql, "TEMP");
                if (ds33.Tables[0].Rows.Count > 0)
                {
                    creatorId = ds33.Tables[0].Rows[0][10].ToString();
                    sTESheetId = ds33.Tables[0].Rows[0][10].ToString();
                }

                //填表人
                values = getDeptInfo(engine, creatorId);

                sw.WriteLine("creatorId :  " + creatorId);
                orgUnitId = values[3];

                ownerId = creatorId;
                ownerOrgId = orgUnitId;
                sw.WriteLine("ownerId :  " + ownerId + " ownerOrgId : " + ownerOrgId);



                string sub = ds33.Tables[0].Rows[0][8].ToString();
                string emp = ds33.Tables[0].Rows[0][6].ToString();
                newSubject = "電腦預報廢申請" + "[" + "申請人:" + emp + ";" + "主旨:" + sub + "]";

                sw.WriteLine("newSubject :  " + newSubject);


                String RqNO = "";
                String MID = "";
                String Creator = "";
                String PartNo = "";
                String RqDate = "";
                String RqPlanDate = "";
                String RqOwner = "";
                String BU = "";
                String RqType = "";
                String RqConent = "";
                String wxsm = "";



                sw.WriteLine("sTESheetId : " + sTESheetId);
                sw.WriteLine("sTESheetNo : " + sTESheetNo);
                sw.WriteLine("sql : " + sql);



                if (ds33.Tables[0].Rows.Count > 0)
                {
                    RqNO = ds33.Tables[0].Rows[0][0].ToString();
                    MID = ds33.Tables[0].Rows[0][1].ToString();
                    Creator = ds33.Tables[0].Rows[0][2].ToString();
                    PartNo = ds33.Tables[0].Rows[0][3].ToString();
                    RqDate = ds33.Tables[0].Rows[0][4].ToString();
                    RqPlanDate = ds33.Tables[0].Rows[0][5].ToString();
                    RqOwner = ds33.Tables[0].Rows[0][6].ToString();
                    BU = ds33.Tables[0].Rows[0][7].ToString();
                    RqType = ds33.Tables[0].Rows[0][8].ToString();
                    RqConent = ds33.Tables[0].Rows[0][9].ToString();
                    wxsm = ds33.Tables[0].Rows[0][11].ToString(); 
                    //sw.WriteLine("EmpNo : " + ds77.Tables[0].Rows[0]["ArtiFicial"].ToString());

                    //單頭

                    agent.engine = engine;
                    agent.query("1=2");
                    dataObject = agent.defaultData.create();

                    dataObject.setData("GUID", headGUID);
                    dataObject.setData("ForwardHR", "N");
                    dataObject.setData("IS_LOCK", "N");
                    dataObject.setData("IS_DISPLAY", "Y");
                    dataObject.setData("DATA_STATUS", "Y");
                    dataObject.setData("Subject", newSubject);
                    dataObject.setData("SheetNo", sheetNo);
                    dataObject.setData("RqNO", RqNO);
                    dataObject.setData("MID", MID);
                    dataObject.setData("Creator", Creator);
                    dataObject.setData("PartNo", PartNo);
                    dataObject.setData("RqDate", RqDate);
                    dataObject.setData("RqPlanDate", RqPlanDate);
                    dataObject.setData("RqOwner", RqOwner);
                    dataObject.setData("BU", BU);
                    dataObject.setData("RqType", RqType);
                    dataObject.setData("RqConent", RqConent);
                    dataObject.setData("wxsm", wxsm);

                    dataObject.setData("SheetNo", BillNo);
                }
                sql2 = "SELECT b.* "+
                      "from [10.3.11.102].ITPlatForm_DB.dbo.IMS_Maintenance_EquipmentList a,[10.3.11.102].ITPlatForm_DB.dbo.IMS_Maintenance_EquipmentList_Details b " +
                      "where a.IMELID=b.IMELID and NewBarcode='" + MID + "' ";

	sw.WriteLine("sql : " + sql2);
                //單身
                
                    ds44 = engine.getDataSet(sql2, "TEMP");
                    String pjlb = "";
                    String pinpai = "";
                    String xinghao = "";

                    if (ds44.Tables[0].Rows.Count > 0)
                    {
                        DataObject objLine = null;
                        DataObjectSet dos = new DataObjectSet();
                        dos.setAssemblyName("WebServerProject");
                        dos.setChildClassString("WebServerProject.form.SQIT008.SQIT008B");
                        dos.setTableName("SQIT008B");
                        dos.isNameLess = true;
                        dos.loadFileSchema();
                        for (int i = 0; i < ds44.Tables[0].Rows.Count; i++)
                        {
                            objLine = dos.create();
                            //objLine.setData("IS_LOCK", "N");
                            //objLine.setData("IS_DISPLAY", "Y");
                            //objLine.setData("DATA_STATUS", "Y");
                            objLine.setData("GUID", IDProcessor.getID(""));
                            objLine.setData("HeaderGUID", headGUID);
                            pjlb = Convert.ToString(ds44.Tables[0].Rows[i]["IMPName"]);
                            pinpai = Convert.ToString(ds44.Tables[0].Rows[i]["IMBName"]);
                            xinghao = Convert.ToString(ds44.Tables[0].Rows[i]["IMPLDes"]);

                            objLine.setData("pjlb", pjlb);
                            objLine.setData("pinpai", pinpai);
                            objLine.setData("xinghao", xinghao);

                            dos.add(objLine);
                        }
                        if (!dataObject.setChild("SQIT008B", dos))
                        {
                            sw.WriteLine("加入單身時發生錯誤");
                        }
                    }

                dataXml = dataObject.saveXML();

                sw.WriteLine("Set Flow Variable ");
                flowVarXml += "<SQIT008>";
                flowVarXml += "<SQIT008>";
                flowVarXml += "<CREATOR DataType=\"java.lang.String\">" + creatorId + "</CREATOR>";
                flowVarXml += "</SQIT008>";
                flowVarXml += "</SQIT008>";

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
                                    "SYSTEM", //ApplicationID
                                    "EASYFLOW", //ModuleID
                                    "SQIT008", //ProcessPageID
                                    "", //sheetNo
                                    "<FORMID>SQIT008</FORMID>", //sheetnoparam
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
                                    "SQIT008", //firstParam
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
                            //newSubject = newSubject + "(" + wfnum + ")";
                            sql = "UPDATE SQIT008A SET SheetNo = '" + wfnum + "', Subject=N'" + newSubject + "' WHERE GUID = (SELECT SMWYAAA019 FROM SMWYAAA WHERE SMWYAAA005='" + wfoid + "')";
                            engine.executeSQL(sql);
                            //更新系統表單主旨
                            sql = "UPDATE SMWYAAA SET SMWYAAA006=N'" + newSubject + "' WHERE SMWYAAA005='" + wfoid + "'";
                            engine.executeSQL(sql);
                            //更新GP主旨
                            sql = "update NaNa.dbo.ProcessInstance set subject = N'" + newSubject + "' where serialNumber = '" + wfoid + "'";
                            engine.executeSQL(sql);

                            //更新附件資料
                            //string sourcePath = "\\\\\\\\10.3.11.198\\\\InformationRequirementsSystem\\\\";
                            //sw.WriteLine("sourcePath:" + sourcePath);
                            //string targetPath = "D:\\\\ECP\\\\WebFormPT\\\\FileStorage\\\\SQIT006\\\\" + wfnum + "\\\\";
                            //sw.WriteLine("targetPath:" + targetPath);
                            //string now = DateTimeUtility.getSystemTime2(null);
                            //sql = " SELECT BillNumber,SUBSTRING(isnull(FileName,''),1,CHARINDEX('.',isnull(FileName,''))-1) FileName1,FileName,SUBSTRING(isnull(FileName,''),CHARINDEX('.',isnull(FileName,''))+1,len(isnull(FileName,''))-charindex('-',isnull(FileName,''))) FileExt FROM [10.3.11.102].ITPlatForm_DB.dbo.IRS_Requirements_Files WHERE BillNumber  = '" + BillNo + "'";
                            //sw.WriteLine("sql:" + sql);
                            //string userId = values[0];
                            //DataSet ds1 = null;
                            //ds1 = engine.getDataSet(sql, "TEMP");
                            //if (ds1.Tables[0].Rows.Count > 0)
                            //{
                            //    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                            //    {
                            //        string FileGUID = IDProcessor.getID("");
                            //        sw.WriteLine(" FileGUID :" + FileGUID);
                            //        string IDFileName = headGUID;
                            //        sw.WriteLine(" IDFileName :" + IDFileName);
                            //        string RealFileName = ds1.Tables[0].Rows[i]["FileName1"].ToString();
                            //        string SFileName = ds1.Tables[0].Rows[i]["FileName"].ToString();
                            //        sw.WriteLine(" RealFileName: " + RealFileName);
                            //        string FileExt = ds1.Tables[0].Rows[i]["FILEEXT"].ToString();
                            //        string FileDesc = "";
                            //        string sourceFile = System.IO.Path.Combine(sourcePath, SFileName);
                            //        string destFile = System.IO.Path.Combine(targetPath, FileGUID);
                            //        sw.WriteLine(" 44444 " + sourceFile + "/n" + destFile);
                            //        if (!System.IO.Directory.Exists(targetPath))
                            //        {
                            //            System.IO.Directory.CreateDirectory(targetPath);
                            //        }
                            //        sw.WriteLine(" 55555 ");
                            //        System.IO.File.Copy(sourceFile, destFile, true);
                            //        sw.WriteLine(" Copy data End ");
                            //        sql = "Insert into WebFormPT.dbo.FILEITEM values ('" + FileGUID + "','" + headGUID + "','";
                            //        sql += FileGUID + "','" + RealFileName + "','" + FileExt + "','','" + FileDesc + "','SQIT006','";
                            //        sql += wfnum + "','" + userId + "','" + now + "','','','','')";
                            //        sw.WriteLine("  Insert Data Sql : " + sql);
                            //        engine.executeSQL(sql);


                            //    }
                            //}
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

    /// <summary>
    /// 取得使用者主管資訊, [0]:OID, [1]: id, [2]: userName
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userID">工號</param>
    /// <returns>string[]</returns>
    protected string[] getUserManagerInfoID(AbstractEngine engine, string userID)
    {
        string[] result = new string[3];
        string Sql = " select u.OID, u.id, u.userName from Functions f, Users u where f.occupantOID = (select OID from Users where id='" + userID + "') and  f.specifiedManagerOID = u.OID and f.isMain='1'";
        DataSet ds = engine.getDataSet(Sql, "TEMP");
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